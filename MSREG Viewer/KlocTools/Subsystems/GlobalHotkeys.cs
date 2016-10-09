using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Klocman.Extensions;

namespace Klocman.Subsystems
{
    public sealed class GlobalHotkeys : IDisposable, ICollection<HotkeyEntry>
    {
        #region Constructors

        public GlobalHotkeys(Form parentForm)
        {
            ParentForm = parentForm;
        }

        #endregion Constructors

        #region Fields

        private readonly List<HotkeyEntry> _registeredHotkeys = new List<HotkeyEntry>();
        private Form _parentForm;

        #endregion Fields

        #region Properties

        public int Count
        {
            get { return _registeredHotkeys.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public Form ParentForm
        {
            get { return _parentForm; }
            set
            {
                if (_parentForm != null)
                {
                    _parentForm.KeyPreview = false;
                    _parentForm.KeyDown -= KeyDown_Handler;
                }

                if (value != null)
                {
                    value.KeyPreview = true;
                    value.KeyDown += KeyDown_Handler;
                }

                _parentForm = value;
            }
        }

        public bool SuppressKeyPresses { get; set; } = true;

        #endregion Properties

        #region Methods

        public void Add(HotkeyEntry item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (item.Master != null)
                item.Master.ShortcutKeyDisplayString = item.ToString();

            _registeredHotkeys.Add(item);
        }

        public void Clear()
        {
            _registeredHotkeys.ForEach(x => x.Dispose());
            _registeredHotkeys.Clear();
        }

        public bool Contains(HotkeyEntry item)
        {
            return _registeredHotkeys.Contains(item);
        }

        public void CopyTo(HotkeyEntry[] array, int arrayIndex)
        {
            throw new InvalidOperationException("Please don't do that");
            //RegisteredHotkeys.CopyTo(array, arrayIndex);
        }

        public void Dispose()
        {
            ParentForm = null;
            Clear();
        }

        public IEnumerator<HotkeyEntry> GetEnumerator()
        {
            return _registeredHotkeys.GetEnumerator();
        }

        public bool Remove(HotkeyEntry item)
        {
            if (item == null)
                return false;
            item.Dispose();
            return _registeredHotkeys.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _registeredHotkeys.GetEnumerator();
        }

        private void KeyDown_Handler(object sender, KeyEventArgs e)
        {
            if (_parentForm == null)
                throw new InvalidOperationException("Hotkey handler called when parent form was null");

            foreach (var hotkey in _registeredHotkeys)
            {
                if (hotkey.Alt == e.Alt && hotkey.Ctrl == e.Control && hotkey.Shift == e.Shift &&
                    hotkey.KeyCode == e.KeyCode)
                {
                    if (hotkey.EventHandler == null || !hotkey.IsEnabled)
                        continue;
                    hotkey.EventHandler(_parentForm, new EventArgs());
                    e.SuppressKeyPress = SuppressKeyPresses;
                        // Stops default windows "wtfding" sound and prevents event bubbling
                    return;
                }
            }
        }

        #endregion Methods
    }

    /// <summary>
    ///     Represents a single hotkey. It gets disposed when removed from main hotkey collection, so don't use it outside
    /// </summary>
    public sealed class HotkeyEntry : IDisposable
    {
        #region Fields

        private readonly Func<bool> _isEnabled;

        #endregion Fields

        #region Constructors

        public HotkeyEntry(Keys key, Action<object, EventArgs> eventHandlerDelegate, ToolStripMenuItem masterControl)
        {
            KeyCode = key;
            EventHandler = eventHandlerDelegate;
            Master = masterControl;
        }

        public HotkeyEntry(Keys key, Action<object, EventArgs> eventHandlerDelegate, ToolStripMenuItem masterControl,
            Func<bool> isEnabledDelegate)
            : this(key, eventHandlerDelegate, masterControl)
        {
            _isEnabled = isEnabledDelegate;
        }

        public HotkeyEntry(Keys key, bool altPressed, bool ctrlPressed, bool shiftPressed,
            Action<object, EventArgs> eventHandlerDelegate, ToolStripMenuItem masterControl)
            : this(key, eventHandlerDelegate, masterControl)
        {
            Alt = altPressed;
            Ctrl = ctrlPressed;
            Shift = shiftPressed;
        }

        public HotkeyEntry(Keys key, bool altPressed, bool ctrlPressed, bool shiftPressed,
            Action<object, EventArgs> eventHandlerDelegate, ToolStripMenuItem masterControl,
            Func<bool> isEnabledDelegate)
            : this(key, altPressed, ctrlPressed, shiftPressed, eventHandlerDelegate, masterControl)
        {
            _isEnabled = isEnabledDelegate;
        }

        #endregion Constructors

        #region Properties

        public bool Alt { get; }

        public bool Ctrl { get; }

        public bool IsEnabled
        {
            get
            {
                if (_isEnabled != null)
                    return _isEnabled();
                return true;
            }
        }

        public Action<object, EventArgs> EventHandler { get; private set; }

        public Keys KeyCode { get; }

        public ToolStripMenuItem Master { get; private set; }

        public bool Shift { get; }

        #endregion Properties

        #region Methods

        public void Dispose()
        {
            if (Master != null)
            {
                Master.ShortcutKeyDisplayString = string.Empty;
                Master = null;
            }
            EventHandler = null;
        }

        public override string ToString()
        {
            return (Ctrl ? "Ctrl+" : string.Empty).AppendIf(Shift, "Shift+").AppendIf(Alt, "Alt+") + KeyCode;
        }

        #endregion Methods
    }
}