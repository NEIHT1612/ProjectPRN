using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int? BookId { get; set; }
        public int? OrderQuantity { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual Book? Book { get; set; }
        public virtual OrderTbl Order { get; set; } = null!;
    }
}
