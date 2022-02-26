
using System;

namespace Web.Model
{
    public partial class ProductWebModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Brief { get; set; }
        public string Description { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sale { get; set; }
        public int SaleMin { get; set; }

        public int VoteRate { get; set; }
        public int VoteNumber { get; set; }
        public int Vote
        {
            get
            {
                return this.VoteNumber == 0 ? 1 : this.VoteRate / this.VoteNumber;
            }
        }

        public int Quantity { get; set; }
        public int ViewNumber { get; set; }
        public int PayNumber { get; set; }

        public DateTime CreateDate { get; set; }
    }
}  
