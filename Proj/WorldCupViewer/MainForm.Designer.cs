using WorldCupViewer.UserControls;

namespace WorldCupViewer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStrip = new StatusStrip();
            MTSSLFailedToLoadSettings = new MultilingualToolStripStatusLabel();
            MTSSLFailedToSaveSettings = new MultilingualToolStripStatusLabel();
            MTSSLFailedToLoadData = new MultilingualToolStripStatusLabel();
            MTSSLGUIDError = new MultilingualToolStripStatusLabel();
            MTSSLDownloadFailed = new MultilingualToolStripStatusLabel();
            MTSSLDownloadState = new MultilingualToolStripStatusLabel();
            MTSSLDownloadStateState = new MultilingualToolStripStatusLabel();
            MTSSLDataIsLoading = new MultilingualToolStripStatusLabel();
            mainTabControl = new TabControl();
            dataSelectTab = new TabPage();
            dataSelectBotTLP = new TableLayoutPanel();
            localDataSourcesGroupBox = new MultilingualGroupBox();
            dataSelectLocalSourcesPanel = new Panel();
            remoteDataSourcesGroupBox = new MultilingualGroupBox();
            dataSelectRemoteSourcesPanel = new Panel();
            dataSelectTopTLP = new TableLayoutPanel();
            languageSelectTLP = new TableLayoutPanel();
            languageSelectLabel = new Label();
            languageSelectComboBox = new ComboBox();
            label1 = new MultilingualLabel();
            teamAndPlayerSelectTab = new TabPage();
            panel3 = new Panel();
            tlpTeamPlayerSelectTop = new TableLayoutPanel();
            panel2 = new Panel();
            SelectedCupDataYearLabel = new Label();
            SelectedCupDataNameLabel = new Label();
            multilingualLabel4 = new MultilingualLabel();
            multilingualLabel2 = new MultilingualLabel();
            SelectedTeamComboBox = new ComboBox();
            multilingualLabel1 = new MultilingualLabel();
            SelectedCupDataPictureBox = new ExternalImage();
            multilingualGroupBox3 = new MultilingualGroupBox();
            panel6 = new Panel();
            multilingualGroupBox1 = new MultilingualGroupBox();
            panel4 = new Panel();
            panel1 = new Panel();
            multilingualGroupBox2 = new MultilingualGroupBox();
            panel5 = new Panel();
            statusStrip.SuspendLayout();
            mainTabControl.SuspendLayout();
            dataSelectTab.SuspendLayout();
            dataSelectBotTLP.SuspendLayout();
            localDataSourcesGroupBox.SuspendLayout();
            remoteDataSourcesGroupBox.SuspendLayout();
            dataSelectTopTLP.SuspendLayout();
            languageSelectTLP.SuspendLayout();
            teamAndPlayerSelectTab.SuspendLayout();
            panel3.SuspendLayout();
            tlpTeamPlayerSelectTop.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SelectedCupDataPictureBox).BeginInit();
            multilingualGroupBox3.SuspendLayout();
            multilingualGroupBox1.SuspendLayout();
            panel1.SuspendLayout();
            multilingualGroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { MTSSLFailedToLoadSettings, MTSSLFailedToSaveSettings, MTSSLFailedToLoadData, MTSSLGUIDError, MTSSLDownloadFailed, MTSSLDownloadState, MTSSLDownloadStateState, MTSSLDataIsLoading });
            statusStrip.Location = new Point(0, 489);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(784, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // MTSSLFailedToLoadSettings
            // 
            MTSSLFailedToLoadSettings.CharacterCasing = CharacterCasing.Normal;
            MTSSLFailedToLoadSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            MTSSLFailedToLoadSettings.ForeColor = Color.Red;
            MTSSLFailedToLoadSettings.Localization = Localization.LocalizationOptions.Failed_To_Load_Settings_Data_Will_Not_Be_Saved;
            MTSSLFailedToLoadSettings.Name = "MTSSLFailedToLoadSettings";
            MTSSLFailedToLoadSettings.PreceedingText = "   ";
            MTSSLFailedToLoadSettings.Size = new Size(317, 17);
            MTSSLFailedToLoadSettings.SucceedingText = "";
            MTSSLFailedToLoadSettings.Visible = false;
            // 
            // MTSSLFailedToSaveSettings
            // 
            MTSSLFailedToSaveSettings.CharacterCasing = CharacterCasing.Normal;
            MTSSLFailedToSaveSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            MTSSLFailedToSaveSettings.ForeColor = Color.Red;
            MTSSLFailedToSaveSettings.Localization = Localization.LocalizationOptions.Failed_To_Save_Settings;
            MTSSLFailedToSaveSettings.Name = "MTSSLFailedToSaveSettings";
            MTSSLFailedToSaveSettings.PreceedingText = "   ";
            MTSSLFailedToSaveSettings.Size = new Size(176, 17);
            MTSSLFailedToSaveSettings.SucceedingText = "";
            MTSSLFailedToSaveSettings.Visible = false;
            // 
            // MTSSLFailedToLoadData
            // 
            MTSSLFailedToLoadData.ActiveLinkColor = Color.RosyBrown;
            MTSSLFailedToLoadData.CharacterCasing = CharacterCasing.Normal;
            MTSSLFailedToLoadData.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            MTSSLFailedToLoadData.ForeColor = Color.Red;
            MTSSLFailedToLoadData.Localization = Localization.LocalizationOptions.Failed_to_Load_Data;
            MTSSLFailedToLoadData.Name = "MTSSLFailedToLoadData";
            MTSSLFailedToLoadData.PreceedingText = "   ";
            MTSSLFailedToLoadData.Size = new Size(154, 17);
            MTSSLFailedToLoadData.SucceedingText = "";
            MTSSLFailedToLoadData.Visible = false;
            // 
            // MTSSLGUIDError
            // 
            MTSSLGUIDError.CharacterCasing = CharacterCasing.Normal;
            MTSSLGUIDError.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            MTSSLGUIDError.ForeColor = Color.Red;
            MTSSLGUIDError.Localization = Localization.LocalizationOptions.Guid_Error_Data_Might_Not_Be_Saved;
            MTSSLGUIDError.Name = "MTSSLGUIDError";
            MTSSLGUIDError.PreceedingText = "   ";
            MTSSLGUIDError.Size = new Size(256, 17);
            MTSSLGUIDError.SucceedingText = "";
            MTSSLGUIDError.Visible = false;
            // 
            // MTSSLDownloadFailed
            // 
            MTSSLDownloadFailed.CharacterCasing = CharacterCasing.Normal;
            MTSSLDownloadFailed.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            MTSSLDownloadFailed.ForeColor = Color.Red;
            MTSSLDownloadFailed.Localization = Localization.LocalizationOptions.Download_Failed;
            MTSSLDownloadFailed.Name = "MTSSLDownloadFailed";
            MTSSLDownloadFailed.PreceedingText = "   ";
            MTSSLDownloadFailed.Size = new Size(136, 17);
            MTSSLDownloadFailed.SucceedingText = "";
            MTSSLDownloadFailed.Visible = false;
            // 
            // MTSSLDownloadState
            // 
            MTSSLDownloadState.CharacterCasing = CharacterCasing.Normal;
            MTSSLDownloadState.Localization = Localization.LocalizationOptions.Download_State;
            MTSSLDownloadState.Name = "MTSSLDownloadState";
            MTSSLDownloadState.PreceedingText = "   ";
            MTSSLDownloadState.Size = new Size(132, 17);
            MTSSLDownloadState.SucceedingText = ":";
            // 
            // MTSSLDownloadStateState
            // 
            MTSSLDownloadStateState.CharacterCasing = CharacterCasing.Upper;
            MTSSLDownloadStateState.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            MTSSLDownloadStateState.Localization = Localization.LocalizationOptions.Ready;
            MTSSLDownloadStateState.Name = "MTSSLDownloadStateState";
            MTSSLDownloadStateState.PreceedingText = "";
            MTSSLDownloadStateState.Size = new Size(72, 17);
            MTSSLDownloadStateState.SucceedingText = "";
            // 
            // MTSSLDataIsLoading
            // 
            MTSSLDataIsLoading.CharacterCasing = CharacterCasing.Normal;
            MTSSLDataIsLoading.Localization = Localization.LocalizationOptions.Data_is_Loading;
            MTSSLDataIsLoading.Name = "MTSSLDataIsLoading";
            MTSSLDataIsLoading.PreceedingText = "   ";
            MTSSLDataIsLoading.Size = new Size(138, 17);
            MTSSLDataIsLoading.SucceedingText = "...";
            MTSSLDataIsLoading.Visible = false;
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(dataSelectTab);
            mainTabControl.Controls.Add(teamAndPlayerSelectTab);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(0, 0);
            mainTabControl.Margin = new Padding(0);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.Padding = new Point(0, 0);
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(784, 489);
            mainTabControl.TabIndex = 1;
            // 
            // dataSelectTab
            // 
            dataSelectTab.Controls.Add(dataSelectBotTLP);
            dataSelectTab.Controls.Add(dataSelectTopTLP);
            dataSelectTab.Location = new Point(4, 24);
            dataSelectTab.Margin = new Padding(0);
            dataSelectTab.Name = "dataSelectTab";
            dataSelectTab.Size = new Size(776, 461);
            dataSelectTab.TabIndex = 0;
            dataSelectTab.Text = "Data Select Slash Config";
            dataSelectTab.UseVisualStyleBackColor = true;
            // 
            // dataSelectBotTLP
            // 
            dataSelectBotTLP.ColumnCount = 3;
            dataSelectBotTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            dataSelectBotTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 6F));
            dataSelectBotTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            dataSelectBotTLP.Controls.Add(localDataSourcesGroupBox, 2, 0);
            dataSelectBotTLP.Controls.Add(remoteDataSourcesGroupBox, 0, 0);
            dataSelectBotTLP.Dock = DockStyle.Fill;
            dataSelectBotTLP.Location = new Point(0, 32);
            dataSelectBotTLP.Name = "dataSelectBotTLP";
            dataSelectBotTLP.RowCount = 1;
            dataSelectBotTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dataSelectBotTLP.Size = new Size(776, 429);
            dataSelectBotTLP.TabIndex = 12;
            // 
            // localDataSourcesGroupBox
            // 
            localDataSourcesGroupBox.CharacterCasing = CharacterCasing.Normal;
            localDataSourcesGroupBox.Controls.Add(dataSelectLocalSourcesPanel);
            localDataSourcesGroupBox.Dock = DockStyle.Fill;
            localDataSourcesGroupBox.Localization = Localization.LocalizationOptions.Local;
            localDataSourcesGroupBox.Location = new Point(315, 1);
            localDataSourcesGroupBox.Margin = new Padding(1);
            localDataSourcesGroupBox.Name = "localDataSourcesGroupBox";
            localDataSourcesGroupBox.PreceedingText = "";
            localDataSourcesGroupBox.Size = new Size(460, 427);
            localDataSourcesGroupBox.SucceedingText = "";
            localDataSourcesGroupBox.TabIndex = 9;
            localDataSourcesGroupBox.TabStop = false;
            // 
            // dataSelectLocalSourcesPanel
            // 
            dataSelectLocalSourcesPanel.AutoScroll = true;
            dataSelectLocalSourcesPanel.BackColor = Color.White;
            dataSelectLocalSourcesPanel.Dock = DockStyle.Fill;
            dataSelectLocalSourcesPanel.Location = new Point(3, 19);
            dataSelectLocalSourcesPanel.Name = "dataSelectLocalSourcesPanel";
            dataSelectLocalSourcesPanel.Padding = new Padding(3);
            dataSelectLocalSourcesPanel.Size = new Size(454, 405);
            dataSelectLocalSourcesPanel.TabIndex = 0;
            // 
            // remoteDataSourcesGroupBox
            // 
            remoteDataSourcesGroupBox.CharacterCasing = CharacterCasing.Normal;
            remoteDataSourcesGroupBox.Controls.Add(dataSelectRemoteSourcesPanel);
            remoteDataSourcesGroupBox.Dock = DockStyle.Fill;
            remoteDataSourcesGroupBox.Localization = Localization.LocalizationOptions.Remote;
            remoteDataSourcesGroupBox.Location = new Point(1, 1);
            remoteDataSourcesGroupBox.Margin = new Padding(1);
            remoteDataSourcesGroupBox.Name = "remoteDataSourcesGroupBox";
            remoteDataSourcesGroupBox.PreceedingText = "";
            remoteDataSourcesGroupBox.Size = new Size(306, 427);
            remoteDataSourcesGroupBox.SucceedingText = "";
            remoteDataSourcesGroupBox.TabIndex = 8;
            remoteDataSourcesGroupBox.TabStop = false;
            // 
            // dataSelectRemoteSourcesPanel
            // 
            dataSelectRemoteSourcesPanel.AutoScroll = true;
            dataSelectRemoteSourcesPanel.BackColor = Color.White;
            dataSelectRemoteSourcesPanel.Dock = DockStyle.Fill;
            dataSelectRemoteSourcesPanel.Location = new Point(3, 19);
            dataSelectRemoteSourcesPanel.Name = "dataSelectRemoteSourcesPanel";
            dataSelectRemoteSourcesPanel.Padding = new Padding(3);
            dataSelectRemoteSourcesPanel.Size = new Size(300, 405);
            dataSelectRemoteSourcesPanel.TabIndex = 1;
            // 
            // dataSelectTopTLP
            // 
            dataSelectTopTLP.ColumnCount = 3;
            dataSelectTopTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            dataSelectTopTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            dataSelectTopTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            dataSelectTopTLP.Controls.Add(languageSelectTLP, 0, 0);
            dataSelectTopTLP.Controls.Add(label1, 1, 0);
            dataSelectTopTLP.Dock = DockStyle.Top;
            dataSelectTopTLP.Location = new Point(0, 0);
            dataSelectTopTLP.Name = "dataSelectTopTLP";
            dataSelectTopTLP.RowCount = 1;
            dataSelectTopTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dataSelectTopTLP.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            dataSelectTopTLP.Size = new Size(776, 32);
            dataSelectTopTLP.TabIndex = 11;
            // 
            // languageSelectTLP
            // 
            languageSelectTLP.ColumnCount = 4;
            languageSelectTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            languageSelectTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            languageSelectTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            languageSelectTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            languageSelectTLP.Controls.Add(languageSelectLabel, 1, 0);
            languageSelectTLP.Controls.Add(languageSelectComboBox, 2, 0);
            languageSelectTLP.Dock = DockStyle.Fill;
            languageSelectTLP.Location = new Point(0, 0);
            languageSelectTLP.Margin = new Padding(0);
            languageSelectTLP.Name = "languageSelectTLP";
            languageSelectTLP.RowCount = 1;
            languageSelectTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            languageSelectTLP.Size = new Size(258, 32);
            languageSelectTLP.TabIndex = 9;
            // 
            // languageSelectLabel
            // 
            languageSelectLabel.Anchor = AnchorStyles.Right;
            languageSelectLabel.AutoSize = true;
            languageSelectLabel.Location = new Point(29, 8);
            languageSelectLabel.Name = "languageSelectLabel";
            languageSelectLabel.Size = new Size(97, 15);
            languageSelectLabel.TabIndex = 0;
            languageSelectLabel.Text = "Language / Jezik:";
            languageSelectLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // languageSelectComboBox
            // 
            languageSelectComboBox.Anchor = AnchorStyles.Left;
            languageSelectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            languageSelectComboBox.FormattingEnabled = true;
            languageSelectComboBox.Location = new Point(132, 4);
            languageSelectComboBox.Name = "languageSelectComboBox";
            languageSelectComboBox.Size = new Size(94, 23);
            languageSelectComboBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.CharacterCasing = CharacterCasing.Normal;
            label1.Dock = DockStyle.Fill;
            label1.Localization = Localization.LocalizationOptions.DataSources;
            label1.Location = new Point(261, 0);
            label1.Name = "label1";
            label1.PreceedingText = "";
            label1.Size = new Size(252, 32);
            label1.SucceedingText = "";
            label1.TabIndex = 9;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // teamAndPlayerSelectTab
            // 
            teamAndPlayerSelectTab.Controls.Add(panel3);
            teamAndPlayerSelectTab.Controls.Add(multilingualGroupBox1);
            teamAndPlayerSelectTab.Location = new Point(4, 24);
            teamAndPlayerSelectTab.Name = "teamAndPlayerSelectTab";
            teamAndPlayerSelectTab.Padding = new Padding(3);
            teamAndPlayerSelectTab.Size = new Size(776, 461);
            teamAndPlayerSelectTab.TabIndex = 1;
            teamAndPlayerSelectTab.Text = "Team Slash Player Select";
            teamAndPlayerSelectTab.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel3.Controls.Add(tlpTeamPlayerSelectTop);
            panel3.Controls.Add(multilingualGroupBox3);
            panel3.Location = new Point(5, 6);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(300, 455);
            panel3.TabIndex = 8;
            // 
            // tlpTeamPlayerSelectTop
            // 
            tlpTeamPlayerSelectTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tlpTeamPlayerSelectTop.ColumnCount = 2;
            tlpTeamPlayerSelectTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpTeamPlayerSelectTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTeamPlayerSelectTop.Controls.Add(panel2, 1, 0);
            tlpTeamPlayerSelectTop.Controls.Add(SelectedCupDataPictureBox, 0, 0);
            tlpTeamPlayerSelectTop.Location = new Point(0, 0);
            tlpTeamPlayerSelectTop.Name = "tlpTeamPlayerSelectTop";
            tlpTeamPlayerSelectTop.RowCount = 1;
            tlpTeamPlayerSelectTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpTeamPlayerSelectTop.Size = new Size(300, 148);
            tlpTeamPlayerSelectTop.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(SelectedCupDataYearLabel);
            panel2.Controls.Add(SelectedCupDataNameLabel);
            panel2.Controls.Add(multilingualLabel4);
            panel2.Controls.Add(multilingualLabel2);
            panel2.Controls.Add(SelectedTeamComboBox);
            panel2.Controls.Add(multilingualLabel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(153, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(144, 142);
            panel2.TabIndex = 1;
            // 
            // SelectedCupDataYearLabel
            // 
            SelectedCupDataYearLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SelectedCupDataYearLabel.Location = new Point(3, 67);
            SelectedCupDataYearLabel.Name = "SelectedCupDataYearLabel";
            SelectedCupDataYearLabel.Size = new Size(138, 25);
            SelectedCupDataYearLabel.TabIndex = 5;
            SelectedCupDataYearLabel.Text = "YEAR GOES HERE";
            SelectedCupDataYearLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SelectedCupDataNameLabel
            // 
            SelectedCupDataNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SelectedCupDataNameLabel.Location = new Point(3, 21);
            SelectedCupDataNameLabel.Name = "SelectedCupDataNameLabel";
            SelectedCupDataNameLabel.Size = new Size(138, 25);
            SelectedCupDataNameLabel.TabIndex = 4;
            SelectedCupDataNameLabel.Text = "NAME GOES HERE";
            SelectedCupDataNameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // multilingualLabel4
            // 
            multilingualLabel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            multilingualLabel4.CharacterCasing = CharacterCasing.Normal;
            multilingualLabel4.Localization = Localization.LocalizationOptions.Year;
            multilingualLabel4.Location = new Point(3, 46);
            multilingualLabel4.Name = "multilingualLabel4";
            multilingualLabel4.Padding = new Padding(0, 6, 0, 0);
            multilingualLabel4.PreceedingText = "";
            multilingualLabel4.Size = new Size(138, 25);
            multilingualLabel4.SucceedingText = ":";
            multilingualLabel4.TabIndex = 4;
            multilingualLabel4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // multilingualLabel2
            // 
            multilingualLabel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            multilingualLabel2.CharacterCasing = CharacterCasing.Normal;
            multilingualLabel2.Localization = Localization.LocalizationOptions.Name;
            multilingualLabel2.Location = new Point(3, 0);
            multilingualLabel2.Name = "multilingualLabel2";
            multilingualLabel2.Padding = new Padding(0, 6, 0, 0);
            multilingualLabel2.PreceedingText = "";
            multilingualLabel2.Size = new Size(138, 25);
            multilingualLabel2.SucceedingText = ":";
            multilingualLabel2.TabIndex = 2;
            multilingualLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SelectedTeamComboBox
            // 
            SelectedTeamComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SelectedTeamComboBox.DisplayMember = "Key";
            SelectedTeamComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SelectedTeamComboBox.FormattingEnabled = true;
            SelectedTeamComboBox.Location = new Point(3, 116);
            SelectedTeamComboBox.Name = "SelectedTeamComboBox";
            SelectedTeamComboBox.Size = new Size(138, 23);
            SelectedTeamComboBox.TabIndex = 0;
            SelectedTeamComboBox.ValueMember = "Value";
            // 
            // multilingualLabel1
            // 
            multilingualLabel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            multilingualLabel1.CharacterCasing = CharacterCasing.Normal;
            multilingualLabel1.Localization = Localization.LocalizationOptions.Selected_Team;
            multilingualLabel1.Location = new Point(3, 92);
            multilingualLabel1.Name = "multilingualLabel1";
            multilingualLabel1.Padding = new Padding(0, 6, 0, 0);
            multilingualLabel1.PreceedingText = "";
            multilingualLabel1.Size = new Size(138, 25);
            multilingualLabel1.SucceedingText = ":";
            multilingualLabel1.TabIndex = 1;
            multilingualLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SelectedCupDataPictureBox
            // 
            SelectedCupDataPictureBox.BorderStyle = BorderStyle.FixedSingle;
            SelectedCupDataPictureBox.Dock = DockStyle.Fill;
            SelectedCupDataPictureBox.ExternalImageID = null;
            SelectedCupDataPictureBox.Location = new Point(3, 3);
            SelectedCupDataPictureBox.Name = "SelectedCupDataPictureBox";
            SelectedCupDataPictureBox.Size = new Size(144, 142);
            SelectedCupDataPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            SelectedCupDataPictureBox.TabIndex = 2;
            SelectedCupDataPictureBox.TabStop = false;
            // 
            // multilingualGroupBox3
            // 
            multilingualGroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            multilingualGroupBox3.CharacterCasing = CharacterCasing.Normal;
            multilingualGroupBox3.Controls.Add(panel6);
            multilingualGroupBox3.Localization = Localization.LocalizationOptions.Favourites;
            multilingualGroupBox3.Location = new Point(3, 154);
            multilingualGroupBox3.Name = "multilingualGroupBox3";
            multilingualGroupBox3.PreceedingText = "";
            multilingualGroupBox3.Size = new Size(297, 295);
            multilingualGroupBox3.SucceedingText = "";
            multilingualGroupBox3.TabIndex = 8;
            multilingualGroupBox3.TabStop = false;
            // 
            // panel6
            // 
            panel6.AutoScroll = true;
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(3, 19);
            panel6.Name = "panel6";
            panel6.Size = new Size(291, 273);
            panel6.TabIndex = 1;
            // 
            // multilingualGroupBox1
            // 
            multilingualGroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            multilingualGroupBox1.CharacterCasing = CharacterCasing.Normal;
            multilingualGroupBox1.Controls.Add(panel4);
            multilingualGroupBox1.Localization = Localization.LocalizationOptions.Player_List;
            multilingualGroupBox1.Location = new Point(311, 6);
            multilingualGroupBox1.Name = "multilingualGroupBox1";
            multilingualGroupBox1.PreceedingText = "";
            multilingualGroupBox1.Size = new Size(457, 449);
            multilingualGroupBox1.SucceedingText = "";
            multilingualGroupBox1.TabIndex = 7;
            multilingualGroupBox1.TabStop = false;
            // 
            // panel4
            // 
            panel4.AutoScroll = true;
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 19);
            panel4.Name = "panel4";
            panel4.Size = new Size(451, 427);
            panel4.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(multilingualGroupBox2);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(308, 455);
            panel1.TabIndex = 0;
            // 
            // multilingualGroupBox2
            // 
            multilingualGroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            multilingualGroupBox2.CharacterCasing = CharacterCasing.Normal;
            multilingualGroupBox2.Controls.Add(panel5);
            multilingualGroupBox2.Localization = Localization.LocalizationOptions.Favourites;
            multilingualGroupBox2.Location = new Point(3, 154);
            multilingualGroupBox2.Name = "multilingualGroupBox2";
            multilingualGroupBox2.PreceedingText = "";
            multilingualGroupBox2.Size = new Size(450, 653);
            multilingualGroupBox2.SucceedingText = "";
            multilingualGroupBox2.TabIndex = 8;
            multilingualGroupBox2.TabStop = false;
            // 
            // panel5
            // 
            panel5.AutoScroll = true;
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 19);
            panel5.Name = "panel5";
            panel5.Size = new Size(444, 631);
            panel5.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 511);
            Controls.Add(mainTabControl);
            Controls.Add(statusStrip);
            MinimumSize = new Size(650, 400);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WorldCupViewer";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            mainTabControl.ResumeLayout(false);
            dataSelectTab.ResumeLayout(false);
            dataSelectBotTLP.ResumeLayout(false);
            localDataSourcesGroupBox.ResumeLayout(false);
            remoteDataSourcesGroupBox.ResumeLayout(false);
            dataSelectTopTLP.ResumeLayout(false);
            languageSelectTLP.ResumeLayout(false);
            languageSelectTLP.PerformLayout();
            teamAndPlayerSelectTab.ResumeLayout(false);
            panel3.ResumeLayout(false);
            tlpTeamPlayerSelectTop.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SelectedCupDataPictureBox).EndInit();
            multilingualGroupBox3.ResumeLayout(false);
            multilingualGroupBox1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            multilingualGroupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private TabControl mainTabControl;
        private TabPage dataSelectTab;
        private TabPage teamAndPlayerSelectTab;
        private MultilingualLabel label1;
        private TableLayoutPanel dataSelectTopTLP;
        private TableLayoutPanel languageSelectTLP;
        private Label languageSelectLabel;
        private ComboBox languageSelectComboBox;
        private TableLayoutPanel dataSelectBotTLP;
        private MultilingualGroupBox localDataSourcesGroupBox;
        private Panel dataSelectLocalSourcesPanel;
        private MultilingualGroupBox remoteDataSourcesGroupBox;
        private Panel dataSelectRemoteSourcesPanel;
        private MultilingualToolStripStatusLabel MTSSLDownloadState;
        private MultilingualToolStripStatusLabel MTSSLDownloadStateState;
        private MultilingualToolStripStatusLabel MTSSLDataIsLoading;
        private MultilingualToolStripStatusLabel MTSSLDownloadFailed;
        private MultilingualToolStripStatusLabel MTSSLFailedToLoadData;
        private MultilingualToolStripStatusLabel MTSSLFailedToSaveSettings;
        private MultilingualToolStripStatusLabel MTSSLFailedToLoadSettings;
        private MultilingualToolStripStatusLabel MTSSLGUIDError;
        private MultilingualGroupBox multilingualGroupBox1;
        private Panel panel4;
        private Panel panel3;
        private MultilingualGroupBox multilingualGroupBox3;
        private Panel panel6;
        private Panel panel1;
        private MultilingualGroupBox multilingualGroupBox2;
        private Panel panel5;
        private TableLayoutPanel tlpTeamPlayerSelectTop;
        private Panel panel2;
        private Label SelectedCupDataYearLabel;
        private Label SelectedCupDataNameLabel;
        private MultilingualLabel multilingualLabel4;
        private MultilingualLabel multilingualLabel2;
        private ComboBox SelectedTeamComboBox;
        private MultilingualLabel multilingualLabel1;
        private ExternalImage SelectedCupDataPictureBox;
    }
}
