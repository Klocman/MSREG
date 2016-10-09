using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Klocman.Extensions
{
    public static class FormsTools
    {
        #region Methods

        /// <summary>
        ///     Convert keycode of a pressed number key to the actual number
        /// </summary>
        public static int ConvertKeycodeToNumber(Keys keyVal)
        {
            var value = -1;
            if (keyVal >= Keys.D0 && keyVal <= Keys.D9)
            {
                value = keyVal - Keys.D0;
            }
            else if (keyVal >= Keys.NumPad0 && keyVal <= Keys.NumPad9)
            {
                value = keyVal - Keys.NumPad0;
            }
            return value;
        }

        public static void InvokeIfRequired(this Control obj, Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            if (obj == null)
                throw new ArgumentNullException("obj");

            if (obj.InvokeRequired)
            {
                obj.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static bool IsChildOf(this Control c, Control parent)
        {
            if (c == null)
                return false;
            return (c.Parent != null && c.Parent == parent) || (c.Parent != null ? c.Parent.IsChildOf(parent) : false);
        }

        public static void SafeInvoke(this Control obj, Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            if (obj == null)
                throw new ArgumentNullException("obj");

            if (!obj.IsDisposed)
                InvokeIfRequired(obj, action);
            /*
            if (obj.InvokeRequired)// && obj.Visible)
            {
                if(!obj.IsDisposed)
                    obj.Invoke(action);
            }
            else
            {
                action();
            }*/
        }

        /*
        public static void InvokeIfRequired<T>(this Control obj, Action<T> action, T arg)
        {
            if (action == null || obj == null)
                throw new ArgumentNullException();
            if (obj.InvokeRequired)
            {
                obj.Invoke(action, arg);
            }
            else
            {
                action(arg);
            }
        }

        public static void InvokeIfRequired<T1, T2>(this Control obj, Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action == null || obj == null)
                throw new ArgumentNullException();
            if (obj.InvokeRequired)
            {
                obj.Invoke(action, arg1, arg2);
            }
            else
            {
                action(arg1, arg2);
            }
        }*/

        public static void SetDoubleBuffered(Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (SystemInformation.TerminalServerSession)
                return;

            var aProp =
                typeof (Control).GetProperty(
                    "DoubleBuffered",
                    BindingFlags.NonPublic |
                    BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        #endregion Methods
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }

    public static class CheckStateTools
    {
        #region Methods

        public static bool ToBool(this CheckState value)
        {
            return value == CheckState.Checked;
        }

        public static CheckState ToCheckState(this bool value)
        {
            return value ? CheckState.Checked : CheckState.Unchecked;
        }

        #endregion Methods
    }
}