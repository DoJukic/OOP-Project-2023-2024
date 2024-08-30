namespace WorldCupViewer
{
    partial class SelectExternalImageDialog
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
            flpMain = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flpMain
            // 
            flpMain.AutoScroll = true;
            flpMain.BackColor = Color.White;
            flpMain.Dock = DockStyle.Fill;
            flpMain.Location = new Point(0, 0);
            flpMain.Margin = new Padding(0);
            flpMain.Name = "flpMain";
            flpMain.Size = new Size(420, 400);
            flpMain.TabIndex = 0;
            // 
            // SelectExternalImageDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 400);
            Controls.Add(flpMain);
            MaximizeBox = false;
            MaximumSize = new Size(436, 439);
            MinimizeBox = false;
            MinimumSize = new Size(436, 439);
            Name = "SelectExternalImageDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SelectExternalImageDialog";
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpMain;
    }
}