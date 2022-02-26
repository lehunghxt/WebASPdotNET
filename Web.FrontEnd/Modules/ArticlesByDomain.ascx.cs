namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class ArticlesByDomain : Web.Asp.UI.VITModule
    {
        private ArticleBLL articleBll;
        private CompanyBLL companyBLL;
        
        protected List<ArticleModel> Data;

        public string Domain
        {
            get
            {
                return this.GetValueParam<string>("Domain");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.articleBll = new ArticleBLL();
            companyBLL = new CompanyBLL();

            var company = companyBLL.GetCompanyByDomain(Domain);
            if (company != null)
            {
                try
                {
                    this.Data = this.articleBll.GetArticles(
                                    company.ID,
                                    Config.Language,
                                    0,
                                    true,
                                    this.GetValueParam<string>("OrderBy"),
                                    string.Empty,
                                    this.GetValueParam<int>("Top")).ToList();
                    foreach (var item in this.Data)
                    {
                        if (!string.IsNullOrEmpty(item.ImagePath))
                            item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, company.ID) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
                    }
                }
                catch (Exception ex)
                {
                    this.Data = new List<ArticleModel>();
                        }
            }
        }
    }
}