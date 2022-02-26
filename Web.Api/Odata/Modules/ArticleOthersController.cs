namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class ArticleOthersController : OdataBaseController<ARTICLELANGUAGEModel, int>
    {
        private ArticleBLL bll;
        public ArticleOthersController()
        {
            this.bll = new ArticleBLL();
        }
        [EnableQuery]
        public override IQueryable<ARTICLELANGUAGEModel> Get()
        {
            var param = this.GetParameter();

            var id = 0;
            if (param.ContainsKey("Id")) int.TryParse(param["Id"], out id);
            var top = 0;
            if (param.ContainsKey("Top")) int.TryParse(param["Top"], out top);
            var data = new List<ARTICLELANGUAGEModel>();

            var query = this.bll.GetOtherArticles(id, this.Web.ID, this.Web.Language, top);
            if(query != null) data = query.Where(e => e.PUBLISH).OrderBy(e => e.ORDERS).ToList();
            foreach(var item in data)
            {
                item.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathArticleImage + item.IMAGE;
            }

            return data.AsQueryable();
        }

    }
}
