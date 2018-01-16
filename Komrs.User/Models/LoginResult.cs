using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.User.Models
{
    public class LoginResult
    {
        public string AccessToken { get; internal set; }
        public DateTime ValidTo { get; internal set; }
    }
}
