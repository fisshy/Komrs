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

        public async Task CreateProduct(CreateProduct product)
        {
            using (var trans = NewTransaction())
            {
                try
                {
                    trans.Begin();

                    var supplierId = product.Supplier.Id ?? await trans.QueryFirstAsync<int>("INSERT INTO dbo.Supplier(Name) VALUES (@Name) SELECT SCOPE_IDENTITY()", new { product.Supplier.Name });

                    var productId = await trans.QueryFirstAsync<int>(
                        @"INSERT INTO dbo.Product(
                                [SupplierId],
                                [ArticleNumber],
                                [Name],
                                [Description],
                                [Price],
                                [Currency],
                                [ProductInfo],
                                [Height],
                                [Width],
                                [Length],
                                [Weight]
                            ) 
                            VALUES (
                                @SupplierId,
                                @ArticleNumber,
                                @Name,
                                @Description,
                                @Price,
                                @Currency,
                                @ProductInfo,
                                @Height,
                                @Width,
                                @Length,
                                @Weight
                            ) SELECT SCOPE_IDENTITY();", new
                        {
                            ArticleNumber = product.ArticleNumber,
                            SupplierId = supplierId,
                            ActualStock = product.ActualStock,
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            Currency = product.Currency,
                            ProductInfo = product.ProductInfo,
                            Height = product.Height,
                            Width = product.Width,
                            Length = product.Length,
                            Weight = product.Weight
                        });

                    foreach (var image in product.Images)
                    {
                        await trans.QueryFirstAsync<int>(
                            @"INSERT INTO dbo.ProductImage (
                                    [ProductId]
                                    [Name]
                                    [Url]
                                    [Type]
                                )
                                VALUES (
                                    @ProductId,
                                    @Name,
                                    @Url,
                                    @Type
                                )", new
                            {
                                ProductId = productId,
                                Name = image.Name,
                                Url = image.Url,
                                Type = image.Type
                            });
                    }

                    foreach (var tag in product.Tags)
                    {
                        var tagId = tag.Id ??
                            await trans.QueryFirstAsync<int>("INSERT INTO dbo.Tag(Name) VALUES (@Name) SELECT SCOPE_IDENTITY()", tag);

                        await trans.ExecuteAsync("INSERT INTO dbo.ProductTag(ProductId, TagId) VALUES (@ProductId, @TagId)",
                            new { ProductId = productId, TagId = tagId });
                    }

                    foreach (var meta in product.Meta)
                    {
                        var metaId = meta.Id ??
                            await trans.QueryFirstAsync<int>("INSERT INTO dbo.Meta(Name) VALUES (@Name) SELECT SCOPE_IDENTITY()", meta);

                        await trans.ExecuteAsync("INSERT INTO dbo.ProductMeta(ProductId, MetaId, Value) VALUES (@ProductId, @MetaId, @Value)",
                            new { MetaId = metaId, ProductId = productId, Value = meta.Value });
                    }

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public async Task<int> CreateSupplier(CreateSupplier supplier)
        {
            return await QueryFirstAsync<int>(
                @"IF NOT EXISTS ( SELECT TOP 1 1 FROM dbo.Supplier WITH(NOLOCK) WHERE Name = @Name)
                BEGIN
                    INSERT INTO dbo.Supplier(Name) VALUES (@Name) SELECT SCOPE_IDENTITY()
                    RETURN
                END 
                SELECT TOP 1 Id FROM dbo.Supplier WITH(NOLOCK) WHERE Name = @Name", 
                new { Name = supplier.Name });
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
