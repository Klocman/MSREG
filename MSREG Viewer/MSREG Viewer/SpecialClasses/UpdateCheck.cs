using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Klocman.Subsystems;

namespace MSREG.Viewer.SpecialClasses
{
    public static class UpdateChecker
    {
        public static readonly Uri UpdateFeedUrl =
            new Uri(@"https://dl.dropboxusercontent.com/u/21871088/Update/MSREG_Viewer.xml");

        /*public struct DeviceExtras {
            public MSREG.Viewer.MsregDeviceType Device;
            public string DatasheetUrl;
        public static DeviceExtras*/

        public static Uri Msr33R7DatasheetUrl { get; private set; }

        public static void CheckForUpdatesInBackground()
        {
            Task.Factory.StartNew(CheckForUpdates);
        }

        public static void CheckForUpdates()
        {
            try
            {
                var client = new WebClient();
                var result = client.DownloadString(UpdateFeedUrl);
                var xmlResult = XDocument.Parse(result);
                var updateInfo = xmlResult.Element("Root").Element("Update");
                var newVersion = new Version(updateInfo.Element("Version").Value);

                if (newVersion.CompareTo(Assembly.GetExecutingAssembly().GetName().Version) > 0)
                {
                    AppLog.Write(string.Format("Dostępna jest nowa wersja programu: {0}", newVersion), LogEntryType.Info,
                        LogEntrySource.UpdateSystem);

                    // New version available
                    if (
                        MessageBox.Show(
                            string.Format("Dostępna jest nowa wersja programu: {0}\nCzy pobrać ją teraz?", newVersion),
                            "Nowa wersja programu", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                        DialogResult.Yes)
                    {
                        // OK to update
                        Process.Start(updateInfo.Element("URL").Value);
                    }
                }
                else
                {
                    AppLog.Write("Brak dostępnych aktualizacji", LogEntryType.Info, LogEntrySource.UpdateSystem);
                }

                Msr33R7DatasheetUrl = new Uri(xmlResult.Element("Root").Element("Datasheet").Element("MSR33E").Value);
            }
            catch (Exception e)
            {
                AppLog.Write(string.Format("Błąd podczas aktualizacji: {0}", e.Message), LogEntryType.Error,
                    LogEntrySource.UpdateSystem);
            }
        }
    }
}