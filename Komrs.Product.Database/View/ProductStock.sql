CREATE VIEW [dbo].[ProductStock]
	AS SELECT ProductId, SUM(ActualStock) AS ActualStock, SUM(AvailableStock) AS  AvailableStock FROM Stock with(nolock)
		GROUP BY ProductId
