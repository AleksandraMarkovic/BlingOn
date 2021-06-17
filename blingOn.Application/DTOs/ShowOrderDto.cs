using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Application.DTOs
{
    public class ShowOrderDto
    {
        public DateTime PlacedAt { get; set; }
        public IEnumerable<ShowOrderLineDto> ShowOrderLines { get; set; } = new List<ShowOrderLineDto>();
    }

    public class ShowOrderLineDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
