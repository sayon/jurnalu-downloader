using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace jurnalu_downloader
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        public MainForm()
        {
            Instance = this;
            InitializeComponent();
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            using (var downloader = new IssueDownloader(textBoxInput.Text, 
                issue => { new DownloadForm(issue).ShowDialog();}, Error))
            {}

        }

        private void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ActiveControl = textBoxInput;
            textBoxInput.Focus();
        }

        private void textBoxInput_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { buttonDownload.PerformClick(); }
        }

        

        private void MainForm_Activated(object sender, EventArgs e)
        {
            textBoxInput.Focus();
        }

        private void linkLabelAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://github.com/sayon/jurnalu-downloader");
        }
    }
}
