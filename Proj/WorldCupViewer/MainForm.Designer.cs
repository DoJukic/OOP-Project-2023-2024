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
            dataSelectLeftPanel = new Panel();
            tabPage2 = new TabPage();
            languageTableLayoutPanel = new TableLayoutPanel();
            languageSelectLabel = new Label();
            languageSelectComboBox = new ComboBox();
            mainTabControl.SuspendLayout();
            dataSelectTab.SuspendLayout();
            dataSelectTableLayout.SuspendLayout();
            dataSelectLeftPanel.SuspendLayout();
            languageTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Location = new Point(0, 428);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(800, 22);
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
            mainTabControl.Size = new Size(800, 428);
            mainTabControl.TabIndex = 1;
            // 
            // dataSelectTab
            // 
            dataSelectTab.Controls.Add(dataSelectTableLayout);
            dataSelectTab.Location = new Point(4, 24);
            dataSelectTab.Margin = new Padding(0);
            dataSelectTab.Name = "dataSelectTab";
            dataSelectTab.Size = new Size(792, 400);
            dataSelectTab.TabIndex = 0;
            dataSelectTab.Text = "Data Select / Config";
            dataSelectTab.UseVisualStyleBackColor = true;
            // 
            // dataSelectTableLayout
            // 
            dataSelectTableLayout.ColumnCount = 3;
            dataSelectTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            dataSelectTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 3F));
            dataSelectTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            dataSelectTableLayout.Controls.Add(dataSelectRightPanel, 3, 0);
            dataSelectTableLayout.Controls.Add(dataSelectLeftPanel, 0, 0);
            dataSelectTableLayout.Dock = DockStyle.Fill;
            dataSelectTableLayout.Location = new Point(0, 0);
            dataSelectTableLayout.Margin = new Padding(0);
            dataSelectTableLayout.Name = "dataSelectTableLayout";
            dataSelectTableLayout.RowCount = 1;
            dataSelectTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            dataSelectTableLayout.Size = new Size(792, 400);
            dataSelectTableLayout.TabIndex = 0;
            // 
            // dataSelectRightPanel
            // 
            dataSelectRightPanel.Dock = DockStyle.Fill;
            dataSelectRightPanel.Location = new Point(397, 0);
            dataSelectRightPanel.Margin = new Padding(0);
            dataSelectRightPanel.Name = "dataSelectRightPanel";
            dataSelectRightPanel.Size = new Size(395, 400);
            dataSelectRightPanel.TabIndex = 5;
            // 
            // dataSelectLeftPanel
            // 
            dataSelectLeftPanel.Controls.Add(languageTableLayoutPanel);
            dataSelectLeftPanel.Dock = DockStyle.Fill;
            dataSelectLeftPanel.Location = new Point(0, 0);
            dataSelectLeftPanel.Margin = new Padding(0);
            dataSelectLeftPanel.Name = "dataSelectLeftPanel";
            dataSelectLeftPanel.Size = new Size(394, 400);
            dataSelectLeftPanel.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 400);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // languageTableLayoutPanel
            // 
            languageTableLayoutPanel.ColumnCount = 2;
            languageTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            languageTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            languageTableLayoutPanel.Controls.Add(languageSelectLabel, 0, 0);
            languageTableLayoutPanel.Controls.Add(languageSelectComboBox, 1, 0);
            languageTableLayoutPanel.Dock = DockStyle.Top;
            languageTableLayoutPanel.Location = new Point(0, 0);
            languageTableLayoutPanel.Margin = new Padding(0);
            languageTableLayoutPanel.Name = "languageTableLayoutPanel";
            languageTableLayoutPanel.RowCount = 1;
            languageTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            languageTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            languageTableLayoutPanel.Size = new Size(394, 40);
            languageTableLayoutPanel.TabIndex = 2;
            // 
            // languageSelectLabel
            // 
            languageSelectLabel.Anchor = AnchorStyles.Right;
            languageSelectLabel.AutoSize = true;
            languageSelectLabel.Location = new Point(97, 12);
            languageSelectLabel.Name = "languageSelectLabel";
            languageSelectLabel.Size = new Size(97, 15);
            languageSelectLabel.TabIndex = 0;
            languageSelectLabel.Text = "Language / Jezik:";
            languageSelectLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // languageSelectComboBox
            // 
            languageSelectComboBox.Anchor = AnchorStyles.Left;
            languageSelectComboBox.FormattingEnabled = true;
            languageSelectComboBox.Location = new Point(200, 8);
            languageSelectComboBox.Name = "languageSelectComboBox";
            languageSelectComboBox.Size = new Size(121, 23);
            languageSelectComboBox.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(mainTabControl);
            Controls.Add(statusStrip);
            MinimumSize = new Size(600, 400);
            Name = "MainForm";
            Text = "WorldCupViewer";
            mainTabControl.ResumeLayout(false);
            dataSelectTab.ResumeLayout(false);
            dataSelectTableLayout.ResumeLayout(false);
            dataSelectLeftPanel.ResumeLayout(false);
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
        private TableLayoutPanel languageTableLayoutPanel;
        private Label languageSelectLabel;
        private ComboBox languageSelectComboBox;
    }
}
