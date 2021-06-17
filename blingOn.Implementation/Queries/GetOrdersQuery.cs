using AutoMapper;
using blingOn.Application.DTOs;
using blingOn.Application.Exceptions;
using blingOn.Application.Interfaces;
using blingOn.Application.Queries;
using blingOn.Application.Searches;
using blingOn.DataAccess;
using blingOn.Domain;
using blingOn.Implementation.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Implementation.Queries
{
    public class GetOrdersQuery : IGetOrdersQuery
    {
        private readonly BlingOnContext _context;
        private readonly IApplicationActor _actor;
        private readonly IMapper _mapper;

        public GetOrdersQuery(BlingOnContext context, IMapper mapper, IApplicationActor actor)
        {
            _context = context;
            _mapper = mapper;
            _actor = actor;
        }
        public int Id => 17;

        public string Name => "Get orders";

        public PagedResponse<ShowOrderDto> Execute(OrderSearch search)
        {
            var query = _context.Orders.Include(x => x.OrderLines).Where(x => x.DeletedAt == null).AsQueryable();

            if (search.PlacedAt.HasValue)
            {
                query = query.Where(x => x.PlacedAt == search.PlacedAt);
            }

            if(!string.IsNullOrEmpty(search.ProductName) && !string.IsNullOrWhiteSpace(search.ProductName))
            {
                search.ProductName = search.ProductName.ToLower();

                query = query.Where(x => x.OrderLines.Any(y => y.ProductName.Contains(search.ProductName)));
            }

            var user = _context.Users.Find(_actor.Id);

            if(user.RoleId == 2)
            {
                query = query.Where(x => x.UserId == user.Id);
            }

            var orders = query.Paged<ShowOrderDto, Order>(search, _mapper);

            if (orders.Items.Count() == 0)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            return orders;
        }
    }
}
