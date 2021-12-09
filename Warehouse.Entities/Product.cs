using System.Collections.Generic;

namespace Warehouse.Entities
{
    public class Product
    {
        public Product()
        {
            RequestNeeds = new HashSet<RequestNeed>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public HashSet<RequestNeed> RequestNeeds { get; set; }
    }
}
