using Komrs.User.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.User.Requests
{
    public class RegisterRequest : IRequest<LoginResult>
    {
        public RegisterRequest(string email, string password, string repeatPassword)
        {
            Email = email;
            Password = password;
            RepeatPassword = repeatPassword;
        }

        internal string Email { get; set; }
        internal string Password { get; set; }
        internal string RepeatPassword { get; set; }
    }
}
