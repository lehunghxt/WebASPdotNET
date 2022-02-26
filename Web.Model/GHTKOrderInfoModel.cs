using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model
{
    public class GHTKOrderInfoModel
    {
        public string label_id { get; set; }
        public int partner_id { get; set; }
        public int status { get; set; }
        public string status_text { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string message { get; set; }
        public DateTime? pick_date { get; set; }
        public DateTime deliver_date { get; set; }
        public string customer_fullname { get; set; }
        public string customer_tel { get; set; }
        public string address { get; set; }
        public int storage_day { get; set; }
        public decimal ship_money { get; set; }
        public decimal insurance { get; set; }
        public decimal value { get; set; }
        public int weight { get; set; }
        public decimal pick_money { get; set; }
        public int is_freeship { get; set; }
    }
}
