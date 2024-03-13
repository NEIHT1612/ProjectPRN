using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Book
    {
        public Book()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int BookId { get; set; }
        public string? Title { get; set; }
        public byte[]? BookImage { get; set; }
        public string? SerialNumber { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
