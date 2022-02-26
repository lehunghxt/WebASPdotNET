using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Web.Asp.Provider;
using Web.Asp.UI;
using Web.Business;
using Web.Model;

namespace Web.FrontEnd.Modules
{
    public partial class Templates : VITModule
    {
        private TemplateBLL templateBLL;
        private CompanyBLL companyBLL;

        protected List<TemplateModel> DataTemplates { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.templateBLL = new TemplateBLL();
            this.companyBLL = new CompanyBLL();

            DataTemplates = this.templateBLL.GetAllTemplates().Where(t => t.IsPublished).ToList();
            this.SetCookie();
        }

        public string GetDomainByTemplate(string templateName)
        {
            var domain = this.templateBLL.GetDomainByTemplatePrivate(templateName);
            if (!string.IsNullOrEmpty(domain)) return domain;
            return templateName + ".vdoni.com";
        }

        private void SetCookie()
        {
            if (!string.IsNullOrEmpty(this.GetValueRequest<string>("refId")))
            {
                Response.Cookies["VdoniRefId"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["VdoniRefId"]["RefId"] = this.GetValueRequest<string>("refId");
                Response.Cookies["VdoniRefId"].Expires = DateTime.Now.AddDays(30);
            }
        }
    }
}