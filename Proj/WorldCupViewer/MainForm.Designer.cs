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
            mainTabControl = new TabControl();
            dataSelectTab = new TabPage();
            dataSelectTableLayout = new TableLayoutPanel();
            dataSelectRightPanel = new Panel();
            panel5 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            localDataSourcesGroupBox = new MultilingualGroupBox();
            dataSelectLocalSourcesPanel = new Panel();
            remoteDataSourcesGroupBox = new MultilingualGroupBox();
            label1 = new MultilingualLabel();
            dataSelectLeftPanel = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel2 = new Panel();
            panel1 = new Panel();
            languageTableLayoutPanel = new TableLayoutPanel();
            languageSelectLabel = new Label();
            languageSelectComboBox = new ComboBox();
            tabPage2 = new TabPage();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            dataSelectRemoteSourcesPanel = new Panel();
            mainTabControl.SuspendLayout();
            dataSelectTab.SuspendLayout();
            dataSelectTableLayout.SuspendLayout();
            dataSelectRightPanel.SuspendLayout();
            panel5.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            localDataSourcesGroupBox.SuspendLayout();
            remoteDataSourcesGroupBox.SuspendLayout();
            dataSelectLeftPanel.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            languageTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Location = new Point(0, 489);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(784, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(dataSelectTab);
            mainTabControl.Controls.Add(tabPage2);
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
            dataSelectTab.Controls.Add(dataSelectTableLayout);
            dataSelectTab.Location = new Point(4, 24);
            dataSelectTab.Margin = new Padding(0);
            dataSelectTab.Name = "dataSelectTab";
            dataSelectTab.Size = new Size(776, 461);
            dataSelectTab.TabIndex = 0;
            dataSelectTab.Text = "Data Select Slash Config";
            dataSelectTab.UseVisualStyleBackColor = true;
            // 
            // dataSelectTableLayout
            // 
            dataSelectTableLayout.ColumnCount = 3;
            dataSelectTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            dataSelectTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 3F));
            dataSelectTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            dataSelectTableLayout.Controls.Add(dataSelectRightPanel, 2, 0);
            dataSelectTableLayout.Controls.Add(dataSelectLeftPanel, 0, 0);
            dataSelectTableLayout.Dock = DockStyle.Fill;
            dataSelectTableLayout.Location = new Point(0, 0);
            dataSelectTableLayout.Margin = new Padding(0);
            dataSelectTableLayout.Name = "dataSelectTableLayout";
            dataSelectTableLayout.RowCount = 1;
            dataSelectTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dataSelectTableLayout.Size = new Size(776, 461);
            dataSelectTableLayout.TabIndex = 0;
            // 
            // dataSelectRightPanel
            // 
            dataSelectRightPanel.Controls.Add(panel5);
            dataSelectRightPanel.Dock = DockStyle.Fill;
            dataSelectRightPanel.Location = new Point(312, 0);
            dataSelectRightPanel.Margin = new Padding(0);
            dataSelectRightPanel.Name = "dataSelectRightPanel";
            dataSelectRightPanel.Padding = new Padding(3);
            dataSelectRightPanel.Size = new Size(464, 461);
            dataSelectRightPanel.TabIndex = 5;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(tableLayoutPanel1);
            panel5.Controls.Add(label1);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 3);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(3);
            panel5.Size = new Size(458, 455);
            panel5.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(localDataSourcesGroupBox, 0, 1);
            tableLayoutPanel1.Controls.Add(remoteDataSourcesGroupBox, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 26);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 67F));
            tableLayoutPanel1.Size = new Size(450, 424);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // localDataSourcesGroupBox
            // 
            localDataSourcesGroupBox.CharacterCasing = CharacterCasing.Normal;
            localDataSourcesGroupBox.Controls.Add(dataSelectLocalSourcesPanel);
            localDataSourcesGroupBox.Dock = DockStyle.Fill;
            localDataSourcesGroupBox.Localization = Localization.LocalizationOptions.Local;
            localDataSourcesGroupBox.Location = new Point(1, 140);
            localDataSourcesGroupBox.Margin = new Padding(1);
            localDataSourcesGroupBox.Name = "localDataSourcesGroupBox";
            localDataSourcesGroupBox.PreceedingText = "";
            localDataSourcesGroupBox.Size = new Size(448, 283);
            localDataSourcesGroupBox.SucceedingText = "";
            localDataSourcesGroupBox.TabIndex = 3;
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
            dataSelectLocalSourcesPanel.Size = new Size(442, 261);
            dataSelectLocalSourcesPanel.TabIndex = 0;
            // 
            // remoteDataSourcesGroupBox
            // 
            remoteDataSourcesGroupBox.CharacterCasing = CharacterCasing.Normal;
            remoteDataSourcesGroupBox.Controls.Add(dataSelectRemoteSourcesPanel);
            remoteDataSourcesGroupBox.Dock = DockStyle.Fill;
            remoteDataSourcesGroupBox.Localization = Localization.LocalizationOptions.Remote;
            remoteDataSourcesGroupBox.Location = new Point(3, 3);
            remoteDataSourcesGroupBox.Name = "remoteDataSourcesGroupBox";
            remoteDataSourcesGroupBox.PreceedingText = "";
            remoteDataSourcesGroupBox.Size = new Size(444, 133);
            remoteDataSourcesGroupBox.SucceedingText = "";
            remoteDataSourcesGroupBox.TabIndex = 2;
            remoteDataSourcesGroupBox.TabStop = false;
            // 
            // label1
            // 
            label1.CharacterCasing = CharacterCasing.Normal;
            label1.Dock = DockStyle.Top;
            label1.Localization = Localization.LocalizationOptions.DataSources;
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.PreceedingText = "";
            label1.Size = new Size(450, 23);
            label1.SucceedingText = "";
            label1.TabIndex = 0;
            label1.TextAlign = ContentAlignment.BottomCenter;
            // 
            // dataSelectLeftPanel
            // 
            dataSelectLeftPanel.Controls.Add(panel3);
            dataSelectLeftPanel.Controls.Add(panel2);
            dataSelectLeftPanel.Dock = DockStyle.Fill;
            dataSelectLeftPanel.Location = new Point(0, 0);
            dataSelectLeftPanel.Margin = new Padding(0);
            dataSelectLeftPanel.Name = "dataSelectLeftPanel";
            dataSelectLeftPanel.Size = new Size(309, 461);
            dataSelectLeftPanel.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(panel4);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 40);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(3);
            panel3.Size = new Size(309, 421);
            panel3.TabIndex = 5;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Transparent;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Size = new Size(303, 415);
            panel4.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(3);
            panel2.Size = new Size(309, 40);
            panel2.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(languageTableLayoutPanel);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(303, 34);
            panel1.TabIndex = 4;
            // 
            // languageTableLayoutPanel
            // 
            languageTableLayoutPanel.ColumnCount = 2;
            languageTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            languageTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            languageTableLayoutPanel.Controls.Add(languageSelectLabel, 0, 0);
            languageTableLayoutPanel.Controls.Add(languageSelectComboBox, 1, 0);
            languageTableLayoutPanel.Dock = DockStyle.Fill;
            languageTableLayoutPanel.Location = new Point(0, 0);
            languageTableLayoutPanel.Margin = new Padding(0);
            languageTableLayoutPanel.Name = "languageTableLayoutPanel";
            languageTableLayoutPanel.RowCount = 1;
            languageTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            languageTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            languageTableLayoutPanel.Size = new Size(301, 32);
            languageTableLayoutPanel.TabIndex = 3;
            // 
            // languageSelectLabel
            // 
            languageSelectLabel.Anchor = AnchorStyles.Right;
            languageSelectLabel.AutoSize = true;
            languageSelectLabel.Location = new Point(50, 8);
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
            languageSelectComboBox.Location = new Point(153, 4);
            languageSelectComboBox.Name = "languageSelectComboBox";
            languageSelectComboBox.Size = new Size(95, 23);
            languageSelectComboBox.TabIndex = 1;
            languageSelectComboBox.SelectedIndexChanged += languageSelectComboBox_SelectedIndexChanged;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(776, 461);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPageTheSecond";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataSelectRemoteSourcesPanel
            // 
            dataSelectRemoteSourcesPanel.AutoScroll = true;
            dataSelectRemoteSourcesPanel.BackColor = Color.White;
            dataSelectRemoteSourcesPanel.Dock = DockStyle.Fill;
            dataSelectRemoteSourcesPanel.Location = new Point(3, 19);
            dataSelectRemoteSourcesPanel.Name = "dataSelectRemoteSourcesPanel";
            dataSelectRemoteSourcesPanel.Padding = new Padding(3);
            dataSelectRemoteSourcesPanel.Size = new Size(438, 111);
            dataSelectRemoteSourcesPanel.TabIndex = 1;
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
            Text = "WorldCupViewer";
            mainTabControl.ResumeLayout(false);
            dataSelectTab.ResumeLayout(false);
            dataSelectTableLayout.ResumeLayout(false);
            dataSelectRightPanel.ResumeLayout(false);
            panel5.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            localDataSourcesGroupBox.ResumeLayout(false);
            remoteDataSourcesGroupBox.ResumeLayout(false);
            dataSelectLeftPanel.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            languageTableLayoutPanel.ResumeLayout(false);
            languageTableLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private TabControl mainTabControl;
        private TabPage dataSelectTab;
        private TableLayoutPanel dataSelectTableLayout;
        private TabPage tabPage2;
        private Panel dataSelectLeftPanel;
        private Panel dataSelectRightPanel;
        private Panel panel2;
        private Panel panel1;
        private TableLayoutPanel languageTableLayoutPanel;
        private Label languageSelectLabel;
        private ComboBox languageSelectComboBox;
        private Panel panel3;
        private Panel panel4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel panel5;
        private TableLayoutPanel tableLayoutPanel1;
        private MultilingualGroupBox localDataSourcesGroupBox;
        private Panel dataSelectLocalSourcesPanel;
        private MultilingualGroupBox remoteDataSourcesGroupBox;
        private MultilingualLabel label1;
        private Panel dataSelectRemoteSourcesPanel;
    }
}
