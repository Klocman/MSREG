using MSREG.Viewer.CustomControls;

namespace MSREG.Viewer.Windows.MdiChildWindows.MSR33
{
    sealed partial class Msr33Window
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
                _regulator.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Msr33Window));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.zedGraphTemperature = new ZedGraph.ZedGraphControl();
            this.zedGraphHumidity = new ZedGraph.ZedGraphControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.infoLabelDate = new System.Windows.Forms.Label();
            this.infoLabelVer = new System.Windows.Forms.Label();
            this.infoLabelRev = new System.Windows.Forms.Label();
            this.infoLabelModel = new System.Windows.Forms.Label();
            this.measurementDisplay1 = new MSREG.Viewer.CustomControls.MeasurementDisplay();
            this.ScaleYgroupBox = new System.Windows.Forms.GroupBox();
            this.radioYmanual = new System.Windows.Forms.RadioButton();
            this.radioYauto = new System.Windows.Forms.RadioButton();
            this.radioYlimits = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioXmanual = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.xLastMinuteCount = new System.Windows.Forms.NumericUpDown();
            this.radioXlastmins = new System.Windows.Forms.RadioButton();
            this.radioXAuto = new System.Windows.Forms.RadioButton();
            this.deviceControls1 = new DeviceControls();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.ScaleYgroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xLastMinuteCount)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.measurementDisplay1);
            this.splitContainer1.Panel2.Controls.Add(this.ScaleYgroupBox);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.deviceControls1);
            this.splitContainer1.Size = new System.Drawing.Size(692, 423);
            this.splitContainer1.SplitterDistance = 497;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.zedGraphTemperature);
            this.splitContainer2.Panel1MinSize = 100;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.zedGraphHumidity);
            this.splitContainer2.Panel2MinSize = 100;
            this.splitContainer2.Size = new System.Drawing.Size(497, 423);
            this.splitContainer2.SplitterDistance = 210;
            this.splitContainer2.TabIndex = 0;
            // 
            // zedGraphTemperature
            // 
            this.zedGraphTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphTemperature.Location = new System.Drawing.Point(0, 0);
            this.zedGraphTemperature.Name = "zedGraphTemperature";
            this.zedGraphTemperature.ScrollGrace = 0D;
            this.zedGraphTemperature.ScrollMaxX = 0D;
            this.zedGraphTemperature.ScrollMaxY = 0D;
            this.zedGraphTemperature.ScrollMaxY2 = 0D;
            this.zedGraphTemperature.ScrollMinX = 0D;
            this.zedGraphTemperature.ScrollMinY = 0D;
            this.zedGraphTemperature.ScrollMinY2 = 0D;
            this.zedGraphTemperature.Size = new System.Drawing.Size(493, 206);
            this.zedGraphTemperature.TabIndex = 2;
            this.zedGraphTemperature.ZoomEvent += new ZedGraph.ZedGraphControl.ZoomEventHandler(this.zedGraphControl_ZoomEvent);
            // 
            // zedGraphHumidity
            // 
            this.zedGraphHumidity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphHumidity.Location = new System.Drawing.Point(0, 0);
            this.zedGraphHumidity.Name = "zedGraphHumidity";
            this.zedGraphHumidity.ScrollGrace = 0D;
            this.zedGraphHumidity.ScrollMaxX = 0D;
            this.zedGraphHumidity.ScrollMaxY = 0D;
            this.zedGraphHumidity.ScrollMaxY2 = 0D;
            this.zedGraphHumidity.ScrollMinX = 0D;
            this.zedGraphHumidity.ScrollMinY = 0D;
            this.zedGraphHumidity.ScrollMinY2 = 0D;
            this.zedGraphHumidity.Size = new System.Drawing.Size(493, 205);
            this.zedGraphHumidity.TabIndex = 3;
            this.zedGraphHumidity.ZoomEvent += new ZedGraph.ZedGraphControl.ZoomEventHandler(this.zedGraphControl_ZoomEvent);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 653);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 97);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dane techniczne regulatora";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.54546F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.infoLabelDate, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.infoLabelVer, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.infoLabelRev, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.infoLabelModel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(169, 78);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Wersja hardware";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Wersja firmware";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Data firmware";
            // 
            // infoLabelDate
            // 
            this.infoLabelDate.Location = new System.Drawing.Point(95, 60);
            this.infoLabelDate.Name = "infoLabelDate";
            this.infoLabelDate.Size = new System.Drawing.Size(71, 20);
            this.infoLabelDate.TabIndex = 0;
            // 
            // infoLabelVer
            // 
            this.infoLabelVer.Location = new System.Drawing.Point(95, 40);
            this.infoLabelVer.Name = "infoLabelVer";
            this.infoLabelVer.Size = new System.Drawing.Size(71, 20);
            this.infoLabelVer.TabIndex = 0;
            // 
            // infoLabelRev
            // 
            this.infoLabelRev.Location = new System.Drawing.Point(95, 20);
            this.infoLabelRev.Name = "infoLabelRev";
            this.infoLabelRev.Size = new System.Drawing.Size(71, 20);
            this.infoLabelRev.TabIndex = 0;
            // 
            // infoLabelModel
            // 
            this.infoLabelModel.Location = new System.Drawing.Point(95, 0);
            this.infoLabelModel.Name = "infoLabelModel";
            this.infoLabelModel.Size = new System.Drawing.Size(71, 20);
            this.infoLabelModel.TabIndex = 0;
            // 
            // measurementDisplay1
            // 
            this.measurementDisplay1.Dock = System.Windows.Forms.DockStyle.Top;
            this.measurementDisplay1.Location = new System.Drawing.Point(0, 408);
            this.measurementDisplay1.Name = "measurementDisplay1";
            this.measurementDisplay1.Size = new System.Drawing.Size(175, 245);
            this.measurementDisplay1.TabIndex = 10;
            // 
            // ScaleYgroupBox
            // 
            this.ScaleYgroupBox.Controls.Add(this.radioYmanual);
            this.ScaleYgroupBox.Controls.Add(this.radioYauto);
            this.ScaleYgroupBox.Controls.Add(this.radioYlimits);
            this.ScaleYgroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ScaleYgroupBox.Location = new System.Drawing.Point(0, 319);
            this.ScaleYgroupBox.Name = "ScaleYgroupBox";
            this.ScaleYgroupBox.Size = new System.Drawing.Size(175, 89);
            this.ScaleYgroupBox.TabIndex = 3;
            this.ScaleYgroupBox.TabStop = false;
            this.ScaleYgroupBox.Text = "Skala Y (wartości)";
            // 
            // radioYmanual
            // 
            this.radioYmanual.AutoSize = true;
            this.radioYmanual.Location = new System.Drawing.Point(9, 65);
            this.radioYmanual.Name = "radioYmanual";
            this.radioYmanual.Size = new System.Drawing.Size(61, 17);
            this.radioYmanual.TabIndex = 4;
            this.radioYmanual.Text = "Ręczny";
            this.radioYmanual.UseVisualStyleBackColor = true;
            this.radioYmanual.CheckedChanged += new System.EventHandler(this.radioYmanual_CheckedChanged);
            // 
            // radioYauto
            // 
            this.radioYauto.AutoSize = true;
            this.radioYauto.Checked = true;
            this.radioYauto.Location = new System.Drawing.Point(10, 19);
            this.radioYauto.Name = "radioYauto";
            this.radioYauto.Size = new System.Drawing.Size(91, 17);
            this.radioYauto.TabIndex = 0;
            this.radioYauto.TabStop = true;
            this.radioYauto.Text = "Automatyczny";
            this.radioYauto.UseVisualStyleBackColor = true;
            // 
            // radioYlimits
            // 
            this.radioYlimits.AutoSize = true;
            this.radioYlimits.Location = new System.Drawing.Point(10, 42);
            this.radioYlimits.Name = "radioYlimits";
            this.radioYlimits.Size = new System.Drawing.Size(112, 17);
            this.radioYlimits.TabIndex = 0;
            this.radioYlimits.Text = "Granice regulatora";
            this.radioYlimits.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioXmanual);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.xLastMinuteCount);
            this.groupBox2.Controls.Add(this.radioXlastmins);
            this.groupBox2.Controls.Add(this.radioXAuto);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 89);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Skala X (czas)";
            // 
            // radioXmanual
            // 
            this.radioXmanual.AutoSize = true;
            this.radioXmanual.Location = new System.Drawing.Point(10, 65);
            this.radioXmanual.Name = "radioXmanual";
            this.radioXmanual.Size = new System.Drawing.Size(61, 17);
            this.radioXmanual.TabIndex = 4;
            this.radioXmanual.Text = "Ręczny";
            this.radioXmanual.UseVisualStyleBackColor = true;
            this.radioXmanual.CheckedChanged += new System.EventHandler(this.radioXmanual_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "minut";
            // 
            // xLastMinuteCount
            // 
            this.xLastMinuteCount.Location = new System.Drawing.Point(74, 41);
            this.xLastMinuteCount.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.xLastMinuteCount.Name = "xLastMinuteCount";
            this.xLastMinuteCount.Size = new System.Drawing.Size(62, 20);
            this.xLastMinuteCount.TabIndex = 2;
            this.xLastMinuteCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xLastMinuteCount.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // radioXlastmins
            // 
            this.radioXlastmins.AutoSize = true;
            this.radioXlastmins.Location = new System.Drawing.Point(10, 42);
            this.radioXlastmins.Name = "radioXlastmins";
            this.radioXlastmins.Size = new System.Drawing.Size(64, 17);
            this.radioXlastmins.TabIndex = 0;
            this.radioXlastmins.Text = "Ostatnie";
            this.radioXlastmins.UseVisualStyleBackColor = true;
            // 
            // radioXAuto
            // 
            this.radioXAuto.AutoSize = true;
            this.radioXAuto.Checked = true;
            this.radioXAuto.Location = new System.Drawing.Point(10, 19);
            this.radioXAuto.Name = "radioXAuto";
            this.radioXAuto.Size = new System.Drawing.Size(91, 17);
            this.radioXAuto.TabIndex = 0;
            this.radioXAuto.TabStop = true;
            this.radioXAuto.Text = "Automatyczny";
            this.radioXAuto.UseVisualStyleBackColor = true;
            // 
            // deviceControls1
            // 
            this.deviceControls1.Dock = System.Windows.Forms.DockStyle.Top;
            this.deviceControls1.Location = new System.Drawing.Point(0, 0);
            this.deviceControls1.Name = "deviceControls1";
            this.deviceControls1.Size = new System.Drawing.Size(175, 230);
            this.deviceControls1.TabIndex = 9;
            this.deviceControls1.TargetDevice = null;
            this.deviceControls1.GraphPlottingSpeedChanged += new System.Action<decimal>(this.deviceControls1_GraphPlottingSpeedChanged);
            // 
            // MSR33Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 423);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 250);
            this.Name = "Msr33Window";
            this.Text = "MSR33Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MSR33Window_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ScaleYgroupBox.ResumeLayout(false);
            this.ScaleYgroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xLastMinuteCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ZedGraph.ZedGraphControl zedGraphTemperature;
        private ZedGraph.ZedGraphControl zedGraphHumidity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown xLastMinuteCount;
        private System.Windows.Forms.RadioButton radioXlastmins;
        private System.Windows.Forms.RadioButton radioXAuto;
        private System.Windows.Forms.GroupBox ScaleYgroupBox;
        private System.Windows.Forms.RadioButton radioYauto;
        private System.Windows.Forms.RadioButton radioYlimits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioYmanual;
        private System.Windows.Forms.RadioButton radioXmanual;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label infoLabelDate;
        private System.Windows.Forms.Label infoLabelVer;
        private System.Windows.Forms.Label infoLabelRev;
        private System.Windows.Forms.Label infoLabelModel;
        private DeviceControls deviceControls1;
        private CustomControls.MeasurementDisplay measurementDisplay1;


    }
}