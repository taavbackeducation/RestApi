using FluentAssertions;
using System.Linq;
using Warehouse.Entities;
using Warehouse.PersistenceEF;
using Warehouse.Services.RequestNeeds.Contracts.Dtos;
using Warehouse.Specs.Infrastructure;
using Warehouse.TestTools.Categories;
using Warehouse.TestTools.Products;
using Warehouse.TestTools.RequestNeeds;
using Xunit;

namespace Warehouse.Specs.RegisterRequestNeeds
{
    [Story(title: "درخواست نیازمندی",
        AsA = "مسئول تدارکات",
        InOrderTo = "نیازمندی کارمندان بخش مربوط به خود را برطرف کنم",
        IWantTo = "به انبار کالاهای مورد نیاز خود را درخواست بدهم")]

    [Scenario(title: "ثبت درخواست نیازمندی به انبار ")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _dbContext;
        private Product _product;
        private RegisterRequestNeedDto _dto;

        public Successful()
        {
            _dbContext = CreateDataContext();
        }

        [Given(description: "هیچ درخواست نیازمندی در سیستم به انبار وجود ندارد")]
        public void Given() { }

        [And(description: "یک کالا با نام مک بوک خوبو در دسته بندی کامپیوتر" +
                          "در انبار با موجودی 10 عدد تعریف شده است")]
        public void AndGiven()
        {
            _product = ProductFactory.GenerateProduct(title: "مک بوک خوبو", stock: 10);
            var category = new CategoryBuilder()
                                .WithTitle("کامپیوتر")
                                .WithProduct(_product)
                                .Build();
            _dbContext.Add(category);
            _dbContext.SaveChanges();
        }

        [When(description: "درخواست 2 عدد کالا با نام مک بوک خوبو مربوط به بخش " +
                           "IT" +
                           "به انبار داده می شود")]
        public void When()
        {
            _dto = RequestNeedFactory.GenerateRegisterDto(_product.Id, count: 2);
            var sut = RequestNeedFactory.GenerateService(_dbContext);

            sut.Register(_dto);
        }

        [Then(description: "باید تنها یک درخواست کالا با عنوان مک بوک خوبو مربوط به بخش" +
                           "IT" +
                           "در فهرست درخواست های نیازمندی به انبار وجود داشته باشد")]
        public void Then()
        {
            var actual = _dbContext.Set<RequestNeed>().First();

            actual.ProductId.Should().Be(_product.Id);
            actual.Section.Should().Be(_dto.Section);
            actual.Count.Should().Be(_dto.Count);
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => AndGiven(),
                _ => When(),
                _ => Then()
                );
        }
    }
}
