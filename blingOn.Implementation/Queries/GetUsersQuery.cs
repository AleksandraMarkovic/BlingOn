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
    public class GetUsersQuery : IGetUsersQuery
    {
        private readonly BlingOnContext _context;
        private readonly IMapper _mapper;

        public GetUsersQuery(BlingOnContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 18;

        public string Name => "Get users";

        public PagedResponse<UserDto> Execute(UserSearch search)
        {
            var query = _context.Users.Where(x => x.DeletedAt == null).AsQueryable();

            if(!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
            {
                search.Keyword = search.Keyword.ToLower();

                query = query.Where(x => x.FirstName.ToLower().Contains(search.Keyword) ||
                x.LastName.ToLower().Contains(search.Keyword) || x.Email.ToLower().Contains(search.Keyword));
            }

            var users = query.Paged<UserDto, User>(search, _mapper);

            if (users.Items.Count() == 0)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            return users;
        }
    }
}
