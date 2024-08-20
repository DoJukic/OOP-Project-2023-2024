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
            tpDataSelect = new TabPage();
            tlpDataSelectBot = new TableLayoutPanel();
            mgbLocalDataSources = new MultilingualGroupBox();
            pnlDataSelectLocalSources = new Panel();
            mgbRemoteDataSources = new MultilingualGroupBox();
            pnlDataSelectRemoteSources = new Panel();
            tlpDataSelectTop = new TableLayoutPanel();
            tlpLanguageSelect = new TableLayoutPanel();
            lblLanguageSelect = new Label();
            cblanguageSelect = new ComboBox();
            label1 = new MultilingualLabel();
            tpTeamAndPlayerSelect = new TabPage();
            panel3 = new Panel();
            tlpTeamDataAndSelect = new TableLayoutPanel();
            panel2 = new Panel();
            lblSelectedCupDataYear = new Label();
            lblSelectedCupDataName = new Label();
            multilingualLabel4 = new MultilingualLabel();
            multilingualLabel2 = new MultilingualLabel();
            cbSelectedTeam = new ComboBox();
            multilingualLabel1 = new MultilingualLabel();
            pbSelectedCupImage = new ExternalImage();
            mgbFavouritePlayers = new MultilingualGroupBox();
            pnlFavouritePlayers = new Panel();
            mgbPlayerList = new MultilingualGroupBox();
            pnlPlayerList = new Panel();
            panel1 = new Panel();
            multilingualGroupBox2 = new MultilingualGroupBox();
            panel5 = new Panel();
            statusStrip.SuspendLayout();
            mainTabControl.SuspendLayout();
            tpDataSelect.SuspendLayout();
            tlpDataSelectBot.SuspendLayout();
            mgbLocalDataSources.SuspendLayout();
            mgbRemoteDataSources.SuspendLayout();
            tlpDataSelectTop.SuspendLayout();
            tlpLanguageSelect.SuspendLayout();
            tpTeamAndPlayerSelect.SuspendLayout();
            panel3.SuspendLayout();
            tlpTeamDataAndSelect.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbSelectedCupImage).BeginInit();
            mgbFavouritePlayers.SuspendLayout();
            mgbPlayerList.SuspendLayout();
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
            mainTabControl.Controls.Add(tpDataSelect);
            mainTabControl.Controls.Add(tpTeamAndPlayerSelect);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(0, 0);
            mainTabControl.Margin = new Padding(0);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.Padding = new Point(0, 0);
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(784, 489);
            mainTabControl.TabIndex = 1;
            // 
            // tpDataSelect
            // 
            tpDataSelect.Controls.Add(tlpDataSelectBot);
            tpDataSelect.Controls.Add(tlpDataSelectTop);
            tpDataSelect.Location = new Point(4, 24);
            tpDataSelect.Margin = new Padding(0);
            tpDataSelect.Name = "tpDataSelect";
            tpDataSelect.Size = new Size(776, 461);
            tpDataSelect.TabIndex = 0;
            tpDataSelect.Text = "Data Select Slash Config";
            tpDataSelect.UseVisualStyleBackColor = true;
            // 
            // tlpDataSelectBot
            // 
            tlpDataSelectBot.ColumnCount = 3;
            tlpDataSelectBot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpDataSelectBot.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 6F));
            tlpDataSelectBot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tlpDataSelectBot.Controls.Add(mgbLocalDataSources, 2, 0);
            tlpDataSelectBot.Controls.Add(mgbRemoteDataSources, 0, 0);
            tlpDataSelectBot.Dock = DockStyle.Fill;
            tlpDataSelectBot.Location = new Point(0, 32);
            tlpDataSelectBot.Name = "tlpDataSelectBot";
            tlpDataSelectBot.RowCount = 1;
            tlpDataSelectBot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDataSelectBot.Size = new Size(776, 429);
            tlpDataSelectBot.TabIndex = 12;
            // 
            // mgbLocalDataSources
            // 
            mgbLocalDataSources.CharacterCasing = CharacterCasing.Normal;
            mgbLocalDataSources.Controls.Add(pnlDataSelectLocalSources);
            mgbLocalDataSources.Dock = DockStyle.Fill;
            mgbLocalDataSources.Localization = Localization.LocalizationOptions.Local;
            mgbLocalDataSources.Location = new Point(315, 1);
            mgbLocalDataSources.Margin = new Padding(1);
            mgbLocalDataSources.Name = "mgbLocalDataSources";
            mgbLocalDataSources.PreceedingText = "";
            mgbLocalDataSources.Size = new Size(460, 427);
            mgbLocalDataSources.SucceedingText = "";
            mgbLocalDataSources.TabIndex = 9;
            mgbLocalDataSources.TabStop = false;
            // 
            // pnlDataSelectLocalSources
            // 
            pnlDataSelectLocalSources.AutoScroll = true;
            pnlDataSelectLocalSources.BackColor = Color.White;
            pnlDataSelectLocalSources.Dock = DockStyle.Fill;
            pnlDataSelectLocalSources.Location = new Point(3, 19);
            pnlDataSelectLocalSources.Name = "pnlDataSelectLocalSources";
            pnlDataSelectLocalSources.Padding = new Padding(3);
            pnlDataSelectLocalSources.Size = new Size(454, 405);
            pnlDataSelectLocalSources.TabIndex = 0;
            // 
            // mgbRemoteDataSources
            // 
            mgbRemoteDataSources.CharacterCasing = CharacterCasing.Normal;
            mgbRemoteDataSources.Controls.Add(pnlDataSelectRemoteSources);
            mgbRemoteDataSources.Dock = DockStyle.Fill;
            mgbRemoteDataSources.Localization = Localization.LocalizationOptions.Remote;
            mgbRemoteDataSources.Location = new Point(1, 1);
            mgbRemoteDataSources.Margin = new Padding(1);
            mgbRemoteDataSources.Name = "mgbRemoteDataSources";
            mgbRemoteDataSources.PreceedingText = "";
            mgbRemoteDataSources.Size = new Size(306, 427);
            mgbRemoteDataSources.SucceedingText = "";
            mgbRemoteDataSources.TabIndex = 8;
            mgbRemoteDataSources.TabStop = false;
            // 
            // pnlDataSelectRemoteSources
            // 
            pnlDataSelectRemoteSources.AutoScroll = true;
            pnlDataSelectRemoteSources.BackColor = Color.White;
            pnlDataSelectRemoteSources.Dock = DockStyle.Fill;
            pnlDataSelectRemoteSources.Location = new Point(3, 19);
            pnlDataSelectRemoteSources.Name = "pnlDataSelectRemoteSources";
            pnlDataSelectRemoteSources.Padding = new Padding(3);
            pnlDataSelectRemoteSources.Size = new Size(300, 405);
            pnlDataSelectRemoteSources.TabIndex = 1;
            // 
            // tlpDataSelectTop
            // 
            tlpDataSelectTop.ColumnCount = 3;
            tlpDataSelectTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpDataSelectTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpDataSelectTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpDataSelectTop.Controls.Add(tlpLanguageSelect, 0, 0);
            tlpDataSelectTop.Controls.Add(label1, 1, 0);
            tlpDataSelectTop.Dock = DockStyle.Top;
            tlpDataSelectTop.Location = new Point(0, 0);
            tlpDataSelectTop.Name = "tlpDataSelectTop";
            tlpDataSelectTop.RowCount = 1;
            tlpDataSelectTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDataSelectTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpDataSelectTop.Size = new Size(776, 32);
            tlpDataSelectTop.TabIndex = 11;
            // 
            // tlpLanguageSelect
            // 
            tlpLanguageSelect.ColumnCount = 4;
            tlpLanguageSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLanguageSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpLanguageSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpLanguageSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLanguageSelect.Controls.Add(lblLanguageSelect, 1, 0);
            tlpLanguageSelect.Controls.Add(cblanguageSelect, 2, 0);
            tlpLanguageSelect.Dock = DockStyle.Fill;
            tlpLanguageSelect.Location = new Point(0, 0);
            tlpLanguageSelect.Margin = new Padding(0);
            tlpLanguageSelect.Name = "tlpLanguageSelect";
            tlpLanguageSelect.RowCount = 1;
            tlpLanguageSelect.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpLanguageSelect.Size = new Size(258, 32);
            tlpLanguageSelect.TabIndex = 9;
            // 
            // lblLanguageSelect
            // 
            lblLanguageSelect.Anchor = AnchorStyles.Right;
            lblLanguageSelect.AutoSize = true;
            lblLanguageSelect.Location = new Point(29, 8);
            lblLanguageSelect.Name = "lblLanguageSelect";
            lblLanguageSelect.Size = new Size(97, 15);
            lblLanguageSelect.TabIndex = 0;
            lblLanguageSelect.Text = "Language / Jezik:";
            lblLanguageSelect.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cblanguageSelect
            // 
            cblanguageSelect.Anchor = AnchorStyles.Left;
            cblanguageSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cblanguageSelect.FormattingEnabled = true;
            cblanguageSelect.Location = new Point(132, 4);
            cblanguageSelect.Name = "cblanguageSelect";
            cblanguageSelect.Size = new Size(94, 23);
            cblanguageSelect.TabIndex = 1;
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
            // tpTeamAndPlayerSelect
            // 
            tpTeamAndPlayerSelect.Controls.Add(panel3);
            tpTeamAndPlayerSelect.Controls.Add(mgbPlayerList);
            tpTeamAndPlayerSelect.Location = new Point(4, 24);
            tpTeamAndPlayerSelect.Name = "tpTeamAndPlayerSelect";
            tpTeamAndPlayerSelect.Padding = new Padding(3);
            tpTeamAndPlayerSelect.Size = new Size(776, 461);
            tpTeamAndPlayerSelect.TabIndex = 1;
            tpTeamAndPlayerSelect.Text = "Team Slash Player Select";
            tpTeamAndPlayerSelect.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel3.Controls.Add(tlpTeamDataAndSelect);
            panel3.Controls.Add(mgbFavouritePlayers);
            panel3.Location = new Point(5, 6);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(300, 455);
            panel3.TabIndex = 8;
            // 
            // tlpTeamDataAndSelect
            // 
            tlpTeamDataAndSelect.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tlpTeamDataAndSelect.ColumnCount = 2;
            tlpTeamDataAndSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpTeamDataAndSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTeamDataAndSelect.Controls.Add(panel2, 1, 0);
            tlpTeamDataAndSelect.Controls.Add(pbSelectedCupImage, 0, 0);
            tlpTeamDataAndSelect.Location = new Point(0, 0);
            tlpTeamDataAndSelect.Name = "tlpTeamDataAndSelect";
            tlpTeamDataAndSelect.RowCount = 1;
            tlpTeamDataAndSelect.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpTeamDataAndSelect.Size = new Size(300, 148);
            tlpTeamDataAndSelect.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblSelectedCupDataYear);
            panel2.Controls.Add(lblSelectedCupDataName);
            panel2.Controls.Add(multilingualLabel4);
            panel2.Controls.Add(multilingualLabel2);
            panel2.Controls.Add(cbSelectedTeam);
            panel2.Controls.Add(multilingualLabel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(153, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(144, 142);
            panel2.TabIndex = 1;
            // 
            // lblSelectedCupDataYear
            // 
            lblSelectedCupDataYear.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSelectedCupDataYear.Location = new Point(3, 67);
            lblSelectedCupDataYear.Name = "lblSelectedCupDataYear";
            lblSelectedCupDataYear.Size = new Size(138, 25);
            lblSelectedCupDataYear.TabIndex = 5;
            lblSelectedCupDataYear.Text = "YEAR GOES HERE";
            lblSelectedCupDataYear.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSelectedCupDataName
            // 
            lblSelectedCupDataName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSelectedCupDataName.Location = new Point(3, 21);
            lblSelectedCupDataName.Name = "lblSelectedCupDataName";
            lblSelectedCupDataName.Size = new Size(138, 25);
            lblSelectedCupDataName.TabIndex = 4;
            lblSelectedCupDataName.Text = "NAME GOES HERE";
            lblSelectedCupDataName.TextAlign = ContentAlignment.MiddleLeft;
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
            // cbSelectedTeam
            // 
            cbSelectedTeam.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbSelectedTeam.DisplayMember = "Key";
            cbSelectedTeam.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelectedTeam.FormattingEnabled = true;
            cbSelectedTeam.Location = new Point(3, 116);
            cbSelectedTeam.Name = "cbSelectedTeam";
            cbSelectedTeam.Size = new Size(138, 23);
            cbSelectedTeam.TabIndex = 0;
            cbSelectedTeam.ValueMember = "Value";
            cbSelectedTeam.SelectedIndexChanged += SelectedTeamChanged;
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
            // pbSelectedCupImage
            // 
            pbSelectedCupImage.BorderStyle = BorderStyle.FixedSingle;
            pbSelectedCupImage.Dock = DockStyle.Fill;
            pbSelectedCupImage.ExternalImageID = null;
            pbSelectedCupImage.Location = new Point(3, 3);
            pbSelectedCupImage.Name = "pbSelectedCupImage";
            pbSelectedCupImage.Size = new Size(144, 142);
            pbSelectedCupImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbSelectedCupImage.TabIndex = 2;
            pbSelectedCupImage.TabStop = false;
            // 
            // mgbFavouritePlayers
            // 
            mgbFavouritePlayers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mgbFavouritePlayers.CharacterCasing = CharacterCasing.Normal;
            mgbFavouritePlayers.Controls.Add(pnlFavouritePlayers);
            mgbFavouritePlayers.Localization = Localization.LocalizationOptions.Favourites;
            mgbFavouritePlayers.Location = new Point(3, 154);
            mgbFavouritePlayers.Name = "mgbFavouritePlayers";
            mgbFavouritePlayers.PreceedingText = "";
            mgbFavouritePlayers.Size = new Size(297, 295);
            mgbFavouritePlayers.SucceedingText = "";
            mgbFavouritePlayers.TabIndex = 8;
            mgbFavouritePlayers.TabStop = false;
            // 
            // pnlFavouritePlayers
            // 
            pnlFavouritePlayers.AutoScroll = true;
            pnlFavouritePlayers.Dock = DockStyle.Fill;
            pnlFavouritePlayers.Location = new Point(3, 19);
            pnlFavouritePlayers.Name = "pnlFavouritePlayers";
            pnlFavouritePlayers.Size = new Size(291, 273);
            pnlFavouritePlayers.TabIndex = 1;
            // 
            // mgbPlayerList
            // 
            mgbPlayerList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mgbPlayerList.CharacterCasing = CharacterCasing.Normal;
            mgbPlayerList.Controls.Add(pnlPlayerList);
            mgbPlayerList.Localization = Localization.LocalizationOptions.Player_List;
            mgbPlayerList.Location = new Point(311, 6);
            mgbPlayerList.Name = "mgbPlayerList";
            mgbPlayerList.PreceedingText = "";
            mgbPlayerList.Size = new Size(457, 449);
            mgbPlayerList.SucceedingText = "";
            mgbPlayerList.TabIndex = 7;
            mgbPlayerList.TabStop = false;
            // 
            // pnlPlayerList
            // 
            pnlPlayerList.AutoScroll = true;
            pnlPlayerList.Dock = DockStyle.Fill;
            pnlPlayerList.Location = new Point(3, 19);
            pnlPlayerList.Name = "pnlPlayerList";
            pnlPlayerList.Size = new Size(451, 427);
            pnlPlayerList.TabIndex = 1;
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
            tpDataSelect.ResumeLayout(false);
            tlpDataSelectBot.ResumeLayout(false);
            mgbLocalDataSources.ResumeLayout(false);
            mgbRemoteDataSources.ResumeLayout(false);
            tlpDataSelectTop.ResumeLayout(false);
            tlpLanguageSelect.ResumeLayout(false);
            tlpLanguageSelect.PerformLayout();
            tpTeamAndPlayerSelect.ResumeLayout(false);
            panel3.ResumeLayout(false);
            tlpTeamDataAndSelect.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbSelectedCupImage).EndInit();
            mgbFavouritePlayers.ResumeLayout(false);
            mgbPlayerList.ResumeLayout(false);
            panel1.ResumeLayout(false);
            multilingualGroupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private TabControl mainTabControl;
        private TabPage tpDataSelect;
        private TabPage tpTeamAndPlayerSelect;
        private MultilingualLabel label1;
        private TableLayoutPanel tlpDataSelectTop;
        private TableLayoutPanel tlpLanguageSelect;
        private Label lblLanguageSelect;
        private ComboBox cblanguageSelect;
        private TableLayoutPanel tlpDataSelectBot;
        private MultilingualGroupBox mgbLocalDataSources;
        private Panel pnlDataSelectLocalSources;
        private MultilingualGroupBox mgbRemoteDataSources;
        private Panel pnlDataSelectRemoteSources;
        private MultilingualToolStripStatusLabel MTSSLDownloadState;
        private MultilingualToolStripStatusLabel MTSSLDownloadStateState;
        private MultilingualToolStripStatusLabel MTSSLDataIsLoading;
        private MultilingualToolStripStatusLabel MTSSLDownloadFailed;
        private MultilingualToolStripStatusLabel MTSSLFailedToLoadData;
        private MultilingualToolStripStatusLabel MTSSLFailedToSaveSettings;
        private MultilingualToolStripStatusLabel MTSSLFailedToLoadSettings;
        private MultilingualToolStripStatusLabel MTSSLGUIDError;
        private MultilingualGroupBox mgbPlayerList;
        private Panel pnlPlayerList;
        private Panel panel3;
        private MultilingualGroupBox mgbFavouritePlayers;
        private Panel pnlFavouritePlayers;
        private Panel panel1;
        private MultilingualGroupBox multilingualGroupBox2;
        private Panel panel5;
        private TableLayoutPanel tlpTeamDataAndSelect;
        private Panel panel2;
        private Label lblSelectedCupDataYear;
        private Label lblSelectedCupDataName;
        private MultilingualLabel multilingualLabel4;
        private MultilingualLabel multilingualLabel2;
        private ComboBox cbSelectedTeam;
        private MultilingualLabel multilingualLabel1;
        private ExternalImage pbSelectedCupImage;
    }
}
