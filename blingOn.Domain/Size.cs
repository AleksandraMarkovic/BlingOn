using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Domain
{
    public class Size : Entity
    {
        public int SizeValue { get; set; }
        public virtual ICollection<ProductSize> SizeProducts { get; set; } = new HashSet<ProductSize>();
    }
}
