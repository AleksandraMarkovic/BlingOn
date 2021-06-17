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

namespace blingOn.Implementation
{
    public class GetProductsQuery : IGetProductsQuery
    {
        private readonly BlingOnContext _context;
        private readonly IMapper _mapper;

        public GetProductsQuery(BlingOnContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Get products";

        public PagedResponse<ShowProductDto> Execute(ProductSearch search)
        {
            var query = _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Ratings)
                .ThenInclude(x => x.User)
                .Include(x => x.ProductSizes)
                .ThenInclude(x => x.Size)
                .Where(x => x.DeletedAt == null)
                .AsQueryable();

            if (search.Id.HasValue)
            {
                query = query.Where(x => x.Id == search.Id);
                if(query == null)
                {
                    throw new EntityNotFoundException(typeof(Product));
                }
            }

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                search.Keyword = search.Keyword.ToLower();

                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword) ||
                x.Description.ToLower().Contains(search.Keyword) ||
                x.Brand.Name.ToLower().Contains(search.Keyword));
            }

            if (search.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= search.MinPrice);
            }

            if (search.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= search.MaxPrice);
            }

            var products = query.Paged<ShowProductDto, Product>(search, _mapper);

            if (products.Items.Count() == 0)
            {
                throw new EntityNotFoundException(typeof(Product));
            }

            return products;
        }
    }
}
