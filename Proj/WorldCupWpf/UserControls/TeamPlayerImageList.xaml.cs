using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using WorldCupLib;
using static SharedDataLib.SettingsProvider;

namespace WorldCupWpf.UserControls
{
    /// <summary>
    /// Interaction logic for TeamPlayerImageList.xaml
    /// </summary>
    public partial class TeamPlayerImageList : UserControl
    {
        public TeamPlayerImageList()
        {
            InitializeComponent();
        }

        public void LoadFromData(CupMatchTeamStatistics teamStatistics, CupMatch cupMatch, CupMatchTeamInfo teamInfo, TeamData teamData, bool isMirrored = false)
        {
            grGoalkeeper.ColumnDefinitions.Clear();
            grDefence.ColumnDefinitions.Clear();
            grMidfield.ColumnDefinitions.Clear();
            grAttack.ColumnDefinitions.Clear();

            grGoalkeeper.Children.Clear();
            grDefence.Children.Clear();
            grMidfield.Children.Clear();
            grAttack.Children.Clear();

            List<TeamPlayerImageDisplay> goalkeep = new();
            List<TeamPlayerImageDisplay> defence = new();
            List<TeamPlayerImageDisplay> midfield = new();
            List<TeamPlayerImageDisplay> attack = new();

            if (isMirrored)
                userControlScaleTransform.ScaleY = -1;
            else
                userControlScaleTransform.ScaleY = 1;

            foreach (var playerPositionPair in teamInfo.playerPositionPairList)
            {
                if (teamStatistics.StartingElevenPlayers.Contains(playerPositionPair.Key))
                {
                    TeamPlayerImageDisplay playerImage = new(playerPositionPair.Key, teamData, cupMatch);
                    if (isMirrored)
                        playerImage.ReverseImageVertically();

                    if (CupMatchTeamInfo.CheckPositionString(playerPositionPair.Value, CupMatchTeamInfo.SupportedCupPlayerPositions.Goalkeeper))
                        goalkeep.Add(playerImage);
                    if (CupMatchTeamInfo.CheckPositionString(playerPositionPair.Value, CupMatchTeamInfo.SupportedCupPlayerPositions.Defence))
                        defence.Add(playerImage);
                    if (CupMatchTeamInfo.CheckPositionString(playerPositionPair.Value, CupMatchTeamInfo.SupportedCupPlayerPositions.Midfield))
                        midfield.Add(playerImage);
                    if (CupMatchTeamInfo.CheckPositionString(playerPositionPair.Value, CupMatchTeamInfo.SupportedCupPlayerPositions.Forward))
                        attack.Add(playerImage);
                }
            }

            // Make things tighter in midfield and attack, so things dont look so samey
            grAttack.ColumnDefinitions.Add(new());

            AddImagesToGridWithEqSpacing(goalkeep, grGoalkeeper);
            AddImagesToGridWithEqSpacing(defence, grDefence);
            AddImagesToGridWithEqSpacing(midfield, grMidfield);
            AddImagesToGridWithEqSpacing(attack, grAttack);

            grAttack.ColumnDefinitions.Add(new());
            return;
            // insert seems to just break everything???
            grAttack.ColumnDefinitions.Insert(0, new());
            grAttack.ColumnDefinitions.Insert(0, new());
            grAttack.ColumnDefinitions.Add(new());
            grAttack.ColumnDefinitions.Add(new());
        }

        private void AddImagesToGridWithEqSpacing(List<TeamPlayerImageDisplay> elements, Grid targetGrid)
        {
            targetGrid.ColumnDefinitions.Add(new());

            for (int i = 0; i < elements.Count; i++)
            {
                ColumnDefinition column = new();
                column.Width = new GridLength(1, GridUnitType.Auto);

                targetGrid.ColumnDefinitions.Add(column);
                targetGrid.Children.Add(elements[i]);
                elements[i].SetValue(Grid.ColumnProperty, targetGrid.ColumnDefinitions.Count - 1);

                targetGrid.ColumnDefinitions.Add(new());
            }
        }

        public string GetPlayerDistributionString()
        {
            const char SEP = '-';
            return CountPlayersInGridChildren(grGoalkeeper).ToString() + SEP +
                CountPlayersInGridChildren(grDefence).ToString() + SEP +
                CountPlayersInGridChildren(grMidfield).ToString() + SEP +
                CountPlayersInGridChildren(grAttack).ToString();
        }

        private int CountPlayersInGridChildren(Grid grid)
        {
            int result = 0;

            foreach (var thing in grid.Children)
                if (thing is TeamPlayerImageDisplay)
                    ++result;

            return result;
        }
    }
}
