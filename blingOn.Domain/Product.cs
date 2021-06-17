using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Domain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<ProductSize> ProductSizes { get; set; } = new HashSet<ProductSize>();
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    }
}
