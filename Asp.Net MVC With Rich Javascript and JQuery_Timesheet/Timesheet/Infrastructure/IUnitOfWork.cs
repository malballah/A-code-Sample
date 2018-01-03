using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Timesheet.Infrastructure
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose(bool disposing);
        void BeginTransaction(IsolationLevel isolationLevel);
        void Commit();
        void Rollback();
    }
}
