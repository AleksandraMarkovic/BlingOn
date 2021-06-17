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
    public class OrderLineValidator : AbstractValidator<OrderLineDto>
    {
        public OrderLineValidator(BlingOnContext context)
        {
            RuleFor(x => x.Quantity).Must(x => x > 0).WithMessage("Quantity must be greater than zero.");
            RuleFor(x => x.ProductId).Must(x => context.Products.Any(y => y.Id == x))
                .WithMessage("Product does not exist.");
            RuleFor(x => x.SizeId).Must(x => context.Sizes.Any(y => y.Id == x)).WithMessage("Size does not exist.");
        }
    }
}
