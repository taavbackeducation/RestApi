using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using webAPI3.App.Dtos;
using webAPI3.App.Models;
using webAPI3.Repositories;

namespace webAPI3.Repositories.Products
{
    internal class ProductsRepository
    {
        private readonly DbSet<Product> _products;

        public ProductsRepository(EFDataContext dbContext)
        {
            _products = dbContext.Set<Product>();
        }

        internal void Add(Product product)
        {
            _products.Add(product);
        }

        internal Product Find(int id)
        {
            return _products.FirstOrDefault(_ => _.Id == id);
        }

        internal List<GetProcutDto> GetAll(string searchText)
        {
            return _products.Where(product => product.Title.Contains(searchText))
                .Select(_ => new GetProcutDto
                {
                    Id = _.Id,
                    Title = _.Title,
                    Price = _.Price,
                    CountInStock = _.Stock,
                    CategoryId = _.CategoryId

                }).ToList();
        }

        internal GetProcutDto GetDetail(int id)
        {
            return _products.Where(_ => _.Id == id)
                .Select(_ => new GetProcutDto
                {
                    Id = _.Id,
                    CountInStock = _.Stock,
                    Price = _.Price,
                    Title = _.Title,
                    CategoryId = _.CategoryId

                }).FirstOrDefault();
        }

        internal void Remove(Product product)
        {
            _products.Remove(product);
        }
    }
}
