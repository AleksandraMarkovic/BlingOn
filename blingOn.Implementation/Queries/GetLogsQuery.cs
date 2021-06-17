using AutoMapper;
using blingOn.Application.DTOs;
using blingOn.Application.Exceptions;
using blingOn.Application.Queries;
using blingOn.Application.Searches;
using blingOn.DataAccess;
using blingOn.Domain;
using blingOn.Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blingOn.Implementation.Queries
{
    public class GetLogsQuery : IGetLogsQuery
    {
        private readonly BlingOnContext _context;
        private readonly IMapper _mapper;

        public GetLogsQuery(BlingOnContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 19;

        public string Name => "Get logs";

        public PagedResponse<LogDto> Execute(LogSearch search)
        {
            var query = _context.UseCaseLogs.AsQueryable();

            if(!string.IsNullOrEmpty(search.UseCaseName) && !string.IsNullOrWhiteSpace(search.UseCaseName))
            {
                search.UseCaseName = search.UseCaseName.ToLower();

                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCaseName));
            }

            if (!string.IsNullOrEmpty(search.Email) && !string.IsNullOrWhiteSpace(search.Email))
            {
                search.Email = search.Email.ToLower();

                query = query.Where(x => x.Email.ToLower().Contains(search.Email));
            }

            if (search.DateFrom.HasValue && search.DateTo.HasValue)
            {
                query = query.Where(x => x.Date >= search.DateFrom && x.Date <= search.DateTo);
            }

            var logs = query.Paged<LogDto, UseCaseLog>(search, _mapper);

            if (logs.Items.Count() == 0)
            {
                throw new EntityNotFoundException(typeof(UseCaseLog));
            }
            return logs;
        }
    }
}
