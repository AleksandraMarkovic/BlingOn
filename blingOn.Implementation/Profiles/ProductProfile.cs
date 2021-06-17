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
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ShowProductDto>()
                .ForMember(x => x.Sizes, y => y.MapFrom(size => size.ProductSizes.Select(s =>
                new SizeDto
                {
                    Id = s.Size.Id,
                    SizeValue = s.Size.SizeValue
                })))
                .ForMember(x => x.Ratings, y => y.MapFrom(rating => rating.Ratings.Select(r =>
                 new ShowRatingDto
                 {
                        FirstName = r.User.FirstName,
                        LastName = r.User.LastName,
                        Rating = r.RatingValue
                 })));

        }
    }
}
