using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Adocka.Mobile.Interfaces;
using SQLite.Net.Async;

namespace Adocka.Mobile.Repo
{
    public interface ILocalRepository<T> where T : class
    {
        AsyncTableQuery<T> AsQueryable();
        Task<List<T>> Get();
        Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        Task<T> Get(int id);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
    public class LocalRepository<T> : ILocalRepository<T> where T : class
    {
        private readonly SQLiteAsyncConnection _connection;
        private readonly ISQLite _sql;
        
        public LocalRepository(ISQLite sql)
        {
            _sql = sql;
            _connection = _sql.GetConnection();
        }
        public AsyncTableQuery<T> AsQueryable() => _connection.Table<T>();
        public async Task<List<T>> Get() => await _connection.Table<T>().ToListAsync();
        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _connection.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }
        public async Task<T> Get(int id) => await _connection.FindAsync<T>(id);
        public async Task<T> Get(Expression<Func<T, bool>> predicate) => await _connection.FindAsync<T>(predicate);
        public async Task<int> Insert(T entity) => await _connection.InsertAsync(entity);
        public async Task<int> Update(T entity) => await _connection.UpdateAsync(entity);
        public async Task<int> Delete(T entity) => await _connection.DeleteAsync(entity);
    }
}
