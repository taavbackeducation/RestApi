namespace Warehouse.Services.SharedContracts
{
    public interface UnitOfWork
    {
        void Complete();
    }
}
