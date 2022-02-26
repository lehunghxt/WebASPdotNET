namespace Web.Backend.Controllers
{
    using Asp.Provider;
    using Library;
    using Library.Web;
    using System;
    using System.Web.Mvc;
    using System.Web.Security;
    using Web.Model;

    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Error = string.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserLoginModel model)
        {
            try
            {
                model.ApplicationId = SettingsManager.AppSettings.ApplicationId;
                ApiHelper api = new ApiHelper();
                var user = api.PostGetObject<UserModel>(string.Format("{0}api/Authenticate/Post", SettingsManager.AppSettings.URMService), model);

                FormsAuthentication.SetAuthCookie(model.UserName, model.CreatePersistentCookie);
                // token
                var key = string.Format(SettingsManager.Constants.CookeiLogin, model.UserName);
                Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies[key][SettingsManager.Constants.CookeiTockenKey] = user.Token;
                Response.Cookies[key][SettingsManager.Constants.CookeiAppIdKey] = user.AppId.ToString();
                Response.Cookies[key].Expires = DateTime.Now.AddDays(SettingsManager.Constants.CookeiTimeout);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }

        public ActionResult ForgetPassword()
        {
            ViewBag.Error = string.Empty;
            ViewBag.Success = false;
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                var api = new ApiHelper();
                var pass = api.PostGetObject<string>(string.Format("http://{0}/api/Authenticate/ForgetPassword", Request.Url.Authority), model);
                if (pass != null && !string.IsNullOrEmpty(pass.ToString()))
                {
                    string chuoi = "<h2>Chào <strong>" + model.UserName + "</strong> bạn đã yêu cầu cấp lại mật khẩu.</h2>";
                    chuoi = "<h3>Thông tin đăng nhập:</h3>";
                    chuoi += "<table>";
                    chuoi += "<tr><td style='width:200px'>Tài khoản:</td><td style='width:300px'>" + model.UserName + "</td></tr>";
                    chuoi += "<tr><td style='width:200px'>Mật khẩu mới:</td><td style='width:300px'>" + pass + "</td></tr>";
                    chuoi += "</table><br /><br />";

                    MailManager mail = new MailManager();
                    mail.EnableSSL = SettingsManager.AppSettings.MailEnableSSL;
                    mail.From = SettingsManager.AppSettings.MailAccount;
                    mail.Password = SettingsManager.AppSettings.MailPassWord;
                    mail.Host = SettingsManager.AppSettings.MailServer;
                    mail.Port = SettingsManager.AppSettings.MailPort;
                    mail.To = model.Email;
                    mail.Title = "[URM] Cấp mật khẩu mới";
                    mail.Content = chuoi;
                    mail.SendEmail();

                    ViewBag.Success = true;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Success = false;
            }

            return View(model);
        }
    }
}