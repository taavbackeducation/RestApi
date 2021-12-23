using Microsoft.EntityFrameworkCore;
using Warehouse.Entities;
using System.Linq;
using Warehouse.Services.Categories.Contracts;
using System.Threading.Tasks;

namespace Warehouse.PersistenceEF.Categories
{
    public class EFCategoryRepository : CategoryRepository
    {
        private readonly DbSet<Category> _categories;

        public EFCategoryRepository(EFDataContext dbContext)
        {
            _categories = dbContext.Set<Category>();
        }

        public void Add(Category category)
        {
            _categories.Add(category);
        }

        public async Task<bool> IsExist(int categoryId)
        {
            return await _categories.AnyAsync(_ => _.Id == categoryId);
        }
		
		public bool IsExist(string title)
        {
            return _categories.Any(_ => _.Title == title);
        }
    }
}
