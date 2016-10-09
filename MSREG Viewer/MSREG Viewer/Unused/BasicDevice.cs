using Klocman;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MSREG_Viewer
{
    /*
    
    /// <summary>
    /// Data automatically sent from the device
    /// </summary>
    public class DeviceOutputData
    {
        public static readonly DeviceOutputData Measurement;
        public static readonly DeviceOutputData AlarmLevel;
        public static readonly DeviceOutputData SensorError;
    }


    /// <summary>
    /// Commands coming out of the device to us.
    /// </summary>
    public class DeviceOutputCommand
    {
        public static readonly DeviceOutputCommand AckSignalReply                = new DeviceOutputCommand ("AckSignal",                    'b', "bOK");
        public static readonly DeviceOutputCommand Measurement                   = new DeviceOutputCommand("Measurement", 'T', string.Empty);
        public static readonly DeviceOutputCommand CurrentAlarm                  = new DeviceOutputCommand ("CurrentAlarm",                 'A', string.Empty);
        public static readonly DeviceOutputCommand VersionInfoReply              = new DeviceOutputCommand ("VersionInfoReply",             'v', string.Empty);
        public static readonly DeviceOutputCommand EditSettingReply              = new DeviceOutputCommand ("EditSettingReply",             'e', "eOK");
        public static readonly DeviceOutputCommand SaveSettingsToEepromReply     = new DeviceOutputCommand ("SaveSettingsToEepromReply",    's', "sOK");
        public static readonly DeviceOutputCommand RestoreDefaultSettingsReply   = new DeviceOutputCommand ("RestoreDefaultSettingsReply",  'r', "rOK");
        public static readonly DeviceOutputCommand ReadSettingsFromEepromReply   = new DeviceOutputCommand ("ReadSettingsFromEepromReply",  'd', "dOK");


        public DeviceOutputCommand(string name, char command, string okPayload)
        {
            Name = name;
            Command = command;
            OkPayload = okPayload;
        }
        
        public string Name;
        public char Command;
        /// <summary>
        /// Payload received when operation was completed successfully
        /// </summary>
        public string OkPayload;
    }

    /// <summary>
    /// Send one of those commands and receive an ack from the device
    /// </summary>
    public class DeviceCommand
    {
        public static readonly DeviceCommand SendAckRequest         = new DeviceInputCommand("SendAckRequest"         , ' ', 'b');
        public static readonly DeviceCommand VersionInfo            = new DeviceInputCommand("VersionInfo"            , 'v');
        public static readonly DeviceCommand EditSetting            = new DeviceInputCommand("EditSetting"            , 'e');
        public static readonly DeviceCommand SaveSettingsToEEPROM   = new DeviceInputCommand("SaveSettingsToEEPROM"   , 's');
        public static readonly DeviceCommand ReadSettingsFromEEPROM = new DeviceInputCommand("ReadSettingsFromEEPROM" , 'r');
        public static readonly DeviceCommand RestoreDefaultSettings = new DeviceInputCommand("RestoreDefaultSettings" , 'd');

        public DeviceCommand(string name, char command, char replyId)
        {
            Name = name;
            Command = command;
            ReplyId = replyId;
        }

        public DeviceCommand(string name, char command)
        {
            Name = name;
            Command = command;
            ReplyId = command;
        }

        public string Name;
        public char Command;
        public char ReplyId;
    }

    public class DeviceCommandGetSettings : DeviceCommand
    {
        public static readonly DeviceCommandGetSettings TemperatureSettings = new DeviceInputCommand("TemperatureSettings", 't', null);
        public static readonly DeviceCommandGetSettings HumiditySettings = new DeviceInputCommand("HumiditySettings", 'h', null);
        
    }

    public class SendCommandResult
    {
        public SendCommandResult (DeviceInputCommand command, string reply)
        {
            if(command == null)
                throw new ArgumentNullException("command can't be null");

            Command = command;
            ReplyPayload = reply;

            if(!ReplyReceived)
            {
                ReplyIsOK = false;
            }
            else
            {
                ReplyIsOK = reply.Equals(command.AckSignal.OkPayload);
            }
        }

        public DeviceInputCommand Command {get; private set;}
        public bool ReplyReceived {get{return !string.IsNullOrEmpty(ReplyPayload);}}
        public bool ReplyIsOK {get; private set;}
        public string ReplyPayload {get; private set;}
    }
    */
    public class BasicDevice
    {
        public BasicDevice()
        {

        }

        /*

        Thread workerThread;
        SafeSerialPort port;

        void SetupPort()
        {
            if (port != null)
                port.Dispose();

            port = new SafeSerialPort();
            port.BaudRate = 4800;
            port.Handshake = Handshake.None;
            port.NewLine = "\r\n";
            port.Parity = Parity.None;
            port.ReadTimeout = 10000;
            port.StopBits = StopBits.One;

            if (workerThread != null)
            {
                workerThread.Abort();
            }

            workerThread = new Thread(WorkerThread);
            workerThread.IsBackground = true;
            workerThread.Name = "DevicePortController";
        }

        public bool Connect(string portName)
        {
            SerialPortTester.SerialPortFixer.Execute(portName);

            AppLog.Write(Strings.Default.ConnectingToDevice + portName, AppLog.LogEntryType.Info, AppLog.LogEntrySource.MsregDevice, TargetPort);

            SetupPort();

            try
            {
                port.PortName = portName;
                port.Open();
            }
            catch (Exception e)
            {
                Disconnect(e.Message);
                return false;
            }

            if (port.IsOpen)
            {
                workerThread.Start();
                //get device info
                for (int i = 0; i < 2; i++)
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

                //OnConnected();
                return true;
            }
            else
            {
                Disconnect(Strings.Default.DisconnectFailedToOpen);
                return false;
            }
        }

        public void Disconnect(string reason)
        {
 
        }

        /// <summary>
        /// Send command to the device and wait for the reply.
        /// </summary>
        public async Task<SendCommandResult> SendCommandAsync (MsregInputCommand command)
        {
            //Monitor.
            await Task.Delay(10);

            return new SendCommandResult(null,null);
        }
        
        //public bool IsConnected { get; private set; }
        public MsregDeviceInfo DeviceInfo { get; private set; }

        public bool IsConnected
        {
            get { return ((port != null) && port.IsOpen) && DeviceInfo != null; }
        }

        public string TargetPort
        {
            get { return port == null ? string.Empty : port.PortName; }
        }
        class WorkerTask
        {
            public MsregOutputCommand Command;
            public bool AckReceived;
            //public bool WasSent;
            //public int TimeoutCounter;
        }

        ConcurrentQueue<WorkerTask> workerThreadTasks = new ConcurrentQueue<WorkerTask>();

        public void WorkerThread()
        {
            if(port == null || !port.IsOpen)
                Disconnect(Strings.Default.DisconnectDisposed);

            if(!WorkerSendSync())
            {
                // Failed to sync
            }
            var devInfo = WorkerGetDeviceInfo();
            if(devInfo == null)
            {
                // Failed to get device info
            }
            DeviceInfo = devInfo;

            // On start try to get ack from the device, then get it's device info

            // Peek and update timeout counter until you get ack or time out. Then, dequeue and set ackreceived and start next task
            //workerThreadTasks.TryPeek();
            //if (!task.WasSent){
            //    task.WasSent = true;
            //    //Send the command
            //}

            // Automatically send ack request every x seconds
        }

        private bool WorkerSendSync()
        {
            port.DiscardInBuffer();
            for (int i2 = 0; i2 < 10; i2++)
            {
                port.Write(new byte[] { (byte)DeviceInputCommand.SendAckRequest.Command }, 0, 1);

                Thread.Sleep(20);

                while (true)
                {
                    string output = port.ReadLine();
                    if (string.IsNullOrEmpty(output))
                        break;

                    if (output.Contains(DeviceInputCommand.SendAckRequest.AckSignal.OkPayload))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void WorkerSendCommand (DeviceInputCommand inputCommand)
        {
            if (inputCommand == null)
                throw new ArgumentNullException("inputCommand can't be null");

            port.Write(new byte[] { (byte)inputCommand.Command }, 0, 1);
        }

        private MsregDeviceInfo WorkerGetDeviceInfo()
        {
            WorkerSendCommand(DeviceInputCommand.VersionInfo);

            List<string> result = new List<string>();


            for (int i2 = 0; i2 < 10; i2++)
            {
                Thread.Sleep(20);

                while (true)
                {
                    string output = port.ReadLine();
                    if (string.IsNullOrEmpty(output))
                        break;

                    if (output[0].Equals(DeviceInputCommand.VersionInfo.AckSignal.Command))
                    {
                        result.Add(output);
                    }
                }
            }

            if (result.Count < 1)
                return null;

            try
            {
                MsregDeviceInfo parseResult = new MsregDeviceInfo();
                List<string> extraInfo = new List<string>();

                foreach (var resultFragment in result)
                {
                    if (resultFragment[0] == 'C')
                    {
                        parseResult.CopyrightInformation = resultFragment.Replace('_', ' ');
                    }
                    else if (resultFragment[0] == 'M')
                    {
                        var resultParts = resultFragment.Split(new string[] { " ", "_", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < resultParts.Length; i++)
                        {
                            var resultPart = resultParts[i];
                            if (resultPart[0] == 'M')
                            {
                                parseResult.Type = (MsregDeviceType)Enum.Parse(typeof(MsregDeviceType), resultPart);
                            }
                            else if (resultPart[0] == 'r')
                            {
                                parseResult.HardwareRevision = int.Parse(resultPart.Substring(1, resultPart.Length - 1));
                            }
                            else if (resultPart[0] == 'v')
                            {
                                parseResult.FirmwareVersion = new Version(resultPart.Substring(1, resultPart.Length - 1));
                            }
                            else if (resultPart[0] >= '0' && resultPart[0] <= '9')
                            {
                                i++;
                                parseResult.FirmwareBuildTime = DateTime.Parse(resultPart + " " + resultParts[i]);
                            }
                            else
                            {
                                AppLog.Write(Strings.Default.DeviceInfoUnknownTag + resultPart, AppLog.LogEntryType.Warning, AppLog.LogEntrySource.MsregDevice, TargetPort);
                            }
                        }
                    }
                    else
                    {
                        extraInfo.Add(resultFragment);
                    }
                }

                parseResult.ExtraInfo = extraInfo.ToArray();
                AppLog.Write(Strings.Default.DeviceInfoReceived + parseResult.ToString(), AppLog.LogEntryType.Info, AppLog.LogEntrySource.MsregDevice, TargetPort);
                return parseResult;
            }
            catch (Exception e)
            {
                AppLog.Write(Strings.Default.DeviceInfoParseFailed + e.Message, AppLog.LogEntryType.Error, AppLog.LogEntrySource.MsregDevice, TargetPort);
            }

            return null;
        }*/
    }
}