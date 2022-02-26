namespace Web.FrontEnd.Modules
{
    using System;
    using Asp.UI;
    using Business;
    using Asp.Provider;
    using Library;

    public partial class MemberProfile : VITModule
    {
        private MemberSecurityProvider provider;
        private CustomerBLL customerBLL;

        protected void Page_Load(object sender, EventArgs e)
        {
            provider = new MemberSecurityProvider();
            customerBLL = new CustomerBLL();
            if (Page.IsPostBack)
            {
                var name = this.GetValueRequest<string>("FullName");
                var phone = this.GetValueRequest<string>("Phone");
                var mail = this.GetValueRequest<string>("Email");
                var address = this.GetValueRequest<string>("Address");
                var birthday = this.GetValueRequest<DateTime>("Birthday");
                var oldpassword = this.GetValueRequest<string>("PasswordOld");
                var newpassword = this.GetValueRequest<string>("PasswordNew");
                var repassword = this.GetValueRequest<string>("PasswordConfirm");
                if (newpassword == repassword && !string.IsNullOrEmpty(newpassword))
                {
                    customerBLL.UpdateCustomer(Config.ID, this.UserContext.UserId, name, address, mail, phone, birthday, oldpassword, newpassword, this.Title, "", null, "");
                }
                else
                {
                    // không cập nhật mật khẩu
                    customerBLL.UpdateCustomer(Config.ID, this.UserContext.UserId, name, address, mail, phone, birthday, oldpassword, "", this.Title, "", null, "");
                }
            }
        }
        
    }
}