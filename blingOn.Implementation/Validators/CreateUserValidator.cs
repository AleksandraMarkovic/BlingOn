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
    public class CreateUserValidator : UserValidator
    {
        public CreateUserValidator(BlingOnContext context) : base(context)
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").DependentRules(() =>
            {
                RuleFor(x => x.Email).Must(x => !context.Users.Any(y => y.Email == x)).WithMessage("Email already exists.");
            });
        }
    }
}
