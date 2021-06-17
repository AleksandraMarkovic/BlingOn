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
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, ShowBrandDto>()
                .ForMember(x => x.Products, y => y.MapFrom(prod => prod.Products.Select(p =>
                new ShowProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                })));

            CreateMap<BrandDto, Brand>();
        }
    }
}
