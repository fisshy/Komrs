using System;
using System.Threading.Tasks;
using Komrs.Product.Models;
using Microsoft.Extensions.Logging;
using Database;

namespace Komrs.Product.Infrastructure
{
    public class ProductRepository : DbContext, IProductRepository
    {
        public ProductRepository(string connectionString, ILogger<ProductRepository> logger) : base(connectionString, logger)
        {
        }

        public async Task CreateProduct(Models.Product product)
        {
            using (var trans = NewTransaction())
            {
                try
                {
                    trans.Begin();

                    await trans.Execute("", null);

                    await trans.Execute("", null);

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
        }

        public Task UpdateProduct(Models.Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStock(Stock stock)
        {
            return Execute("INERT INTO dbo.Stock(ProductId, AvailableStock, ActualStock) VALUES (@ProductId, @AvailableStock, @ActualStock)", stock);
        }
    }
}
