using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Truck.Core;
using Truck.Data.Repositories;


namespace Truck.Services
{
    //implementation of the IDbService
    public class DbService<TEntity> : IDbService<TEntity> where TEntity:class,IEntity,new()
    {
        //Repository object
        public IRepository<TEntity> Repository { get; set; }

        public DbService(IRepository<TEntity> repository)
        {
            this.Repository = repository;
        }
        public IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
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

        public void Delete(params int [] ids)
        {
            foreach (var id in ids)
            {
                Repository.Delete(id);
            }
            
        }

        public void Delete(TEntity entity)
        {
            Repository.Delete(entity);
        }

        public void Update(TEntity entity)
        {
            Repository.Update(entity);
        }

        public IQueryable<TEntity> Query(string query)
        {
            return Repository.Query(query);
        }

        #region async implementation

        public Task<List<TEntity>> AllListAsync => Repository.AllListAsync;

        public  IQueryable<TEntity> AllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return  Repository.AllIncludingAsync(includeProperties);
        }

        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await Repository.FindAsync(keyValues);
        }

        public async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await Repository.FindAsync(cancellationToken, keyValues);
        }

        public async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }

        public async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }
            await Repository.DeleteAsync(cancellationToken, entity);
            return true;
        }
        #endregion async implementation
    }
}