using AutoMapper;
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
    public class UpdateBrandCommand : IUpdateBrandCommand
    {
        private readonly BlingOnContext _context;
        private readonly UpdateBrandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateBrandCommand(BlingOnContext context, UpdateBrandValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }
        public int Id => 5;

        public string Name => "Update brand";

        public void Execute(BrandDto request)
        {
            _validator.ValidateAndThrow(request);
            var brand = _context.Brands.Where(x => x.DeletedAt == null).FirstOrDefault(x => x.Id == request.Id);

            if(brand == null)
            {
                throw new EntityNotFoundException(typeof(Brand));
            }

            brand.Name = request.Name;
            brand.DateModified = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
