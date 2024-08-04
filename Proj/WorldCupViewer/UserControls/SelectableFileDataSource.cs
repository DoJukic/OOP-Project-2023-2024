using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldCupViewer.UserControls
{
    public partial class SelectableFileDataSource : UserControl
    {
        public int MyProperty { get; set; }

        public SelectableFileDataSource()
        {
            InitializeComponent();
        }
    }
}
