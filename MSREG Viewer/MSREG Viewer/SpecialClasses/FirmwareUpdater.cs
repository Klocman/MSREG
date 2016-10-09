using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MSREG.Viewer.SpecialClasses
{
    public static class FirmwareUpdater
    {
        private static readonly string ExecName = "MSREG Firmware Updater.exe";

        public static void OpenUpdater()
        {
            OpenUpdater(string.Empty);
        }

        public static void OpenUpdater(string targetPort)
        {
            var appdir = Path.GetDirectoryName(Application.ExecutablePath);
            var myfile = Path.Combine(appdir, ExecName);

            Process.Start(myfile, targetPort);
        }
    }
}