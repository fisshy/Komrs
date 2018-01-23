using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Application.Exceptions
{
    public class SupplierNotCreateException : Exception
    {
        public SupplierNotCreateException(string message) : base(message)
        {
        }
    }
}
