
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class AttributeValueModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Value { get; set; }
        public int Orders { get; set; }
    }
}  
