namespace URM.Business
{
    using Data;
    using System;
    using System.Linq;
    using Library;
    using Model;
    using System.Collections.Generic;

    public class AppInfoBLL : BLLBase
    {
        #region Constructor

        private UserAccountDAL accountDAL;
        private GroupDAL groupDAL;
        private UserInfoDAL infoDAL;
        private AppInfoDAL appDAL;
        private RoleDAL roleDAL;

        public AppInfoBLL(string connectionString = "")
            : base(connectionString)
        {
            this.accountDAL = new UserAccountDAL(this.DatabaseFactory);
            this.groupDAL = new GroupDAL(this.DatabaseFactory);
            this.infoDAL = new UserInfoDAL(this.DatabaseFactory);
            this.appDAL = new AppInfoDAL(this.DatabaseFactory);
            this.roleDAL = new RoleDAL(this.DatabaseFactory);
        }

        #endregion

        #region Application
        public string GetAppName(int appId)
        {
            var app = this.appDAL.GetAll().FirstOrDefault(e => e.Id == appId);
            if (app != null) return app.Name;
            return string.Empty;
        }

        public void ReName(int appId, string newName)
        {
            if (string.IsNullOrEmpty(newName)) throw new BusinessException("Tên ứng dụng không được rỗng");

            var app = this.appDAL.GetAll().FirstOrDefault(e => e.Id == appId);
            if (app == null) throw new BusinessException(string.Format("Application '{0}' không tồn tại", appId));

            if (app.Name != newName)
            {
                app.Name = newName.Trim();
                this.appDAL.Update(app);
                this.SaveChanges();
            }
        }

        public int CreateAppInfo(URMAppCreateModel model)
        {
            if (string.IsNullOrEmpty(model.UserName)) throw new BusinessException("Vui lòng nhập tài khoản");
            if (string.IsNullOrEmpty(model.Password)) throw new BusinessException("Vui lòng nhập mật khẩu");
            if (string.IsNullOrEmpty(model.AppName)) throw new BusinessException("Vui lòng nhập tên ứng dụng");
            if (string.IsNullOrEmpty(model.FullName)) throw new BusinessException("Vui lòng nhập Họ tên bạn");
            if (string.IsNullOrEmpty(model.Email)) throw new BusinessException("Vui lòng nhập Email");
            if (model.Password != model.ConfirmPassword) throw new BusinessException("Nhập lại mật khẩu không khớp");
            
            var existAccount = this.accountDAL.GetAll().Any(e => e.UserName == model.UserName);
            if (existAccount) throw new BusinessException("Tài khoản đã được người khác sử dụng");
            var existEmail = this.infoDAL.GetAll().Any(e => e.Email == model.Email);
            if (existEmail) throw new BusinessException(string.Format("Email '{0}' đã được sử dụng bởi người khác", model.Email));

            var userAdmin = this.accountDAL.GetAll().FirstOrDefault(e => e.UserName == "Administrator");
            if (userAdmin == null) throw new BusinessException("Không tìm thấy tài khoản Administrator");
            
            var account = new UserAccount();
            account.UserName = model.UserName.Trim();
            account.Password = model.Password.EnCodeMD5();
            account.Roles = userAdmin.Roles;
            account.UserInfo = new UserInfo();
            account.UserInfo.Address = model.Address.Trim();
            account.UserInfo.Birthday = model.Birthday;
            account.UserInfo.CreateBy = userAdmin.UserId;
            account.UserInfo.CreateDate = DateTime.Now;
            account.UserInfo.Email = model.Email.Trim();
            account.UserInfo.FullName = model.FullName.Trim();
            account.UserInfo.Phone = model.Phone.Trim();

            this.accountDAL.Add(account);
            this.SaveChanges();

            return account.UserInfo.AppId;
        }
        #endregion

        #region Role
        public IQueryable<URMRoleModel> GetAllRole(int appId)
        {
            var query = this.roleDAL.GetAll()
                            .Where(e => e.AppId == appId || e.AppId == 1)
                            .Select(e => new URMRoleModel
                            {
                                ID = e.RoleId,
                                RoleGroup = e.RoleGroup,
                                RoleName = e.RoleName
                            });

            //var data = query.ToList();
            return query;
        }

        public IQueryable<URMRoleModel> GetAllRoleByUserName(string userName, int appId)
        {
            var query = this.roleDAL.GetAll()
                            .Where(e => e.AppId == appId || e.AppId == 1)
                            .Select(e => new URMRoleModel
                            {
                                ID = e.RoleId,
                                RoleGroup = e.RoleGroup,
                                RoleName = e.RoleName
                            });

            var account = this.accountDAL.GetAll()
                            .Where(e => e.UserName == userName && e.UserInfo.AppId == appId)
                            .Select(e => new { e.UserId, e.Roles })
                            .FirstOrDefault();
            
            var roleGroups = this.infoDAL.GetAll()
                            .Where(e => e.AppInfo.Id == appId && e.UserAccount.UserName == userName)
                            .Select(e => e.Group.Roles)
                            .FirstOrDefault();

            var roles = account.Roles + roleGroups;

            var listRole = roles.Split('|').Where(e => !string.IsNullOrEmpty(e)).Distinct();

            query = query.Where(e => listRole.Contains(e.ID));
            return query;
        }

