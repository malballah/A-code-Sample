using Store.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Store.Data.Repositories;

namespace Store.Services
{
    public interface IEntityService<TEntity>:IService where TEntity:class,IEntity,new()
    {
        IRepository<TEntity> Repository { get; set; }
        IEnumerable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> All { get; }
        TEntity Find(int id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
     
    }
}
