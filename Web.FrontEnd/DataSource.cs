namespace Web.FrontEnd
{
    using System.Linq;
    using System.Collections.Generic;
    using Web.Business;
    using Web.Model;

    public static class DataSource
    {
        private static IEnumerable<ItemIntSimple> ProvinceSources = new List<ItemIntSimple>
        {
            new ItemIntSimple { Id = 202, Name = "Hồ Chí Minh" },
            new ItemIntSimple { Id = 201, Name = "Hà Nội" },
            new ItemIntSimple { Id = 217, Name = "An Giang" },
            new ItemIntSimple { Id = 206, Name = "Bà Rịa - Vũng Tàu" },
            new ItemIntSimple { Id = 248, Name = "Bắc Giang" },
            new ItemIntSimple { Id = 245, Name = "Bắc Kạn" },
            new ItemIntSimple { Id = 253, Name = "Bạc Liêu" },
            new ItemIntSimple { Id = 249, Name = "Bắc Ninh" },
            new ItemIntSimple { Id = 213, Name = "Bến Tre" },
            new ItemIntSimple { Id = 262, Name = "Bình Định" },
            new ItemIntSimple { Id = 205, Name = "Bình Dương" },
            new ItemIntSimple { Id = 239, Name = "Bình Phước" },
            new ItemIntSimple { Id = 258, Name = "Bình Thuận" },
            new ItemIntSimple { Id = 252, Name = "Cà Mau" },
            new ItemIntSimple { Id = 220, Name = "Cần Thơ" },
            new ItemIntSimple { Id = 246, Name = "Cao Bằng" },
            new ItemIntSimple { Id = 203, Name = "Đà Nẵng" },
            new ItemIntSimple { Id = 241, Name = "Đắc Nông" },
            new ItemIntSimple { Id = 210, Name = "Đắk Lắk" },
            new ItemIntSimple { Id = 265, Name = "Điện Biên" },
            new ItemIntSimple { Id = 204, Name = "Đồng Nai" },
            new ItemIntSimple { Id = 216, Name = "Đồng Tháp" },
            new ItemIntSimple { Id = 207, Name = "Gia Lai" },
            new ItemIntSimple { Id = 227, Name = "Hà Giang" },
            new ItemIntSimple { Id = 232, Name = "Hà Nam" },
            new ItemIntSimple { Id = 236, Name = "Hà Tĩnh" },
            new ItemIntSimple { Id = 225, Name = "Hải Dương" },
            new ItemIntSimple { Id = 224, Name = "Hải Phòng" },
            new ItemIntSimple { Id = 250, Name = "Hậu Giang" },
            new ItemIntSimple { Id = 267, Name = "Hoà Bình" },
            new ItemIntSimple { Id = 268, Name = "Hưng Yên" },
            new ItemIntSimple { Id = 208, Name = "Khánh Hòa" },
            new ItemIntSimple { Id = 219, Name = "Kiên Giang" },
            new ItemIntSimple { Id = 259, Name = "Kon Tum" },
            new ItemIntSimple { Id = 264, Name = "Lai Châu" },
            new ItemIntSimple { Id = 209, Name = "Lâm Đồng" },
            new ItemIntSimple { Id = 247, Name = "Lạng Sơn" },
            new ItemIntSimple { Id = 269, Name = "Lào Cai" },
            new ItemIntSimple { Id = 211, Name = "Long An" },
            new ItemIntSimple { Id = 231, Name = "Nam Định" },
            new ItemIntSimple { Id = 235, Name = "Nghệ An" },
            new ItemIntSimple { Id = 233, Name = "Ninh Bình" },
            new ItemIntSimple { Id = 261, Name = "Ninh Thuận" },
            new ItemIntSimple { Id = 229, Name = "Phú Thọ" },
            new ItemIntSimple { Id = 260, Name = "Phú Yên" },
            new ItemIntSimple { Id = 237, Name = "Quảng Bình" },
            new ItemIntSimple { Id = 243, Name = "Quảng Nam" },
            new ItemIntSimple { Id = 242, Name = "Quảng Ngãi" },
            new ItemIntSimple { Id = 230, Name = "Quảng Ninh" },
            new ItemIntSimple { Id = 238, Name = "Quảng Trị" },
            new ItemIntSimple { Id = 218, Name = "Sóc Trăng" },
            new ItemIntSimple { Id = 266, Name = "Sơn La" },
            new ItemIntSimple { Id = 240, Name = "Tây Ninh" },
            new ItemIntSimple { Id = 226, Name = "Thái Bình" },
            new ItemIntSimple { Id = 244, Name = "Thái Nguyên" },
            new ItemIntSimple { Id = 234, Name = "Thanh Hoá" },
            new ItemIntSimple { Id = 223, Name = "Thừa Thiên Huế" },
            new ItemIntSimple { Id = 212, Name = "Tiền Giang" },
            new ItemIntSimple { Id = 214, Name = "Trà Vinh" },
            new ItemIntSimple { Id = 228, Name = "Tuyên Quang" },
            new ItemIntSimple { Id = 215, Name = "Vĩnh Long" },
            new ItemIntSimple { Id = 221, Name = "Vĩnh Phúc" },
            new ItemIntSimple { Id = 263, Name = "Yên Bái" },
        };
        public static List<ItemIntSimple> ProvinceSourceCollection = ProvinceSources.ToList();

        private static IEnumerable<ItemStringSimple> OnePayVisaStatus = new List<ItemStringSimple>
        {
            new ItemStringSimple { Id = "00", Name = "Giao dịch thành công." },
            new ItemStringSimple { Id = "01", Name = "Ngân hàng từ chối thanh toán: thẻ/tài khoản bị khóa." },
            new ItemStringSimple { Id = "03", Name = "Mã đơn vị không tồn tại." },
            new ItemStringSimple { Id = "04", Name = "Không đúng access code." },
            new ItemStringSimple { Id = "05", Name = "Số tiền không hợp lệ." },
            new ItemStringSimple { Id = "06", Name = "Mã tiền tệ không tồn tại" },
            new ItemStringSimple { Id = "07", Name = "Người sử dụng hủy giao dịch." },
            new ItemStringSimple { Id = "08", Name = "Số thẻ không đúng." },
            new ItemStringSimple { Id = "09", Name = "Tên chủ thẻ không đúng." },
            new ItemStringSimple { Id = "10", Name = "Thẻ hết hạn/Thẻ bị khóa." },
            new ItemStringSimple { Id = "11", Name = "Thẻ chưa đăng ký sử dụng dịch vụ." },
            new ItemStringSimple { Id = "12", Name = "Ngày phát hành/Hết hạn không đúng." },
            new ItemStringSimple { Id = "13", Name = "Vượt quá hạn mức thanh toán." },
            new ItemStringSimple { Id = "21", Name = "Số tiền không đủ để thanh toán" },
            new ItemStringSimple { Id = "99", Name = "Không xác định." },
        };
        public static List<ItemStringSimple> OnePayVisaStatusCollection = OnePayVisaStatus.ToList();

        private static IEnumerable<ItemStringSimple> OnePayATMStatus = new List<ItemStringSimple>
        {
            new ItemStringSimple { Id = "00", Name = "Giao dịch thành công." },
            new ItemStringSimple { Id = "01", Name = "Ngân hàng từ chối thanh toán: thẻ/tài khoản bị khóa." },
            new ItemStringSimple { Id = "02", Name = "Thông tin thẻ không hợp lệ." },
            new ItemStringSimple { Id = "03", Name = "Thẻ hết hạn." },
            new ItemStringSimple { Id = "04", Name = "Lỗi người mua hàng: Quá số lần cho phép. (Sai OTP, quá hạn mức trong ngày)." },
            new ItemStringSimple { Id = "05", Name = "Không có trả lời của Ngân hàng." },
            new ItemStringSimple { Id = "06", Name = "Lỗi giao tiếp với Ngân hàng." },
            new ItemStringSimple { Id = "07", Name = "Tài khoản không đủ tiền." },
            new ItemStringSimple { Id = "08", Name = "Lỗi dữ liệu." },
            new ItemStringSimple { Id = "09", Name = "Kiểu giao dịch không được hỗ trợ." },
            new ItemStringSimple { Id = "10", Name = "Giao dịch không thành công." },
            new ItemStringSimple { Id = "11", Name = "Giao dịch chưa xác thực OTP." },
            new ItemStringSimple { Id = "12", Name = "Giao dịch không thành công, số tiền giao dịch vượt hạn mức ngày." },
            new ItemStringSimple { Id = "13", Name = "Thẻ chưa đăng ký Internet Banking" },
            new ItemStringSimple { Id = "14", Name = "Khách hàng nhập sai OTP." },
            new ItemStringSimple { Id = "15", Name = "Khách hàng nhập sai thông tin xác thực." },
            new ItemStringSimple { Id = "16", Name = "Khách hàng nhập sai tên chủ thẻ." },
            new ItemStringSimple { Id = "17", Name = "Khách hàng nhập sai số thẻ." },
            new ItemStringSimple { Id = "18", Name = "Khách hàng nhập sai ngày phát hành thẻ." },
            new ItemStringSimple { Id = "19", Name = "Khách hàng nhập sai ngày hết hạn thẻ." },
            new ItemStringSimple { Id = "20", Name = "OTP hết thời gian hiệu lực." },
            new ItemStringSimple { Id = "21", Name = "Quá thời gian thực hiện request (7 phút) hoặc OTP timeout." },
            new ItemStringSimple { Id = "22", Name = "Khách hàng chưa xác thực thông tin thẻ." },
            new ItemStringSimple { Id = "23", Name = "Thẻ không đủ điều kiện thanh toán (Thẻ/Tài khoản không hợp lệ hoặc TK không đủ số dư)." },
            new ItemStringSimple { Id = "24", Name = "Giao dịch vượt quá hạn mức một lần thanh toán của ngân hàng." },
            new ItemStringSimple { Id = "25", Name = "Giao dịch vượt quá hạn mức của ngân hàng." },
            new ItemStringSimple { Id = "26", Name = "Giao dịch chờ xác nhận từ Ngân hàng." },
            new ItemStringSimple { Id = "27", Name = "Khách hàng nhập sai thông tin bảo mật thẻ." },
            new ItemStringSimple { Id = "28", Name = "Giao dịch không thành công do quá thời gian quy định." },
            new ItemStringSimple { Id = "29", Name = "Lỗi xử lý giao dịch tại hệ thống Ngân hàng." },
            new ItemStringSimple { Id = "99", Name = "Không xác định." },
        };
        public static List<ItemStringSimple> OnePayATMStatusCollection = OnePayATMStatus.ToList();
    }

    public class ItemStringSimple
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ItemIntSimple
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}