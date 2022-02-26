namespace Web.Backend.Odata
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Business;
    using Web.Model;
    public class UserInfoController : OdataBaseController<URMUserInfoModel, int>
    {
        private UserBLL bll;

        public UserInfoController()
        {
            this.bll = new UserBLL();
        }

        /// <sURMmary>
        /// Get thông tin tài khoản thông qua UserName
        /// </sURMmary>
        [EnableQuery]
        public override IQueryable<URMUserInfoModel> Select()
        {
            var data = new List<URMUserInfoModel>();
            var param = this.GetParameter();
            var appId = this.User.AppId;
            if (param.ContainsKey("ApplicationId")) int.TryParse(param["ApplicationId"], out appId);
            var userId = 0;
            if (param.ContainsKey("UserId")) int.TryParse(param["UserId"], out userId);
            if (userId > 0) data.Add(this.bll.GetUserInfo(userId, appId));
            else
            {
                var userName = string.Empty;
                if (param.ContainsKey("UserName")) userName = param["UserName"];
                if (!string.IsNullOrEmpty(userName)) data.Add(this.bll.GetUserInfoByUserName(userName, appId));
                else data.Add(this.bll.GetUserInfo(this.User.UserId, appId));
            }

            return data.AsQueryable();
        }


        /// <sURMmary>
        /// Get thông tin tài khoản thông qua UserId
        /// </sURMmary>
        protected override URMUserInfoModel GetEntityByKey(int id)
        {
            return this.bll.GetUserInfo(id, this.User.AppId);
        }

        /// <sURMmary>
        /// Cập nhật thông tin cá nhân
        /// </sURMmary>
        protected override URMUserInfoModel Update(int key, URMUserInfoModel model)
        {
            this.bll.UpdateInfo(model, this.User.AppId);
            return model;
        }
    }
}
