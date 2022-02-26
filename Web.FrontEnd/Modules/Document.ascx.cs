namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.Provider.Cache;
    using Asp.UI;
    using Business;
    using Library;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Document : VITModule
    {
        private ArticleBLL _bll;
        private ItemBLL _itemBLL;

        protected DocumentModel dto;
        
        protected bool DisplayTitle { get; set; }
        protected bool DisplayImage { get; set; }
        
        private int _numberPost;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._bll = new ArticleBLL();
            this._itemBLL = new ItemBLL();

            this.DisplayTitle = this.GetValueParam<bool>("DisplayTitle");
            this.DisplayImage = this.GetValueParam<bool>("DisplayImage");

            var id = this.GetValueParam<int>("DocumentId");
            if (id == 0) id = this.GetRequestThenParam<int>(SettingsManager.Constants.SendDocument, "DocumentId");

            this.dto = CacheProvider.GetCache<DocumentModel>(CacheProvider.Keys.Doc, this.Config.ID, id, this.Config.Language);
            if (this.dto == null)
            {
                this.dto = this._bll.GetDocument(this.Config.ID, this.Config.Language, id);
                if (dto == null)
                {
                    if (this.GetValueParam<bool>("ErrorIfNull")) HREF.RedirectComponent("Errors", "PageNotFound");
                    else dto = new DocumentModel();
                }
                else
                {
                    dto.PathImage = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathDocumentFile + dto.IMAGE;
                    if (string.IsNullOrEmpty(dto.FileUrl))
                        dto.FileUrl = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathDocumentFile + dto.FileName;
                }

                CacheProvider.SetCache(this.dto, CacheProvider.Keys.Art, this.Config.ID, id, this.Config.Language);
            }

            if (dto != null && dto.ID > 0 && this.GetValueParam<bool>("IsUpdateView")) this._itemBLL.UpView(id, this.Config.ID);
            
            // over-write skin title by category title
            if (this.GetValueParam<bool>("IsOverWriteTitle"))
            {
                this.Title = dto.TITLE;
            }
        }
    }
}