using blasa.access.management.Core.Application.Interfaces;
using blasa.access.management.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace blasa.access.management.persistance.Contexts
{
    public class ApplicationDbContext :    IdentityDbContext<User>, IApplicationDbContext
     
    {  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
   
        public DbSet<User> Users { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
