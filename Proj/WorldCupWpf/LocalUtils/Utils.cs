using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using static WorldCupWpf.LocalUtils.Utils;
using System.Windows.Media;
using WorldCupLib;
using System.IO;
using System.Windows.Media.Imaging;

namespace WorldCupWpf.LocalUtils
{
    public static class Utils
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

        public static string GetPlayerStartedHoveringSignal(CupPlayer player) =>
            nameof(CupPlayer) + "|IS_HOVERING|" + player.team.fifaCode + '|' + player.shirtNumber;
        public static string GetPlayerStoppedHoveringSignal(CupPlayer player) =>
            nameof(CupPlayer) + "|IS_NOT_HOVERING|" + player.team.fifaCode + '|' + player.shirtNumber;
        public static string GetFullPauseSignal() =>
            nameof(MainWindow) + "|START_PAUSE";
        public static string GetFullResumeSignal() =>
            nameof(MainWindow) + "|STOP_PAUSE";
        public static string GetMatchChangedSignal() =>
            nameof(MainWindow) + "|MATCH_CHANGED";
    }
}
