using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using blasa.access.management.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace blasa.access.management.Core.Application.Interfaces
{
    public interface IAuthentificationDbContext
    {
        DbSet<User> Users { get; set; }
        
        Task<int> SaveChangesAsync();
    }
}
