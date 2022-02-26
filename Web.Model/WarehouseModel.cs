namespace Web.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WarehouseModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int Id { get; set; }
        public string Code { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalCount { get; set; }
        public bool Type { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
