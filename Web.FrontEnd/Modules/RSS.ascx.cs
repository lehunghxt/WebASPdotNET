using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Asp.Provider;
using Web.Asp.Provider.Cache;
using Web.Asp.UI;
using Web.Business;
using Web.Model;
using Library.Web.RSS;
using Library;

namespace Web.FrontEnd.Modules
{
    public partial class RSS : VITModule
    {
        private ArticleBLL articleBll;
        private ProductBLL productBLL;

        private string RederectComponent { get; set; }
        private string RederectSendKey { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.articleBll = new ArticleBLL(); productBLL = new ProductBLL();

            var top = this.GetValueParam<int>("Top");
            var categoryId = this.GetRequestThenParam<int>(SettingsManager.Constants.SendCategory, "CategoryId");
            var source = this.GetValueParam<string>("Source");
            var order = this.GetRequestThenParam<string>("sort", "OrderBy");

            var category = LoadCategory(categoryId);
            var data = this.GetData(source, categoryId, top, order);

            //tạo rss document và tạo channel
            RSSCreate rss = new RSSCreate();

            //tao channel
            RssChannel channel = new RssChannel();
            channel.Title = category.Title;
            channel.Link = category.URL;
            channel.Description = category.Description;
            channel.Language = this.Config.Language;
            channel.Copyright = HREF.Domain;
            channel.Generator = HREF.Domain;
            channel.PublicationDate = category.CreateDate;
            rss.AddRssChannel(channel);

            foreach (var item in data)
            {
                var link = item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign());
                if (!link.Contains(HREF.Domain)) link = HREF.GetScheme() + "://" + HREF.Domain + link;

                //rss item
                RssItem rssItem = new RssItem();
                rssItem.Title = item.Title;
                rssItem.Link = link;
                rssItem.Guid = link;
                rssItem.Description = item.Description;
                rssItem.PublicationDate = item.CreateDate;
                rssItem.Author = HREF.Domain;
                rss.AddRssItem(rssItem);
            }

            Response.Clear();
            Response.ContentType = "text/xml";
            Response.Write(rss.RssDocument);
            Response.End();//tạo rss document và tạo channel
        }

        private DataSimpleModel LoadCategory(int categoryId)
        {
            var Category = CacheProvider.GetCache<DataSimpleModel>(CacheProvider.Keys.Obj, Config.ID, "Category", Config.Language, categoryId);
            if (Category == null)
            {
                Category = this.articleBll.GetDataSingle(
                            Config.ID,
                            Config.Language,
                            "CAT",
                            categoryId);
                if (Category == null)
                {
                    Category = new DataSimpleModel();
                    Category.Title = Category.Description = "Chua co noi dung voi ngon ngu '" + Config.Language + "'";
                }
                CacheProvider.SetCache(Category, CacheProvider.Keys.Obj, Config.ID, "Category", Config.Language, categoryId);
            }
            
            switch (Category.TargetTag)
            {
                case "ART":
                    Category.URL = HREF.LinkComponent("Articles", SettingsManager.Constants.SendCategory + "/" + categoryId + "/" + Category.Title.ConvertToUnSign());
                    break;

                case "PRO":
                    Category.URL = HREF.LinkComponent("Products", SettingsManager.Constants.SendCategory + "/" + categoryId + "/" + Category.Title.ConvertToUnSign());
                    break;

                case "MID":
                    Category.URL = HREF.LinkComponent("Album", SettingsManager.Constants.SendCategory + "/" + categoryId + "/" + Category.Title.ConvertToUnSign());
                    break;

                case "DOC":
                    Category.URL = HREF.LinkComponent("Documents", SettingsManager.Constants.SendCategory + "/" + categoryId + "/" + Category.Title.ConvertToUnSign());
                    break;
            }

            return Category;
        }

