using System;
using System.Globalization;
using System.Xml;

namespace Library.Web.RSS
{
    public class RSSCreate
    {
        private static XmlDocument addRssChannel(XmlDocument xmlDocument, RssChannel channel)
        {
            XmlElement channelElement = xmlDocument.CreateElement("channel");
            XmlNode rssElement = xmlDocument.SelectSingleNode("rss");
            rssElement.AppendChild(channelElement);
            XmlElement titleElement = xmlDocument.CreateElement("title");
            titleElement.InnerText = channel.Title;
            channelElement.AppendChild(titleElement);
            XmlElement linkElement = xmlDocument.CreateElement("link");
            linkElement.InnerText = channel.Link;
            channelElement.AppendChild(linkElement);
            XmlElement descriptionElement = xmlDocument.CreateElement("description");
            descriptionElement.InnerText = channel.Description;
            channelElement.AppendChild(descriptionElement);
            // language
            if (channel.Language != null)
            {
                XmlElement languageElement = xmlDocument.CreateElement("language");
                languageElement.InnerText = channel.Language;
                channelElement.AppendChild(languageElement);
            }
            // managing editor
            if (channel.ManagingEditor != null)
            {
                XmlElement editorElement = xmlDocument.CreateElement("managingEditor");
                editorElement.InnerText = channel.ManagingEditor;
                channelElement.AppendChild(editorElement);
            }
            // web master
            if (channel.Webmaster != null)
            {
                XmlElement editorElement = xmlDocument.CreateElement("webMaster");
                editorElement.InnerText = channel.Webmaster;
                channelElement.AppendChild(editorElement);
            }
            // copyright
            if (channel.Copyright != null)
            {
                XmlElement copyrightElement = xmlDocument.CreateElement("copyright");
                copyrightElement.InnerText = "Copyright © " + channel.Copyright;
                channelElement.AppendChild(copyrightElement);

            }
            //publication date
            XmlElement pubDateElement = xmlDocument.CreateElement("pubDate");
            pubDateElement.InnerText = channel.PublicationDate.ToString("ddd, dd MMM yyyy HH:mm:ss zz00", CultureInfo.InvariantCulture);
            channelElement.AppendChild(pubDateElement);
            // category
            if (channel.Category != null)
            {
                XmlElement categoryElement = xmlDocument.CreateElement("category");
                categoryElement.InnerText = channel.Category;
                channelElement.AppendChild(categoryElement);
            }
            // time to live
            XmlElement ttlElement = xmlDocument.CreateElement("ttl");
            ttlElement.InnerText = channel.TimeToLive.ToString();
            channelElement.AppendChild(ttlElement);
            //// document specification url
            //XmlElement docsElement = xmlDocument.CreateElement("docs");
            //docsElement.InnerText = "http://backend.userland.com/rss/";
            //channelElement.AppendChild(docsElement);
            // Generator information
            XmlElement generatorElement = xmlDocument.CreateElement("generator");
            generatorElement.InnerText = channel.Generator;
            channelElement.AppendChild(generatorElement);
            return xmlDocument;
        }

        private static XmlDocument addRssItem(XmlDocument xmlDocument, RssItem item)
        {
            XmlElement itemElement = xmlDocument.CreateElement("item");
            XmlNode channelElement = xmlDocument.SelectSingleNode("rss/channel");
            XmlElement titleElement = xmlDocument.CreateElement("title");
            titleElement.InnerText = item.Title;
            itemElement.AppendChild(titleElement);
            XmlElement linkElement = xmlDocument.CreateElement("link");
            linkElement.InnerText = item.Link;
            itemElement.AppendChild(linkElement);
            XmlElement descriptionElement = xmlDocument.CreateElement("description");
            descriptionElement.InnerText = item.Description;
            itemElement.AppendChild(descriptionElement);
            // author
            if (item.Author != null)
            {
                XmlElement authorElement = xmlDocument.CreateElement("author");
                authorElement.InnerText = item.Author;
                itemElement.AppendChild(authorElement);
            }
            // guid
            if (item.Guid != null)
            {
                XmlElement guidElement = xmlDocument.CreateElement("guid");
                guidElement.InnerText = item.Guid;
                itemElement.AppendChild(guidElement);
            }
            // Date
            XmlElement pubDateElement = xmlDocument.CreateElement("pubDate");
            pubDateElement.InnerText = item.PublicationDate.ToString("ddd, dd MMM yyyy HH:mm:ss zz00", CultureInfo.InvariantCulture);
            itemElement.AppendChild(pubDateElement);
            // source
            if (item.Source != null)
            {
                XmlElement sourceElement = xmlDocument.CreateElement("source");
                sourceElement.InnerText = item.Source;
                itemElement.AppendChild(sourceElement);
            }
            // category
            if (item.Category != null)
            {
                XmlElement categoryElement = xmlDocument.CreateElement("category");
                categoryElement.InnerText = item.Category;
                itemElement.AppendChild(categoryElement);
            }
            // comments
            XmlElement commentsElement = xmlDocument.CreateElement("comments");
            commentsElement.InnerText = item.Comments;
            itemElement.AppendChild(commentsElement);
            // append the item
            channelElement.AppendChild(itemElement);
            return xmlDocument;
        }

        private XmlDocument _rss = null;

        public RSSCreate()
        {
            _rss = new XmlDocument();
            XmlDeclaration xmlDeclaration = _rss.CreateXmlDeclaration("1.0", "utf-8", null);
            _rss.InsertBefore(xmlDeclaration, _rss.DocumentElement);
            XmlElement rssElement = _rss.CreateElement("rss");
            XmlAttribute rssVersionAttribute = _rss.CreateAttribute("version");
            rssVersionAttribute.InnerText = "2.0";
            rssElement.Attributes.Append(rssVersionAttribute);
            _rss.AppendChild(rssElement);
        }

        public void AddRssChannel(RssChannel channel) { _rss = addRssChannel(_rss, channel); }
        public void AddRssItem(RssItem item) { _rss = addRssItem(_rss, item); }
        public string RssDocument { get { return _rss.OuterXml; } }
        public XmlDocument RssXmlDocument { get { return _rss; } }

        ////tạo rss document và tạo channel
        //CreateRSS rss1 = new CreateRSS();

        ////tao channel
        //RssChannel channel = new RssChannel();  
        //channel.Title = "music";
        //channel.Link = "http://yoursite.com/News/YourNewsCategory/";
        //channel.Description = "music online";
        //rss1.AddRssChannel(channel);

        public void CreateChannel(string title, string link, string description)
        {
            RssChannel channel = new RssChannel();
            channel.Title = title;
            channel.Link = link;
            channel.Description = description;
            this.AddRssChannel(channel);
        }

        ////rss item
        //RssItem item = new RssItem();
        //item.Title = "hoangvuIT";
        //item.Link = "http://yoursite.com/News/YourNewsCategory/YourNewsId/";
        //item.Description = "lap trinh vien cui mia";
        //rss1.AddRssItem(item);

        public void CreateItem(string title, string link, string description, DateTime Date)
        {
            RssItem item = new RssItem();
            item.PublicationDate = Date;
            item.Title = title;
            item.Link = link;
            item.Description = description;
            this.AddRssItem(item);
        }

        ////write cái Rss trên thành Xml document ra Response object tại cái ASPX page tương tác với bên ngoài
        //Response.Clear();
        //Response.ContentType = "text/xml";
        //Response.Write(rss1.RssDocument);
        //Response.End();//tạo rss document và tạo channel
    }
}
