namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class ArticleCommentViewModel
    {
        public int ArticleId { get; set; }
        public int CommentId { get; set; }
        public IList<ITEMCOMMENTModel> Comments { get; set; }

        public ArticleCommentViewModel()
        {
            Comments = new List<ITEMCOMMENTModel>();
        }
    }
}
