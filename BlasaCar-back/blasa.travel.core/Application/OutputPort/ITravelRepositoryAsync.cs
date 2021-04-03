using blasa.travel.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace blasa.travel.Core.Application.OutputPort
{
   public interface IUserRepositoryAsync
    {
        Task<IReadOnlyList<Travel>> GetTravelByUserIdAsync(string id);
       
    }
}
