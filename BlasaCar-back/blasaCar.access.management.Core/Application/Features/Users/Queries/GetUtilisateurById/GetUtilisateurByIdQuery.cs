using blasa.access.management.Core.Application.Interfaces;
using blasa.access.management.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace blasa.access.management.Core.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
        {
            private readonly IAuthentificationDbContext _context;
            public GetUserByIdQueryHandler(IAuthentificationDbContext context)
            {
                _context = context;
            }
            public  async Task<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
            {
                var User = _context.Users.Where(a => a.Id.Equals(  query.Id)).FirstOrDefault();
                if (User == null) return null;
                return User;
            }
        }
    }
}
