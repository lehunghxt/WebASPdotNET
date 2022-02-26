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
    public partial class Product : VITModule
    {
        /// <summary>
        /// The _product bll.
        /// </summary>
        private ProductBLL _productBLL;
        private ItemBLL _itemBLL;

        private int _productId;
        private bool _isOverwriteTitle;

        protected int WidthImage { get; set; }
        protected int HeightImage { get; set; }
        protected int VoteNumber { get; set; }
        protected bool DisplayCode { get; set; }
        protected bool DisplayCorlor { get; set; }
        protected bool DisplayTitle { get; set; }
        protected bool DisplayOrder { get; set; }

        protected List<string> Tags { get; set; }

        protected ProductModel dto;
        public IList<ProductPriceModel> Prices { get; set; }
        public IList<ProductColorModel> Colors { get; set; }
        public IList<ProductAttributeModel> ProductAttributes { get; set; }
        public IList<ProductAttributeModel> OrderProperties { get; set; }
        public IList<ItemImageModel> Images { get; set; }
        public IList<ProductAddOnModel> AddOns { get; set; }
        public IList<ProductAddOnModel> Relatied { get; set; }
        public GrouponModel Groupon { get; set; }

        protected int CountImageDetails { get; set; }

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
            this._itemBLL = new ItemBLL();

            this._productId = this.GetRequestThenParam<int>(SettingsManager.Constants.SendProduct, "ProductId");
            this.WidthImage = this.GetValueParam<int>("WidthImage");
            this.HeightImage = this.GetValueParam<int>("HeightImage");
            this._isOverwriteTitle = this.GetValueParam<bool>("IsOverWriteTitle");
            this.DisplayCode = this.GetValueParam<bool>("DisplayCode");
            this.DisplayCorlor = this.GetValueParam<bool>("DisplayCorlor");
            this.DisplayTitle = this.GetValueParam<bool>("DisplayTitle");
            DisplayOrder = this.GetValueParam<bool>("DisplayOrder");
            this.VoteNumber = this.GetValueParam<int>("VoteNumber");

            var companyId = this.GetValueRequest<int>(SettingsManager.Constants.SendCompany);
            if (companyId == 0) companyId = this.Config.ID;

            this.dto = CacheProvider.GetCache<ProductModel>(CacheProvider.Keys.Pro, this.Config.ID, this._productId, this.Config.Language);
            if (this.dto == null)
            {
                this.dto = _productBLL.GetProduct(this.Config.ID, this.Config.Language, _productId);
                if (dto == null) HREF.RedirectComponent("PageNotFound");
                dto.PathImage = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + dto.Image;
                
                CacheProvider.SetCache(this.dto, CacheProvider.Keys.Pro, this.Config.ID, this._productId, this.Config.Language);
            }

            this.Groupon = _productBLL.GetGroupon(this.Config.ID, this._productId);

            this.Tags = this.HREF.LinkTag("Products", dto.Tag);

            this.Prices = CacheProvider.GetCache<List<ProductPriceModel>>(CacheProvider.Keys.Obj, this._productId);
            if (this.Prices == null || Prices.Count == 0)
            {
                this.Prices = this._productBLL.GetAllProducePrices(new List<int> { dto.ID }).ToList();
                CacheProvider.SetCache(this.Prices, CacheProvider.Keys.Obj, this._productId);
            }

            this.Colors = _productBLL.GetProduceColors(this._productId).ToList();
            foreach (var color in this.Colors)
            {
                color.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + color.ImageName;
            }

            this.Images = _itemBLL.GetImages(this._productId, this.Config.ID).ToList();
            foreach (var image in this.Images)
            {
                image.PathImage = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + image.Image;
            }

            ProductAttributes = new List<ProductAttributeModel>();
            var displayAttribute = this.GetValueParam<string>("Attributes");
            OrderProperties = new List<ProductAttributeModel>();
            var sendProperties = this.GetValueParam<string>("SendProperties");
            if (!string.IsNullOrEmpty(displayAttribute) || !string.IsNullOrEmpty(sendProperties))
            {
                var attributes = _productBLL.GetProductAttributes(this._productId, this.Config.ID).ToList();
               
                if (!string.IsNullOrEmpty(displayAttribute))
                {
                    var atts = displayAttribute.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    ProductAttributes = attributes.Where(o => atts.Contains(o.ID.ToString())).ToList();
                }
                if (!string.IsNullOrEmpty(sendProperties))
                {
                    var atts = sendProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    OrderProperties = attributes.Where(o => atts.Contains(o.ID.ToString())).ToList();
                }
            }            

            this.AddOns = _productBLL.GetListProductAddOns(this._productId, this.Config.ID, this.Config.Language);
            foreach (var addon in this.AddOns)
            {
                addon.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + addon.ImagePath;
            }

            this.Relatied = _productBLL.GetListRelatiedProducts(this._productId, this.Config.ID, this.Config.Language);
            foreach (var relatied in this.Relatied)
            {
                relatied.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + relatied.ImagePath;
            }

            if (!Page.IsPostBack)
            {
                this._itemBLL.UpView(this._productId, Config.ID);
            }

            // over-write skin title by category title
            if (this._isOverwriteTitle)
            {
                this.Title = dto.Title;
            }

            if (this.dto != null)
            {
                var daxem = Session[SettingsManager.Constants.SessionDaXem + Config.ID] as List<ProductModel>;
                if (daxem == null) daxem = new List<ProductModel>();
                if (!daxem.Any(o => o.ID == dto.ID))
                {
                    daxem.Insert(0, dto);
                    Session[SettingsManager.Constants.SessionDaXem + Config.ID] = daxem;
                }
            }
        }
    }
}