using Microsoft.EntityFrameworkCore;
using webAPI3.App.Models;
using webAPI3.Repositories;

namespace webAPI3.Repositories.Categories
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
