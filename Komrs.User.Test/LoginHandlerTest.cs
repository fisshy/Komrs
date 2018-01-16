using MediatR;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Komrs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Komrs.User.Test
{

    public class LoginHandlerTest : IDisposable
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signinManager;
        private Settings _settings;


        public LoginHandlerTest()
        {
            var serviceCollection = new ServiceCollection()
                .AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("KomrsIdentityDB"));


            serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>((a) =>
            {
                return new HttpContextAccessor
                {
                    HttpContext = new DefaultHttpContext()
                };
            });

            serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _signinManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

            _settings = new Settings
            {
                JwtExpireDays = 30,
                JwtIssuer = "joachim",
                JwtKey = "ABC%%%111999__----abc90+!?"
            };
        }

        public void Dispose()
        {

        }

        [Fact]
        public async Task ShouldThrowExceptionIfEmptyNameAndOrPassword()
        {
            var loginHandler = new LoginHandler(_userManager, _signinManager, _settings);

            await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () =>
            {
                await loginHandler.Handle(new Requests.LoginRequest(string.Empty, string.Empty), new CancellationToken());
            });

            await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () =>
            {
                await loginHandler.Handle(new Requests.LoginRequest(string.Empty, "password"), new CancellationToken());
            });

            await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () =>
            {
                await loginHandler.Handle(new Requests.LoginRequest("username", string.Empty), new CancellationToken());
            });
        }

        [Fact]
        public void ShouldGenerateToken()
        {
            var token = Token.GenerateJwtToken("test@test.se", new ApplicationUser
            {
                Email = "test@test.se",
                Id = "test"

            }, _settings);

            Assert.NotEmpty(token.AccessToken);
            Assert.True(token.ValidTo > DateTime.Now);
        }
    }
}
