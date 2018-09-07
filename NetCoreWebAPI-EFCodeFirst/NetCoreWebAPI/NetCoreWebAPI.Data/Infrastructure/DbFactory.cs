

namespace NetCoreWebAPI.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private NetCoreWebAPIDbContext dbContext;
        public NetCoreWebAPIDbContext Init()
        {
            return dbContext ?? (dbContext = new NetCoreWebAPIDbContext());
            //return dbContext;
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
