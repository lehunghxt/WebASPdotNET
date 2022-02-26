
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public partial class VoucherModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsPercent { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public bool Publish { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EffectDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpirDate { get; set; }
    }
}  
