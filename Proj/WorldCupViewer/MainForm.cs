using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SharedDataLib;
using WorldCupLib;
using WorldCupViewer.ExternalImages;
using WorldCupViewer.Localization;
using WorldCupViewer.UserControls;

namespace WorldCupViewer
{
    public partial class MainForm : Form
    {
        private int loadingRepoActionID = 0;
        private int availableFileDetailsChangedActionID = 0;
        private int downloadableFileDetailsChangedActionID = 0;

        private SettingsProvider.CurrSettings currSettings;

        private bool APIState = true;
        private SupportedLanguages.SupportedLanguageInfo? CurrentLanguage = null;

        private readonly IEnumerable<SupportedLanguages.SupportedLanguageInfo> supportedLanguages;

        private String? lastSelectedRepository = null;
        private IWorldCupDataRepo? activeRepository = null;
        private AvailableFileDetails? activeRepositoryDetails = null;
        private SettingsProvider.WorldCupData? activeRepoData = null;

        public MainForm()
        {
            supportedLanguages = SupportedLanguages.GetSupportedLanguageInfoList();

            currSettings = SettingsProvider.GetSettings();
            if (SettingsProvider.GetErrorOccured())
                MTSSLFailedToLoadSettings.Visible = true;

            if (currSettings.SelectedWorldCupGUID != null)
                lastSelectedRepository = currSettings.SelectedWorldCupGUID;

            InitializeComponent();

            mainTabControl.Controls.Remove(teamAndPlayerSelectTab);

            // https://stackoverflow.com/questions/17423348/how-can-i-wait-until-a-form-handle-has-been-created-before-using-its-components
            // I think I could've just used the load event tbh
            HandleCreated += MainForm_HandleCreated;
            if (IsHandleCreated) // Paranoia strikes deep
                MainForm_HandleCreated(null, new()); // Into your heart it will creep
        }

        private void MainForm_HandleCreated(object? sender, EventArgs e)
        {
            LocalizationHandler.SubscribeToLocalizationChanged(new(this.LocalizationChangeReported));
            WorldCupRepoBroker.OnAvailableFileDetailsChanged.SafeSubscribe(new(this.AvailableFileDetailsChangeReported));
            WorldCupRepoBroker.OnAPISourcesLoaded.SafeSubscribe(new(this.DownloadableFileDetailsChangeReported));
            WorldCupRepoBroker.OnAPIStateChanged.SafeSubscribe(new(this.APIChangeReported));
            WorldCupRepoBroker.OnAPIFailed.SafeSubscribe(new(this.APIFailReported));
            Images.SubscribeToExternalImagesChanged(new(this.ExternalImageChangeReported));

            AvailableFileDetailsChangeReported();
            DownloadableFileDetailsChangeReported();
            APIChangeReported();

            // Language logic

            SupportedLanguages.SupportedLanguageInfo targetLang;
            try
            {
#pragma warning disable CS8629 // Nullable value type may be null, but we're in a try-catch
                targetLang = SupportedLanguages.GetSupportedLanguageInfo((SupportedLanguages.SupportedLanguage)currSettings.Language);
#pragma warning restore CS8629
            }
            catch (Exception)
            {
                targetLang = SupportedLanguages.GetDefaultSupportedLanguageInfo();
            }

            languageSelectComboBox.SelectedIndexChanged += languageSelectComboBox_SelectedIndexChanged;
            languageSelectComboBox.DisplayMember = "Name";

            foreach (SupportedLanguages.SupportedLanguageInfo language in supportedLanguages)
            {
                languageSelectComboBox.Items.Add(language);

                if (language.langID == targetLang.langID)
                {
                    languageSelectComboBox.SelectedIndex = languageSelectComboBox.Items.IndexOf(language);
                }
            }
        }

