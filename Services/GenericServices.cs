using CodieGo_Adventure.Repository;
using System.Linq.Expressions;

namespace CodieGo_Adventure.Services
{
    public class GenericServices : IGenericServices
    {
        private readonly IGenericRepository _repo;

        public GenericServices(IGenericRepository repo)
        {
            _repo = repo;
        }

        /* METHODS OVERVIEW */

        // Generic Methods for CRUD Operations
        // T represents any entity class
        // Example: T can be User, Product, Order, etc.
        // These methods call the corresponding methods in the IGenericRepository
        // and provide a service layer for business logic if needed in the future.
        // This promotes code reusability and reduces redundancy.

        public async Task<IEnumerable<T>> GetAllDataAsync<T>(params Expression<Func<T, object>>[] includes) where T : class =>
            await _repo.GetAllAsync<T>(includes);

        public async Task<IEnumerable<T>> GetAllDataByIdAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class =>
            await _repo.GetAllByIdAsync<T>(predicate, includes);

        public async Task<T> GetDataByIdAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class =>
            await _repo.GetByIdAsync<T>(predicate, includes);

        public async Task<IEnumerable<T>> SearchDataAsync<T>(string search, string propertyName) where T : class =>
            await _repo.GetBySearchAsync<T>(search, propertyName);

        public async Task<T> GetDataByUsernameOrEmailAndPasswordAsync<T>(string usernameoremail, string password) where T : class =>
            await _repo.GetByUsernameOrEmailAndPasswordAsync<T>(usernameoremail, password);

        public async Task<T> GetDataBySessionIdAsync<T>(Guid sessionId) where T : class => 
            await _repo.GetBySessionIdAsync<T>(sessionId);

        public async Task<T> GetDataByTokenAsync<T>(string token) where T : class => 
            await _repo.GetByTokenAsync<T>(token);

        public async Task AddDataAsync<T>(T entity) where T : class =>
            await _repo.AddAsync<T>(entity);

        public async Task UpdateDataAsync<T>(T entity) where T : class =>
            await _repo.UpdateAsync<T>(entity);

        public async Task DeleteDataAsync<T>(T entity) where T : class =>
            await _repo.DeleteAsync<T>(entity);
    }
}
