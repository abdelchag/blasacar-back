﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using blasa.travel.Core.Domain.Entities;
using blasa.travel.Core.Application.Repositories;

namespace blasa.travel.persistance.Contexts
{
    public class TravelDbContext : DbContext //, IApplicationDbContext

    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Tarvel");
            base.OnModelCreating(builder);

        }



        public DbSet<Travel> Travels { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
