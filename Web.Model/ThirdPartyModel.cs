
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class ThirdPartyModel
    {
        public int Id { get; set; }
        public string ThirdPartyName { get; set; }
        public string ContentHTML { get; set; }
        public string PositionName { get; set; }
        public string TemplateName { get; set; }
        public bool IsPublished { get; set; }

    }
}  
