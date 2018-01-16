using Komrs.User.Models;
using Komrs.User.Requests;
using Komrs.User.Validation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Validator;

namespace Komrs.User
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOptions<Settings> _settings;

        public LoginHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<Settings> settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _settings = settings;
        }

        public async Task<LoginResult> Handle(LoginRequest request, CancellationToken cancellationToken)
        {

            await Validator<LoginValidation, LoginRequest>.ValidateAndThrowAsync(request);

            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == request.Email);
                return Token.GenerateJwtToken(request.Email, appUser, _settings);
            }

            throw new Exception("LOGIN_FAILED");
        }

        
    }
}
