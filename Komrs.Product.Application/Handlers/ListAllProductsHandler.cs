using Komrs.Product.Application.Exceptions;
using Komrs.Product.Application.Models;
using Komrs.Product.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Komrs.Product.Application.Handlers
{
    public class ListAllProductsHandler : IRequestHandler<ListAllProductsRequest, IEnumerable<ProductListItemModel>>
    {
        private readonly IProductQueryRepository _repository;

        public ListAllProductsHandler(IProductQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductListItemModel>> Handle(ListAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllProducts();

            if(products == null)
            {
                throw new ProductsNotFoundException("Products not found");
            }

            return products.Select(p => new ProductListItemModel()
            {
                Id = p.Id,
                ArticleNumber = p.ArticleNumber,
                Currency = p.Currency,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Price = p.Price,
                ProductInfo = p.ProductInfo,
                SupplierName = p.SupplierName
            });
        }
    }
}
