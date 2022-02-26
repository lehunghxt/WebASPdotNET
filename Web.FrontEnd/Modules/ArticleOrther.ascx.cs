namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.UI;
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ArticleOrther : VITModule
    {
        private ArticleBLL _bll;

        public List<ARTICLELANGUAGEModel> Data { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this._bll = new ArticleBLL();

            Data = this._bll.GetOtherArticles(this.GetRequestThenParam<int>("sArt", "ArticleId"), this.Config.ID, this.Config.Language, this.GetValueParam<int>("Top")).ToList();
            foreach (var item in this.Data)
            {
                if (!string.IsNullOrEmpty(item.IMAGE))
                    item.PathImage = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + item.IMAGE;
            }
        }
    }
}