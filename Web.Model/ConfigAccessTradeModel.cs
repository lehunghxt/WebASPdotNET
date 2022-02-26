
namespace Web.Model
{
    public partial class ConfigAccessTradeModel
    {
        public int ID { get; set; }
        public string SecretKey { get; set; }
        public string AccessKey { get; set; }

        public string SourceId { get; set; }
        public string DeepLink { get; set; }
    }
}  
