using Komrs.User.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Komrs.User
{
    public static class Token
    {
        public static LoginResult GenerateJwtToken(string email, ApplicationUser user, IOptions<Settings> settings)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Value.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(settings.Value.JwtExpireDays));

            var token = new JwtSecurityToken(
                settings.Value.JwtIssuer,
                settings.Value.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResult
            {
                AccessToken = accessToken,
                ValidTo = token.ValidTo
            };
        }
    }
}
