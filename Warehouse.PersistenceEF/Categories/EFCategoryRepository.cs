﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Entities;
using System.Linq;
using Warehouse.Services.Categories.Contracts;

namespace Warehouse.PersistenceEF.Categories
{
    public class EFCategoryRepository : CategoryRepository
    {
        private readonly DbSet<Category> _categories;

        public EFCategoryRepository(EFDataContext dbContext)
        {
            _categories = dbContext.Set<Category>();
        }

        public void Add(Category category)
        {
            _categories.Add(category);
        }

        public bool IsExist(int categoryId)
        {
            return _categories.Any(_ => _.Id == categoryId);
        }
		
		public bool IsExist(string title)
        {
            return _categories.Any(_ => _.Title == title);
        }
    }
}
