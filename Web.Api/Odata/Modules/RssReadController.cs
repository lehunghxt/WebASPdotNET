namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.ServiceModel.Syndication;
    using System.Text;
    using System.Net.Http;
    using System.Net;

    public class RssReadController : OdataBaseController<RssModel, int>
    {
        private ArticleBLL bll;
        public RssReadController()
        {
            this.bll = new ArticleBLL();
        }
        [EnableQuery]
        public override IQueryable<RssModel> Get()
        {
            var param = this.GetParameter();
            var linkId = 0;
            if (param.ContainsKey("LinkId")) int.TryParse(param["LinkId"], out linkId);

            var data = new List<RssModel>();

            var link = this.bll.GetLinks(this.Web.ID, this.Web.Language).FirstOrDefault(e => e.ID == linkId);
            if(link != null)
            {
                ////XmlReader reader = XmlReader.Create(link.URL);
                //XmlTextReader reader = new XmlTextReader(link.URL);
                //SyndicationFeed feed = SyndicationFeed.Load(reader);
                //reader.Close();
                //foreach (SyndicationItem item in feed.Items)
                //{
                //    var rss = new RssModel();
                //    rss.Title = item.Title.Text;
                //    data.Add(rss);
                //}
                XmlDocument rssXmlDoc = new XmlDocument(); 
                // Load the RSS file from the RSS URL 
                //rssXmlDoc.Load(link.URL);
                //rssXmlDoc.Load("http://rss.cnn.com/rss/edition.rss");
                string text;
                using (var client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    text = client.DownloadString(link.URL);
                }
                rssXmlDoc.LoadXml(text);
                // Parse the Items in the RSS file
                XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
                StringBuilder rssContent = new StringBuilder();

                // Iterate through the items in the RSS file
                for (int i = 0; i < rssNodes.Count; i++)
                {
                    var rss = new RssModel();
                    rss.ID = i;

                    XmlNode rssSubNode = rssNodes[i].SelectSingleNode("title");
                    rss.Title = rssSubNode != null ? rssSubNode.InnerText : "";

                    rssSubNode = rssNodes[i].SelectSingleNode("link");
                    rss.Link = rssSubNode != null ? rssSubNode.InnerText : "";

                    rssSubNode = rssNodes[i].SelectSingleNode("description");
                    rss.Description = rssSubNode != null ? rssSubNode.InnerText : "";

                    data.Add(rss);
                }
            }
            return data.AsQueryable();
        }

    }
}
