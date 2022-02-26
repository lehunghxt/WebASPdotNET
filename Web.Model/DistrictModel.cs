namespace Web.Model
{
    public class DistrictModel
    {
        public string Code { get; set; }
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public bool IsRepresentative { get; set; }
        public string MiningText { get; set; }
        public int Priority { get; set; }
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int SupportType { get; set; }
        public int Type { get; set; }
    }
}