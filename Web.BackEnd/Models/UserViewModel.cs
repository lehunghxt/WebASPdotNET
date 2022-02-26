namespace Web.Backend.Models
{
    using Web.Model;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class UserViewModel
    {
        public string Action { get; set; }

        public int AccountId { get; set; } // dùng để xóa
        public URMUserModel UserModel { get; set; } // dùng để tạo thêm
        public List<URMUserModel> Users { get; set; } // dùng để hiển thị
        public UserViewModel()
        {
            UserModel = new URMUserModel();
            Users = new List<URMUserModel>();
        }
    }
}
