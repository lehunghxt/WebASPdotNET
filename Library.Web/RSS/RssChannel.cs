using System;

namespace Library.Web.RSS
{
    public class RssChannel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public string ManagingEditor { get; set; }
        public string Webmaster { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Category { get; set; }
        public int TimeToLive { get; set; }
        public string Generator { get; set; }
    }
}
