using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Models
{
    public class ProductListItem
    {
        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SupplierName { get; set; }
        public int AvailableStock { get; set; }
        public int ActualStock { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string ProductInfo { get; set; }
        public string ImageUrl { get; set; }
    }
}
