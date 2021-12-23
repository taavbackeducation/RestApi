using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Entities;
using Warehouse.PersistenceEF;
using Warehouse.Services.RequestNeeds.Contracts;
using Warehouse.Services.RequestNeeds.Exceptions;
using Warehouse.TestTools.Categories;
using Warehouse.TestTools.Products;
using Warehouse.TestTools.RequestNeeds;
using Warehouse.UnitTests.Infrastructure;
using Xunit;

namespace Warehouse.UnitTests.RequestNeeds
{
    public class RequestNeedServiceTests
    {
        private readonly EFDataContext _dbContext;
        private readonly RequestNeedService _sut;

        public RequestNeedServiceTests()
        {
            _dbContext = new EFInMemoryDatabase().CreateDataContext<EFDataContext>();
            _sut = RequestNeedFactory.GenerateService(_dbContext);
        }

        [Fact]
        public async Task Register_register_request_need_properly()
        {
            var product = ProductFactory.GenerateProduct();
            var category = new CategoryBuilder().WithProduct(product).Build();
            _dbContext.Manipulate(_ => _.Add(category));
            var dto = RequestNeedFactory.GenerateRegisterDto(product.Id);

            await _sut.Register(dto);

            var actualRequestNeed = _dbContext.Set<RequestNeed>().First();
            actualRequestNeed.ProductId.Should().Be(product.Id);
            actualRequestNeed.Count.Should().Be(dto.Count);
            actualRequestNeed.Section.Should().Be(dto.Section);
        }

        [Fact]
        public async Task Register_not_register_when_product_is_invalid()
        {
            var invalidProductId = 0;
            var dto = RequestNeedFactory.GenerateRegisterDto(productId: invalidProductId);

            Func<Task> actual = async () => await _sut.Register(dto);

            await actual.Should().ThrowExactlyAsync<ProductNotFoundException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Register_not_register_when_request_need_count_is_less_than_one(int invalidCount)
        {
            var product = ProductFactory.GenerateProduct();
            var category = new CategoryBuilder().WithProduct(product).Build();
            _dbContext.Manipulate(_ => _.Add(category));
            var dto = RequestNeedFactory.GenerateRegisterDto(product.Id, count: invalidCount);

            Func<Task> actual = async () => await _sut.Register(dto);

            await actual.Should().ThrowExactlyAsync<RequestNeedCountIsLessThanOneException>();
        }
    }
}
