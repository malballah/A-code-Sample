using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Truck.Core;


namespace Truck.Data.Repositories
{
    public interface IRepository<TEntity>  where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> All { get; }
        TEntity Find(int id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entity);
        void DeleteAll(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity entity);
        Task<List<TEntity>> AllListAsync { get; }
        IQueryable<TEntity> AllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<TEntity> Query(string query);
    }
}
