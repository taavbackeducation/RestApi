namespace Warehouse.Services.Products.Contracts.Dtos
{
    public class AddProductDto
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
