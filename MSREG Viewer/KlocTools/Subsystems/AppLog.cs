using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Klocman.Subsystems
{

    #region Enumerations

    public enum LogEntrySource
    {
        None,
        LogSystem,
        MsregDevice,
        TerminalWindow,
        UpdateSystem
    }

    public enum LogEntryType
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3
    }

    #endregion Enumerations

    public static class AppLog
    {
        #region Events

        public static event Action<LogEntry> EntryAdded;

        #endregion Events

        #region Fields

        private static int _maxLogSize = 1023;
        private static readonly List<LogEntry> LogEntryList = new List<LogEntry>(_maxLogSize + 1);

        private static readonly string AppLogHistoryTooSmall = "Zbyt mały rozmiar loga";

        private static readonly string AppLogCleared = "Log wyczyszczony";
        //static readonly string AppLogTrimmed = "Log przycięty";

        #endregion Fields

        #region Properties

        public static IEnumerable<LogEntry> LogEntries => LogEntryList;

        public static int MaxLogSize
        {
            get { return _maxLogSize; }
            set
            {
                if (value < 2)
                    throw new ArgumentOutOfRangeException("value", AppLogHistoryTooSmall);
                _maxLogSize = value;
                LogEntryList.Capacity = value + 1;

                Write(string.Format("Nowy maksymalny rozmiar logu: ({0})", value), LogEntryType.Debug,
                    LogEntrySource.LogSystem);
            }
        }

        #endregion Properties

        #region Methods

        public static void ClearLog()
        {
            lock (LogEntryList)
            {
                LogEntryList.Clear();
                Write(AppLogCleared, LogEntryType.Info, LogEntrySource.LogSystem);
            }
        }

        public static IEnumerable<LogEntry> GetLogEntries(LogEntryType filterLevel)
        {
            lock (LogEntryList)
            {
                return LogEntryList.Where(x => x.Type.CompareTo(filterLevel) >= 0).ToArray();
            }
        }

        public static Color GetLogEntryTypeColor(LogEntryType type)
        {
            switch (type)
            {
                case LogEntryType.Debug:
                    return Color.DarkCyan;

                case LogEntryType.Info:
                    return Color.Black;

                case LogEntryType.Warning:
                    return Color.DarkGoldenrod;

                case LogEntryType.Error:
                    return Color.Red;

                default:
                    return Color.Pink;
            }
        }

        public static void Write(string message, LogEntryType type, LogEntrySource source, string extraSourceInfo)
        {
            var newEntry = new LogEntry(message, type, source, extraSourceInfo);
            lock (LogEntryList)
            {
                LogEntryList.Add(newEntry);
                TrimLogList();
            }
            OnEntryAdded(newEntry);
        }

        public static void Write(string message, LogEntryType type, LogEntrySource source)
        {
            Write(message, type, source, string.Empty);
        }

        public static void Write(string message, LogEntryType type)
        {
            Write(message, type, LogEntrySource.None, string.Empty);
        }

        public static void Write(string message)
        {
            Write(message, LogEntryType.Info, LogEntrySource.None, string.Empty);
        }

        private static void OnEntryAdded(LogEntry entry)
        {
            EntryAdded?.Invoke(entry);
        }

        private static void TrimLogList()
        {
            lock (LogEntryList)
            {
                while (LogEntryList.Count > _maxLogSize)
                    LogEntryList.RemoveAt(0);

                // Old trimming code
                //var oldAmount = _LogEntries.Count;
                //_LogEntries.RemoveRange(0, _maxLogSize / 2);
                //Write(string.Format("{0} ({1}>{2})", MSREG_Viewer.Strings.Default.AppLogTrimmed, oldAmount, _LogEntries.Count), LogEntryType.Debug, LogEntrySource.LogSystem);
            }
        }

        #endregion Methods
    }

    public class LogEntry
    {
        #region Constructors

        public LogEntry(string message, LogEntryType type, LogEntrySource sourceName, string extraSourceInfo)
        {
            Message = message;
            SourceName = sourceName;
            ExtraSourceInfo = extraSourceInfo;
            Type = type;
            Date = DateTime.Now;
        }

        #endregion Constructors

        #region Properties

        public DateTime Date { get; }

        public string ExtraSourceInfo { get; }

        public string Message { get; }

        public LogEntrySource SourceName { get; }

        public LogEntryType Type { get; }

        #endregion Properties

        #region Methods

        public string ToLongString()
        {
            var sb = new StringBuilder();
            sb.Append(Date);
            sb.Append(" - ");
            sb.Append(Type.ToString().PadRight(8));

            string tempSourceName;
            if (string.IsNullOrEmpty(ExtraSourceInfo))
                tempSourceName = string.Empty;
            else
                tempSourceName = ":" + ExtraSourceInfo;

            sb.Append(string.Concat("(", SourceName.ToString(), tempSourceName, ")").PadRight(18));
            sb.Append(" - ");
            sb.Append(Message);

            return sb.ToString();
        }

        public string ToShortString()
        {
            return string.Format("({0}{1}) - {2}", SourceName,
                string.IsNullOrEmpty(ExtraSourceInfo) ? string.Empty : ":" + ExtraSourceInfo, Message);
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}{2}) - {3}", Type, SourceName,
                string.IsNullOrEmpty(ExtraSourceInfo) ? string.Empty : ":" + ExtraSourceInfo, Message);
        }

        #endregion Methods
    }
}