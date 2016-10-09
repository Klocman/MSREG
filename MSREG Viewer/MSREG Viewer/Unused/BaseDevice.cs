using Klocman;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSREG_Viewer
{
    class BaseDevice
    {
        public bool IsConnected { get; private set; }

        int timeoutCount = 0;
        List<DeviceCommand> commandQueue = new List<DeviceCommand>();
        SafeSerialPort target;
        Thread lineReceiver;
        object commandSendLock = new object();

        public BaseDevice()
        {
        }

        public void Connect(string portName)
        {
            SetupComPort();

            target.PortName = portName;
            target.Open();

            TryToSyncSignal();

            var deviceVersion = new DeviceVersionCommand();
            AddCommand(deviceVersion);

            for (int i = 0; i < 20; i++)
            {
                if (deviceVersion.DataReceived)
                    break;

                Thread.Sleep(10);
            }

            if (!deviceVersion.DataReceived)
            {
                Disconnect("sdsd");
                return;
            }


        }

        bool TryToSyncSignal()
        {
            lock (commandSendLock)
            {
                for (int i = 0; i < 10; i++)
                {
                    //if (SendSyncSignal())
                    //    return true;
                    Thread.Sleep(10);
                }
                //AppLog.Write(Strings.Default.TryToSyncSignalFailed, AppLog.LogEntryType.Debug, AppLog.LogEntrySource.MsregDevice, TargetPort);
                return false;
            }
        }

        void Disconnect(string reason)
        {
            lock (commandSendLock)
            {
                if (target != null)
                {
                    //AppLog.Write(Strings.Default.DisconnectHeader + reason, AppLog.LogEntryType.Warning, AppLog.LogEntrySource.MsregDevice, TargetPort);

                    target.Dispose();
                    target = null;

                    GC.Collect();

                    //OnDisconnected(reason);
                }

                if (lineReceiver != null)
                {
                    lineReceiver.Abort();
                    lineReceiver = null;
                }

                //ConnectedDeviceInfo = null;

                //ackReplies.Clear();
            }
        }

        void SetupComPort()
        {
            if (target != null)
                target.Dispose();

            target = new SafeSerialPort();
            target.BaudRate = 4800;
            target.Handshake = Handshake.None;
            target.NewLine = "\r\n";
            target.Parity = Parity.None;
            target.ReadTimeout = 10000;
            target.StopBits = StopBits.One;

            if (lineReceiver != null)
                lineReceiver.Abort();

            lineReceiver = new Thread(ReceiveLine_T);
            lineReceiver.IsBackground = true;
            lineReceiver.Name = this.ToString() + "_LineReceiver";
            lineReceiver.Start();

            commandQueue.Clear();
        }

        public bool AddCommand(DeviceCommand command)
        {
            if (commandQueue.Contains(command) || !IsConnected)
                return false;

            commandQueue.Add(command);
            return true;
        }

        void ReceiveLine_T()
        {
            while (true)
            {
                try
                {
                    string inputLine = string.Empty;

                    try { inputLine = target.ReadLine(); }
                    catch (TimeoutException) { if (++timeoutCount > 2) throw; else AddCommand(DeviceCommand.Heartbeat); }

                    foreach (var command in commandQueue)
                    {
                        if (command.WasSent)
                        {
                            command.TryReceive(inputLine);
                            if (command.DataReceived == true)
                                timeoutCount = 0;
                        }
                        else
                        {
                            target.Write(command.GetCommandToSend());
                            command.WasSent = true;
                        }
                    }

                    commandQueue.RemoveAll((x) => x.DataReceived);
                }
                catch (Exception e)
                {/*
                    if (e is ArgumentNullException || e is FormatException || e is OverflowException)
                    {
                        AppLog.Write(Strings.Default.DataReceiveFailed + e.Message, AppLog.LogEntryType.Debug, AppLog.LogEntrySource.MsregDevice, TargetPort);
                        OnInputParseFailed(e.Message);
                    }
                    else*/ if (e is TimeoutException || e is InvalidOperationException)
                    {
                        //AppLog.Write("Blad odczytu danych: " + e.Message, AppLog.LogEntryType.Error, AppLog.LogEntrySource.MsregDevice, TargetPort);
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
    }
}
