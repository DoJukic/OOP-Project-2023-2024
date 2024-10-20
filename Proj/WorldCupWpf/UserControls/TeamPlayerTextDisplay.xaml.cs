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
using WorldCupLib;
using WorldCupLib.Interface;
using WorldCupWpf.Signals;
using static WorldCupWpf.LocalUtils;

namespace WorldCupWpf.UserControls
{
    /// <summary>
    /// Interaction logic for TeamPlayerTextDisplay.xaml
    /// </summary>
    public partial class TeamPlayerTextDisplay : UserControl,  ISignalReciever
    {
        private string signalStartHovering;
        private string signalStopHovering;
        private LinearAnimationController linearAnimationController;

        public TeamPlayerTextDisplay(CupPlayer player)
        {
            InitializeComponent();

            lblName.Content = player.name;
            lblNumber.Content = player.shirtNumber;

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
            linearAnimationController.animDirTowardsEnd = false;
        }

        private void SignalStartHovering()
        {
            linearAnimationController.animDirTowardsEnd = true;
        }
    }
}
