using blingOn.Application.DTOs;
using blingOn.Application.Interfaces;
using blingOn.Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Application.Queries
{
    public interface IGetOrdersQuery : IQuery<OrderSearch, PagedResponse<ShowOrderDto>>
    {
    }
}
