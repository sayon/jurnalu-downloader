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

namespace jurnalu_downloader
{
    public partial class DownloadForm : Form
    {
        public DownloadForm()
        {
            InitializeComponent();
        }

        int current;
        bool _hasMore = false;

        private readonly Logic.RequestInfo _request;
        private readonly string _directoryPath;
        private DirectoryInfo _directory;
        public DownloadForm(Logic.RequestInfo request)
        {
            InitializeComponent();
            progressBarCurrent.Minimum = 0;
            progressBarCurrent.Value = 1;
            progressBarTotal.Minimum = 0;
            progressBarTotal.Maximum = request.LastIndex;
            progressBarTotal.Value = 0;
            progressBarCurrent.Maximum = 100;
            _request = request;
            current = request.FirstIndex;
            _directoryPath = _request.Book + "/" + _request.Issue;
            labelDownloadingIntoContent.Text = _directoryPath;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void download(WebClient wc, int i)
        {
            var fname = i + "." + _request.Extension;
            var url = _request.BaseUrl + "/" + fname;

            wc.DownloadFileAsync(new Uri(url), _directoryPath + "/" + fname);

        }

        private static void notify(string msg, string header = "")
        {
            var item = new NotifyIcon();
            item.Visible = true;
            item.Icon = System.Drawing.SystemIcons.Information;
            item.ShowBalloonTip(100, header, msg, ToolTipIcon.Info);
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {
            _directory = Directory.CreateDirectory(_directoryPath);
            WebClient wc = new WebClient();

            var theform = this;
            wc.DownloadProgressChanged += (o, ee) => progressBarCurrent.Value = ee.ProgressPercentage;

            wc.DownloadFileCompleted += (o, ee) =>
            {
                if (current < _request.LastIndex)
                {
                    labelPage.Text = "Page " + ++current + "/" + _request.LastIndex;
                    progressBarTotal.Value = current;
                    download(wc, current);
                }
                else
                {
                    notify("Download completed: " + _request.Book + ", " + _request.Issue);
                    buttonMore.Show();
                    buttonMore.Focus();
                }

            };

            download(wc, _request.FirstIndex);


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
            _hasMore = true;
            MainForm.Instance.Reset();
            MainForm.Instance.Show();
            MainForm.Instance.Refocus();
            Close();
        }
    }
}
