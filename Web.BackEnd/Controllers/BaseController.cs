namespace Web.Backend.Controllers
{
    using Asp.Security;
    using log4net;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Web.Business;

    public class BaseController : Controller
    {
        public new UserPrincipal User { get; set; }
        public string LanguageId { get; set; }

        private CompanyBLL companyBLL;

        protected static readonly ILog log = LogManager.GetLogger(typeof(BaseController));

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            this.User = base.User as UserPrincipal;
            if (User == null) Response.Redirect("/login");

            companyBLL = new CompanyBLL();
            LanguageId = companyBLL.GetLanguageDefault(this.User.CompanyId);

            ViewBag.Company = companyBLL.GetCompanyInfo(this.User.CompanyId, LanguageId);
            ViewBag.UserFullName = this.User.FullName;
        }

        protected virtual void CheckRole(string role)
        {
            if (this.User == null) Response.Redirect("/login");
            else if (!this.User.Roles.Contains(role)) throw new HttpUnhandledException("Bạn không có quyền thực hiện chức năng này");
        }
    }
}