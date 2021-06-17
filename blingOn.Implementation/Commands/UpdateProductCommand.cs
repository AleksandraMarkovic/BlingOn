using blingOn.Application.Commands;
using blingOn.Application.DTOs;
using blingOn.Application.Exceptions;
using blingOn.DataAccess;
using blingOn.Domain;
using blingOn.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Implementation.Commands
{
    public class UpdateProductCommand : IUpdateProductCommand
    {
        private readonly BlingOnContext _context;
        private readonly UpdateProductValidator _validator;

        public UpdateProductCommand(BlingOnContext context, UpdateProductValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 10;

        public string Name => "Update product";

        public void Execute(ProductDto request)
        {
            _validator.ValidateAndThrow(request);
            var product = _context.Products.Where(x => x.DeletedAt == null).FirstOrDefault(x => x.Id == request.Id);

            if (product == null)
            {
                throw new EntityNotFoundException(typeof(Product));
            }

            if(request.ImagePath != null)
            {
                var guid = Guid.NewGuid();
                var extension = Path.GetExtension(request.ImagePath.FileName);
                var newImage = guid + extension;

                product.ImagePath = newImage;
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.BrandId = request.BrandId;
            product.ProductSizes = request.ProductSizes.Select(x =>
            {
                return new ProductSize
                {
                    SizeId = x.SizeId,
                    Quanity = x.Quantity
                };
            }).ToList();
            product.DateModified = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
