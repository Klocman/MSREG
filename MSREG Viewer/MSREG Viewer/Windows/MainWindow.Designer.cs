namespace MSREG.Viewer.Windows
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aktualizacjaFirmwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logProgramuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprawdźAktualizacjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip = new System.Windows.Forms.SplitContainer();
            this.toolStripStatusLabel = new System.Windows.Forms.Label();
            this.tabList = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusStrip)).BeginInit();
            this.statusStrip.Panel1.SuspendLayout();
            this.statusStrip.Panel2.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabList.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.viewMenu,
            this.toolsMenu,
            this.windowsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(762, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator3,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator4,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.printSetupToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(34, 20);
            this.fileMenu.Text = "&Plik";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.newToolStripMenuItem.Text = "&Podłącz urządzenie...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.ShowConnectNew);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(214, 6);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.saveAsToolStripMenuItem.Text = "&Eksportuj dane";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(214, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Enabled = false;
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.printToolStripMenuItem.Text = "&Drukuj";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Enabled = false;
            this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.printPreviewToolStripMenuItem.Text = "&Podgląd drukowania";
            // 
            // printSetupToolStripMenuItem
            // 
            this.printSetupToolStripMenuItem.Enabled = false;
            this.printSetupToolStripMenuItem.Name = "printSetupToolStripMenuItem";
            this.printSetupToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.printSetupToolStripMenuItem.Text = "Ustawienia drukowania";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(214, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.exitToolStripMenuItem.Text = "&Wyjście";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(48, 20);
            this.viewMenu.Text = "&Widok";
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.statusBarToolStripMenuItem.Text = "&Pasek statusu";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.aktualizacjaFirmwareToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(66, 20);
            this.toolsMenu.Text = "&Narzędzia";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Enabled = false;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.optionsToolStripMenuItem.Text = "&Ustawienia";
            // 
            // aktualizacjaFirmwareToolStripMenuItem
            // 
            this.aktualizacjaFirmwareToolStripMenuItem.Name = "aktualizacjaFirmwareToolStripMenuItem";
            this.aktualizacjaFirmwareToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.aktualizacjaFirmwareToolStripMenuItem.Text = "Aktualizacja firmware";
            this.aktualizacjaFirmwareToolStripMenuItem.Click += new System.EventHandler(this.aktualizacjaFirmwareToolStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(44, 20);
            this.windowsMenu.Text = "&Okna";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.cascadeToolStripMenuItem.Text = "&Kaskaduj";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.tileVerticalToolStripMenuItem.Text = "Ułóż pio&nowo";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.tileHorizontalToolStripMenuItem.Text = "Ułóż po&ziomo";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.closeAllToolStripMenuItem.Text = "Zamknij &wszystkie";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.indexToolStripMenuItem,
            this.logProgramuToolStripMenuItem,
            this.sprawdźAktualizacjeToolStripMenuItem,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(50, 20);
            this.helpMenu.Text = "&Pomoc";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Enabled = false;
            this.indexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indexToolStripMenuItem.Image")));
            this.indexToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.indexToolStripMenuItem.Text = "&Pomoc";
            this.indexToolStripMenuItem.Click += new System.EventHandler(this.indexToolStripMenuItem_Click);
            // 
            // logProgramuToolStripMenuItem
            // 
            this.logProgramuToolStripMenuItem.Name = "logProgramuToolStripMenuItem";
            this.logProgramuToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.logProgramuToolStripMenuItem.Text = "&Log programu";
            this.logProgramuToolStripMenuItem.Click += new System.EventHandler(this.OpenLogViewer);
            // 
            // sprawdźAktualizacjeToolStripMenuItem
            // 
            this.sprawdźAktualizacjeToolStripMenuItem.Name = "sprawdźAktualizacjeToolStripMenuItem";
            this.sprawdźAktualizacjeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.sprawdźAktualizacjeToolStripMenuItem.Text = "Sprawdź aktualizacje";
            this.sprawdźAktualizacjeToolStripMenuItem.Click += new System.EventHandler(this.sprawdźAktualizacjeToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(171, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.aboutToolStripMenuItem.Text = "&O programie...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusStrip.Location = new System.Drawing.Point(0, 521);
            this.statusStrip.Name = "statusStrip";
            // 
            // statusStrip.Panel1
            // 
            this.statusStrip.Panel1.Controls.Add(this.toolStripStatusLabel);
            // 
            // statusStrip.Panel2
            // 
            this.statusStrip.Panel2.Controls.Add(this.tabList);
            this.statusStrip.Size = new System.Drawing.Size(762, 22);
            this.statusStrip.SplitterDistance = 385;
            this.statusStrip.TabIndex = 5;
            this.statusStrip.TabStop = false;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripStatusLabel.Location = new System.Drawing.Point(0, 0);
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(381, 18);
            this.toolStripStatusLabel.TabIndex = 0;
            this.toolStripStatusLabel.Text = "Status";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel.Click += new System.EventHandler(this.OpenLogViewer);
            // 
            // tabList
            // 
            this.tabList.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabList.Controls.Add(this.tabPage1);
            this.tabList.Controls.Add(this.tabPage2);
            this.tabList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabList.Location = new System.Drawing.Point(0, 0);
            this.tabList.Name = "tabList";
            this.tabList.SelectedIndex = 0;
            this.tabList.Size = new System.Drawing.Size(369, 18);
            this.tabList.TabIndex = 5;
            this.tabList.SelectedIndexChanged += new System.EventHandler(this.tabList_SelectedIndexChanged);
            this.tabList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabList_MouseDoubleClick);
            this.tabList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabList_MouseDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(361, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(361, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 543);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "MSREG Viewer";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.Panel1.ResumeLayout(false);
            this.statusStrip.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusStrip)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem printSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem logProgramuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.SplitContainer statusStrip;
        private System.Windows.Forms.Label toolStripStatusLabel;
        private System.Windows.Forms.TabControl tabList;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem sprawdźAktualizacjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aktualizacjaFirmwareToolStripMenuItem;
    }
}



