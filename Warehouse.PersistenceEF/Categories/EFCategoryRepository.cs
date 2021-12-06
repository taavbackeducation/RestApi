using Warehouse.Services.Products.Contracts;
using Microsoft.EntityFrameworkCore;
using Warehouse.Entities;
using System.Linq;

namespace Warehouse.PersistenceEF.Categories
{
    public class EFCategoryRepository : CategoryRepository
    {
        private readonly DbSet<Category> _categories;

        public EFCategoryRepository(EFDataContext dbContext)
        {
            _categories = dbContext.Set<Category>();
        }

        public bool IsExist(int categoryId)
        {
            return _categories.Any(_ => _.Id == categoryId);
        }
    }
}
