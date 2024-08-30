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
using WorldCupViewer.UserControls;

namespace WorldCupViewer
{
    public partial class SelectExternalImageDialog : Form
    {
        public SelectExternalImageDialog()
        {
            InitializeComponent();

            foreach (var thing in Images.GetAllExternalImageNames())
            {
                if (thing != null)
                    flpMain.Controls.Add(new ExternalImageSelectionOption(thing));
            }

            bool b = true;
        }
    }
}
