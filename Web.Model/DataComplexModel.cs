namespace Web.Model
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class DataComplexModel
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDesc { get; set; }
        public string CategoryImage { get; set; }
        public string Type { get; set; }

        public string JsonItems { get; set; }
        public string JsonChilds { get; set; }

        public IList<DataSimpleModel> Items;

        public IList<DataComplexModel> Childs;

        public DataComplexModel()
        {
            Items = new List<DataSimpleModel>();
            Childs = new List<DataComplexModel>();
        }
    }
}
