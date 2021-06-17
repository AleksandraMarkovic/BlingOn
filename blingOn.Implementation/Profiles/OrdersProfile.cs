using AutoMapper;
using blingOn.Application.DTOs;
using blingOn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Implementation.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, ShowOrderDto>()
                .ForMember(x => x.ShowOrderLines, y => y.MapFrom(order => order.OrderLines.Select(o =>
                new ShowOrderLineDto
                {
                    ProductName = o.ProductName,
                    Price = o.Price,
                    Quantity = o.Quantity
                })));
        }
    }
}
