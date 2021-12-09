using Warehouse.Entities;

namespace Warehouse.TestTools.Categories
{
    public class CategoryBuilder
    {
        private Category _category;

        public CategoryBuilder()
        {
            _category = new Category { Title = "dummy title" };
        }

        public CategoryBuilder WithTitle(string title)
        {
            _category.Title = title;

            return this;
        }

        public CategoryBuilder WithProduct(Product product)
        {
            _category.Products.Add(product);

            return this;
        }

        public Category Build()
        {
            return _category;
        }
    }
}
