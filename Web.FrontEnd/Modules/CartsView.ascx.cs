namespace Web.FrontEnd.Modules
{
    using Library;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Asp.Provider;
    using Web.Asp.UI;
    using Web.Business;
    using Web.Model;

    public partial class CartsView : VITModule
    {
        private ProductBLL _bll;
        private CompanyBLL _companyBLL;
        
        protected List<OrderProductModel> Carts { get; set; }
        protected List<ProductPriceModel> ProductPrices { get; set; }
        protected ConfigGHTKModel GHTK { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this._bll = new ProductBLL();
            this._companyBLL = new CompanyBLL();

            Carts = Session[SettingsManager.Constants.SessionGioHang + this.Config.ID] as List<OrderProductModel>;
            if (Carts == null) Carts = new List<OrderProductModel>();

            var productIds = Carts.Select(p => p.ProductId).Distinct().ToList();
            ProductPrices = this._bll.GetAllProducePrices(productIds).ToList();

            this.GHTK = _companyBLL.GetConfigGHTK(Config.ID);
        }

        protected decimal GetPrice(int productId, int quatity)
        {
            var price = this.ProductPrices.Where(e => e.ProductId == productId && e.Quantity < quatity)
                .OrderByDescending(e => e.Quantity)
                .Select(e => e.Price)
                .FirstOrDefault();

            return price;
        }
    }
}