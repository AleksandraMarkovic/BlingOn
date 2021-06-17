using blingOn.Application.DTOs;
using blingOn.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Implementation.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator(BlingOnContext context)
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Price).Must(x => x >= 0.1m).WithMessage("Minimal price is 0.1");
            RuleFor(x => x.BrandId).Must(x => context.Brands.Any(y => y.Id == x)).WithMessage("Brand does not exist.");
            RuleFor(x => x.ProductSizes).NotEmpty().WithMessage("Product size are required.").DependentRules(() =>
            {
                RuleFor(x => x.ProductSizes).Must(productSizes =>
                {
                    var distinct = productSizes.Select(x => x.SizeId).Distinct();
                    return distinct.Count() == productSizes.Count();
                }).WithMessage("There are duplicate product sizes.").DependentRules(() =>
                {
                    RuleForEach(x => x.ProductSizes).SetValidator(new ProductSizeValidator(context));
                });
            });
        }
    }
}
