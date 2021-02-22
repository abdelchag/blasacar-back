using blasa.travel.Core.Application.Repositories;
using blasa.travel.persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blasa.travel.Core.Domain.Entities;

namespace blasa.travel.persistance.Repositories
{
    public class TravelRepositoryAsync : IGenericRepositoryAsync<Travel>  
    {
        private readonly ApplicationDbContext _dbContext;

        public TravelRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Travel> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Travel>().FindAsync(id);
        }

        public async Task<IReadOnlyList<Travel>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<Travel>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Travel> AddAsync(Travel entity)
        {
            await _dbContext.Set<Travel>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Travel entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Travel entity)
        {
            _dbContext.Set<Travel>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Travel>> GetAllAsync()
        {
            return await _dbContext
                 .Set<Travel>()
                 .ToListAsync();
        }
    }
}
