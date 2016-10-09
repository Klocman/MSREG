using System;
using System.Windows.Forms;
using Klocman.Subsystems;
using MSREG.Viewer.SpecialClasses;
using MSREG.Viewer.Windows;

namespace MSREG.Viewer
{
    internal static class Program
    {
        #region Methods

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppLog.Write("Start aplikacji");
            UpdateChecker.CheckForUpdatesInBackground();
            Application.Run(new MainWindow());
        }

        #endregion Methods
    }
}