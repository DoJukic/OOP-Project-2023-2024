namespace WorldCupViewer.UserControls
{
    partial class SelectableFileDataSource_v2
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
            loadButton = new MultilingualButton();
            DeleteButton = new MultilingualButton();
            mainPictureBox = new PictureBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            dataLabel = new MultilingualLabel();
            structureLabel = new MultilingualLabel();
            dataStatusLabel = new MultilingualLabel();
            structureStatusLabel = new MultilingualLabel();
            infoLabel = new MultilingualLabel();
            infoStatusLabel = new MultilingualLabel();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
            tableLayoutPanel1.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Location = new Point(59, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(288, 50);
            tableLayoutPanel1.TabIndex = 12;
            // 
            // titleLabel
            // 
            titleLabel.AutoEllipsis = true;
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.Location = new Point(3, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(207, 50);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Name + Year here";
            // 
            // panel1
            // 
            panel1.Controls.Add(loadButton);
            panel1.Controls.Add(DeleteButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(213, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(75, 50);
            panel1.TabIndex = 2;
            // 
            // loadButton
            // 
            loadButton.CharacterCasing = CharacterCasing.Normal;
            loadButton.Dock = DockStyle.Top;
            loadButton.Localization = Localization.LocalizationOptions.Load;
            loadButton.Location = new Point(0, 0);
            loadButton.Name = "loadButton";
            loadButton.PreceedingText = "> ";
            loadButton.Size = new Size(75, 23);
            loadButton.SucceedingText = "";
            loadButton.TabIndex = 8;
            loadButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            DeleteButton.CharacterCasing = CharacterCasing.Normal;
            DeleteButton.Dock = DockStyle.Bottom;
            DeleteButton.Localization = Localization.LocalizationOptions.Delete;
            DeleteButton.Location = new Point(0, 27);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.PreceedingText = "X ";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.SucceedingText = "";
            DeleteButton.TabIndex = 9;
            DeleteButton.UseVisualStyleBackColor = true;
            // 
            // mainPictureBox
            // 
            mainPictureBox.BackColor = Color.Transparent;
            mainPictureBox.Location = new Point(3, 3);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new Size(50, 50);
            mainPictureBox.TabIndex = 11;
            mainPictureBox.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 8;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(dataLabel, 6, 0);
            tableLayoutPanel2.Controls.Add(structureLabel, 3, 0);
            tableLayoutPanel2.Controls.Add(dataStatusLabel, 7, 0);
            tableLayoutPanel2.Controls.Add(structureStatusLabel, 4, 0);
            tableLayoutPanel2.Controls.Add(infoLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(infoStatusLabel, 1, 0);
            tableLayoutPanel2.Location = new Point(3, 59);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(344, 24);
            tableLayoutPanel2.TabIndex = 13;
            // 
            // dataLabel
            // 
            dataLabel.AutoSize = true;
            dataLabel.BackColor = Color.Transparent;
            dataLabel.CharacterCasing = CharacterCasing.Normal;
            dataLabel.Dock = DockStyle.Fill;
            dataLabel.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            dataLabel.Localization = Localization.LocalizationOptions.Data;
            dataLabel.Location = new Point(224, 0);
            dataLabel.Margin = new Padding(0);
            dataLabel.Name = "dataLabel";
            dataLabel.PreceedingText = "";
            dataLabel.Size = new Size(60, 24);
            dataLabel.SucceedingText = ":";
            dataLabel.TabIndex = 9;
            dataLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // structureLabel
            // 
            structureLabel.AutoSize = true;
            structureLabel.BackColor = Color.Transparent;
            structureLabel.CharacterCasing = CharacterCasing.Normal;
            structureLabel.Dock = DockStyle.Fill;
            structureLabel.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            structureLabel.Localization = Localization.LocalizationOptions.Structure;
            structureLabel.Location = new Point(99, 0);
            structureLabel.Margin = new Padding(0);
            structureLabel.Name = "structureLabel";
            structureLabel.PreceedingText = "";
            structureLabel.Size = new Size(83, 24);
            structureLabel.SucceedingText = ":";
            structureLabel.TabIndex = 8;
            structureLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dataStatusLabel
            // 
            dataStatusLabel.AutoSize = true;
            dataStatusLabel.BackColor = Color.Transparent;
            dataStatusLabel.CharacterCasing = CharacterCasing.Upper;
            dataStatusLabel.Dock = DockStyle.Fill;
            dataStatusLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            dataStatusLabel.ForeColor = Color.DarkOrange;
            dataStatusLabel.Localization = Localization.LocalizationOptions.Wait;
            dataStatusLabel.Location = new Point(284, 0);
            dataStatusLabel.Margin = new Padding(0);
            dataStatusLabel.Name = "dataStatusLabel";
            dataStatusLabel.PreceedingText = "";
            dataStatusLabel.Size = new Size(61, 24);
            dataStatusLabel.SucceedingText = "";
            dataStatusLabel.TabIndex = 7;
            dataStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // structureStatusLabel
            // 
            structureStatusLabel.AutoSize = true;
            structureStatusLabel.BackColor = Color.Transparent;
            structureStatusLabel.CharacterCasing = CharacterCasing.Upper;
            structureStatusLabel.Dock = DockStyle.Fill;
            structureStatusLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            structureStatusLabel.ForeColor = Color.DarkOrange;
            structureStatusLabel.Localization = Localization.LocalizationOptions.Wait;
            structureStatusLabel.Location = new Point(182, 0);
            structureStatusLabel.Margin = new Padding(0);
            structureStatusLabel.Name = "structureStatusLabel";
            structureStatusLabel.PreceedingText = "";
            structureStatusLabel.Size = new Size(61, 24);
            structureStatusLabel.SucceedingText = "";
            structureStatusLabel.TabIndex = 6;
            structureStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // infoLabel
            // 
            infoLabel.AutoSize = true;
            infoLabel.BackColor = Color.Transparent;
            infoLabel.CharacterCasing = CharacterCasing.Normal;
            infoLabel.Dock = DockStyle.Fill;
            infoLabel.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            infoLabel.Localization = Localization.LocalizationOptions.Info;
            infoLabel.Location = new Point(0, 0);
            infoLabel.Margin = new Padding(0);
            infoLabel.Name = "infoLabel";
            infoLabel.PreceedingText = "";
            infoLabel.Size = new Size(57, 24);
            infoLabel.SucceedingText = ":";
            infoLabel.TabIndex = 5;
            infoLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // infoStatusLabel
            // 
            infoStatusLabel.AutoSize = true;
            infoStatusLabel.BackColor = Color.Transparent;
            infoStatusLabel.CharacterCasing = CharacterCasing.Upper;
            infoStatusLabel.Dock = DockStyle.Fill;
            infoStatusLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            infoStatusLabel.ForeColor = Color.DarkOrange;
            infoStatusLabel.Localization = Localization.LocalizationOptions.Wait;
            infoStatusLabel.Location = new Point(57, 0);
            infoStatusLabel.Margin = new Padding(0);
            infoStatusLabel.Name = "infoStatusLabel";
            infoStatusLabel.PreceedingText = "";
            infoStatusLabel.Size = new Size(61, 24);
            infoStatusLabel.SucceedingText = "";
            infoStatusLabel.TabIndex = 4;
            infoStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SelectableFileDataSource_v2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(mainPictureBox);
            MinimumSize = new Size(255, 90);
            Name = "SelectableFileDataSource_v2";
            Size = new Size(350, 90);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label titleLabel;
        private Panel panel1;
        private MultilingualButton loadButton;
        private MultilingualButton DeleteButton;
        private PictureBox mainPictureBox;
        private TableLayoutPanel tableLayoutPanel2;
        private MultilingualLabel infoStatusLabel;
        private MultilingualLabel infoLabel;
        private MultilingualLabel dataStatusLabel;
        private MultilingualLabel structureStatusLabel;
        private MultilingualLabel structureLabel;
        private MultilingualLabel dataLabel;
    }
}
