using System;

namespace Library.Web.RSS
{
    public class RssItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Guid { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Source { get; set; }
        public string Category { get; set; }
        public string Comments { get; set; }
    }
}
