using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WorldCupWpf.LocalUtils;
using System.Windows.Controls;
using System.Windows.Input;
using WorldCupWpf.Signals;
using WorldCupLib;

namespace WorldCupWpf.UserControls
{
    public class TeamPlayerDisplay : UserControl, ISignalReciever
    {
        private string signalStartHovering;
        private string signalStopHovering;
        private LinearAnimationController linearAnimationController;

        public TeamPlayerDisplay(CupPlayer player)
        {
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
