using Warehouse.Services.RequestNeeds.Contracts.Dtos;
using Warehouse.Entities;
using Warehouse.Services.SharedContracts;
using System;
using Warehouse.Services.RequestNeeds.Contracts;
using Warehouse.Services.RequestNeeds.Exceptions;
using Warehouse.Services.Products.Contracts;

namespace Warehouse.Services.RequestNeeds
{
    public class RequestNeedAppService : RequestNeedService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly RequestNeedRepository _requestNeeds;
        private readonly ProductRepository _Products;

        public RequestNeedAppService(
            UnitOfWork unitOfWork,
            RequestNeedRepository requestNeeds,
            ProductRepository products)
        {
            _unitOfWork = unitOfWork;
            _requestNeeds = requestNeeds;
            _Products = products;
        }

        public void Register(RegisterRequestNeedDto dto)
        {
            StopIfProductNotFound(dto);
            StopIfRequestNeedCoundIsLessThanOne(dto);

            var requestNeed = new RequestNeed
            {
                ProductId = dto.ProductId,
                Count = dto.Count,
                Section = dto.Section
            };

            _requestNeeds.Add(requestNeed);
            _unitOfWork.Complete();
        }

        private static void StopIfRequestNeedCoundIsLessThanOne(RegisterRequestNeedDto dto)
        {
            if (dto.Count <= 0)
                throw new RequestNeedCountIsLessThanOneException();
        }

        private void StopIfProductNotFound(RegisterRequestNeedDto dto)
        {
            if (!_Products.IsExist(dto.ProductId))
                throw new ProductNotFoundException();
        }
    }
}
