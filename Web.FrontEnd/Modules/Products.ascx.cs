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
    public partial class Products : VITModule
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
        protected string Search { get; set; }

        protected IList<DataSimpleModel> Categories;
        protected CATEGORYLANGUAGEModel Category;
        
        protected IList<ProductAttributeModel> Properties { get; set; }
        protected IList<ProductAttributeModel> OrderProperties { get; set; }
        protected IList<ProductColorModel> Colors { get; set; }

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
            Search = this.GetValueRequest<string>("key");
            if (string.IsNullOrEmpty(Search)) Search = this.GetValueRequest<string>(SettingsManager.Constants.SendTag);

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

            var data = CacheProvider.GetCache<IList<ProductWebModel>>(
          CacheProvider.Keys.Pro,
          this.Config.ID,
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
          Search);

            var totalItem = CacheProvider.GetCache<int>(
                CacheProvider.Keys.ProCount,
                this.Config.ID,
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
          Search);

            if (data == null || data.Count == 0 || totalItem == 0)
            {
                data = this._productBLL.GetListProductWithImageAndPriceAndVote(
                this.Config.ID,
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
          Search);
                foreach (var product in data)
                {
                    product.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + product.ImageName;
                }
                CacheProvider.SetCache(
                    data,
                    CacheProvider.Keys.Pro,
                    this.Config.ID,
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
          Search);

                CacheProvider.SetCache(
                    totalItem,
                    CacheProvider.Keys.ProCount,
                    this.Config.ID,
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
          Search);
            }

            this.rpt.DataSource = data;
            this.rpt.DataBind();
            this.pager.TotalRowCount = totalItem;


            Category = CacheProvider.GetCache<CATEGORYLANGUAGEModel>(CacheProvider.Keys.Cat, this.Config.ID, this._categoryId, this.Config.Language);
            if (Category == null)
            {
                Category = this.articleBLL.GetCategoryById(this.Config.ID, this.Config.Language, _categoryId);
                if (this.Category == null)
                {
                    this.Category = new CATEGORYLANGUAGEModel();
                    this.Category.NAME = this.Category.DESCRIPTION = "Chua co noi dung voi ngon ngu '" + Config.Language + "'";
                }
                CacheProvider.SetCache(Category, CacheProvider.Keys.Cat, this.Config.ID, this._categoryId, this.Config.Language);
            }
            

            // over-write skin title by category title
            if (this.GetValueParam<bool>("OverWriteTitle"))
            {
                if (Category != null && !string.IsNullOrEmpty(Category.NAME)) this.Title = Category.NAME;
                if (this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue) > 0 && this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeId) > 0)
                {
                    var attributeCategory = _productBLL.GetAttributeCategory(this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeId), this.Config.ID);
                    if (attributeCategory != null) Title = attributeCategory.Name;
                    var attribute = _productBLL.GetAttributeValue(this.Config.ID, this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue));
                    if (attribute != null) Title = Title + ": " + attribute.Value;
                }
            }

            var sendProperties = this.GetValueParam<string>("SendProperties");
            if (!string.IsNullOrEmpty(sendProperties))
            {
                this.Properties = _productBLL.GetAttributeByCategoryId(this.Config.ID, this._categoryId, _inChildCategory);
                OrderProperties = new List<ProductAttributeModel>();

                var atts = sendProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                OrderProperties = Properties.Where(o => atts.Contains(o.ID.ToString())).ToList();

                this.Colors = _productBLL.GetColorByCategoryId(this.Config.ID, this._categoryId, _inChildCategory).ToList();
                foreach (var color in Colors)
                {
                    color.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathColorImage + color.ImageName;
                }
            }
        }
    }
}