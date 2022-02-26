
using System.Collections.Generic;

namespace Web.Model
{
    public partial class MenuShortcutModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryType { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int No { get; set; }
        public bool IsCategories { get; set; }
        public string Blank { get; set; }

        public MenuShortcutModel Shortcut { get; set; }
        public List<MenuShortcutModel> Shortcuts { get; set; }

        public MenuShortcutModel()
        {
            Shortcuts = new List<MenuShortcutModel>();
        }
    }
}  
