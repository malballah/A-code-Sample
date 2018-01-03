using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Store.Core;


namespace Store.Data.Repositories
{
    public interface IRepository<TEntity>  where TEntity : class, IEntity, new()
    {
        IEnumerable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> All { get; }
        TEntity Find(int id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entity);
        void DeleteAll(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity entity);
    }
}
