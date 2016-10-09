using System;
using System.IO.Ports;
using System.Timers;
using System.Windows.Forms;
using Klocman.IO;
using Klocman.Subsystems;
using MSREG.Viewer.CustomControls;
using MSREG.Viewer.SpecialClasses;
using Timer = System.Timers.Timer;

namespace MSREG.Viewer.Windows.MdiChildWindows
{
    public sealed partial class TerminalWindow : MdiChildForm
    {
        private readonly Timer _readTimer;
        private SafeSerialPort _target;

        public TerminalWindow(string portName)
        {
            InitializeComponent();

            _readTimer = new Timer();
            _readTimer.Interval = 200;
            _readTimer.SynchronizingObject = this;
            _readTimer.AutoReset = true;
            _readTimer.Elapsed += readTimer_Elapsed;
            _readTimer.Start();

            SetupComPort();

            AppLog.Write(Strings.Default.ConnectingToPort + portName, LogEntryType.Info, LogEntrySource.TerminalWindow);

            try
            {
                _target.PortName = portName;
                _target.Open();
            }
            catch (Exception e)
            {
                AppLog.Write(Strings.Default.DisconnectFailedToOpen + e.Message, LogEntryType.Error,
                    LogEntrySource.TerminalWindow, portName);
                Close();
            }

            Text = "Terminal: " + portName;
        }

        private void readTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                outputTextBox.AppendText(_target.ReadExisting());
            }
            catch (Exception)
            {
                Close();
            }
        }

        private void SetupComPort()
        {
            if (_target != null)
                _target.Dispose();

            _target = new SafeSerialPort();
            _target.BaudRate = 4800;
            _target.Handshake = Handshake.None;
            _target.NewLine = "\r\n";
            _target.Parity = Parity.None;
            _target.ReadTimeout = 10000; // 3 second regulator timeout
            _target.StopBits = StopBits.One;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(inputTextBox.Text))
            {
                try
                {
                    _target.Write(inputTextBox.Text);
                }
                catch (Exception)
                {
                    Close();
                }
            }
        }

        private void TerminalWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppLog.Write(Strings.Default.DisconnectingPort,
                LogEntryType.Info, LogEntrySource.TerminalWindow,
                _target != null ? _target.PortName : string.Empty);

            _readTimer.Dispose();
            _target.Dispose();
        }
    }
}