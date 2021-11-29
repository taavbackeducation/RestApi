﻿using System.Collections.Generic;

namespace webAPI3.App.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public HashSet<Product> Products { get; set; }
    }
}