        /// <summary>
        /// Get Data
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="categoryId">
        /// The category Id.
        /// </param>
        public List<DataSimpleModel> GetData(string source, int categoryId, int top, string order)
        {
            var Data = CacheProvider.GetCache<List<DataSimpleModel>>(CacheProvider.Keys.Obj, Config.ID, source, Config.Language, categoryId, true, 0, top);
            var totalRow = CacheProvider.GetCache<int>(CacheProvider.Keys.ObjCount, Config.ID, source, Config.Language, categoryId, true, 0, top);
            if (Data == null || Data.Count == 0 || totalRow == 0)
            {
                Data = new List<DataSimpleModel>();

                switch (source)
                {
                    case "PRO":
                        Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            true, top, order).ToList();
                        foreach (var item in Data)
                        {
                            if (!string.IsNullOrEmpty(item.ImagePath))
                                item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + item.ImagePath;
                        }
                        break;
                    case "ATR":
                        Data = this.productBLL.GetSimpleAttributes(Config.ID, categoryId).ToList();
                        break;
                    case "COR":
                        var colors = productBLL.GetColorByCategoryId(Config.ID, categoryId, true).ToList();
                        var datas = colors.Select(e => new DataSimpleModel
                        {
                            ID = e.Id,
                            Title = e.Name,
                            ImagePath = e.ImageName,
                            Description = e.Value,
                            CategoryId = categoryId
                        }).ToList();
                        foreach (var color in datas)
                        {
                            if (!Data.Any(e => e.Description == color.Description))
                            {
                                color.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathColorImage + color.ImagePath;
                                Data.Add(color);
                            }
                            else
                            {
                                var item = Data.First(e => e.Description == color.Description);
                                if (string.IsNullOrEmpty(item.Title) && !string.IsNullOrEmpty(color.Title))
                                {
                                    Data.Remove(item);
                                    Data.Add(color);
                                }
                            }
                        }
                        break;
                    case "PTG":
                        var arrP = productBLL.GetTagByCategoryId(Config.ID, categoryId, true).Distinct();

                        foreach (var onetag in arrP)
                        {
                            var item = new DataSimpleModel();
                            item.Title = onetag;
                            item.CategoryId = categoryId;
                            Data.Add(item);
                        }
                        break;
                    case "ATG":
                        var arrA = articleBll.GetTagByCategoryId(Config.ID, categoryId, true);
                        foreach (var onetag in arrA)
                        {
                            var item = new DataSimpleModel();
                            item.Title = onetag;
                            item.CategoryId = categoryId;
                            Data.Add(item);
                        }
                        break;
                    case "ART":
                        Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            true, top, order).ToList();
                        foreach (var item in Data)
                        {
                            if (!string.IsNullOrEmpty(item.ImagePath))
                                item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
                        }
                        break;
                    case "CAT":
                        Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            true, top).ToList();
                        foreach (var item in Data)
                        {
                            if (!string.IsNullOrEmpty(item.ImagePath))
                                item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCategoryImage + item.ImagePath;
                        }
                        break;
                    case "LIN":
                        Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            true, top, order).ToList();
                        foreach (var item in Data)
                        {
                            if (!string.IsNullOrEmpty(item.ImagePath))
                                item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
                        }
                        break;
                    case "MID":
                        Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            true, top,
                            order).ToList();
                        foreach (var item in Data)
                        {
                            if (!string.IsNullOrEmpty(item.ImagePath))
                                item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathMediaFile + item.ImagePath;
                        }
                        break;
                }

                CacheProvider.SetCache(Data, CacheProvider.Keys.Obj, Config.ID, source, Config.Language, categoryId, true, 0, top);
                CacheProvider.SetCache(totalRow, CacheProvider.Keys.ObjCount, Config.ID, source, Config.Language, categoryId, true, 0, top);
            }

            switch (source)
            {
                case "PRO":
                    this.RederectComponent = "Product";
                    this.RederectSendKey = SettingsManager.Constants.SendProduct;
                    break;
                case "COR":
                    this.RederectComponent = "Products";
                    this.RederectSendKey = SettingsManager.Constants.SendColor;
                    break;
                case "ART":
                    this.RederectComponent = "Article";
                    this.RederectSendKey = SettingsManager.Constants.SendArticle;
                    break;
                case "PTG":
                    this.RederectComponent = "Products";
                    this.RederectSendKey = SettingsManager.Constants.SendTag;
                    break;
                case "ATG":
                    this.RederectComponent = "Articles";
                    this.RederectSendKey = SettingsManager.Constants.SendTag;
                    break;
                case "CAT":
                    var type = CacheProvider.GetCache<string>(CacheProvider.Keys.CatType, Config.ID, Config.Language, categoryId);
                    if (string.IsNullOrEmpty(type))
                    {
                        var cat = this.articleBll.GetCategoryById(Config.ID, Config.Language, categoryId);
                        if (cat != null)
                        {
                            type = cat.TYPEID;
                            CacheProvider.SetCache(type, CacheProvider.Keys.CatType, Config.ID, Config.Language, categoryId);
                        }
                    }

                    switch (type)
                    {
                        case "ART":
                            this.RederectComponent = "Articles";
                            this.RederectSendKey = SettingsManager.Constants.SendCategory;
                            break;

                        case "PRO":
                            this.RederectComponent = "Products";
                            this.RederectSendKey = SettingsManager.Constants.SendCategory;
                            break;

                        case "MID":
                            this.RederectComponent = "Album";
                            this.RederectSendKey = SettingsManager.Constants.SendCategory;
                            break;

                        case "DOC":
                            this.RederectComponent = "Documents";
                            this.RederectSendKey = SettingsManager.Constants.SendCategory;
                            break;
                    }

                    break;
                case "LIN":
                    break;
                case "MID":
                    this.RederectComponent = "Album";
                    this.RederectSendKey = SettingsManager.Constants.SendMedia;

                    break;
                case "DOC":
                    this.RederectComponent = "Document";
                    this.RederectSendKey = SettingsManager.Constants.SendArticle;

                    break;
            }

            return Data;
        }
    }
}