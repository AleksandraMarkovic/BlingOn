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
    public class RateValidator : AbstractValidator<RateDto>
    {
        public RateValidator(BlingOnContext context)
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product is required.").DependentRules(() =>
            {
                RuleFor(x => x.ProductId).Must(x => context.Products.Any(y => y.Id == x)).WithMessage("Product does not exist.");
            });

            RuleFor(x => x.Rating).NotEmpty().WithMessage("Rating is required.").DependentRules(() =>
            {
                RuleFor(x => x.Rating).Must(x => x >= 1 && x <= 5).WithMessage("Rating must be between 1 and 5.");
            });
        }
    }
}
