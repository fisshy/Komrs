using Komrs.Product.Application.Models;
using Komrs.Product.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Komrs.Product.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, ProductModel>
    {
        public Task<ProductModel> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
