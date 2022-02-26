namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Vouchers : Web.Asp.UI.VITModule
    {
        private ProductBLL productBLL;

        protected List<VoucherModel> Data;

        protected void Page_Load(object sender, EventArgs e)
        {
            productBLL = new ProductBLL();
            Data = productBLL.GetVouchers(this.Config.ID).ToList();
        }
    }
}