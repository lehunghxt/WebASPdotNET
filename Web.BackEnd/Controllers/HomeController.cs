namespace Web.Backend.Controllers
{
    using Library;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Web.Backend.Models;
    using Web.Business;

    public class HomeController : BaseController
    {
        private readonly ProductBLL productBLL;
        private readonly ItemBLL itemBLL;

        public HomeController()
        {
            this.productBLL = new ProductBLL();
            itemBLL = new ItemBLL();
        }

        public ActionResult Index()
        {
            var model = new DaskboardModel();

            model.BinhLuanMoi = itemBLL.CountNewComment(this.User.CompanyId, 3);
            model.ThongKe = productBLL.GetThongKe(this.User.CompanyId, 5);

            model.ThongKeBanHang = productBLL.GetStatisticCurrent(this.User.CompanyId, 12);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            model.ThongKeJson = jss.Serialize(model.ThongKeBanHang);
            return View(model);
        }
    }
}