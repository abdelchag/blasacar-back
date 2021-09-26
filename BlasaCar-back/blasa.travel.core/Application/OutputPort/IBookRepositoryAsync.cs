using blasa.travel.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace blasa.travel.Core.Application.OutputPort
{
   public interface IBookRepositoryAsync
    {
        Task<IReadOnlyList<Book>> GetBookByUserIdAsync(string id);
        Task<IReadOnlyList<Book>> GetBookByFiltre(Book entity);

    }
}
