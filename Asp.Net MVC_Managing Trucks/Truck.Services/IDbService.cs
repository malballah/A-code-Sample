using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Truck.Core;
using Truck.Data.Repositories;

namespace Truck.Services
{
    public interface IDbService<TEntity>:IService where TEntity:class,IEntity,new()
    {
        IRepository<TEntity> Repository { get; set; }
        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> All { get; }
        TEntity Find(int id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void Delete(params int[] id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        IQueryable<TEntity> Query(string query);
    }
}
