using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Application.DTOs
{
    public class ShowBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ShowProductDto> Products { get; set; } = new List<ShowProductDto>();
    }
}
