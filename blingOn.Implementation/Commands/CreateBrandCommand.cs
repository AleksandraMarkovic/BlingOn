using AutoMapper;
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
    public class CreateBrandCommand : ICreateBrandCommand
    {
        private readonly BlingOnContext _context;
        private readonly CreateBrandValidator _validator;
        private readonly IMapper _mapper;

        public CreateBrandCommand(BlingOnContext context, CreateBrandValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Insert brand";

        public void Execute(BrandDto request)
        {
            _validator.ValidateAndThrow(request);
            var brand = _mapper.Map<Brand>(request);
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }
    }
}
