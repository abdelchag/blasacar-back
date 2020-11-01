using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using blasa.access.management.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace blasa.access.management.Core.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Utilisateur> Utilisateurs { get; set; }
        
        Task<int> SaveChangesAsync();
    }
}
