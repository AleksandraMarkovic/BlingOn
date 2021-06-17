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
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator(BlingOnContext context)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.").DependentRules(() =>
            {
                RuleFor(x => x.FirstName).MinimumLength(3).MaximumLength(15).Matches("^[A-Z][a-z]")
                .WithMessage("First name is not entered correctly.");
            });

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.").DependentRules(() =>
            {
                RuleFor(x => x.FirstName).MinimumLength(3).MaximumLength(25).Matches("^[A-Z][a-z]")
                .WithMessage("Last name is not entered correctly.");
            });

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.").DependentRules(() =>
            {
                RuleFor(x => x.Password).MinimumLength(8).Matches("[a-z]").Matches("[0-9]")
                .WithMessage("Password is not entered correctly.");
            });

            
        }
    }
}
