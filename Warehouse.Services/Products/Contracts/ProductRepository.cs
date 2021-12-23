using Warehouse.Services.Products.Contracts.Dtos;
using System.Collections.Generic;
using Warehouse.Entities;
using System.Threading.Tasks;

namespace Warehouse.Services.Products.Contracts
{
    public interface ProductRepository
    {
        void Add(Product product);
        Task<List<GetProcutDto>> GetAll(string searchText);
        GetProcutDto GetDetail(int id);
        Product Find(int id);
        void Remove(Product product);
        Task<bool> IsExist(int productId);
    }
}
