namespace SmileApp.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private SmileAppDbContext dbContext;

        public SmileAppDbContext Init()
        {
            return dbContext ?? (dbContext = new SmileAppDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}