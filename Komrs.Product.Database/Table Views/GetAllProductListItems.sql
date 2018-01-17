CREATE FUNCTION [dbo].[GetAllProductListItems]
(
	
)
RETURNS @returntable TABLE
(
	Id int not null,
	ArticleNumber nvarchar(256) not null,
	Name nvarchar(256) not null,
	Description nvarchar(1024) not null,
	SupplierName  nvarchar(256) not null,
	AvailableStock int not null,
	ActualStock int not null,
	Price money not null,
	Currency nvarchar(5) not null,
	ProductInfo nvarchar(2056)
)
AS
BEGIN
	INSERT @returntable (
		Id, 
		ArticleNumber, 
		Name, 
		Description, 
		SupplierName, 
		AvailableStock, 
		ActualStock, 
		Price, 
		Currency, 
		ProductInfo
	)
	SELECT	p.Id, 
			p.ArticleNumber, 
			p.Name, 
			p.Description, 
			s.Name as SupplierName, 
			ps.AvailableStock, 
			ps.ActualStock, 
			p.Price, 
			p.Currency, 
			p.ProductInfo
		FROM dbo.Product p with(nolock)
			INNER JOIN dbo.ProductStock ps on p.Id = ps.ProductId
			INNER JOIN dbo.Supplier s with(nolock) on s.Id = p.SupplierId
	
	RETURN
END
