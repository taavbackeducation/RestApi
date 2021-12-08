using Warehouse.Services.Categories.Contracts.Dtos;
using Warehouse.Services.Categories.Exceptions;
using Warehouse.Services.Categories.Contracts;
using Warehouse.Services.SharedContracts;
using Warehouse.Entities;

namespace Warehouse.Services.Categories
{
    public class CategoryAppService : CategoryService
    {
        private readonly CategoryRepository _categories;
        private readonly UnitOfWork _unitOfWork;

        public CategoryAppService(
            CategoryRepository categories, 
            UnitOfWork unitOfWork)
        {
            _categories = categories;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddCategoryDto dto)
        {
            StopIfCategoryIsDuplicated(dto);

            var category = new Category { Title = dto.Title };

            _categories.Add(category);

            _unitOfWork.Complete();
        }

        private void StopIfCategoryIsDuplicated(AddCategoryDto dto)
        {
            if (_categories.IsExist(dto.Title))
                throw new CategoryTitleIsDuplicatedException();
        }
    }
}
