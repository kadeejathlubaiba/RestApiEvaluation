using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.ViewModel
{
    public class ProductViewModel
    {
        public int ProductCode { get; set; }
        public short? Qty { get; set; }
        public double? RatePerUnit { get; set; }
        public double? Gstvalue { get; set; }
        public int? Cid { get; set; }
        public double Net_Rate { get; set; }

        public string Category1 { get; set; }
    }
}


