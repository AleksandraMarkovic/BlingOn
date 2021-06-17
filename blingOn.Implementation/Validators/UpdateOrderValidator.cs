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
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator(BlingOnContext context)
        {
            RuleFor(x => x.DeliveredAt).NotEmpty().WithMessage("Delivered date is required.").DependentRules(() => 
            {
                RuleFor(x => x.DeliveredAt).Must(x => x <= DateTime.UtcNow);
            }
            );
        }
    }
}
