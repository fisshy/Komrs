using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ArticleNumber { get; set; }

        public int AvailableStock { get; set; }
        public int ActualStock { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string ProductInfo { get; set; }

        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal Weight { get; set; }

        public Supplier Supplier { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
        public IEnumerable<ProductTag> Tags { get; set; }
    }
}
