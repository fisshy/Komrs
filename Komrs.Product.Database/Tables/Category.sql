CREATE TABLE [dbo].[Category]
(
	[Id] int not null primary key identity (1,1),
	[ParentId] int null,
	[Name] nvarchar(512) not null,
	[Order] int not null  default(0), 
	[Enabled] bit null,
    CONSTRAINT [FK_Category_Category] FOREIGN KEY ([ParentId]) REFERENCES [Category]([Id])
)
