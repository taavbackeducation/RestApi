using Warehouse.Services.RequestNeeds.Contracts;
using Warehouse.PersistenceEF.NeedRequests;
using Warehouse.Services.RequestNeeds;
using Warehouse.PersistenceEF;
using Warehouse.Services.RequestNeeds.Contracts.Dtos;
using Warehouse.PersistenceEF.Products;

namespace Warehouse.TestTools.RequestNeeds
{
    public static class RequestNeedFactory
    {
        public static RequestNeedService GenerateService(EFDataContext dbContext)
        {
            var unitOfWork = new EFUnitOfWork(dbContext);
            var requestNeeds = new EFRequestNeedRepository(dbContext);
            var products = new EFProductRepository(dbContext);

            return new RequestNeedAppService(unitOfWork, requestNeeds, products);
        }

        public static RegisterRequestNeedDto GenerateRegisterDto(int productId, int count = 2)
        {
            return new RegisterRequestNeedDto
            {
                ProductId = productId,
                Count = count,
                Section = "dummy section"
            };
        }
    }
}
