using EventBus;
using Komrs.User.Events;
using Komrs.User.Exceptions;
using Komrs.User.Models;
using Komrs.User.Requests;
using Komrs.User.Validation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Validator;
using System.Linq;

namespace Komrs.User
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, LoginResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IBusClient _client;

        public RegisterHandler(UserManager<ApplicationUser> userManager, IMediator mediator, IBusClient client)
        {
            _userManager = userManager;
            _mediator = mediator;
            _client = client;
        }

        async Task<LoginResult> IRequestHandler<RegisterRequest, LoginResult>.Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            await Validator<RegisterValidation, RegisterRequest>.ValidateAndThrowAsync(request);

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                _client.PublishEvent(new UserCreated { Id = user.Id, Email = user.Email, UserName = request.Email });
                return await _mediator.Send(new LoginRequest(request.Email, request.Password));
            }

            if(result.Errors != null)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new RegisterException(string.Join(",", errors));
            }

            throw new Exception("Register failed, please contact your administrator.");
        }
    }
}
