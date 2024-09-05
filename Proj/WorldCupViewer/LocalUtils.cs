using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupViewer
{
    internal static class LocalUtils
    {
        //https://stackoverflow.com/questions/9616617/c-sharp-copy-paste-an-image-region-into-another-image
        public static void CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, Bitmap destBitmap, Rectangle destRegion)
        {
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }

        public static void CenterFormToParent(Form parent, Form child)
        {
            int differenceX = (parent.Width - child.Width) / 2;
            int differenceY = (parent.Height - child.Height) / 2;

            child.DesktopLocation = new Point(parent.DesktopLocation.X + differenceX, parent.DesktopLocation.Y + differenceY);
        }

        // https://stackoverflow.com/questions/653284/get-available-controls-from-a-form - ProfK's answer, slightly modified.
        public static IEnumerable<Control> GetAllControls(Control control)
        {
            var controlList = new List<Control>();

            foreach (Control childControl in control.Controls)
            {
                // Recurse child controls.
                controlList.Add(childControl);
                controlList.AddRange(GetAllControls(childControl));
            }

            return controlList;
        }

        // https://stackoverflow.com/questions/372974/winforms-how-to-programmatically-fire-an-event-handler
        // Turns out I didn't need this, the event wasn't firing because the handlers werent created yet, which is just insane...
        /// <summary>
        /// Programatically fire an event handler of an object
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="eventName"></param>
        /// <param name="e"></param>
        public static void FireEvent(Object targetObject, string eventName, EventArgs e)
        {
            /*
             * By convention event handlers are internally called by a protected
             * method called OnEventName
             * e.g.
             *     public event TextChanged
             * is triggered by
             *     protected void OnTextChanged
             * 
             * If the object didn't create an OnXxxx protected method,
             * then you're screwed. But your alternative was over override
             * the method and call it - so you'd be screwed the other way too.
             */

            //Event thrower method name //e.g. OnTextChanged
            String methodName = "On" + eventName;

            MethodInfo? mi = targetObject.GetType().GetMethod(
                  methodName,
                  BindingFlags.Instance | BindingFlags.NonPublic);

            if (mi == null)
                throw new ArgumentException("Cannot find event thrower named " + methodName);

            mi.Invoke(targetObject, new object[] { e });
        }

        // Bruh
        public static void ClearControlsWithDispose(Control control)
        {
            control.SuspendLayout();

            foreach (var thing in control.Controls)
                if (thing is Control crtl)
                    crtl.Dispose();

            control.Controls.Clear();

            control.ResumeLayout();
        }
    }
}
