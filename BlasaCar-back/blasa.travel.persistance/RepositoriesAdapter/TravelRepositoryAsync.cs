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

            return await _dbContext.Travels.Where(t => t.Userid == id).OrderByDescending(t => t.DepartureDate).ToListAsync();

        }
        public async Task<IReadOnlyList<Travel>> GetTravelByFiltre(Travel filreTravel)
        {
           
            var list = _dbContext.Travels.Where(t =>
          t.DepartureCity == filreTravel.DepartureCity &&
          t.ArrivalCity == filreTravel.ArrivalCity &&          
         ( t.DepartureDate == filreTravel.DepartureDate || filreTravel.DepartureDate == null)&&
         (t.DepartureTime == filreTravel.DepartureTime || filreTravel.DepartureTime == null)&&
         (t.IsAutomaticAcceptance == filreTravel.IsAutomaticAcceptance || filreTravel.IsAutomaticAcceptance == null) &&
         ( t.NumberPlaces == filreTravel.NumberPlaces || filreTravel.NumberPlaces == null )&&        
         ( t.Price <= filreTravel.Price || filreTravel.Price == null));



            return await list.OrderByDescending(t => t.DepartureDate).ThenBy(t => t.DepartureTime).ToListAsync();
        }


    }
}