        public void NewCupDataTaskStarted(Task<IWorldCupDataRepo?> repo, String? guid, AvailableFileDetails? deets)
        {
            if (guid != null)
            {
                currSettings.SelectedWorldCupGUID = guid;
                SaveSettings();
            }

            int currentActionID = ++loadingRepoActionID;
            MTSSLDataIsLoading.Visible = true;
            MTSSLFailedToLoadData.Visible = false;
            Task.Run(() =>
            {
                repo.Wait();

                this.Invoke((Delegate)(() =>
                {
                    if (currentActionID != loadingRepoActionID)
                        return;

                    MTSSLDataIsLoading.Visible = false;

                    if (repo.Result == null)
                    {
                        MTSSLFailedToLoadData.Visible = true;
                        return;
                    }

                    activeRepository = repo.Result;

                    //TODO: All tabs other than the main one must be removed here
                    mainTabControl.Controls.Remove(teamAndPlayerSelectTab);

                    activeRepositoryDetails = deets;

                    // add all the data here
                    if (activeRepositoryDetails != null)
                    {
                        SelectedCupDataNameLabel.Text = activeRepositoryDetails.Name;
                        SelectedCupDataYearLabel.Text = activeRepositoryDetails.Year.ToString();
                        SelectedCupDataPictureBox.Image = Image.FromStream(
                            Images.GetInternalImageStream_DO_NOT_DISPOSE_OR_WRITE(activeRepositoryDetails.InternalImageID) ??
                            Images.GetImgNotFoundPngStream_DO_NOT_DISPOSE_OR_WRITE());

                        if (activeRepositoryDetails.GUID == null || activeRepositoryDetails.GUID == Guid.Empty.ToString())
                            MTSSLGUIDError.Visible = true;
                        else
                            MTSSLGUIDError.Visible = false;
                    }
                    else
                    {
                        SelectedCupDataNameLabel.Text = "Unknown";
                        SelectedCupDataYearLabel.Text = "????";
                        SelectedCupDataPictureBox.Image = Image.FromStream(Images.GetImgNotFoundPngStream_DO_NOT_DISPOSE_OR_WRITE());
                        MTSSLGUIDError.Visible = true;
                    }

                    SelectedTeamComboBox.Items.Clear();
                    //SelectedTeamComboBox.SelectedIndex = -1;
                    //SelectedTeamComboBox.ResetText();


                    foreach (var team in activeRepository.GetCupTeams())
                    {
                        if (team == null) //Shouldn't happen
                            continue;
                        SelectedTeamComboBox.Items.Add(new KeyValuePair<string, CupTeam>(team.countryName + "(" + team.fifaCode + ")", team));
                    }

                    mainTabControl.Controls.Add(teamAndPlayerSelectTab);
                    mainTabControl.SelectedTab = teamAndPlayerSelectTab;
                }));
            });
        }

        public void AvailableFileDetailsChangeReported()
        {
            // This func is intended to be called from a thread pool context, so we have to take extra care
            this.Invoke(() =>
            {
                if (lastSelectedRepository != null)
                {
                    AvailableFileDetails? deets;
                    var thing = WorldCupRepoBroker.BeginGetRepoByGUID(lastSelectedRepository, out deets);
                    if (thing != null)
                        NewCupDataTaskStarted(thing, lastSelectedRepository, deets);
                    lastSelectedRepository = null;
                }

                int currentAvailableFileDetailsChangedActionID = ++availableFileDetailsChangedActionID;

                _ = Task.Run(async () =>
                {
                    var deets = await WorldCupRepoBroker.BeginGetAvailableFileDetails().ConfigureAwait(false);

                    this.Invoke(() =>
                    {
                        if (currentAvailableFileDetailsChangedActionID != availableFileDetailsChangedActionID)
                            return;

                        bool isFirst = true;
                        int tabIndex = deets.Count() + 20000;

                        dataSelectLocalSourcesPanel.Visible = false;
                        dataSelectLocalSourcesPanel.SuspendLayout();
                        dataSelectLocalSourcesPanel.Controls.Clear();

                        var nonNullDeets = deets.Where((deet) => deet != null);

                        if (activeRepositoryDetails != null && activeRepositoryDetails.GUID != null && activeRepositoryDetails.GUID != Guid.Empty.ToString())
                        {
                            bool noMatchingGuid = true;
                            foreach (var deet in nonNullDeets)
                            {
                                if (deet.GUID == activeRepositoryDetails.GUID)
                                {
                                    noMatchingGuid = false;
                                    break;
                                }
                            }

                            if (noMatchingGuid)
                                MTSSLGUIDError.Visible = true;
                            else
                                MTSSLGUIDError.Visible = false;
                        }

                        foreach (var deet in nonNullDeets)
                        {
                            tabIndex--;

                            if (isFirst)
                                isFirst = false;
                            else
                            {
                                Panel gap = new();
                                gap.Height = 6;
                                dataSelectLocalSourcesPanel.Controls.Add(gap);
                                gap.Dock = DockStyle.Top;
                            }

                            LocalCupDataSource SFDSV2 = new(deet);

                            SFDSV2.TabIndex = tabIndex;
                            SFDSV2.OnLoadButtonPressed += this.NewCupDataTaskStarted;

                            SFDSV2.BorderStyle = BorderStyle.FixedSingle;
                            dataSelectLocalSourcesPanel.Controls.Add(SFDSV2);
                            SFDSV2.Dock = DockStyle.Top;
                        }

                        dataSelectLocalSourcesPanel.ResumeLayout();
                        dataSelectLocalSourcesPanel.Visible = true;
                    });
                });

            });
        }

