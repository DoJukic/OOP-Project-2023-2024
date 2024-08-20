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
            InitializeComponent();

            cbSelectedTeam.MouseWheel += ignoreMouseWheel;

            supportedLanguages = SupportedLanguages.GetSupportedLanguageInfoList();

            currSettings = SettingsProvider.GetSettings();
            if (SettingsProvider.GetErrorOccured())
                MTSSLFailedToLoadSettings.Visible = true;

            if (currSettings.SelectedWorldCupGUID != null)
                lastSelectedRepository = currSettings.SelectedWorldCupGUID;

            mainTabControl.Controls.Remove(tpTeamAndPlayerSelect);

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

            cblanguageSelect.SelectedIndexChanged += languageSelectComboBox_SelectedIndexChanged;
            cblanguageSelect.DisplayMember = "Name";

            foreach (SupportedLanguages.SupportedLanguageInfo language in supportedLanguages)
            {
                cblanguageSelect.Items.Add(language);

                if (language.langID == targetLang.langID)
                {
                    cblanguageSelect.SelectedIndex = cblanguageSelect.Items.IndexOf(language);
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
                    activeRepositoryDetails = deets;
                    activeRepoData = new() { GUID = Guid.Empty.ToString(), TeamDataList = new() };

                    //TODO: All tabs other than the main one must be removed here
                    mainTabControl.Controls.Remove(tpTeamAndPlayerSelect);

                    // add all the data here
                    if (activeRepositoryDetails != null)
                    {
                        lblSelectedCupDataName.Text = activeRepositoryDetails.Name;
                        lblSelectedCupDataYear.Text = activeRepositoryDetails.Year.ToString();
                        pbSelectedCupImage.Image = Image.FromStream(
                            Images.GetInternalImageStream_DO_NOT_DISPOSE_OR_WRITE(activeRepositoryDetails.InternalImageID) ??
                            Images.GetImgNotFoundPngStream_DO_NOT_DISPOSE_OR_WRITE());

                        if (activeRepositoryDetails.GUID == null || activeRepositoryDetails.GUID == Guid.Empty.ToString())
                        {
                            MTSSLGUIDError.Visible = true;
                        }
                        else
                        {
                            MTSSLGUIDError.Visible = false;

                            activeRepoData = SettingsProvider.TryGetDataFromGuid(currSettings, activeRepositoryDetails.GUID);

                            if (activeRepoData == null)
                            {
                                if (currSettings.WorldCupDataList == null)
                                    currSettings.WorldCupDataList = new();

                                activeRepoData = new() { GUID = activeRepositoryDetails.GUID, TeamDataList = new() };
                                currSettings.WorldCupDataList.Add(activeRepoData);

                                SaveSettings();
                            }
                        }
                    }
                    else
                    {
                        lblSelectedCupDataName.Text = "Unknown";
                        lblSelectedCupDataYear.Text = "????";
                        pbSelectedCupImage.Image = Image.FromStream(Images.GetImgNotFoundPngStream_DO_NOT_DISPOSE_OR_WRITE());
                        MTSSLGUIDError.Visible = true;
                    }

                    cbSelectedTeam.Items.Clear();
                    foreach (var team in activeRepository.GetCupTeams())
                    {
                        if (team == null) //Shouldn't happen
                            continue;
                        cbSelectedTeam.Items.Add(new KeyValuePair<string, CupTeam>(team.countryName + "(" + team.fifaCode + ")", team));
                    }

                    if (activeRepoData.SelectedTeamFifaID != null)
                    {
                        for (int i = 0; i < cbSelectedTeam.Items.Count; i++)
                        {
                            if (cbSelectedTeam.Items[i] is not KeyValuePair<string, CupTeam> kvp)
                                continue;

                            if (kvp.Value.fifaCode == activeRepoData.SelectedTeamFifaID)
                            {
                                cbSelectedTeam.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    mainTabControl.Controls.Add(tpTeamAndPlayerSelect);
                    mainTabControl.SelectedTab = tpTeamAndPlayerSelect;
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

                        pnlDataSelectLocalSources.Visible = false;
                        pnlDataSelectLocalSources.SuspendLayout();
                        pnlDataSelectLocalSources.Controls.Clear();

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
                                pnlDataSelectLocalSources.Controls.Add(gap);
                                gap.Dock = DockStyle.Top;
                            }

                            LocalCupDataSource SFDSV2 = new(deet);

                            SFDSV2.TabIndex = tabIndex;
                            SFDSV2.OnLoadButtonPressed += this.NewCupDataTaskStarted;

                            SFDSV2.BorderStyle = BorderStyle.FixedSingle;
                            pnlDataSelectLocalSources.Controls.Add(SFDSV2);
                            SFDSV2.Dock = DockStyle.Top;
                        }

                        pnlDataSelectLocalSources.ResumeLayout();
                        pnlDataSelectLocalSources.Visible = true;
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

                        pnlDataSelectRemoteSources.Visible = false;
                        pnlDataSelectRemoteSources.SuspendLayout();
                        pnlDataSelectRemoteSources.Controls.Clear();

                        foreach (var deet in deets.Where((deet) => deet != null))
                        {
                            tabIndex--;

                            if (isFirst)
                                isFirst = false;
                            else
                            {
                                Panel gap = new();
                                gap.Height = 6;
                                pnlDataSelectRemoteSources.Controls.Add(gap);
                                gap.Dock = DockStyle.Top;
                            }

                            RemoteCupDataSource RFDS = new(deet);

                            RFDS.TabIndex = tabIndex;

                            RFDS.BorderStyle = BorderStyle.FixedSingle;
                            pnlDataSelectRemoteSources.Controls.Add(RFDS);
                            RFDS.Dock = DockStyle.Top;
                        }

                        ApplyAPIStateToRemoteDataSources();
                        pnlDataSelectRemoteSources.ResumeLayout();
                        pnlDataSelectRemoteSources.Visible = true;
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
                LocalizationHandler.LocalizeAllChildren(tpDataSelect);
                LocalizationHandler.LocalizeAllChildren(tpTeamAndPlayerSelect);

                // status strips are *special*
                foreach (var unkn in statusStrip.Items)
                    if (unkn is ILocalizeable loc)
                        LocalizationHandler.LocalizeOne(loc);

                tpDataSelect.Text =
                    LocalizationHandler.GetCurrentLocOptionsString(
                        LocalizationOptions.Data_Select_Slash_Config);
                tpTeamAndPlayerSelect.Text =
                    LocalizationHandler.GetCurrentLocOptionsString(
                        LocalizationOptions.Team_Slash_Player_Select);
            });
        }

        public void ExternalImageChangeReported()
        {
            this.Invoke(() =>
            {
                ExternalImageUpdater.UpdateAllChildren(tpDataSelect);
                ExternalImageUpdater.UpdateAllChildren(tpTeamAndPlayerSelect);
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
            foreach (var thing in pnlDataSelectRemoteSources.Controls)
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

        private void SelectedTeamChanged(object sender, EventArgs e)
        {
            if (sender is not ComboBox cb || activeRepoData == null)
                return;

            if (cb.SelectedItem is not KeyValuePair<String, CupTeam> targetCupTeam)
                return;

            activeRepoData.SelectedTeamFifaID = targetCupTeam.Value.fifaCode;

            pnlPlayerList.Controls.Clear();
            pnlFavouritePlayers.Controls.Clear();

            pnlPlayerList.SuspendLayout();
            pnlPlayerList.Visible = false;

            foreach (var player in targetCupTeam.Value.SortedPlayers)
            {
                if (!(pnlPlayerList.Controls.Count == 0))
                {
                    Panel spacingPanel = new();
                    spacingPanel.Height = 6;

                    pnlPlayerList.Controls.Add(spacingPanel);
                    spacingPanel.Dock = DockStyle.Top;
                }

                CupPlayerDisplay PD = new (player, pnlPlayerList.Width - 6, false);
                pnlPlayerList.Controls.Add(PD);

                PD.Dock = DockStyle.Top;
            }

            pnlPlayerList.ResumeLayout();
            pnlPlayerList.Visible = true;

            SaveSettings();
        }

        void ignoreMouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
    }
}
