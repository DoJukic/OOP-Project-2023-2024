using System.ComponentModel;
using System.Windows.Forms;
using WorldCupLib;
using WorldCupViewer.Localization;
using WorldCupViewer.UserControls;

namespace WorldCupViewer
{
    public partial class MainForm : Form
    {
        public readonly String ENG = "English";
        public readonly String HRV = "Hrvatski";

        private int availableFileDetailsChangedActionID = 0;

        public MainForm()
        {
            this.DoubleBuffered = true;

            InitializeComponent();
            LocalizationHandler.SubscribeToLocalizationChanged(new(this.LocalizationChangeReported));
            WorldCupRepoBroker.OnAvailableFileDetailsChanged.SafeSubscribe(new(this.AvailableFileDetailsChangeReported));

            languageSelectComboBox.Items.Add(ENG);
            languageSelectComboBox.Items.Add(HRV);
            languageSelectComboBox.SelectedIndex = 0;
        }

        public void LocalizationChangeReported()
        {
            LocalizationHandler.LocalizeAllChildren(dataSelectTab);

            dataSelectTab.Text =
                LocalizationHandler.GetCurrentLocOptionsString(
                    LocalizationOptions.Data_Select_Slash_Config);
        }

        public void AvailableFileDetailsChangeReported()
        {
            // Be careful~
            _ = this.Invoke(async () =>
            {
                int currentAvailableFileDetailsChangedActionID = ++availableFileDetailsChangedActionID;

                var deets = await WorldCupRepoBroker.BeginGetAvailableFileDetails().ConfigureAwait(false);

                this.Invoke(() =>
                {
                    if (currentAvailableFileDetailsChangedActionID != availableFileDetailsChangedActionID)
                        return;

                    dataSelectLocalSourcesPanel.SuspendLayout();
                    dataSelectLocalSourcesPanel.Controls.Clear();
                    //localDataSourcesGroupBox.Controls.Clear();
                    //Panel newPanel = new();

                    foreach (var deet in deets.Where((deet) => deet != null))
                    {
                        SelectableFileDataSource_v2 SFDSV2 = new();
                        SFDSV2.LoadAvailableFileDetails(deet);

                        //newPanel.Controls.Add(SFDSV2);
                        dataSelectLocalSourcesPanel.Controls.Add(SFDSV2);
                        SFDSV2.Dock = DockStyle.Top;
                    }

                    dataSelectLocalSourcesPanel.ResumeLayout();

                    /*newPanel.AutoScroll = true;
                    newPanel.Width = localDataSourcesGroupBox.Width;
                    newPanel.Height = localDataSourcesGroupBox.Height;

                    localDataSourcesGroupBox.Controls.Add(newPanel);
                    newPanel.Dock = DockStyle.Fill;*/
                });
            });
        }

        private void languageSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == null || sender is not ComboBox targetComboBox ||
                targetComboBox.SelectedItem == null || targetComboBox.SelectedItem is not String str)
                return;

            if (str == ENG)
                LocalizationHandler.SetCulture(new(LocalizationHandler.CULTURE_EN));
            else if (str == HRV)
                LocalizationHandler.SetCulture(new(LocalizationHandler.CULTURE_HR));
        }
    }
}
