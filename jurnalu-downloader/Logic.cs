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

    public class IssueDownloader : IDisposable
    {
        public delegate void IssueDownloaded(ComicIssue issue);
        public delegate void ErrorHandler(string error);
        public IssueDownloader(string address, IssueDownloaded forIssueDo, ErrorHandler error)
        {
            _webClient = new WebClient();
            _webClient.DownloadDataCompleted += (o, e) => {
                var issue = ParsePage(Encoding.UTF8.GetString(e.Result));
                if (issue == null) error("Can't parse this page. Make sure you have opened a comic page!");
                else forIssueDo(issue);
            };
            try
            {
                _webClient.DownloadDataAsync(new Uri(address));
            }
            catch (UriFormatException) { error("Check URL format"); }
            catch (WebException) { error("Can't download, check network connection"); }


        }
        private const string _regexImageUrl = @" *<a href=""(.*)\/(.+)\.(.+)"" rel=""shadowbox""><b>Увеличить</b></a>";
        private const string _regexIndices = @"select class=""C"" .*>-(\d+)-</option>.*</select>";
        private const string _regexBookIssue = @"<meta NAME=""description"" content=""Читайте онлайн комикс '(.*)' номер '(.*)' на сайте Jurnalu.ru"">";

        private WebClient _webClient;

        public static ComicIssue ParsePage(string contents)
        {
            var imageMatch = Regex.Matches(contents, _regexImageUrl);
            if (imageMatch.Count == 0) return null;
            var baseurl = imageMatch[0].Groups[1].Value;
            var ext = imageMatch[0].Groups[3].Value;

            var imageIndicesMatch = Regex.Matches(contents, _regexIndices, RegexOptions.Singleline);
            if (imageIndicesMatch.Count == 0) return null;
            var lastIdx = Int32.Parse(imageIndicesMatch[0].Groups[1].Value);

            var matches3 = Regex.Matches(contents, _regexBookIssue);
            if (matches3.Count == 0) return null;
            var book = matches3[0].Groups[1].Value;
            var issue = matches3[0].Groups[2].Value;

            var filenames = new LinkedList<string>();
            for (int i = 1; i <= lastIdx; i++)
                filenames.AddLast(String.Format("{0}/{1}.{2}", baseurl, i, ext));

            return new ComicIssue(filenames, book, issue);
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }

    }



}