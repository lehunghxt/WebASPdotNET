namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class DocumentsController : OdataBaseController<DocumentModel, int>
    {
        private ArticleBLL bll;
        public DocumentsController()
        {
            this.bll = new ArticleBLL();
        }
        [EnableQuery]
        public override IQueryable<DocumentModel> Get()
        {
            var query = this.bll.GetDocuments(this.Web.ID, this.Web.Language).Where(e => e.PUBLISH).OrderBy(e => e.ORDERS).AsQueryable();
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
            if (param.ContainsKey("Top")) int.TryParse(param["Top"], out top);
            if (top > 0) query = query.Take(top);

            var data = query.ToList();
            foreach(var item in data)
            {
                item.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathDocumentFile + item.FileName;
            }

            return data.AsQueryable();
        }

    }
}
