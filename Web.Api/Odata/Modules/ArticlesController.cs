namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class ArticlesController : OdataBaseController<ARTICLELANGUAGEModel, int>
    {
        private ArticleBLL bll;
        public ArticlesController()
        {
            this.bll = new ArticleBLL();
        }
        [EnableQuery]

        [CacheWebApi(Duration = 3600)]
        public override IQueryable<ARTICLELANGUAGEModel> Get()
        {
            var query = this.bll.GetAllArticles(this.Web.ID, this.Web.Language).Where(e => e.PUBLISH).OrderBy(e => e.ORDERS).AsQueryable();
            var param = this.GetParameter();

            var categoryId = 0;
            if (param.ContainsKey("CategoryId")) int.TryParse(param["CategoryId"], out categoryId);
            if (categoryId > 0)
            {
                IList<int> listCategory = new List<int>();
                var inChildCat = false;
                if (param.ContainsKey("InChildCat")) bool.TryParse(param["InChildCat"], out inChildCat);
                if (inChildCat)
                {
                    listCategory = bll.GetAllChildId(categoryId, this.Web.ID);
                }

                listCategory.Add(categoryId);
                query = query.Where(c => listCategory.Contains(c.CATEGORYID));
            }

            var top = 0;
            if (param.ContainsKey("Top")) top = Convert.ToInt32(param["Top"]);
            if (top > 0) query = query.Take(top);

            var data = query.ToList();
            foreach(var item in data)
            {
                item.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathArticleImage + item.IMAGE;
            }

            return data.AsQueryable();
        }

    }
}
