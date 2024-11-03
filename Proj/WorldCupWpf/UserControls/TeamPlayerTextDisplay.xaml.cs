using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
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
using WorldCupLib.Interface;
using WorldCupWpf.Dialog;
using WorldCupWpf.LocalUtils;
using WorldCupWpf.Signals;
using static SharedDataLib.SettingsProvider;
using static WorldCupWpf.LocalUtils.Utils;

namespace WorldCupWpf.UserControls
{
    /// <summary>
    /// Interaction logic for TeamPlayerTextDisplay.xaml
    /// </summary>
    public partial class TeamPlayerTextDisplay : UserControl,  ISignalReciever
    {
        private string signalStartHovering;
        private string signalStopHovering;
        private LinearScaleAnimationController linearAnimationController;

        private CupPlayer player;
        private CupMatch match;
        private TeamData teamData;

        public TeamPlayerTextDisplay(CupPlayer player, CupMatch match, TeamData teamData)
        {
            InitializeComponent();

            this.player = player;
            this.match = match;
            this.teamData = teamData;

            lblName.Content = player.name;
            lblNumber.Content = player.shirtNumber;

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
        }

        public void ReverseText()
        {
            lblNameScaleTransorm.ScaleX = -1;
            lblNumberScaleTransorm.ScaleX = -1;
        }

        public void SetFontSize(int fontSize)
        {
            lblName.FontSize = fontSize;
            lblNumber.FontSize = fontSize;
        }

        private void NotifyStartHover(object sender, MouseEventArgs e)
        {
            SignalController.TriggerSignal(signalStartHovering);
        }

        private void NotifyEndHover(object sender, MouseEventArgs e)
        {
            SignalController.TriggerSignal(signalStopHovering);
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

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PlayerInfoWindow piw = new(player, teamData, match);
            piw.Owner = Application.Current.MainWindow;
            piw.Show();
        }
    }
}
