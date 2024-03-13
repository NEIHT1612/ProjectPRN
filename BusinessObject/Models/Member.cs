using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Member
    {
        public Member()
        {
            OrderTbls = new HashSet<OrderTbl>();
        }

        public int MemberId { get; set; }
        public string? MemberName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Contact { get; set; }

        public virtual ICollection<OrderTbl> OrderTbls { get; set; }
    }
}
