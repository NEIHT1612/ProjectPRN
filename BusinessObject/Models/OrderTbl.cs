using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class OrderTbl
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? MemberId { get; set; }
        public decimal? Freight { get; set; }

        public virtual Member? Member { get; set; }
        public virtual OrderDetail OrderDetail { get; set; } = null!;
    }
}
