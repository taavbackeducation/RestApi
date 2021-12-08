using System.Collections.Generic;
using Warehouse.Entities;
using System;
using Warehouse.Services.Products.Contracts.Dtos;
using Warehouse.Services.Products.Contracts;
using Warehouse.Services.SharedContracts;
using Warehouse.Services.Categories.Contracts;
using Warehouse.Services.Products.Exceptions;

namespace Warehouse.Services.Products
{
    public class ProductAppService : ProductService
    {
        private readonly ProductRepository _products;
        private readonly CategoryRepository _categories;
        private readonly UnitOfWork _unitOfWork;

        public ProductAppService(
            ProductRepository products, 
            CategoryRepository categories, 
            UnitOfWork unitOfWork)
        {
            _products = products;
            _categories = categories;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddProductDto dto)
        {
            StopIfCategoryNotExist(dto.CategoryId);
            _products.Add(GenerateProduct(dto.Title, dto.Price, dto.CategoryId));
            _unitOfWork.Complete();
        }

        public List<GetProcutDto> GetAll(string searchText)
        {
            return _products.GetAll(searchText);
        }

        public GetProcutDto GetDetail(int id)
        {
            return _products.GetDetail(id);
        }

        public void Change(int id, UpdateProductDto dto)
        {
            var product = _products.Find(id);
            StopIfProductNotFound(product);

            StopIfCategoryNotExist(dto.CategoryId);

            product.Title = dto.Title;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;

            _unitOfWork.Complete();
        }

        public void IncreaseStock(int id, IncreaseStockDto dto)
        {
            var product = _products.Find(id);
            StopIfProductNotFound(product);

            product.Stock += dto.Stock;

            _unitOfWork.Complete();
        }

        public void Delete(int id)
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
                throw new CategoryNotFoundException();
        }
    }
}
