using blingOn.Application.Commands;
using blingOn.Application.DTOs;
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
    public class CreateSizeCommand : ICreateSizeCommand
    {
        private readonly BlingOnContext _context;
        private readonly CreateSizeValidator _validator;

        public CreateSizeCommand(BlingOnContext context, CreateSizeValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Insert size";

        public void Execute(SizeDto request)
        {
            _validator.ValidateAndThrow(request);

            var size = new Size
            {
                SizeValue = request.SizeValue
            };

            _context.Sizes.Add(size);
            _context.SaveChanges();
        }
    }
}
