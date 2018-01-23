using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using Komrs.Product.Application.Models;
using System.Net;
using System.Threading;
using Komrs.Product.Application.Exceptions;
using Komrs.Product.Application.Requests;

namespace Komrs.Product.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/suppliers")]
    public class SupplierController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(IMediator mediator, ILogger<SupplierController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateSupplier([FromBody]CreateSupplierModel s, CancellationToken cancellationToken)
        {
            try
            {
                var supplierId = await _mediator.Send(new CreateSupplierRequest
                {
                    Name = s?.Name
                });

                return CreatedAtAction("CreateSupplier", supplierId);
            }
            catch (SupplierNotCreateException ex)
            {
                _logger.LogError(ex, "Failed to create supplier");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to create supplier");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<SupplierModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ListAllSuppliers( CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _mediator.Send(new ListAllSuppliersRequest()));
            }
            catch (SuppliersNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to list supplier");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to list supplier");
                return BadRequest();
            }
        }
    }
}