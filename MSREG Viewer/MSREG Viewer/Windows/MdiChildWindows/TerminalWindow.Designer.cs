namespace MSREG.Viewer.Windows.MdiChildWindows
{
    sealed partial class TerminalWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalWindow));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.outputTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.inputTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.buttonSend);
            this.splitContainer1.Size = new System.Drawing.Size(292, 373);
            this.splitContainer1.SplitterDistance = 344;
            this.splitContainer1.TabIndex = 2;
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.Color.Black;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputTextBox.ForeColor = System.Drawing.Color.White;
            this.outputTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(292, 344);
            this.outputTextBox.TabIndex = 2;
            this.outputTextBox.Text = "";
            // 
            // inputTextBox
            // 
            this.inputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputTextBox.Location = new System.Drawing.Point(3, 2);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(205, 20);
            this.inputTextBox.TabIndex = 3;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(214, 2);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 21);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Wyślij";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // TerminalWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 373);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(150, 200);
            this.Name = "TerminalWindow";
            this.Text = "TerminalWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TerminalWindow_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.Button buttonSend;
    }
}