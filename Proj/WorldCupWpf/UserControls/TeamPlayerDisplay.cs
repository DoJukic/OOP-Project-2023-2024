using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WorldCupWpf.LocalUtils.Utils;
using System.Windows.Controls;
using System.Windows.Input;
using WorldCupWpf.Signals;
using WorldCupLib;
using WorldCupWpf.LocalUtils;

namespace WorldCupWpf.UserControls
{
    public class TeamPlayerDisplay : UserControl, ISignalReciever
    {
        private string signalStartHovering;
        private string signalStopHovering;
        private LinearScaleAnimationController linearAnimationController;

        public TeamPlayerDisplay(CupPlayer player)
        {
            signalStartHovering = GetPlayerStartedHoveringSignal(player);
            signalStopHovering = GetPlayerStoppedHoveringSignal(player);

            linearAnimationController = new(this);
            linearAnimationController.ScaleXEnd = 1.1;
            linearAnimationController.ScaleYEnd = 1.1;
            linearAnimationController.AnimDirTowardsEnd = false;

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
            linearAnimationController.AnimDirTowardsEnd = false;
        }

        private void SignalStartHovering()
        {
            linearAnimationController.AnimDirTowardsEnd = true;
        }
    }
}
