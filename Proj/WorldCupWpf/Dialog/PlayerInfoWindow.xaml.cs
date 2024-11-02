using SharedDataLib;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WorldCupLib;
using WorldCupLib.Interface;
using WorldCupWpf.Signals;
using WorldCupWpf.UserControls;
using static SharedDataLib.SettingsProvider;

namespace WorldCupWpf.Dialog
{
    /// <summary>
    /// Interaction logic for PlayerInfoWindow.xaml
    /// </summary>
    public partial class PlayerInfoWindow : Window, ISignalReciever
    {
        CupPlayer player;
        TeamData teamData;

        private Action imgRefreshAction;

        public PlayerInfoWindow()
        {
            InitializeComponent();

            SignalController.SubscribeToSignal(LocalUtils.GetFullPauseSignal(), this);
            SignalController.SubscribeToSignal(LocalUtils.GetFullResumeSignal(), this);
            SignalController.SubscribeToSignal(LocalUtils.GetMatchChangedSignal(), this);

            LocDataBindable.GenerateBindingsForTarget(lblName, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Name, ":");
            LocDataBindable.GenerateBindingsForTarget(lblShirtNumber, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Shirt_Number, ":");
            LocDataBindable.GenerateBindingsForTarget(lblMatchInfo, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Match_Data, ":");
            LocDataBindable.GenerateBindingsForTarget(lblPosition, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Position, ":");
            LocDataBindable.GenerateBindingsForTarget(lblIsCaptain, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Captain, ":");
            LocDataBindable.GenerateBindingsForTarget(lblGoalsScored, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Goals_Scored, ":");
            LocDataBindable.GenerateBindingsForTarget(lblYellowCards, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Yellow_Cards, ":");
        }

        public PlayerInfoWindow(CupPlayer player, TeamData teamData, CupMatch match) : this()
        {
            this.player = player;
            this.teamData = teamData;

            WeakReference<PlayerInfoWindow> weakRef = new(this);
            imgRefreshAction = () => {
                if (Application.Current != null)
                    Application.Current.Dispatcher.Invoke(() => { PlayerInfoWindow.ImageRefreshNeeded(weakRef); });
            };
            Images.SubscribeToExternalImagesChanged(imgRefreshAction);

            lblNameData.Content = player.name;
            lblShirtNumberData.Content = player.shirtNumber;

            CupMatchTeamInfo BLUFORTeamInfo;
            List<CupEvent> BLUFORCupTeamEvents;

            if (match.homeTeam.team == player.team)
            {
                BLUFORTeamInfo = match.homeTeam;
                BLUFORCupTeamEvents = match.HomeTeamEvents;
            }
            else
            {
                BLUFORTeamInfo = match.awayTeam;
                BLUFORCupTeamEvents = match.AwayTeamEvents;
            }

            foreach (var kvp in BLUFORTeamInfo.playerPositionPairList)
            {
                if (kvp.Key == player)
                {
                    lblPositionData.Content = kvp.Value;
                    break;
                }
            }

            foreach (var captain in BLUFORTeamInfo.captainList)
            {
                if (player == captain)
                {
                    LocDataBindable.GenerateBindingsForTarget(lblIsCaptainData, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.Yes);
                    goto finishCaptainCheck;
                }
            }
            LocDataBindable.GenerateBindingsForTarget(lblIsCaptainData, Label.ContentProperty, WorldCupViewer.Localization.LocalizationOptions.No);
        finishCaptainCheck:

            int goalCounter = 0;
            int yellowCardCounter = 0;
            foreach (var cupEvent in BLUFORCupTeamEvents)
            {
                if (cupEvent.player == player)
                {
                    if (CupEvent.CheckCupEvent(cupEvent, CupEvent.SupportedCupEventTypes.Goal))
                        goalCounter++;
                    if (CupEvent.CheckCupEvent(cupEvent, CupEvent.SupportedCupEventTypes.YellowCard))
                        yellowCardCounter++;
                }
            }

            lblGoalsScoredData.Content = goalCounter;
            lblYellowCardsData.Content = yellowCardCounter;

            ReloadImage();
        }

        public void RecieveSignal(string signalSignature)
        {
            if (signalSignature == LocalUtils.GetFullPauseSignal())
            {
                ShowLoadingScreen();
            }
            if (signalSignature == LocalUtils.GetFullResumeSignal())
            {
                HideLoadingScreen();
            }
            if (signalSignature == LocalUtils.GetMatchChangedSignal())
            {
                Close();
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

        public static void ImageRefreshNeeded(WeakReference<PlayerInfoWindow> weakRef)
        {
            if (!weakRef.TryGetTarget(out PlayerInfoWindow? window) || window == null)
                return;

            window.ReloadImage();
        }
        public void ReloadImage()
        {
            string cupPlayerImageTarget = "";
            byte[]? imgData;

            if (teamData.PlayerImagePairList == null)
                goto failed;

            foreach (var entry in teamData.PlayerImagePairList)
            {
                if (entry.Key != player.shirtNumber)
                    continue;

                cupPlayerImageTarget = entry.Value;
                break;
            }

            imgData = Images.TryGetExternalImageBytes(cupPlayerImageTarget);

            if (imgData == null)
                goto failed;

            goto fin;

        failed:
            imgData = Images.GetNoDataPngBytes();

        fin:
            imgPlayer.Source = LocalUtils.LoadImageFromByteArray(imgData);
        }
    }
}
