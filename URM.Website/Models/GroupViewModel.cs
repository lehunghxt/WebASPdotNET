namespace URM.Website.Models
{
    using System.Collections.Generic;
    using URM.Model;

    public class GroupViewModel
    {
        public string Action { get; set; }

        public int GroupId { get; set; } // dùng để xóa
        public string GroupName { get; set; } // dùng để Cập nhật
        public URMGroupModel Group { get; set; } // dùng để tạo thêm
        public IList<URMGroupModel> Groups { get; set; }
        public Dictionary<string, List<URMRoleModel>> Roles { get; set; } // dùng để hiển thị

        public GroupViewModel()
        {
            Group = new URMGroupModel();
            Groups = new List<URMGroupModel>();
            Roles = new Dictionary<string, List<URMRoleModel>>();
        }
    }
}
