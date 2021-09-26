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
    public class BookRepositoryAsync : IBookRepositoryAsync
    {
        private readonly TravelDbContext _dbContext;

        public BookRepositoryAsync(TravelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Book>> GetBookByUserIdAsync(string id)
        {

            return await _dbContext.Books.Where(t => t.Userid == id).OrderByDescending(t => t.Created).ToListAsync();

        }
        public async Task<IReadOnlyList<Book>> GetBookByFiltre(Book filreTravel)
        {

            var list = _dbContext.Books.Where(t =>
          (t.Userid == filreTravel.Userid || filreTravel.Userid == null) && 
          (t.IsAccepted == filreTravel.IsAccepted || filreTravel.IsAccepted == null) &&
         (t.Travel.Id == filreTravel.Travel.Id || filreTravel.Travel == null)  
         );


            return await list.OrderByDescending(t => t.Created).ToListAsync();
        }


    }
}
