using FluentValidation;
using Komrs.User.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.User.Validation
{
    public class LoginValidation : AbstractValidator<LoginRequest>
    {
        public LoginValidation()
        {
            RuleFor(l => l.Email).EmailAddress();
            RuleFor(l => l.Password).NotEmpty();
        }
    }
}
