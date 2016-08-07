using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Net;
using System.IO;
using System.ComponentModel;

namespace jurnalu_downloader
{
    public class ComicIssue
    {
        public readonly LinkedList<string> Urls;
        public readonly string Book;
        public readonly string Issue;
        public ComicIssue(LinkedList<string> urls, string book, string issue)
        {
            Urls = urls;
            Book = book;
            Issue = issue;
        }
    }

    public class IssueDownloader
    {
        public static ComicIssue ParsePage(string contents)
        {
            var matches = Regex.Matches(contents, @" *<a href=""(.*)\/(.+)\.(.+)"" rel=""shadowbox""><b>Увеличить</b></a>");
            if (matches.Count == 0) return null;
            var baseurl = matches[0].Groups[1].Value;
            var ext = matches[0].Groups[3].Value;

            var matches2 = Regex.Matches(contents, @"select class=""C"" .*>-(\d+)-</option>.*</select>", RegexOptions.Singleline);
            if (matches2.Count == 0) return null;
            var lastIdx = Int32.Parse(matches2[0].Groups[1].Value);

            var matches3 = Regex.Matches(contents, @"<meta NAME=""description"" content=""Читайте онлайн комикс '(.*)' номер '(.*)' на сайте Jurnalu.ru"">");
            if (matches3.Count == 0) return null;
            var book = matches3[0].Groups[1].Value;
            var issue = matches3[0].Groups[2].Value;
            LinkedList<String> filenames = new LinkedList<string>();
            for (int i = 1; i <= lastIdx; i++)
                filenames.AddLast(baseurl + "/" + i + "." + ext);

            return new ComicIssue(filenames, book, issue);
        }

        public ComicIssue Issue = null;
        public void Initialize(Uri uri)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadDataCompleted += (o, e) => Issue = IssueDownloader.ParsePage(Encoding.UTF8.GetString(e.Result));
                wc.DownloadDataAsync(uri);
            }
        }


        public void PerformDownload(DownloadProgressChangedEventHandler onDownloadProgressChanged,
            AsyncCompletedEventHandler onDownloadDataCompleted)
        {
            int i = 1;
            if (Issue == null) 
            using (WebClient wc = new WebClient())
            {
                var enumerator = Issue.Urls.GetEnumerator();
                var directoryPath = Issue.Book + "/" + Issue.Issue;

                wc.DownloadFileCompleted += (o, e) =>
                {
                    if (enumerator.MoveNext())
                        wc.DownloadFileAsync(new Uri(enumerator.Current), directoryPath + "/" + (i++) + Path.GetExtension(enumerator.Current));
                };

                if (onDownloadProgressChanged != null)
                    wc.DownloadProgressChanged += onDownloadProgressChanged;

                if (onDownloadDataCompleted != null)
                    wc.DownloadFileCompleted += onDownloadDataCompleted;

                if (enumerator.MoveNext())
                    wc.DownloadFileAsync(new Uri(enumerator.Current), directoryPath + "/" + (i++) + Path.GetExtension(enumerator.Current));
            };
        }
    }

    

}