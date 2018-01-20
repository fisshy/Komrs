using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Application.Models
{
    public class ProductListItemModel
    {
        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SupplierName { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string ProductInfo { get; set; }
        public string ImageUrl { get; set; }
    }
}
