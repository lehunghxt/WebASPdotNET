
namespace Web.Model
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ThongKeLoiNhuanModel
    {
        public string Thang { get; set; }
        public decimal TongThu { get; set; }
        public decimal TongChi { get; set; }
        public decimal LoiNhuan { get; set; }
    }
}  
