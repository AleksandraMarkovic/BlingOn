using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? ImagePath { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<ProductSizeDto> ProductSizes { get; set; } = new List<ProductSizeDto>();
    }

    public class ProductSizeDto
    {
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
