using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Timesheet.Models;

namespace Timesheet.Infrastructure
{
    /// <summary>
    /// Repository can be used to make CRUD actions to a database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity >
        where TEntity : class, IDbEntity, new()
    {
        /// <summary>
        /// Database context
        /// </summary>
        private readonly DbContext _dbContext;
        /// <summary>
        /// Data Set of context model
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public Repository(TimesheetDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            
        }
        /// <summary>
        /// Get all data objects
        /// </summary>
        public IQueryable<TEntity> All => _dbSet;
        /// <summary>
        /// Get all data objects without tracking
        /// </summary>
        public IQueryable<TEntity> AllNoTrack => _dbSet.AsNoTracking();
        /// <summary>
        /// Find data object with it's keys
        /// </summary>
        /// <param name="keys">The primary key values of the data object</param>
        /// <returns>The found data object </returns>
        public TEntity Find(params object[] keys)
        {
            return _dbSet.Find(keys);
        }
        /// <summary>
        /// Find data objects by search condition
        /// </summary>
        /// <param name="predicate">A predicate to filter objects with</param>
        /// /// <param name="noTrack">Whether to track or not track changes on found data objects</param>
        /// <returns>Found data objects</returns>
        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate,bool noTrack=true)
        {
            return noTrack ? AllNoTrack.Where(predicate) : _dbSet.Where(predicate);
        }
        /// <summary>
        /// Insert new data objects into the data context
        /// </summary>
        /// <param name="entities">New data objects to insert into the data context </param>
        public virtual void Insert(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }
        
        /// <summary>
        /// Delete data objects from the data context
        /// </summary>
        /// <param name="entities">Data objects to delete from the data context</param>
        public virtual void Delete(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }
        /// <summary>
        /// Update data objects
        /// </summary>
        /// <param name="entities">Data objects to be updated</param>
        public virtual void Update(params TEntity[] entities)
        {
            foreach (var dbEntityEntry in entities.Select(item => _dbContext.Entry<TEntity>(item)))
            {
                dbEntityEntry.State = EntityState.Modified;
            }
        }
        
    }

}