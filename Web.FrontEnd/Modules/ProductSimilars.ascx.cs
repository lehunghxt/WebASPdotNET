namespace Web.FrontEnd.Modules
{
    using System;
    using System.Collections.Generic;
    using Web.Asp.Provider;
    using Web.Asp.Provider.Cache;
    using Web.Asp.UI;
    using Web.Business;
    using Web.Model;

    /// <summary>
    /// The products.
    /// </summary>
    public partial class ProductSimilars : VITModule
    {
        /// <summary>
        /// The _product bll.
        /// </summary>
        private ProductBLL _productBLL;

        private int _startRowIndex;
        protected bool _hasPaging;
        private int _productId;

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
        protected ProductModel Product { get; set; }

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

            this.pager.PageSize = this.GetValueParam<int>("Top"); 
            this._hasPaging = this.GetValueParam<bool>("HasPaging");
            this._startRowIndex = this.pager != null ? this.pager.StartRowIndex : 0;
            this._productId = this.GetRequestThenParam<int>(SettingsManager.Constants.SendProduct, "ProductId");

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

            this.pager.Visible = this._hasPaging;
            if (this.pager.PageSize == 0)
            {
                this.pager.Visible = false;
            }

            var data = CacheProvider.GetCache<IList<ProductWebModel>>(
                CacheProvider.Keys.Pro,
                this.Config.ID,
                this._productId,
                this._startRowIndex,
                this.pager.PageSize,
                this.Config.Language, 
                this.ShowMinPrice);

            var totalItem = CacheProvider.GetCache<int>(
                CacheProvider.Keys.ProCount,
                this._productId,
                this.Config.ID,
                this._startRowIndex,
                this.pager.PageSize,
                this.Config.Language, 
                this.ShowMinPrice);

            if (data == null || totalItem == 0)
            {
                data = this._productBLL.GetListProductTheSameCategory( 
                    this._productId, this.Config.ID,
                    out totalItem,
                    this._startRowIndex,
                    this.pager.PageSize,
                    this.Config.Language, 
                    this.ShowMinPrice);
                foreach (var product in data)
                {
                    product.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + product.ImageName;
                }
                CacheProvider.SetCache(
                    data,
                    CacheProvider.Keys.Pro,
                     this.Config.ID,
                    this._productId,
                    this._startRowIndex,
                    this.pager.PageSize,
                    this.Config.Language, 
                    this.ShowMinPrice);

                CacheProvider.SetCache(
                    totalItem,
                    CacheProvider.Keys.ProCount,
                    this.Config.ID,
                    this._productId,
                    this._startRowIndex,
                    this.pager.PageSize,
                    this.Config.Language, 
                    this.ShowMinPrice);
            }

            this.rpt.DataSource = data;
            this.rpt.DataBind();

            this.pager.TotalRowCount = totalItem;

            Product = CacheProvider.GetCache<ProductModel>(CacheProvider.Keys.Pro, this.Config.ID, this._productId, this.Config.Language);
            if (Product == null)
            {
                Product = _productBLL.GetProduct(this.Config.ID, this.Config.Language, _productId);
            }

            if (Product != null)
            {
                Product.PathImage = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + Product.Image;
                if (this.GetValueParam<bool>("OverWriteTitle")) this.Title = Product.CategoryName;
                CacheProvider.SetCache(Product, CacheProvider.Keys.Pro, this.Config.ID, this._productId, this.Config.Language);
            }
        }
    }
}