using Warehouse.Services.Products.Contracts.Dtos;
using System.Collections.Generic;
using Warehouse.Entities;

namespace Warehouse.Services.Products.Contracts
{
    public interface ProductRepository
    {
        void Add(Product product);
        List<GetProcutDto> GetAll(string searchText);
        GetProcutDto GetDetail(int id);
        Product Find(int id);
        void Remove(Product product);
    }
}
