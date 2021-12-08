using Warehouse.Entities;
using Warehouse.Services.Categories.Contracts.Dtos;
using Warehouse.Services.Categories.Contracts;
using Warehouse.PersistenceEF.Categories;
using Warehouse.Services.Categories;
using Warehouse.PersistenceEF;
using System;

namespace Warehouse.TestTools.Categories
{
    public static class CategoryFactory
    {
		public static CategoryService GenerateService(EFDataContext dbContext)
        {
            var categories = new EFCategoryRepository(dbContext);
            var unitOfWork = new EFUnitOfWork(dbContext);

            return new CategoryAppService(categories, unitOfWork);
        }

        public static AddCategoryDto GenerateAddDto(string title = "dummy title")
        {
            return new AddCategoryDto { Title = title };
        }
		
        public static Category GenerateCategory(string title = "toy")
        {
            return new Category { Title = title };
        }
    }
}
