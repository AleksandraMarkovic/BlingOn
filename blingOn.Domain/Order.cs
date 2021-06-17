using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Domain
{
    public class Order : Entity
    {
        public DateTime PlacedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new HashSet<OrderLine>();
    }
}
