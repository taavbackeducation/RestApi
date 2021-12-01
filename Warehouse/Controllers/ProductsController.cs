using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Warehouse.App.Dtos;
using Warehouse.App.Models;
using Warehouse.Repositories;
using Warehouse.Repositories.Categories;
using Warehouse.Repositories.Products;

namespace Warehouse.Controllers
{
    [Route("/api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsRepository _products;
        private readonly CategoriesRepository _categories;
        private readonly UnitOfWork _unitOfWork;

        public ProductsController()
        {
            var dbContext = new EFDataContext();
            _products = new ProductsRepository(dbContext);
            _categories = new CategoriesRepository(dbContext);
            _unitOfWork = new UnitOfWork(dbContext);
        }

        [HttpPost]
        public void Add([FromBody]AddProductDto dto)
        {
            StopIfCategoryNotExist(dto.CategoryId);
            _products.Add(GenerateProduct(dto.Title, dto.Price, dto.CategoryId));
            _unitOfWork.Complete();
        }

        [HttpGet]
        public List<GetProcutDto> GetAll(string searchText = "")
        {
            return _products.GetAll(searchText);
        }

        [HttpGet("{id}")]
        public GetProcutDto GetDetail(int id)
        {
            return _products.GetDetail(id);
        }

        [HttpPut("{id}")]
        public void Change(int id, [FromBody]UpdateProductDto dto)
        {
            var product = _products.Find(id);
            StopIfProductNotFound(product);

            StopIfCategoryNotExist(dto.CategoryId);

            product.Title = dto.Title;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;

            _unitOfWork.Complete();
        }

        [HttpPatch("{id}")]
        public void IncreaseStock(int id, [FromBody]IncreaseStockDto dto) 
        {
            var product = _products.Find(id);
            StopIfProductNotFound(product);

            product.Stock += dto.Stock;

            _unitOfWork.Complete();
        }

        [HttpDelete("{id}")]
        public void Delete(int id , [FromHeader] string x) 
        {
            var product = _products.Find(id);
            StopIfProductNotFound(product);

            _products.Remove(product);

            _unitOfWork.Complete();
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
        private void StopIfCategoryNotExist(int categoryId)
        {
            if (!_categories.IsExist(categoryId))
                throw new Exception("category not exist!");
        }

    }
}
