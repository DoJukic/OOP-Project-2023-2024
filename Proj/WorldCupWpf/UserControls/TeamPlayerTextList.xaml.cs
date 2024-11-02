using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldCupLib;
using static SharedDataLib.SettingsProvider;

namespace WorldCupWpf.UserControls
{
    /// <summary>
    /// Interaction logic for TeamPlayerTextList.xaml
    /// </summary>
    public partial class TeamPlayerTextList : UserControl
    {
        bool textDisplaced = false;

        public TeamPlayerTextList()
        {
            InitializeComponent();

            LocDataBindable.GenerateBindingsForTarget(
                lblStartingEleven,
                Label.ContentProperty,
                WorldCupViewer.Localization.LocalizationOptions.Starting_Eleven,
                casing: CharacterCasing.Upper);
            LocDataBindable.GenerateBindingsForTarget(
                lblBench,
                Label.ContentProperty,
                WorldCupViewer.Localization.LocalizationOptions.Bench,
                casing: CharacterCasing.Upper);

            
        }

        public void ReverseDisplay()
        {
            mainGridScaleTransform.ScaleX = -1;
            lblBenchScaleTransform.ScaleX = -1;
            lblStartingElevenScaleTransform.ScaleX = -1;

            foreach (var child in spStartingEleven.Children)
                if (child is TeamPlayerTextDisplay tptd)
                    tptd.ReverseText();
            foreach (var child in spBenchLeft.Children)
                if (child is TeamPlayerTextDisplay tptd)
                    tptd.ReverseText();
            foreach (var child in spBenchRight.Children)
                if (child is TeamPlayerTextDisplay tptd)
                    tptd.ReverseText();
        }

        public void LoadTeam(CupMatch match, CupMatchTeamStatistics teamStatistics, TeamData teamData, System.Windows.Media.Color? color = null)
        {
            if (color != null)
                rectStartingEleven.Fill = rectBench.Fill = new SolidColorBrush((System.Windows.Media.Color)color);

            IEnumerable<CupPlayer> startingEleven = teamStatistics.StartingElevenPlayers;
            IEnumerable<CupPlayer> bench = teamStatistics.SubstitutePlayers;

            spStartingEleven.Children.Clear();

            foreach (var player in startingEleven)
            {
                TeamPlayerTextDisplay playerDisplay = new(player, match, teamData);
                playerDisplay.SetFontSize(16);
                spStartingEleven.Children.Add(playerDisplay);
            }

            spBenchLeft.Children.Clear();
            spBenchRight.Children.Clear();
            bool putInLeftSP = true;

            foreach (var player in bench)
            {
                TeamPlayerTextDisplay playerDisplay = new(player, match, teamData);
                playerDisplay.SetFontSize(16);

                if (putInLeftSP)
                    spBenchLeft.Children.Add(playerDisplay);
                else
                    spBenchRight.Children.Add(playerDisplay);

                putInLeftSP = !putInLeftSP;
            }
        }

        public void ResetText()
        {
            textDisplaced = false;

            lblBench.Visibility = Visibility.Visible;
            lblStartingEleven.Visibility = Visibility.Visible;
            lblBenchTranslateTransform.X = 0;
            lblStartingElevenTranslateTransform.X = 0;
        }
        public void HideText()
        {
            lblBench.Visibility = Visibility.Collapsed;
            lblStartingEleven.Visibility = Visibility.Collapsed;
        }
        public void DisplaceText()
        {
            textDisplaced = true;
            DoDisplaceText();
        }
        public void ReDisplaceText()
        {
            if (!textDisplaced)
                return;
            DoDisplaceText();
        }
        private void DoDisplaceText()
        {
            lblBenchTranslateTransform.X = lblBench.ActualWidth / 2;
            lblStartingElevenTranslateTransform.X = lblStartingEleven.ActualWidth / 2;
        }

        private void lblStartingEleven_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReDisplaceText();
        }
        private void lblBench_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReDisplaceText();
        }
    }
}
