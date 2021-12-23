using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Categories.Contracts
{
    public interface CategoryRepository
    {
        void Add(Category category);
        bool IsExist(string title);
        Task<bool> IsExist(int categoryId);
    }
}
