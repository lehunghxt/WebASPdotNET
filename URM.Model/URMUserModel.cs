namespace URM.Model
{
    using System;
    public class URMUserModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int ID { get; set; }
        
        public string UserName { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Thuộc nhóm nào, GrooupId = 0: không thuộc nhóm nào
        /// </summary>
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupRoles { get; set; }

        /// <summary>
        /// Danh sách quyền
        /// </summary>
        public string Roles { get; set; }

        public string Token { get; set; }
        public int AppId { get; set; }

        /// <summary>
        /// chỉ dùng trong insert
        /// </summary>
        public string Password { get; set; } 
    }
}
