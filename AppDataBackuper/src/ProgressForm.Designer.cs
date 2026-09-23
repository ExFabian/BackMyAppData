namespace AppDataBackuper
{
    partial class ProgressForm
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
            components = new System.ComponentModel.Container();
            ProgressBar = new ProgressBar();
            LogBox = new TextBox();
            StartOperationButton = new Button();
            CancelOperationButton = new Button();
            CommonSetting1 = new CheckBox();
            SettingsLabel = new Label();
            CommonSetting2 = new CheckBox();
            SpecificSetting1 = new CheckBox();
            CommonSetting1ToolTip = new ToolTip(components);
            CommonSetting2ToolTip = new ToolTip(components);
            SpecificSetting1ToolTip = new ToolTip(components);
            SuspendLayout();
            // 
            // ProgressBar
            // 
            ProgressBar.Location = new Point(12, 201);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(387, 27);
            ProgressBar.Style = ProgressBarStyle.Continuous;
            ProgressBar.TabIndex = 0;
            // 
            // LogBox
            // 
            LogBox.AcceptsReturn = true;
            LogBox.Location = new Point(12, 12);
            LogBox.Multiline = true;
            LogBox.Name = "LogBox";
            LogBox.ReadOnly = true;
            LogBox.ScrollBars = ScrollBars.Vertical;
            LogBox.Size = new Size(387, 168);
            LogBox.TabIndex = 1;
            // 
            // StartOperationButton
            // 
            StartOperationButton.Location = new Point(38, 432);
            StartOperationButton.Name = "StartOperationButton";
            StartOperationButton.Size = new Size(105, 33);
            StartOperationButton.TabIndex = 2;
            StartOperationButton.Text = "Start";
            StartOperationButton.UseVisualStyleBackColor = true;
            StartOperationButton.Click += StartOperationButton_Click;
            // 
            // CancelOperationButton
            // 
            CancelOperationButton.Location = new Point(265, 431);
            CancelOperationButton.Name = "CancelOperationButton";
            CancelOperationButton.Size = new Size(105, 34);
            CancelOperationButton.TabIndex = 3;
            CancelOperationButton.Text = "Cancel";
            CancelOperationButton.UseVisualStyleBackColor = true;
            CancelOperationButton.Click += CancelOperationButton_Click;
            // 
            // CommonSetting1
            // 
            CommonSetting1.AutoSize = true;
            CommonSetting1.Location = new Point(12, 277);
            CommonSetting1.Name = "CommonSetting1";
            CommonSetting1.Size = new Size(120, 19);
            CommonSetting1.TabIndex = 4;
            CommonSetting1.Text = "CommonSetting1";
            CommonSetting1.UseVisualStyleBackColor = true;
            // 
            // SettingsLabel
            // 
            SettingsLabel.AutoSize = true;
            SettingsLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SettingsLabel.Location = new Point(12, 240);
            SettingsLabel.Name = "SettingsLabel";
            SettingsLabel.Size = new Size(142, 25);
            SettingsLabel.TabIndex = 5;
            SettingsLabel.Text = "[type] Settings";
            // 
            // CommonSetting2
            // 
            CommonSetting2.AutoSize = true;
            CommonSetting2.Location = new Point(12, 302);
            CommonSetting2.Name = "CommonSetting2";
            CommonSetting2.Size = new Size(120, 19);
            CommonSetting2.TabIndex = 6;
            CommonSetting2.Text = "CommonSetting2";
            CommonSetting2.UseVisualStyleBackColor = true;
            // 
            // SpecificSetting1
            // 
            SpecificSetting1.AutoSize = true;
            SpecificSetting1.Location = new Point(12, 327);
            SpecificSetting1.Name = "SpecificSetting1";
            SpecificSetting1.Size = new Size(110, 19);
            SpecificSetting1.TabIndex = 7;
            SpecificSetting1.Text = "SpecificSetting1";
            SpecificSetting1.UseVisualStyleBackColor = true;
            // 
            // ProgressForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(413, 487);
            ControlBox = false;
            Controls.Add(SpecificSetting1);
            Controls.Add(CommonSetting2);
            Controls.Add(SettingsLabel);
            Controls.Add(CommonSetting1);
            Controls.Add(CancelOperationButton);
            Controls.Add(StartOperationButton);
            Controls.Add(LogBox);
            Controls.Add(ProgressBar);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProgressForm";
            ShowInTaskbar = false;
            Text = "ProgressForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar ProgressBar;
        private TextBox LogBox;
        private Button StartOperationButton;
        private Button CancelOperationButton;
        private CheckBox CommonSetting1;
        private Label SettingsLabel;
        private CheckBox CommonSetting2;
        private CheckBox SpecificSetting1;
        private ToolTip CommonSetting1ToolTip;
        private ToolTip CommonSetting2ToolTip;
        private ToolTip SpecificSetting1ToolTip;
    }
}