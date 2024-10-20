using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
using WorldCupViewer.Localization;
using static System.Net.Mime.MediaTypeNames;
using static WorldCupWpf.LocalUtils;

namespace WorldCupWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SharedDataLib.SettingsProvider.CurrSettings settings = SharedDataLib.SettingsProvider.GetSettings();
        private SharedDataLib.SettingsProvider.WorldCupData? selectedWorldCupData;
        private IWorldCupDataRepo? worldCupRepo;

        private MainWindowBindings bindings = new();

        bool saveDataWarningShown = false;

        Thickness dumbassFieldImageShadowBinding;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoBindings();

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

            SettingsData resDat = new() { X = (int?)settings.ResolutionX ?? (int)this.Width,
                                            Y = (int?)settings.ResolutionY ?? (int)this.Height,
                                            maximized = settings.Maximized ?? false };

            if (settings.Language != null)
                LocalizationHandler.SetCulture(SupportedLanguages.GetSupportedLanguageInfo((SupportedLanguages.SupportedLanguage)(settings.Language)).culture);
            else
                LocalizationHandler.SetCulture(SupportedLanguages.GetDefaultSupportedLanguageInfo().culture);

            if (settings.Language == null || settings.ResolutionX == null || settings.ResolutionY == null)
            {
                ShowSettingsDialog(resDat);
            }

            PerformResolutionLogic(resDat);
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

        private void ShowSettingsDialog(SettingsData settingsDat)
        {
            SettingsWindow SW = new(settingsDat);
            SW.Owner = this;
            SW.ShowDialog();

            settings.Language = SupportedLanguages.GetSupportedLanguageInfoList()
                .Where(x => x.culture.ThreeLetterISOLanguageName == Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName)?.SingleOrDefault()?.langID;
            settings.ResolutionX = settingsDat.X;
            settings.ResolutionY = settingsDat.Y;
            settings.Maximized = settingsDat.maximized;

            PerformResolutionLogic(settingsDat);

            SaveData();
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
            Panel.SetZIndex(LoadingOverlay, 0);
            AnimationHelper.FadeIn(LoadingOverlay, 250);
        }
        private void HideLoadingScreen()
        {
            AnimationHelper.FadeOut(LoadingOverlay, 250, () => { Panel.SetZIndex(LoadingOverlay, -1000000); });
        }

        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            ShowLoadingScreen();
            SettingsData resDat = new() { X = (int)this.Width, Y = (int)this.Height, maximized = (this.WindowState == WindowState.Maximized) };

            ShowSettingsDialog(resDat);

            HideLoadingScreen();
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

        }

        LinearAnimationController crtl;
        private void btnTEST_Click(object sender, RoutedEventArgs e)
        {
            if (crtl is not null)
            {
                crtl.animDirTowardsEnd = !crtl.animDirTowardsEnd;
            }
            else
            {
                crtl = new(imgFootball);
                crtl.scaleXEnd = 2;
                crtl.scaleYEnd = 1.5;
                crtl.animationDurationSec = 2;
            }
        }
    }
}