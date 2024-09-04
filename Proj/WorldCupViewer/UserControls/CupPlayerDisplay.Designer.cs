namespace WorldCupViewer.UserControls
{
    partial class CupPlayerDisplay
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CupPlayerDisplay));
            tableLayoutPanel1 = new TableLayoutPanel();
            epbProfilePicture = new ExternalImage();
            panel1 = new Panel();
            tableLayoutPanel5 = new TableLayoutPanel();
            lblName = new Label();
            pbIsFavourite = new PictureBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            multilingualLabel3 = new MultilingualLabel();
            lblUsualPosition = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            lblGamesCaptained = new Label();
            multilingualLabel2 = new MultilingualLabel();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblShirtNumber = new Label();
            multilingualLabel1 = new MultilingualLabel();
            cmsCupPlayer = new ContextMenuStrip(components);
            changeImageToolStripMenuItem = new ToolStripMenuItem();
            selectToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)epbProfilePicture).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbIsFavourite).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            cmsCupPlayer.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(epbProfilePicture, 1, 0);
            tableLayoutPanel1.Controls.Add(panel1, 2, 0);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(298, 90);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // epbProfilePicture
            // 
            epbProfilePicture.ExternalImageID = "tatatatatatata";
            epbProfilePicture.Image = (Image)resources.GetObject("epbProfilePicture.Image");
            epbProfilePicture.Location = new Point(14, 2);
            epbProfilePicture.Margin = new Padding(0, 2, 0, 0);
            epbProfilePicture.Name = "epbProfilePicture";
            epbProfilePicture.Size = new Size(84, 84);
            epbProfilePicture.SizeMode = PictureBoxSizeMode.Zoom;
            epbProfilePicture.TabIndex = 0;
            epbProfilePicture.TabStop = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(tableLayoutPanel5);
            panel1.Controls.Add(tableLayoutPanel4);
            panel1.Controls.Add(tableLayoutPanel3);
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Location = new Point(101, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(180, 84);
            panel1.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(lblName, 0, 0);
            tableLayoutPanel5.Controls.Add(pbIsFavourite, 1, 0);
            tableLayoutPanel5.Location = new Point(0, 0);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(183, 27);
            tableLayoutPanel5.TabIndex = 8;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(3, 5);
            lblName.Margin = new Padding(3, 5, 0, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(69, 15);
            lblName.TabIndex = 0;
            lblName.Text = "NAMEHERE";
            // 
            // pbIsFavourite
            // 
            pbIsFavourite.Location = new Point(72, 4);
            pbIsFavourite.Margin = new Padding(0, 4, 3, 6);
            pbIsFavourite.Name = "pbIsFavourite";
            pbIsFavourite.Size = new Size(15, 15);
            pbIsFavourite.SizeMode = PictureBoxSizeMode.Zoom;
            pbIsFavourite.TabIndex = 1;
            pbIsFavourite.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(multilingualLabel3, 0, 0);
            tableLayoutPanel4.Controls.Add(lblUsualPosition, 1, 0);
            tableLayoutPanel4.Location = new Point(0, 62);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(180, 20);
            tableLayoutPanel4.TabIndex = 7;
            // 
            // multilingualLabel3
            // 
            multilingualLabel3.AutoSize = true;
            multilingualLabel3.CharacterCasing = CharacterCasing.Normal;
            multilingualLabel3.Localization = Localization.LocalizationOptions.Usual_Position;
            multilingualLabel3.Location = new Point(3, 0);
            multilingualLabel3.Margin = new Padding(3, 0, 0, 0);
            multilingualLabel3.Name = "multilingualLabel3";
            multilingualLabel3.PreceedingText = "";
            multilingualLabel3.Size = new Size(115, 15);
            multilingualLabel3.SucceedingText = ":";
            multilingualLabel3.TabIndex = 0;
            // 
            // lblUsualPosition
            // 
            lblUsualPosition.Dock = DockStyle.Fill;
            lblUsualPosition.Location = new Point(118, 0);
            lblUsualPosition.Margin = new Padding(0);
            lblUsualPosition.Name = "lblUsualPosition";
            lblUsualPosition.Size = new Size(62, 20);
            lblUsualPosition.TabIndex = 6;
            lblUsualPosition.Text = "USUALPOSITION";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(lblGamesCaptained, 1, 0);
            tableLayoutPanel3.Controls.Add(multilingualLabel2, 0, 0);
            tableLayoutPanel3.Location = new Point(0, 43);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(180, 20);
            tableLayoutPanel3.TabIndex = 7;
            // 
            // lblGamesCaptained
            // 
            lblGamesCaptained.Dock = DockStyle.Fill;
            lblGamesCaptained.Location = new Point(136, 0);
            lblGamesCaptained.Margin = new Padding(0);
            lblGamesCaptained.Name = "lblGamesCaptained";
            lblGamesCaptained.Size = new Size(44, 20);
            lblGamesCaptained.TabIndex = 5;
            lblGamesCaptained.Text = "GAMESCAPTAINED";
            // 
            // multilingualLabel2
            // 
            multilingualLabel2.AutoSize = true;
            multilingualLabel2.CharacterCasing = CharacterCasing.Normal;
            multilingualLabel2.Localization = Localization.LocalizationOptions.Games_Captained;
            multilingualLabel2.Location = new Point(3, 0);
            multilingualLabel2.Margin = new Padding(3, 0, 0, 0);
            multilingualLabel2.Name = "multilingualLabel2";
            multilingualLabel2.PreceedingText = "";
            multilingualLabel2.Size = new Size(133, 15);
            multilingualLabel2.SucceedingText = ":";
            multilingualLabel2.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(lblShirtNumber, 1, 0);
            tableLayoutPanel2.Controls.Add(multilingualLabel1, 0, 0);
            tableLayoutPanel2.Location = new Point(0, 24);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(180, 20);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // lblShirtNumber
            // 
            lblShirtNumber.AutoSize = true;
            lblShirtNumber.Location = new Point(114, 0);
            lblShirtNumber.Margin = new Padding(0);
            lblShirtNumber.Name = "lblShirtNumber";
            lblShirtNumber.Size = new Size(63, 20);
            lblShirtNumber.TabIndex = 9;
            lblShirtNumber.Text = "NUMBERGOESHERE";
            // 
            // multilingualLabel1
            // 
            multilingualLabel1.AutoSize = true;
            multilingualLabel1.CharacterCasing = CharacterCasing.Normal;
            multilingualLabel1.Localization = Localization.LocalizationOptions.Shirt_Number;
            multilingualLabel1.Location = new Point(3, 0);
            multilingualLabel1.Margin = new Padding(3, 0, 0, 0);
            multilingualLabel1.Name = "multilingualLabel1";
            multilingualLabel1.PreceedingText = "";
            multilingualLabel1.Size = new Size(111, 15);
            multilingualLabel1.SucceedingText = ":";
            multilingualLabel1.TabIndex = 0;
            // 
            // cmsCupPlayer
            // 
            cmsCupPlayer.Items.AddRange(new ToolStripItem[] { changeImageToolStripMenuItem, selectToolStripMenuItem });
            cmsCupPlayer.Name = "contextMenuStrip1";
            cmsCupPlayer.Size = new Size(152, 48);
            // 
            // changeImageToolStripMenuItem
            // 
            changeImageToolStripMenuItem.Image = Properties.Resources.PictureBoxPicture;
            changeImageToolStripMenuItem.Name = "changeImageToolStripMenuItem";
            changeImageToolStripMenuItem.Size = new Size(151, 22);
            changeImageToolStripMenuItem.Text = "Change Image";
            changeImageToolStripMenuItem.Click += changeImageToolStripMenuItem_Click;
            // 
            // selectToolStripMenuItem
            // 
            selectToolStripMenuItem.Image = Properties.Resources.MouseCursorTransparent_PlusPNG;
            selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            selectToolStripMenuItem.Size = new Size(151, 22);
            selectToolStripMenuItem.Text = "Select";
            selectToolStripMenuItem.Click += selectToolStripMenuItem_Click;
            // 
            // CupPlayerDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            ContextMenuStrip = cmsCupPlayer;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(0);
            Name = "CupPlayerDisplay";
            Size = new Size(298, 90);
            Click += CupPlayerDisplay_Click;
            MouseEnter += CupPlayerDisplay_MouseEnter;
            MouseLeave += CupPlayerDisplay_MouseLeave;
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)epbProfilePicture).EndInit();
            panel1.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbIsFavourite).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            cmsCupPlayer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private ExternalImage epbProfilePicture;
        private Panel panel1;
        private Label lblName;
        private Label lblUsualPosition;
        private Label lblGamesCaptained;
        private ContextMenuStrip cmsCupPlayer;
        private ToolStripMenuItem changeImageToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel2;
        private MultilingualLabel multilingualLabel1;
        private TableLayoutPanel tableLayoutPanel4;
        private MultilingualLabel multilingualLabel3;
        private TableLayoutPanel tableLayoutPanel3;
        private MultilingualLabel multilingualLabel2;
        private ToolStripMenuItem selectToolStripMenuItem;
        private Label lblShirtNumber;
        private TableLayoutPanel tableLayoutPanel5;
        private PictureBox pbIsFavourite;
    }
}
