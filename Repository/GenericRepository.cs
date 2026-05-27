using CodieGo_Adventure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodieGo_Adventure.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly CGADbContext _context;

        public GenericRepository(CGADbContext context)
        {
            _context = context;
        }

        /* METHODS OVERVIEW */

        // Generic Methods for CRUD Operations
        // T represents any entity class
        // Using EF Core's DbSet<T> to perform operations
        // Using async methods for better performance
        // Using EF.Property to access properties dynamically
        // Assumes that the entity classes are properly configured in the DbContext
        // and that the database is set up correctly.

        public async Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByIdAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class 
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetBySearchAsync<T>(string search, string propertyName) where T : class => 
            await _context.Set<T>().Where(x => 
            EF.Property<string>(x, propertyName).StartsWith(search) || 
            EF.Property<string>(x, propertyName).Contains(search) || 
            EF.Property<string>(x, propertyName).EndsWith(search))
            .ToListAsync();

        public async Task<T> GetByUsernameOrEmailAndPasswordAsync<T>(string usernameoremail, string password) where T : class =>
            await _context.Set<T>().FirstOrDefaultAsync(x => 
            (EF.Property<string>(x, "Username") == usernameoremail || EF.Property<string>(x, "Email") == usernameoremail) &&
            EF.Property<string>(x, "Password") == password);

        public async Task<T> GetBySessionIdAsync<T>(Guid sessionId) where T : class =>
            await _context.Set<T>().FirstOrDefaultAsync(x =>
            EF.Property<Guid>(x, "SessionId") == sessionId && 
            EF.Property<string>(x, "Status") == "Online");

        public async Task<T> GetByTokenAsync<T>(string token) where T : class =>
            await _context.Set<T>().FirstOrDefaultAsync(x =>
            EF.Property<string>(x, "Token") == token);

        public async Task AddAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
