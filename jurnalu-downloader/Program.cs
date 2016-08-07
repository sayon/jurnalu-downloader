using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace jurnalu_downloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            /*AsyncFileDownloader fd = new AsyncFileDownloader(new string[] {@"https://pp.vk.me/c631918/v631918000/39633/htI9LPHs9sA.jpg",
                             @"https://pp.vk.me/c604321/v604321000/1dc28/_OXq8tczhd4.jpg",
                             @"https://pp.vk.me/c626521/v626521000/39b5/o0ilOehLcwM.jpg"},
                             (i, s) => i.ToString() + "_" + Path.GetFileName(s));
            
            fd.onDownloadProgressChanged += (oo, ee) =>
            System.Diagnostics.Debug.Print("Progress: {0}\n", ee.ProgressPercentage);
                fd.DownloadAll("d:");

            
            System.Diagnostics.Debug.WriteLine("Done"); */
        }
    }
}
