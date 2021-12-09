using Warehouse.Services.RequestNeeds.Contracts;
using Microsoft.EntityFrameworkCore;
using Warehouse.Entities;

namespace Warehouse.PersistenceEF.NeedRequests
{
    public class EFRequestNeedRepository : RequestNeedRepository
    {
        private readonly DbSet<RequestNeed> _requestNeeds;

        public EFRequestNeedRepository(EFDataContext dbContext)
        {
            _requestNeeds = dbContext.Set<RequestNeed>();
        }

        public void Add(RequestNeed requestNeed)
        {
            _requestNeeds.Add(requestNeed);
        }
    }
}
