using blasa.travel.Core.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace blasa.travel.Core.Application.Commands
{
    public interface ITravelCommandAsync
    {
        Task<Travel> GetTravelByUserEmailAsync(string UserEmail);
        Task<Travel> GetTravelByUserIdAsync(Guid id);
    }
}