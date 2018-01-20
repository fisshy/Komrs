using Komrs.Product.Application.Models;
using Komrs.Product.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Application.Requests
{
    public class CreateProductRequest : CreateProductModel, IRequest<int>
    {
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
