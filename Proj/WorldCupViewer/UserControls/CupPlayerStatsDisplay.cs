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
using WorldCupViewer.Selectables;

namespace WorldCupViewer.UserControls
{
    public partial class CupPlayerStatsDisplay : UserControl
    {

        public CupPlayerStatsDisplay(CupPlayer player, String row1Text, String row2Text)
        {
            lblName.Text = player.name;
            lblRow1.Text = row1Text;
            lblRow2.Text = row2Text;
        }
    }
}
