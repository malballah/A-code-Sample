using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Truck.Data.Repositories;

namespace Truck.Data.Infrastructure
{
    //implementation of unit of work patter
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IDbFactory _dbFactory; private TruckDbContext _dbContext;
        public UnitOfWork(IDbFactory dbFactory) { this._dbFactory = dbFactory; }
        public TruckDbContext DbContext => _dbContext ?? (_dbContext = _dbFactory.Init());

        #region Private Fields

        private bool _disposed;
        private DbContextTransaction _transaction;

        #endregion Private Fields

        #region Constuctor/Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Constuctor/Dispose

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

       
        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }


        #region Unit of Work Transactions

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _transaction = DbContext.Database.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        #endregion
    }
}
