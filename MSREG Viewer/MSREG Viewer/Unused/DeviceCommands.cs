namespace MSREG_Viewer
{
    using System;
    using System.Collections.Generic;
    using Klocman;

    public class CommandResult
    {
        #region Constructors

        public CommandResult(bool wasOK, string fullMessage)
        {
            FullMessage = fullMessage;
            WasOk = wasOK;
        }

        #endregion Constructors

        #region Properties

        public string FullMessage
        {
            get; private set;
        }

        public bool WasOk
        {
            get; private set;
        }

        #endregion Properties
    }

    public class DeviceCommand
    {
        #region Fields

        public static readonly DeviceCommand Heartbeat = new DeviceCommand(" ", "b");

        string commandToSend;
        string replyToGet;
        CommandResult Result;

        #endregion Fields

        #region Constructors

        public DeviceCommand(string command)
            : this(command, command)
        {
        }

        public DeviceCommand(string command, string reply)
        {
            if (string.IsNullOrEmpty(command))
                throw new ArgumentNullException("command can't be null or empty");
            if (string.IsNullOrEmpty(reply))
                throw new ArgumentNullException("reply can't be null or empty");

            commandToSend = command;
            replyToGet = reply;
            DataReceived = false;
            WasSent = false;
        }

        #endregion Constructors

        #region Properties

        public bool DataReceived
        {
            get; protected set;
        }

        public bool WasSent
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public virtual string GetCommandToSend()
        {
            return commandToSend;
        }

        public virtual void Reset()
        {
            DataReceived = false;
            WasSent = false;
        }

        public virtual void TryReceive(string reply)
        {
            if (reply.StartsWith(replyToGet))
            {
                Result = new CommandResult(reply.Contains("OK"), reply.Substring(replyToGet.Length));
                DataReceived = true;
            }
        }

        #endregion Methods
    }

    public class DeviceVersionCommand : DeviceCommand
    {
        #region Constructors

        public DeviceVersionCommand()
            : base("v")
        {
        }

        #endregion Constructors

        #region Properties

        public MsregDeviceInfo ParseResult
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public override void TryReceive(string reply)
        {
            if (!reply.StartsWith("v"))
                return;

            MsregDeviceInfo parseResult = new MsregDeviceInfo();
            List<string> extraInfo = new List<string>();

            var replyParts = reply.Split(new char[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < replyParts.Length; i++)
            {
                var subPart = replyParts[i].Substring(1);
                switch (replyParts[i][0])
                {
                    case 'C':
                        parseResult.CopyrightInformation = string.Join(" ", replyParts.SubArray(i, replyParts.Length - i));
                        i = replyParts.Length;
                        break;

                    case 'M':
                        foreach (MsregDeviceType item in Enum.GetValues(typeof(MsregDeviceType)))
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
                            parseResult.FirmwareBuildTime = DateTime.Parse(replyParts[i] + " " + replyParts[i + 1]);
                            i++;
                        }
                        else
                        {
                            //AppLog.Write(Strings.Default.DeviceInfoUnknownTag + replyParts[i], AppLog.LogEntryType.Warning, AppLog.LogEntrySource.MsregDevice, TargetPort);
                        }
                        break;
                }
            }

            ParseResult = parseResult;
            DataReceived = true;
            //AppLog.Write(Strings.Default.DeviceInfoReceived + parseResult.ToString(), AppLog.LogEntryType.Info, AppLog.LogEntrySource.MsregDevice, TargetPort);

            return;
        }

        #endregion Methods
    }

    

    public class MSR33MeasurementCommand : DeviceCommand
    {
        #region Constructors

        /*
         * Temperature_measurement = 'T',
         * Humidity_measurement = 'H',
         * Temperature_regulation_result = 'G',
         * Humidity_regulation_result = 'N',
         */
        public MSR33MeasurementCommand()
            : base(" ")
        {
            //WasSent = true;
        }

        #endregion Constructors

        #region Properties

        public MSR33Measurement LastMeasurement
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public override void TryReceive(string reply)
        {
            if (!reply.StartsWith("T"))
                return;

            var replyParts = reply.Split(new char[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries);
            double t = -1000, h = -1000;
            RegulationResult tr = (RegulationResult)1000, hr = (RegulationResult)1000;
            foreach (var part in replyParts)
            {
                switch (part[0])
                {
                    case 'T':
                        t = double.Parse(part.Substring(1));
                        break;
                    case 'H':
                        h = double.Parse(part.Substring(1));
                        break;
                    case 'G':
                        tr = (RegulationResult)int.Parse(part.Substring(1));
                        break;
                    case 'N':
                        hr = (RegulationResult)int.Parse(part.Substring(1));
                        break;
                }
            }

            var newMeasurement = new MSR33Measurement(t, h, tr, hr);
            if (newMeasurement.IsValid)
                LastMeasurement = newMeasurement;
        }

        #endregion Methods
    }
}