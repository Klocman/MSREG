using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MSREG.FirmwareUpdater
{
    internal static class Program
    {
        public static IEnumerable<string> CommandLineArgs { get; private set; }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            CommandLineArgs = args;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}