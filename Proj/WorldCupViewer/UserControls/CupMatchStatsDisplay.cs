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
    public partial class CupMatchStatsDisplay : UserControl
    {
        public CupMatchStatsDisplay()
        {
            InitializeComponent();
        }

        public CupMatchStatsDisplay(String matchLocation, String homeTeamText, String attendaceText, String awayTeamText) : this()
        {
            lblMatchLocation.Text = matchLocation;
            lblHomeTeam.Text = homeTeamText;
            lblAttendance.Text = attendaceText;
            lblAwayTeam.Text = awayTeamText;
        }
    }
}
