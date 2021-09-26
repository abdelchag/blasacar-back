using blasa.travel.Core.Application.OutputPort;
using blasa.travel.Core.Application.Repositories;
using blasa.travel.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace blasa.travel.Core.Application.Commands
{
    public class BookCommandAsync : IBookCommandAsync
    {
        IBookRepositoryAsync _BookRepositoryAsync;

        public BookCommandAsync(IBookRepositoryAsync BookRepositoryAsync)
        {
            _BookRepositoryAsync = BookRepositoryAsync;
        }
        public async Task<IReadOnlyList<Book>> GetBookByUserIdAsync(string id)
        {
            return await _BookRepositoryAsync.GetBookByUserIdAsync(id);

        }

        public async Task<IReadOnlyList<Book>> GetBookByFiltre(Book entity)
        {
            return await _BookRepositoryAsync.GetBookByFiltre(entity);

        }



    }
}
