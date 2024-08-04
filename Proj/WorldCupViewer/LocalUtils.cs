using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupViewer
{
    internal static class LocalUtils
    {
        // https://stackoverflow.com/questions/653284/get-available-controls-from-a-form - ProfK's answer, slightly modified.
        public static IEnumerable<Control> GetAllControls(Control form)
        {
            var controlList = new List<Control>();

            foreach (Control childControl in form.Controls)
            {
                // Recurse child controls.
                controlList.Add(childControl);
                controlList.AddRange(GetAllControls(childControl));
            }
            return controlList;
        }
    }
}
