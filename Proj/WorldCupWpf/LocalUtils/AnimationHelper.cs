using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace WorldCupWpf.LocalUtils.LocalUtils
{
    // https://stackoverflow.com/questions/36517594/wpf-trigger-animation-from-code
    // (Modified)
    public static class AnimationHelper
    {
        private static void AnimateOpacity(DependencyObject target, double from, double to, int durationMS = 500, Action? OnFinished = null)
        {
            var opacityAnimation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(durationMS),
            };

            opacityAnimation.Completed += (dont, care) => { OnFinished?.Invoke(); };

            Storyboard.SetTarget(opacityAnimation, target);
            Storyboard.SetTargetProperty(opacityAnimation, new("Opacity"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(opacityAnimation);
            storyboard.Begin();
        }

        /// <summary>
        /// Fades in the given dependency object.
        /// </summary>
        /// <param name="target">The target dependency object to fade in.</param>
        public static void FadeIn(DependencyObject target, int? durationMS = null, Action? OnFinished = null)
        {
            if (durationMS == null)
                AnimateOpacity(target, 0, 1, OnFinished: OnFinished);
            else
                AnimateOpacity(target, 0, 1, (int)durationMS, OnFinished);
        }

        /// <summary>
        /// Fades in the given dependency object.
        /// </summary>
        /// <param name="target">The target dependency object to fade in.</param>
        public static void FadeOut(DependencyObject target, int? durationMS = null, Action? OnFinished = null)
        {
            if (durationMS == null)
                AnimateOpacity(target, 1, 0, OnFinished: OnFinished);
            else
                AnimateOpacity(target, 1, 0, (int)durationMS, OnFinished);
        }
    }
}
