namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class CategoriesController : OdataBaseController<CATEGORYLANGUAGEModel, int>
    {
        private ArticleBLL bll;
        public CategoriesController()
        {
            this.bll = new ArticleBLL();
        }
        [EnableQuery]
        public override IQueryable<CATEGORYLANGUAGEModel> Get()
        {
            var data = this.bll.GetCategoryByType(this.Web.ID, this.Web.Language).Where(e => e.PUBLISH).OrderBy(e => e.ORDERS).AsQueryable();

            var param = this.GetParameter();
            var type = string.Empty;
            if(param.ContainsKey("CategoryType")) type = param["CategoryType"];
            if (!string.IsNullOrEmpty(type)) data = data.Where(e => e.TYPEID == type);

            var parentId = 0;
            if (param.ContainsKey("CategoryId")) int.TryParse(param["CategoryId"], out parentId);
            if (parentId > 0)
            {
                IList<int> listCategory = new List<int>();
                var inChildCat = false;
                if (param.ContainsKey("InChildCat")) bool.TryParse(param["InChildCat"], out inChildCat);
                if (inChildCat)
                {
                    listCategory = bll.GetAllChildId(parentId, this.Web.ID);
                }

                listCategory.Add(parentId);
                data = data.Where(c => listCategory.Contains(c.ID));
            }

            var listData = data.ToList();
            foreach (var item in listData)
            {
                item.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathCategoryImage + item.IMAGE;
            }

            return data;
        }

    }
}
