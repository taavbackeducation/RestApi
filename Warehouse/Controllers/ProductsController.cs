﻿using Warehouse.Services.Products.Contracts.Dtos;
using Warehouse.Services.Products.Contracts;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Warehouse.Controllers
{
    [Route("/api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task Add([FromBody]AddProductDto dto)
        {
            await _productService.Add(dto);
        }

        [HttpGet]
        public async Task<List<GetProcutDto>> GetAll(string searchText = "")
        {
            return await _productService.GetAll(searchText);
        }

        [HttpGet("{id}")]
        public GetProcutDto GetDetail(int id)
        {
            return _productService.GetDetail(id);
        }

        [HttpPut("{id}")]
        public void Change(int id, [FromBody]UpdateProductDto dto)
        {
            _productService.Change(id, dto);
        }

        [HttpPatch("{id}")]
        public void IncreaseStock(int id, [FromBody]IncreaseStockDto dto) 
        {
            _productService.IncreaseStock(id, dto);
        }

        [HttpDelete("{id}")]
        public void Delete(int id) 
        {
            _productService.Delete(id);
        }
    }
}
