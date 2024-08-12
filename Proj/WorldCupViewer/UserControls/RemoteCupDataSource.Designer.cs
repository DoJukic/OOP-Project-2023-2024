namespace WorldCupViewer.UserControls
{
    partial class RemoteCupDataSource
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
            tableLayoutPanel1 = new TableLayoutPanel();
            titleLabel = new Label();
            panel1 = new Panel();
            downloadButton = new MultilingualButton();
            mainPictureBox = new PictureBox();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Location = new Point(59, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(288, 50);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // titleLabel
            // 
            titleLabel.AutoEllipsis = true;
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.Location = new Point(3, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(202, 50);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Name + Year here";
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Controls.Add(downloadButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(208, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(80, 50);
            panel1.TabIndex = 2;
            // 
            // downloadButton
            // 
            downloadButton.CharacterCasing = CharacterCasing.Normal;
            downloadButton.Localization = Localization.LocalizationOptions.Download;
            downloadButton.Location = new Point(0, 13);
            downloadButton.Name = "downloadButton";
            downloadButton.PreceedingText = "V ";
            downloadButton.Size = new Size(80, 23);
            downloadButton.SucceedingText = "";
            downloadButton.TabIndex = 8;
            downloadButton.UseVisualStyleBackColor = true;
            // 
            // mainPictureBox
            // 
            mainPictureBox.BackColor = Color.Transparent;
            mainPictureBox.Location = new Point(3, 3);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new Size(50, 50);
            mainPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            mainPictureBox.TabIndex = 13;
            mainPictureBox.TabStop = false;
            // 
            // RemoteCupDataSource
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(mainPictureBox);
            MinimumSize = new Size(255, 60);
            Name = "RemoteCupDataSource";
            Size = new Size(350, 60);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label titleLabel;
        private Panel panel1;
        private MultilingualButton downloadButton;
        private PictureBox mainPictureBox;
    }
}
