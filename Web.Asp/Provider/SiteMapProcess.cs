namespace Web.Asp.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Xml;

    using log4net;
    using Web.Model;
    using System.Web;

    public class SiteMapProcess
    {
        public string Domain { get; set; }

        // folder root web ngoai
        public string FilePath { get; set; }

        public int CompanyId { get; set; }

        protected static readonly ILog log = LogManager.GetLogger(typeof(SiteMapProcess));
        
        public SiteMapProcess(string domain = "", string filePath = "", int companyId = 0)
        {
            this.Domain = domain;
            this.FilePath = filePath;
            this.CompanyId = companyId;
        }

        public void CreateSiteMap(IList<SEOLinkModel> urls)
        {
            if(!string.IsNullOrEmpty(this.Domain))
            {   
                try
                {
                    using (var writer = XmlWriter.Create(FilePath))
                    {
                        var scheam = this.GetScheme();
                        if (!this.Domain.StartsWith(scheam)) this.Domain = scheam + "://" + this.Domain;

                        log.Info(string.Format("===== Begin create sitemap: {0} =====", DateTime.Now));
                        writer.WriteStartDocument();
                        writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                        WriteTag("1", "Monthly", this.Domain, writer);

                        // tao sitemap
                        var maps = this.CreateMap(urls);
                        foreach (var item in maps)
                        {
                            WriteTag(item.Priority, item.Freq, item.Navigation, writer);
                        }

                        writer.WriteEndDocument();
                        writer.Close();

                        log.Info(string.Format("===== End create sitemap: {0} =====", DateTime.Now));
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// The write tag.
        /// </summary>
        /// <param name="priority">
        /// The priority.
        /// </param>
        /// <param name="freq">
        /// The freq.
        /// </param>
        /// <param name="navigation">
        /// The navigation.
        /// </param>
        /// <param name="myWriter">
        /// The my writer.
        /// </param>
        private void WriteTag(string priority, string freq, string navigation, XmlWriter myWriter)
        {
            myWriter.WriteStartElement("url");

            myWriter.WriteStartElement("loc");
            myWriter.WriteValue(navigation);
            myWriter.WriteEndElement();

            myWriter.WriteStartElement("lastmod");
            myWriter.WriteValue(String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            myWriter.WriteEndElement();

            myWriter.WriteStartElement("changefreq");
            myWriter.WriteValue(freq);
            myWriter.WriteEndElement();

            myWriter.WriteStartElement("priority");
            myWriter.WriteValue(priority);
            myWriter.WriteEndElement();

            myWriter.WriteEndElement();
        }

        /// <summary>
        /// Ham lay tat ca cac link cua web
        /// </summary>
        /// <param name="urls">
        /// Danh sach tat ca cac link
        /// </param>
        /// <param name="flagUrls">
        /// Danh sach cac link da va de get link roi
        /// </param>
        private void GetAllLink(IList<string> urls, IList<string> flagUrls, IList<string> errorUrls)
        {
            using (var client = new WebClient())
            {
                // lay cac link chua duoc kiem tra
                var goUrls = urls.Where(e => !flagUrls.Contains(e)).ToList();
                foreach (var url in goUrls)
                {
                    // vao tung link de get cac link trong trang do
                    var htmlSource = string.Empty;
                    try
                    {
                        htmlSource = client.DownloadString(url);
                    }
                    catch
                    {
                        errorUrls.Add(url);
                        continue;
                    }
                    
                    var links = this.GetLinksFromWebsite(htmlSource);

                    // loc lai chi lay nhung link thuoc web minh
                    links = links.Where(e => e.Contains(this.Domain) && !e.Contains("/uploads/") && !e.Contains("/includes/") && !e.Contains("/templates/") && !e.Contains("/modules/") && !e.Contains(".css") && !e.Contains(".js")).ToList();

                    // luu vet la da vao link nay
                    flagUrls.Add(url);

                    // lay ra nhung link chua co trong danh sach trong nhung link moi get ve
                    links = links.Where(e => !urls.Contains(e)).ToList();

                    // neu ko co link moi thi ko goi de quy nua
                    if (links.Count > 0)
                    {
                        // neu con link moi thi them nhung link moi get ve vao danh sach
                        foreach (var link in links)
                        {
                            urls.Add(link);
                        }

                        // goi de quy de get tiep nhung link con
                        this.GetAllLink(urls, flagUrls, errorUrls);
                    }
                }
            }
        }

        private IList<string> GetLinksFromWebsite(string htmlSource)
        {
            //string linkPattern = "<a href=\"(.*?)\">(.*?)</a>";
            // link thuoc domain minh
            string linkPattern = string.Format("href=\"{0}(.*?)\"", this.Domain);
            MatchCollection linkMatches = Regex.Matches(htmlSource, linkPattern, RegexOptions.Singleline);
            var linkContents = new List<string>();
            foreach (Match match in linkMatches)
            {
                if (!linkContents.Contains(match.Value) && !match.Value.Contains(".css"))
                {
                    var link = match.Value.Substring(6, match.Value.Length - 7);
                    if (link.EndsWith("/") || link.EndsWith("\\")) link.Substring(0, link.Length - 1);
                    linkContents.Add(link);
                }
            }

            // link thuoc domain minh khong co domain
            linkPattern = "href=\"/(.*?)\"";
            linkMatches = Regex.Matches(htmlSource, linkPattern, RegexOptions.Singleline);
            foreach (Match match in linkMatches)
            {
                if (!linkContents.Contains(match.Value) && !match.Value.Contains(".css"))
                {
                    var link = this.Domain + match.Value.Substring(6, match.Value.Length - 7); //them ten mien vao
                    if (link.EndsWith("/") || link.EndsWith("\\")) link.Substring(0, link.Length - 1);
                    linkContents.Add(link);
                }
            }

            return linkContents.Distinct().ToList();
        }

        private IList<MapItem> CreateMap(IList<SEOLinkModel> urls)
        {
            var maps = new List<MapItem>();
            
            foreach (var url in urls)
            {
                    var mapItem = new MapItem();
                    mapItem.Navigation = this.Domain + url.SeoUrl;

                    if (url.Url.ToLower().Contains("-scat-"))
                    {
                        mapItem.Freq = "Daily";
                        mapItem.Priority = "0.2";
                    }
                    else if (url.Url.ToLower().Contains("-satrvl") || url.Url.ToLower().Contains("-tag"))
                    {
                        mapItem.Freq = "Monthly";
                        mapItem.Priority = "0.4";
                    }
                    else
                    {
                        mapItem.Freq = "Yearly";
                        mapItem.Priority = "0.8";
                    }

                    maps.Add(mapItem);
            }

            return maps;
        }

        private class MapItem
        {
            public string Priority { get; set; }
            public string Freq { get; set; }
            public string Navigation { get; set; }
        }

        private string GetScheme()
        {
            if (HttpContext.Current.Request.Headers.AllKeys.Any(e => e.ToLower() == "x-forwarded-proto"))
                return HttpContext.Current.Request.Headers["X-Forwarded-Proto"];
            else if (HttpContext.Current.Request.IsSecureConnection)
                return "https";
            else
                return HttpContext.Current.Request.Url.Scheme;
        }
    }
}