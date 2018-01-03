using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Store.Data.Repositories;
using Store.Core;

namespace Store.Services
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity:class,IEntity,new()
    {
        public IRepository<TEntity> Repository { get; set; }

        public EntityService(IRepository<TEntity> repository)
        {
            this.Repository = repository;
        }
        public IEnumerable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Repository.AllIncluding(includeProperties);
        }

        public IQueryable<TEntity> All => Repository.All;

        public TEntity Find(int id)
        {
            return Repository.Find(id);
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FindBy(predicate);
        }

        public void Insert(TEntity entity)
        {
            Repository.Insert(entity);
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
        }

        public void Delete(TEntity entity)
        {
            Repository.Delete(entity);
        }

        public void Update(TEntity entity)
        {
            Repository.Update(entity);
        }
        
    }
}