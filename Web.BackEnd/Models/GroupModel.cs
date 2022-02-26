namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using Web.Model;

    public class GroupModel
    {
        public string Action { get; set; }

        public int GroupId { get; set; } // dùng để xóa
        public string GroupName { get; set; } // dùng để Cập nhật
        public UserGroupModel Group { get; set; } // dùng để tạo thêm
        public IList<UserGroupModel> Groups { get; set; }
        public Dictionary<string, List<RoleModel>> Roles { get; set; } // dùng để hiển thị

        public GroupModel()
        {
            Group = new UserGroupModel();
            Groups = new List<UserGroupModel>();
            Roles = new Dictionary<string, List<RoleModel>>();
        }
    }
}
