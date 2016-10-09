using System;
using System.Diagnostics;
using System.Windows.Forms;
using Klocman.Extensions;
using MSREG.Viewer.CustomControls;
using MSREG.Viewer.SpecialClasses;

namespace MSREG.Viewer.Windows.MdiChildWindows.MSR33
{
    public sealed partial class Msr33ToolsPopup : MdiChildForm
    {
        private readonly MsregDevice _targetDevice;

        public Msr33ToolsPopup(MsregDevice target)
        {
            if (!target.IsConnected)
            {
                Close();
                return;
            }

            InitializeComponent();

            _targetDevice = target;

            _targetDevice.Disconnected += (x, y) => this.SafeInvoke(Close);

            textBox1.Text = target.ConnectedDeviceInfo.ToFullString();
            Text = "Nażędzia MSR33 : " + target.TargetPort;
        }

        private void buttonRes_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Czy na pewno zrestartować regulator? Zostanie on odłączony od programu.",
                    "Restart regulatora", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                _targetDevice.SendCommand(MsregInputCommand.RestartDevice);
                _targetDevice.Disconnect();
            }
        }

        private void buttonUpdt_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(
                    "Czy na pewno otworzyć aktualizator firmware? Regulator zostanie odłączony od programu.",
                    "Aktualizacja firmware", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                _targetDevice.Disconnect();
                FirmwareUpdater.OpenUpdater(_targetDevice.TargetPort);
            }
        }

        private void buttonDatasheet_Click(object sender, EventArgs e)
        {
            var tempPath = UpdateChecker.Msr33R7DatasheetUrl.ToString();
            if (string.IsNullOrWhiteSpace(tempPath))
            {
                MessageBox.Show("Instrukcja nie jest dostępna lub wystąpił błąd połączenia z serwerem aktualizacyjnym.");
            }
            else
            {
                Process.Start(tempPath);
            }
        }
    }
}