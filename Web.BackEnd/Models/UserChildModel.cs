namespace Web.Backend.Models
{
    using Web.Model;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class UserChildModel
    {
        public string Action { get; set; }

        public int AccountId { get; set; } // dùng để xóa
        public UserModel UserModel { get; set; } // dùng để tạo thêm
        public Dictionary<string, List<UserModel>> Childs { get; set; } // dùng để hiển thị
        public IEnumerable<SelectListItem> Groups { get; set; }

        public UserChildModel()
        {
            UserModel = new UserModel();
            Childs = new Dictionary<string, List<UserModel>>();
            Groups = new List<SelectListItem>();
        }
    }
}