        public IQueryable<URMRoleModel> GetAllRoleByUserId(int userId, int appId)
        {
            var query = this.roleDAL.GetAll()
                            .Where(e => e.AppId == appId || e.AppId == 1)
                            .Select(e => new URMRoleModel
                            {
                                ID = e.RoleId,
                                RoleGroup = e.RoleGroup,
                                RoleName = e.RoleName
                            });

            var roleAccounts = this.accountDAL.GetAll()
                            .Where(e => e.UserId == userId && e.UserInfo.AppId == appId)
                            .Select(e => e.Roles)
                            .FirstOrDefault();

            var groupId = this.infoDAL.GetAll().Where(e => e.Id == userId)
                            .Select(e => e.Group.Id)
                            .FirstOrDefault();
            var roleGroups = this.groupDAL.GetAll()
                            .Where(e => e.Id == groupId && e.AppId == appId)
                            .Select(e => e.Roles)
                            .FirstOrDefault();

            var roles = roleAccounts + roleGroups;

            var listRole = roles.Split('|').Where(e => !string.IsNullOrEmpty(e)).Distinct().ToList();

            query = query.Where(e => listRole.Contains(e.ID));
            return query;
        }

        public IQueryable<URMRoleModel> GetAllRoleByGroup(int groupId, int appId)
        {
            var roles = this.groupDAL.GetAll()
                            .Where(e => e.Id == groupId && e.AppId == appId)
                            .Select(e => e.Roles)
                            .FirstOrDefault();
            var listRole = roles.Split('|');

            var query = this.roleDAL.GetAll()
                            .Where(e => listRole.Contains(e.RoleId) && (e.AppId == appId || e.AppId == 1))
                            .Select(e => new URMRoleModel
                            {
                                ID = e.RoleId,
                                RoleGroup = e.RoleGroup,
                                RoleName = e.RoleName
                            });
            
            return query;
        }

        public URMRoleModel GetRole(string id, int appId)
        {
            var query = this.roleDAL.GetAll()
                            .Where(e => e.RoleId == id && (e.AppId == appId || e.AppId == 1))
                            .Select(e => new URMRoleModel
                            {
                                ID = e.RoleId,
                                RoleGroup = e.RoleGroup,
                                RoleName = e.RoleName
                            });

            var data = query.FirstOrDefault();
            return data;
        }

        public void InsertRole(URMRoleModel model, int appId)
        {
            if (string.IsNullOrEmpty(model.RoleName)) throw new BusinessException("Quyền không được rỗng");
            if (string.IsNullOrEmpty(model.RoleGroup)) throw new BusinessException("Nhóm quyền không được rỗng");

            var exist = this.roleDAL.GetAll().Any(e => e.RoleId == model.ID && e.AppId == appId);
            if (exist) throw new BusinessException(string.Format("Quyền '{0}' đã tồn tại", model.ID));

            var role = new Role();
            role.AppId = appId;
            role.RoleGroup = model.RoleGroup;
            role.RoleName = model.RoleName;
            role.RoleId = model.ID;
            this.roleDAL.Add(role);

            this.SaveChanges();
        }

        public void DeleteRole(string roleId, int appId)
        {
            var role = this.roleDAL.GetAll().FirstOrDefault(e => e.RoleId == roleId && e.AppId == appId);
            if (role == null)
            {
                var systamRole = this.roleDAL.GetAll().FirstOrDefault(e => e.RoleId == roleId && e.AppId == 1);
                if (systamRole == null) throw new BusinessException(string.Format("Không tồn tại quyền '{0}'", roleId));
                else throw new BusinessException("Quyền hệ thống không thể xóa");
            }
            this.roleDAL.Delete(role);
            this.SaveChanges();
        }

        public void UpdateFullRoleForAdmin(string userName, int appId)
        {
            // get quyền của admin trên UM 
            var adminRoles = string.Empty;

            if (userName != "Administrator")
            {
                adminRoles = this.accountDAL.GetAll()
                           .Where(e => e.UserName == "Administrator")
                           .Select(e => e.Roles)
                           .FirstOrDefault();
            }

            // danh sách quyền trên ứng dụng của user
            var listRole = this.roleDAL.GetAll()
                            .Where(e => e.AppId == appId)
                            .Select(e => e.RoleId)
                            .ToList();
            
            listRole.Add(adminRoles);
            listRole = listRole.Distinct().ToList();

            var fullRoles = string.Join("|", listRole);

            // update quyền cho user
            var account = this.accountDAL.GetAll().Where(e => e.UserName == userName && e.UserInfo.AppId == appId).FirstOrDefault();

            account.Roles = fullRoles;
            this.accountDAL.Update(account);
            this.SaveChanges();
        }
        #endregion
    }
}