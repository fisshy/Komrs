using Komrs.Product.Application.Requests;
using Komrs.Product.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Komrs.Product.Application.Handlers
{
    public class CreateSupplierHandler : IRequestHandler<CreateSupplierRequest, int>
    {
        private readonly IProductRepository _repository;

        public CreateSupplierHandler(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(CreateSupplierRequest request, CancellationToken cancellationToken)
        {
            return await _repository.CreateSupplier(new CreateSupplier
            {
                Name = request.Name
            });
        }
    }
}
