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
    public partial class ProductSearch : VITModule
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

        protected IList<DataSimpleModel> Categories;
        protected ProductModel Product { get; set; }

        protected string _key = string.Empty;

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
            
            if (!string.IsNullOrEmpty(this.GetValueRequest<string>("k")))
            {
                var re = Request.RawUrl.Split(new string[] { "/k/" }, StringSplitOptions.RemoveEmptyEntries)[1];
                var de = Server.UrlDecode(re);
                this._key = de;
            }
            else if (!string.IsNullOrEmpty(this.GetValueRequest<string>("key")))
            {
                var re = Request.RawUrl.Split(new string[] { "/key/" }, StringSplitOptions.RemoveEmptyEntries)[1];
                var de = Server.UrlDecode(re);
                this._key = de;
            }

            this.pager.PageSize = this.GetValueParam<int>("Top"); 
            this._hasPaging = this.GetValueParam<bool>("HasPaging");
            this._startRowIndex = this.pager != null ? this.pager.StartRowIndex : 0;
            this._productId = this.GetRequestThenParam<int>(SettingsManager.Constants.SendProduct, "ProductId");

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

            int totalItem = 0;
            var data = this._productBLL.Search(  
                    this.Config.ID,
                    this.Config.Language,
                    _key,
                    out totalItem,
                    this._startRowIndex,
                    this.pager.PageSize,
                    this.ShowMinPrice);
                foreach (var product in data)
                {
                    product.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + product.ImageName;
                }

            this.rpt.DataSource = data;
            this.rpt.DataBind();

            this.pager.TotalRowCount = totalItem;
        }
    }
}