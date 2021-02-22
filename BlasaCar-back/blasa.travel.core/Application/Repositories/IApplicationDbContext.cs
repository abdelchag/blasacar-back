 




    using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using blasa.travel.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace blasa.travel.Core.Application.Repositories
{
    public interface IApplicationDbContext
    {
        DbSet<Travel> Travels { get; set; }

        Task<int> SaveChangesAsync();
    }
}
