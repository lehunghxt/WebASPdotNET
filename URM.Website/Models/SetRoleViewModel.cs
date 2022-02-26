namespace URM.Website.Models
{
    using System.Collections.Generic;
    using URM.Model;

    public class SetRoleViewModel
    {
        public string Action { get; set; }
        public URMUserModel User { get; set; }
        public Dictionary<string, List<URMRoleModel>> Roles { get; set; } // dùng để hiển thị

        public SetRoleViewModel()
        {
            Roles = new Dictionary<string, List<URMRoleModel>>();
        }
    }
}
