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
    public class CreateBrandValidator : AbstractValidator<BrandDto>
    {
        public CreateBrandValidator(BlingOnContext context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Brand name is required.").DependentRules(() =>
            {
                RuleFor(x => x.Name).Must(x => !context.Brands.Any(y => y.Name == x && y.DeletedAt == null))
                .WithMessage("Brand name already exists.");
            });
        }
    }
}
