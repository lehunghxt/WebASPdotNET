using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Asp.Provider;
using Web.Asp.Provider.Cache;
using Web.Asp.UI;
using Web.Business;
using Web.Model;
using Library.Web.RSS;
using Library;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Net;
using System.Xml.Linq;
using System.Text;

namespace Web.FrontEnd.Modules
{
    public partial class RSSReader : VITModule
    {
        protected SyndicationFeed Feed;

        protected void Page_Load(object sender, EventArgs e)
        {
            string url = this.GetValueParam<string>("RSSLink");

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                       | SecurityProtocolType.Tls11
                                                       | SecurityProtocolType.Tls12
                                                       | SecurityProtocolType.Ssl3;

            try
            {
                XmlReader reader = XmlReader.Create(url);
                this.Feed = SyndicationFeed.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    var webClient = new WebClient();
                    webClient.Encoding = Encoding.UTF8;
                    webClient.Encoding = UTF8Encoding.UTF8;
                    string result = webClient.DownloadString(url);
                    XDocument document = XDocument.Parse(result);
                    var data = (from descendant in document.Descendants("item")
                                select new SyndicationItem(descendant.Element("title").Value,
                                descendant.Element("description").Value,
                                new Uri(descendant.Element("link").Value))
                                {
                                    Summary = new TextSyndicationContent(descendant.Element("description").Value),
                                    PublishDate = new DateTimeOffset(Convert.ToDateTime(descendant.Element("pubDate").Value)),
                                }).ToList();
                    Feed = new SyndicationFeed(data);
                }
                catch
                { }
            }
        }
    }
}