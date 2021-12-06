using Warehouse.Entities;
using Warehouse.Services.SharedContracts;

namespace Warehouse.PersistenceEF
{
    public class EFUnitOfWork : UnitOfWork
    {
        private readonly EFDataContext _dbContext;

        public EFUnitOfWork(EFDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Complete()
        {

            var x = _dbContext.Set<Product>();
            _dbContext.SaveChanges();
        }
    }
}
