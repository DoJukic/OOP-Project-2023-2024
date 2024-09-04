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
using WorldCupViewer.Selectables;

namespace WorldCupViewer.UserControls
{
    public partial class CupPlayerStatsDisplay : UserControl, IPlayerImageHolder, IFavouriteablePlayerHolder
    {
        CupPlayer associatedPlayer;

        public CupPlayerStatsDisplay(CupPlayer player, String Text)
        {
            InitializeComponent();

            associatedPlayer = player;

            lblName.Text = Text;

            SetIsFavourite(false);
            pbIsFavourite.Image = Image.FromStream(Images.GetStarPngStream());
        }

        public long GetPlayerNumber()
        {
            return associatedPlayer.shirtNumber;
        }

        public string GetPlayerImageID()
        {
            return epbProfilePicture.ExternalImageID ?? "";
        }

        public void SetPlayerImageID(string externalImageID)
        {
            epbProfilePicture.ExternalImageID = externalImageID;
        }

        private void changeImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectExternalImageDialog dialog = new(associatedPlayer, epbProfilePicture.ExternalImageID ?? "");

            using (dialog)
            {
                dialog.ShowDialog();
            }
        }

        public CupPlayer GetAssociatedPlayer()
        {
            return this.associatedPlayer;
        }

        public void SetIsFavourite(bool isFavourite)
        {
            pbIsFavourite.Visible = isFavourite;
        }
    }
}
