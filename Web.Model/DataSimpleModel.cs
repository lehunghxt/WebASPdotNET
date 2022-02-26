namespace Web.Model
{
    using System;

    [Serializable]
    public class DataSimpleModel
    {
        public int ID { get; set; }
        public int CategoryId { get; set; }
        public string URL { get; set; }
        public string TargetTag { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
