using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Timesheet.Infrastructure
{
    /// <summary>
    /// Database service instance
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DbService<TEntity> : IDbService<TEntity> 
        where TEntity : class, IDbEntity, new()
    {
        /// <summary>
        /// Repository instance of the data model
        /// </summary>
        public IRepository<TEntity> Repository { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public DbService(IRepository<TEntity> repository)
        {
            Repository = repository;
        }
        /// <summary>
        /// Get all data objects
        /// </summary>
        public IQueryable<TEntity> All => Repository.All;
        /// <summary>
        /// Get all data objects without tracking
        /// </summary>
        public IQueryable<TEntity> AllNoTrack => Repository.AllNoTrack;
        /// <summary>
        /// Find data object with it's keys
        /// </summary>
        /// <param name="keys">The primary key values of the data object</param>
        /// <returns>The found data object </returns>
        public TEntity Find(params object[] keys)
        {
            return Repository.Find(keys);
        }
        /// <summary>
        /// Find data objects by search condition
        /// </summary>
        /// <param name="predicate">A predicate to filter objects with</param>
        /// /// <param name="noTrack">Whether to track or not track changes on found data objects</param>
        /// <returns>Found data objects</returns>
        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, bool noTrack = true)
        {
            return Repository.FindBy(predicate,noTrack);
        }
        /// <summary>
        /// Insert new data objects into the data context
        /// </summary>
        /// <param name="entities">New data objects to insert into the data context </param>
        public virtual void Insert(params TEntity[] entities)
        {
            Repository.Insert(entities);
        }
        
        /// <summary>
        /// Delete data objects from the data context
        /// </summary>
        /// <param name="entities">Data objects to delete from the data context</param>
        public virtual void Delete(params TEntity[] entities)
        {
            Repository.Delete(entities);
        }
        /// <summary>
        /// Update data objects
        /// </summary>
        /// <param name="entities">Data objects to be updated</param>
        public virtual void Update(params TEntity[] entities)
        {
            Repository.Update(entities);
        }

       
    }
}