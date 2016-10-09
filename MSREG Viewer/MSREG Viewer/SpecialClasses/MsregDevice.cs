using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Klocman.Extensions;
using Klocman.IO;
using Klocman.Subsystems;

namespace MSREG.Viewer.SpecialClasses
{

    #region Enumerations

    [Flags]
    public enum MsregAlarm
    {
        None = 0, // Whole value equals 0 if no alarm is set
        BootFailed = 1 << 1,
        SensorFailure = 1 << 2,
        TemperatureRegulation = 1 << 3,
        HumidityRegulation = 1 << 4,
        Other = 1 << 7
    }

    public enum MsregDeviceType
    {
        Msr33
        //MSR11
    }

    public enum MsregEditSettingChoices
    {
        Nastawa = '1',
        Histereza = '2',
        OdstepPrzelaczen = '3',
        PoziomAlarmowy = '4',
        NormallyClosed = '5'
    }

    public enum MsregInputCommand
    {
        VersionInfo = 'v',
        TemperatureSettings = 't',
        HumiditySettings = 'h',
        EditSetting = 'e',
        SaveSettings = 's',
        RestoreDefaultSettings = 'd',
        ReadSettings = 'r',
        BaudRateAutocorrect = ' ',
        RestartDevice = '!'
    }

    public enum MsregOutputCommand
    {
        Measurement = 'T',
        CurrentAlarm = 'A', // Check alarm codes
        SensorError = 'E',
        VersionInfo = 'v',
        EditSettingReply = 'e',
        SaveSettingsReply = 's',
        RestoreDefaultSettingsReply = 'd',
        ReadSettingsReply = 'r',
        BaudRateCorrectReply = 'b',
        TemperatureSettingsReply = 't',
        HumiditySettingsReply = 'h'
    }

    public enum RegulationResult
    {
        DoNothing = 0,
        Disable = 1,
        AboveSetting = 2,
        AboveSettingAlarm = 3,
        BelowSetting = 4,
        BelowSettingAlarm = 5
    }

    public enum TargetMeasurementType
    {
        Temperature = 't',
        Humidity = 'h'
    }

    #endregion Enumerations

    public class Msr33Measurement
    {
        #region Constructors

        public Msr33Measurement(double temp, double hum, RegulationResult tempReg, RegulationResult humReg)
        {
            Temperature = temp;
            Humidity = hum;
            TemperatureRegulationResult = tempReg;
            HumidityRegulationResult = humReg;
        }

        #endregion Constructors

        #region Properties

        public double Humidity { get; set; }

        public RegulationResult HumidityRegulationResult { get; set; }

        public bool IsValid => Temperature > -80 && Temperature < 200
                               && Humidity >= 0 && Humidity <= 100
                               && TemperatureRegulationResult >= 0 && TemperatureRegulationResult < (RegulationResult) 200
                               && HumidityRegulationResult >= 0 && HumidityRegulationResult < (RegulationResult) 200;

        public double Temperature { get; set; }

        public RegulationResult TemperatureRegulationResult { get; set; }

        #endregion Properties
    }

    public class MsregDeviceInfo
    {
        #region Properties

        /*public string CopyrightInformation
        {
            get;
            set;
        }*/

        public string ExtraInfo { get; set; }

        public DateTime FirmwareBuildTime { get; set; }

        public string FirmwareVersion { get; set; }

        public string HardwareRevision { get; set; }

        public MsregDeviceType Type { get; set; }

        #endregion Properties

        #region Methods

        public override string ToString() => string.Format("{0}, rev{1}, v{2}", Type, HardwareRevision, FirmwareVersion);

        public string ToFullString() => string.Format(
            "Model regulatora: {0}\nRewizja sprzętu: {1}\nWersja firmware: {2}\nDodatkowe informacje:\n{3}",
            Type, HardwareRevision, FirmwareVersion, ExtraInfo);

        #endregion Methods
    }

    public class RegulationSettings
    {
        #region Constructors

        public RegulationSettings()
        {
            AlarmOffset = -1000;
            Histeresis = -1000;
            TargetValue = -1000;
            WaitTime = -1000;
            NormallyClosed = false;
        }

        #endregion Constructors

        #region Properties

        public double AlarmOffset { get; set; }

        public double Histeresis { get; set; }

