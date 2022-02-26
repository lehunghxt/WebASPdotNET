namespace Library.Web
{
    using System;
    using System.Diagnostics;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Net.Sockets;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Summary description for WhoisUtils
    /// </summary>
    public class WhoisUtil
    {

        private string UrlCheckExist = "http://prowhois.net/api/checkexist.aspx?domain={0}";
        private string UrlDetail = "http://prowhois.net/api/detail.aspx?domain={0}";


        private static WhoisUtil _current;

        static WhoisUtil()
        {
            _current = new WhoisUtil();
        }

        public static WhoisUtil Current
        {
            get
            {
                return _current;
            }
        }

        #region all in one

        /// <summary>
        /// CheckExist
        /// </summary>
        /// <param name="sDomain"></param>
        /// <param name="sTLD"></param>
        /// <returns>-1 : failure; 0: available; 1: existed</returns>
        public string CheckExist(string sDomain)
        {
            if (!IsDomain(sDomain)) { return "-1"; }
            string url = string.Format(UrlCheckExist, sDomain);

            return GetHTML(url);
        }


        /// <summary>
        /// GetDetail
        /// </summary>
        /// <param name="sDomain"></param>
        /// <param name="sTLD"></param>
        /// <returns>Return whois information of domain name </returns>
        public string GetDetail(string sDomain)
        {
            if (!IsDomain(sDomain)) { return "No support"; }

            string url = string.Format(UrlDetail, sDomain);

            return GetHTML(url);

        }

        public string DomainMessage(string _code)
        {
            switch (_code.ToLower())
            {
                case "-1":
                    return "invalid/no support";
                case "0":
                    return "available";
                case "1":
                    return "registered";
            }
            return "failure";
        }
        #endregion


        #region Util

        private string GetHTML(string URL)
        {
            HttpWebRequest request = CreateRequest(URL);

            request.Referer = URL;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return GetResponse(response);
        }


        private string GetResponse(HttpWebResponse response)
        {
            string result = string.Empty;
            System.IO.Stream stream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            result = reader.ReadToEnd();
            response.Close();
            stream.Close();
            reader.Close();
            return result;
        }

        private HttpWebRequest CreateRequest(string uriString)
        {
            WebRequest request = WebRequest.Create(uriString);
            (request as HttpWebRequest).UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.0.3705; .NET CLR 1.1.4322; Media Center PC 4.0; .NET CLR 2.0.50727)";
            (request as HttpWebRequest).CookieContainer = new CookieContainer();
            (request as HttpWebRequest).Timeout =
                (request as HttpWebRequest).ReadWriteTimeout = 1000 * 30;

            return (HttpWebRequest)request;
        }



        public bool IsDomain(string sDomain)
        {
            if (!Regex.IsMatch(sDomain, @"^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}$"))
                return false;

            string ext = GetTLD(sDomain).ToLower();
            if (ext.Contains(".aspx") || ext.Contains(".asp") || ext.Contains(".html") || ext.Contains(".thm")
                 || ext.Contains(".js") || ext.Contains(".css") || ext.Contains(".cs") || ext.Contains(".gif")
                 || ext.Contains(".jpg") || ext.Contains(".png") || ext.Contains(".bmp") || ext.Contains(".xml")
                 || ext.Contains(".txt") || ext.Contains(".ico"))
                return false;

            return true;
        }

        public bool IsDomainNotExt(string sDomain)
        {
            return Regex.IsMatch(sDomain, @"(?=^.{1,254}$)(^(?:(?!\d+\.|-)[a-zA-Z0-9_\-]{1,63}$){1,63}(?<!-)\.?)");
        }

        public string GetTLD(string sDomain)
        {
            if (sDomain.Contains("."))
            {
                int pos = sDomain.IndexOf('.');
                if (pos == 0)
                    return sDomain;
                else
                    return sDomain.Substring(pos, sDomain.Length - pos);
            }
            return ".aspx";
        }

        private string QueryWhois(string sServer, string sDomain)
        {
            string ReturnString = "";
            using (TcpClient WhoIsClient = new TcpClient())
            {
                WhoIsClient.Connect(sServer, 43);
                NetworkStream WhoIsStream = WhoIsClient.GetStream();
                StreamWriter WhoIsWriter = new StreamWriter(WhoIsStream);
                WhoIsWriter.WriteLine(sDomain);
                WhoIsWriter.Flush();
                WhoIsStream = WhoIsClient.GetStream();
                StreamReader WhoIsReader = new StreamReader(WhoIsStream);


                string line = null;
                do
                {
                    line = WhoIsReader.ReadLine();
                    if (line != null)
                        ReturnString += line + "<br />";
                }
                while (line != null);

                WhoIsStream.Close();
                WhoIsClient.Close();

            }
            return ReturnString.Replace("   ", "&nbsp;&nbsp;&nbsp;");
        }


        public string GetDomainExt(string Domain)
        {
            string result = string.Empty;
            Regex myRegex = new Regex("^[a-zA-Z0-9\\-]+([\\.a-zA-Z0-9]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Match myMatch = myRegex.Match(Domain);
            if (myMatch.Success)
            {
                result = myMatch.Groups[1].Value.Trim();
            }
            return result;
        }
        public string GetDomainWithouExt(string Domain)
        {
            string result = string.Empty;
            Regex myRegex = new Regex("^([a-zA-Z0-9\\-]+)[\\.a-zA-Z0-9]+", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Match myMatch = myRegex.Match(Domain);
            if (myMatch.Success)
            {
                result = myMatch.Groups[1].Value.Trim();
            }
            return result;
        }
        public static string UpdateQueryString(string QueryStringKey, string QueryStringValue, string Url)
        {
            string NewUrl = Url;
            if (Url == "")
            {
                NewUrl = HttpContext.Current.Request.Url.PathAndQuery;
            }
            string PageUrl = NewUrl;
            if (NewUrl.IndexOf("?", 0) >= 0)
                PageUrl = NewUrl.Substring(0, NewUrl.IndexOf("?", 0));

            string NewKey = QueryStringKey + "=" + QueryStringValue;
            string NewQueryString = "";

            string OldQueryString = "";
            if (NewUrl.IndexOf("?", 0) >= 0)
                OldQueryString = NewUrl.Substring(NewUrl.IndexOf("?", 0) + 1);

            if (OldQueryString != "" && QueryStringKey != "")
            {
                string[] ArrayQuery = OldQueryString.Split('&');
                for (int i = 0; i < ArrayQuery.Length; i++)
                {
                    if (string.Compare(QueryStringKey, ArrayQuery[i].Substring(0, ArrayQuery[i].LastIndexOf("=")), true) == 0)
                    {
                        //NewQueryString += NewKey;
                    }
                    else
                    {
                        if (NewQueryString != "")
                            NewQueryString += "&";

                        NewQueryString += ArrayQuery[i];
                    }
                }


                if (QueryStringValue != "")
                {
                    if (NewQueryString != "")
                        NewQueryString += "&";

                    NewQueryString += NewKey;
                }


                if (NewQueryString != "")
                    NewUrl = PageUrl + "?" + NewQueryString;
                else
                    NewUrl = PageUrl;

            }
            else
            {
                if (QueryStringKey != "" && QueryStringValue != "")
                {
                    NewUrl = PageUrl + "?" + NewKey;
                }
            }
            return NewUrl;
        }
        #endregion
    }
}