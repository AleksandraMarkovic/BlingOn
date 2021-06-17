using blingOn.Application.Commands;
using blingOn.Application.DTOs;
using blingOn.Application.Exceptions;
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
    public class UpdateUserCommand : IUpdateUserCommand
    {
        private readonly BlingOnContext _context;
        private readonly UpdateUserValidator _validator;

        public UpdateUserCommand(BlingOnContext context, UpdateUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 12;

        public string Name => "Update user";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);
            var user = _context.Users.Find(request.Id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Password = request.Password;
            user.Address = request.Address;
            user.DateModified = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
