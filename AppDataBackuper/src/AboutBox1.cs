using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDataBackuper
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
            this.Text = $"About {Global.appName}";
            AppNameLabel.Text = $"{Global.appName} version {Global.appVersion}";
            AppCreatorLabel.Text = "Created by ExFabian";
            AppCreatorLinkLabel.Text = "https://github.com/ExFabian";
            AppLinkLabel.Text = "Link to the project's main page";
        }

        private void AppCreatorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AppCreatorLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = "https://github.com/ExFabian", UseShellExecute = true });
        }

        private void AppLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AppLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = "https://github.com/ExFabian/BackMyAppData", UseShellExecute = true });
        }
    }
}
