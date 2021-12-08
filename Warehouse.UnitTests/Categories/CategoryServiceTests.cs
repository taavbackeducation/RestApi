using Warehouse.Services.Categories.Contracts;
using Warehouse.TestTools.Categories;
using Warehouse.UnitTests.Tools;
using Warehouse.PersistenceEF;
using Warehouse.Entities;
using FluentAssertions;
using System.Linq;
using Xunit;
using System;
using Warehouse.Services;
using Warehouse.Services.Categories.Exceptions;

namespace Warehouse.UnitTests.Categories
{
    public class CategoryServiceTests
    {
        private readonly EFDataContext _dbContext;
        private readonly CategoryService _sut;

        public CategoryServiceTests()
        {
            _dbContext = new EFInMemoryDatabase().CreateDataContext<EFDataContext>();
            _sut = CategoryFactory.GenerateService(_dbContext);
        }

        [Fact]
        public void Add_add_category_properly()
        {
            var dto = CategoryFactory.GenerateAddDto();

            _sut.Add(dto);

            var expected = _dbContext.Set<Category>().First();
            expected.Title.Should().Be(dto.Title);
        }

        [Fact]
        public void Add_not_add_when_title_is_duplicated()
        {
            var existCategory = CategoryFactory.GenerateCategory();
            _dbContext.Manipulate(_ => _.Add(existCategory));
            var dto = CategoryFactory.GenerateAddDto(existCategory.Title);

            Action actual = () => _sut.Add(dto);

            actual.Should().ThrowExactly<CategoryTitleIsDuplicatedException>();
        }
    }
}
