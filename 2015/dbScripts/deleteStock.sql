USE [stock]
GO
alter table [dbo].[tradeAlert]
nocheck constraint FK_tradeAlert_stockCode

alter table [dbo].[priceDataSum]
nocheck constraint FK_priceDataSum_stockCode

alter table [dbo].[priceData]
nocheck constraint FK_priceData_stockCode

DELETE FROM [dbo].[stockCode]
      WHERE [dbo].[stockCode].code='AGC'

alter table [dbo].[tradeAlert]
check constraint FK_tradeAlert_stockCode

alter table [dbo].[priceDataSum]
check constraint FK_priceDataSum_stockCode

alter table [dbo].[priceData]
check constraint FK_priceData_stockCode

GO


