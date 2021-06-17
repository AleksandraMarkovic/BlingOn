using blingOn.Application.Interfaces;
using blingOn.DataAccess;
using blingOn.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Implementation.Logging
{
	public class DatabaseUseCaseLogger : IUseCaseLogger
	{
        private readonly BlingOnContext _context;

        public DatabaseUseCaseLogger(BlingOnContext context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IApplicationActor actor, object useCaseData)
        {
            _context.UseCaseLogs.Add(new UseCaseLog
            {
                UserId = actor.Id,
                UseCaseName = useCase.Name,
                Data = JsonConvert.SerializeObject(useCaseData),
                Date = DateTime.UtcNow,
                Email = actor.Email
            });

            _context.SaveChanges();
        }
    }
}
