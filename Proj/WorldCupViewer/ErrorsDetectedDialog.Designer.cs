namespace WorldCupViewer
{
    partial class ErrorsDetectedDialog
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
            lblMain = new Label();
            pnlErrorList = new Panel();
            SuspendLayout();
            // 
            // lblMain
            // 
            lblMain.BackColor = SystemColors.Control;
            lblMain.Dock = DockStyle.Top;
            lblMain.Location = new Point(0, 0);
            lblMain.Name = "lblMain";
            lblMain.Size = new Size(384, 46);
            lblMain.TabIndex = 3;
            lblMain.Text = "IF U SEE THIS ERROR MACHINE BROKE";
            lblMain.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlErrorList
            // 
            pnlErrorList.AutoScroll = true;
            pnlErrorList.BackColor = Color.White;
            pnlErrorList.BorderStyle = BorderStyle.FixedSingle;
            pnlErrorList.Location = new Point(12, 49);
            pnlErrorList.Name = "pnlErrorList";
            pnlErrorList.Size = new Size(360, 300);
            pnlErrorList.TabIndex = 4;
            // 
            // ErrorsDetectedDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 361);
            Controls.Add(pnlErrorList);
            Controls.Add(lblMain);
            KeyPreview = true;
            MaximizeBox = false;
            MaximumSize = new Size(400, 400);
            MinimizeBox = false;
            MinimumSize = new Size(400, 400);
            Name = "ErrorsDetectedDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ErrorsDetectedDialog";
            KeyPress += ErrorsDetectedDialog_KeyPress;
            ResumeLayout(false);
        }

        #endregion
        private Label lblMain;
        private Panel pnlErrorList;
    }
}