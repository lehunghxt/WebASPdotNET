using System.Collections.Generic;

namespace Web.Model
{
    public class GHTKCreateOrderModel
    {
        public ICollection<GHTKProductDto> products { get; set; }
        public GHTKOrderDto order { get; set; }

        public GHTKCreateOrderModel()
        {
            products = new List<GHTKProductDto>();
        }

    }

    public class GHTKOrderDto
    {
        public string id { get; set; }
        public string pick_address_id { get; set; }
        public string pick_name { get; set; }
        public string pick_tel { get; set; }
        public string pick_district { get; set; }
        public string pick_province { get; set; }
        public string pick_address { get; set; }
        public decimal pick_money { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public int is_freeship { get; set; }
        public string note { get; set; }
        public string weight_option { get; set; }
        public double total_weight { get; set; }
        public string pick_option { get; set; }
    }

    public class GHTKProductDto
    {
        public string name { get; set; }
        public double weight { get; set; }
        public int quantity { get; set; }
    }
}
