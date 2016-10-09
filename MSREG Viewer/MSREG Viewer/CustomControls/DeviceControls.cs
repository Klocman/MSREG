using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSREG.Viewer.SpecialClasses;
using MSREG.Viewer.Windows.MdiChildWindows.MSR33;

namespace MSREG.Viewer.CustomControls
{
    public partial class DeviceControls : UserControl
    {
        private MsregDevice _targetDevice;

        private Msr33SettingsPopup _settingsPopup;


        private Msr33ToolsPopup _toolsPopup;

        public DeviceControls()
        {
            InitializeComponent();
            EnableFunctionButtons(false);
        }

        public MsregDevice TargetDevice
        {
            get { return _targetDevice; }
            set
            {
                if (value != null)
                {
                    value.Connected += value_Connected;
                    value.Disconnected += value_Disconnected;
                    if (value.IsConnected)
                        value_Connected(value);
                }
                _targetDevice = value;
            }
        }

        private void EnableFunctionButtons(bool e)
        {
            buttonSettings.Enabled = e;
            buttonTools.Enabled = e;
        }

        private void value_Disconnected(MsregDevice arg1, string arg2)
        {
            Invoke(new Action(() =>
            {
                infoLabelStatus.Text = "Odłączony";

                buttonConnection.Text = "Podłącz";

                EnableFunctionButtons(false);
            }));
        }

        private void value_Connected(MsregDevice obj)
        {
            Invoke(new Action(() =>
            {
                infoLabelStatus.Text = "Połączony";
                infoLabelPort.Text = obj.TargetPort;
                buttonConnection.Text = "Odłącz";

                EnableFunctionButtons(true);
            }));
        }

        /// <summary>
        ///     Gets time in seconds between graph points
        /// </summary>
        public decimal GetGraphPlottingSpeed()
        {
            if (radioXAuto.Checked)
                return 0;
            return xLastMinuteCount.Value;
        }

        public event Action<decimal> GraphPlottingSpeedChanged;

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            if (_targetDevice != null)
            {
                if (_targetDevice.IsConnected)
                {
                    var prevSet = _targetDevice.AutoReconnect;
                    _targetDevice.AutoReconnect = false;

                    TargetDevice.Disconnect();

                    _targetDevice.AutoReconnect = prevSet;
                }
                else
                {
                    Task.Factory.StartNew(() => TargetDevice.Connect());
                }
            }
        }

        private void OnGraphPlottingSpeedChanged(object sender, EventArgs e)
        {
            if (GraphPlottingSpeedChanged != null)
                GraphPlottingSpeedChanged(GetGraphPlottingSpeed());
        }

        private void OpenSettingsPopup(object sender, EventArgs e)
        {
            if (_settingsPopup != null && !_settingsPopup.IsDisposed)
                _settingsPopup.Focus();
            else
            {
                _settingsPopup = new Msr33SettingsPopup(TargetDevice);
                var parent = ParentForm as MdiChildForm;
                _settingsPopup.SetupAndShowMdiChildForm(parent.MdiParent, parent.MasterTabControl);
                //settingsPopup.WindowState = FormWindowState.Normal;
                //settingsPopup.ShowDialog();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (_targetDevice != null)
                _targetDevice.AutoReconnect = checkBox2.Checked;
        }

        private void OpenToolsPopup(object sender, EventArgs e)
        {
            if (_toolsPopup != null && !_toolsPopup.IsDisposed)
                _toolsPopup.Focus();
            else
            {
                _toolsPopup = new Msr33ToolsPopup(TargetDevice);
                var parent = ParentForm as MdiChildForm;
                _toolsPopup.SetupAndShowMdiChildForm(parent.MdiParent, parent.MasterTabControl);
                //settingsPopup.WindowState = FormWindowState.Normal;
                //settingsPopup.ShowDialog();
            }
        }
    }
}