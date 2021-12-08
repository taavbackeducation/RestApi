using FluentAssertions;
using System;
using System.Linq;
using Warehouse.Entities;
using Warehouse.PersistenceEF;
using Warehouse.Services.Categories.Exceptions;
using Warehouse.Services.Products.Contracts;
using Warehouse.Services.Products.Exceptions;
using Warehouse.TestTools.Categories;
using Warehouse.TestTools.Products;
using Warehouse.UnitTests.Tools;
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
        public void Add_add_product_correctly()
        {
            var category = CategoryFactory.GenerateCategory();
            _dbContext.Manipulate(_ => _.Add(category));
            var dto = ProductFactory.GenerateAddDto(category.Id);

            _sut.Add(dto);

            var expectedProduct = _dbContext.Set<Product>().First();
            expectedProduct.Title.Should().Be(dto.Title);
            expectedProduct.Price.Should().Be(dto.Price);
            expectedProduct.CategoryId.Should().Be(dto.CategoryId);
        }

        [Fact]
        public void Add_not_add_when_category_not_found()
        {
            var invalidCategoryId = 0;
            var dto = ProductFactory.GenerateAddDto(categoryId: invalidCategoryId);

            Action actual = () => _sut.Add(dto);

            actual.Should().ThrowExactly<CategoryNotFoundException>();
        }


    }
}
