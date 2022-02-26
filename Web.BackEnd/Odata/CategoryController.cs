namespace Web.Backend.Odata
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;

    public class CategoryController : OdataBaseController<CATEGORYLANGUAGEModel, int>
    {
        private ArticleBLL bll;
        public CategoryController()
        {
            this.bll = new ArticleBLL();
        }
        [EnableQuery]
        public override IQueryable<CATEGORYLANGUAGEModel> Get()
        {
            var param = this.GetParameter();
            var catid = 0;
            if (param.ContainsKey("CategoryId")) int.TryParse(param["CategoryId"], out catid);
             var data = this.bll.GetCategoryByType(this.User.CompanyId, param["Language"], param["Type"], catid);
            return data;
        }

        protected override CATEGORYLANGUAGEModel GetEntityByKey(int id)
        {
            var param = this.GetParameter();
            var data = this.bll.GetCategoryById(this.User.CompanyId, param["Language"], id);
            return data;
        }
    }
}
