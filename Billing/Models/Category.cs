using System;
using System.Collections.Generic;

namespace Billing.Models
{
    public partial class Category
    {
        public Category()
        {
            Gst = new HashSet<Gst>();
            Products = new HashSet<Products>();
        }

        public int Cid { get; set; }
        public string Category1 { get; set; }

        public virtual ICollection<Gst> Gst { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
