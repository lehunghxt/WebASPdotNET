namespace URM.Website.Odata
{
    using Security;
    using System.Linq;
    using System.Web.Http.OData;
    using URM.Business;
    using URM.Model;
    public class GroupController : OdataBaseController<URMGroupModel, int>
    {
        private GroupBLL bll;

        public GroupController()
        {
            this.bll = new GroupBLL();
        }

        [EnableQuery]
        public override IQueryable<URMGroupModel> Select()
        {
            var param = this.GetParameter();

            var appId = this.User.AppId;
            if (param.ContainsKey("AppId")) int.TryParse(param["AppId"], out appId);

            return bll.GetGroups(appId);
        }

        protected override URMGroupModel GetEntityByKey(int id)
        {
            return bll.GetGroup(id, this.User.AppId);
        }

        protected override URMGroupModel Update(int key, URMGroupModel model)
        {
            model.ID = key;
            this.bll.UpdateGroup(model, base.User.AppId);
            return model;
        }

        protected override URMGroupModel Insert(URMGroupModel model)
        {
            model.ID = this.bll.InsertGroup(model, this.User.AppId);
            return model;
        }

        public override void Drop(int key)
        {
            this.bll.DeleteGroup(key, this.User.AppId);
        }
    }
}
