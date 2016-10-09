using System;
using System.ComponentModel;
using System.Windows.Forms;
using Klocman.Extensions;
using MSREG.Viewer.SpecialClasses;

namespace MSREG.Viewer.CustomControls
{
    public partial class RegulatorSettingPanel : UserControl
    {
        public RegulatorSettingPanel()
        {
            InitializeComponent();
            buttonSend.Click += buttonSend_Click;
            buttonRefresh.Click += buttonRefresh_Click;
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return groupBox3.Text; }

            set { groupBox3.Text = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Limits")]
        public decimal TargetValueMax
        {
            get { return numFieldTarget.Maximum; }

            set { numFieldTarget.Maximum = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Limits")]
        public decimal TargetValueMin
        {
            get { return numFieldTarget.Minimum; }

            set { numFieldTarget.Minimum = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Limits")]
        public decimal HisteresisValueMax
        {
            get { return numFieldHisteress.Maximum; }

            set { numFieldHisteress.Maximum = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Limits")]
        public decimal WaitValueMax
        {
            get { return numFieldTime.Maximum; }

            set { numFieldTime.Maximum = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Limits")]
        public decimal AlarmValueMax
        {
            get { return numFieldAlarm.Maximum; }

            set { numFieldAlarm.Maximum = value; }
        }

        public event Action<RegulatorSettingPanel, EventArgs> RefreshButtonClicked;
        public event Action<RegulatorSettingPanel, EventArgs> SendButtonClicked;

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (RefreshButtonClicked != null)
                RefreshButtonClicked(this, e);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (SendButtonClicked != null)
                SendButtonClicked(this, e);
        }

        /*
        void settingChangeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            settingChangeTimer.Stop();

            if (ValueChanged != null)
            {
                var newSettings = new RegulationSettings();
                newSettings.TargetValue = (double)numFieldTarget.Value;
                newSettings.Histeresis = (double)numFieldHisteress.Value;
                newSettings.WaitTime = (double)numFieldTime.Value;
                newSettings.AlarmOffset = (double)numFieldAlarm.Value;
                newSettings.NormallyClosed = checkBox1.CheckState == CheckState.Checked;

                ValueChanged(this, newSettings);
            }
        }
        private void numericUpDownValueChanged(object sender, EventArgs e)
        {
            if (settingChangeTimer != null)
            {
                settingChangeTimer.Stop();
                settingChangeTimer.Start();
            }
        }
        */


        public RegulationSettings GetNewSettings()
        {
            var newSettings = new RegulationSettings();
            newSettings.TargetValue = (double) numFieldTarget.Value;
            newSettings.Histeresis = (double) numFieldHisteress.Value;
            newSettings.WaitTime = (double) numFieldTime.Value;
            newSettings.AlarmOffset = (double) numFieldAlarm.Value;
            newSettings.NormallyClosed = checkBox1.CheckState == CheckState.Checked;
            return newSettings.IsComplete ? newSettings : null;
        }

        public void UpdateValueBoxes(RegulationSettings newSettings)
        {
            if (newSettings != null && newSettings.IsComplete)
            {
                this.SafeInvoke(() =>
                {
                    numFieldTarget.Value = (decimal) newSettings.TargetValue;
                    lN.Text = string.Format("{0:0.0}", newSettings.TargetValue);

                    numFieldHisteress.Value = (decimal) newSettings.Histeresis;
                    lH.Text = string.Format("{0:0.0}", newSettings.Histeresis);

                    numFieldTime.Value = (decimal) newSettings.WaitTime;
                    lO.Text = string.Format("{0:0.0}", newSettings.WaitTime);

                    numFieldAlarm.Value = (decimal) newSettings.AlarmOffset;
                    lA.Text = string.Format("{0:0.0}", newSettings.AlarmOffset);

                    //checkBox1.CheckState = newSettings.NormallyClosed ? CheckState.Checked : CheckState.Unchecked;
                    checkBox1.Checked = newSettings.NormallyClosed;
                    nc.Checked = newSettings.NormallyClosed;

                    Enabled = true;
                });
            }
        }
    }
}