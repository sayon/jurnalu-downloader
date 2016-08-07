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

        volatile bool started = false;
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (!started)
            {
                started = true;
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadDataCompleted += (o, ee) =>
                    {
                        var parseResult = IssueDownloader.ParsePage(Encoding.UTF8.GetString(ee.Result));
                        if (parseResult != null)
                        {
                            var df = new DownloadForm(parseResult);
                            df.Show();
                            Hide();
                        }
                        else {
                            textBoxInput.Enabled = true;
                            _error("Can't parse this page. Make sure you have opened a comic page!");
                        }
                    };
                    try
                    {
                        wc.DownloadDataAsync(new System.Uri(textBoxInput.Text));
                        textBoxInput.Enabled = false;
                    }
                    catch (UriFormatException) { _error("Check URL format"); }
                    catch (WebException) { _error("Can't download, check network connection"); }
                }
            }
        }
        private void _error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            started = false;
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

        internal void Reset()
        {
            started = false;
            buttonDownload.Enabled = true;
            textBoxInput.Enabled = true;
            textBoxInput.ResetText();
            textBoxInput.Focus();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            textBoxInput.Focus();
        }
    }
}
