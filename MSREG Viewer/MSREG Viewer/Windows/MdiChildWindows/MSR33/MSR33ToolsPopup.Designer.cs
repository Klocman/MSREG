namespace MSREG.Viewer.Windows.MdiChildWindows.MSR33
{
    sealed partial class Msr33ToolsPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Msr33ToolsPopup));
            this.buttonRes = new System.Windows.Forms.Button();
            this.buttonUpdt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonDatasheet = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRes
            // 
            this.buttonRes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRes.Location = new System.Drawing.Point(6, 77);
            this.buttonRes.Name = "buttonRes";
            this.buttonRes.Size = new System.Drawing.Size(222, 23);
            this.buttonRes.TabIndex = 0;
            this.buttonRes.Text = "Restartuj regulator";
            this.buttonRes.UseVisualStyleBackColor = true;
            this.buttonRes.Click += new System.EventHandler(this.buttonRes_Click);
            // 
            // buttonUpdt
            // 
            this.buttonUpdt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdt.Location = new System.Drawing.Point(6, 48);
            this.buttonUpdt.Name = "buttonUpdt";
            this.buttonUpdt.Size = new System.Drawing.Size(222, 23);
            this.buttonUpdt.TabIndex = 0;
            this.buttonUpdt.Text = "Aktualizacja firmware";
            this.buttonUpdt.UseVisualStyleBackColor = true;
            this.buttonUpdt.Click += new System.EventHandler(this.buttonUpdt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRes);
            this.groupBox1.Controls.Add(this.buttonDatasheet);
            this.groupBox1.Controls.Add(this.buttonUpdt);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 108);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nażędzia";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 135);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informacje o regulatorze";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(228, 116);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "";
            // 
            // buttonDatasheet
            // 
            this.buttonDatasheet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDatasheet.Location = new System.Drawing.Point(6, 19);
            this.buttonDatasheet.Name = "buttonDatasheet";
            this.buttonDatasheet.Size = new System.Drawing.Size(222, 23);
            this.buttonDatasheet.TabIndex = 0;
            this.buttonDatasheet.Text = "Instrukcja obsługi i specyfikacja";
            this.buttonDatasheet.UseVisualStyleBackColor = true;
            this.buttonDatasheet.Click += new System.EventHandler(this.buttonDatasheet_Click);
            // 
            // MSR33ToolsPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 243);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Msr33ToolsPopup";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRes;
        private System.Windows.Forms.Button buttonUpdt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox textBox1;
        private System.Windows.Forms.Button buttonDatasheet;
    }
}