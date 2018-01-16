using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.User.API.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; internal set; }
    }
}
