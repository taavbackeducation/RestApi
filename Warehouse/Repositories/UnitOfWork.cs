namespace Warehouse.Repositories
{
    internal class UnitOfWork
    {
        private readonly EFDataContext _dbContext;

        public UnitOfWork(EFDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal void Complete()
        {
            _dbContext.SaveChanges();
        }
    }
}
