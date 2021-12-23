using Warehouse.Services.SharedContracts;
using System.Threading.Tasks;

namespace Warehouse.PersistenceEF
{
    public class EFUnitOfWork : UnitOfWork
    {
        private readonly EFDataContext _dbContext;

        public EFUnitOfWork(EFDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Complete()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
