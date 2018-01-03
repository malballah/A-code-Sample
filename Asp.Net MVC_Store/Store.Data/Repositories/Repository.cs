using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Store.Core;
using Store.Data.Infrastructure;

namespace Store.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private StoreDbContext _dataContext;

        #region Properties

        protected IDbFactory DbFactory { get; private set; }

        protected StoreDbContext DbContext => _dataContext ?? (_dataContext = DbFactory.Init());

        public Repository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        #endregion

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> All => GetAll();

        public virtual IEnumerable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public TEntity Find(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate);
        }

        public virtual void Insert(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(int id)
        {
            Delete(Find(id));
        }
        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteAll(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var item in FindBy(predicate))
            {
                Delete(item);
            }
        }

        public virtual void Update(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        #region async implementation

        public Task<List<TEntity>> AllListAsync
        {
            get { return DbContext.Set<TEntity>().ToListAsync<TEntity>(); }
        }

        public IQueryable<TEntity> AllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await DbContext.Set<TEntity>().FindAsync(keyValues);
        }

        public async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await DbContext.Set<TEntity>().FindAsync(cancellationToken, keyValues);
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
            DbContext.Set<TEntity>().Remove(entity);
            return true;
        }
        #endregion async implementation
    }

}