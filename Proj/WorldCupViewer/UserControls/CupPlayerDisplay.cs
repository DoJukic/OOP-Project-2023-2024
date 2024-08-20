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
    public partial class CupPlayerDisplay : UserControl
    {
        public CupPlayerDisplay(CupPlayer player, int startingWidth, bool isFavourite)
        {
            InitializeComponent();

            this.Width = startingWidth;

            lblName.Text = player.name;
            lblShirtNumber.Text = player.shirtNumber.ToString();

            int gamesCaptained = 0;

            foreach (var match in player.SortedMatches)
            {
                if (match.awayTeamStatistics.StartingElevenPlayers.Union(match.awayTeamStatistics.SubstitutePlayers).Contains(player) &&
                    match.awayTeam.captainList.Contains(player))
                {
                    gamesCaptained += 1;
                }
                if (match.homeTeamStatistics.StartingElevenPlayers.Union(match.homeTeamStatistics.SubstitutePlayers).Contains(player) &&
                    match.homeTeam.captainList.Contains(player))
                {
                    gamesCaptained += 1;
                }
            }

            lblGamesCaptained.Text = gamesCaptained.ToString();
        }
    }
}
