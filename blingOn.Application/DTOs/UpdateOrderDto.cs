using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Application.DTOs
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public DateTime DeliveredAt { get; set; }
    }
}
