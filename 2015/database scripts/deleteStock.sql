USE [stock]
GO
DECLARE @code nvarchar(20);  
SET @code='HBD';

DELETE FROM [dbo].[tradeAlert]
      WHERE [dbo].[tradeAlert].stockCode=@code

DELETE FROM [dbo].[priceDataSum]
      WHERE [dbo].[priceDataSum].stockCode=@code


DELETE FROM [dbo].[priceData]
      WHERE [dbo].[priceData].stockCode=@code
	  
DELETE FROM [dbo].transactions
WHERE [dbo].transactions.stockCode=@code

DELETE FROM [dbo].investorStock
WHERE [dbo].investorStock.stockCode=@code

DELETE FROM [dbo].[stockCode]
      WHERE [dbo].[stockCode].code=@code



