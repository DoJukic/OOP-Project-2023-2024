using SharedDataLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupLib;
using WorldCupViewer.Localization;
using WorldCupViewer.PlayerImages;
using WorldCupViewer.UserControls;

namespace WorldCupViewer
{
    public partial class SelectExternalImageDialog : Form
    {
        CupPlayer associatedPlayer;
        String startingSelectedImageID;

        private static SelectExternalImageDialog? lastActiveDialog = null;

        public SelectExternalImageDialog()
        {
            InitializeComponent();
        }

        public SelectExternalImageDialog(CupPlayer player, String currentSelectedImageID)
        {
            InitializeComponent();

            this.Text = LocalizationHandler.GetCurrentLocOptionsString(LocalizationOptions.Choose_An_Image);

            associatedPlayer = player;
            startingSelectedImageID = currentSelectedImageID;
        }

        private void SelectExternalImageDialog_Load(object sender, EventArgs e)
        {
            lastActiveDialog = this;
            LoadImages();
        }

        public static void LoadImages()
        {
            if (lastActiveDialog == null)
                return;

            LocalUtils.ClearControlsWithDispose(lastActiveDialog.flpMain);

            lastActiveDialog.flpMain.Visible = false;
            lastActiveDialog.flpMain.SuspendLayout();

            ExternalImageSelectionOption noneOption = new("NOPLS");
            lastActiveDialog.flpMain.Controls.Add(noneOption);
            noneOption.Selected();
            lastActiveDialog.ActiveControl = noneOption; // Focus() no work before show :(
            noneOption.OnSelected += new(() => lastActiveDialog.OptionSelected(""));

            foreach (var externalImageID in Images.GetAllExternalImageNames())
            {
                if (externalImageID != null)
                {
                    ExternalImageSelectionOption option = new(externalImageID);
                    lastActiveDialog.flpMain.Controls.Add(option);

                    if (externalImageID == lastActiveDialog.startingSelectedImageID)
                    {
                        noneOption.Deselected();
                        option.Selected();
                        lastActiveDialog.ActiveControl = option;
                    }

                    option.OnSelected += new(() => lastActiveDialog.OptionSelected(externalImageID));
                }
            }

            lastActiveDialog.flpMain.ResumeLayout();
            lastActiveDialog.flpMain.Visible = true;
        }

        private void OptionSelected(String externalImageID)
        {
            foreach (var control in LocalUtils.GetAllControls(this))
            {
                if (control is not ExternalImageSelectionOption option)
                    continue;

                if (option.GetExternalImageID() != externalImageID)
                {
                    option.Deselected();
                }
            }

            PlayerImageLinkIntermediary.CreateLink(associatedPlayer.shirtNumber, externalImageID);
        }

        private void SelectExternalImageDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Escape:
                    this.Close();
                    break;
            }

            e.Handled = true;
        }

        private void SelectExternalImageDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (lastActiveDialog == this)
            {
                lastActiveDialog = null;
            }
        }
    }
}
