using Komrs.Product.Application.Exceptions;
using Komrs.Product.Application.Models;
using Komrs.Product.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Komrs.Product.Application.Handlers
{
    public class ListAllSuppliersHandler : IRequestHandler<ListAllSuppliersRequest, IEnumerable<SupplierModel>>
    {
        private readonly IProductQueryRepository _repository;

        public ListAllSuppliersHandler(IProductQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SupplierModel>> Handle(ListAllSuppliersRequest request, CancellationToken cancellationToken)
        {
            var suppliers = await _repository.GetAllSuppliers();

            if(suppliers == null)
            {
                throw new SuppliersNotFoundException("Suppliers not found");
            }

            return suppliers.Select(s => new SupplierModel
            {
                Id = s.Id.Value,
                Name = s.Name
            });

        }
    }
}
