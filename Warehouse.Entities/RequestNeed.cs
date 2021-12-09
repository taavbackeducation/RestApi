namespace Warehouse.Entities
{
    public class RequestNeed
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Section { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
