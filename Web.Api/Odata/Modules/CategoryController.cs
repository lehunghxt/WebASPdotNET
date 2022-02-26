namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    [CacheWebApi(Duration = 3600)]
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
            var id = 0;
            if (param.ContainsKey("Id")) int.TryParse(param["Id"], out id);
            var data = this.bll.GetCategoryById(this.Web.ID, this.Web.Language, id);

            if (data != null)
            {
                data.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathCategoryImage + data.IMAGE;
            }

            return new List<CATEGORYLANGUAGEModel> { data }.AsQueryable();
        }

    }
}
