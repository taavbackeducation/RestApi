using Warehouse.Services.RequestNeeds.Contracts.Dtos;
using Warehouse.Services.RequestNeeds.Contracts;
using Microsoft.AspNetCore.Mvc;

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
        public void Register([FromBody] RegisterRequestNeedDto dto)
        {
            _requestNeeds.Register(dto);
        }
    }
}
