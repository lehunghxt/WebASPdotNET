namespace Web.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class URMUserInfoModel
    {
        /// <sURMmary>
        /// User Id
        /// </sURMmary>
        public int ID { get; set; }        
        public string FullName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
