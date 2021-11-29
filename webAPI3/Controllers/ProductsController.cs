using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using webAPI3.App.Dtos;
using webAPI3.App.Models;
using webAPI3.Repositories;

namespace webAPI3.Controllers
{
    [Route("/api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly EFDataContext _dbContext;
        private readonly DbSet<Product> _products;
        private readonly DbSet<Category> _categories;

        public ProductsController()
        {
            _dbContext = new EFDataContext();
            _products = _dbContext.Set<Product>();
            _categories = _dbContext.Set<Category>();
        }

        [HttpPost]
        public void Add([FromBody]AddProductDto dto)
        {
            if (_categories.Any(_ => _.Id == dto.CategoryId) == false)
                throw new Exception("category not exist!");
            _products.Add(GenerateProduct(dto.Title, dto.Price, dto.CategoryId));
            _dbContext.SaveChanges();
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

            _dbContext.SaveChanges();
        }

        [HttpPatch("{id}")]
        public void IncreaseStock(int id, [FromBody]IncreaseStockDto dto) 
        {
            var product = _products.FirstOrDefault(_ => _.Id == id);
            StopIfProductNotFound(product);

            product.Stock += dto.Stock;
            
            _dbContext.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id , [FromHeader] string x) 
        {
            var product = _products.FirstOrDefault(_ => _.Id == id);
            StopIfProductNotFound(product);

            _products.Remove(product);

            _dbContext.SaveChanges();
        }

        private static void StopIfProductNotFound(Product product)
        {
            if (product == null)
                throw new Exception("product is not valid");
        }

        private static Product GenerateProduct(string title, double price, int categoryId)
        {
            return new Product
            {
                Title = title,
                Price = price,
                CategoryId = categoryId,
                Stock = 0
            };
        }
    }
}
