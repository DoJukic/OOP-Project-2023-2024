﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WorldCupWpf.LocalUtils.Utils;
using WorldCupWpf.Signals;
using WorldCupLib;
using SharedDataLib;
using static SharedDataLib.SettingsProvider;
using System.IO;
using System.Numerics;
using System.Windows.Threading;
using WorldCupWpf.Dialog;
using System.Text.RegularExpressions;
using WorldCupWpf.LocalUtils;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace WorldCupWpf.UserControls
{
    /// <summary>
    /// Interaction logic for TeamPlayerImageDisplay.xaml
    /// </summary>
    public partial class TeamPlayerImageDisplay : UserControl, ISignalReciever
    {
        private string signalStartHovering;
        private string signalStopHovering;
        private LinearScaleAnimationController linearAnimationController;

        private CupPlayer player;
        private TeamData teamData;
        private CupMatch match;

        private Action imgRefreshAction;

        private int imageReloadActionCounter = 0;

        private DoubleAnimation OpacityAnimation = new();

        public TeamPlayerImageDisplay(CupPlayer player, TeamData teamData, CupMatch match)
        {
            InitializeComponent();

            // https://stackoverflow.com/questions/20298/how-to-stop-an-animation-in-c-sharp-wpf
            OpacityAnimation.From = 0;
            OpacityAnimation.To = 1;
            OpacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            this.player = player;
            this.teamData = teamData;
            this.match = match;

            WeakReference<TeamPlayerImageDisplay> weakRef = new(this);
            imgRefreshAction = () => {
                if (Application.Current != null)
                    Application.Current.Dispatcher.Invoke(() => { TeamPlayerImageDisplay.ImageRefreshNeeded(weakRef); });
            };
            Images.SubscribeToExternalImagesChanged(imgRefreshAction);

            ReloadImage();

            signalStartHovering = GetPlayerStartedHoveringSignal(player);
            signalStopHovering = GetPlayerStoppedHoveringSignal(player);

            linearAnimationController = new(this);
            linearAnimationController.ScaleXEnd = 1.1;
            linearAnimationController.ScaleYEnd = 1.1;
            linearAnimationController.AnimDirTowardsEnd = false;
            linearAnimationController.AnimationDurationSec = 0.15;

            SignalController.SubscribeToSignal(signalStartHovering, this);
            SignalController.SubscribeToSignal(signalStopHovering, this);

            MouseEnter += NotifyStartHover;
            MouseLeave += NotifyEndHover;

            lblNumber.Content = player.shirtNumber;
        }

        ~TeamPlayerImageDisplay()
        {
            Images.UnsubscribeFromExternalImagesChanged(imgRefreshAction);
        }

        public void ReverseImageVertically()
        {
            gridScaleTransform.ScaleY = -1;
        }

        public static void ImageRefreshNeeded(WeakReference<TeamPlayerImageDisplay> weakRef)
        {
            if (!weakRef.TryGetTarget(out TeamPlayerImageDisplay? display) || display == null)
                return;

            display.ReloadImage();
        }

        private void NotifyStartHover(object sender, MouseEventArgs e)
        {
            SignalController.TriggerSignal(signalStartHovering);
        }

        private void NotifyEndHover(object sender, MouseEventArgs e)
        {
            SignalController.TriggerSignal(signalStopHovering);
        }

        public void ReloadImage()
        {
            this.BeginAnimation(UserControl.OpacityProperty, null);

            int imageActionID = ++imageReloadActionCounter;
            Task.Run((Action)(() =>
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
                if (Application.Current == null)
                    return;

                BitmapImage btmpimg = LoadImageFromByteArray(imgData);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (imageActionID != imageReloadActionCounter)
                        return;

                    imgPlayer.Source = btmpimg;

                    this.BeginAnimation(UserControl.OpacityProperty, OpacityAnimation);
                });
            }));
        }

        public void RecieveSignal(string signalSignature)
        {
            if (signalSignature != signalStartHovering)
                SignalStartHovering();
            else if (signalSignature != signalStopHovering)
                SignalStopHovering();
        }

        private void SignalStopHovering()
        {
            linearAnimationController.AnimDirTowardsEnd = true;
        }

        private void SignalStartHovering()
        {
            linearAnimationController.AnimDirTowardsEnd = false;
        }

        private void userControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PlayerInfoWindow piw = new(player, teamData, match);
            piw.Owner = Application.Current.MainWindow;
            piw.Show();
        }
    }
}
