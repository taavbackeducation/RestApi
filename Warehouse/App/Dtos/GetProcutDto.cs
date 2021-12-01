namespace Warehouse.App.Dtos
{
    public class GetProcutDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int CountInStock { get; set; }
        public int CategoryId { get; set; }
    }
}
