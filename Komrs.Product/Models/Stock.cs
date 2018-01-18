using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Models
{
    public class Stock
    {
        public int ProductId { get; set; }
        public int AvailableStock { get; set; }
        public int ActualStock { get; set; }
    }
}
