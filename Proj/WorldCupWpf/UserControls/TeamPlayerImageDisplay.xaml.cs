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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WorldCupWpf.LocalUtils;
using WorldCupWpf.Signals;
using WorldCupLib;
using SharedDataLib;
using static SharedDataLib.SettingsProvider;
using System.IO;
using System.Numerics;

namespace WorldCupWpf.UserControls
{
    /// <summary>
    /// Interaction logic for TeamPlayerImageDisplay.xaml
    /// </summary>
    public partial class TeamPlayerImageDisplay : UserControl, ISignalReciever
    {
        private string signalStartHovering;
        private string signalStopHovering;
        private LinearAnimationController linearAnimationController;

        private CupPlayer player;
        private TeamData teamData;

        private Action imgRefreshAction;

        public TeamPlayerImageDisplay(CupPlayer player, TeamData teamData)
        {
            InitializeComponent();

            this.player = player;
            this.teamData = teamData;

            imgRefreshAction = () => { TeamPlayerImageDisplay.ImageRefreshNeeded(new(this)); };
            Images.SubscribeToExternalImagesChanged(imgRefreshAction);

            ReloadImage();

            signalStartHovering = GetPlayerStartedHoveringSignal(player);
            signalStopHovering = GetPlayerStoppedHoveringSignal(player);

            linearAnimationController = new(this);
            linearAnimationController.scaleXEnd = 1.1;
            linearAnimationController.scaleYEnd = 1.1;
            linearAnimationController.animDirTowardsEnd = false;

            SignalController.SubscribeToSignal(signalStartHovering, this);
            SignalController.SubscribeToSignal(signalStopHovering, this);

            MouseEnter += NotifyStartHover;
            MouseLeave += NotifyEndHover;
        }

        ~TeamPlayerImageDisplay()
        {
            Images.UnsubscribeFromExternalImagesChanged(imgRefreshAction);
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
            imgData = Images.GetImgNotFoundPngBytes();

        fin:
            imgPlayer.Source = LoadImageFromByteArray(imgData);
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
            linearAnimationController.animDirTowardsEnd = false;
        }

        private void SignalStartHovering()
        {
            linearAnimationController.animDirTowardsEnd = true;
        }
    }
}
