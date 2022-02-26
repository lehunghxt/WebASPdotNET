namespace URM.Website.Odata
{
    using Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.OData;
    using URM.Business;
    using URM.Model;

    /// <summary>
    /// API quản lý tài khoản (UserAccount-UserInfo)
    /// </summary>
    public class UserController : OdataBaseController<URMUserModel, int>
    {
        private UserBLL bll;

        public UserController()
        {
            this.bll = new UserBLL();
        }

        [EnableQuery]
        public override IQueryable<URMUserModel> Select()
        {
            var param = this.GetParameter();

            var appId = this.User.AppId;
            if (param.ContainsKey("AppId")) int.TryParse(param["AppId"], out appId);

            List<int> userIds = null;
            if (param.ContainsKey("UserIds") && !string.IsNullOrEmpty(param["UserIds"])) userIds = param["UserIds"].Split(',').Select(e => Convert.ToInt32(e)).ToList();

            var parentId = 0;
            if (param.ContainsKey("ParentId")) int.TryParse(param["ParentId"], out parentId);

            return bll.GetAllChildUser(appId, userIds, parentId);
        }

        protected override URMUserModel GetEntityByKey(int id)
        {
            return bll.GetProfileByUserId(id, this.User.AppId);
        }

        protected override URMUserModel Insert(URMUserModel model)
        {
            if (model.AppId == 0) model.AppId = this.User.AppId;
            model.ID = this.bll.CreateAccount(model, this.User.UserId);
            return model;
        }

        protected override URMUserModel Update(int key, URMUserModel model)
        {
            model.ID = key;
            if (model.AppId == 0) model.AppId = this.User.AppId;
            this.bll.UpdateAccount(model);
            return model;
        }

        public override void Drop(int key)
        {
            this.bll.DeleteAccount(key);
        }
    }
}
