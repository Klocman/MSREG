using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MSREG.Viewer.CustomControls
{
    public class MdiChildForm : Form
    {
        public MdiChildForm()
        {
            Activated += MDIChild_Activated;
            FormClosing += MDIChild_Closing;
            TextChanged += MdiChildForm_TextChanged;
        }

        public TabPage ChildTabPage { get; private set; }
        public TabControl MasterTabControl { get; private set; }

        private void MdiChildForm_TextChanged(object sender, EventArgs e)
        {
            if (ChildTabPage != null)
                ChildTabPage.Text = Text;
        }

        public void SetupAndShowMdiChildForm(string title, Form mdiParent, TabControl masterTabControl)
        {
            Text = title;

            SetupAndShowMdiChildForm(mdiParent, masterTabControl);
        }

        public void SetupAndShowMdiChildForm(Form mdiParent, TabControl masterTabControl)
        {
            MasterTabControl = masterTabControl;
            MdiParent = mdiParent;

            var newTab = new TabPage(Text);
            ChildTabPage = newTab;
            newTab.Parent = masterTabControl;

            newTab.Show();

            Show();

            if (!MaximizeBox)
                WindowState = FormWindowState.Normal;

            masterTabControl.SelectedTab = newTab;
        }

        private void MDIChild_Activated(object sender, EventArgs e)
        {
            //Activate the corresponding Tabpage
            MasterTabControl.SelectedTab = ChildTabPage;

            if (!MasterTabControl.Visible)
            {
                MasterTabControl.Visible = true;
            }
        }

        private void MDIChild_Closing(object sender, CancelEventArgs e)
        {
            //Destroy the corresponding Tabpage when closing MDI child form
            ChildTabPage.Dispose();

            //If no Tabpage left
            if (!MasterTabControl.HasChildren)
            {
                MasterTabControl.Visible = false;
            }
        }
    }
}