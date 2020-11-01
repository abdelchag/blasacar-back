using blasa.access.management.Core.Application.Interfaces;
using blasa.access.management.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace blasa.access.management.Core.Application.Features.Utilisateurs.Commands.CreateUtilisateur
{
    public class CreateUtilisateurCommand : IRequest<int>
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string Sexe { get; set; }
        public string MotDePasse { get; set; }
        public class CreateUtilisateurCommandHandler : IRequestHandler<CreateUtilisateurCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateUtilisateurCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateUtilisateurCommand command, CancellationToken cancellationToken)
            {
                var Utilisateur = new Utilisateur();
                Utilisateur.Nom = command.Nom;
                Utilisateur.Prenom = command.Prenom;
                Utilisateur.Telephone = command.Telephone;
                Utilisateur.Adresse = command.Adresse;
                Utilisateur.Email = command.Email;
                Utilisateur.DateDeNaissance = command.DateDeNaissance;
                Utilisateur.Sexe = command.Sexe;
                Utilisateur.MotDePasse = command.MotDePasse;


                _context.Utilisateurs.Add(Utilisateur);
                await _context.SaveChangesAsync();
                return Utilisateur.Id;
            }
        }
    }
}