        public bool IsComplete => AlarmOffset != -1000
                                  && Histeresis != -1000
                                  && TargetValue != -1000
                                  && WaitTime != -1000;

        public bool NormallyClosed { get; set; }

        public double TargetValue { get; set; }

        public double WaitTime { get; set; }

        #endregion Properties
    }

    public class MsregDevice : IDisposable
    {
        #region Constructors

        public MsregDevice()
        {
            AutoReconnect = false;
        }

        #endregion Constructors

        #region Fields

        private static readonly CultureInfo ParseCultureInfo = new CultureInfo("en-US");

        private readonly ConcurrentDictionary<MsregOutputCommand, string> _ackReplies =
            new ConcurrentDictionary<MsregOutputCommand, string>();

        private readonly object _commandSendLock = new object();
        private Thread _lineReceiver;
        private SafeSerialPort _target;

        #endregion Fields

        #region Events

        public event Action<MsregDevice> Connected;

        public event Action<MsregDevice, string> Disconnected;

        public event Action<MsregDevice, string> InputParseFailed;

        public event Action<MsregDevice, MsregAlarm> ReceivedAlarmValue;

        public event Action<MsregDevice, RegulationSettings> ReceivedHumiditySettings;

        public event Action<MsregDevice, Msr33Measurement> ReceivedMeasurement;

        public event Action<MsregDevice, RegulationSettings> ReceivedTemperatureSettings;

        #endregion Events

        #region Properties

        public MsregDeviceInfo ConnectedDeviceInfo { get; private set; }

        public bool IsConnected => (_target != null) && _target.IsOpen && ConnectedDeviceInfo != null;

        public string TargetPort => _target == null ? string.Empty : _target.PortName;

        public bool AutoReconnect { get; set; }

        private string LastConnectedPort { get; set; }

        #endregion Properties

        #region Methods

        public bool Connect() => !string.IsNullOrEmpty(LastConnectedPort) && Connect(LastConnectedPort);

        public bool Connect(string portName)
        {
            lock (_commandSendLock)
            {
                AppLog.Write(Strings.Default.ConnectingToDevice + portName, LogEntryType.Info,
                    LogEntrySource.MsregDevice, TargetPort);

                SetupComPort();

                try
                {
                    _target.PortName = portName;
                    _target.Open();
                }
                catch (Exception e)
                {
                    Disconnect(e.Message);
                    return false;
                }

                if (_target.IsOpen)
                {
                    for (var i = 0; i < 2; i++)
                    {
                        var tempInfo = GetDeviceInfo();
                        if (tempInfo != null)
                        {
                            ConnectedDeviceInfo = tempInfo;
                            break;
                        }
                    }

                    if (ConnectedDeviceInfo == null)
                    {
                        Disconnect(Strings.Default.DisconnectFailedToRespond);
                        return false;
                    }

                    LastConnectedPort = portName;
                    OnConnected();
                    return true;
                }
                Disconnect(Strings.Default.DisconnectFailedToOpen);
                return false;
            }
        }

        public void Disconnect()
        {
            Disconnect(Strings.Default.DisconnectByUser);
        }

        public void Dispose()
        {
            Disconnect(Strings.Default.DisconnectDisposed);
        }

        public MsregDeviceInfo GetDeviceInfo()
        {
            lock (_commandSendLock)
            {
                SendCommand(MsregInputCommand.VersionInfo);

                string result = null;
                for (var i = 0; i < 20; i++)
                {
                    string tempOut;
                    if (_ackReplies.TryRemove(MsregOutputCommand.VersionInfo, out tempOut))
                    {
                        result = tempOut.Trim();
                        break;
                    }
                    Thread.Sleep(10);
                }

                if (string.IsNullOrEmpty(result))
                    return null;

                try
                {
                    var parseResult = new MsregDeviceInfo();
                    //List<string> extraInfo = new List<string>();

                    var replyParts = result.Split(new[] {' ', '_'}, StringSplitOptions.RemoveEmptyEntries);

                    for (var i = 0; i < replyParts.Length; i++)
                    {
                        var subPart = replyParts[i].Substring(1);
                        switch (replyParts[i][0])
                        {
                            case 'C':
                                parseResult.ExtraInfo = string.Join(" ", replyParts.SubArray(i, replyParts.Length - i));
                                i = replyParts.Length;
                                break;

                            case 'M':
                                foreach (MsregDeviceType item in Enum.GetValues(typeof (MsregDeviceType)))
                                {
                                    if (subPart.StartsWith(item.ToString()))
                                        parseResult.Type = item;
                                }
                                break;

                            case 'r':
                                parseResult.HardwareRevision = subPart;
                                break;

                            case 'v':
                                parseResult.FirmwareVersion = subPart;
                                break;

                            default:
                                if (replyParts[i][0] >= '0' && replyParts[i][0] <= '9')
                                {
                                    parseResult.FirmwareBuildTime =
                                        DateTime.Parse(replyParts[i] + " " + replyParts[i + 1], ParseCultureInfo);
                                    i++;
                                }
                                break;
                        }
                    }

                    AppLog.Write(Strings.Default.DeviceInfoReceived + parseResult, LogEntryType.Info,
                        LogEntrySource.MsregDevice, TargetPort);
                    return parseResult;
                }
                catch (Exception e)
                {
                    AppLog.Write(Strings.Default.DeviceInfoParseFailed + e.Message, LogEntryType.Error,
                        LogEntrySource.MsregDevice, TargetPort);
                }

                return null;
            }
        }

