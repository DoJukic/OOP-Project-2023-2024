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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorldCupLib;
using WorldCupLib.Interface;
using WorldCupWpf.LocalUtils;
using WorldCupWpf.LocalUtils.LocalUtils;
using WorldCupWpf.Signals;

namespace WorldCupWpf.Dialog
{
    /// <summary>
    /// Interaction logic for TeamInfoWindow.xaml
    /// </summary>
    public partial class TeamInfoWindow : Window, ISignalReciever
    {
        public TeamInfoWindow()
        {
            InitializeComponent();

            SignalController.SubscribeToSignal(Utils.GetFullPauseSignal(), this);
            SignalController.SubscribeToSignal(Utils.GetFullResumeSignal(), this);
        }
        public TeamInfoWindow(CupTeam ct) : this()
        {
            int gamesPlayed = 0;
            int gamesWon = 0;
            int gamesLost = 0;
            int gamesDraw = 0;

            int goalsScored = 0;
            int goalsTaken = 0;

            foreach (var match in ct.SortedMatches)
            {
                gamesPlayed++;

                if (match.winningTeam == ct)
                    gamesWon++;
                else if (match.winningTeam == null)
                    gamesDraw++;
                else
                    gamesLost++;

                List<CupEvent> BLUFORCupTeamEvents;
                List<CupEvent> OPFORCupTeamEvents;

                if (match.homeTeam.team == ct)
                {
                    BLUFORCupTeamEvents = match.HomeTeamEvents;
                    OPFORCupTeamEvents = match.AwayTeamEvents;
                }
                else
                {
                    BLUFORCupTeamEvents = match.AwayTeamEvents;
                    OPFORCupTeamEvents = match.HomeTeamEvents;
                }

                foreach (var cupEvent in BLUFORCupTeamEvents)
                    if (CupEvent.CheckCupEvent(cupEvent, CupEvent.SupportedCupEventTypes.Goal))
                        goalsScored++;
                foreach (var cupEvent in OPFORCupTeamEvents)
                    if (CupEvent.CheckCupEvent(cupEvent, CupEvent.SupportedCupEventTypes.Goal))
                        goalsTaken++;
            }

            lblTeamName.Content = ct.countryName + " (" + ct.fifaCode + ")";
            lblGamesDrawData.Content = gamesDraw;
            lblGamesWonData.Content = gamesWon;
            lblGamesLostData.Content = gamesLost;
            lblGamesPlayedData.Content = gamesPlayed;

            lblGoalsScoredData.Content = goalsScored;
            lblGoalsTakenData.Content = goalsTaken;
            lblGoalsDifferentialData.Content = goalsScored - goalsTaken;
        }

        private void Window_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            LocDataBindable.GenerateBindingsForTarget(lblGames, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Games, ":");
            LocDataBindable.GenerateBindingsForTarget(lblGamesDraw, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Draws);
            LocDataBindable.GenerateBindingsForTarget(lblGamesLost, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Losses);
            LocDataBindable.GenerateBindingsForTarget(lblGamesPlayed, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Played);
            LocDataBindable.GenerateBindingsForTarget(lblGamesWon, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Wins);

            LocDataBindable.GenerateBindingsForTarget(lblGoals, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Goals, ":");
            LocDataBindable.GenerateBindingsForTarget(lblGoalsDifferential, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Differential);
            LocDataBindable.GenerateBindingsForTarget(lblGoalsScored, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Scored);
            LocDataBindable.GenerateBindingsForTarget(lblGoalsTaken, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Taken);
        }

        public void RecieveSignal(string signalSignature)
        {
            if (signalSignature == Utils.GetFullPauseSignal())
            {
                ShowLoadingScreen();
            }
            if (signalSignature == Utils.GetFullResumeSignal())
            {
                HideLoadingScreen();
            }
        }

        private void ShowLoadingScreen()
        {
            Panel.SetZIndex(LoadingOverlay, 0);
            AnimationHelper.FadeIn(LoadingOverlay, 250);
        }

        private void HideLoadingScreen()
        {
            AnimationHelper.FadeOut(LoadingOverlay, 250, () => { Panel.SetZIndex(LoadingOverlay, -1000000); });
        }
    }
}
