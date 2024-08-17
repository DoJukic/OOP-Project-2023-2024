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
    public partial class YesNoDontAskAgainDialog : Form
    {
        private static List<String> blacklisted = new();

        public Response response = new();
        public YesNoDontAskAgainDialog(String caption, String text)
        {
            InitializeComponent();
            LocalizationHandler.LocalizeAllChildren(this);

            this.Text = caption;
            textLabel.Text = text;
        }

        public static Response ShowNew(String ID, String caption, String text)
        {
            if (blacklisted.Exists((str) => str == ID))
                return new() { yes = true };

            YesNoDontAskAgainDialog dialog = new(caption, text);

            dialog.ShowDialog();

            if (dialog.response.dontAskAgain)
                blacklisted.Add(ID);

            return dialog.response;
        }

        public class Response
        {
            public bool yes = false;
            public bool dontAskAgain = false;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            response.yes = true;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnYesAndBuzzOff_Click(object sender, EventArgs e)
        {
            response.yes = true;
            response.dontAskAgain = true;
            Close();
        }

        // Key preview must be on for this to work properly
        private void YesNoDontAskAgainDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar){
                case 'y':
                case (char)Keys.Enter:
                    btnYes_Click(this, new());
                    break;
                case 'n':
                case (char)Keys.Escape:
                    btnNo_Click(this, new());
                    break;
                case 'd':
                    btnYesAndBuzzOff_Click(this, new());
                    break;
            }
        }
    }
}
