using System;
using System.Linq;
using System.Windows.Forms;
using Klocman.Extensions;
using Klocman.Subsystems;
using MSREG.Viewer.CustomControls;
using MSREG.Viewer.SpecialClasses;
using MSREG.Viewer.Windows.MdiChildWindows;

namespace MSREG.Viewer.Windows
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            tabList.TabPages.Clear();

            ShowConnectNew(null, null);
            AppLog.EntryAdded += entry =>
            {
                if (entry.Type >= LogEntryType.Info)
                    this.SafeInvoke(() => toolStripStatusLabel.Text = entry.ToShortString());
            };
            toolStripStatusLabel.Text = AppLog.LogEntries.First().ToShortString();
        }

        private void ShowConnectNew(object sender, EventArgs e)
        {
            foreach (var cform in MdiChildren)
            {
                if (cform is ConnectWindow)
                {
                    cform.Select();
                    return;
                }
            }

            var connectionWindow = new ConnectWindow();
            connectionWindow.SetupAndShowMdiChildForm(connectionWindow.Text, this, tabList);
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void OpenLogViewer(object sender, EventArgs e)
        {
            foreach (var cform in MdiChildren)
            {
                if (cform is LogWindow)
                {
                    cform.Select();
                    return;
                }
            }

            var logWindow = new LogWindow();
            logWindow.SetupAndShowMdiChildForm(logWindow.Text, this, tabList);
        }

        private void tabList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (MdiChildForm childForm in MdiChildren)
            {
                //Check for its corresponding MDI child form
                if (childForm.ChildTabPage.Equals(tabList.SelectedTab))
                {
                    //Activate the MDI child form
                    childForm.Select();
                    break;
                }
            }
        }

        private void tabList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (MdiChildForm childForm in MdiChildren)
            {
                if (childForm.ChildTabPage.Equals(tabList.SelectedTab))
                {
                    switch (childForm.WindowState)
                    {
                        case FormWindowState.Minimized:
                        case FormWindowState.Maximized:
                            childForm.WindowState = FormWindowState.Normal;
                            break;
                        case FormWindowState.Normal:
                            if (childForm.MaximizeBox)
                                childForm.WindowState = FormWindowState.Maximized;
                            break;
                    }
                    childForm.Select();
                    break;
                }
            }
        }

        private void tabList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Middle)
                return;

            foreach (MdiChildForm childForm in MdiChildren)
            {
                if (childForm.ChildTabPage.Equals(tabList.SelectedTab))
                {
                    childForm.Close();
                    break;
                }
            }
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var cform in MdiChildren)
            {
                if (cform is HelpWindow)
                {
                    cform.Select();
                    return;
                }
            }

            var logWindow = new HelpWindow();
            logWindow.SetupAndShowMdiChildForm(logWindow.Text, this, tabList);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var cform in MdiChildren)
            {
                if (cform is AboutBox)
                {
                    cform.Select();
                    return;
                }
            }

            var logWindow = new AboutBox();
            logWindow.SetupAndShowMdiChildForm(logWindow.Text, this, tabList);
        }

        private void sprawdźAktualizacjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateChecker.CheckForUpdatesInBackground();
        }

        private void aktualizacjaFirmwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Przed używaniem aktualizatora upewnij się że regulator nie jest podłączony w żadnym oknie.",
                "Aktualizacja firmware", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FirmwareUpdater.OpenUpdater();
        }
    }
}