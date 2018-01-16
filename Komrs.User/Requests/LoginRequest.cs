using Komrs.User.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.User.Requests
{
    public class LoginRequest : IRequest<LoginResult>
    {

        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        internal string Email { get; set; }
        internal string Password { get; set; }
    }
}
