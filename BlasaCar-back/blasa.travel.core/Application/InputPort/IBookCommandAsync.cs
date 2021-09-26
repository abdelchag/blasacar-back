using blasa.travel.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blasa.travel.Core.Application.Commands
{
    public interface IBookCommandAsync
    {       
        Task<IReadOnlyList<Book>> GetBookByUserIdAsync(string id);
        Task<IReadOnlyList<Book>> GetBookByFiltre(Book entity);
    }

}