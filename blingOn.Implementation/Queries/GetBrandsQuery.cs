using AutoMapper;
using blingOn.Application.DTOs;
using blingOn.Application.Exceptions;
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
    public class GetBrandsQuery : IGetBrandsQuery
    {
        private readonly BlingOnContext _context;
        private readonly IMapper _mapper;

        public GetBrandsQuery(BlingOnContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Get brands";

        public PagedResponse<ShowBrandDto> Execute(BrandSearch search)
        {
            var query = _context.Brands.Include(x => x.Products).Where(x => x.DeletedAt == null).AsQueryable();

            if(!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                search.Name = search.Name.ToLower();

                query = query.Where(x => x.Name.ToLower().Contains(search.Name));
            }

            var brands = query.Paged<ShowBrandDto, Brand>(search, _mapper);

            if (brands.Items.Count() == 0)
            {
                throw new EntityNotFoundException(typeof(Brand));
            }
            return brands;
        }
    }
}
