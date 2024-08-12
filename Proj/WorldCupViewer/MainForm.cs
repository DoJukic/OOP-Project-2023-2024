using System.ComponentModel;
using System.Windows.Forms;
using SharedDataLib;
using WorldCupLib;
using WorldCupViewer.Localization;
using WorldCupViewer.UserControls;

namespace WorldCupViewer
{
    public partial class MainForm : Form
    {
        private int loadingRepoActionID = 0;
        private int availableFileDetailsChangedActionID = 0;
        private int downloadableFileDetailsChangedActionID = 0;

        private readonly IEnumerable<SupportedLanguages.SupportedLanguageInfo> supportedLanguages;

        public MainForm()
        {
            supportedLanguages = SupportedLanguages.GetSupportedLanguageInfoList();

            InitializeComponent();

            // https://stackoverflow.com/questions/17423348/how-can-i-wait-until-a-form-handle-has-been-created-before-using-its-components
            HandleCreated += MainForm_HandleCreated;
            if (IsHandleCreated) // Paranoia strikes deep
                MainForm_HandleCreated(null, new()); // Into your heart it will creep
        }

        private void MainForm_HandleCreated(object? sender, EventArgs e)
        {
            LocalizationHandler.SubscribeToLocalizationChanged(new(this.LocalizationChangeReported));
            WorldCupRepoBroker.OnAvailableFileDetailsChanged.SafeSubscribe(new(this.AvailableFileDetailsChangeReported));
            WorldCupRepoBroker.OnAPISourcesLoaded.SafeSubscribe(new(this.DownloadableFileDetailsChangeReported));

            AvailableFileDetailsChangeReported();
            DownloadableFileDetailsChangeReported();

            // Language logic
            var defaultLanguage = SupportedLanguages.GetDefaultSupportedLanguageInfo();

            foreach (var language in supportedLanguages)
            {
                languageSelectComboBox.Items.Add(language.name);

                if (language.culture.Name == defaultLanguage.culture.Name)
                {
                    languageSelectComboBox.SelectedIndex = languageSelectComboBox.Items.IndexOf(language.name);
                }
            }
        }

        public void NewCupDataTaskStarted(Task<IWorldCupDataRepo?> repo)
        {
            int currentActionID = ++loadingRepoActionID;
            Task.Run(() =>
            {
                repo.Wait();

                this.Invoke(() =>
                {
                    if (currentActionID != loadingRepoActionID)
                        return;

                    //TODO: display next tab page with data
                });
            });
        }

        public void AvailableFileDetailsChangeReported()
        {
            // This func is intended to be called from a thread pool context, so we have to take extra care
            this.Invoke(() =>
            {
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

                        dataSelectLocalSourcesPanel.SuspendLayout();
                        dataSelectLocalSourcesPanel.Controls.Clear();

                        foreach (var deet in deets.Where((deet) => deet != null))
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
                            // TODO
                            //RFDS.OnDownloadButtonPressed += this.NewCupDataTaskStarted;

                            RFDS.BorderStyle = BorderStyle.FixedSingle;
                            dataSelectRemoteSourcesPanel.Controls.Add(RFDS);
                            RFDS.Dock = DockStyle.Top;
                        }

                        dataSelectRemoteSourcesPanel.ResumeLayout();
                    });
                });

            });
        }

        private void languageSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == null || sender is not ComboBox targetComboBox ||
                targetComboBox.SelectedItem == null || targetComboBox.SelectedItem is not String str)
                return;

            LocalizationHandler.SetCulture(supportedLanguages.First((thing) => thing.name == str).culture);
        }

        public void LocalizationChangeReported()
        {
            LocalizationHandler.LocalizeAllChildren(dataSelectTab);

            dataSelectTab.Text =
                LocalizationHandler.GetCurrentLocOptionsString(
                    LocalizationOptions.Data_Select_Slash_Config);
        }
    }
}
