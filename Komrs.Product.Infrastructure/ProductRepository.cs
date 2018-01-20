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

                    var productId = await trans.Execute(
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
                            SupplierId = product.SupplierId,
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
                        await trans.Execute(
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
                        var tagId = tag.Id ?? await trans.Execute("INSERT INTO dbo.Tag(Name) VALUES (@Name)", tag);
                        await trans.Execute("INSERT INTO dbo.ProductTag(ProductId, TagId) VALUES (@ProductId, @TagId)",
                            new { ProductId = productId, TagId = tagId });
                    }

                    foreach (var meta in product.Meta)
                    {
                        var metaId = meta.Id ?? await trans.Execute("INSERT INTO dbo.Meta(Name, Value) VALUES (@Name, @Value)", meta);
                        await trans.Execute("INSERT INTO dbo.ProductMeta(ProductId, MetaId) VALUES (@ProductId, @MetaId)",
                            new { MetaId = metaId, ProductId = productId });
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
