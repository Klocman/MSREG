using System;
using System.IO;
using System.Windows.Forms;
using MSREG.Viewer.CustomControls;

namespace MSREG.Viewer.Windows.MdiChildWindows
{
    public partial class HelpWindow : MdiChildForm
    {
        public HelpWindow()
        {
            InitializeComponent();

            var appdir = Path.GetDirectoryName(Application.ExecutablePath);
            var myfile = Path.Combine(appdir, "Pomoc.html");
            webBrowser1.Url = new Uri("file:///" + myfile);
        }
    }
}