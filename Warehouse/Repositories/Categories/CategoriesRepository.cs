using Microsoft.EntityFrameworkCore;
using Warehouse.App.Models;
using System.Linq;

namespace Warehouse.Repositories.Categories
{
    internal class CategoriesRepository
    {
        private readonly DbSet<Category> _categories;

        public CategoriesRepository(EFDataContext dbContext)
        {
            _categories = dbContext.Set<Category>();
        }

        internal bool IsExist(int categoryId)
        {
            return _categories.Any(_ => _.Id == categoryId);
        }
    }
}
