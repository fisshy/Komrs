using FluentValidation;
using Komrs.User.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.User.Validation
{
    public class RegisterValidation : AbstractValidator<RegisterRequest>
    {
        public RegisterValidation()
        {
            RuleFor(r => r.Email).EmailAddress().WithMessage("Valid email is missing");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is missing");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Repeat-password is missing");
            RuleFor(r => r.RepeatPassword).NotEqual(r => r.Password).WithMessage("Passwords does not match");
        }
    }
}
