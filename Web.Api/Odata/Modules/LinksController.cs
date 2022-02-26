namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class LinksController : OdataBaseController<ARTICLEURLModel, int>
    {
        private ArticleBLL bll;
        public LinksController()
        {
            this.bll = new ArticleBLL();
        }
        [EnableQuery]
        public override IQueryable<ARTICLEURLModel> Get()
        {
            var data = this.bll.GetLinks(this.Web.ID, this.Web.Language).Where(e => e.PUBLISH).OrderBy(e => e.ORDERS).AsQueryable();
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
                data = data.Where(c => listCategory.Contains(c.CATEGORYID));
            }

            var top = 0;
            if (param.ContainsKey("Top")) int.TryParse(param["Top"], out top);
            if (top > 0) data = data.Take(top);

            var listdata = data.ToList();
            foreach (var item in listdata)
            {
                item.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathArticleImage + item.IMAGE;
            }

            return data;
        }

    }
}
