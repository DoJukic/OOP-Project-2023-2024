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
            tableLayoutPanel1 = new TableLayoutPanel();
            epbProfilePicture = new ExternalImage();
            panel1 = new Panel();
            lblRow1 = new Label();
            lblRow2 = new Label();
            lblName = new Label();
            cmsCupPlayer = new ContextMenuStrip(components);
            changeImageToolStripMenuItem = new ToolStripMenuItem();
            selectToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)epbProfilePicture).BeginInit();
            panel1.SuspendLayout();
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
            panel1.Controls.Add(lblRow1);
            panel1.Controls.Add(lblRow2);
            panel1.Controls.Add(lblName);
            panel1.Location = new Point(101, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(180, 84);
            panel1.TabIndex = 1;
            // 
            // lblRow1
            // 
            lblRow1.Location = new Point(0, 40);
            lblRow1.Name = "lblRow1";
            lblRow1.Size = new Size(180, 16);
            lblRow1.TabIndex = 3;
            lblRow1.Text = "WORDHERE";
            lblRow1.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblRow2
            // 
            lblRow2.Location = new Point(0, 56);
            lblRow2.Name = "lblRow2";
            lblRow2.Size = new Size(180, 16);
            lblRow2.TabIndex = 2;
            lblRow2.Text = "NUMBERHERE";
            lblRow2.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblName
            // 
            lblName.Location = new Point(0, 12);
            lblName.Name = "lblName";
            lblName.Size = new Size(180, 16);
            lblName.TabIndex = 0;
            lblName.Text = "NAMEHERE";
            lblName.TextAlign = ContentAlignment.TopCenter;
            // 
            // cmsCupPlayer
            // 
            cmsCupPlayer.Items.AddRange(new ToolStripItem[] { changeImageToolStripMenuItem, selectToolStripMenuItem });
            cmsCupPlayer.Name = "contextMenuStrip1";
            cmsCupPlayer.Size = new Size(152, 48);
            // 
            // changeImageToolStripMenuItem
            // 
            changeImageToolStripMenuItem.Name = "changeImageToolStripMenuItem";
            changeImageToolStripMenuItem.Size = new Size(151, 22);
            changeImageToolStripMenuItem.Text = "Change Image";
            // 
            // selectToolStripMenuItem
            // 
            selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            selectToolStripMenuItem.Size = new Size(151, 22);
            selectToolStripMenuItem.Text = "Select";
            // 
            // CupPlayerStatsDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            ContextMenuStrip = cmsCupPlayer;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(0);
            Name = "CupPlayerStatsDisplay";
            Size = new Size(298, 90);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)epbProfilePicture).EndInit();
            panel1.ResumeLayout(false);
            cmsCupPlayer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private ExternalImage epbProfilePicture;
        private Panel panel1;
        private Label lblName;
        private ContextMenuStrip cmsCupPlayer;
        private ToolStripMenuItem changeImageToolStripMenuItem;
        private ToolStripMenuItem selectToolStripMenuItem;
        private Label lblRow1;
        private Label lblRow2;
    }
}
