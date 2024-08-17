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

namespace WorldCupViewer.UserControls
{
    public partial class LocalCupDataSource : UserControl
    {
        private readonly AvailableFileDetails associatedDeets;
        public event Action<Task<IWorldCupDataRepo?>, String, AvailableFileDetails>? OnLoadButtonPressed;

        public LocalCupDataSource()
        {
            associatedDeets = new("WHY_THO", Guid.NewGuid(), "If you see this, something has gone horribly wrong.", 0, null, null);
            InitializeComponent();
        }

        public LocalCupDataSource(AvailableFileDetails deets)
        {
            InitializeComponent();

            associatedDeets = deets;

            mainPictureBox.Image = Image.FromStream(SharedDataLib.Images.GetInternalImageStream_DO_NOT_DISPOSE_OR_WRITE(deets.InternalImageID)
                ?? SharedDataLib.Images.GetImgNotFoundPngStream_DO_NOT_DISPOSE_OR_WRITE());

            titleLabel.Text = "[" + deets.ID + "] " + deets.Name + " (" + deets.Year + ")";

            if (deets.InfoFileValid)
                SetLabelOK(infoStatusLabel);
            else
                SetLabelERROR(infoStatusLabel);

            if (deets.FileStructureValid != null)
            {
                if ((bool)deets.FileStructureValid)
                    SetLabelOK(structureStatusLabel);
                else
                    SetLabelERROR(structureStatusLabel);
            }
            else
                SetLabelWAIT(structureStatusLabel);

            if (deets.JsonValid != null)
            {
                if ((bool)deets.JsonValid)
                    SetLabelOK(dataStatusLabel);
                else
                    SetLabelERROR(dataStatusLabel);
            }
            else
                SetLabelWAIT(dataStatusLabel);

            loadButton.Click += loadButton_Click;
            DeleteButton.Click += DeleteButton_Click;
        }

        private void SetLabelOK(MultilingualLabel label)
        {
            label.Localization = LocalizationOptions.Okay;
            label.ForeColor = Color.Green;
        }
        private void SetLabelWAIT(MultilingualLabel label)
        {
            label.Localization = LocalizationOptions.Wait;
            label.ForeColor = Color.DarkOrange;
        }
        private void SetLabelERROR(MultilingualLabel label)
        {   
            label.Localization = LocalizationOptions.Error;
            label.ForeColor = Color.Red;
        }

        private void loadButton_Click(object? sender, EventArgs e)
        {
            var parentForm = this.FindForm();
            if (parentForm != null)
                parentForm.ActiveControl = null;

            loadButton.Enabled = false;
            var targetTask = associatedDeets.BeginGetAssociatedDataRepo();
            OnLoadButtonPressed?.Invoke(targetTask, associatedDeets.GUID, associatedDeets);

            Task.Run(() =>
            {
                targetTask.Wait();
                this.Invoke(() =>
                {
                    loadButton.Enabled = true;
                });
            });
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            associatedDeets.TryDelete();
        }
    }
}
