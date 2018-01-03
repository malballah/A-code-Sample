using Store.Data;


namespace Store.Data.Infrastructure
{
    public class DbFactory: Disposable, IDbFactory
    {
        private StoreDbContext _dbContext;
        public StoreDbContext Init()
        {
            return _dbContext ?? (_dbContext = new StoreDbContext());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
