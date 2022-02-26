
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class AttributeCategoryModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public int CountAttributeValues { get; set; }
    }
}  
