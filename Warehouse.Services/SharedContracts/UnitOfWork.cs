using System.Threading.Tasks;

namespace Warehouse.Services.SharedContracts
{
    public interface UnitOfWork
    {
        Task Complete();
    }
}
