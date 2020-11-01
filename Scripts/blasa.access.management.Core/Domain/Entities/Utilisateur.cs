using blasa.access.management.Core.Domain.Common;
using System;

namespace blasa.access.management.Core.Domain.Entities
{
    public class Utilisateur : AuditableBaseEntity
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string Sexe { get; set; }
        public string MotDePasse { get; set; }
    }
}
