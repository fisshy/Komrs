using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Application.Exceptions
{
    public class SuppliersNotFoundException : Exception
    {
        public SuppliersNotFoundException(string message) : base(message)
        {
        }
    }
}
