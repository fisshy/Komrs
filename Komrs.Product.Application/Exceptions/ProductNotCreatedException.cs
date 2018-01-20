using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Application.Exceptions
{
    public class ProductNotCreatedException : Exception
    {
        public ProductNotCreatedException(string message) : base(message)
        {
        }
    }
}
