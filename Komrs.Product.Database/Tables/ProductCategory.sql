CREATE TABLE [dbo].[ProductCategory]
(
	[ProductId] int not null,
	[CategoryId] int not null, 
    CONSTRAINT [FK_ProductCategory_Product] FOREIGN KEY (ProductId) REFERENCES Product(Id),
	CONSTRAINT [FK_ProductCategory_Tag] FOREIGN KEY (CategoryId) REFERENCES Tag(Id),
	CONSTRAINT PK_ProductCategory_Tag PRIMARY KEY (ProductId,CategoryId)
)
