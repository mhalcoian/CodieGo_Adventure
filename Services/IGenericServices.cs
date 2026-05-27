using System.Linq.Expressions;

namespace CodieGo_Adventure.Services
{
    public interface IGenericServices
    {
        // Generic CRUD operations Interface
        Task<IEnumerable<T>> GetAllDataAsync<T>(params Expression<Func<T, object>>[] includes) where T : class;
        Task<IEnumerable<T>> GetAllDataByIdAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;
        Task<T> GetDataByIdAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;
        Task<IEnumerable<T>> SearchDataAsync<T>(string search, string propertyName) where T : class;
        Task<T> GetDataByUsernameOrEmailAndPasswordAsync<T>(string usernameoremail, string password) where T : class;
        Task<T> GetDataBySessionIdAsync<T>(Guid sessionId) where T : class;
        Task<T> GetDataByTokenAsync<T>(string token) where T : class;
        Task AddDataAsync<T>(T entity) where T : class;
        Task UpdateDataAsync<T>(T entity) where T : class;
        Task DeleteDataAsync<T>(T entity) where T : class;
    }
}
