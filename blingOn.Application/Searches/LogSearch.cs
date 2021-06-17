using blingOn.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Application.Searches
{
    public class LogSearch : PagedSearch
    {
        public string UseCaseName { get; set; }
        public string Email { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
