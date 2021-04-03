using blasa.travel.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace blasa.travel.Core.Application.OutputPort
{
   public interface ITravelRepositoryAsync
    {
        Task<Travel> GetTravelByUserIdAsync(Guid id);
        Task<Travel> GetTravelByUserEmailAsync(String UserEmail );
    }
}
