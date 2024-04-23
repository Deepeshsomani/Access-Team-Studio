using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessTeamStudio.DelegatesHelpers
{
    public class CommonDelegates
    {
        public static DialogResult DisplayMessageBox(Form form, string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            DialogResult result = DialogResult.Ignore;

            MethodInvoker DisplayMessage = delegate
            {
                result = MessageBox.Show(form, message, caption, buttons, icon);
            };

            if (form.InvokeRequired)
            {
                form.Invoke(DisplayMessage);
                return result;
            }
            else
            {
                DisplayMessage();
                return result;
            }
        }

        public static void SetCursor(Control control, Cursor cursor)
        {
            MethodInvoker miSetCursor = delegate
            {
                control.Cursor = cursor;
            };

            if (control.InvokeRequired)
            {
                control.Invoke(miSetCursor);
            }
            else
            {
                miSetCursor();
            }
        }

        public static void SetFocus(Control control)
        {
            MethodInvoker miSetFocus = delegate
            {
                control.Focus();
            };

            if (control.InvokeRequired)
            {
                control.Invoke(miSetFocus);
            }
            else
            {
                miSetFocus();
            }
        }

        internal static void SetEnableState(Control control, bool enabled)
        {
            MethodInvoker miSetEnableState = delegate
            {
                control.Enabled = enabled;
            };

            if (control.InvokeRequired)
            {
                control.Invoke(miSetEnableState);
            }
            else
            {
                miSetEnableState();
            }
        }
    }
}