namespace MSREG.FirmwareUpdater
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonRefreshPorts = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxActions.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "hex";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Intel hex|*.hex";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActions.Controls.Add(this.buttonUpload);
            this.groupBoxActions.Enabled = false;
            this.groupBoxActions.Location = new System.Drawing.Point(3, 137);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(250, 51);
            this.groupBoxActions.TabIndex = 10;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Akcje";
            // 
            // buttonUpload
            // 
            this.buttonUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpload.Location = new System.Drawing.Point(8, 19);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(236, 23);
            this.buttonUpload.TabIndex = 5;
            this.buttonUpload.Text = "Wyślij firmware do regulatora";
            this.toolTip1.SetToolTip(this.buttonUpload, "Rozpocznij proces aktualizacji regulatora.\r\nStan operacji jest wyświetlany poniże" +
        "j.\r\n\r\nPo rozpoczęciu wymagany jest restart regulatora.");
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.OpenConnection);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.statusLabel);
            this.groupBox2.Location = new System.Drawing.Point(2, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 65);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(6, 35);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(239, 23);
            this.progressBar1.TabIndex = 3;
            this.toolTip1.SetToolTip(this.progressBar1, "Postęp wysyłania nowego firmware. Jeżeli pasek jest w pozycji 0, proces jest jesz" +
        "cze odwracalny.");
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(6, 17);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(129, 13);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = "Oczekiwanie na komendy";
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSettings.Controls.Add(this.label1);
            this.groupBoxSettings.Controls.Add(this.comboBox1);
            this.groupBoxSettings.Controls.Add(this.label2);
            this.groupBoxSettings.Controls.Add(this.textBox1);
            this.groupBoxSettings.Controls.Add(this.buttonRefreshPorts);
            this.groupBoxSettings.Controls.Add(this.button1);
            this.groupBoxSettings.Controls.Add(this.checkBox1);
            this.groupBoxSettings.Location = new System.Drawing.Point(2, 2);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(251, 129);
            this.groupBoxSettings.TabIndex = 8;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Ustawienia";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Port regulatora";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 77);
            this.comboBox1.MaxDropDownItems = 20;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(158, 21);
            this.comboBox1.TabIndex = 2;
            this.toolTip1.SetToolTip(this.comboBox1, "Port pod który podpięty jest regulator. Jeżeli danego brakuje na liście, kliknij " +
        "na przycisk \"Odśwież\"");
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ValidateSettings);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ścieżka do pliku z firmware:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 20);
            this.textBox1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBox1, resources.GetString("textBox1.ToolTip"));
            this.textBox1.TextChanged += new System.EventHandler(this.ValidateSettings);
            // 
            // buttonRefreshPorts
            // 
            this.buttonRefreshPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefreshPorts.Location = new System.Drawing.Point(170, 75);
            this.buttonRefreshPorts.Name = "buttonRefreshPorts";
            this.buttonRefreshPorts.Size = new System.Drawing.Size(75, 23);
            this.buttonRefreshPorts.TabIndex = 3;
            this.buttonRefreshPorts.Text = "Odśwież";
            this.buttonRefreshPorts.UseVisualStyleBackColor = true;
            this.buttonRefreshPorts.Click += new System.EventHandler(this.RefreshPortList);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(170, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Przeglądaj...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(8, 105);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(174, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Automatyczny restart regulatora";
            this.toolTip1.SetToolTip(this.checkBox1, "Jeżeli regulator jest włączony i działa prawidłowo, automatycznie go zrestartuj.\r" +
        "\nW przeciwnym wypadku wymagane jest ręczne włączenie zasilania regulatora po sta" +
        "rcie procesu aktualizacji.");
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 260);
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "MSREG Firmware Updater";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxActions.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonRefreshPorts;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}

