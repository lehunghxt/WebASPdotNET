namespace Web.Model
{
    using System;
    public class OrderProductModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductProperties { get; set; }
        public int IOId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalCost { get; set; }

        public bool IsAddOn { get; set; }
        public bool IsGroupon { get; set; }
    }
}
