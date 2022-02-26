namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class MediaController : OdataBaseController<MediaModel, int>
    {
        private FileBLL bll;
        private ArticleBLL articleBLL;
        private ItemBLL itemBLL;
        public MediaController()
        {
            this.bll = new FileBLL();
            articleBLL = new ArticleBLL();
            itemBLL = new ItemBLL();
        }
        [EnableQuery]
        public override IQueryable<MediaModel> Get()
        {
            var param = this.GetParameter();

            var id = 0;
            if (param.ContainsKey("Id")) int.TryParse(param["Id"], out id);

            var data = this.bll.GetMedia(this.Web.ID, this.Web.Language, id);
            
            var upView = false;
            if (param.ContainsKey("UpView")) bool.TryParse(param["UpView"], out upView);
            if (upView) itemBLL.UpView(id, this.Web.ID);

            if(data != null)
            {
                if (string.IsNullOrEmpty(data.FilePath))
                    data.FilePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathMediaFile + data.FileName;
                if (!string.IsNullOrEmpty(data.Poster))
                    data.PosterPath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathMediaFile + data.Poster;
            }

            return new List<MediaModel> { data }.AsQueryable();
        }

    }
}
