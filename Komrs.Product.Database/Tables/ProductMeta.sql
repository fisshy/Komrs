CREATE TABLE [dbo].[ProductMeta]
(
    [ProductId] INT NOT NULL,
    [MetaId] INT NOT NULL, 
    CONSTRAINT [FK_ProductMeta_Product] FOREIGN KEY (ProductId) REFERENCES Product(Id),
    CONSTRAINT [FK_ProductMeta_Meta] FOREIGN KEY (MetaId) REFERENCES Meta(Id),
    CONSTRAINT PK_Product_Meta PRIMARY KEY (ProductId,MetaId)
)
