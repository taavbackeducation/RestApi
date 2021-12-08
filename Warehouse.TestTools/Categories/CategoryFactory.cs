using Warehouse.Entities;

namespace Warehouse.TestTools.Categories
{
    public static class CategoryFactory
    {
        public static Category GenerateCategory(string title = "toy")
        {
            return new Category { Title = title };
        }
    }
}
