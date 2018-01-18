using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Komrs.Product.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Database;

namespace Komrs.Product.Infrastructure
{
    public class ProductQueryRepository : DbContext, IProductQueryRepository
    { 
        private readonly string connectionString;
        private readonly ILogger logger;

        public ProductQueryRepository(string connectionString, ILogger<ProductQueryRepository> logger) : base(connectionString, logger)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await Query<Category>("SELECT Id, Name, Order, ParentId FROM dbo.Category with(nolock)");
        }

        public async Task<IEnumerable<Models.ProductListItem>> GetAllProducts()
        {
            return await Query<ProductListItem>("SELECT * FROM dbo.GetAllProductListItems()");
        }

        public Task<Models.Product> GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Models.ProductListItem>> GetProductsInCategory(int categoryId)
        {
            return await Query<ProductListItem>("SELECT * FROM dbo.GetFilteredProductListItems(@categoryId)", new { categoryId  });
        }
    }
}
