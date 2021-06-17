using blingOn.Application.Commands;
using blingOn.Application.DTOs;
using blingOn.Application.Exceptions;
using blingOn.DataAccess;
using blingOn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Implementation.Commands
{
    public class DeleteBrandCommand : IDeleteBrandCommand
    {
        private readonly BlingOnContext _context;

        public DeleteBrandCommand(BlingOnContext context)
        {
            _context = context;
        }

        public int Id => 6;

        public string Name => "Delete brand";

        public void Execute(int request)
        {
            var brand = _context.Brands.Find(request);

            if(brand == null)
            {
                throw new EntityNotFoundException(typeof(Brand));
            }

            brand.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
