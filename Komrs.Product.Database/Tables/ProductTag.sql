CREATE TABLE [dbo].[ProductTag]
(
	[ProductId] int not null,
	[TagId] int not null, 
    CONSTRAINT [FK_ProductTag_Product] FOREIGN KEY (ProductId) REFERENCES Product(Id),
	CONSTRAINT [FK_ProductTag_Tag] FOREIGN KEY (TagId) REFERENCES Tag(Id),
	CONSTRAINT PK_Product_Tag PRIMARY KEY (ProductId,TagId)
)
