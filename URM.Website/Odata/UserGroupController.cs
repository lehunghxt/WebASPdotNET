namespace URM.Website.Odata
{
    using System.Linq;
    using URM.Business;
    using URM.Model;
    public class UserGroupController : OdataBaseController<URMGroupUsersModel, int>
    {
        private GroupBLL bll;

        public UserGroupController()
        {
            this.bll = new GroupBLL();
        }

        protected override URMGroupUsersModel Update(int key, URMGroupUsersModel model)
        {
            model.ID = key;

            var userIds = model.UserIds.Split(',').Select(int.Parse).ToList();
            this.bll.UpdateUserGroup(key, userIds, base.User.AppId);
            return model;
        }
    }
}
