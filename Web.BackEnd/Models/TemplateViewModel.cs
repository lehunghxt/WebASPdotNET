namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class TemplateViewModel
    {
        public string Action { get; set; }
        public TemplatePositionModel Position { get; set; }
        public TemplateSkinModel Skin { get; set; }
        public TemplateModel Template { get; set; } // dùng để tạo thêm
        public IList<TemplateModel> Templates { get; set; }
        public IList<TemplatePositionModel> Positions { get; set; }
        public IList<TemplateSkinModel> Skins { get; set; }
        public IList<ModuleModel> Modules { get; set; }

        public TemplateViewModel()
        {
            Template = new TemplateModel();
            Templates = new List<TemplateModel>();
            Positions = new List<TemplatePositionModel>();
            Skins = new List<TemplateSkinModel>();
            Modules = new List<ModuleModel>();
        }
    }
}
