using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Klocman.Extensions;
using MSREG.Viewer.CustomControls;
using MSREG.Viewer.SpecialClasses;
using MSREG.Viewer.Windows.MdiChildWindows.MSR33;
using Timer = System.Timers.Timer;

namespace MSREG.Viewer.Windows.MdiChildWindows
{
    public partial class ConnectWindow : MdiChildForm
    {
        #region Constructors

        public ConnectWindow()
        {
            InitializeComponent();

            _listRefreshTimer.SynchronizingObject = this;
            _listRefreshTimer.Elapsed += listRefreshTimer_Elapsed;
            _listRefreshTimer.AutoReset = true;
            _listRefreshTimer.Start();
        }

        #endregion Constructors

        private void ConnectWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _listRefreshTimer.Stop();
        }

        #region Fields

        private readonly MsregDevice _connectionTester = new MsregDevice();
        private readonly Dictionary<MsregDeviceInfo, string> _detectedDevices = new Dictionary<MsregDeviceInfo, string>();
        private readonly Timer _listRefreshTimer = new Timer(1000);
        //Thread listRefreshWorker;
        private string[] _previousPorts = {};

        #endregion Fields

        #region Methods

        private void ConnectWindow_Shown(object sender, EventArgs e)
        {
            RefreshDeviceList(sender, e);
        }

        private void devicebuttonOk_Click(object sender, EventArgs e)
        {
            if (devicelistBox.SelectedItem is MsregDeviceInfo)
            {
                var device = devicelistBox.SelectedItem as MsregDeviceInfo;
                var port = _detectedDevices[device];
                OpenDevice(port);
            }
        }

        private void listRefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_previousPorts.Length != SerialPort.GetPortNames().Length && !_isRefreshing)
            {
                RefreshDeviceList(sender, e);
            }
        }

        private void OpenDevice(string port)
        {
            _connectionTester.Disconnect();

            var newWindw = new Msr33Window();
            newWindw.SetupAndShowMdiChildForm(newWindw.Text, MdiParent, MasterTabControl);

            Task.Factory.StartNew(() =>
            {
                if (newWindw.Connect(port))
                {
                    Invoke(new Action(() => { Close(); }));
                }
                else
                {
                    newWindw.Invoke(new Action(() =>
                    {
                        newWindw.Dispose();
                        //MessageBox.Show("Polaczenie z urzadzeniem sie nie powiodlo, sprawdz przewody i sproboj ponownie",
                        //    "Blad polaczenia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }));
                }
            });
        }

        private void OpenPort(object sender, EventArgs e)
        {
            if (portlistBox.SelectedItem is string)
            {
                var term = new TerminalWindow(portlistBox.SelectedItem as string);
                if (!term.IsDisposed)
                {
                    term.SetupAndShowMdiChildForm(term.Text, MdiParent, MasterTabControl);
                }
                Close();
            }
        }

        private bool _isRefreshing;
        private readonly object _refreshLock = new object();

        private void RefreshDeviceList(object sender, EventArgs e)
        {
            _connectionTester.Disconnect();

            RefreshPortList(sender, e);

            lock (_refreshLock)
            {
                if (_isRefreshing)
                    return;

                _detectedDevices.Clear();
                devicelistBox.Items.Clear();
                devicelistBox.Items.Add(Strings.Default.Searching);
                devicebuttonOk.Enabled = false;
                devicebuttonRef.Enabled = false;

                Task.Factory.StartNew(RefreshDeviceList_T);
            }
        }

        private void RefreshDeviceList_T()
        {
            try
            {
                lock (_refreshLock)
                {
                    if (_isRefreshing)
                        return;
                    _isRefreshing = true;
                }

                _connectionTester.Disconnect();
                //Thread.Sleep(50);

                foreach (var portName in SerialPort.GetPortNames())
                {
                    _connectionTester.Connect(portName);
                    if (_connectionTester.IsConnected)
                    {
                        _detectedDevices.Add(_connectionTester.ConnectedDeviceInfo, portName);
                    }
                }
                _connectionTester.Disconnect();

                devicelistBox.SafeInvoke(() =>
                {
                    devicelistBox.Items.Clear();
                    if (_detectedDevices.Count == 0)
                    {
                        devicelistBox.Items.Add(Strings.Default.SearchingFoundNoDevices);
                        devicebuttonOk.Enabled = false;
                    }
                    else
                    {
                        devicelistBox.Items.AddRange(_detectedDevices.Keys.ToArray());
                        devicebuttonOk.Enabled = true;
                    }
                    devicebuttonRef.Enabled = true;
                });
                _isRefreshing = false;
            }
            catch (IOException)
            {
                // Thread ended
                _isRefreshing = false;
            }
        }

        private void RefreshPortList(object sender, EventArgs e)
        {
            lock (_refreshLock)
            {
                portlistBox.Items.Clear();
                var portNames = SerialPort.GetPortNames();
                if (portNames.Length == 0)
                {
                    portlistBox.Items.Add(Strings.Default.SearchingFoundNoPorts);
                    portbuttonOk.Enabled = false;
                }
                else
                {
                    portlistBox.Items.AddRange(portNames);
                    portbuttonOk.Enabled = true;
                }
                _previousPorts = portNames;
            }
        }

        #endregion Methods
    }
}