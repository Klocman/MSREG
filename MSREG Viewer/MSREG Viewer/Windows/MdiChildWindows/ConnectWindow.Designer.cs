namespace MSREG.Viewer.Windows.MdiChildWindows
{
    partial class ConnectWindow
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
                _connectionTester.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectWindow));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.devicebuttonOk = new System.Windows.Forms.Button();
            this.devicebuttonRef = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.devicelistBox = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.portbuttonOk = new System.Windows.Forms.Button();
            this.portbuttonRef = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.portlistBox = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(289, 321);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.devicebuttonOk);
            this.tabPage1.Controls.Add(this.devicebuttonRef);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.devicelistBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(281, 295);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Panel sterowania";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // devicebuttonOk
            // 
            this.devicebuttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.devicebuttonOk.Location = new System.Drawing.Point(113, 266);
            this.devicebuttonOk.Name = "devicebuttonOk";
            this.devicebuttonOk.Size = new System.Drawing.Size(160, 23);
            this.devicebuttonOk.TabIndex = 5;
            this.devicebuttonOk.Text = "Otwórz panel sterowania";
            this.devicebuttonOk.UseVisualStyleBackColor = true;
            this.devicebuttonOk.Click += new System.EventHandler(this.devicebuttonOk_Click);
            // 
            // devicebuttonRef
            // 
            this.devicebuttonRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.devicebuttonRef.Location = new System.Drawing.Point(8, 266);
            this.devicebuttonRef.Name = "devicebuttonRef";
            this.devicebuttonRef.Size = new System.Drawing.Size(99, 23);
            this.devicebuttonRef.TabIndex = 6;
            this.devicebuttonRef.Text = "Odśwież listę";
            this.devicebuttonRef.UseVisualStyleBackColor = true;
            this.devicebuttonRef.Click += new System.EventHandler(this.RefreshDeviceList);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(8, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(265, 57);
            this.label2.TabIndex = 4;
            this.label2.Text = "Otwórz panel sterowania wybranego urządzenia.\r\nPoniższa lista zawiera automatyczn" +
    "ie wykryte urządzenia podłączone do tego komputera, które nie są używane przez ż" +
    "adne inne okna/aplikacje.";
            // 
            // devicelistBox
            // 
            this.devicelistBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.devicelistBox.FormattingEnabled = true;
            this.devicelistBox.IntegralHeight = false;
            this.devicelistBox.Location = new System.Drawing.Point(8, 66);
            this.devicelistBox.Name = "devicelistBox";
            this.devicelistBox.Size = new System.Drawing.Size(265, 194);
            this.devicelistBox.TabIndex = 3;
            this.devicelistBox.DoubleClick += new System.EventHandler(this.devicebuttonOk_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.portbuttonOk);
            this.tabPage2.Controls.Add(this.portbuttonRef);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.portlistBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(281, 295);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Terminal";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // portbuttonOk
            // 
            this.portbuttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.portbuttonOk.Location = new System.Drawing.Point(113, 266);
            this.portbuttonOk.Name = "portbuttonOk";
            this.portbuttonOk.Size = new System.Drawing.Size(160, 23);
            this.portbuttonOk.TabIndex = 9;
            this.portbuttonOk.Text = "Otwórz w terminalu";
            this.portbuttonOk.UseVisualStyleBackColor = true;
            this.portbuttonOk.Click += new System.EventHandler(this.OpenPort);
            // 
            // portbuttonRef
            // 
            this.portbuttonRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.portbuttonRef.Location = new System.Drawing.Point(8, 266);
            this.portbuttonRef.Name = "portbuttonRef";
            this.portbuttonRef.Size = new System.Drawing.Size(99, 23);
            this.portbuttonRef.TabIndex = 10;
            this.portbuttonRef.Text = "Odśwież listę";
            this.portbuttonRef.UseVisualStyleBackColor = true;
            this.portbuttonRef.Click += new System.EventHandler(this.RefreshPortList);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 57);
            this.label1.TabIndex = 8;
            this.label1.Text = "Otwórz okno terminalu dla wybranego portu. Lista zawiera wszystkie porty podłączo" +
    "ne do tego komputera. Jeżeli port jest już używany przez inne okno/program, jego" +
    " otwarcie zakończy się błędem.";
            // 
            // portlistBox
            // 
            this.portlistBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portlistBox.FormattingEnabled = true;
            this.portlistBox.IntegralHeight = false;
            this.portlistBox.Location = new System.Drawing.Point(8, 66);
            this.portlistBox.Name = "portlistBox";
            this.portlistBox.Size = new System.Drawing.Size(265, 194);
            this.portlistBox.TabIndex = 7;
            this.portlistBox.DoubleClick += new System.EventHandler(this.OpenPort);
            // 
            // ConnectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 321);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(295, 350);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(295, 150);
            this.Name = "ConnectWindow";
            this.Text = "Podłącz urządzenie...";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectWindow_FormClosing);
            this.Shown += new System.EventHandler(this.ConnectWindow_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button devicebuttonOk;
        private System.Windows.Forms.Button devicebuttonRef;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox devicelistBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button portbuttonOk;
        private System.Windows.Forms.Button portbuttonRef;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox portlistBox;

    }
}