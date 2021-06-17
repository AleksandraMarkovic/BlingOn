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
    public class DeleteSizeCommand : IDeleteSizeCommand
    {
        private readonly BlingOnContext _context;

        public DeleteSizeCommand(BlingOnContext context)
        {
            _context = context;
        }
        public int Id => 8;

        public string Name => "Delete size";

        public void Execute(int request)
        {
            var size = _context.Sizes.Find(request);

            if (size == null)
            {
                throw new EntityNotFoundException(typeof(Size));
            }

            size.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
