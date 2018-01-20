﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Komrs.Product
{
    public interface IProductRepository
    {
        Task CreateProduct(Models.CreateProduct product);
        Task UpdateProduct(Models.Product product);
        Task UpdateStock(Models.Stock stock);
    }

    public interface IProductQueryRepository
    {
        Task<Models.Product> GetProduct(int productId);
        Task<IEnumerable<Models.ProductListItem>> GetAllProducts();
        Task<IEnumerable<Models.ProductListItem>> GetProductsInCategory(int categoryId);
        Task<IEnumerable<Models.Category>> GetAllCategories();
    }
}
