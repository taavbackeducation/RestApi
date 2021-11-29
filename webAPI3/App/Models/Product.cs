using System.ComponentModel.DataAnnotations;

namespace webAPI3.App.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
