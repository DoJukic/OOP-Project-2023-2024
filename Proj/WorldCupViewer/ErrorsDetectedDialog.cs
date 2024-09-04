using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupViewer.Localization;

namespace WorldCupViewer
{
    public partial class ErrorsDetectedDialog : Form
    {
        public ErrorsDetectedDialog()
        {
            InitializeComponent();
        }

        public static void ShowNew(List<String> errors)
        {
            ErrorsDetectedDialog dialog = new();

            dialog.Text = LocalizationHandler.GetCurrentLocOptionsString(LocalizationOptions.Errors_Detected);
            dialog.lblMain.Text = LocalizationHandler.GetCurrentLocOptionsString(LocalizationOptions.The_Following_Errors_Have_Been_Detected_While_Loading_Data) + ":";

            foreach (var thing in errors)
            {
                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Height = 40;
                lbl.Text = thing;
                lbl.TextAlign = ContentAlignment.MiddleCenter;

                dialog.pnlErrorList.Controls.Add(lbl);
                lbl.Dock = DockStyle.Top;
            }

            using (dialog)
                dialog.ShowDialog();

        }

        private void ErrorsDetectedDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                case (char)Keys.Escape:
                case (char)Keys.Space:
                    this.Close();
                    break;
            }

            e.Handled = true;
        }
    }
}
