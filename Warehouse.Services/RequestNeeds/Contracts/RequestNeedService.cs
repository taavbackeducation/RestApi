using System.Threading.Tasks;
using Warehouse.Services.RequestNeeds.Contracts.Dtos;

namespace Warehouse.Services.RequestNeeds.Contracts
{
    public interface RequestNeedService
    {
        Task Register(RegisterRequestNeedDto dto);
    }
}
