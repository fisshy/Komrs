using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Komrs.User;
using MediatR;
using Komrs.User.Requests;
using Komrs.User.API.Models;
using System.Threading;
using System.Net;
using Komrs.User.Models;
using Microsoft.Extensions.Logging;
using Komrs.User.Exceptions;

namespace Komrs.User.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/user")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(LoginResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _mediator.Send(new LoginRequest(model.Email, model.Password), cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to register");
                return NotFound();
            }
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(LoginResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _mediator.Send(new RegisterRequest(model.Email, model.Password, model.RepeatPassword), cancellationToken));
            }
            catch(RegisterException ex)
            {
                _logger.LogError(ex, "Failed to register");
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "Failed to register");
                return BadRequest();
            }
        }
    }
}