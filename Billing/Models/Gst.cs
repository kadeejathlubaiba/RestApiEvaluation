using System;
using System.Collections.Generic;

namespace Billing.Models
{
    public partial class Gst
    {
        public int? Cid { get; set; }
        public double? Gstvalue { get; set; }
        public int GstId { get; set; }

        public virtual Category C { get; set; }
    }
}
