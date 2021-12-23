using Warehouse.Services.Products.Contracts.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Services.Products.Contracts
{
    public interface ProductService
    {
        Task Add(AddProductDto dto);
        Task<List<GetProcutDto>> GetAll(string searchText);
        GetProcutDto GetDetail(int id);
        void Change(int id, UpdateProductDto dto);
        void IncreaseStock(int id, IncreaseStockDto dto);
        void Delete(int id);
    }
}
