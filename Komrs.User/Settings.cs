using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.User
{
    public class Settings
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public int JwtExpireDays { get; set; }
    }
}
