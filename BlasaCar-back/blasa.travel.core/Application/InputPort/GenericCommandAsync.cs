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
            return await _GenericRepositoryAsync.AddAsync(entity);
 
        }

        public async Task<T> DeleteAsync(int id )
        {
              return await _GenericRepositoryAsync.DeleteAsync( id);
        }

        public Task DeleteAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
           return await _GenericRepositoryAsync.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _GenericRepositoryAsync.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _GenericRepositoryAsync.GetPagedReponseAsync( pageNumber,  pageSize);
        }

        public async Task<T>  UpdateAsync(T entity)
        {
             return  await _GenericRepositoryAsync.UpdateAsync(entity);
        }
    }
}