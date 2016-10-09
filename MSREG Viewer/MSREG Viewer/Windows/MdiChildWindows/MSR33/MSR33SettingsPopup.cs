using System;
using Klocman.Extensions;
using MSREG.Viewer.CustomControls;
using MSREG.Viewer.SpecialClasses;

namespace MSREG.Viewer.Windows.MdiChildWindows.MSR33
{
    public sealed partial class Msr33SettingsPopup : MdiChildForm
    {
        private readonly MsregDevice _targetDevice;

        public Msr33SettingsPopup(MsregDevice target)
        {
            if (!target.IsConnected)
            {
                Close();
                return;
            }

            InitializeComponent();

            _targetDevice = target;

            _targetDevice.Disconnected += (x, y) => this.SafeInvoke(Close);

            regulatorSettingPanelTemperature.Enabled = false;
            regulatorSettingPanelHumidity.Enabled = false;
            _targetDevice.ReceivedTemperatureSettings += (x, y) => regulatorSettingPanelTemperature.UpdateValueBoxes(y);
            _targetDevice.ReceivedHumiditySettings += (x, y) => regulatorSettingPanelHumidity.UpdateValueBoxes(y);

            regulatorSettingPanelTemperature.RefreshButtonClicked +=
                (x, y) => _targetDevice.SendCommandAsync(MsregInputCommand.TemperatureSettings);
            regulatorSettingPanelHumidity.RefreshButtonClicked +=
                (x, y) => _targetDevice.SendCommandAsync(MsregInputCommand.HumiditySettings);

            regulatorSettingPanelTemperature.SendButtonClicked += (x, y) =>
                _targetDevice.UploadSettingsAsync(TargetMeasurementType.Temperature,
                    regulatorSettingPanelTemperature.GetNewSettings());
            regulatorSettingPanelHumidity.SendButtonClicked += (x, y) =>
                _targetDevice.UploadSettingsAsync(TargetMeasurementType.Humidity,
                    regulatorSettingPanelHumidity.GetNewSettings());

            _targetDevice.SendCommandAsync(MsregInputCommand.TemperatureSettings);
            _targetDevice.SendCommandAsync(MsregInputCommand.HumiditySettings);

            Text = "Ustawienia regulatora MSR33 : " + target.TargetPort;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _targetDevice.SendCommandAsync(MsregInputCommand.RestoreDefaultSettings);
            _targetDevice.SendCommandAsync(MsregInputCommand.TemperatureSettings);
            _targetDevice.SendCommandAsync(MsregInputCommand.HumiditySettings);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _targetDevice.SendCommandAsync(MsregInputCommand.SaveSettings);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}