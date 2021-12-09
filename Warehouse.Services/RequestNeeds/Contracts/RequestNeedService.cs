using Warehouse.Services.RequestNeeds.Contracts.Dtos;

namespace Warehouse.Services.RequestNeeds.Contracts
{
    public interface RequestNeedService
    {
        void Register(RegisterRequestNeedDto dto);
    }
}
