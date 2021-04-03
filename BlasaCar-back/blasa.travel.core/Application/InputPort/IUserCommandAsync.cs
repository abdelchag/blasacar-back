using blasa.travel.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blasa.travel.Core.Application.Commands
{
    public interface IUserCommandAsync
    {       
        Task<IReadOnlyList<Travel>> GetTravelByUserIdAsync(string id);
    }

}