        public void SendCommand(MsregInputCommand inputCommand)
        {
            lock (_commandSendLock)
            {
                TryToSyncSignal();
                SendCommandFast(inputCommand);
            }
        }

        public void SendCommandAsync(MsregInputCommand inputCommand)
        {
            Task.Factory.StartNew(() => { SendCommand(inputCommand); });
        }

        /*void TryUploadSetting(TargetMeasurementType type, MsregEditSettingChoices setting, double value)
        {
            lock (commandSendLock)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (UploadSetting(type, setting, value))
                        return;
                }
            }
        }*/

        public bool UploadSetting(TargetMeasurementType type, MsregEditSettingChoices setting, double value)
        {
            lock (_commandSendLock)
            {
                if (!TryToSyncSignal())
                    return false;

                var outStr = string.Format(ParseCultureInfo, "e{0}{1}{2:000.0}f", (char) type, (char) setting, value);
                _target.Write(outStr);

                AppLog.Write("Wysłana zmiana ustawienia: " + outStr, LogEntryType.Debug, LogEntrySource.MsregDevice,
                    TargetPort);

                for (var i = 0; i < 20; i++)
                {
                    string result;
                    if (_ackReplies.TryRemove(MsregOutputCommand.EditSettingReply, out result))
                    {
                        if (result.StartsWith("OK"))
                            return true;

                        AppLog.Write("Błąd podczas zmiany ustawienia: " + result, LogEntryType.Error,
                            LogEntrySource.MsregDevice, TargetPort);

                        return false;
                    }
                    Thread.Sleep(10);
                }
                return false;
            }
        }

        public void UploadSettingsAsync(TargetMeasurementType type, RegulationSettings settings)
        {
            Task.Factory.StartNew(() =>
            {
                UploadSetting(type, MsregEditSettingChoices.Nastawa, settings.TargetValue);
                UploadSetting(type, MsregEditSettingChoices.Histereza, settings.Histeresis);
                UploadSetting(type, MsregEditSettingChoices.OdstepPrzelaczen, settings.WaitTime);
                UploadSetting(type, MsregEditSettingChoices.PoziomAlarmowy, settings.AlarmOffset);
                UploadSetting(type, MsregEditSettingChoices.NormallyClosed, settings.NormallyClosed ? 0.1 : 0);

                SendCommandFast(type == TargetMeasurementType.Temperature
                    ? MsregInputCommand.TemperatureSettings
                    : MsregInputCommand.HumiditySettings);
            });
        }

