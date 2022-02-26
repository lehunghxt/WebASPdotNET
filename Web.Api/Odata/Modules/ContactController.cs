namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using Web.Model;
    using Web.Business;
    using System.Collections.Generic;
    using Library;
    using System;

    public class ContactController : OdataBaseController<ContactModel, string>
    {
        private CompanyBLL bll;
        private Dictionary<string, string> MailContent;
        public ContactController()
        {
            this.bll = new CompanyBLL();
            MailContent = new Dictionary<string, string>();
        } 
        protected override ContactModel CreateEntity(ContactModel model)
        {
                var infoLables = model.InfoLable.Split('|').ToList();
            var infoValues = model.InfoValue.Split('|').ToList();
                for(int i = 0; i < infoLables.Count; i++)
                {
                    this.MailContent[infoLables[i]] = infoValues[i];
                }

            try
            {
                var data = this.bll.GetConfig(this.Web.ID);
                MailManager mail = new MailManager();
                mail.EnableSSL = data.MailEnableSSL;
                mail.Host = data.MailServer;
                mail.Port = data.MailPort ?? 57;
                mail.From = data.MailAccount;
                mail.Password = data.MailPassword;
                mail.To = model.Email;
                mail.Title = model.Title;
                mail.Content = Info(model.Email);
                mail.SendEmail();
                model.Message = "Gửi mail thành công";
            }
            catch (Exception ex)
            {
                model.Message = "Lỗi Email: " + ex.Message;
            }

            return model;
        }

        private string Info(string email)
        {
            string chuoi = "<p>Thông tin:</p>";
            chuoi += "<table cellpadding='3' cellspacing='3'>";
            chuoi += "<tr><td style='width:200px'>Email:</td><td style='width:300px'>" + email + "</td></tr>";
            foreach (var item in this.MailContent)
            {
                chuoi += "<tr><td style='width:200px'>" + item.Key + ":</td><td>" + item.Value + "</td></tr>";
            }

            chuoi += "</table><br /><br />";
            return chuoi;
        }

        private bool ktmail(string email)
        {
            int mail = email.IndexOf("@");
            if (mail > 1)
                if (email.IndexOf(".", mail) > 2)
                    return true;
            return false;
        }
    }
}
