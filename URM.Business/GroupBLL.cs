namespace URM.Business
{
    using Data;
    using System.Linq;
    using Library;
    using Model;
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The membership business.
    /// </summary>
    public class GroupBLL : BLLBase
    {
        #region Constructor

        private UserAccountDAL accountDAL;
        private UserInfoDAL infoDAL;
        private GroupDAL groupDAL;
        private AppInfoDAL appDAL;

        public GroupBLL(string connectionString = "")
            : base(connectionString)
        {
            this.accountDAL = new UserAccountDAL(this.DatabaseFactory);
            this.infoDAL = new UserInfoDAL(this.DatabaseFactory);
            this.groupDAL = new GroupDAL(this.DatabaseFactory);
            this.appDAL = new AppInfoDAL(this.DatabaseFactory);
        }

        #endregion
        
        #region Group

        public IQueryable<URMGroupModel> GetGroups(int appId)
        {
            var query = this.groupDAL.GetAll()
                                .Where(e => e.AppId == appId)
                                .Select(e => new URMGroupModel
                                {
                                    ID = e.Id,
                                    Name = e.Name,
                                    Roles = e.Roles
                                });

            return query;
        }

        public URMGroupModel GetGroup(int groupId, int appId)
        {
            var query = this.groupDAL.GetAll()
                                .Where(e => e.Id == groupId && e.AppId == appId)
                                .Select(e => new URMGroupModel
                                {
                                    ID = e.Id,
                                    Name = e.Name,
                                    Roles = e.Roles
                                });

            var data = query.FirstOrDefault();
            return data;
        }

        public int InsertGroup(URMGroupModel model, int appId)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new BusinessException("Tên nhóm không được rỗng");
            if (model.Name == "Administrator") throw new BusinessException("Không được thêm nhóm tên 'Administrator'");

            var app = this.appDAL.GetAll().FirstOrDefault(e => e.Id == appId);
            if (app == null) throw new BusinessException(string.Format("Application '{0}' không tồn tại", appId));

            var group = new Group();
            group.Name = model.Name.Trim();
            group.AppId = appId;
            group.Roles = model.Roles;
            this.groupDAL.Add(group);
            this.SaveChanges();

            return group.Id;
        }

        public void UpdateGroup(URMGroupModel model, int appId)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new BusinessException("Tên nhóm không được rỗng");

            var group = this.groupDAL.GetAll().FirstOrDefault(e => e.Id == model.ID && e.AppId == appId);
            if (group == null) throw new BusinessException(string.Format("Nhóm '{0}' không tồn tại", model.ID));
            if(group.Name == "Administrator" && group.Name != model.Name) throw new BusinessException("không được đổi tên nhóm Administrator");

            group.Name = model.Name.Trim();
            group.Roles = model.Roles;
            this.groupDAL.Update(group);
            this.SaveChanges();
        }
        
        public void DeleteGroup(int id, int appId)
        {
            var group = this.groupDAL.GetAll().FirstOrDefault(e => e.Id == id && e.AppId == appId);
            if (group == null) throw new BusinessException(string.Format("Nhóm '{0}' không tồn tại", id));
            if (group.Name == "Administrator") throw new BusinessException("không được xóa nhóm Administrator");

            var contain = this.groupDAL.GetAll().Any(e => e.AppInfo != null);
            if (contain) throw new BusinessException("Nhóm có User không được xóa");

            this.groupDAL.Delete(group);
            this.SaveChanges();
        }
        #endregion

        #region UserGroup
        public void UpdateUserGroup(int groupId, List<int> userIds, int appId)
        {
            var group = this.groupDAL.AllIncludes(e => e.UserInfo)
                                .FirstOrDefault(e => e.Id == groupId && e.AppId == appId);
            if (group == null) throw new BusinessException(string.Format("Nhóm '{0}' không tồn tại", groupId));
            if (userIds .Count > 0)
            {
                group.UserInfo = this.infoDAL.GetAll().Where(e => e.AppId == appId && userIds.Contains(e.Id)).ToList();
                this.SaveChanges();
            }
        }
        #endregion
    }
}