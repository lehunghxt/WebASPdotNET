namespace URM.Website.Models
{
    using Model;
    using System.Collections.Generic;

    public class RoleGroupModel
    {
        public string Action { get; set; }
        public string RoleId { get; set; } // dùng để xóa
        public URMRoleModel Role { get; set; } // dùng để thêm
        public Dictionary<string, List<URMRoleModel>> Groups { get; set; } // dùng để hiển thị

        public RoleGroupModel()
        {
            Role = new URMRoleModel();
            Groups = new Dictionary<string, List<URMRoleModel>>();
        }
    }
}
