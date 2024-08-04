namespace WorldCupViewer.UserControls
{
    partial class SelectableFileDataSource
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
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            button1 = new Button();
            button2 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Location = new Point(10, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoEllipsis = true;
            label1.BackColor = Color.Transparent;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(199, 50);
            label1.TabIndex = 1;
            label1.Text = "Name + Year here";
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 2;
            label2.Text = "Info:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.DarkOrange;
            label3.Location = new Point(59, 0);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 3;
            label3.Text = "WAIT";
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.DarkOrange;
            label4.Location = new Point(182, 0);
            label4.Name = "label4";
            label4.Size = new Size(50, 15);
            label4.TabIndex = 5;
            label4.Text = "WAIT";
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(115, 0);
            label5.Name = "label5";
            label5.Size = new Size(61, 15);
            label5.TabIndex = 4;
            label5.Text = "Structure:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.BackColor = Color.Transparent;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.DarkOrange;
            label6.Location = new Point(294, 0);
            label6.Name = "label6";
            label6.Size = new Size(53, 15);
            label6.TabIndex = 7;
            label6.Text = "WAIT";
            // 
            // label7
            // 
            label7.BackColor = Color.Transparent;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(238, 0);
            label7.Name = "label7";
            label7.Size = new Size(50, 15);
            label7.TabIndex = 6;
            label7.Text = "Data:";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Top;
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 8;
            button1.Text = "> Load";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Bottom;
            button2.Location = new Point(0, 27);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 9;
            button2.Text = "X Delete";
            button2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Location = new Point(66, 10);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(280, 50);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(205, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(75, 50);
            panel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.BackColor = Color.Transparent;
            tableLayoutPanel2.ColumnCount = 6;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.1290359F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.1290321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.35484F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.1290321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.1290321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.1290321F));
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(label6, 5, 0);
            tableLayoutPanel2.Controls.Add(label3, 1, 0);
            tableLayoutPanel2.Controls.Add(label7, 4, 0);
            tableLayoutPanel2.Controls.Add(label5, 2, 0);
            tableLayoutPanel2.Controls.Add(label4, 3, 0);
            tableLayoutPanel2.Location = new Point(0, 70);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(350, 15);
            tableLayoutPanel2.TabIndex = 11;
            // 
            // SelectableFileDataSource
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            BackColor = SystemColors.Window;
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(pictureBox1);
            Margin = new Padding(0);
            Name = "SelectableFileDataSource";
            Size = new Size(350, 100);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button button1;
        private Button button2;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
