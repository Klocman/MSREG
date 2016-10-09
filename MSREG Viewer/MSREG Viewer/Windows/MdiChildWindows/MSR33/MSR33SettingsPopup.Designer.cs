using MSREG.Viewer.CustomControls;

namespace MSREG.Viewer.Windows.MdiChildWindows.MSR33
{
    sealed partial class Msr33SettingsPopup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Msr33SettingsPopup));
            this.regulatorSettingPanelHumidity = new RegulatorSettingPanel();
            this.regulatorSettingPanelTemperature = new RegulatorSettingPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // regulatorSettingPanelHumidity
            // 
            this.regulatorSettingPanelHumidity.AlarmValueMax = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.regulatorSettingPanelHumidity.Dock = System.Windows.Forms.DockStyle.Top;
            this.regulatorSettingPanelHumidity.HisteresisValueMax = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.regulatorSettingPanelHumidity.Location = new System.Drawing.Point(0, 229);
            this.regulatorSettingPanelHumidity.MaximumSize = new System.Drawing.Size(500, 0);
            this.regulatorSettingPanelHumidity.MinimumSize = new System.Drawing.Size(413, 229);
            this.regulatorSettingPanelHumidity.Name = "regulatorSettingPanelHumidity";
            this.regulatorSettingPanelHumidity.Size = new System.Drawing.Size(413, 229);
            this.regulatorSettingPanelHumidity.TabIndex = 9;
            this.regulatorSettingPanelHumidity.TargetValueMax = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.regulatorSettingPanelHumidity.TargetValueMin = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.regulatorSettingPanelHumidity.Text = "Regulacja wilgotnosci";
            this.regulatorSettingPanelHumidity.WaitValueMax = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // regulatorSettingPanelTemperature
            // 
            this.regulatorSettingPanelTemperature.AlarmValueMax = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.regulatorSettingPanelTemperature.Dock = System.Windows.Forms.DockStyle.Top;
            this.regulatorSettingPanelTemperature.HisteresisValueMax = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.regulatorSettingPanelTemperature.Location = new System.Drawing.Point(0, 0);
            this.regulatorSettingPanelTemperature.MaximumSize = new System.Drawing.Size(500, 0);
            this.regulatorSettingPanelTemperature.MinimumSize = new System.Drawing.Size(413, 229);
            this.regulatorSettingPanelTemperature.Name = "regulatorSettingPanelTemperature";
            this.regulatorSettingPanelTemperature.Size = new System.Drawing.Size(413, 229);
            this.regulatorSettingPanelTemperature.TabIndex = 8;
            this.regulatorSettingPanelTemperature.TargetValueMax = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.regulatorSettingPanelTemperature.TargetValueMin = new decimal(new int[] {
            40,
            0,
            0,
            -2147483648});
            this.regulatorSettingPanelTemperature.Text = "Regulacja temperatury";
            this.regulatorSettingPanelTemperature.WaitValueMax = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 465);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Zapisz ustawienia na stałe";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(5, 465);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Ustawienia domyślne";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(303, 465);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Wyjdź";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MSR33SettingsPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 493);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.regulatorSettingPanelHumidity);
            this.Controls.Add(this.regulatorSettingPanelTemperature);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Msr33SettingsPopup";
            this.Text = "MSR33SettingsPopup";
            this.ResumeLayout(false);

        }

        #endregion

        private RegulatorSettingPanel regulatorSettingPanelHumidity;
        private RegulatorSettingPanel regulatorSettingPanelTemperature;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

    }
}