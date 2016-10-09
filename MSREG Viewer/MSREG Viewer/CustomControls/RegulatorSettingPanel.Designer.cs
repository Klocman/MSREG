namespace MSREG.Viewer.CustomControls
{
    partial class RegulatorSettingPanel
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
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.nc = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lA = new System.Windows.Forms.Label();
            this.lO = new System.Windows.Forms.Label();
            this.lH = new System.Windows.Forms.Label();
            this.lN = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numFieldTarget = new System.Windows.Forms.NumericUpDown();
            this.numFieldHisteress = new System.Windows.Forms.NumericUpDown();
            this.numFieldTime = new System.Windows.Forms.NumericUpDown();
            this.numFieldAlarm = new System.Windows.Forms.NumericUpDown();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldHisteress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldAlarm)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonRefresh);
            this.groupBox3.Controls.Add(this.buttonSend);
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.MinimumSize = new System.Drawing.Size(170, 152);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(410, 229);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ustawienia regulatora";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRefresh.Location = new System.Drawing.Point(6, 199);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(190, 23);
            this.buttonRefresh.TabIndex = 6;
            this.buttonRefresh.Text = "Odśwież obecne wartości";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(203, 199);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(201, 23);
            this.buttonSend.TabIndex = 5;
            this.buttonSend.Text = "Wyślij nowe wartości do regulatora";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.nc, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkBox1, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.lA, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lO, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lH, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lN, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.numFieldTarget, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numFieldHisteress, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.numFieldTime, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.numFieldAlarm, 2, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(398, 174);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(5, 139);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(134, 33);
            this.label12.TabIndex = 12;
            this.label12.Text = "Normalnie włączony (N/C)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nc
            // 
            this.nc.Checked = true;
            this.nc.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.nc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nc.Enabled = false;
            this.nc.Location = new System.Drawing.Point(147, 142);
            this.nc.Name = "nc";
            this.nc.Size = new System.Drawing.Size(99, 27);
            this.nc.TabIndex = 11;
            this.nc.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox1.Location = new System.Drawing.Point(254, 142);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(139, 27);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lA
            // 
            this.lA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lA.Location = new System.Drawing.Point(147, 111);
            this.lA.Name = "lA";
            this.lA.Size = new System.Drawing.Size(99, 26);
            this.lA.TabIndex = 9;
            this.lA.Text = "Brak";
            this.lA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lO
            // 
            this.lO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lO.Location = new System.Drawing.Point(147, 83);
            this.lO.Name = "lO";
            this.lO.Size = new System.Drawing.Size(99, 26);
            this.lO.TabIndex = 8;
            this.lO.Text = "Brak";
            this.lO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lH
            // 
            this.lH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lH.Location = new System.Drawing.Point(147, 55);
            this.lH.Name = "lH";
            this.lH.Size = new System.Drawing.Size(99, 26);
            this.lH.TabIndex = 7;
            this.lH.Text = "Brak";
            this.lH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lN
            // 
            this.lN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lN.Location = new System.Drawing.Point(147, 27);
            this.lN.Name = "lN";
            this.lN.Size = new System.Drawing.Size(99, 26);
            this.lN.TabIndex = 6;
            this.lN.Text = "Brak";
            this.lN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nazwa ustawienia";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(147, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Obecna wartość";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(5, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 26);
            this.label6.TabIndex = 1;
            this.label6.Text = "Poziom alarmowy";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(254, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 23);
            this.label7.TabIndex = 3;
            this.label7.Text = "Nowa wartość";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(5, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 26);
            this.label5.TabIndex = 1;
            this.label5.Text = "Odstęp przełączeń";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(5, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nastawa";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(5, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 26);
            this.label4.TabIndex = 1;
            this.label4.Text = "Histereza";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numFieldTarget
            // 
            this.numFieldTarget.DecimalPlaces = 1;
            this.numFieldTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numFieldTarget.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numFieldTarget.Location = new System.Drawing.Point(254, 30);
            this.numFieldTarget.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numFieldTarget.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            -2147483648});
            this.numFieldTarget.Name = "numFieldTarget";
            this.numFieldTarget.Size = new System.Drawing.Size(139, 20);
            this.numFieldTarget.TabIndex = 5;
            this.numFieldTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numFieldHisteress
            // 
            this.numFieldHisteress.DecimalPlaces = 1;
            this.numFieldHisteress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numFieldHisteress.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numFieldHisteress.Location = new System.Drawing.Point(254, 58);
            this.numFieldHisteress.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numFieldHisteress.Name = "numFieldHisteress";
            this.numFieldHisteress.Size = new System.Drawing.Size(139, 20);
            this.numFieldHisteress.TabIndex = 0;
            this.numFieldHisteress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numFieldTime
            // 
            this.numFieldTime.DecimalPlaces = 1;
            this.numFieldTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numFieldTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numFieldTime.Location = new System.Drawing.Point(254, 86);
            this.numFieldTime.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numFieldTime.Name = "numFieldTime";
            this.numFieldTime.Size = new System.Drawing.Size(139, 20);
            this.numFieldTime.TabIndex = 0;
            this.numFieldTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numFieldAlarm
            // 
            this.numFieldAlarm.DecimalPlaces = 1;
            this.numFieldAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numFieldAlarm.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numFieldAlarm.Location = new System.Drawing.Point(254, 114);
            this.numFieldAlarm.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numFieldAlarm.Name = "numFieldAlarm";
            this.numFieldAlarm.Size = new System.Drawing.Size(139, 20);
            this.numFieldAlarm.TabIndex = 0;
            this.numFieldAlarm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // RegulatorSettingPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.MinimumSize = new System.Drawing.Size(410, 229);
            this.Name = "RegulatorSettingPanel";
            this.Size = new System.Drawing.Size(410, 229);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numFieldTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldHisteress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFieldAlarm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numFieldAlarm;
        private System.Windows.Forms.NumericUpDown numFieldTime;
        private System.Windows.Forms.NumericUpDown numFieldHisteress;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox nc;
        private System.Windows.Forms.Label lA;
        private System.Windows.Forms.Label lO;
        private System.Windows.Forms.Label lH;
        private System.Windows.Forms.Label lN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numFieldTarget;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonSend;
    }
}
