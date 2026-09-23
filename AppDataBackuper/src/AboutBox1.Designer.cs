namespace AppDataBackuper
{
    partial class AboutBox1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            AppNameLabel = new Label();
            AppCreatorLabel = new Label();
            AppCreatorLinkLabel = new LinkLabel();
            pictureBox1 = new PictureBox();
            AppLinkLabel = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // AppNameLabel
            // 
            AppNameLabel.AutoSize = true;
            AppNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AppNameLabel.Location = new Point(147, 10);
            AppNameLabel.Name = "AppNameLabel";
            AppNameLabel.Size = new Size(117, 21);
            AppNameLabel.TabIndex = 0;
            AppNameLabel.Text = "AppNameLabel";
            // 
            // AppCreatorLabel
            // 
            AppCreatorLabel.AutoSize = true;
            AppCreatorLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AppCreatorLabel.Location = new Point(147, 41);
            AppCreatorLabel.Name = "AppCreatorLabel";
            AppCreatorLabel.Size = new Size(127, 21);
            AppCreatorLabel.TabIndex = 1;
            AppCreatorLabel.Text = "AppCreatorLabel";
            // 
            // AppCreatorLinkLabel
            // 
            AppCreatorLinkLabel.AutoSize = true;
            AppCreatorLinkLabel.Location = new Point(147, 62);
            AppCreatorLinkLabel.Name = "AppCreatorLinkLabel";
            AppCreatorLinkLabel.Size = new Size(152, 15);
            AppCreatorLinkLabel.TabIndex = 2;
            AppCreatorLinkLabel.TabStop = true;
            AppCreatorLinkLabel.Text = "link to my github goes here";
            AppCreatorLinkLabel.LinkClicked += AppCreatorLinkLabel_LinkClicked;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Icon256;
            pictureBox1.Location = new Point(13, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 128);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // AppLinkLabel
            // 
            AppLinkLabel.AutoSize = true;
            AppLinkLabel.Location = new Point(147, 104);
            AppLinkLabel.Name = "AppLinkLabel";
            AppLinkLabel.Size = new Size(174, 15);
            AppLinkLabel.TabIndex = 4;
            AppLinkLabel.TabStop = true;
            AppLinkLabel.Text = "link to the app's page goes here";
            AppLinkLabel.LinkClicked += AppLinkLabel_LinkClicked;
            // 
            // AboutBox1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(394, 141);
            Controls.Add(AppLinkLabel);
            Controls.Add(pictureBox1);
            Controls.Add(AppCreatorLinkLabel);
            Controls.Add(AppCreatorLabel);
            Controls.Add(AppNameLabel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox1";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Label AppNameLabel;
        private Label AppCreatorLabel;
        private LinkLabel AppCreatorLinkLabel;
        private PictureBox pictureBox1;
        private LinkLabel AppLinkLabel;
    }
}
