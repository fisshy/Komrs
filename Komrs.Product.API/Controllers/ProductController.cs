using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Komrs.Product.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using Komrs.Product.Application.Requests;
using Komrs.Product.Application.Exceptions;

namespace Komrs.Product.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/products")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<ProductListItemModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ListAllProducts(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _mediator.Send(new ListAllProductsRequest(), cancellationToken));
            }
            catch (ProductsNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to list products");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to register");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductModel p, [FromForm]List<IFormFile> files, CancellationToken cancellationToken)
        {
            try
            {
                /* TODO -> Use automap */
                CreateProductRequest request = new CreateProductRequest
                {
                    ArticleNumber = p.ArticleNumber,
                    Name = p.Name,
                    Description = p.Description,
                    ProductInfo = p.ProductInfo,
                    Price = p.Price,
                    Currency = p.Currency,
                    ActualStock = p.ActualStock,
                    Meta = p.Meta,
                    Categories = p.Categories,
                    Height = p.Height,
                    Length = p.Length,
                    Supplier = p.Supplier,
                    Tags = p.Tags,
                    Weight = p.Weight,
                    Width = p.Width,
                    Images = files
                };

                var productId = await _mediator.Send(request, cancellationToken);


                return Ok();
            }
            catch (ProductNotCreatedException ex)
            {
                _logger.LogError(ex, "Failed to create product");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to product");
                return BadRequest();
            }
        }
    }
}