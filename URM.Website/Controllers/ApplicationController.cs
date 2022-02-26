namespace URM.Website.Controllers
{
    using Library;
    using System;
    using System.Web.Mvc;
    using URM.Business;
    using URM.Model;

    public class ApplicationController : Controller
    {
        private AppInfoBLL appBLL;
        private UserBLL userBLL;

        public ApplicationController()
        {
            appBLL = new AppInfoBLL();
            userBLL = new UserBLL();
        }

        public ActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        public ActionResult Create(URMAppCreateModel model)
        {
            var appId = 1;
            try
            {
                appId = appBLL.CreateAppInfo(model);

                try
                {
                    string chuoi = "<h2>Chào <strong>" + model.FullName + "</strong> bạn đã đăng ký ứng dụng thành công.</h2>";
                    chuoi = "<h3>Thông tin đăng nhập:</h3>";
                    chuoi += "<table>";
                    chuoi += "<tr><td style='width:200px'>Tài khoản:</td><td style='width:300px'>" + model.UserName + "</td></tr>";
                    chuoi += "<tr><td style='width:200px'>Mật khẩu:</td><td style='width:300px'>" + model.Password + "</td></tr>";
                    chuoi += "<tr><td style='width:200px'>Mã ứng dụng:</td><td style='width:300px'>" + appId + "</td></tr>";
                    chuoi += "</table><br /><br />";

                    MailManager mail = new MailManager();
                    mail.EnableSSL = SettingsManager.AppSettings.MailEnableSSL;
                    mail.From = SettingsManager.AppSettings.MailAccount;
                    mail.Password = SettingsManager.AppSettings.MailPassWord;
                    mail.Host = SettingsManager.AppSettings.MailServer;
                    mail.Port = SettingsManager.AppSettings.MailPort;
                    mail.To = model.Email;
                    mail.Title = "[URM] Đăng ký tài khoản thành công";
                    mail.Content = chuoi;
                    mail.SendEmail();

                }
                catch (Exception ex)
                {
                    return Redirect("/Home");
                }

                return Redirect("/Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(model);
        }
    }
}