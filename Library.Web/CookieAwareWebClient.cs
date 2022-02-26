namespace Library.Web
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    public class CookieAwareWebClient : WebClient
    {
        public void Login(string loginPageAddress, NameValueCollection loginData)
        {
            CookieContainer container;

            var request = (HttpWebRequest)WebRequest.Create(loginPageAddress);
                    
            string baseSiteString = this.DownloadString(loginPageAddress);
            string csrfToken = Regex.Match(baseSiteString, "<meta name=\"csrf-token\" content=\"(.*?)\"").Groups[1].Value;
            if(!string.IsNullOrEmpty(csrfToken))
            {
                request.Headers.Add("X-CSRF-Token", csrfToken);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                loginData.Add("_token", csrfToken);
                request.CookieContainer = this.CookieContainer;
            }
            else
            {
                request.CookieContainer = new CookieContainer();
            }

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            
            var query = string.Join("&",
              loginData.Cast<string>().Select(key => $"{key}={loginData[key]}"));

            var buffer = Encoding.ASCII.GetBytes(query);
            request.ContentLength = buffer.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();

            container = request.CookieContainer;

            var response = request.GetResponse();
            response.Close();
            CookieContainer = container;
        }

        public CookieAwareWebClient(CookieContainer container)
        {
            CookieContainer = container;
        }

        public CookieAwareWebClient()
          : this(new CookieContainer())
        { }

        public CookieContainer CookieContainer { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }
    }
}
