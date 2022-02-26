namespace Web.FrontEnd.Modules
{
    using System;
    using Asp.UI;
    using Business;
    using Asp.Provider;
    using Library;

    public partial class MemberLogin : VITModule
    {
        private MemberSecurityProvider provider;
        private CustomerBLL customerBLL;

        protected string Message;

        protected void Page_Load(object sender, EventArgs e)
        {
            provider = new MemberSecurityProvider();
            customerBLL = new CustomerBLL();

            Message = string.Empty;
            if (Page.IsPostBack)
            {
                switch (this.GetValueRequest<string>("Action"))
                {
                    case "LOGIN":
                        var usr = this.GetValueRequest<string>("LoginUser");
                        var pwd = this.GetValueRequest<string>("LoginPassword");
                        var spw = this.GetValueRequest<string>("LoginSave");
                        bool isSavePass = spw == "on";

                        //Log in
                        if (usr != null || pwd != null)
                        {
                            usr = usr.Trim();
                            pwd = pwd.Trim();

                            if (provider.ValidateUser(Config.ID, usr, pwd, isSavePass))
                                HREF.RedirectComponent(this.GetValueParam<string>("ComponentReturn"));
                        }
                        break;
                    case "RESET":
                        var mailReser = GetValueRequest<string>("ResetEmail");
                        if (mailReser != null)
                        {
                            var pass = customerBLL.ForgetPassword(mailReser);
                            if(!string.IsNullOrEmpty(pass))
                            {
                                var customer = customerBLL.GetCustomer(Config.ID, mailReser);
                                string chuoi = "<h2>Chào <strong>" + customer.Name + "</strong> bạn đã yêu cầu cấp lại mật khẩu.</h2>";
                                chuoi = "<h3>Thông tin đăng nhập:</h3>";
                                chuoi += "<table>";
                                chuoi += "<tr><td style='width:200px'>Mật khẩu mới:</td><td style='width:300px'>" + pass + "</td></tr>";
                                chuoi += "</table><br /><br />";
                                
                                MailManager mailMa = new MailManager();
                                mailMa.EnableSSL = SettingsManager.AppSettings.MailEnableSSL;
                                mailMa.From = SettingsManager.AppSettings.MailAccount;
                                mailMa.Password = SettingsManager.AppSettings.MailPassWord;
                                mailMa.Host = SettingsManager.AppSettings.MailServer;
                                mailMa.Port = SettingsManager.AppSettings.MailPort;
                                mailMa.To = mailReser;
                                mailMa.Title = "Cấp mật khẩu mới";
                                mailMa.Content = chuoi;
                                mailMa.SendEmail();
                            }
                        }
                        break;
                    case "REGIS":
                        var name = this.GetValueRequest<string>("FullName");
                        var phone = this.GetValueRequest<string>("Phone");
                        var mail = this.GetValueRequest<string>("Email"); 
                        var address = this.GetValueRequest<string>("Address");
                        var password = this.GetValueRequest<string>("Password");
                        var repassword = this.GetValueRequest<string>("PasswordConfirm");
                        if (password == repassword)
                        {
                            try
                            {
                                customerBLL.RegisCustomer(Config.ID, name, address, mail, phone, DateTime.Now, password, this.Title, "", null, "");
                                var login = false;
                                if (!string.IsNullOrEmpty(phone)) login = provider.ValidateUser(Config.ID, phone, password, true);
                                else if (!string.IsNullOrEmpty(mail)) login = provider.ValidateUser(Config.ID, mail, password, true);
                                if (login) HREF.RedirectComponent(this.GetValueParam<string>("ComponentReturn"));
                            }
                            catch (BusinessException ex)
                            {
                                Message = ex.Message;
                            }
                            catch (Exception ex)
                            {
                                Message = ex.Message;
                            }
                        }
                        else
                        {
                            Message = "Mật khẩu xác nhận không đúng";
                        }
                        break;
                }
            }
        }
        
    }
}