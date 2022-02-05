using System;
using System.Collections.Generic;

namespace Billing.Models
{
    public partial class Products
    {
        public int ProductCode { get; set; }
        public string DescProduct { get; set; }
        public short? Qty { get; set; }
        public double? RatePerUnit { get; set; }
        public int? Cid { get; set; }

        public virtual Category C { get; set; }
    }
}
