using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace Utilities
{
    class AsyncFileDownloader
    {
        private bool _working = true;
        private void _end() { Done = true; _working = false; }
        protected FileNameGenerator FileNameForUrl;
        protected readonly LinkedList<string> _urls = new LinkedList<string>();
        public AsyncFileDownloader(IEnumerable<string> urls, FileNameGenerator fileNameForUrl )
        {
            if (urls == null) throw new ArgumentNullException();
            foreach (var i in urls) _urls.AddLast(i);
            Done = false;
            FileNameForUrl = fileNameForUrl;
        }

        public DownloadProgressChangedEventHandler onDownloadProgressChanged { get; set; }
        public AsyncCompletedEventHandler onDownloadDataCompleted { get; set; }
        
        public delegate string FileNameGenerator(int index, string url);
        
        public void DownloadAll(string directoryPath)
        {
            int i = 0;
            var enumerator = _urls.GetEnumerator();

            using (WebClient wc = new WebClient())
            {   
                wc.DownloadFileCompleted += (o, e) =>
                {
                    if (FileNameForUrl == null)
                        throw new InvalidOperationException("FileNameForUrl field should be initialized");
                    if (enumerator.MoveNext())
                    {
                        var fname = directoryPath + "/" + FileNameForUrl(i++, enumerator.Current);
                        wc.DownloadFileAsync(new Uri(enumerator.Current), fname);
                    }
                    else _end();
                };

                if (onDownloadProgressChanged != null)
                    wc.DownloadProgressChanged += onDownloadProgressChanged;

                if (onDownloadDataCompleted != null)
                    wc.DownloadFileCompleted += onDownloadDataCompleted;

                if (enumerator.MoveNext())
                {
                    var fname = directoryPath + "/" + FileNameForUrl(i++, enumerator.Current);
                    wc.DownloadFileAsync(new Uri(enumerator.Current), fname);
                }
                else _end();
            };
        }

        public bool Done { get; private set; }
        /**
         * Uses spinlock
         */
        public void WaitForFinish()
        {
            while (_working && !Done);
        }
    }
}
