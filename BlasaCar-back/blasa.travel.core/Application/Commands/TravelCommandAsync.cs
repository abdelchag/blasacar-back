using blasa.travel.Core.Application.Repositories;
using blasa.travel.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace blasa.travel.Core.Application.Commands
{
    public class TravelCommandAsync : IGenericCommandAsync<Travel>
    {
        IGenericRepositoryAsync<Travel> TravelRepositoryAsync;
        public TravelCommandAsync( IGenericRepositoryAsync<Travel> TravelRepositoryAsync)
        {
            this.TravelRepositoryAsync = TravelRepositoryAsync;
        }
        public TravelCommandAsync() { }
        public async Task<Travel> AddAsync(Travel obj)
        {
            Travel newTravel = await TravelRepositoryAsync.AddAsync(obj);
             

            return newTravel;
        }

        public Task DeleteAsync(Travel entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Travel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Travel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Travel>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Travel entity)
        {
            throw new NotImplementedException();
        }
    }
}
