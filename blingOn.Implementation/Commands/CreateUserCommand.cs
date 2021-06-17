using blingOn.Application.Commands;
using blingOn.Application.DTOs;
using blingOn.Application.Email;
using blingOn.DataAccess;
using blingOn.Domain;
using blingOn.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Implementation.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly BlingOnContext _context;
        private readonly CreateUserValidator _validator;
        private readonly IEmailSender _sender;
        public CreateUserCommand(BlingOnContext context, CreateUserValidator validator, IEmailSender sender)
        {
            _context = context;
            _validator = validator;
            _sender = sender;
        }

        public int Id => 3;

        public string Name => "User registration";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);
            var role = _context.Roles.FirstOrDefault(x => x.Name == "User");

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Address = request.Address,
                RoleId = role.Id
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            _sender.Send(new SendEmailDto
            {
                Content = "You have successfully registered!",
                SendTo = request.Email,
                Subject = "Registration"
            });
        }
    }
}
