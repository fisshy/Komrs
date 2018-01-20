using Komrs.Product.Application.Models;
using Komrs.Product.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Komrs.Product.Models;
using Storage;

namespace Komrs.Product.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, int>
    {
        private readonly IProductRepository _repository;
        private readonly IStorage _storage;

        public CreateProductHandler(IProductRepository repository, IStorage storage)
        {
            _repository = repository;
            _storage = storage;
        }

        public async Task<int> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            /* TODO -> Validate */

            var images = new List<ProductImage>();

            foreach (var image in request.Images)
            {
                var file = new File
                {
                    FileName = image.FileName
                };

                await image.CopyToAsync(file.Stream, cancellationToken);

                var url = await _storage.UploadImage(file, cancellationToken);

                images.Add(new ProductImage
                {
                    Name = image.FileName,
                    Type = "original",
                    Url = url
                });
            }

            await _repository.CreateProduct(new CreateProduct
            {
                ArticleNumber = request.ArticleNumber,
                Name = request.Name,
                Description = request.Description,
                ProductInfo = request.ProductInfo,
                Price = request.Price,
                Currency = request.Currency,
                ActualStock = request.ActualStock,
                Meta = request.Meta,
                Categories = request.Categories,
                Height = request.Height,
                Length = request.Length,
                SupplierId = request.SupplierId,
                Tags = request.Tags,
                Weight = request.Weight,
                Width = request.Width,
                Images = images
            });


            return 0;
        }
    }
}
