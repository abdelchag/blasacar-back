using blasa.access.management.Core.Application.Interfaces;
using blasa.access.management.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace blasa.access.management.Core.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string Sexe { get; set; }
        public string MotDePasse { get; set; }
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
        {
            private readonly IAuthentificationDbContext _context;
            public CreateUserCommandHandler(IAuthentificationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(CreateUserCommand command, CancellationToken cancellationToken)
            {
                var User = new User();
                //User.Nom = command.Nom;
                //User.Prenom = command.Prenom;
                //User.Telephone = command.Telephone;
                //User.Adresse = command.Adresse;
                //User.Email = command.Email;
                //User.DateDeNaissance = command.DateDeNaissance;
                //User.Sexe = command.Sexe;
                //User.MotDePasse = command.MotDePasse;


                _context.Users.Add(User);
                await _context.SaveChangesAsync();
                return User.Id;
            }
        }
    }
}
