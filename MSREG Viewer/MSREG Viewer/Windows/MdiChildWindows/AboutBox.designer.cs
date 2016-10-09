namespace MSREG.Viewer.Windows.MdiChildWindows
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.okButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelCpu = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelLicencja = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(9, 9);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.logoPictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.okButton);
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(454, 335);
            this.splitContainer1.SplitterDistance = 217;
            this.splitContainer1.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = global::MSREG.Viewer.Properties.Resources.aboutImage;
            this.logoPictureBox.InitialImage = null;
            this.logoPictureBox.Location = new System.Drawing.Point(0, 0);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(213, 331);
            this.logoPictureBox.TabIndex = 13;
            this.logoPictureBox.TabStop = false;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(151, 305);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelCpu, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelProductName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelVersion, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxDescription, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelCompanyName, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelCopyright, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelLicencja, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(229, 321);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // labelCpu
            // 
            this.labelCpu.AutoSize = true;
            this.labelCpu.Location = new System.Drawing.Point(6, 40);
            this.labelCpu.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelCpu.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCpu.Name = "labelCpu";
            this.labelCpu.Size = new System.Drawing.Size(120, 13);
            this.labelCpu.TabIndex = 26;
            this.labelCpu.Text = "Architektura programu:  ";
            this.labelCpu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.Location = new System.Drawing.Point(6, 0);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(96, 13);
            this.labelProductName.TabIndex = 25;
            this.labelProductName.Text = "Nazwa programu:  ";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(6, 20);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(49, 13);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "Wersja:  ";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDescription.Location = new System.Drawing.Point(6, 163);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDescription.Size = new System.Drawing.Size(220, 124);
            this.textBoxDescription.TabIndex = 27;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = "Description";
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.AutoSize = true;
            this.labelCompanyName.Location = new System.Drawing.Point(6, 60);
            this.labelCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(52, 13);
            this.labelCompanyName.TabIndex = 23;
            this.labelCompanyName.Text = "Twórca:  ";
            this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(6, 80);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(60, 13);
            this.labelCopyright.TabIndex = 21;
            this.labelCopyright.Text = "Copyright:  ";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelLicencja
            // 
            this.labelLicencja.AutoSize = true;
            this.labelLicencja.Location = new System.Drawing.Point(6, 100);
            this.labelLicencja.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelLicencja.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelLicencja.Name = "labelLicencja";
            this.labelLicencja.Size = new System.Drawing.Size(100, 13);
            this.labelLicencja.TabIndex = 28;
            this.labelLicencja.Text = "Licencja:  Freeware";
            this.labelLicencja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(6, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 40);
            this.label1.TabIndex = 30;
            this.label1.Text = "Ikony: http://www.interactivemania.com\r\nWykresy: ZedGraph";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 353);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(478, 378);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(478, 378);
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "O programie";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.AboutBox_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelCpu;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelLicencja;
        private System.Windows.Forms.Label label1;

    }
}
