namespace MSREG.Viewer.CustomControls
{
    partial class DeviceControls
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.xLastMinuteCount = new System.Windows.Forms.NumericUpDown();
            this.radioXlastmins = new System.Windows.Forms.RadioButton();
            this.radioXAuto = new System.Windows.Forms.RadioButton();
            this.spacer1 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.buttonTools = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonConnection = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.infoLabelStatus = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.infoLabelPort = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xLastMinuteCount)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.xLastMinuteCount);
            this.groupBox3.Controls.Add(this.radioXlastmins);
            this.groupBox3.Controls.Add(this.radioXAuto);
            this.groupBox3.Controls.Add(this.spacer1);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.buttonTools);
            this.groupBox3.Controls.Add(this.buttonSettings);
            this.groupBox3.Controls.Add(this.buttonConnection);
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(175, 231);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Panel kontrolny";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Dodawaj pomiary do wykresów...";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(6, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 2);
            this.label1.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "sekund";
            // 
            // xLastMinuteCount
            // 
            this.xLastMinuteCount.DecimalPlaces = 1;
            this.xLastMinuteCount.Location = new System.Drawing.Point(47, 203);
            this.xLastMinuteCount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            65536});
            this.xLastMinuteCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xLastMinuteCount.Name = "xLastMinuteCount";
            this.xLastMinuteCount.Size = new System.Drawing.Size(63, 20);
            this.xLastMinuteCount.TabIndex = 9;
            this.xLastMinuteCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xLastMinuteCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.xLastMinuteCount.ValueChanged += new System.EventHandler(this.OnGraphPlottingSpeedChanged);
            // 
            // radioXlastmins
            // 
            this.radioXlastmins.AutoSize = true;
            this.radioXlastmins.Location = new System.Drawing.Point(9, 204);
            this.radioXlastmins.Name = "radioXlastmins";
            this.radioXlastmins.Size = new System.Drawing.Size(38, 17);
            this.radioXlastmins.TabIndex = 7;
            this.radioXlastmins.Text = "Co";
            this.radioXlastmins.UseVisualStyleBackColor = true;
            this.radioXlastmins.CheckedChanged += new System.EventHandler(this.OnGraphPlottingSpeedChanged);
            // 
            // radioXAuto
            // 
            this.radioXAuto.AutoSize = true;
            this.radioXAuto.Checked = true;
            this.helpProvider1.SetHelpString(this.radioXAuto, "Dodaje pomiary wtedy gdy regulator odczytuje dane z czujnika.");
            this.radioXAuto.Location = new System.Drawing.Point(9, 181);
            this.radioXAuto.Name = "radioXAuto";
            this.helpProvider1.SetShowHelp(this.radioXAuto, true);
            this.radioXAuto.Size = new System.Drawing.Size(97, 17);
            this.radioXAuto.TabIndex = 8;
            this.radioXAuto.TabStop = true;
            this.radioXAuto.Text = "Jak najczęściej";
            this.radioXAuto.UseVisualStyleBackColor = true;
            // 
            // spacer1
            // 
            this.spacer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spacer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spacer1.Location = new System.Drawing.Point(6, 89);
            this.spacer1.Name = "spacer1";
            this.spacer1.Size = new System.Drawing.Size(163, 2);
            this.spacer1.TabIndex = 6;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.helpProvider1.SetHelpString(this.checkBox2, "Automatycznie wznów połączenie z regulatorem jeżeli zostanie ono przerwane.");
            this.checkBox2.Location = new System.Drawing.Point(98, 63);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.helpProvider1.SetShowHelp(this.checkBox2, true);
            this.checkBox2.Size = new System.Drawing.Size(68, 17);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "Automat.";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // buttonTools
            // 
            this.buttonTools.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTools.Location = new System.Drawing.Point(6, 126);
            this.buttonTools.Name = "buttonTools";
            this.buttonTools.Size = new System.Drawing.Size(162, 23);
            this.buttonTools.TabIndex = 4;
            this.buttonTools.Text = "Narzędzia i informacje\r\n";
            this.buttonTools.UseVisualStyleBackColor = true;
            this.buttonTools.Click += new System.EventHandler(this.OpenToolsPopup);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSettings.Location = new System.Drawing.Point(6, 97);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(162, 23);
            this.buttonSettings.TabIndex = 4;
            this.buttonSettings.Text = "Ustawienia regulatora";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.OpenSettingsPopup);
            // 
            // buttonConnection
            // 
            this.buttonConnection.Location = new System.Drawing.Point(6, 59);
            this.buttonConnection.Name = "buttonConnection";
            this.buttonConnection.Size = new System.Drawing.Size(86, 23);
            this.buttonConnection.TabIndex = 4;
            this.buttonConnection.Text = "Odłącz";
            this.buttonConnection.UseVisualStyleBackColor = true;
            this.buttonConnection.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.54546F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            this.tableLayoutPanel2.Controls.Add(this.label16, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.infoLabelStatus, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label18, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.infoLabelPort, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(169, 39);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(3, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 20);
            this.label16.TabIndex = 0;
            this.label16.Text = "Status polaczenia";
            // 
            // infoLabelStatus
            // 
            this.infoLabelStatus.Location = new System.Drawing.Point(95, 0);
            this.infoLabelStatus.Name = "infoLabelStatus";
            this.infoLabelStatus.Size = new System.Drawing.Size(71, 20);
            this.infoLabelStatus.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(3, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 20);
            this.label18.TabIndex = 0;
            this.label18.Text = "Port";
            // 
            // infoLabelPort
            // 
            this.infoLabelPort.Location = new System.Drawing.Point(95, 20);
            this.infoLabelPort.Name = "infoLabelPort";
            this.infoLabelPort.Size = new System.Drawing.Size(71, 20);
            this.infoLabelPort.TabIndex = 0;
            // 
            // DeviceControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Name = "DeviceControls";
            this.Size = new System.Drawing.Size(175, 231);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xLastMinuteCount)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label spacer1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonConnection;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label infoLabelStatus;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label infoLabelPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown xLastMinuteCount;
        private System.Windows.Forms.RadioButton radioXlastmins;
        private System.Windows.Forms.RadioButton radioXAuto;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button buttonTools;
    }
}
