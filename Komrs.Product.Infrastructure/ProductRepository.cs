using System;
using System.Threading.Tasks;
using Komrs.Product.Models;

namespace Komrs.Product.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        public Task CreateProduct(Models.Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Models.Product product)
        {
            throw new NotImplementedException();
        }
    }
}
