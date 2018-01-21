using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Models
{
    public class CreateProduct
    {
        public string ArticleNumber { get; set; }
        public int ActualStock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string ProductInfo { get; set; }
        public decimal? Height { get; set; }
        public decimal? Width { get; set; }
        public decimal? Length { get; set; }
        public decimal? Weight { get; set; }

        public Supplier Supplier { get; set; }

        public IEnumerable<int> Categories { get; set; }
        public IEnumerable<ProductTag> Tags { get; set; }
        public IEnumerable<ProductMeta> Meta { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
    }
}
