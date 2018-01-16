using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.User.Exceptions
{
    public class RegisterException : Exception
    {
        public RegisterException()
        {
        }

        public RegisterException(string message)
        : base(message)
        {
        }
    }
}
