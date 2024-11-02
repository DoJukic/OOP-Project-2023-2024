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

        // https://stackoverflow.com/questions/19317064/disconnecting-an-element-from-any-unspecified-parent-container-in-wpf
        public static void RemoveFromParent(DependencyObject parent, UIElement child)
        {
            var panel = parent as Panel;
            if (panel != null)
            {
                panel.Children.Remove(child);
                return;
            }

            var decorator = parent as Decorator;
            if (decorator != null)
            {
                if (decorator.Child == child)
                {
                    decorator.Child = null;
                }
                return;
            }

            var contentPresenter = parent as ContentPresenter;
            if (contentPresenter != null)
            {
                if (contentPresenter.Content == child)
                {
                    contentPresenter.Content = null;
                }
                return;
            }

            var contentControl = parent as ContentControl;
            if (contentControl != null)
            {
                if (contentControl.Content == child)
                {
                    contentControl.Content = null;
                }
                return;
            }

            // maybe more
        }

        public class LinearScaleAnimationController
        {
            private const double FPS = 60;
            public double animationDurationSec = 1;
            public double AnimationDurationSec
            {
                get { return animationDurationSec; }
                set
                {
                    animationDurationSec = value;
                    AnimationStart();
                }
            }

            private ScaleTransform targetScaleTransform;
            public double scaleXStart = 1;
            public double ScaleXStart
            {
                get { return scaleXStart; }
                set
                {
                    scaleXStart = value;
                    AnimationStart();
                }
            }
            public double scaleXEnd = 1;
            public double ScaleXEnd
            {
                get { return scaleXEnd; }
                set
                {
                    scaleXEnd = value;
                    AnimationStart();
                }
            }

            public double scaleYStart = 1;
            public double ScaleYStart
            {
                get { return scaleYStart; }
                set
                {
                    scaleYStart = value;
                    AnimationStart();
                }
            }
            public double scaleYEnd = 1;
            public double ScaleYEnd
            {
                get { return scaleYEnd; }
                set
                {
                    scaleYEnd = value;
                    AnimationStart();
                }
            }

            private bool animRunning = false;

            public bool animDirTowardsEnd = true;
            public bool AnimDirTowardsEnd
            {
                get { return animDirTowardsEnd; }
                set {
                    animDirTowardsEnd = value;
                    AnimationStart();
                }
            }

            public LinearScaleAnimationController(UIElement target)
            {
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

                AnimationStart();

                return;

                //int timerMS = (int)(1000 / FPS);
                //timer = new(LinearAnimationController.TimerCallbackFunc, new WeakReference<LinearAnimationController>(this), timerMS, timerMS);
            }

            private void AnimationStart()
            {
                if (!animRunning)
                {
                    animRunning = true;
                    RunAnimation(new(this));
                }
            }
            private void AnimationFinished()
            {
                animRunning = false;
            }

            private static void RunAnimation(WeakReference<LinearScaleAnimationController> crtlMaybe)
            {
                Task.Run(async () =>
                {
                    await Task.Delay((int)(1000 / FPS)); if (!crtlMaybe.TryGetTarget(out var crtl) || crtl == null)
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

                            if (crtl.AnimDirTowardsEnd)
                            {
                                total = crtl.ScaleXEnd - crtl.ScaleXStart;
                                step = total / FPS / crtl.AnimationDurationSec;
                                toGo = crtl.ScaleXEnd - crtl.targetScaleTransform.ScaleX;

                                if (step > toGo)
                                    step = toGo;

                                crtl.targetScaleTransform.ScaleX += step;

                                total = crtl.ScaleYEnd - crtl.ScaleYStart;
                                step = total / FPS / crtl.AnimationDurationSec;
                                toGo = crtl.ScaleYEnd - crtl.targetScaleTransform.ScaleY;

                                if (step > toGo)
                                    step = toGo;

                                crtl.targetScaleTransform.ScaleY += step;
                            }
                            else
                            {
                                total = crtl.ScaleXStart - crtl.ScaleXEnd;
                                step = total / FPS / crtl.AnimationDurationSec;
                                toGo = crtl.ScaleXStart - crtl.targetScaleTransform.ScaleX;

                                if (step < toGo)
                                    step = toGo;

                                crtl.targetScaleTransform.ScaleX += step;

                                total = crtl.ScaleYStart - crtl.ScaleYEnd;
                                step = total / FPS / crtl.AnimationDurationSec;
                                toGo = crtl.ScaleYStart - crtl.targetScaleTransform.ScaleY;

                                if (step < toGo)
                                    step = toGo;

                                crtl.targetScaleTransform.ScaleY += step;
                            }

                            if (toGo != 0)
                            {
                                RunAnimation(crtlMaybe);
                                return;
                            }
                            crtl.AnimationFinished();
                        });
                    }
                    catch (Exception)
                    {
                        return; // dang
                    }
                });
            }

            private static void TimerCallbackFunc(object? obj)
            {
                if (obj is not WeakReference<LinearScaleAnimationController> crtlMaybe)
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

                        if (crtl.AnimDirTowardsEnd)
                        {
                            total = crtl.ScaleXEnd - crtl.ScaleXStart;
                            step = total / FPS / crtl.AnimationDurationSec;
                            toGo = crtl.ScaleXEnd - crtl.targetScaleTransform.ScaleX;

                            if (step > toGo)
                                step = toGo;

                            crtl.targetScaleTransform.ScaleX += step;

                            total = crtl.ScaleYEnd - crtl.ScaleYStart;
                            step = total / FPS / crtl.AnimationDurationSec;
                            toGo = crtl.ScaleYEnd - crtl.targetScaleTransform.ScaleY;

                            if (step > toGo)
                                step = toGo;

                            crtl.targetScaleTransform.ScaleY += step;
                        }
                        else
                        {
                            total = crtl.ScaleXStart - crtl.ScaleXEnd;
                            step = total / FPS / crtl.AnimationDurationSec;
                            toGo = crtl.ScaleXStart - crtl.targetScaleTransform.ScaleX;

                            if (step < toGo)
                                step = toGo;

                            crtl.targetScaleTransform.ScaleX += step;

                            total = crtl.ScaleYStart - crtl.ScaleYEnd;
                            step = total / FPS / crtl.AnimationDurationSec;
                            toGo = crtl.ScaleYStart - crtl.targetScaleTransform.ScaleY;

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
        public static string GetFullPauseSignal() =>
            (nameof(MainWindow) + "|START_PAUSE");
        public static string GetFullResumeSignal() =>
            (nameof(MainWindow) + "|STOP_PAUSE");
        public static string GetMatchChangedSignal() =>
            (nameof(MainWindow) + "|MATCH_CHANGED");
    }
}
