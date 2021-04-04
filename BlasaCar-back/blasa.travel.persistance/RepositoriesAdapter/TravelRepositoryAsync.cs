using blasa.travel.Core.Application.Repositories;
using blasa.travel.persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blasa.travel.Core.Domain.Entities;
using blasa.travel.Core.Application.OutputPort;
using System;

namespace blasa.travel.persistance.Repositories 
{
   public  class TravelRepositoryAsync : ITravelRepositoryAsync
    {
        private readonly TravelDbContext _dbContext;

        public TravelRepositoryAsync(TravelDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IReadOnlyList<Travel>> GetTravelByUserIdAsync(string id)
        {

            return await _dbContext.Travels.Where(t => t.Userid == id).ToListAsync();
        }
        public async Task<Travel> UpdateAsync(Travel travel)
        {
          //  _dbContext.(entity).State = EntityState.Modified;
            _dbContext.Entry(travel).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return travel;
        }

        }

       
    }
}
