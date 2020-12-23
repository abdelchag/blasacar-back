using Microsoft.AspNetCore.Identity;
using System;

namespace blasa.access.management.Core.Domain.Entities
{
    public class User : IdentityUser  // AuditableBaseEntity
    {
        
        public string Address { get; set; }        
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }       
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string Telephone { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Provider Provider { get; set; }
    }
}
