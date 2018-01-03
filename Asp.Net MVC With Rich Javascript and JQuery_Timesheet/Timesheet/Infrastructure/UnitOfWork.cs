using System;
using System.Data;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Models;

namespace Timesheet.Infrastructure
{
    public class UnitOfWork:IUnitOfWork
    {

        public UnitOfWork(TimesheetDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TimesheetDbContext DbContext;

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

        public Task<int> SaveChangesAsync()
        {
            return this.DbContext.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                    DbContext = null;
                }
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }
        
        #endregion Constuctor/Dispose

        public int SaveChanges()
        {
            return this.DbContext.SaveChanges();
        }

       

        #region Unit of Work Transactions

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _transaction = this.DbContext.Database.BeginTransaction(isolationLevel);
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

