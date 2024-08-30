namespace WorldCupViewer.UserControls
{
    partial class ExternalImageSelectionOption
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
            epbImage = new ExternalImage();
            lblExternalImageName = new Label();
            ((System.ComponentModel.ISupportInitialize)epbImage).BeginInit();
            SuspendLayout();
            // 
            // epbImage
            // 
            epbImage.ExternalImageID = null;
            epbImage.Location = new Point(15, 0);
            epbImage.Margin = new Padding(0);
            epbImage.Name = "epbImage";
            epbImage.Size = new Size(70, 70);
            epbImage.SizeMode = PictureBoxSizeMode.Zoom;
            epbImage.TabIndex = 0;
            epbImage.TabStop = false;
            // 
            // lblExternalImageName
            // 
            lblExternalImageName.Location = new Point(0, 70);
            lblExternalImageName.Margin = new Padding(0);
            lblExternalImageName.Name = "lblExternalImageName";
            lblExternalImageName.Size = new Size(100, 30);
            lblExternalImageName.TabIndex = 1;
            lblExternalImageName.Text = "thingamagajigadotjaypeg.jpg";
            lblExternalImageName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ExternalImageSelectionOption
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblExternalImageName);
            Controls.Add(epbImage);
            Margin = new Padding(0);
            Name = "ExternalImageSelectionOption";
            Size = new Size(100, 100);
            ((System.ComponentModel.ISupportInitialize)epbImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ExternalImage epbImage;
        private Label lblExternalImageName;
    }
}
