using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace Utilities
{
    sealed class AsyncFileDownloader : IDisposable
    {
        private enum State
        {
            NOT_STARTED,
            DONE,
            WORKING
        }

        private State _state = State.NOT_STARTED;

        private readonly FileNameGenerator FileNameForUrl;
        private readonly LinkedList<string> _urls = new LinkedList<string>();
        private readonly string _directoryPath;

        private LinkedList<string>.Enumerator _enumerator;
        private int _currentUrlIndex = 0;
        private WebClient _webClient = new WebClient();


        public AsyncFileDownloader(IEnumerable<string> urls, FileNameGenerator fileNameForUrl, string directoryPath )
        {
            if (fileNameForUrl == null)
                throw new InvalidOperationException("FileNameForUrl can not be null");
            
            if (directoryPath == null)
                throw new InvalidOperationException("directoryPath can not be null");
            
            _directoryPath = directoryPath;
            FileNameForUrl = fileNameForUrl; 
            
            if (urls == null) throw new ArgumentNullException();
            foreach (var i in urls) _urls.AddLast(i); 
            _enumerator = _urls.GetEnumerator();
            
            _webClient.DownloadFileCompleted += (o, e) => DownloadNext();
        }

        public DownloadProgressChangedEventHandler DownloadProgressChanged { 
            set {
            _webClient.DownloadProgressChanged += value;
            }
        }
        public AsyncCompletedEventHandler OnDownloadDataCompleted {
            set {   
                _webClient.DownloadFileCompleted += value;
            } 
        }
        
        public delegate string FileNameGenerator(int index, string url);

      

        private void DownloadNext() {
             if (_enumerator.MoveNext())
                    {
                        var fname = _directoryPath + "/" + FileNameForUrl(_currentUrlIndex++, _enumerator.Current);
                        _webClient.DownloadFileAsync(new Uri(_enumerator.Current), fname);
                    }
             else _state = State.DONE;
        }
        public void DownloadAll()
        {
            _state = State.WORKING;
            DownloadNext();         
        }

        public bool Done { get { return _state == State.DONE; } }
        /**
         * Uses spinlock
         */
        public void WaitForFinish()
        {
            while (_state != State.DONE);
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