        public void DownloadableFileDetailsChangeReported()
        {
            // This func is intended to be called from a thread pool context, so we have to take extra care
            this.Invoke(() =>
            {
                int currentDownloadableFileDetailsChangedActionID = ++downloadableFileDetailsChangedActionID;

                _ = Task.Run(async () =>
                {
                    var deets = await WorldCupRepoBroker.BeginGetCurrentAPISources().ConfigureAwait(false);

                    this.Invoke(() =>
                    {
                        if (currentDownloadableFileDetailsChangedActionID != downloadableFileDetailsChangedActionID)
                            return;

                        bool isFirst = true;
                        int tabIndex = deets.Count() + 10000;

                        dataSelectRemoteSourcesPanel.Visible = false;
                        dataSelectRemoteSourcesPanel.SuspendLayout();
                        dataSelectRemoteSourcesPanel.Controls.Clear();

                        foreach (var deet in deets.Where((deet) => deet != null))
                        {
                            tabIndex--;

                            if (isFirst)
                                isFirst = false;
                            else
                            {
                                Panel gap = new();
                                gap.Height = 6;
                                dataSelectRemoteSourcesPanel.Controls.Add(gap);
                                gap.Dock = DockStyle.Top;
                            }

                            RemoteCupDataSource RFDS = new(deet);

                            RFDS.TabIndex = tabIndex;

                            RFDS.BorderStyle = BorderStyle.FixedSingle;
                            dataSelectRemoteSourcesPanel.Controls.Add(RFDS);
                            RFDS.Dock = DockStyle.Top;
                        }

                        ApplyAPIStateToRemoteDataSources();
                        dataSelectRemoteSourcesPanel.ResumeLayout();
                        dataSelectRemoteSourcesPanel.Visible = true;
                    });
                });

            });
        }

        private void languageSelectComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (sender == null || sender is not ComboBox targetComboBox ||
                targetComboBox.SelectedItem == null || targetComboBox.SelectedItem is not SupportedLanguages.SupportedLanguageInfo languageInfo ||
                languageInfo == CurrentLanguage)
                return;

            if (CurrentLanguage != null)
                if (!YesNoDontAskAgainDialog.ShowNew(
                        "languageSelectComboBox_SelectedIndexChanged_Verify",
                        LocalizationHandler.GetCurrentLocOptionsString(LocalizationOptions.Are_You_Sure) + '?',
                        LocalizationHandler.GetCurrentLocOptionsString(LocalizationOptions.Are_You_Sure_You_Want_To_Change_The_Language) + '?'
                    ).yes)
                {
                    targetComboBox.SelectedItem = CurrentLanguage;
                    return;
                }

            CurrentLanguage = languageInfo;
            LocalizationHandler.SetCulture(languageInfo.culture);
            currSettings.Language = languageInfo.langID;
            SaveSettings();
        }

        public void LocalizationChangeReported()
        {
            this.Invoke(() =>
            {
                LocalizationHandler.LocalizeAllChildren(dataSelectTab);
                LocalizationHandler.LocalizeAllChildren(teamAndPlayerSelectTab);

                // status strips are *special*
                foreach (var unkn in statusStrip.Items)
                    if (unkn is ILocalizeable loc)
                        LocalizationHandler.LocalizeOne(loc);

                dataSelectTab.Text =
                    LocalizationHandler.GetCurrentLocOptionsString(
                        LocalizationOptions.Data_Select_Slash_Config);
                teamAndPlayerSelectTab.Text =
                    LocalizationHandler.GetCurrentLocOptionsString(
                        LocalizationOptions.Team_Slash_Player_Select);
            });
        }

        public void ExternalImageChangeReported()
        {
            this.Invoke(() =>
            {
                ExternalImageUpdater.UpdateAllChildren(dataSelectTab);
                ExternalImageUpdater.UpdateAllChildren(teamAndPlayerSelectTab);
            });
        }

        public void APIChangeReported()
        {
            this.Invoke(() =>
            {
                APIState = WorldCupRepoBroker.BeginGetAPIFetchIsReady().Result;
                ApplyAPIStateToRemoteDataSources();
                if (APIState)
                {
                    MTSSLDownloadStateState.Localization = LocalizationOptions.Ready;
                    MTSSLDownloadStateState.ForeColor = Color.Green;
                    return;
                }
                MTSSLDownloadFailed.Visible = false;
                MTSSLDownloadStateState.Localization = LocalizationOptions.Busy;
                MTSSLDownloadStateState.ForeColor = Color.DarkOrange;
            });
        }

        public void ApplyAPIStateToRemoteDataSources()
        {
            foreach (var thing in dataSelectRemoteSourcesPanel.Controls)
            {
                if (thing is not RemoteCupDataSource RCDS)
                    continue;

                RCDS.setAPIState(APIState);
            }
        }

        public void APIFailReported()
        {
            this.Invoke(() =>
            {
                MTSSLDownloadFailed.Visible = true;
            });
        }

        public void SaveSettings()
        {
            if (SettingsProvider.TrySave())
                MTSSLFailedToSaveSettings.Visible = false;
            else
                MTSSLFailedToSaveSettings.Visible = true;
        }

        private void SelectedCupDataPictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
