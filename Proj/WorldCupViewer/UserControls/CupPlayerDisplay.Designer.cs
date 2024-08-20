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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CupPlayerDisplay));
            tableLayoutPanel1 = new TableLayoutPanel();
            epbProfilePicture = new ExternalImage();
            panel1 = new Panel();
            lblUsualPosition = new Label();
            lblGamesCaptained = new Label();
            lblShirtNumber = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            lblName = new Label();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)epbProfilePicture).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(epbProfilePicture, 2, 0);
            tableLayoutPanel1.Controls.Add(panel1, 3, 0);
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
            epbProfilePicture.Location = new Point(7, 3);
            epbProfilePicture.Name = "epbProfilePicture";
            epbProfilePicture.Size = new Size(84, 84);
            epbProfilePicture.SizeMode = PictureBoxSizeMode.Zoom;
            epbProfilePicture.TabIndex = 0;
            epbProfilePicture.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblUsualPosition);
            panel1.Controls.Add(lblGamesCaptained);
            panel1.Controls.Add(lblShirtNumber);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(lblName);
            panel1.Location = new Point(97, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(194, 84);
            panel1.TabIndex = 1;
            // 
            // lblUsualPosition
            // 
            lblUsualPosition.Location = new Point(94, 64);
            lblUsualPosition.Name = "lblUsualPosition";
            lblUsualPosition.Size = new Size(97, 20);
            lblUsualPosition.TabIndex = 6;
            lblUsualPosition.Text = "USUALPOSITION";
            // 
            // lblGamesCaptained
            // 
            lblGamesCaptained.Location = new Point(110, 44);
            lblGamesCaptained.Name = "lblGamesCaptained";
            lblGamesCaptained.Size = new Size(84, 20);
            lblGamesCaptained.TabIndex = 5;
            lblGamesCaptained.Text = "GAMESCAPTAINED";
            // 
            // lblShirtNumber
            // 
            lblShirtNumber.Location = new Point(63, 24);
            lblShirtNumber.Name = "lblShirtNumber";
            lblShirtNumber.Size = new Size(128, 20);
            lblShirtNumber.TabIndex = 4;
            lblShirtNumber.Text = "NUMBERHERE";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 64);
            label4.Name = "label4";
            label4.Size = new Size(85, 15);
            label4.TabIndex = 3;
            label4.Text = "Usual position:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 44);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 2;
            label3.Text = "Games captained:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 24);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 1;
            label2.Text = "Number:";
            // 
            // lblName
            // 
            lblName.Location = new Point(3, 4);
            lblName.Name = "lblName";
            lblName.Size = new Size(180, 20);
            lblName.TabIndex = 0;
            lblName.Text = "NAMEHERE";
            // 
            // CupPlayerDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(0);
            Name = "CupPlayerDisplay";
            Size = new Size(298, 88);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)epbProfilePicture).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private ExternalImage epbProfilePicture;
        private Panel panel1;
        private Label lblName;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label lblUsualPosition;
        private Label lblGamesCaptained;
        private Label lblShirtNumber;
    }
}
