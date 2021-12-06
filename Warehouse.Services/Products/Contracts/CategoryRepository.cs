namespace Warehouse.Services.Products.Contracts
{
    public interface CategoryRepository
    {
        bool IsExist(int categoryId);
    }
}
