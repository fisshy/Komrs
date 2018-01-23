using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Komrs.Product.Models;
using Microsoft.Extensions.Logging;
using Database;

namespace Komrs.Product.Infrastructure
{
    public class ProductQueryRepository : DbContext, IProductQueryRepository
    { 

        public ProductQueryRepository(string connectionString, ILogger<ProductQueryRepository> logger) : base(connectionString, logger)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await Query<Category>("SELECT Id, Name, Order, ParentId FROM dbo.Category with(nolock)");
        }

        public async Task<IEnumerable<ProductListItem>> GetAllProducts()
        {
            return await Query<ProductListItem>("SELECT * FROM dbo.GetAllProductListItems()");
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            return await Query<Supplier>("SELECT Id, Name FROM dbo.Supplier WITH(NOLOCK)");
        }

        public Task<Models.Product> GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductListItem>> GetProductsInCategory(int categoryId)
        {
            return await Query<ProductListItem>("SELECT * FROM dbo.GetFilteredProductListItems(@categoryId)", new { categoryId  });
        }
    }
}
