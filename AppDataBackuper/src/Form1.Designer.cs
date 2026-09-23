
using AppDataBackuper.src;

namespace AppDataBackuper
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            ItemList = new FixedCheckedListBox();
            ItemContentListBox = new FixedCheckedListBox();
            LocalButton = new Button();
            RoamingButton = new Button();
            SoloBackupButton = new Button();
            SoloRestoreButton = new Button();
            LocalLowButton = new Button();
            LocalCountLabel = new Label();
            RoamingCountLabel = new Label();
            LocalLowCountLabel = new Label();
            OpenFolderButton = new Button();
            SelectAllButton = new Button();
            DeselectAllButton = new Button();
            AboutButton = new Button();
            SelectAllSubButton = new Button();
            DeselectAllSubButton = new Button();
            SuspendLayout();
            // 
            // ItemList
            // 
            ItemList.HorizontalScrollbar = true;
            ItemList.Location = new Point(16, 99);
            ItemList.Name = "ItemList";
            ItemList.Size = new Size(333, 238);
            ItemList.TabIndex = 0;
            ItemList.ItemCheck += ItemList_ItemCheck;
            ItemList.SelectedIndexChanged += ItemList_SelectedIndexChanged;
            // 
            // ItemContentListBox
            // 
            ItemContentListBox.HorizontalScrollbar = true;
            ItemContentListBox.Location = new Point(398, 99);
            ItemContentListBox.Name = "ItemContentListBox";
            ItemContentListBox.Size = new Size(246, 184);
            ItemContentListBox.TabIndex = 0;
            ItemContentListBox.ItemCheck += ItemContentListBox_ItemCheck;
            // 
            // LocalButton
            // 
            LocalButton.Location = new Point(16, 12);
            LocalButton.Name = "LocalButton";
            LocalButton.Size = new Size(127, 39);
            LocalButton.TabIndex = 2;
            LocalButton.Text = "LocalAppData";
            LocalButton.UseVisualStyleBackColor = true;
            LocalButton.Click += LocalButton_Click;
            // 
            // RoamingButton
            // 
            RoamingButton.Location = new Point(149, 12);
            RoamingButton.Name = "RoamingButton";
            RoamingButton.Size = new Size(127, 39);
            RoamingButton.TabIndex = 3;
            RoamingButton.Text = "RoamingAppData";
            RoamingButton.UseVisualStyleBackColor = true;
            RoamingButton.Click += RoamingButton_Click;
            // 
            // SoloBackupButton
            // 
            SoloBackupButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SoloBackupButton.Location = new Point(398, 343);
            SoloBackupButton.Name = "SoloBackupButton";
            SoloBackupButton.Size = new Size(122, 38);
            SoloBackupButton.TabIndex = 4;
            SoloBackupButton.Text = "Backup";
            SoloBackupButton.UseVisualStyleBackColor = true;
            SoloBackupButton.Click += SoloBackupButton_Click;
            // 
            // SoloRestoreButton
            // 
            SoloRestoreButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SoloRestoreButton.Location = new Point(526, 343);
            SoloRestoreButton.Name = "SoloRestoreButton";
            SoloRestoreButton.Size = new Size(118, 38);
            SoloRestoreButton.TabIndex = 5;
            SoloRestoreButton.Text = "Restore";
            SoloRestoreButton.UseVisualStyleBackColor = true;
            SoloRestoreButton.Click += SoloRestoreButton_Click;
            // 
            // LocalLowButton
            // 
            LocalLowButton.Location = new Point(282, 12);
            LocalLowButton.Name = "LocalLowButton";
            LocalLowButton.Size = new Size(127, 39);
            LocalLowButton.TabIndex = 6;
            LocalLowButton.Text = "LocalLowAppData";
            LocalLowButton.UseVisualStyleBackColor = true;
            LocalLowButton.Click += LocalLowButton_Click;
            // 
            // LocalCountLabel
            // 
            LocalCountLabel.AutoSize = true;
            LocalCountLabel.Location = new Point(52, 54);
            LocalCountLabel.Name = "LocalCountLabel";
            LocalCountLabel.Size = new Size(52, 15);
            LocalCountLabel.TabIndex = 7;
            LocalCountLabel.Text = "Count: 0";
            LocalCountLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // RoamingCountLabel
            // 
            RoamingCountLabel.AutoSize = true;
            RoamingCountLabel.Location = new Point(184, 54);
            RoamingCountLabel.Name = "RoamingCountLabel";
            RoamingCountLabel.Size = new Size(52, 15);
            RoamingCountLabel.TabIndex = 8;
            RoamingCountLabel.Text = "Count: 0";
            RoamingCountLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // LocalLowCountLabel
            // 
            LocalLowCountLabel.AutoSize = true;
            LocalLowCountLabel.Location = new Point(313, 54);
            LocalLowCountLabel.Name = "LocalLowCountLabel";
            LocalLowCountLabel.Size = new Size(52, 15);
            LocalLowCountLabel.TabIndex = 9;
            LocalLowCountLabel.Text = "Count: 0";
            LocalLowCountLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // OpenFolderButton
            // 
            OpenFolderButton.Enabled = false;
            OpenFolderButton.Location = new Point(242, 343);
            OpenFolderButton.Name = "OpenFolderButton";
            OpenFolderButton.Size = new Size(107, 38);
            OpenFolderButton.TabIndex = 10;
            OpenFolderButton.Text = "Open folder in Explorer";
            OpenFolderButton.UseVisualStyleBackColor = true;
            OpenFolderButton.Click += OpenFolderButton_Click;
            // 
            // SelectAllButton
            // 
            SelectAllButton.Location = new Point(16, 343);
            SelectAllButton.Name = "SelectAllButton";
            SelectAllButton.Size = new Size(107, 38);
            SelectAllButton.TabIndex = 11;
            SelectAllButton.Text = "Select All";
            SelectAllButton.UseVisualStyleBackColor = true;
            SelectAllButton.Click += SelectAllButton_Click;
            // 
            // DeselectAllButton
            // 
            DeselectAllButton.Location = new Point(129, 343);
            DeselectAllButton.Name = "DeselectAllButton";
            DeselectAllButton.Size = new Size(107, 38);
            DeselectAllButton.TabIndex = 12;
            DeselectAllButton.Text = "Deselect All";
            DeselectAllButton.UseVisualStyleBackColor = true;
            DeselectAllButton.Click += DeselectAllButton_Click;
            // 
            // AboutButton
            // 
            AboutButton.Location = new Point(574, 20);
            AboutButton.Name = "AboutButton";
            AboutButton.Size = new Size(75, 23);
            AboutButton.TabIndex = 13;
            AboutButton.Text = "About";
            AboutButton.UseVisualStyleBackColor = true;
            AboutButton.Click += AboutButton_Click;
            // 
            // SelectAllSubButton
            // 
            SelectAllSubButton.Location = new Point(398, 289);
            SelectAllSubButton.Name = "SelectAllSubButton";
            SelectAllSubButton.Size = new Size(122, 26);
            SelectAllSubButton.TabIndex = 14;
            SelectAllSubButton.Text = "Select All";
            SelectAllSubButton.UseVisualStyleBackColor = true;
            SelectAllSubButton.Click += SelectAllSubButton_Click;
            // 
            // DeselectAllSubButton
            // 
            DeselectAllSubButton.Location = new Point(526, 289);
            DeselectAllSubButton.Name = "DeselectAllSubButton";
            DeselectAllSubButton.Size = new Size(118, 26);
            DeselectAllSubButton.TabIndex = 15;
            DeselectAllSubButton.Text = "Deselect All";
            DeselectAllSubButton.UseVisualStyleBackColor = true;
            DeselectAllSubButton.Click += DeselectAllSubButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(661, 396);
            Controls.Add(DeselectAllSubButton);
            Controls.Add(SelectAllSubButton);
            Controls.Add(AboutButton);
            Controls.Add(DeselectAllButton);
            Controls.Add(SelectAllButton);
            Controls.Add(OpenFolderButton);
            Controls.Add(LocalLowCountLabel);
            Controls.Add(RoamingCountLabel);
            Controls.Add(LocalCountLabel);
            Controls.Add(ItemList);
            Controls.Add(ItemContentListBox);
            Controls.Add(LocalLowButton);
            Controls.Add(SoloRestoreButton);
            Controls.Add(SoloBackupButton);
            Controls.Add(RoamingButton);
            Controls.Add(LocalButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FixedCheckedListBox ItemList;
        private FixedCheckedListBox ItemContentListBox;
        private Button LocalButton;
        private Button RoamingButton;
        private Button SoloBackupButton;
        private Button SoloRestoreButton;
        private Button LocalLowButton;
        private Label LocalCountLabel;
        private Label RoamingCountLabel;
        private Label LocalLowCountLabel;
        private Button OpenFolderButton;
        private Button SelectAllButton;
        private Button DeselectAllButton;
        private Button AboutButton;
        private Button SelectAllSubButton;
        private Button DeselectAllSubButton;
    }
}
