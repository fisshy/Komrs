CREATE TABLE [dbo].[Product]
(
	[Id] int not null primary key identity(1,1),
	[SupplierId] int not null,
	ArticleNumber nvarchar(256) not null,
	[Name] nvarchar(256) not null,
	[Description] nvarchar(1024) not null,
	Price money not null,
	Currency nvarchar(5) not null,
	ProductInfo nvarchar(2056), 
	Height decimal(18, 2),
	Width  decimal(18, 2),
	[Length] decimal(18, 2),
	[Weight] decimal(18, 2)

    CONSTRAINT [FK_Product_Supplier] FOREIGN KEY (SupplierId) REFERENCES Supplier(Id)
)
