using blasa.access.management.Core.Application.Interfaces;
using blasa.access.management.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace blasa.access.management.persistance.Contexts
{
    public class AuthentificationDbContext :    IdentityDbContext<User>, IAuthentificationDbContext

    {  
        public AuthentificationDbContext(DbContextOptions<AuthentificationDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
            builder.HasDefaultSchema("AccessManagement");
            base.OnModelCreating(builder);
            builder.Entity<Provider>().HasData(
               new Provider  {  Id = 1,  Label = "Facebook"},
               new Provider  { Id = 2, Label = "Gmail" }
           );
        }
       

        public DbSet<User> Users { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
