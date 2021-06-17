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
    public class CreateOrderValidator : AbstractValidator<OrderDto>
    {
        public CreateOrderValidator(BlingOnContext context)
        {
            RuleFor(x => x.OrderLines).NotEmpty().WithMessage("Order lines are required.").DependentRules(() =>
            {
                RuleFor(x => x.OrderLines).Must(orderLines =>
                    orderLines.Select(o => o.ProductId + o.SizeId).Distinct().Count() == orderLines.Count()
                ).WithMessage("There are duplicate order lines.").DependentRules(() =>
                {
                    RuleForEach(x => x.OrderLines).SetValidator(new OrderLineValidator(context));
                });
            });
        }
    }
}
