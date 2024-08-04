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
    public partial class SelectableFileDataSource_v2 : UserControl
    {
        public SelectableFileDataSource_v2()
        {
            InitializeComponent();
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

        public void LoadAvailableFileDetails(AvailableFileDetails deets)
        {
            titleLabel.Text = deets.Name + " (" + deets.Year + ").";

            if (deets.InfoFileValid)
                SetLabelOK(infoStatusLabel);
            else
                SetLabelERROR(infoStatusLabel);

            if (deets.FileStructureValid != null)
            {
                if ((bool) deets.FileStructureValid)
                    SetLabelOK(structureStatusLabel);
                else
                    SetLabelERROR(structureStatusLabel);
            }
            else
                SetLabelWAIT(structureStatusLabel);

            if (deets.JsonValid != null)
            {
                if ((bool) deets.JsonValid)
                    SetLabelOK(dataStatusLabel);
                else
                    SetLabelERROR(dataStatusLabel);
            }
            else
                SetLabelWAIT(dataStatusLabel);
        }
    }
}
