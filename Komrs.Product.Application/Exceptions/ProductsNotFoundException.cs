using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Application.Exceptions
{
    public class ProductsNotFoundException : Exception
    {
        public ProductsNotFoundException(string message) : base(message)
        {
        }
    }
}
