namespace Web.Backend.Odata
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    public class ArticleController : OdataBaseController<ARTICLELANGUAGEModel, int>
    {
        private ArticleBLL bll;
        public ArticleController()
        {
            this.bll = new ArticleBLL();
        }
        [EnableQuery]
        public override IQueryable<ARTICLELANGUAGEModel> Get()
        {
            var param = this.GetParameter();
            var data = this.bll.GetAllArticles(this.User.CompanyId, param["Language"]);
            return data;
        }

        protected override ARTICLELANGUAGEModel GetEntityByKey(int id)
        {
            var param = this.GetParameter();
            var data = this.bll.GetArticle(this.User.CompanyId, param["Language"], id);
            return data;
        }
    }
}
