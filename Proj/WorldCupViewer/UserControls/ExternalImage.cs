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
    public partial class ExternalImage : PictureBox, IExternalImage
    {
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

            var imgPath = Images.GetExternalImagePath(ExternalImageID);
            if (imgPath == null)
            {
                var stream = new MemoryStream(Images.GetNoDataPngBytes());
                Image = Image.FromStream(stream);
            }

            this.ImageLocation = imgPath;
            return;
        }
    }
}
