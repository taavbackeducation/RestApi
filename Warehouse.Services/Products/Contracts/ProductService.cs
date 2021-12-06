using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Services.Products.Contracts.Dtos;

namespace Warehouse.Services.Products.Contracts
{
    public interface ProductService
    {
        void Add(AddProductDto dto);
        List<GetProcutDto> GetAll(string searchText);
        GetProcutDto GetDetail(int id);
        void Change(int id, UpdateProductDto dto);
        void IncreaseStock(int id, IncreaseStockDto dto);
        void Delete(int id);
    }
}
