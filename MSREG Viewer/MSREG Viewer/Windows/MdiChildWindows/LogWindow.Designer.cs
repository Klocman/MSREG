namespace MSREG.Viewer.Windows.MdiChildWindows
{
    partial class LogWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogWindow));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logList = new System.Windows.Forms.RichTextBox();
            this.saveLogButton = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.filterSourceComboBox = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
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
            this.splitContainer1.Panel1.Controls.Add(this.logList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.saveLogButton);
            this.splitContainer1.Panel2.Controls.Add(this.buttonClear);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.filterSourceComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox1);
            this.splitContainer1.Size = new System.Drawing.Size(692, 403);
            this.splitContainer1.SplitterDistance = 372;
            this.splitContainer1.TabIndex = 1;
            // 
            // logList
            // 
            this.logList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logList.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logList.Location = new System.Drawing.Point(0, 0);
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(692, 372);
            this.logList.TabIndex = 2;
            this.logList.Text = "";
            this.logList.SelectionChanged += new System.EventHandler(this.logList_SelectionChanged);
            // 
            // saveLogButton
            // 
            this.saveLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveLogButton.Location = new System.Drawing.Point(84, 3);
            this.saveLogButton.Name = "saveLogButton";
            this.saveLogButton.Size = new System.Drawing.Size(75, 21);
            this.saveLogButton.TabIndex = 2;
            this.saveLogButton.Text = "Zapisz...";
            this.saveLogButton.UseVisualStyleBackColor = true;
            this.saveLogButton.Click += new System.EventHandler(this.saveLogButton_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClear.Location = new System.Drawing.Point(3, 3);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 21);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Wyczyść log";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(205, 6);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(112, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Filtruj źródło";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(450, 6);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(112, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Minimalny poziom logu";
            // 
            // filterSourceComboBox
            // 
            this.filterSourceComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.filterSourceComboBox.FormattingEnabled = true;
            this.filterSourceComboBox.Location = new System.Drawing.Point(323, 3);
            this.filterSourceComboBox.Name = "filterSourceComboBox";
            this.filterSourceComboBox.Size = new System.Drawing.Size(121, 21);
            this.filterSourceComboBox.TabIndex = 0;
            this.filterSourceComboBox.SelectedIndexChanged += new System.EventHandler(this.filterSourceComboBox_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(568, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Pliki logów|*.log|Pliki tekstowe|*.txt|Wszystkie pliki|*.*";
            this.saveFileDialog1.Title = "Zapisz log do pliku...";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // LogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 403);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogWindow";
            this.Text = "Log programu";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox logList;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox filterSourceComboBox;
        private System.Windows.Forms.Button saveLogButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}