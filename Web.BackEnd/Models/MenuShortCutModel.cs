namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using Web.Model;

    public class MenuShortcutViewModel
    {
        public string Action { get; set; }
        public MenuShortcutModel Shortcut { get; set; }
        public IList<MenuShortcutModel> Shortcuts { get; set; }
        public IList<CATEGORYLANGUAGEModel> Categories { get; set; }

        public MenuShortcutViewModel()
        {
            Shortcut = new MenuShortcutModel();
            Shortcuts = new List<MenuShortcutModel>();
            Categories = new List<CATEGORYLANGUAGEModel>();
        }
    }
}
