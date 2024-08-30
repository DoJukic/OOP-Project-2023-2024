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
    public partial class YesNoDialog : Form
    {
        public bool response = false;
        public YesNoDialog(String caption, String text)
        {
            InitializeComponent();
            LocalizationHandler.LocalizeAllChildren(this);

            this.Text = caption;
            textLabel.Text = text;
        }

        public static bool ShowNew(String caption, String text)
        {
            YesNoDialog dialog = new(caption, text);

            dialog.ShowDialog();

            return dialog.response;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            response = true;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Key preview must be on for this to work properly
        private void YesNoDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'y':
                case (char)Keys.Enter:
                    btnYes_Click(this, new());
                    break;
                case 'n':
                case (char)Keys.Escape:
                    btnNo_Click(this, new());
                    break;
            }
        }
    }
}