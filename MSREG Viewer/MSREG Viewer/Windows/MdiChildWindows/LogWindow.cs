using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Klocman.Extensions;
using Klocman.Subsystems;
using MSREG.Viewer.CustomControls;

namespace MSREG.Viewer.Windows.MdiChildWindows
{
    public partial class LogWindow : MdiChildForm
    {
        private LogEntryType _filterLevel = LogEntryType.Info;
        private LogEntrySource _filterSource = LogEntrySource.None;

        public LogWindow()
        {
            InitializeComponent();

            AppLog.EntryAdded += AppLog_EntryAdded;

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(Enum.GetNames(typeof (LogEntryType)));
            comboBox1.SelectedItem = comboBox1.Items[1];

            filterSourceComboBox.Items.Clear();
            filterSourceComboBox.Items.AddRange(Enum.GetNames(typeof (LogEntrySource)));
            filterSourceComboBox.SelectedItem = filterSourceComboBox.Items[0];

            ReloadLogList();
        }

        private void ReloadLogList()
        {
            logList.Clear();
            foreach (var entry in AppLog.GetLogEntries(_filterLevel))
            {
                AppLog_EntryAdded(entry);
            }
        }

        private void AppLog_EntryAdded(LogEntry obj)
        {
            try
            {
                if (obj.Type >= _filterLevel
                    && (_filterSource == LogEntrySource.None || obj.SourceName == _filterSource)
                    && !logList.IsDisposed) // Disposed check last to minimize race conditions
                {
                    logList.SafeInvoke(() =>
                    {
                        logList.SuspendLayout();
                        logList.AppendText("\n");
                        logList.AppendText(obj.Date.ToLongTimeString(), Color.DarkGray);
                        logList.AppendText(" - ");
                        logList.AppendText(obj.Type.ToString().PadRight(8, ' '), AppLog.GetLogEntryTypeColor(obj.Type));
                        //logList.AppendText("\t");

                        string tempSourceName;
                        if (string.IsNullOrEmpty(obj.ExtraSourceInfo))
                            tempSourceName = string.Empty;
                        else
                            tempSourceName = ":" + obj.ExtraSourceInfo;

                        logList.AppendText(
                            string.Concat("(", obj.SourceName.ToString(), tempSourceName, ")").PadRight(18, ' '));
                        logList.AppendText(" - ");
                        logList.AppendText(obj.Message);
                        logList.ResumeLayout();
                    });
                }
            }
            catch (Exception)
            {
                /*Eat shit and die*/
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            AppLog.ClearLog();
            ReloadLogList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                Enum.TryParse(comboBox1.SelectedItem as string, out _filterLevel);
                ReloadLogList();
            }
        }

        private void filterSourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterSourceComboBox.SelectedItem != null)
            {
                Enum.TryParse(filterSourceComboBox.SelectedItem as string, out _filterSource);
                ReloadLogList();
            }
        }

        private void logList_SelectionChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(logList.SelectedText))
            {
                Clipboard.SetText(logList.SelectedText);
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                //var result = string.Join(Environment.NewLine, ));
                File.WriteAllLines(saveFileDialog1.FileName,
                    AppLog.GetLogEntries(_filterLevel).Select(x => x.ToLongString()));
                AppLog.Write("Log zapisany do: " + saveFileDialog1.FileName, LogEntryType.Info, LogEntrySource.LogSystem);
            }
            catch (Exception ex)
            {
                AppLog.Write("Błąd zapisu logu: " + ex.Message, LogEntryType.Info, LogEntrySource.LogSystem);
            }
        }

        private void saveLogButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
    }
}