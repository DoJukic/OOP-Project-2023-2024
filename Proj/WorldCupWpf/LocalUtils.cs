using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using static WorldCupWpf.LocalUtils;
using System.Windows.Media;
using WorldCupLib;
using System.IO;
using System.Windows.Media.Imaging;

namespace WorldCupWpf
{
    public static class LocalUtils
    {
        public static void MediaElementInfiniteLoop(object sender, RoutedEventArgs e)
        {
            if (sender is not MediaElement me)
                return;

            me.Position = TimeSpan.FromSeconds(0);
            me.Play();
        }

        public static BitmapImage LoadImageFromByteArray(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        public class LinearAnimationController
        {
            private UIElement target;

            private const double FPS = 60;
            public int animationDurationSec = 1;

#pragma warning disable IDE0052 // Remove unread private members
            private Timer? timer;
#pragma warning restore IDE0052 // Remove unread private members

            private ScaleTransform targetScaleTransform;
            public double scaleXStart = 1;
            public double scaleXEnd = 1;
            public double scaleYStart = 1;
            public double scaleYEnd = 1;

            public bool animDirTowardsEnd = true;

            public LinearAnimationController(UIElement target)
            {
                this.target = target;

                TransformGroup trGroup;
                if (target.RenderTransform != null && target.RenderTransform is TransformGroup transformGroup)
                {
                    trGroup = transformGroup;
                }
                else
                {
                    trGroup = new();
                    target.RenderTransform = trGroup;
                }

                targetScaleTransform = new();
                trGroup.Children.Add(targetScaleTransform);

                int timerMS = (int)(1000 / FPS);
                timer = new(LinearAnimationController.TimerCallbackFunc, new WeakReference<LinearAnimationController>(this), timerMS, timerMS);
            }

            ~LinearAnimationController()
            {
                timer?.Dispose();
                timer = null; // buh bye
            }

            private static void TimerCallbackFunc(object? obj)
            {
                if (obj is not WeakReference<LinearAnimationController> crtlMaybe)
                    return;

                if (!crtlMaybe.TryGetTarget(out var crtl) || crtl == null)
                    return;

                try
                {
                    // Needed when exiting application :(
                    if (Application.Current == null)
                        return;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        double total;
                        double step;
                        double toGo;

                        if (crtl.animDirTowardsEnd)
                        {
                            total = crtl.scaleXEnd - crtl.scaleXStart;
                            step = total / FPS / crtl.animationDurationSec;
                            toGo = crtl.scaleXEnd - crtl.targetScaleTransform.ScaleX;

                            if (step > toGo)
                                step = toGo;

                            crtl.targetScaleTransform.ScaleX += step;

                            total = crtl.scaleYEnd - crtl.scaleYStart;
                            step = total / FPS / crtl.animationDurationSec;
                            toGo = crtl.scaleYEnd - crtl.targetScaleTransform.ScaleY;

                            if (step > toGo)
                                step = toGo;

                            crtl.targetScaleTransform.ScaleY += step;
                        }
                        else
                        {
                            total = crtl.scaleXStart - crtl.scaleXEnd;
                            step = total / FPS / crtl.animationDurationSec;
                            toGo = crtl.scaleXStart - crtl.targetScaleTransform.ScaleX;

                            if (step < toGo)
                                step = toGo;

                            crtl.targetScaleTransform.ScaleX += step;

                            total = crtl.scaleYStart - crtl.scaleYEnd;
                            step = total / FPS / crtl.animationDurationSec;
                            toGo = crtl.scaleYStart - crtl.targetScaleTransform.ScaleY;

                            if (step < toGo)
                                step = toGo;

                            crtl.targetScaleTransform.ScaleY += step;
                        }
                    });
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        public static string GetPlayerStartedHoveringSignal(CupPlayer player) =>
            (nameof(CupPlayer) + "|IS_HOVERING|" + player.team.fifaCode + '|' + player.shirtNumber);
        public static string GetPlayerStoppedHoveringSignal(CupPlayer player) =>
            (nameof(CupPlayer) + "|IS_NOT_HOVERING|" + player.team.fifaCode + '|' + player.shirtNumber);
    }
}
