using Warehouse.Services.RequestNeeds.Contracts.Dtos;
using Warehouse.Services.RequestNeeds.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Warehouse.Controllers
{
    [Route("api/request-needs")]
    public class RequestNeedsController : ControllerBase
    {
        private readonly RequestNeedService _requestNeeds;

        public RequestNeedsController(RequestNeedService requestNeeds)
        {
            _requestNeeds = requestNeeds;
        }

        [HttpPost]
        public async Task Register([FromBody] RegisterRequestNeedDto dto)
        {
            await _requestNeeds.Register(dto);
        }
    }
}
