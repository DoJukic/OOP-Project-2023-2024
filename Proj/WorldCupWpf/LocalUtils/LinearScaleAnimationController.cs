using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace WorldCupWpf.LocalUtils
{
    public class LinearScaleAnimationController
    {
        private const double FPS = 60;
        private double animationDurationSec = 1;

        private ScaleTransform targetScaleTransform;
        private double scaleXStart = 1;
        private double scaleXEnd = 1;
        private double scaleYStart = 1;
        private double scaleYEnd = 1;
        private bool animDirTowardsEnd = true;

        private bool animRunning = false;

        public double AnimationDurationSec
        {
            get { return animationDurationSec; }
            set
            {
                animationDurationSec = value;
                AnimationStart();
            }
        }

        public double ScaleXStart
        {
            get { return scaleXStart; }
            set
            {
                scaleXStart = value;
                AnimationStart();
            }
        }
        public double ScaleXEnd
        {
            get { return scaleXEnd; }
            set
            {
                scaleXEnd = value;
                AnimationStart();
            }
        }
        public double ScaleYStart
        {
            get { return scaleYStart; }
            set
            {
                scaleYStart = value;
                AnimationStart();
            }
        }
        public double ScaleYEnd
        {
            get { return scaleYEnd; }
            set
            {
                scaleYEnd = value;
                AnimationStart();
            }
        }

        public bool AnimDirTowardsEnd
        {
            get { return animDirTowardsEnd; }
            set
            {
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

}
