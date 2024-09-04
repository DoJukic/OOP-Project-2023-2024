namespace WorldCupViewer.UserControls
{
    partial class CupPlayerStatsDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CupPlayerStatsDisplay));
            epbProfilePicture = new ExternalImage();
            cmsCupPlayer = new ContextMenuStrip(components);
            changeImageToolStripMenuItem = new ToolStripMenuItem();
            pbIsFavourite = new PictureBox();
            lblName = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)epbProfilePicture).BeginInit();
            cmsCupPlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbIsFavourite).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // epbProfilePicture
            // 
            epbProfilePicture.ExternalImageID = "tatatatatatata";
            epbProfilePicture.Image = (Image)resources.GetObject("epbProfilePicture.Image");
            epbProfilePicture.Location = new Point(2, 2);
            epbProfilePicture.Margin = new Padding(2, 2, 0, 0);
            epbProfilePicture.Name = "epbProfilePicture";
            epbProfilePicture.Size = new Size(50, 50);
            epbProfilePicture.SizeMode = PictureBoxSizeMode.Zoom;
            epbProfilePicture.TabIndex = 0;
            epbProfilePicture.TabStop = false;
            // 
            // cmsCupPlayer
            // 
            cmsCupPlayer.Items.AddRange(new ToolStripItem[] { changeImageToolStripMenuItem });
            cmsCupPlayer.Name = "contextMenuStrip1";
            cmsCupPlayer.Size = new Size(181, 48);
            // 
            // changeImageToolStripMenuItem
            // 
            changeImageToolStripMenuItem.Image = Properties.Resources.PictureBoxPicture;
            changeImageToolStripMenuItem.Name = "changeImageToolStripMenuItem";
            changeImageToolStripMenuItem.Size = new Size(180, 22);
            changeImageToolStripMenuItem.Text = "Change Image";
            changeImageToolStripMenuItem.Click += changeImageToolStripMenuItem_Click;
            // 
            // pbIsFavourite
            // 
            pbIsFavourite.Location = new Point(0, 15);
            pbIsFavourite.Margin = new Padding(0, 15, 0, 0);
            pbIsFavourite.Name = "pbIsFavourite";
            pbIsFavourite.Size = new Size(16, 16);
            pbIsFavourite.SizeMode = PictureBoxSizeMode.Zoom;
            pbIsFavourite.TabIndex = 2;
            pbIsFavourite.TabStop = false;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Location = new Point(16, 0);
            lblName.Margin = new Padding(0);
            lblName.Name = "lblName";
            lblName.Size = new Size(176, 49);
            lblName.TabIndex = 0;
            lblName.Text = "TEXTHERE";
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pbIsFavourite, 0, 0);
            tableLayoutPanel1.Controls.Add(lblName, 1, 0);
            tableLayoutPanel1.Location = new Point(55, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(192, 49);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // CupPlayerStatsDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            ContextMenuStrip = cmsCupPlayer;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(epbProfilePicture);
            Margin = new Padding(0);
            Name = "CupPlayerStatsDisplay";
            Size = new Size(250, 56);
            ((System.ComponentModel.ISupportInitialize)epbProfilePicture).EndInit();
            cmsCupPlayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbIsFavourite).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ExternalImage epbProfilePicture;
        private ContextMenuStrip cmsCupPlayer;
        private ToolStripMenuItem changeImageToolStripMenuItem;
        private PictureBox pbIsFavourite;
        private Label lblName;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
