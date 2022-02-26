
using System;

namespace Web.Model
{
    public partial class ConfigGHNModel
    {
        public int ID { get; set; }
        public string GHNUserName { get; set; }
        public string GHNPassword { get; set; }
        public Nullable<int> GHNFromDistrict { get; set; }
        public string GHNToken { get; set; }
    }
}  
