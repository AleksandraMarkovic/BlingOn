using blingOn.Application.Commands;
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
    public class DeleteProductCommand : IDeleteProductCommand
    {
        private readonly BlingOnContext _context;

        public DeleteProductCommand(BlingOnContext context)
        {
            _context = context;
        }
        public int Id => 11;

        public string Name => "Delete product";

        public void Execute(int request)
        {
            var product = _context.Products.Find(request);

            if (product == null)
            {
                throw new EntityNotFoundException(typeof(Product));
            }

            product.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
