namespace WorldCupViewer
{
    partial class YesNoDontAskAgainDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnYes = new UserControls.MultilingualButton();
            btnNo = new UserControls.MultilingualButton();
            textLabel = new Label();
            btnYesAndBuzzOff = new UserControls.MultilingualButton();
            SuspendLayout();
            // 
            // btnYes
            // 
            btnYes.CharacterCasing = CharacterCasing.Normal;
            btnYes.Localization = Localization.LocalizationOptions.Yes;
            btnYes.Location = new Point(12, 56);
            btnYes.Name = "btnYes";
            btnYes.PreceedingText = "[Y] ";
            btnYes.Size = new Size(75, 23);
            btnYes.SucceedingText = " ";
            btnYes.TabIndex = 0;
            btnYes.UseVisualStyleBackColor = true;
            btnYes.Click += btnYes_Click;
            // 
            // btnNo
            // 
            btnNo.CharacterCasing = CharacterCasing.Normal;
            btnNo.Localization = Localization.LocalizationOptions.No;
            btnNo.Location = new Point(97, 56);
            btnNo.Name = "btnNo";
            btnNo.PreceedingText = "[N] ";
            btnNo.Size = new Size(75, 23);
            btnNo.SucceedingText = " ";
            btnNo.TabIndex = 1;
            btnNo.UseVisualStyleBackColor = true;
            btnNo.Click += btnNo_Click;
            // 
            // textLabel
            // 
            textLabel.Location = new Point(12, 9);
            textLabel.Name = "textLabel";
            textLabel.Size = new Size(160, 33);
            textLabel.TabIndex = 2;
            textLabel.Text = "LABEL LABEL LABEL LABEL LABEL";
            textLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnYesAndBuzzOff
            // 
            btnYesAndBuzzOff.CharacterCasing = CharacterCasing.Normal;
            btnYesAndBuzzOff.Localization = Localization.LocalizationOptions.Yes_And_Do_Not_Ask_Again;
            btnYesAndBuzzOff.Location = new Point(12, 85);
            btnYesAndBuzzOff.Name = "btnYesAndBuzzOff";
            btnYesAndBuzzOff.PreceedingText = "[D] ";
            btnYesAndBuzzOff.Size = new Size(160, 23);
            btnYesAndBuzzOff.SucceedingText = "";
            btnYesAndBuzzOff.TabIndex = 3;
            btnYesAndBuzzOff.UseVisualStyleBackColor = true;
            btnYesAndBuzzOff.Click += btnYesAndBuzzOff_Click;
            // 
            // YesNoDontAskAgainDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(184, 121);
            Controls.Add(btnYesAndBuzzOff);
            Controls.Add(textLabel);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            KeyPreview = true;
            MaximizeBox = false;
            MaximumSize = new Size(200, 160);
            MinimizeBox = false;
            MinimumSize = new Size(200, 160);
            Name = "YesNoDontAskAgainDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "YesNoDontAskAgainDialog";
            KeyPress += YesNoDontAskAgainDialog_KeyPress;
            ResumeLayout(false);
        }

        #endregion

        private UserControls.MultilingualButton btnYes;
        private UserControls.MultilingualButton btnNo;
        private Label textLabel;
        private UserControls.MultilingualButton btnYesAndBuzzOff;
    }
}