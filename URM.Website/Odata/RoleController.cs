namespace URM.Website.Odata
{
    using Security;
    using System;
    using System.Linq;
    using System.Web.Http.OData;
    using URM.Business;
    using URM.Model;
    public class RoleController : OdataBaseController<URMRoleModel, string>
    {
        private AppInfoBLL bll;

        public RoleController()
        {
            this.bll = new AppInfoBLL();
        }

        [EnableQuery]
        public override IQueryable<URMRoleModel> Select()
        {
            var param = this.GetParameter();
            if (param.ContainsKey("UserName")) return bll.GetAllRoleByUserName(param["UserName"], this.User.AppId);
            if (param.ContainsKey("UserId")) return bll.GetAllRoleByUserId(Convert.ToInt32(param["UserId"]), this.User.AppId);
            else return bll.GetAllRole(this.User.AppId);
        }

        protected override URMRoleModel GetEntityByKey(string id)
        {
            return bll.GetRole(id, this.User.AppId);
        }

        protected override URMRoleModel Insert(URMRoleModel model)
        {
            this.bll.InsertRole(model, this.User.AppId);
            this.bll.UpdateFullRoleForAdmin(this.User.UserName, this.User.AppId);
            return model;
        }

        public override void Drop(string key)
        {
            this.bll.DeleteRole(key, this.User.AppId);
            this.bll.UpdateFullRoleForAdmin(this.User.UserName, this.User.AppId);
        }
    }
}
