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
using WorldCupViewer.ExternalImages;

namespace WorldCupViewer.UserControls
{
    public partial class ExternalImage : PictureBox, IExternalImage, IDisposable
    {
        private MemoryStream? imageDataStream;

        private String? externalImageID;
        public String? ExternalImageID
        {
            get { return externalImageID; }
            set { externalImageID = value; UpdateExternalImage(); }
        }

        public void UpdateExternalImage()
        {
            if (ExternalImageID == null)
                return;

            var imgBytes = Images.TryGetExternalImageBytes(ExternalImageID);
            if (imgBytes == null)
            {
                ImageLoadFailed();
                return;
            }

            try
            {
                imageDataStream?.Dispose();
                imageDataStream = null;

                imageDataStream = new(imgBytes);
                Image = Image.FromStream(imageDataStream);
            }
            catch (Exception)
            {
                ImageLoadFailed();
            }
        }

        public new void Dispose()
        {
            base.Dispose();

            imageDataStream?.Dispose();
            imageDataStream = null;
        }

        private void ImageLoadFailed()
        {
            var stream = new MemoryStream(Images.GetNoDataPngBytes());
            Image = Image.FromStream(stream);
        }
    }
}
