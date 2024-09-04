using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic.Devices;
using SharedDataLib;
using WorldCupLib;
using WorldCupLib.Interface;
using WorldCupViewer.ExternalImages;
using WorldCupViewer.Localization;
using WorldCupViewer.PlayerImages;
using WorldCupViewer.Selectables;
using WorldCupViewer.UserControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WorldCupViewer
{
    public partial class MainForm : Form
    {
        private bool hasBeenShown = false;

        private int loadingRepoActionID = 0;
        private int availableFileDetailsChangedActionID = 0;
        private int downloadableFileDetailsChangedActionID = 0;

        private Object pnlPlayerListControlsChangeIDLock = new();
        private int pnlPlayerListControlsChangeID = 0;
        private Object pnlFavouritePlayerListControlsChangeIDLock = new();
        private int pnlFavouritePlayerListControlsChangeID = 0;

        private Object dragAndDropLock = new();
        private bool dragAndDropPrimed = false;

        private SettingsProvider.CurrSettings currSettings;

        private bool APIState = true;
        private SupportedLanguages.SupportedLanguageInfo? CurrentLanguage = null;

        private readonly IEnumerable<SupportedLanguages.SupportedLanguageInfo> supportedLanguages;

        private String? lastSelectedRepository = null;
        private IWorldCupDataRepo? activeRepository = null;
        private AvailableFileDetails? activeRepositoryDetails = null;
        private SettingsProvider.WorldCupData? activeRepoData = null;
        private SettingsProvider.TeamData? activeRepoTeamData = null;
        private KeyValuePair<String, CupTeam>? currentTargetCupTeamKVP = null;

        public MainForm()
        {
            InitializeComponent();

            // This makes sizing logic give us the unique behaviour you can see in the middle tab
            panel3.Height -= 20;
            mgbFavouritePlayers.MaximumSize = mgbFavouritePlayers.Size;
            panel3.Height += 20;

            supportedLanguages = SupportedLanguages.GetSupportedLanguageInfoList();

            currSettings = SettingsProvider.GetSettings();
            if (SettingsProvider.GetErrorOccured())
                MTSSLFailedToLoadSettings.Visible = true;

            if (currSettings.SelectedWorldCupGUID != null)
                lastSelectedRepository = currSettings.SelectedWorldCupGUID;

            mainTabControl.Controls.Remove(tpTeamAndPlayerSelect);
            mainTabControl.Controls.Remove(tpPlayerStatistics);

            // https://stackoverflow.com/questions/17423348/how-can-i-wait-until-a-form-handle-has-been-created-before-using-its-components
            // I think I could've just used the load event tbh
            HandleCreated += MainForm_HandleCreated;
            if (IsHandleCreated)
                MainForm_HandleCreated(null, new());
        }

        private void MainForm_HandleCreated(object? sender, EventArgs e)
        {
            SelectablesHandler.Init();

            LocalizationHandler.SubscribeToLocalizationChanged(new(this.LocalizationChangeReported));
            WorldCupRepoBroker.OnAvailableFileDetailsChanged.SafeSubscribe(new(this.AvailableFileDetailsChangeReported));
            WorldCupRepoBroker.OnAPISourcesLoaded.SafeSubscribe(new(this.DownloadableFileDetailsChangeReported));
            WorldCupRepoBroker.OnAPIStateChanged.SafeSubscribe(new(this.APIChangeReported));
            WorldCupRepoBroker.OnAPIFailed.SafeSubscribe(new(this.APIFailReported));
            Images.SubscribeToExternalImagesChanged(new(this.ExternalImageChangeReported));

            AvailableFileDetailsChangeReported();
            DownloadableFileDetailsChangeReported();
            APIChangeReported();

            pnlPlayerList.MouseDown += pnlPlayerList_DragAndDropMouseDown;
            pnlPlayerList.MouseUp += DragAndDropMouseUp;
            pnlPlayerList.DragEnter += pnlPlayerList_DragAndDropEnter;
            pnlPlayerList.DragDrop += pnlPlayerList_DragDrop;

            pnlFavouritePlayerList.MouseDown += pnlFavouritePlayerList_DragAndDropMouseDown;
            pnlFavouritePlayerList.MouseUp += DragAndDropMouseUp;
            pnlFavouritePlayerList.DragEnter += pnlFavouritePlayerList_DragAndDropEnter;
            pnlFavouritePlayerList.DragDrop += pnlFavouritePlayerList_DragDrop;

            pnlFavouritePlayerList.ControlAdded += pnlFavouritePlayerListChanged;
            pnlFavouritePlayerList.ControlRemoved += pnlFavouritePlayerListChanged;
            pnlFavouritePlayerListChanged(null, null);

            PlayerImageLinkIntermediary.onPlayerImagePairListChanged += PlayerImageLinkIntermediary_OnPlayerImagePairListChanged;

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

        private void MainForm_Shown(object sender, EventArgs e)
        {
            hasBeenShown = true;
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

                this.Invoke((() =>
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

                    // All tabs other than the main one must be removed here
                    mainTabControl.Controls.Remove(tpTeamAndPlayerSelect);
                    mainTabControl.Controls.Remove(tpPlayerStatistics);

                    // Add all the data here
                    if (activeRepositoryDetails != null)
                    {
                        lblSelectedCupDataName.Text = activeRepositoryDetails.Name;
                        lblSelectedCupDataYear.Text = activeRepositoryDetails.Year.ToString();
                        pbSelectedCupImage.Image = Image.FromStream(
                            Images.GetInternalImageStream(activeRepositoryDetails.InternalImageID) ??
                            Images.GetImgNotFoundPngStream());

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
                        pbSelectedCupImage.Image = Image.FromStream(Images.GetImgNotFoundPngStream());
                        MTSSLGUIDError.Visible = true;
                    }

                    cbSelectedTeam.Items.Clear();
                    foreach (var team in activeRepository.GetCupTeams())
                    {
                        if (team == null) //Shouldn't happen
                            continue;
                        cbSelectedTeam.Items.Add(new KeyValuePair<string, CupTeam>(team.countryName + "(" + team.fifaCode + ")", team));
                    }

                    lock (pnlPlayerListControlsChangeIDLock)
                        pnlPlayerListControlsChangeID++;
                    lock (pnlFavouritePlayerListControlsChangeIDLock)
                        pnlFavouritePlayerListControlsChangeID++;

                    LocalUtils.ClearControlsWithDispose(pnlPlayerList);
                    LocalUtils.ClearControlsWithDispose(pnlFavouritePlayerList);

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

                    var errorList = activeRepository.GetErrorList();
                    if (errorList.Count > 0)
                    {
                        MTSSLErrorsDetectedInData.Visible = true;
                        if (hasBeenShown)
                            ErrorsDetectedDialog.ShowNew(errorList);
                        else
                        {
                            EventHandler thingy = null;
                            thingy = new((obj, e) =>
                            {
                                ErrorsDetectedDialog.ShowNew(errorList);
                                this.Shown -= thingy;
                            });

                            this.Shown += thingy;
                        }
                    }
                    else
                        MTSSLErrorsDetectedInData.Visible = true;
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
                        LocalUtils.ClearControlsWithDispose(pnlDataSelectLocalSources);

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
                        LocalUtils.ClearControlsWithDispose(pnlDataSelectRemoteSources);

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
                LocalizationHandler.LocalizeAllChildren(tpPlayerStatistics);

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
                tpPlayerStatistics.Text =
                    LocalizationHandler.GetCurrentLocOptionsString(
                        LocalizationOptions.Team_Statistics);
            });
        }

        public void ExternalImageChangeReported()
        {
            this.Invoke(() =>
            {
                ExternalImageUpdater.UpdateAllChildren(tpDataSelect);
                ExternalImageUpdater.UpdateAllChildren(tpTeamAndPlayerSelect);
                ExternalImageUpdater.UpdateAllChildren(tpPlayerStatistics);

                SelectExternalImageDialog.LoadImages();
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
                    MTSSLDownloadState.Visible = false;
                    MTSSLDownloadStateState.Visible = false;

                    MTSSLDownloadStateState.Localization = LocalizationOptions.Ready;
                    MTSSLDownloadStateState.ForeColor = Color.Green;
                    return;
                }
                MTSSLDownloadState.Visible = true;
                MTSSLDownloadStateState.Visible = true;

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

            if (cb.SelectedItem is not KeyValuePair<String, CupTeam> tgtCupTeamKVP)
                return;

            mainTabControl.Controls.Remove(tpPlayerStatistics);

            activeRepoTeamData = null;

            currentTargetCupTeamKVP = tgtCupTeamKVP;
            activeRepoData.SelectedTeamFifaID = currentTargetCupTeamKVP.Value.Value.fifaCode;

            foreach (var team in activeRepoData.TeamDataList ??= new())
            {
                if (team.FifaID == activeRepoData.SelectedTeamFifaID)
                {
                    activeRepoTeamData = team;
                    break;
                }
            }

            if (activeRepoTeamData == null)
            {
                activeRepoTeamData = new();
                activeRepoTeamData.FifaID = currentTargetCupTeamKVP.Value.Value.fifaCode;
                activeRepoData.TeamDataList.Add(activeRepoTeamData);
            }

            PlayerImageLinkIntermediary.Configure(activeRepoTeamData.PlayerImagePairList ??= new());

            SaveSettings();

            int currentPlayerListChangeID;
            int currentFavouritePlayerListChangeID;

            lock (pnlPlayerListControlsChangeIDLock)
                currentPlayerListChangeID = ++pnlPlayerListControlsChangeID;
            lock (pnlFavouritePlayerListControlsChangeIDLock)
                currentFavouritePlayerListChangeID = ++pnlFavouritePlayerListControlsChangeID;

            List<Control> pnlPlayerListControls = new();
            List<Control> pnlFavouritePlayersControls = new();
            var favouritePlayerShirtNumbers = new[]
            {
                activeRepoTeamData.FavPlayer1ShirtNum,
                activeRepoTeamData.FavPlayer2ShirtNum,
                activeRepoTeamData.FavPlayer3ShirtNum
            };

            LocalUtils.ClearControlsWithDispose(pnlPlayerList);
            LocalUtils.ClearControlsWithDispose(pnlFavouritePlayerList);

            MTSSLPlayerDataIsLoading.Visible = true;

            Task.Run(() =>
            {
                int playerListTabIndex = currentTargetCupTeamKVP.Value.Value.SortedPlayers.Count() + 20000;
                int favouritePlayerListTabIndex = currentTargetCupTeamKVP.Value.Value.SortedPlayers.Count() + 10000;

                foreach (var player in currentTargetCupTeamKVP.Value.Value.SortedPlayers.Reverse())
                {
                    if (!(pnlPlayerListControls.Count == 0))
                    {
                        Panel spacingPanel = new();
                        spacingPanel.Height = 6;

                        pnlPlayerListControls.Add(spacingPanel);
                    }

                    CupPlayerDisplay PlayerDisplay = new(player, false);
                    pnlPlayerListControls.Add(PlayerDisplay);

                    PlayerDisplay.TabIndex = playerListTabIndex--;

                    if (favouritePlayerShirtNumbers.Contains(player.shirtNumber))
                    {
                        // just do the same thing except with pnlFavouritePlayersControls instead of pnlPlayerListControls
                        if (!(pnlFavouritePlayersControls.Count == 0))
                        {
                            Panel spacingPanel = new();
                            spacingPanel.Height = 6;

                            pnlFavouritePlayersControls.Add(spacingPanel);
                        }

                        CupPlayerDisplay FavouritePlayerDisplay = new(player, false);
                        pnlFavouritePlayersControls.Add(FavouritePlayerDisplay);

                        FavouritePlayerDisplay.TabIndex = favouritePlayerListTabIndex--;
                    }

                    lock (pnlPlayerListControlsChangeIDLock)
                        if (currentPlayerListChangeID != pnlPlayerListControlsChangeID)
                            return;
                }

                this.Invoke(() =>
                {
                    lock (pnlPlayerListControlsChangeIDLock)
                        if (currentPlayerListChangeID != pnlPlayerListControlsChangeID)
                            return;

                    MTSSLPlayerDataIsLoading.Visible = false;

                    pnlPlayerList.SuspendLayout();
                    pnlPlayerList.Visible = false;

                    foreach (var control in pnlPlayerListControls)
                    {
                        control.Dock = DockStyle.Top;
                    }
                    pnlPlayerList.Controls.AddRange(pnlPlayerListControls.ToArray());

                    PlayerImageLinkIntermediary.UpdateAllChildPlayerImageHolders(pnlPlayerList);

                    pnlPlayerList.ResumeLayout();
                    pnlPlayerList.Visible = true;

                    InitPlayerListDragAndDrop();

                    // just do the same thing except with pnlFavouritePlayersControls instead of pnlPlayerListControls, again.
                    lock (pnlFavouritePlayerListControlsChangeIDLock)
                        if (currentFavouritePlayerListChangeID != pnlFavouritePlayerListControlsChangeID)
                            return;

                    pnlFavouritePlayerList.SuspendLayout();
                    pnlFavouritePlayerList.Visible = false;

                    foreach (var control in pnlFavouritePlayersControls)
                    {
                        pnlFavouritePlayerList.Controls.Add(control);
                        control.Dock = DockStyle.Top;
                    }

                    PlayerImageLinkIntermediary.UpdateAllChildPlayerImageHolders(pnlFavouritePlayerList);

                    pnlFavouritePlayerList.ResumeLayout();
                    pnlFavouritePlayerList.Visible = true;

                    InitFavouritePlayerListDragAndDrop();
                });
            });
        }
        private void RegenerateFavouritePlayersPanel()
        {
            if (activeRepoData == null || currentTargetCupTeamKVP == null || activeRepoTeamData == null)
                return;

            int currentFavouritePlayerListChangeID;
            lock (pnlFavouritePlayerListControlsChangeIDLock)
                currentFavouritePlayerListChangeID = ++pnlFavouritePlayerListControlsChangeID;

            List<Control> pnlFavouritePlayersControls = new();
            var favouritePlayerShirtNumbers = new[]
            {
                activeRepoTeamData.FavPlayer1ShirtNum,
                activeRepoTeamData.FavPlayer2ShirtNum,
                activeRepoTeamData.FavPlayer3ShirtNum
            };

            LocalUtils.ClearControlsWithDispose(pnlFavouritePlayerList);

            Task.Run(() =>
            {
                int favouritePlayerListTabIndex = currentTargetCupTeamKVP.Value.Value.SortedPlayers.Count() + 10000;

                foreach (var player in currentTargetCupTeamKVP.Value.Value.SortedPlayers.Reverse())
                {
                    if (favouritePlayerShirtNumbers.Contains(player.shirtNumber))
                    {
                        if (!(pnlFavouritePlayersControls.Count == 0))
                        {
                            Panel spacingPanel = new();
                            spacingPanel.Height = 6;

                            pnlFavouritePlayersControls.Add(spacingPanel);
                        }

                        CupPlayerDisplay FavouritePlayerDisplay = new(player, false);
                        pnlFavouritePlayersControls.Add(FavouritePlayerDisplay);

                        FavouritePlayerDisplay.TabIndex = favouritePlayerListTabIndex--;
                    }

                    lock (pnlFavouritePlayerListControlsChangeIDLock)
                        if (currentFavouritePlayerListChangeID != pnlFavouritePlayerListControlsChangeID)
                            return;
                }

                this.Invoke(() =>
                {
                    lock (pnlFavouritePlayerListControlsChangeIDLock)
                        if (currentFavouritePlayerListChangeID != pnlFavouritePlayerListControlsChangeID)
                            return;

                    pnlFavouritePlayerList.SuspendLayout();
                    pnlFavouritePlayerList.Visible = false;

                    foreach (var control in pnlFavouritePlayersControls)
                    {
                        pnlFavouritePlayerList.Controls.Add(control);
                        control.Dock = DockStyle.Top;
                    }

                    PlayerImageLinkIntermediary.UpdateAllChildPlayerImageHolders(pnlFavouritePlayerList);

                    pnlFavouritePlayerList.ResumeLayout();
                    pnlFavouritePlayerList.Visible = true;

                    InitFavouritePlayerListDragAndDrop();
                    SetCorrectImagesForAllPlayers();
                });
            });
        }

        private void InitPlayerListDragAndDrop()
        {
            foreach (Control control in LocalUtils.GetAllControls(pnlPlayerList))
            {
                control.MouseDown += pnlPlayerList_DragAndDropMouseDown;
                control.MouseUp += DragAndDropMouseUp;

                control.AllowDrop = true;
                control.DragEnter += pnlPlayerList_DragAndDropEnter;
                control.DragDrop += pnlPlayerList_DragDrop;
            }
        }
        private void InitFavouritePlayerListDragAndDrop()
        {
            foreach (Control control in LocalUtils.GetAllControls(pnlFavouritePlayerList))
            {
                control.MouseDown += pnlFavouritePlayerList_DragAndDropMouseDown;
                control.MouseUp += DragAndDropMouseUp;

                control.AllowDrop = true;
                control.DragEnter += pnlFavouritePlayerList_DragAndDropEnter;
                control.DragDrop += pnlFavouritePlayerList_DragDrop;
            }
        }

        private void pnlPlayerList_DragAndDropMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                BeginMonitoredDragAndDrop(pnlPlayerList);
        }
        private void pnlFavouritePlayerList_DragAndDropMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                BeginMonitoredDragAndDrop(pnlFavouritePlayerList);
        }
        private void DragAndDropMouseUp(object? sender, MouseEventArgs e)
        {
            lock (dragAndDropLock)
                dragAndDropPrimed = false;
        }
        private void BeginMonitoredDragAndDrop(Control target)
        {
            lock (dragAndDropLock)
                dragAndDropPrimed = true;

            Task.Run(async () =>
            {
                bool dndPrimed;
                do
                {
                    await Task.Delay(1).ConfigureAwait(false);

                    if (SelectablesHandler.IsMouseOffsetTooLarge())
                    {
                        this.Invoke(() =>
                        {
                            target.DoDragDrop(
                                new KeyValuePair<String, List<ISelectable>>(target.Name, SelectablesHandler.TryGetSelectedChildren(target) ?? new()),
                                DragDropEffects.All
                            );
                        });

                        lock (dragAndDropLock)
                            dragAndDropPrimed = false;

                        return;
                    }

                    lock (dragAndDropLock)
                        dndPrimed = dragAndDropPrimed;

                } while (dndPrimed);
            });
        }

        private void DoDragDropEnterWithNameCheck(Control nameCheck, DragDropEffects effects, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;

            IEnumerable<ISelectable>? dataOut;
            if (!DoDragDropWithNameCheck(nameCheck, e, out dataOut))
                return;

            e.Effect = effects;
        }
        private bool DoDragDropWithNameCheck(Control nameCheck, DragEventArgs e, out IEnumerable<ISelectable>? dataOut)
        {
            dataOut = null;
            e.Effect = DragDropEffects.None;

            if (e.Data == null)
                return false;

            var unknown = e.Data.GetData(e.Data.GetFormats()[0]);

            if (unknown is not KeyValuePair<String, List<ISelectable>> data)
                return false;
            if (data.Key != nameCheck.Name)
                return false;

            dataOut = data.Value;
            return true;
        }

        private void pnlFavouritePlayerList_DragAndDropEnter(object? sender, DragEventArgs e)
        {
            DoDragDropEnterWithNameCheck(pnlPlayerList, DragDropEffects.Copy, e);
        }
        private void pnlPlayerList_DragAndDropEnter(object? sender, DragEventArgs e)
        {
            DoDragDropEnterWithNameCheck(pnlFavouritePlayerList, DragDropEffects.Move, e);
        }
        private void pnlPlayerList_DragDrop(object? sender, DragEventArgs e)
        {
            if (activeRepoTeamData == null || currentTargetCupTeamKVP == null)
                return;

            IEnumerable<ISelectable>? dataOut;
            if (!DoDragDropWithNameCheck(pnlFavouritePlayerList, e, out dataOut))
                return;
            if (dataOut == null)
                return;

            bool changeDetected = false;

            foreach (var thing in dataOut)
            {
                if (thing is CupPlayerDisplay crtl)
                {
                    if (activeRepoTeamData.FavPlayer1ShirtNum == crtl.associatedPlayer.shirtNumber)
                    {
                        activeRepoTeamData.FavPlayer1ShirtNum = null;
                        changeDetected = true;
                        continue;
                    }
                    if (activeRepoTeamData.FavPlayer2ShirtNum == crtl.associatedPlayer.shirtNumber)
                    {
                        activeRepoTeamData.FavPlayer2ShirtNum = null;
                        changeDetected = true;
                        continue;
                    }
                    if (activeRepoTeamData.FavPlayer3ShirtNum == crtl.associatedPlayer.shirtNumber)
                    {
                        activeRepoTeamData.FavPlayer3ShirtNum = null;
                        changeDetected = true;
                        continue;
                    }
                }
            }

            if (changeDetected)
            {
                SaveSettings();
                RegenerateFavouritePlayersPanel();
            }
        }
        private void pnlFavouritePlayerList_DragDrop(object? sender, DragEventArgs e)
        {
            if (activeRepoTeamData == null || currentTargetCupTeamKVP == null)
                return;

            IEnumerable<ISelectable>? dataOut;
            if (!DoDragDropWithNameCheck(pnlPlayerList, e, out dataOut))
                return;
            if (dataOut == null)
                return;

            var favouritePlayerShirtNumbers = new[]
            {
                activeRepoTeamData.FavPlayer1ShirtNum,
                activeRepoTeamData.FavPlayer2ShirtNum,
                activeRepoTeamData.FavPlayer3ShirtNum
            };

            bool changeDetected = false;

            foreach (var thing in dataOut)
            {
                if (thing is CupPlayerDisplay crtl)
                {
                    if (favouritePlayerShirtNumbers.Contains(crtl.associatedPlayer.shirtNumber))
                        continue;

                    if (activeRepoTeamData.FavPlayer1ShirtNum == null)
                    {
                        activeRepoTeamData.FavPlayer1ShirtNum = crtl.associatedPlayer.shirtNumber;
                        changeDetected = true;
                        continue;
                    }
                    if (activeRepoTeamData.FavPlayer2ShirtNum == null)
                    {
                        activeRepoTeamData.FavPlayer2ShirtNum = crtl.associatedPlayer.shirtNumber;
                        changeDetected = true;
                        continue;
                    }
                    if (activeRepoTeamData.FavPlayer3ShirtNum == null)
                    {
                        activeRepoTeamData.FavPlayer3ShirtNum = crtl.associatedPlayer.shirtNumber;
                        changeDetected = true;
                        continue;
                    }
                    break;
                }
            }

            if (changeDetected)
            {
                SaveSettings();
                RegenerateFavouritePlayersPanel();
            }
        }

        private void pnlFavouritePlayerListChanged(object? sender, ControlEventArgs e)
        {
            mainTabControl.Controls.Remove(tpPlayerStatistics);

            int numOfFavourites = 0;
            foreach (var crtl in pnlFavouritePlayerList.Controls)
            {
                if (crtl is CupPlayerDisplay)
                    numOfFavourites++;
            }

            mgbFavouritePlayers.SucceedingText = " (" + numOfFavourites + "/3)";

            UpdateFavouritePlayers(true);
        }
        private void UpdateFavouritePlayers(bool generateRankings = false)
        {
            List<CupPlayer> favourites = new();
            foreach (var crtl in pnlFavouritePlayerList.Controls)
            {
                if (crtl is IFavouriteablePlayerHolder pd)
                    favourites.Add(pd.GetAssociatedPlayer());
            }

            foreach (var crtl in LocalUtils.GetAllControls(tpPlayerStatistics).Union(LocalUtils.GetAllControls(tpTeamAndPlayerSelect)))
            {
                if (crtl is IFavouriteablePlayerHolder pd)
                {
                    pd.SetIsFavourite(favourites.Contains(pd.GetAssociatedPlayer()));
                }
            }

            if (generateRankings && favourites.Count >= 3)
            {
                GenerateTeamStatistics();
            }
        }

        private void PlayerImageLinkIntermediary_OnPlayerImagePairListChanged()
        {
            SaveSettings();
            SetCorrectImagesForAllPlayers();
        }

        private void SetCorrectImagesForAllPlayers()
        {
            PlayerImageLinkIntermediary.UpdateAllChildPlayerImageHolders(tpTeamAndPlayerSelect);
            PlayerImageLinkIntermediary.UpdateAllChildPlayerImageHolders(tpPlayerStatistics);
        }

        private void GenerateTeamStatistics()
        {
            mainTabControl.Controls.Remove(tpPlayerStatistics);

            LocalUtils.ClearControlsWithDispose(pnlPlayersByGoalsScored);
            LocalUtils.ClearControlsWithDispose(pnlPlayersByYellowCards);
            LocalUtils.ClearControlsWithDispose(pnlMatchesByAttendance);

            if (currentTargetCupTeamKVP == null)
                return;

            SortedDictionary<long, LinkedList<CupPlayer>> playerGoalsScoredDict = new();
            SortedDictionary<long, LinkedList<CupPlayer>> playerYellowCardsDict = new();

            foreach (var cupPlayer in currentTargetCupTeamKVP.Value.Value.SortedPlayers)
            {
                long goalsScored = 0;
                long yellowCards = 0;

                foreach (var cupEvent in cupPlayer.RelatedEvents)
                {
                    if (CupEvent.CheckCupEvent(cupEvent, CupEvent.SupportedCupEventTypes.Goal))
                        goalsScored++;
                    if (CupEvent.CheckCupEvent(cupEvent, CupEvent.SupportedCupEventTypes.YellowCard))
                        yellowCards++;
                }

                if (!playerGoalsScoredDict.TryGetValue(goalsScored, out LinkedList<CupPlayer>? llcpGoals) || llcpGoals == null)
                {
                    llcpGoals = new();
                    llcpGoals.AddFirst(cupPlayer);
                    playerGoalsScoredDict.Add(goalsScored, llcpGoals);
                }
                else
                    llcpGoals.AddLast(cupPlayer);

                if (!playerYellowCardsDict.TryGetValue(yellowCards, out LinkedList<CupPlayer>? llcpCards) || llcpCards == null)
                {
                    llcpCards = new();
                    llcpCards.AddFirst(cupPlayer);
                    playerYellowCardsDict.Add(yellowCards, llcpCards);
                }
                else
                    llcpCards.AddLast(cupPlayer);
            }

            int counter = currentTargetCupTeamKVP.Value.Value.SortedPlayers.Count;

            foreach (var kvp in playerGoalsScoredDict)
            {
                foreach (var cupPlayer in kvp.Value)
                {
                    if (pnlPlayersByGoalsScored.Controls.Count > 0)
                    {
                        Panel spacingPanel = new();
                        spacingPanel.Height = 6;
                        pnlPlayersByGoalsScored.Controls.Add(spacingPanel);
                        spacingPanel.Dock = DockStyle.Top;
                    }

                    int count = counter--;
                    CupPlayerStatsDisplay statsDisplay = new(cupPlayer, count.ToString() + ". " + cupPlayer.name + " (" + kvp.Key.ToString() + ")");
                    pnlPlayersByGoalsScored.Controls.Add(statsDisplay);
                    statsDisplay.Dock = DockStyle.Top;
                }
            }

            counter = currentTargetCupTeamKVP.Value.Value.SortedPlayers.Count;

            foreach (var kvp in playerYellowCardsDict)
            {
                foreach (var cupPlayer in kvp.Value)
                {
                    if (pnlPlayersByYellowCards.Controls.Count > 0)
                    {
                        Panel spacingPanel = new();
                        spacingPanel.Height = 6;
                        pnlPlayersByYellowCards.Controls.Add(spacingPanel);
                        spacingPanel.Dock = DockStyle.Top;
                    }

                    int count = counter--;
                    CupPlayerStatsDisplay statsDisplay = new(cupPlayer, count.ToString() + ". " + cupPlayer.name + " (" + kvp.Key.ToString() + ")");
                    pnlPlayersByYellowCards.Controls.Add(statsDisplay);
                    statsDisplay.Dock = DockStyle.Top;
                }
            }

            counter = currentTargetCupTeamKVP.Value.Value.SortedMatches.Count;

            foreach (var match in currentTargetCupTeamKVP.Value.Value.SortedMatches.OrderBy(match => match.attendance))
            {
                if (pnlMatchesByAttendance.Controls.Count > 0)
                {
                    Panel spacingPanel = new();
                    spacingPanel.Height = 6;
                    pnlMatchesByAttendance.Controls.Add(spacingPanel);
                    spacingPanel.Dock = DockStyle.Top;
                }

                int count = counter--;
                CupMatchStatsDisplay statsDisplay = new(
                    count.ToString() + ". " + match.location,
                    match.homeTeam.team.countryName + " (" + match.homeTeam.team.fifaCode + ")",
                    match.attendance.ToString(),
                    match.awayTeam.team.countryName + " (" + match.awayTeam.team.fifaCode + ")"
                );
                pnlMatchesByAttendance.Controls.Add(statsDisplay);
                statsDisplay.Dock = DockStyle.Top;
            }

            PlayerImageLinkIntermediary.UpdateAllChildPlayerImageHolders(tpPlayerStatistics);
            UpdateFavouritePlayers();

            mainTabControl.Controls.Add(tpPlayerStatistics);
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            this.SuspendLayout();
            base.OnResizeBegin(e);
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            this.ResumeLayout();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            this.SuspendLayout();
            base.OnClosing(e);
            this.ResumeLayout();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            return; // TODO: REMOVEME
            e.Cancel = !YesNoDialog.ShowNew(
                LocalizationHandler.GetCurrentLocOptionsString(LocalizationOptions.Are_You_Sure) + "?",
                LocalizationHandler.GetCurrentLocOptionsString(LocalizationOptions.Are_You_Sure_You_Want_To_Close_The_Application) + "?"
            );
        }
    }
}
