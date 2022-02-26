namespace Web.Model
{
    using System;
    public class UserModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int ID { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Danh sách quyền
        /// </summary>
        public string Roles { get; set; }

        public string Token { get; set; }
        public int AppId { get; set; }
    }
}
