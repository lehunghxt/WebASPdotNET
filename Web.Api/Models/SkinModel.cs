using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models
{
    public class SkinModel
    {
        public string SkinName { get; set; }
        public string Title { get; set; }
        public Dictionary<string, object> Params { get; set; }

        public decimal Data { get; set; }

        public SkinModel()
        {
            Params = new Dictionary<string, object>();
        }
    }
}