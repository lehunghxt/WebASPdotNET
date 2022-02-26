
namespace Web.Model
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class MediaModel
    {
		public int ID { get; set; }
        public int CategoryId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }

        public string LANGUAGEID { get; set; }
        public string TITLE { get; set; }
		public string BRIEF { get; set; }
                
        public string Languages { get; set; }

        public string Embed { get; set; }

        public int Views { get; set; }
        public int Comments { get; set; }

        public string FilePath { get; set; }

        public string Poster { get; set; }
        public string PosterPath { get; set; }
    }
}  
