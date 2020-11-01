using blasa.access.management.Core.Application.Interfaces;
using blasa.access.management.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace blasa.access.management.Core.Application.Features.Utilisateurs.Queries.GetUtilisateurById
{
    public class GetUtilisateurByIdQuery : IRequest<Utilisateur>
    {
        public int Id { get; set; }
        public class GetUtilisateurByIdQueryHandler : IRequestHandler<GetUtilisateurByIdQuery, Utilisateur>
        {
            private readonly IApplicationDbContext _context;
            public GetUtilisateurByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public  async Task<Utilisateur> Handle(GetUtilisateurByIdQuery query, CancellationToken cancellationToken)
            {
                var Utilisateur = _context.Utilisateurs.Where(a => a.Id == query.Id).FirstOrDefault();
                if (Utilisateur == null) return null;
                return Utilisateur;
            }
        }
    }
}
