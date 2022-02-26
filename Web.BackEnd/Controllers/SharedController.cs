using System.Linq;
using System.Web.Mvc;
using Web.Backend.Controllers;
using Web.Business;

namespace Web.BackEnd.Controllers
{
    public class SharedController : BaseController
    {
        private CompanyBLL companyBLL;

        public SharedController()
        {
            companyBLL = new CompanyBLL();
        }

        // GET: Shared
        public ActionResult UserMenu()
        {
            var model = companyBLL.GetMenuShortcut(this.User.CompanyId).ToList();
            foreach(var data in model)
            {
                foreach(var child in model)
                {
                    if (child.ParentId == data.Id)
                    {
                        child.Shortcut = data;
                        data.Shortcuts.Add(child);
                    }
                }
            }
            model = model.Where(e => e.ParentId == 0).ToList();
            return PartialView(model);
        }
    }
}