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
    public class DeleteUserCommand : IDeleteUserCommand
    {
        private readonly BlingOnContext _context;

        public DeleteUserCommand(BlingOnContext context)
        {
            _context = context;
        }

        public int Id => 13;

        public string Name => "Delete user";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            user.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
