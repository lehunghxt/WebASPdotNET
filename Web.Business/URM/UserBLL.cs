namespace Web.Business
{
    using Data;
    using System.Linq;
    using Library;
    using Model;
    using System;
    using System.Collections.Generic;
    using Data.DataAccess;

    /// <summary>
    ///     The membership business.
    /// </summary>
    public class UserBLL : BaseBLL
    {
        #region Constructor

        private UserAccountDAL accountDAL;
        private UserInfoDAL infoDAL;
        private GroupDAL groupDAL;
        private AppInfoDAL appDAL;

        public UserBLL(string connectionString = "")
            : base(connectionString)
        {
            this.accountDAL = new UserAccountDAL(this.DatabaseFactory);
            this.infoDAL = new UserInfoDAL(this.DatabaseFactory);
            this.groupDAL = new GroupDAL(this.DatabaseFactory);
            this.appDAL = new AppInfoDAL(this.DatabaseFactory);
        }

        #endregion
        
        #region Login
        /// <summary>
        /// Hàm kiểm tra Tài khoản có đúng là quản lý tên miền này hay không
        /// </summary>
        /// <returns>
        /// 0: Không tồn tại hoặc tài khoản không phảo quản lý tên miền này
        /// > 0: Nếu khớp thì trả về CompanyId
        /// </returns>
        public int CheckUser(string userName, string password, int appId)
        {
            var user = this.accountDAL.GetAll()
                        .FirstOrDefault(e => e.UserName == userName && e.UserInfo.AppId == appId);

            if (user == null) return 0;

            var md5Pass = password.EnCodeMD5();

            if (user.Password == md5Pass) return user.UserId;
            return 0;
        }

        public string ForgetPassword(string userName, string email, int appId)
        {
            var account = this.accountDAL.GetAll()
                        .FirstOrDefault(e => e.UserName == userName && e.UserInfo.Email == email && e.UserInfo.AppId == appId);
            if (account == null) return string.Empty;

            var newPass = GenerateRandomCode.RandomCode(6);

            account.Password = newPass.Trim().EnCodeMD5();
            this.accountDAL.Update(account);
            this.SaveChanges();
            return newPass;
        }
        #endregion

        #region UserAccount
        public URMUserModel GetProfileByUserName(string userName, int appId)
        {
            var query = this.infoDAL.GetAll()
                .Where(info => info.UserAccount.UserName == userName && info.AppId == appId)
                .Select(info => new URMUserModel
                {
                    ID = info.Id,
                    Address = info.Address,
                    Birthday = info.Birthday,
                    Email = info.Email,
                    FullName = info.FullName,
                    Phone = info.Phone,
                    AppId = info.AppId,
                    UserName = info.UserAccount.UserName,
                    Roles = info.UserAccount.Roles,
                    GroupId = info.Group == null ? 0 : info.Group.Id,
                    GroupName = info.Group == null ? "" : info.Group.Name,
                    GroupRoles = info.Group == null ? "" : info.Group.Roles,
                });

            var data = query.FirstOrDefault();

            if (data == null) return null;
            if (!string.IsNullOrEmpty(data.GroupRoles)) data.Roles = data.Roles + "|" + data.GroupRoles;
            if (!string.IsNullOrEmpty(data.Roles))
            {
                var listRole = data.Roles.Split('|').Where(e => !string.IsNullOrEmpty(e)).Distinct();
                data.Roles = string.Join("|", listRole);
            }

            return data;
        }

        public URMUserModel GetProfileByUserId(int id, int appId)
        {
            var query = this.infoDAL.GetAll()
                .Where(info => info.Id == id && info.AppId == appId)
                .Select(info => new URMUserModel
                {
                    ID = info.Id,
                    Address = info.Address,
                    Birthday = info.Birthday,
                    Email = info.Email,
                    FullName = info.FullName,
                    Phone = info.Phone,
                    AppId = info.AppId,
                    UserName = info.UserAccount.UserName,
                    Roles = info.UserAccount.Roles,
                    GroupId = info.Group == null ? 0 : info.Group.Id,
                    GroupName = info.Group == null ? "" : info.Group.Name,
                    GroupRoles = info.Group == null ? "" : info.Group.Roles,
                });

            var data = query.FirstOrDefault();

            if (!string.IsNullOrEmpty(data.GroupRoles)) data.Roles = data.Roles + "|" + data.GroupRoles;
            var listRole = data.Roles.Split('|').Where(e => !string.IsNullOrEmpty(e)).Distinct();
            data.Roles = string.Join("|", listRole);

            return data;
        }

        public int CreateAccount(URMUserModel model, int userId)
        {
            if (string.IsNullOrEmpty(model.FullName)) throw new BusinessException("Vui lòng nhập Họ tên bạn");
            if (string.IsNullOrEmpty(model.Email)) throw new BusinessException("Vui lòng nhập Email");
            if (string.IsNullOrEmpty(model.UserName)) throw new BusinessException("Vui lòng nhập tài khoản");
            if (string.IsNullOrEmpty(model.Password)) throw new BusinessException("Vui lòng nhập mật khẩu");

            var existUserName = this.accountDAL.GetAll().Any(e => e.UserName == model.UserName && e.UserInfo.AppId == model.AppId);
            if (existUserName) throw new BusinessException(string.Format("Tên tài khoản '{0}' đã được sử dụng bởi người khác", model.UserName));
            var existEmail = this.infoDAL.GetAll().Any(e => e.Email == model.Email && e.UserAccount.UserInfo.AppId == model.AppId);
            if (existEmail) throw new BusinessException(string.Format("Email '{0}' đã được sử dụng bởi người khác", model.Email));
            if (model.GroupId > 0)
            {
                var existGroup = this.groupDAL.GetAll().Any(e => e.Id == model.GroupId && e.AppId == model.AppId);
                if (!existGroup) throw new BusinessException(string.Format("Nhóm '{0}' không tồn tại", model.GroupId));
            }
            if (model.AppId > 0)
            {
                var existApp = this.appDAL.GetAll().Any(e => e.Id == model.AppId);
                if (!existApp) throw new BusinessException(string.Format("Ứng dụng '{0}' không tồn tại", model.AppId));
            }

            var account = new UserAccount();
            account.UserName = model.UserName.Trim();
            account.Password = model.Password.Trim().EnCodeMD5();
            //account.Roles = model.Roles;
            account.UserInfo = new UserInfo();
            account.UserInfo.AppId = model.AppId;
            account.UserInfo.Birthday = model.Birthday;
            account.UserInfo.CreateBy = userId;
            account.UserInfo.CreateDate = DateTime.Now;
            account.UserInfo.Email = model.Email.Trim();
            account.UserInfo.FullName = model.FullName.Trim();
            if (string.IsNullOrEmpty(model.Phone)) account.UserInfo.Phone = string.Empty;
            else account.UserInfo.Phone = model.Phone.Trim();
            if (string.IsNullOrEmpty(model.Address)) account.UserInfo.Address = string.Empty;
            else account.UserInfo.Address = model.Address.Trim();
            if (model.GroupId > 0) account.UserInfo.Group = this.groupDAL.GetAll().FirstOrDefault(e => e.Id == model.GroupId && e.AppId == model.AppId);

            this.accountDAL.Add(account);
            this.SaveChanges();
            return account.UserId;
        }

        public void UpdateAccount(URMUserModel model)
        {
            if (string.IsNullOrEmpty(model.FullName)) throw new BusinessException("Vui lòng nhập Họ tên bạn");
            if (string.IsNullOrEmpty(model.Email)) throw new BusinessException("Vui lòng nhập Email");

            var info = this.infoDAL.GetAll().FirstOrDefault(e => e.Id == model.ID && e.AppId == model.AppId);
            if (info == null) throw new BusinessException(string.Format("User '{0}' không tồn tại", model.ID));
            if (info.Email != model.Email)
            {
                var existEmail = this.infoDAL.GetAll().Any(e => e.Email == model.Email && e.AppId == model.AppId);
                if (existEmail) throw new BusinessException(string.Format("Email '{0}' đã được sử dụng bởi người khác", model.Email));
            }
            if (model.GroupId > 0)
            {
                var group = this.groupDAL.GetAll().FirstOrDefault(e => e.Id == model.GroupId && e.AppId == model.AppId);
                if (group == null) throw new BusinessException(string.Format("Nhóm '{0}' không tồn tại", model.GroupId));
                else info.Group = group;
            }

            info.UserAccount = this.accountDAL.GetAll().FirstOrDefault(e => e.UserId == model.ID);
            //info.UserAccount.Roles = model.Roles;
            if (!string.IsNullOrEmpty(model.Password) && model.Password != info.UserAccount.Password)
                info.UserAccount.Password = model.Password.Trim().EnCodeMD5();

            info.Birthday = model.Birthday;
            info.Email = model.Email.Trim();
            info.FullName = model.FullName.Trim();
            if (string.IsNullOrEmpty(model.Phone)) info.Phone = string.Empty;
            else info.Phone = model.Phone.Trim();
            if (string.IsNullOrEmpty(model.Address)) info.Address = string.Empty;
            else info.Address = model.Address.Trim();

            this.infoDAL.Update(info);
            this.SaveChanges();
        }

        public void ChangePassword(URMUserAccountModel model, int appId)
        {
            var account = this.accountDAL.GetAll().FirstOrDefault(e => e.UserId == model.ID && e.UserInfo.AppId == appId);
            if (account == null) throw new BusinessException(string.Format("User '{0}' không tồn tại", model.ID));

            if (string.IsNullOrEmpty(model.Password)) throw new BusinessException("Mật khẩu cũ không được rỗng");
            if (model.Password.Trim().EnCodeMD5() != account.Password) throw new BusinessException("Mật khẩu cũ không đúng");
            if (string.IsNullOrEmpty(model.Password)) throw new BusinessException("Mật khẩu mới không được rỗng");
            if (model.NewPassword.Trim() == model.Password.Trim()) throw new BusinessException("Mật khẩu mới phải khác mật khẩu cũ");

            account.Password = model.NewPassword.Trim().EnCodeMD5();
            this.accountDAL.Update(account);
            this.SaveChanges();
        }

        public string ResetPassword(int userId, int appId)
        {
            var account = this.accountDAL.GetAll().FirstOrDefault(e => e.UserId == userId && e.UserInfo.AppId == appId);
            if (account == null) throw new BusinessException(string.Format("User '{0}' không tồn tại", userId));

            var newPass = GenerateRandomCode.RandomString(6);

            account.Password = newPass.Trim().EnCodeMD5();
            this.accountDAL.Update(account);
            this.SaveChanges();
            return newPass;
        }

        public void UpdateRoleAccount(int userId, string roles, int appId)
        {
            var account = this.accountDAL.GetAll().FirstOrDefault(e => e.UserId == userId && e.UserInfo.AppId == appId);
            if (account == null) throw new BusinessException(string.Format("User '{0}' không tồn tại", userId));

            account.Roles = roles;
            this.accountDAL.Update(account);
            this.SaveChanges();
        }

        /// <summary>
        /// Hàm lấy tất cả user
        /// </summary>
        /// <param name="userId">Chỉ lấy user do user này tạo ra</param>
        public IQueryable<URMUserModel> GetAllChildUser(int appId, List<int> userIds, int userId = 0)
        {
            var query = this.infoDAL.GetAll().Where(e => e.AppId == appId);
            if (userIds != null && userIds.Count > 0) query = query.Where(e => userIds.Contains(e.Id));

            var queryData = query.Select(e => new URMUserModel {
                                    Address = e.Address,
                                    Birthday = e.Birthday,
                                    AppId = appId,
                                    Email = e.Email,
                                    FullName = e.FullName,
                                    ID = e.Id,
                                    Phone = e.Phone,
                                    UserName = e.UserAccount.UserName,
                GroupId = e.Group == null ? 0 : e.Group.Id,
                GroupName = e.Group == null ? "" : e.Group.Name,
                GroupRoles = e.Group == null ? "" : e.Group.Roles,
            });

            if (userId == 0) return queryData;

            var users = queryData.ToList();
            var listUserId = this.infoDAL.GetAll()
                                .Where(e => e.AppId == appId)
                                .Select(e => new Child { Id = e.Id, ParentId = e.CreateBy })
                                .ToList();
            
            var childIds = this.GetAllChild(listUserId, userId).Select(e => e.Id).ToList();
            users = users.Where(e => childIds.Contains(e.ID)).ToList();

            return users.AsQueryable();
        }

        public void DeleteAccount(int userId)
        {
            var info = this.infoDAL.GetAll().FirstOrDefault(e => e.Id == userId);
            if (info == null) throw new BusinessException(string.Format("User '{0}' không tồn tại", userId));

            this.accountDAL.Delete(e => e.UserId == userId);
            this.infoDAL.Delete(info);
            this.SaveChanges();
        }

        /// <summary>
        /// Get list parent id
        /// </summary>
        /// <param name="category">Danh sách category</param>
        /// <param name="categoryId">Category id</param>
        /// <returns>List Id</returns>
        private IList<Child> GetAllChild(IList<Child> list, int? parentId)
        {
            var childs = list.Where(e => e.ParentId == parentId).ToList();
            if (childs.Count == 0) return new List<Child>();
            var current = new List<Child>();
            current.AddRange(childs);
            foreach (var child in childs)
            {
                current.AddRange(this.GetAllChild(list, child.Id));
            }
                
            return current;
        }
        #endregion

        #region UserInfo
        public URMUserInfoModel GetUserInfoByUserName(string userName, int appId)
        {
            var query = this.infoDAL.GetAll()
                            .Where(e => e.UserAccount.UserName == userName && e.AppId == appId)
                            .Select(e => new URMUserInfoModel
                            {
                                Address = e.Address,
                                Birthday = e.Birthday,
                                Email = e.Email,
                                FullName = e.FullName,
                                Phone = e.Phone,
                                ID = e.Id,
                                CreateDate = e.CreateDate
                            });
            var data = query.FirstOrDefault();
            return data;
        }

        public URMUserInfoModel GetUserInfo(int id, int appId)
        {
            var query = this.infoDAL.GetAll()
                            .Where(e => e.Id == id && e.AppId == appId)
                            .Select(e => new URMUserInfoModel
                            {
                                Address = e.Address,
                                Birthday = e.Birthday,
                                Email = e.Email,
                                FullName = e.FullName,
                                Phone = e.Phone,
                                ID = e.Id,
                                CreateDate = e.CreateDate
                            });
            var data = query.FirstOrDefault();
            return data;
        }

        public void UpdateInfo(URMUserInfoModel model, int appId)
        {
            if (string.IsNullOrEmpty(model.FullName)) throw new BusinessException("Họ tên không được rỗng");
            if (string.IsNullOrEmpty(model.Phone)) throw new BusinessException("Số điện thoại không được rỗng");
            if (string.IsNullOrEmpty(model.Email)) throw new BusinessException("Email không được rỗng");

            var user = this.infoDAL.GetAll().FirstOrDefault(e => e.Id == model.ID && e.AppId == appId);
            if (user == null) throw new BusinessException(string.Format("User '{0}' không tồn tại", model.ID));


            user.Address = model.Address.Trim();
            user.Birthday = model.Birthday;
            user.Email = model.Email.Trim();
            user.FullName = model.FullName.Trim();
            user.Phone = model.Phone.Trim();
            this.infoDAL.Update(user);
            this.SaveChanges();

        }
        #endregion

        class Child
        {
            public int Id { get; set; }
            public int? ParentId { get; set; }
        }
    }
}