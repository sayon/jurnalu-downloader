using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Utilities;

namespace jurnalu_downloader
{
    public partial class DownloadForm : Form
    {
        public DownloadForm() { InitializeComponent(); }

        private bool _hasMore = false;

        private readonly ComicIssue _issue;
        private readonly string _directoryPath;
        private DirectoryInfo _directory;
        public DownloadForm(ComicIssue issue)
        {
            _issue = issue;
            _directoryPath = Sanitize(_issue.Book) + "/" + Sanitize(_issue.Issue);
            
            InitializeComponent();
            progressBarCurrent.Minimum = 0;
            progressBarCurrent.Value = 1;
            progressBarTotal.Minimum = 0;
            progressBarTotal.Maximum = _issue.Urls.Count;
            progressBarTotal.Value = 0;
            progressBarCurrent.Maximum = 100;

            labelDownloadingIntoContent.Text = _directoryPath;
        }

       
        private static void Notify(string msg, string header = "")
        {
            var item = new NotifyIcon();
            item.Visible = true;
            item.Icon = System.Drawing.SystemIcons.Information;
            item.ShowBalloonTip(100, header, msg, ToolTipIcon.Info);
        }

        private string Sanitize(string name)
        {
            var invalids = System.IO.Path.GetInvalidFileNameChars();
            return String.Join("", name.Split(invalids, StringSplitOptions.RemoveEmptyEntries));
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {
            _directory = Directory.CreateDirectory(_directoryPath);

            var current = 1;
            _updatePage(current);

            using (var downloader = new AsyncFileDownloader(_issue.Urls, (i, s) => Path.GetFileName(s), _directory.FullName))
            {

                downloader.DownloadProgressChanged = (o, ee) => progressBarCurrent.Value = ee.ProgressPercentage;
                downloader.OnDownloadDataCompleted = (o, ee) =>
                {
                    _updatePage(current);
                    progressBarTotal.Value = current++;
                    if (current > _issue.Urls.Count)
                    {
                        Notify("Download completed: " + _issue.Book + ", " + _issue.Issue);
                        buttonMore.Show();
                        buttonMore.Focus();
                    }
                };
                downloader.DownloadAll();
            }

        }

        private void _updatePage(int current)
        {
            labelPage.Text = "Page " + current + "/" + _issue.Urls.Count;
        }

        private void buttonShowInExplorer_Click(object sender, EventArgs e)
        {
            Process.Start(_directory.FullName); 
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_hasMore) MainForm.Instance.Close();
        }

        private void buttonMore_Click(object sender, EventArgs e)
        { 
            Close();
        }

        private void linkLabelAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://github.com/sayon/jurnalu-downloader");
        } 
        }
    } 
