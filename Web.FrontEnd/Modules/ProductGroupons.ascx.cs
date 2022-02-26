namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.Provider.Cache;
    using Asp.UI;
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The products.
    /// </summary>
    public partial class ProductGroupons : VITModule
    {
        /// <summary>
        /// The _product bll.
        /// </summary>
        private ProductBLL _productBLL;
        private ArticleBLL articleBLL;
        private int _startRowIndex;
        protected bool _hasPaging;
        protected int _categoryId;
        private bool _inChildCategory;
        private string _orderBy;

        protected int ColumnCount { get; set; }

        protected int WidthImage { get; set; }
        protected int HeightImage { get; set; }
        protected int WidthProduct { get; set; }
        protected int HeightProduct { get; set; }
        protected int VoteNumber { get; set; }
        protected bool HasOrder { get; set; }
        protected bool HasPrice { get; set; }
        protected bool ShowMinPrice { get; set; }
        protected string ComponentDetail { get; set; }
        protected string Unit { get; set; }

        protected IList<DataSimpleModel> Categories;
        protected CATEGORYLANGUAGEModel Category;

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this._productBLL = new ProductBLL();
            articleBLL = new ArticleBLL();
            this.pager.PageSize = this.GetValueParam<int>("Top");
            this._hasPaging = this.GetValueParam<bool>("HasPaging");
            this._startRowIndex = this.pager != null ? this.pager.StartRowIndex : 0;
            this._inChildCategory = this.GetValueParam<bool>("InChildCategory");
            this._orderBy = this.GetRequestThenParam<string>("OrderBy", "OrderBy");
            
            this._categoryId = this.GetValueParam<int>("CategoryId");
            if (this.GetValueParam<bool>("PriorityCatRequest")) this._categoryId = this.GetRequestThenParam<int>(SettingsManager.Constants.SendCategory, "CategoryId");
            else this._categoryId = this.GetParamThenRequest<int>(SettingsManager.Constants.SendCategory, "CategoryId");

            this.ComponentDetail = this.GetValueParam<string>("ComponentDetail");
            this.Unit = this.GetValueParam<string>("Unit");
            this.ColumnCount = this.GetValueParam<int>("ColumnCount");
            this.WidthImage = this.GetValueParam<int>("WidthImage");
            this.HeightImage = this.GetValueParam<int>("HeightImage");
            this.WidthProduct = this.GetValueParam<int>("WidthProduct");
            this.HeightProduct = this.GetValueParam<int>("HeightProduct");
            this.VoteNumber = this.GetValueParam<int>("VoteNumber");
            this.HasOrder = this.GetValueParam<bool>("HasOrder");
            this.HasPrice = this.GetValueParam<bool>("HasPrice");
            this.ShowMinPrice = this.GetValueParam<bool>("ShowMinPrice");
            var search = this.GetValueRequest<string>("key");
            if (!string.IsNullOrEmpty(search)) this.GetValueRequest<string>(SettingsManager.Constants.SendTag);

            this.pager.Visible = this._hasPaging;
            if (this.pager.PageSize == 0)
            {
                this.pager.Visible = false;
            }

            this.Categories = CacheProvider.GetCache<IList<DataSimpleModel>>(CacheProvider.Keys.Cat, this.Config.ID, this._categoryId, "CAT", this.Config.Language, this._inChildCategory);
            if (Categories == null || Categories.Count == 0)
            {
                this.Categories = this.articleBLL.GetDataSimple(Config.ID,
                            Config.Language,
                            "CAT",
                            _categoryId,
                            _inChildCategory).ToList();
                CacheProvider.SetCache(this.Categories, CacheProvider.Keys.Pro, this.Config.ID, this._categoryId, "", this.Config.Language, this._inChildCategory);
            }

            var totalItem = 0;
               var data = this._productBLL.GetListGrouponWithImageAndPrice(
                this.Config.ID,
                DateTime.Now,
                out totalItem,
                this._startRowIndex,
                this.pager.PageSize, 
                this._categoryId,
                this._inChildCategory,
                this.GetRequestThenParam<int>(SettingsManager.Constants.SendProduct, "OrtherId"),
                this.GetRequestThenParam<string>(SettingsManager.Constants.SendColor, "ColorId"), 
                this.Config.Language,
                this.ShowMinPrice,
                this._orderBy,
          this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeId),
          this.GetValueRequest<string>(SettingsManager.Constants.SendAttributeValue),
          search);

                foreach (var product in data)
                {
                    product.PathImage = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + product.Image;
                }
                
            this.rpt.DataSource = data;
            this.rpt.DataBind();
            this.pager.TotalRowCount = totalItem;


            Category = CacheProvider.GetCache<CATEGORYLANGUAGEModel>(CacheProvider.Keys.Cat, this.Config.ID, this._categoryId, this.Config.Language);
            if (Category == null)
            {
                Category = this.articleBLL.GetCategoryById(this.Config.ID, this.Config.Language, _categoryId);
                CacheProvider.SetCache(Category, CacheProvider.Keys.Cat, this.Config.ID, this._categoryId, this.Config.Language);
            }
            // over-write skin title by category title
            if (this.GetValueParam<bool>("OverWriteTitle") && Category != null && !string.IsNullOrEmpty(Category.NAME)) this.Title = Category.NAME; 
        }
    }
}