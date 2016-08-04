using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace jurnalu_downloader
{
    public class Logic
    {
        public class RequestInfo
        {
            public readonly int FirstIndex;
            public readonly int LastIndex;
            public readonly string BaseUrl;
            public readonly string Extension;
            public readonly string Book;
            public readonly string Issue;
            public RequestInfo(int firstIndex, int lastIndex, string baseUrl, string extension, string book="book", string issue ="issue")
            {
                FirstIndex = firstIndex;
                LastIndex = lastIndex;
                BaseUrl = baseUrl;
                Extension = extension;
                Book = book;
                Issue = issue;
            }
            public override bool Equals(object obj)
            {
                if (obj.GetType() == typeof(RequestInfo))
                {
                    var other = obj as RequestInfo;
                    return this.FirstIndex == other.FirstIndex &&
                        this.FirstIndex == other.FirstIndex &&
                        this.BaseUrl == other.BaseUrl &&
                        this.Extension == other.Extension &&
                        this.Issue == other.Issue &&
                        this.Book == other.Book;
                }
                return false;
            }
        }

        
        public static RequestInfo ParsePage(string url, string contents)
        {
            var matches = Regex.Matches(contents, @" *<a href=""(.*)\/(.+)\.(.+)"" rel=""shadowbox""><b>Увеличить</b></a>");
            if (matches.Count == 0) return null;
            var baseurl = matches[0].Groups[1].Value;
            var idx = Int32.Parse(matches[0].Groups[2].Value);
            var ext = matches[0].Groups[3].Value;

            var matches2 = Regex.Matches(contents, @"select class=""C"" .*>-(\d+)-</option>.*</select>", RegexOptions.Singleline);
            if (matches2.Count == 0) return null;
            var lastIdx = Int32.Parse(matches2[0].Groups[1].Value);
            var matches3 = Regex.Matches(url, @".*/([^/]+)/([^/]+)/?$");

            if (matches3.Count == 0) return null;
            var book = matches3[0].Groups[1].Value;
            var issue = matches3[0].Groups[2].Value;


            return new RequestInfo(idx, lastIdx, baseurl, ext, book, issue);
        }
    }


}