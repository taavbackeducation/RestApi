using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using webAPI3.App.Dtos;
using webAPI3.App.Models;

namespace webAPI3.Controllers
{
    [Route("/api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly List<Product> _products;

        public ProductsController()
        {
            _products = new List<Product>();
            _products.Add(GenerateProduct(id: 1, title: "toy car"));
            _products.Add(GenerateProduct(id: 2, title: "toy airplane"));
        }

        [HttpPost]
        public void Add([FromBody]AddProductDto dto)
        {
            _products.Add(new Product
            {
                Id = _products.Count + 1,
                Title = dto.Title,
                Price = dto.Price
            });
        }

        [HttpGet]
        public List<GetProcutDto> GetAll(string searchText = "")
        {
            return _products.Where(product => product.Title.Contains(searchText))
                .Select(_ => new GetProcutDto
                {
                    Id = _.Id,
                    Title = _.Title,
                    Price = _.Price,
                    CountInStock = _.Stock

                }).ToList();
        }

        [HttpGet("{id}")]
        public GetProcutDto GetDetail(int id)
        {
            return _products.Where(_ => _.Id == id)
                .Select(_ => new GetProcutDto
                {
                    Id = _.Id,
                    CountInStock = _.Stock,
                    Price = _.Price,
                    Title = _.Title

                }).FirstOrDefault();
        }

        [HttpPut("{id}")]
        public void Change(int id, [FromBody]UpdateProductDto dto)
        {
            var product = _products.FirstOrDefault(_ => _.Id == id);
            StopIfProductNotFound(product);

            product.Title = dto.Title;
            product.Price = dto.Price;
        }

        private static void StopIfProductNotFound(Product product)
        {
            if (product == null)
                throw new Exception("product is not valid");
        }

        [HttpPatch("{id}")]
        public void IncreaseStock(int id, [FromBody]IncreaseStockDto dto) 
        {
            var product = _products.FirstOrDefault(_ => _.Id == id);
            StopIfProductNotFound(product);

            product.Stock += dto.Stock;
        }

        [HttpDelete("{id}")]
        public void Delete(int id , [FromHeader] string x) 
        {
            var product = _products.FirstOrDefault(_ => _.Id == id);
            StopIfProductNotFound(product);

            _products.Remove(product);
        }

        private static Product GenerateProduct(int id, string title)
        {
            return new Product
            {
                Id = id,
                Title = title,
                Price = 1000,
                Stock = 10
            };
        }
    }
}
