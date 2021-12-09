using Warehouse.Services.Products.Contracts.Dtos;
using Warehouse.Services.Products.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Warehouse.Entities;
using System.Linq;

namespace Warehouse.PersistenceEF.Products
{
    public class EFProductRepository : ProductRepository
    {
        private readonly DbSet<Product> _products;

        public EFProductRepository(EFDataContext dbContext)
        {
            _products = dbContext.Set<Product>();
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public Product Find(int id)
        {
            return _products.FirstOrDefault(_ => _.Id == id);
        }

        public List<GetProcutDto> GetAll(string searchText)
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

        public GetProcutDto GetDetail(int id)
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

        public bool IsExist(int productId)
        {
            return _products.Any(_ => _.Id == productId);
        }

        public void Remove(Product product)
        {
            _products.Remove(product);
        }
    }
}
