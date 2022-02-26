namespace Web.Backend.Models
{
    using Model;
    using System.Collections.Generic;

    public class RoleGroupModel
    {
        public string Action { get; set; }
        public string RoleId { get; set; } // dùng để xóa
        public RoleModel Role { get; set; } // dùng để thêm
        public Dictionary<string, List<RoleModel>> Groups { get; set; } // dùng để hiển thị

        public RoleGroupModel()
        {
            Role = new RoleModel();
            Groups = new Dictionary<string, List<RoleModel>>();
        }
    }
}
