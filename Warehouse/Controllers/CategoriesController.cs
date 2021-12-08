using Microsoft.AspNetCore.Mvc;
using Warehouse.Services.Categories.Contracts;
using Warehouse.Services.Categories.Contracts.Dtos;

namespace Warehouse.Controllers
{
    [Route("/api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoreis;

        public CategoriesController(CategoryService categoreis)
        {
            _categoreis = categoreis;
        }

        [HttpPost]
        public void Add(AddCategoryDto dto)
        {
            _categoreis.Add(dto);
        }
    }
}
