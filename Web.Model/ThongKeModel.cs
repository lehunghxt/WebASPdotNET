
namespace Web.Model
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ThongKeModel
    {
        public int SanPhamSapHet { get; set; }
        public int SanPhamDaHet { get; set; }
        public int SanPhamAm { get; set; }
        public int DonHangMoi { get; set; }
        public int DonHangChuaGui { get; set; }
        public decimal TongTienNhap { get; set; }
        public decimal TongTienBan { get; set; }
        public decimal TongTienTonKho { get; set; }
    }
}  
