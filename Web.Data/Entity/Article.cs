//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public Article()
        {
            this.ArticleLanguages = new HashSet<ArticleLanguage>();
            this.ArticleRelatieds = new HashSet<ArticleRelatied>();
        }
    
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public string Tag { get; set; }
        public Nullable<System.DateTime> DisplayDate { get; set; }
        public Nullable<bool> HasComment { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Item Item { get; set; }
        public virtual ICollection<ArticleLanguage> ArticleLanguages { get; set; }
        public virtual File File { get; set; }
        public virtual ICollection<ArticleRelatied> ArticleRelatieds { get; set; }
        public virtual ArticleLink ArticleLink { get; set; }
        public virtual Product Product { get; set; }
    }
}