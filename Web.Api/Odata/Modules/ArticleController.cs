namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class ArticleController : OdataBaseController<ARTICLELANGUAGEModel, int>
    {
        private ArticleBLL bll;
        private ItemBLL itemBLL;
        public ArticleController()
        {
            this.bll = new ArticleBLL();
            itemBLL = new ItemBLL();
        }
        [EnableQuery]
        public override IQueryable<ARTICLELANGUAGEModel> Get()
        {
            var param = this.GetParameter();
            var id = 0;
            if (param.ContainsKey("Id")) int.TryParse(param["Id"], out id);

            var data = this.bll.GetArticle(this.Web.ID, this.Web.Language, id);

            var upView = false;
            if (param.ContainsKey("UpView")) bool.TryParse(param["UpView"], out upView);
            if (upView) itemBLL.UpView(id, this.Web.ID);

            if (data != null)
            {
                data.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathArticleImage + data.IMAGE;
            }

            return new List<ARTICLELANGUAGEModel> { data }.AsQueryable();
        }

    }
}
