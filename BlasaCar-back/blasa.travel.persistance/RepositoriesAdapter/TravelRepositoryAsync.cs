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
        private readonly TravelDbContext _dbContext;

        public TravelRepositoryAsync(TravelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Travel> GetByIdAsync(int id)
        {
            return await _dbContext.Travels.FindAsync(id);
        }

        public async Task<IReadOnlyList<Travel>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Travels
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Travel> AddAsync(Travel entity)
        {
            await _dbContext.Travels.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Travel travel)
        {
          //  _dbContext.(entity).State = EntityState.Modified;
            _dbContext.Entry(travel).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Travel entity)
        {
            _dbContext.Travels.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Travel>> GetAllAsync()
        {
            return await _dbContext
                 .Travels
                 .ToListAsync();
        }
    }
}
