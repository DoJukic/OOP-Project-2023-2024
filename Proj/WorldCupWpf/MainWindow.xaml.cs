using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharedDataLib;
using WorldCupLib;
using WorldCupLib.Interface;
using WorldCupViewer.Localization;
using WorldCupWpf.Dialog;
using WorldCupWpf.Signals;
using static System.Net.Mime.MediaTypeNames;
using static SharedDataLib.SettingsProvider;
using static WorldCupWpf.LocalUtils.Utils;

namespace WorldCupWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISignalReciever
    {
        private SharedDataLib.SettingsProvider.CurrSettings settings = SharedDataLib.SettingsProvider.GetSettings();
        private SharedDataLib.SettingsProvider.WorldCupData? selectedWorldCupData;

        bool saveDataWarningShown = false;

        private bool disableExitWarning = false;
        private bool disableDateReload = false;

        // we start in the loading screen, this is really just a dumb workaround for both the load cup info and settings window closing the loading screens, do not use elsewhere!
        private int loadingScreenCounter = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoBindings();

            SignalController.SubscribeToSignal(GetFullPauseSignal(), this);
            SignalController.SubscribeToSignal(GetFullResumeSignal(), this);

            cbSelectedTeam.DisplayMemberPath = "TeamName";
            cbSelectedOPFOR.DisplayMemberPath = "TeamName";

            using (MemoryStream stream = new MemoryStream(SharedDataLib.Images.GetImgNotFoundPngBytes()))
            {
                LoadingImage.Source = BitmapFrame.Create(stream,
                                                  BitmapCreateOptions.None,
                                                  BitmapCacheOption.OnLoad);
            }

            if (SettingsProvider.GetErrorOccured() || settings == null)
            {
                ErrorWindow ew = new(LocalizationOptions.Could_Not_Load_Data_Fatal);
                ew.Owner = this;
                ew.ShowDialog();
                Close();
                return;
            }

            if (settings.SelectedWorldCupGUID == null)
            {
                ShowFatalCupLoadingError();
                return;
            }

            selectedWorldCupData = settings?.WorldCupDataList?.Find((x) => { return x.GUID == settings.SelectedWorldCupGUID; });

            BeginLoadCupInfo(settings.SelectedWorldCupGUID);

            var screenX = System.Windows.SystemParameters.PrimaryScreenWidth;
            var screenY = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Left = (screenX - this.Width) / 2;
            this.Top = (screenY - this.Height) / 2;

            LocalizationHandler.SubscribeToLocalizationChanged(() => { Dispatcher.Invoke(LocBinder.LocalizationChanged); });
            LocalizationHandler.SubscribeToLocalizationChanged(() => { Dispatcher.Invoke(RefreshDates); });

            SettingsData resDat = new() { X = (int?)settings.ResolutionX ?? (int)this.Width,
                                            Y = (int?)settings.ResolutionY ?? (int)this.Height,
                                            maximized = settings.Maximized ?? false };

            if (settings.Language != null)
                LocalizationHandler.SetCulture(SupportedLanguages.GetSupportedLanguageInfo((SupportedLanguages.SupportedLanguage)(settings.Language)).culture);
            else
                LocalizationHandler.SetCulture(SupportedLanguages.GetDefaultSupportedLanguageInfo().culture);

            if (settings.Language == null || settings.ResolutionX == null || settings.ResolutionY == null)
            {
                _ = Task.Run(async () =>
                {
                    await Task.Delay(1); // Breaks the loading circle animation otherwise, apparently.

                    System.Windows.Application.Current.Dispatcher.Invoke(() => {
                        ShowSettingsDialog(resDat, false);
                    });
                });
            }

            PerformResolutionLogic(resDat);

            playerTextListRight.ReverseDisplay();

            ResizeLogic();
        }

        private void RefreshDates()
        {
            disableDateReload = true;

            var selectedItem = cbSelectedOPFORDate.SelectedItem;
            List<Object?> items = new();

            foreach (var item in cbSelectedOPFORDate.Items)
                items.Add(item);

            cbSelectedOPFORDate.Items.Clear();

            foreach (var item in items)
                cbSelectedOPFORDate.Items.Add(item);

            cbSelectedOPFORDate.SelectedItem = selectedItem;

            disableDateReload = false;
        }

        private void DoBindings()
        {
            return;
            /*
            // Shadow rectangle
            FieldImageBorder.SizeChanged += (obj, sizeChangedArgs) =>
            {
                bindings.FieldImageShadowMarginBinding = new(FieldImageBorder.ActualWidth, 0, 0, 0);
            };
            bindings.FieldImageShadowMarginBinding = new(FieldImageBorder.ActualWidth, 0, 0, 0);

            Binding locBinding = new(nameof(bindings.FieldImageShadowMarginBinding));

            locBinding.Source = bindings;
            locBinding.Mode = BindingMode.OneWay;

            BindingOperations.SetBinding(FieldImageShadowRect, Rectangle.MarginProperty, locBinding);
            */
        }

        private void BeginLoadCupInfo(String GUID)
        {
            Task.Run(() =>
            {
                AvailableFileDetails? deets;
                Task<IWorldCupDataRepo?>? task = WorldCupRepoBroker.BeginGetRepoByGUID(GUID, out deets);

                if (task == null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(ShowFatalCupLoadingError);
                    return;
                }

                task.Wait();
                var repo = task.Result;

                if (repo == null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(ShowFatalCupLoadingError);
                    return;
                }

                if (deets != null)
                {
                    if (deets.InternalImageID != null)
                        System.Windows.Application.Current.Dispatcher.Invoke(() => {
                                LoadingImage.Source = BitmapFrame.Create(SharedDataLib.Images.GetInternalImageStream(deets.InternalImageID),
                                                                          BitmapCreateOptions.None,
                                                                          BitmapCacheOption.OnLoad);
                        });

                    if (deets.Name != null)
                        System.Windows.Application.Current.Dispatcher.Invoke(() => {
                            LoadingText.Content = deets.Name + " (" + deets.Year + ")";
                        });
                }

                _ = Task.Run(async () =>
                {
                    await Task.Delay(1000); // WITNESS ME

                    System.Windows.Application.Current.Dispatcher.Invoke(() => {
                        HideLoadingScreen();
                    });
                });

                System.Windows.Application.Current.Dispatcher.Invoke(() => {
                    foreach (var cupTeam in repo.GetCupTeams())
                        cbSelectedTeam.Items.Add(new CupTeamWrapper(cupTeam));

                    if (selectedWorldCupData != null && selectedWorldCupData.SelectedTeamFifaID != null)
                    {
                        foreach (var cupTeamWrapper in cbSelectedTeam.Items)
                        {
                            if (cupTeamWrapper is not CupTeamWrapper ctw)
                                continue;

                            if (ctw.relatedTeam.fifaCode == selectedWorldCupData.SelectedTeamFifaID)
                            {
                                cbSelectedTeam.SelectedItem = ctw;
                                return;
                            }
                        }
                    }

                    disableExitWarning = true;
                });
            });
        }

        private void ShowFatalCupLoadingError()
        {
            ErrorWindow ew = new(LocalizationOptions.Could_Not_Load_Cup_Fatal);
            ew.Owner = this;
            ew.ShowDialog();
            Close();
            return;
        }

        private void SaveData()
        {
            if (!SharedDataLib.SettingsProvider.TrySave() && !saveDataWarningShown)
            {
                saveDataWarningShown = true;
                ErrorWindow ew = new(LocalizationOptions.Could_Not_Save_Data_Warning);
                ew.ShowDialog();
            }
        }

        private void ShowSettingsDialog(SettingsData settingsDat, bool useConfirmationMsg = true)
        {
            SignalController.TriggerSignal(GetFullPauseSignal());

            SettingsWindow SW = new(settingsDat);
            SW.Owner = this;
            SW.ShowDialog();

            settings.Language = SupportedLanguages.GetSupportedLanguageInfoList()
                .Where(x => x.culture.ThreeLetterISOLanguageName == Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName)?.SingleOrDefault()?.langID;

            if (settingsDat.changesMade && useConfirmationMsg)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("These settings will be saved.", "Are you sure?", System.Windows.MessageBoxButton.OKCancel);
                if (messageBoxResult != System.Windows.MessageBoxResult.OK)
                {
                    SaveData(); // Language changes dynamically, no point in not saving - it'd just be odd
                    return;
                }
            }

            settings.ResolutionX = settingsDat.X;
            settings.ResolutionY = settingsDat.Y;
            settings.Maximized = settingsDat.maximized;

            PerformResolutionLogic(settingsDat);

            SaveData();

            SignalController.TriggerSignal(GetFullResumeSignal());
        }
        private void PerformResolutionLogic(SettingsData settingsDat)
        {
            this.WindowState = settingsDat.maximized ? WindowState.Maximized : WindowState.Normal;

            if (this.Width == settingsDat.X && this.Height == settingsDat.Y)
                return;

            var screenX = System.Windows.SystemParameters.PrimaryScreenWidth;
            var screenY = System.Windows.SystemParameters.PrimaryScreenHeight;

            this.Width = settingsDat.X;
            this.Height = settingsDat.Y;

            if (this.Width > screenX)
                this.Width = screenX;
            if (this.Height > screenY)
                this.Height = screenY;

            this.Left = (screenX - this.Width) / 2;
            this.Top = (screenY - this.Height) / 2;

            if (this.Left < 0)
                this.Left = 0;
            if (this.Top < 0)
                this.Top = 0;
        }

        private void ShowLoadingScreen()
        {
            if (loadingScreenCounter == 0)
            {
                Panel.SetZIndex(LoadingOverlay, 0);
                LocalUtils.LocalUtils.AnimationHelper.FadeIn(LoadingOverlay, 250);
            }
            loadingScreenCounter++;
        }
        private void HideLoadingScreen()
        {
            loadingScreenCounter--;
            if (loadingScreenCounter <= 0)
            {
                if (loadingScreenCounter < 0)
                    loadingScreenCounter = 0;
                LocalUtils.LocalUtils.AnimationHelper.FadeOut(LoadingOverlay, 250, () => { Panel.SetZIndex(LoadingOverlay, -1000000); });
            }
        }

        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            //ShowLoadingScreen();
            SettingsData resDat = new() { X = (int)this.Width, Y = (int)this.Height, maximized = (this.WindowState == WindowState.Maximized) };

            ShowSettingsDialog(resDat);
            //HideLoadingScreen();
        }

        private class CupTeamWrapper
        {
            public CupTeam relatedTeam;

            public CupTeamWrapper(CupTeam relatedTeam)
            {
                this.relatedTeam = relatedTeam;
            }

            public String TeamName { get { return relatedTeam.countryName + " (" + relatedTeam.fifaCode + ")"; } }
        }
        private class CupOppositionWrapper
        {
            public CupTeam relatedTeam;
            public List<CupMatch> relatedMatches = new();

            public CupOppositionWrapper(CupTeam relatedTeam)
            {
                this.relatedTeam = relatedTeam;
            }

            public String TeamName
            {
                get
                {
                    if (relatedMatches.Count > 1)
                        return relatedTeam.countryName + " (" + relatedTeam.fifaCode + ")" + " [" + relatedMatches.Count + "]";

                    return relatedTeam.countryName + " (" + relatedTeam.fifaCode + ")";
                }
            }
        }
        // It might happen that we get two games should two teams that already played together make it to the finals
        private class CupMatchTimeWrapper
        {
            public CupMatch relatedMatch;

            public CupMatchTimeWrapper(CupMatch relatedMatch)
            {
                this.relatedMatch = relatedMatch;
            }

            public String LocalTimeString { get
                {
                    DateTime localTime = relatedMatch.matchDateTime.LocalDateTime;
                    return localTime.ToString("d");
                }
            }
        }

        private void cbSelectedTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSelectedTeam.SelectedItem == null || cbSelectedTeam.SelectedItem is not CupTeamWrapper ctw)
                return;

            cbSelectedOPFOR.SelectedIndex = -1;
            cbSelectedOPFOR.Items.Clear();

            SortedDictionary<String, CupOppositionWrapper> teamsDict = new();

            foreach (var match in ctw.relatedTeam.SortedMatches)
            {
                CupTeam targetTeam;

                if (match.homeTeam.team.fifaCode != ctw.relatedTeam.fifaCode)
                    targetTeam = match.homeTeam.team;
                else if (match.awayTeam.team.fifaCode != ctw.relatedTeam.fifaCode)
                    targetTeam = match.awayTeam.team;
                else
                    continue;

                teamsDict.TryAdd(targetTeam.fifaCode, new(targetTeam));
                teamsDict.First((x) => { return x.Key == targetTeam.fifaCode; }).Value.relatedMatches.Add(match);
            }

            foreach (var kvp in teamsDict)
            {
                cbSelectedOPFOR.Items.Add(kvp.Value);
            }

            if (cbSelectedOPFOR.Items.Count > 0)
                cbSelectedOPFOR.SelectedIndex = 0;

            if (selectedWorldCupData != null)
            {
                selectedWorldCupData.SelectedTeamFifaID = ctw.relatedTeam.fifaCode;
                SaveData();
            }
        }

        private void cbSelectedOPFOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbSelectedOPFORDate.SelectedIndex = -1;
            cbSelectedOPFORDate.Items.Clear();

            if (cbSelectedOPFOR.SelectedItem == null || cbSelectedOPFOR.SelectedItem is not CupOppositionWrapper cow)
                return;

            foreach (var match in cow.relatedMatches)
            {
                cbSelectedOPFORDate.Items.Add(new CupMatchTimeWrapper(match));
            }

            if (cbSelectedOPFORDate.Items.Count > 0)
                cbSelectedOPFORDate.SelectedIndex = 0;
        }

        private void cbSelectedOPFORDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (disableDateReload)
                return;
            if (cbSelectedOPFORDate.SelectedItem == null || cbSelectedOPFORDate.SelectedItem is not CupMatchTimeWrapper cmtw)
                return;
            if (cbSelectedTeam.SelectedItem == null || cbSelectedTeam.SelectedItem is not CupTeamWrapper ctw)
                return;
            if (cbSelectedOPFOR.SelectedItem == null || cbSelectedOPFOR.SelectedItem is not CupOppositionWrapper cow)
                return;

            CupMatchTeamInfo BLUFORTeamInfo;
            CupMatchTeamInfo OPFORTeamInfo;
            CupMatchTeamStatistics BLUFORTeamStatistics;
            CupMatchTeamStatistics OPFORTeamStatistics;
            List<CupEvent> BLUFORCupTeamEvents;
            List<CupEvent> OPFORCupTeamEvents;

            if (cmtw.relatedMatch.homeTeam.team == ctw.relatedTeam)
            {
                BLUFORTeamStatistics = cmtw.relatedMatch.homeTeamStatistics;
                BLUFORTeamInfo = cmtw.relatedMatch.homeTeam;
                OPFORTeamStatistics = cmtw.relatedMatch.awayTeamStatistics;
                OPFORTeamInfo = cmtw.relatedMatch.awayTeam;
                BLUFORCupTeamEvents = cmtw.relatedMatch.HomeTeamEvents;
                OPFORCupTeamEvents = cmtw.relatedMatch.AwayTeamEvents;
            }
            else
            {
                BLUFORTeamStatistics = cmtw.relatedMatch.awayTeamStatistics;
                BLUFORTeamInfo = cmtw.relatedMatch.awayTeam;
                OPFORTeamStatistics = cmtw.relatedMatch.homeTeamStatistics;
                OPFORTeamInfo = cmtw.relatedMatch.homeTeam;
                BLUFORCupTeamEvents = cmtw.relatedMatch.AwayTeamEvents;
                OPFORCupTeamEvents = cmtw.relatedMatch.HomeTeamEvents;
            }

            TeamData? BLUFORTargetTeamData = null;
            if (selectedWorldCupData != null && selectedWorldCupData.TeamDataList != null)
                foreach (var teamData in selectedWorldCupData.TeamDataList)
                    if (teamData.FifaID == BLUFORTeamInfo.team.fifaCode)
                        BLUFORTargetTeamData = teamData;
            if (BLUFORTargetTeamData == null)
                BLUFORTargetTeamData = new();

            TeamData? OPFORTargetTeamData = null;
            if (selectedWorldCupData != null && selectedWorldCupData.TeamDataList != null)
                foreach (var teamData in selectedWorldCupData.TeamDataList)
                    if (teamData.FifaID == OPFORTeamInfo.team.fifaCode)
                        OPFORTargetTeamData = teamData;
            if (OPFORTargetTeamData == null)
                OPFORTargetTeamData = new();

            playerTextListLeft.LoadTeam(cmtw.relatedMatch, BLUFORTeamStatistics, BLUFORTargetTeamData);
            playerTextListLeft.Visibility = Visibility.Visible;
            playerTextListRight.LoadTeam(cmtw.relatedMatch, OPFORTeamStatistics, OPFORTargetTeamData, Colors.Red);
            playerTextListRight.Visibility = Visibility.Visible;

            playerTextListRight.ReverseDisplay();

            if (BLUFORTeamInfo.penalties > 0)
                lblResultBLUFOR.Content = BLUFORTeamInfo.goals + " (" + BLUFORTeamInfo.penalties + ")";
            else
                lblResultBLUFOR.Content = BLUFORTeamInfo.goals;

            if (OPFORTeamInfo.penalties > 0)
                lblResultOPFOR.Content = OPFORTeamInfo.goals + " (" + OPFORTeamInfo.penalties + ")";
            else
                lblResultOPFOR.Content = OPFORTeamInfo.goals;

            //Away team top, home team bottom

            bottomImageList.LoadFromData(BLUFORTeamStatistics, cmtw.relatedMatch, BLUFORTeamInfo, BLUFORTargetTeamData, true);

            lblPlayerDistributionBLUFOR.Content = bottomImageList.GetPlayerDistributionString();

            topImageList.LoadFromData(OPFORTeamStatistics, cmtw.relatedMatch, OPFORTeamInfo, OPFORTargetTeamData, false);

            lblPlayerDistributionOPFOR.Content = topImageList.GetPlayerDistributionString();

            SignalController.TriggerSignal(GetMatchChangedSignal());
        }

        LocalUtils.LinearScaleAnimationController crtl;
        private void btnTEST_Click(object sender, RoutedEventArgs e)
        {
            if (crtl is not null)
            {
                crtl.AnimDirTowardsEnd = !crtl.AnimDirTowardsEnd;
            }
            else
            {
                crtl = new(imgFootball);
                crtl.ScaleXEnd = 2;
                crtl.ScaleYEnd = 1.5;
                crtl.AnimationDurationSec = 2;
            }
        }

        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeLogic();
        }

        private void ResizeLogic()
        {
            RemoveFromParent(playerTextListLeft.Parent, playerTextListLeft);
            RemoveFromParent(playerTextListRight.Parent, playerTextListRight);

            if ((this.ActualWidth - imgFootball.ActualWidth) < 600)
            {
                grdContentTop.Children.Add(playerTextListLeft);
                grdContentTop.Children.Add(playerTextListRight);
                playerTextListLeft.HideText();
                playerTextListRight.DisplaceText();
                return;
            }

            grdContentMiddle.Children.Add(playerTextListLeft);
            grdContentMiddle.Children.Add(playerTextListRight);
            playerTextListLeft.ResetText();
            playerTextListRight.ResetText();
        }

        private void btnBLUFORInfo_Click(object sender, RoutedEventArgs e)
        {
            if (cbSelectedTeam.SelectedItem is not CupTeamWrapper cupTeamWrapper)
                return;

            TeamInfoWindow tiw = new(cupTeamWrapper.relatedTeam);
            tiw.Owner = this;
            tiw.Show();
        }

        private void btnOPFORInfo_Click(object sender, RoutedEventArgs e)
        {
            if (cbSelectedOPFOR.SelectedItem is not CupOppositionWrapper cupTeamWrapper)
                return;

            TeamInfoWindow tiw = new(cupTeamWrapper.relatedTeam);
            tiw.Owner = this;
            tiw.Show();
        }

        public void RecieveSignal(string signalSignature)
        {
            if (signalSignature == GetFullPauseSignal())
            {
                ShowLoadingScreen();
            }
            if (signalSignature == GetFullResumeSignal())
            {
                HideLoadingScreen();
            }
        }

        private void mainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!disableExitWarning)
                return;

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("The application will close.", "Are you sure?", System.Windows.MessageBoxButton.OKCancel);
            if (messageBoxResult != System.Windows.MessageBoxResult.OK)
            {
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
            return;
        }
    }
}