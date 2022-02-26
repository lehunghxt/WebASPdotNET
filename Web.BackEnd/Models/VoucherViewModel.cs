namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class VoucherViewModel
    {
        public string Action { get; set; }
        public VoucherModel Voucher { get; set; }
        public IList<VoucherModel> Vouchers { get; set; }

        public VoucherViewModel()
        {
            Voucher = new VoucherModel();
            Vouchers = new List<VoucherModel>();
        }
    }
}
