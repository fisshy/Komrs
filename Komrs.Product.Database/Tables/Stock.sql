CREATE TABLE [dbo].[Stock]
(
	[Id] INT NOT NULL PRIMARY KEY,
	ProductId INT NOT NULL,
	AvailableStock int not null default(0),
	ActualStock int not null default(0), 
    CONSTRAINT [FK_Stock_Product] FOREIGN KEY (ProductId) REFERENCES Product(Id)
)
