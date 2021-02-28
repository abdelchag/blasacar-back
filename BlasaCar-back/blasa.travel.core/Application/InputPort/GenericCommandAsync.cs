using blasa.travel.Core.Application.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blasa.travel.Core.Application.Commands
{
    public class GenericCommandAsync<T> : IGenericCommandAsync<T> where T : class
    {
        IGenericRepositoryAsync<T> _GenericRepositoryAsync;
        public GenericCommandAsync(IGenericRepositoryAsync<T> GenericRepositoryAsync)
        {
            _GenericRepositoryAsync = GenericRepositoryAsync;
        }
        public async Task<T> AddAsync(T entity)
        {
            var newObj = await _GenericRepositoryAsync.AddAsync(entity);


            return newObj;
        }

        public Task DeleteAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}