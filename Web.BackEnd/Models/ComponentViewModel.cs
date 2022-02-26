namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class ComponentViewModel
    {
        public string Action { get; set; }
        public TemplateComponentPositionModel Position { get; set; }
        public TemplateComponentModel Component { get; set; } // dùng để tạo thêm
        public IList<TemplateComponentModel> Components { get; set; }
        public IList<TemplateComponentPositionModel> Positions { get; set; }

        public ComponentViewModel()
        {
            Component = new TemplateComponentModel();
            Components = new List<TemplateComponentModel>();
            Positions = new List<TemplateComponentPositionModel>();
        }
    }
}
