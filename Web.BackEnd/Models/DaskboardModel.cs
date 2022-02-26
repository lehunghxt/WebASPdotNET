namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class DaskboardModel
    {
        public int BinhLuanMoi { get; set; }
        public ThongKeModel ThongKe { get; set; }

        public IList<ThongKeLoiNhuanModel> ThongKeBanHang { get; set; }
        public string ThongKeJson { get; set; }

        public DaskboardModel()
        {
            ThongKe = new ThongKeModel();
            ThongKeBanHang = new List<ThongKeLoiNhuanModel>();
        }
    }
}
