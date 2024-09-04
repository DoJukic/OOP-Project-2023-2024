namespace WorldCupViewer.UserControls
{
    partial class CupMatchStatsDisplay
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
            lblAwayTeam = new Label();
            lblAttendance = new Label();
            multilingualLabel3 = new MultilingualLabel();
            multilingualLabel1 = new MultilingualLabel();
            lblHomeTeam = new Label();
            lblMatchLocation = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lblAwayTeam, 2, 1);
            tableLayoutPanel1.Controls.Add(lblAttendance, 1, 0);
            tableLayoutPanel1.Controls.Add(multilingualLabel3, 2, 0);
            tableLayoutPanel1.Controls.Add(multilingualLabel1, 0, 0);
            tableLayoutPanel1.Controls.Add(lblHomeTeam, 0, 1);
            tableLayoutPanel1.Location = new Point(0, 20);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(248, 54);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblAwayTeam
            // 
            lblAwayTeam.Dock = DockStyle.Fill;
            lblAwayTeam.Location = new Point(164, 18);
            lblAwayTeam.Margin = new Padding(0);
            lblAwayTeam.Name = "lblAwayTeam";
            lblAwayTeam.Size = new Size(84, 36);
            lblAwayTeam.TabIndex = 5;
            lblAwayTeam.Text = "AWAY TEAM HERE";
            lblAwayTeam.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAttendance
            // 
            lblAttendance.AutoSize = true;
            lblAttendance.Dock = DockStyle.Fill;
            lblAttendance.Location = new Point(84, 0);
            lblAttendance.Margin = new Padding(0);
            lblAttendance.Name = "lblAttendance";
            tableLayoutPanel1.SetRowSpan(lblAttendance, 2);
            lblAttendance.Size = new Size(80, 54);
            lblAttendance.TabIndex = 4;
            lblAttendance.Text = "ATTENDANCE";
            lblAttendance.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // multilingualLabel3
            // 
            multilingualLabel3.CharacterCasing = CharacterCasing.Normal;
            multilingualLabel3.Dock = DockStyle.Fill;
            multilingualLabel3.Localization = Localization.LocalizationOptions.Away_Team;
            multilingualLabel3.Location = new Point(164, 0);
            multilingualLabel3.Margin = new Padding(0);
            multilingualLabel3.Name = "multilingualLabel3";
            multilingualLabel3.PreceedingText = "";
            multilingualLabel3.Size = new Size(84, 18);
            multilingualLabel3.SucceedingText = ":";
            multilingualLabel3.TabIndex = 2;
            multilingualLabel3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // multilingualLabel1
            // 
            multilingualLabel1.CharacterCasing = CharacterCasing.Normal;
            multilingualLabel1.Dock = DockStyle.Fill;
            multilingualLabel1.Localization = Localization.LocalizationOptions.Home_Team;
            multilingualLabel1.Location = new Point(0, 0);
            multilingualLabel1.Margin = new Padding(0);
            multilingualLabel1.Name = "multilingualLabel1";
            multilingualLabel1.PreceedingText = "";
            multilingualLabel1.Size = new Size(84, 18);
            multilingualLabel1.SucceedingText = ":";
            multilingualLabel1.TabIndex = 0;
            multilingualLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHomeTeam
            // 
            lblHomeTeam.Dock = DockStyle.Fill;
            lblHomeTeam.Location = new Point(0, 18);
            lblHomeTeam.Margin = new Padding(0);
            lblHomeTeam.Name = "lblHomeTeam";
            lblHomeTeam.Size = new Size(84, 36);
            lblHomeTeam.TabIndex = 3;
            lblHomeTeam.Text = "HOME TEAM HERE";
            lblHomeTeam.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMatchLocation
            // 
            lblMatchLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblMatchLocation.Location = new Point(3, 0);
            lblMatchLocation.Name = "lblMatchLocation";
            lblMatchLocation.Size = new Size(242, 20);
            lblMatchLocation.TabIndex = 1;
            lblMatchLocation.Text = "LOCATION HERE";
            lblMatchLocation.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CupMatchStatsDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblMatchLocation);
            Controls.Add(tableLayoutPanel1);
            Name = "CupMatchStatsDisplay";
            Size = new Size(248, 74);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label lblAwayTeam;
        private Label lblAttendance;
        private MultilingualLabel multilingualLabel3;
        private MultilingualLabel multilingualLabel1;
        private Label lblHomeTeam;
        private Label lblMatchLocation;
    }
}
