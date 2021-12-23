using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Entities;
using Warehouse.PersistenceEF;
using Warehouse.Services.Products.Contracts;
using Warehouse.Services.Products.Exceptions;
using Warehouse.TestTools.Categories;
using Warehouse.TestTools.Products;
using Warehouse.UnitTests.Infrastructure;
using Xunit;

namespace Warehouse.UnitTests
{
    public class ProductServiceTests
    {
        private readonly ProductService _sut;
        private readonly EFDataContext _dbContext;

        public ProductServiceTests()
        {
            _dbContext = new EFInMemoryDatabase().CreateDataContext<EFDataContext>();
            _sut = ProductFactory.GenerateService(_dbContext);
        }

        [Fact]
        public async Task Add_add_product_correctly()
        {
            var category = CategoryFactory.GenerateCategory();
            _dbContext.Manipulate(_ => _.Add(category));
            var dto = ProductFactory.GenerateAddDto(category.Id);

            await _sut.Add(dto);

            var expectedProduct = _dbContext.Set<Product>().First();
            expectedProduct.Title.Should().Be(dto.Title);
            expectedProduct.Price.Should().Be(dto.Price);
            expectedProduct.CategoryId.Should().Be(dto.CategoryId);
        }

        [Fact]
        public async Task Add_not_add_when_category_not_found()
        {
            var invalidCategoryId = 0;
            var dto = ProductFactory.GenerateAddDto(categoryId: invalidCategoryId);

            Func<Task> actual = async () => await _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<CategoryNotFoundException>();
        }


    }
}
