namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class MediasController : OdataBaseController<MediaModel, int>
    {
        private FileBLL bll;
        private ArticleBLL articleBLL;
        public MediasController()
        {
            this.bll = new FileBLL();
            articleBLL = new ArticleBLL();
        }
        [EnableQuery]
        public override IQueryable<MediaModel> Get()
        {
            var param = this.GetParameter();

            var categoryId = 0;
            if (param.ContainsKey("CategoryId")) int.TryParse(param["CategoryId"], out categoryId);

            var query = this.bll.GetMedias(this.Web.ID, this.Web.Language, categoryId);
            if (categoryId > 0)
            {
                IList<int> listCategory = new List<int>();
                var inChildCat = false;
                if (param.ContainsKey("InChildCat")) bool.TryParse(param["InChildCat"], out inChildCat);
                if (inChildCat)
                {
                    listCategory = articleBLL.GetAllChildId(categoryId, this.Web.ID);
                }

                listCategory.Add(categoryId);
                query = query.Where(c => listCategory.Contains(c.CategoryId));
            }

            var top = 0;
            if (param.ContainsKey("Top")) int.TryParse(param["Top"], out top);
            if (top > 0) query = query.Take(top);

            var data = query.ToList();
            foreach(var file in data)
            {
                if (string.IsNullOrEmpty(file.FilePath))
                    file.FilePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathMediaFile + file.FileName;
                if (!string.IsNullOrEmpty(file.Poster))
                    file.PosterPath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathMediaFile + file.Poster;
            }

            return data.AsQueryable();
        }

    }
}
