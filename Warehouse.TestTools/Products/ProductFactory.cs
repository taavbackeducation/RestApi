using Warehouse.PersistenceEF;
using Warehouse.PersistenceEF.Categories;
using Warehouse.PersistenceEF.Products;
using Warehouse.Services.Products;
using Warehouse.Services.Products.Contracts;
using Warehouse.Services.Products.Contracts.Dtos;

namespace Warehouse.TestTools.Products
{
    public static class ProductFactory
    {
        public static ProductService GenerateService(EFDataContext dbContext)
        {
            var products = new EFProductRepository(dbContext);
            var categories = new EFCategoryRepository(dbContext);
            var unitOfWork = new EFUnitOfWork(dbContext);
            return new ProductAppService(products, categories, unitOfWork);
        }

        public static AddProductDto GenerateAddDto(int categoryId)
        {
            return new AddProductDto
            {
                Title = "dummy toy",
                Price = 10000,
                CategoryId = categoryId
            };
        }
    }
}
