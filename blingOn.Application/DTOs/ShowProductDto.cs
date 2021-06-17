using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Application.DTOs
{
    public class ShowProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<SizeDto> Sizes { get; set; } = new List<SizeDto>();
        public IEnumerable<ShowRatingDto> Ratings { get; set; } = new List<ShowRatingDto>();
    }
}
