﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
