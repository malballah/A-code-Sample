using Truck.Data;


namespace Truck.Data.Infrastructure
{
    //implemented factory pattern to instansiate new object from database context
    public class DbFactory: Disposable, IDbFactory
    {
        private TruckDbContext _dbContext;
        public TruckDbContext Init()
        {
            return _dbContext ?? (_dbContext = new TruckDbContext());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