        private static RegulationSettings ParseRegulationSettings(string reply)
        {
            var newSettings = new RegulationSettings();

            var replyParts = reply.Split(new[] {' ', '_'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in replyParts)
            {
                var cutPart = part.Substring(1);

                switch ((MsregEditSettingChoices) part[0])
                {
                    case MsregEditSettingChoices.Nastawa:
                        newSettings.TargetValue = double.Parse(cutPart, ParseCultureInfo);
                        break;

                    case MsregEditSettingChoices.Histereza:
                        newSettings.Histeresis = double.Parse(cutPart, ParseCultureInfo);
                        break;

                    case MsregEditSettingChoices.NormallyClosed:
                        newSettings.NormallyClosed = double.Parse(cutPart, ParseCultureInfo) != 0;
                        break;

                    case MsregEditSettingChoices.OdstepPrzelaczen:
                        newSettings.WaitTime = double.Parse(cutPart, ParseCultureInfo);
                        break;

                    case MsregEditSettingChoices.PoziomAlarmowy:
                        newSettings.AlarmOffset = double.Parse(cutPart, ParseCultureInfo);
                        break;
                }
            }
            return newSettings;
        }

        private void Disconnect(string reason)
        {
            lock (_commandSendLock)
            {
                if (_target != null)
                {
                    AppLog.Write(Strings.Default.DisconnectHeader + reason, LogEntryType.Info,
                        LogEntrySource.MsregDevice, TargetPort);

                    //target.Close();
                    _target.Dispose();
                    _target = null;

                    //GC.Collect();

                    OnDisconnected(reason);
                }

                if (_lineReceiver != null)
                {
                    _lineReceiver.Abort();
                    _lineReceiver = null;
                }

                ConnectedDeviceInfo = null;

                _ackReplies.Clear();
            }

            if (AutoReconnect)
                Connect();
        }

        private void OnConnected()
        {
            Connected?.Invoke(this);
        }

        private void OnDisconnected(string reason)
        {
            Disconnected?.Invoke(this, reason);
        }

        private void OnInputParseFailed(string arg)
        {
            InputParseFailed?.Invoke(this, arg);
        }

        private void OnMeasurementReceived(string reply)
        {
            var replyParts = reply.Split(new[] {' ', '_'}, StringSplitOptions.RemoveEmptyEntries);
            double t = -1000, h = -1000;
            RegulationResult tr = (RegulationResult) 1000, hr = (RegulationResult) 1000;
            foreach (var part in replyParts)
            {
                switch (part[0])
                {
                    case 'T':
                        t = double.Parse(part.Substring(1), ParseCultureInfo);
                        break;
                    case 'H':
                        h = double.Parse(part.Substring(1), ParseCultureInfo);
                        break;
                    case 'G':
                        tr = (RegulationResult) int.Parse(part.Substring(1), ParseCultureInfo);
                        break;
                    case 'N':
                        hr = (RegulationResult) int.Parse(part.Substring(1), ParseCultureInfo);
                        break;
                }
            }

            var newMeasurement = new Msr33Measurement(t, h, tr, hr);
            if (newMeasurement.IsValid)
                ReceivedMeasurement?.Invoke(this, newMeasurement);
        }

        private void OnReceivedAckMessage(MsregOutputCommand ackType, string fullAckData)
        {
            _ackReplies.GetOrAdd(ackType, fullAckData);
        }

        private void OnReceivedAlarmValue(MsregAlarm args)
        {
            ReceivedAlarmValue?.Invoke(this, args);
        }

        private void OnReceivedHumiditySettings(string reply)
        {
            if (ReceivedHumiditySettings != null)
            {
                var newSettings = ParseRegulationSettings(reply);

                if (newSettings.IsComplete)
                    ReceivedHumiditySettings(this, newSettings);
            }
        }

        private void OnReceivedTemperatureSettings(string reply)
        {
            if (ReceivedTemperatureSettings != null)
            {
                var newSettings = ParseRegulationSettings(reply);

                if (newSettings.IsComplete)
                    ReceivedTemperatureSettings(this, newSettings);
            }
        }

        private void ReceiveLine_T()
        {
            Task badCharReceivedSolver = null;

            while (true)
            {
                while (_target == null || !_target.IsOpen)
                    Thread.Sleep(20);

                Thread.Sleep(4);

                try
                {
                    var rawInput = _target.ReadLine().Trim();

                    if (!string.IsNullOrEmpty(rawInput))
                    {
                        AppLog.Write(Strings.Default.DataReceived + rawInput, LogEntryType.Debug,
                            LogEntrySource.MsregDevice, TargetPort);

                        //MeasureEventArgs newMeasurement = new MeasureEventArgs();
                        //RegulationSettings newSettingResult = new RegulationSettings();
                        //bool isTempSettingResult = true;

                        //for (int i = 0; i < inputs.Length; i++)
                        //{
                        //string input = inputs[i];
                        //if (input.Length == 0)
                        //    continue;

                        var cutInput = rawInput.Substring(1);
                        var commandType = (MsregOutputCommand) rawInput[0];
                        switch (commandType)
                        {
                            case MsregOutputCommand.Measurement:
                                OnMeasurementReceived(rawInput);
                                break;

                            case MsregOutputCommand.CurrentAlarm: // Current alarm (0 if none)
                                OnReceivedAlarmValue((MsregAlarm) int.Parse(cutInput, ParseCultureInfo));
                                break;

                            case MsregOutputCommand.VersionInfo:
                            case MsregOutputCommand.EditSettingReply:
                            case MsregOutputCommand.SaveSettingsReply:
                            case MsregOutputCommand.RestoreDefaultSettingsReply:
                            case MsregOutputCommand.ReadSettingsReply:
                            case MsregOutputCommand.BaudRateCorrectReply:
                                OnReceivedAckMessage(commandType, cutInput);
                                break;

                            case MsregOutputCommand.TemperatureSettingsReply:
                                OnReceivedTemperatureSettings(cutInput);
                                break;
                            case MsregOutputCommand.HumiditySettingsReply:
                                OnReceivedHumiditySettings(cutInput);
                                break;

                            default:
                                // Received unknown command, could be because of bad sync
                                if (badCharReceivedSolver == null || badCharReceivedSolver.IsCompleted)
                                    badCharReceivedSolver = Task.Factory.StartNew(TryToSyncSignal);

                                //throw new NotImplementedException("Unknown command: " + input);
                                // Skip processing rest of the data, since it is bogus
                                //target.DiscardInBuffer();

                                break;
                        }
                    }
                }
                    //catch (IOException) { }

                catch (Exception e)
                {
                    if (e is ArgumentNullException || e is FormatException || e is OverflowException)
                    {
                        AppLog.Write(Strings.Default.DataReceiveFailed + e.Message, LogEntryType.Error,
                            LogEntrySource.MsregDevice, TargetPort);
                        OnInputParseFailed(e.Message);
                    }
                    else if (e is TimeoutException) //|| e is InvalidOperationException)
                    {
                        //AppLog.Write("Blad odczytu danych: " + e.Message, LogEntryType.Error, LogEntrySource.MsregDevice, TargetPort);
                        Disconnect(Strings.Default.DisconnectStoppedResponding);
                    }
                    else if (e is IOException)
                    {
                        // Thread ended on us
                        return;
                    }
                    else throw;
                }
            }
        }

        private void SendCommandFast(MsregInputCommand inputCommand)
        {
            _target?.Write(new[] {(byte) inputCommand}, 0, 1);
        }

        private bool SendSyncSignal()
        {
            SendCommandFast(MsregInputCommand.BaudRateAutocorrect);
            for (var i = 0; i < 4; i++)
            {
                string result;
                if (_ackReplies.TryRemove(MsregOutputCommand.BaudRateCorrectReply, out result))
                    return result.StartsWith("OK");

                Thread.Sleep(40);
            }
            return false;
        }

        private void SetupComPort()
        {
            _target?.Dispose();

            _target = new SafeSerialPort
            {
                BaudRate = 4800,
                Handshake = Handshake.None,
                NewLine = "\r\n",
                Parity = Parity.None,
                ReadTimeout = 10000,
                StopBits = StopBits.One
            };

            _lineReceiver?.Abort();

            _lineReceiver = new Thread(ReceiveLine_T)
            {
                IsBackground = true,
                Name = ToString() + "_LineReceiver"
            };
            _lineReceiver.Start();
        }

        private bool TryToSyncSignal()
        {
            lock (_commandSendLock)
            {
                for (var i = 0; i < 10; i++)
                {
                    if (SendSyncSignal())
                        return true;
                    Thread.Sleep(10);
                }
                AppLog.Write(Strings.Default.TryToSyncSignalFailed, LogEntryType.Warning, LogEntrySource.MsregDevice,
                    TargetPort);
                return false;
            }
        }

        /*
        void UploadSettingSendCommand(MsregEditSettingChoices setting, Int16 value)
        {
            byte hiByte = (byte)(value >> 8);
            byte loByte = (byte)(value);
            byte xor = (byte)(hiByte ^ loByte);

            if (target != null)
                target.Write(new byte[] { (byte)setting, hiByte, loByte, xor }, 0, 4);
        }*/

        #endregion Methods
    }
}