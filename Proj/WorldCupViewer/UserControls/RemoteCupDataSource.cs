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

namespace WorldCupViewer.UserControls
{
    public partial class RemoteCupDataSource : UserControl
    {
        public AvailableRemoteSourceDetails deets;

        public RemoteCupDataSource()
        {
            deets = new("WHY_THO", 0, "If you see this, something has gone horribly wrong.", "");
            InitializeComponent();
        }

        public RemoteCupDataSource(AvailableRemoteSourceDetails details)
        {
            InitializeComponent();

            deets = details;

            mainPictureBox.Image = Image.FromStream(SharedDataLib.Images.GetInternalImageStream(deets.internalImageID)
                ?? SharedDataLib.Images.GetImgNotFoundPngStream());

            titleLabel.Text = deets.name + " (" + deets.year + ")";

        }

        internal void setAPIState(bool APIState)
        {
            var parentForm = this.FindForm();
            if (parentForm == null)
                return;

            if (APIState)
            {
                downloadButton.Visible = true;
                cancelDownloadButton.Visible = false;
                if (parentForm.ActiveControl == cancelDownloadButton)
                    parentForm.ActiveControl = downloadButton;
            }
            else
            {
                cancelDownloadButton.Visible = true;
                downloadButton.Visible = false;
                if (parentForm.ActiveControl == downloadButton)
                    parentForm.ActiveControl = cancelDownloadButton;
            }
        }

        private void downloadButton_Click(object? sender, EventArgs e)
        {
            deets.TryBeginDownload();
        }

        private void cancelDownloadButton_Click(object sender, EventArgs e)
        {
            WorldCupRepoBroker.BeginCancelAPIRequest();
        }
    }
}
