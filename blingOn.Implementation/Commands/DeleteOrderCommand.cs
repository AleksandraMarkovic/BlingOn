using blingOn.Application.Commands;
using blingOn.Application.Exceptions;
using blingOn.Application.Interfaces;
using blingOn.DataAccess;
using blingOn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Implementation.Commands
{
    public class DeleteOrderCommand : IDeleteOrderCommand
    {
        private readonly BlingOnContext _context;
        private readonly IApplicationActor _actor;

        public DeleteOrderCommand(BlingOnContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public int Id => 16;

        public string Name => "Delete order";

        public void Execute(int request)
        {
            var order = _context.Orders.Find(request);

            if (order == null)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            order.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
