using System;
using System.Collections.Generic;

namespace Web.Model
{
    public class GHPostOrderModel
    {
        public bool SendFromCenter { get; set; }
        public decimal COD { get; set; }
        public string ToDistrict { get; set; }
        public double Weight { get; set; }

        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool ReceiverPay { get; set; }
    }
}