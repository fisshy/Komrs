CREATE TABLE [dbo].[ProductImage]
(
	[Id] int not null primary key identity (1,1),
	[ProductId] int not null,
	[Name] nvarchar(256) null,
	[Url] nvarchar(512) not null,
	[Type] nvarchar(50) not null, 
    CONSTRAINT [FK_ProductImage_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
)
