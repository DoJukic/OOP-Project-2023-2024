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
        public event Action<Task<bool>>? OnDownloadButtonPressed;

        public RemoteCupDataSource()
        {
            deets = new("WHY_THO", 0, "If you see this, something has gone horribly wrong.", "");
            InitializeComponent();
        }

        public RemoteCupDataSource(AvailableRemoteSourceDetails details)
        {
            InitializeComponent();

            deets = details;

            mainPictureBox.Image = Image.FromStream(SharedDataLib.Images.getInternalImageStream_DO_NOT_DISPOSE_OR_WRITE(deets.internalImageID)
                ?? SharedDataLib.Images.GetImgNotFoundPngStream_DO_NOT_DISPOSE_OR_WRITE());

            titleLabel.Text = deets.name + " (" + deets.year + ")";

            downloadButton.Click += downloadButton_Click;
        }

        private void downloadButton_Click(object? sender, EventArgs e)
        {
            var parentForm = this.FindForm();
            if (parentForm != null)
                parentForm.ActiveControl = null;

            downloadButton.Enabled = false;
            var targetTask = deets.TryBeginDownload();
            OnDownloadButtonPressed?.Invoke(targetTask);

            Task.Run(() =>
            {
                targetTask.Wait();
                this.Invoke(() =>
                {
                    downloadButton.Enabled = true;
                });
            });
        }
    }
}
