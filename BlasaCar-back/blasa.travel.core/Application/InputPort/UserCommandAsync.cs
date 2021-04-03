using blasa.travel.Core.Application.OutputPort;
using blasa.travel.Core.Application.Repositories;
using blasa.travel.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace blasa.travel.Core.Application.Commands
{
    public class UserCommandAsync : IUserCommandAsync
    {
        IUserRepositoryAsync _TravelRepositoryAsync;

        public UserCommandAsync(IUserRepositoryAsync TravelRepositoryAsync)
        {
            _TravelRepositoryAsync = TravelRepositoryAsync;
        }

        public async Task<IReadOnlyList<Travel>> GetTravelByUserIdAsync(string id)
        {
            return await _TravelRepositoryAsync.GetTravelByUserIdAsync(id);

        }

       

    }
}
