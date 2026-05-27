using System.Linq.Expressions;

namespace CodieGo_Adventure.Repository
{
    public interface IGenericRepository
    {
        // Generic CRUD operations Interface
        Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class;
        Task<IEnumerable<T>> GetAllByIdAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;
        Task<T> GetByIdAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;
        Task<IEnumerable<T>> GetBySearchAsync<T>(string search, string propertyName) where T : class;
        Task<T> GetByUsernameOrEmailAndPasswordAsync<T>(string usernameoremail, string password) where T : class;
        Task<T> GetBySessionIdAsync<T>(Guid sessionId) where T : class;
        Task<T> GetByTokenAsync<T>(string token) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
    }
}
