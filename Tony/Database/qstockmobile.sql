USE [master]
GO
/****** Object:  Database [QstockMobile]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE DATABASE [QstockMobile]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QstockMobile].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QstockMobile] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QstockMobile] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QstockMobile] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QstockMobile] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QstockMobile] SET ARITHABORT OFF 
GO
ALTER DATABASE [QstockMobile] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QstockMobile] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [QstockMobile] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QstockMobile] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QstockMobile] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QstockMobile] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QstockMobile] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QstockMobile] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QstockMobile] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QstockMobile] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QstockMobile] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QstockMobile] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QstockMobile] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QstockMobile] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QstockMobile] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QstockMobile] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QstockMobile] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [QstockMobile] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QstockMobile] SET RECOVERY FULL 
GO
ALTER DATABASE [QstockMobile] SET  MULTI_USER 
GO
ALTER DATABASE [QstockMobile] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QstockMobile] SET DB_CHAINING OFF 
GO
USE [QstockMobile]
GO
/****** Object:  StoredProcedure [dbo].[addStockToAlarm]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addStockToAlarm]
	@stock nvarchar(20),
	@investor nvarchar(20),
	@type nvarchar(10),
	@condition nvarchar(10),
	@value int,
	@comment nvarchar(MAX),
	@status int,
	@expiry datetime,
	@notification int
AS
DECLARE
	@investorCode nvarchar(10) = NULL
BEGIN
	SELECT @investorCode = i.code FROM dbo.investor i WHERE i.account = @investor

	IF NOT EXISTS (SELECT * FROM dbo.alarm a
					WHERE a.stockCode = @stock AND a.investor = @investor AND a.type = @type)
		BEGIN
			INSERT into dbo.alarm (stockCode, investor, type, condition, value, comment, status, expiryDate, notification)
			VALUES (@stock, @investorCode, @type, @condition, @value, @comment, @status, @expiry, @notification)
		END
END



GO
/****** Object:  StoredProcedure [dbo].[addStockToPortfolio]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addStockToPortfolio]
	@stock nvarchar(20),
	@investor nvarchar(20),
	@buyDate datetime,
	@quantity decimal(10, 0),
	@buyAmount decimal(18, 0)
AS
DECLARE 
	@portfolioCode nvarchar(10)
BEGIN
	SELECT @portfolioCode = p.code
    FROM dbo.investor i, dbo.portfolio p
    WHERE i.account = @investor
	AND p.type = 1
	AND i.code = p.investorCode

	BEGIN TRANSACTION

	INSERT into dbo.investorStock (stockCode, portfolio, buyDate, qty, buyAmt)
	VALUES (@stock, @portfolioCode, @buyDate, @quantity, @buyAmount)

	IF NOT EXISTS (SELECT * FROM dbo.investment i
					WHERE i.portfolio = @portfolioCode 
					AND i.stockCode = @stock)
		BEGIN
			INSERT into dbo.investment (stockCode, portfolio, quantity, cost)
			VALUES (@stock, @portfolioCode, @quantity, @buyAmount * @quantity)
		END
	ELSE
		BEGIN
			DECLARE @oldQuantity decimal(10, 0)
			DECLARE @oldCost decimal(18, 0)
			SET @oldQuantity = (SELECT i.quantity 
								FROM dbo.investment i
								WHERE i.portfolio = @portfolioCode
								AND i.stockCode = @stock)
			SET @oldCost = (SELECT i.cost
							FROM dbo.investment i
							WHERE i.portfolio = @portfolioCode
							AND i.stockCode = @stock)
			UPDATE dbo.investment 
			SET quantity = (@oldQuantity + @quantity), cost = (@oldCost + @buyAmount * @quantity)
			WHERE portfolio = @portfolioCode
			AND stockCode = @stock
		END

	COMMIT

END



GO
/****** Object:  StoredProcedure [dbo].[addStockToWatchlist]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addStockToWatchlist]
	@stock nvarchar(20),
	@investor nvarchar(20),
	@buyDate datetime
AS
DECLARE 
	@portfolioCode nvarchar(10) = NULL
BEGIN
	SELECT @portfolioCode = p.code
    FROM dbo.investor i, dbo.portfolio p
    WHERE i.account = @investor
	AND p.type = 2
	AND i.code = p.investorCode

	IF NOT EXISTS (SELECT * FROM dbo.investorStock i 
					WHERE i.portfolio = @portfolioCode AND i.stockCode = @stock)
		BEGIN
			INSERT into dbo.investorStock (stockCode, portfolio, buyDate, qty, buyAmt)
			VALUES (@stock, @portfolioCode, @buyDate, 0, 0)
		END

END



GO
/****** Object:  StoredProcedure [dbo].[deleteStockFromPortfolio]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[deleteStockFromPortfolio]
	@stock nvarchar(20),
	@investor nvarchar(20),
	@sellDate datetime,
	@quantity decimal(10, 0),
	@sellAmount decimal(18, 0)
AS
DECLARE 
	@portfolioCode nvarchar(10)
BEGIN
	SELECT @portfolioCode = p.code
    FROM dbo.investor i, dbo.portfolio p
    WHERE i.account = @investor
	AND p.type = 1
	AND i.code = p.investorCode

	IF NOT EXISTS (SELECT * FROM dbo.investment i
					WHERE i.portfolio = @portfolioCode 
					AND i.stockCode = @stock)
		RETURN
	ELSE
		BEGIN
			DECLARE @oldQuantity decimal(10, 0)
			DECLARE @oldCost decimal(18, 0)
			SET @oldQuantity = (SELECT i.quantity 
								FROM dbo.investment i
								WHERE i.portfolio = @portfolioCode
								AND i.stockCode = @stock)
			SET @oldCost = (SELECT i.cost
							FROM dbo.investment i
							WHERE i.portfolio = @portfolioCode
							AND i.stockCode = @stock)

			IF @quantity >= @oldQuantity
				BEGIN 
					BEGIN TRANSACTION

					DELETE FROM dbo.investment 
					WHERE portfolio = @portfolioCode
					AND stockCode = @stock

					DELETE FROM dbo.investorStock
					WHERE portfolio = @portfolioCode
					AND stockCode = @stock

					COMMIT
				END
			ELSE
				BEGIN 
					BEGIN TRANSACTION

					INSERT into dbo.investorStock (stockCode, portfolio, buyDate, qty, buyAmt)
					VALUES (@stock, @portfolioCode, @sellDate, -@quantity, @sellAmount)

					UPDATE dbo.investment 
					SET quantity = (@oldQuantity - @quantity), cost = (@oldCost - @sellAmount * @quantity)
					WHERE portfolio = @portfolioCode
					AND stockCode = @stock
				
					COMMIT
				END
		END
END


GO
/****** Object:  StoredProcedure [dbo].[getInvestment]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getInvestment]
	@investor nvarchar(20)
AS
DECLARE
	@portfolioCode nvarchar(10) = NULL
BEGIN
	SELECT @portfolioCode = p.code
    FROM dbo.investor i, dbo.portfolio p
    WHERE i.account = @investor
	AND p.type = 1
	AND i.code = p.investorCode

	SELECT i.stockCode, i.quantity, (i.cost / i.quantity) as avgCost, d.closePrice FROM dbo.investment i, dbo.priceData d
	WHERE i.portfolio = @portfolioCode
	AND i.stockCode = d.stockCode
	AND d.onDate = (select MAX(onDate) from dbo.priceData)
END



GO
/****** Object:  Table [dbo].[alarm]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[alarm](
	[stockCode] [nvarchar](20) NOT NULL,
	[investor] [nvarchar](10) NOT NULL,
	[type] [nvarchar](10) NOT NULL,
	[condition] [nvarchar](10) NOT NULL,
	[value] [int] NOT NULL,
	[comment] [nvarchar](max) NULL,
	[status] [int] NOT NULL,
	[expiryDate] [datetime] NOT NULL,
	[notification] [int] NOT NULL,
 CONSTRAINT [PK_alarm] PRIMARY KEY CLUSTERED 
(
	[stockCode] ASC,
	[investor] ASC,
	[type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[exchangeDetail]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[exchangeDetail](
	[orderId] [smallint] NOT NULL,
	[code] [nvarchar](20) NULL,
	[marketCode] [nvarchar](10) NOT NULL,
	[address] [nvarchar](255) NOT NULL,
	[goTrue] [nvarchar](20) NOT NULL,
	[goFalse] [nvarchar](20) NOT NULL,
	[notes] [nvarchar](255) NULL,
	[culture] [nchar](2) NULL,
	[isEnabled] [bit] NULL,
 CONSTRAINT [PK_exchangeDetail] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[financialCategory]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[financialCategory](
	[id] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_financialCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[financialData]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[financialData](
	[stock] [nvarchar](20) NOT NULL,
	[rubric] [int] NOT NULL,
	[time] [nchar](10) NOT NULL,
	[value] [nchar](20) NOT NULL,
 CONSTRAINT [PK_financialData] PRIMARY KEY CLUSTERED 
(
	[stock] ASC,
	[rubric] ASC,
	[time] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[financialRubric]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[financialRubric](
	[id] [int] NOT NULL,
	[id_parent] [int] NULL,
	[category] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_financialRubric] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[investment]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[investment](
	[stockCode] [nvarchar](20) NOT NULL,
	[portfolio] [nvarchar](10) NOT NULL,
	[quantity] [decimal](10, 0) NOT NULL,
	[cost] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_invesment] PRIMARY KEY CLUSTERED 
(
	[stockCode] ASC,
	[portfolio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[investor]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[investor](
	[code] [nvarchar](10) NOT NULL,
	[type] [tinyint] NOT NULL,
	[firstName] [nchar](20) NOT NULL,
	[lastName] [nchar](30) NOT NULL,
	[displayName] [nvarchar](100) NOT NULL,
	[sex] [tinyint] NOT NULL,
	[address1] [nvarchar](255) NOT NULL,
	[address2] [nvarchar](255) NULL,
	[email] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](15) NULL,
	[mobile] [nvarchar](15) NULL,
	[city] [nchar](10) NULL,
	[country] [nchar](3) NULL,
	[account] [nvarchar](20) NOT NULL,
	[password] [nvarchar](32) NOT NULL,
	[catCode] [nvarchar](10) NOT NULL,
	[expireDate] [smalldatetime] NOT NULL,
	[status] [tinyint] NOT NULL,
	[note] [ntext] NULL,
 CONSTRAINT [PK_investor] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[investorStock]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[investorStock](
	[stockCode] [nvarchar](20) NOT NULL,
	[portfolio] [nvarchar](10) NOT NULL,
	[buyDate] [smalldatetime] NOT NULL,
	[qty] [decimal](10, 0) NOT NULL,
	[buyAmt] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_investorStock] PRIMARY KEY CLUSTERED 
(
	[stockCode] ASC,
	[portfolio] ASC,
	[buyDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[messages]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[messages](
	[MsgId] [int] IDENTITY(1,1) NOT NULL,
	[type] [smallint] NOT NULL,
	[OnDate] [datetime] NOT NULL CONSTRAINT [DF_messages_OnDate]  DEFAULT (getdate()),
	[SenderId] [nvarchar](10) NOT NULL,
	[ReceiverId] [nvarchar](10) NOT NULL,
	[Category] [nvarchar](3) NOT NULL,
	[Subject] [nvarchar](255) NOT NULL,
	[MsgBody] [nvarchar](1024) NOT NULL,
	[status] [smallint] NOT NULL,
	[RefMsgId] [int] NULL,
 CONSTRAINT [PK_messages] PRIMARY KEY CLUSTERED 
(
	[MsgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[portfolio]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[portfolio](
	[type] [tinyint] NOT NULL,
	[code] [nvarchar](10) NOT NULL,
	[investorCode] [nvarchar](10) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[startCapAmt] [decimal](18, 0) NOT NULL,
	[usedCapAmt] [decimal](18, 0) NOT NULL,
	[stockAccumulatePerc] [decimal](4, 1) NOT NULL,
	[stockReducePerc] [decimal](4, 1) NOT NULL,
	[maxBuyAmtPerc] [decimal](4, 1) NOT NULL,
	[interestedStock] [ntext] NOT NULL,
	[interestedSector] [ntext] NOT NULL
)

GO
/****** Object:  Table [dbo].[portfolioDetail]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[portfolioDetail](
	[portfolio] [nvarchar](10) NOT NULL,
	[code] [nvarchar](20) NOT NULL,
	[subCode] [nvarchar](20) NOT NULL,
	[data] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_portfolioDetail] PRIMARY KEY CLUSTERED 
(
	[portfolio] ASC,
	[code] ASC,
	[subCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[priceData]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[priceData](
	[stockCode] [nvarchar](20) NOT NULL,
	[onDate] [datetime] NOT NULL,
	[openPrice] [decimal](8, 1) NOT NULL,
	[closePrice] [decimal](8, 1) NOT NULL,
	[lowPrice] [decimal](8, 1) NOT NULL,
	[highPrice] [decimal](8, 1) NOT NULL,
	[volume] [decimal](16, 0) NOT NULL,
 CONSTRAINT [PK_priceData] PRIMARY KEY CLUSTERED 
(
	[stockCode] ASC,
	[onDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[priceDataSum]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[priceDataSum](
	[type] [char](2) NOT NULL,
	[stockCode] [nvarchar](20) NOT NULL,
	[onDate] [datetime] NOT NULL,
	[openPrice] [decimal](8, 1) NOT NULL,
	[closePrice] [decimal](8, 1) NOT NULL,
	[lowPrice] [decimal](8, 1) NOT NULL,
	[highPrice] [decimal](8, 1) NOT NULL,
	[volume] [decimal](16, 0) NOT NULL,
 CONSTRAINT [PK_priceDataSum] PRIMARY KEY CLUSTERED 
(
	[type] ASC,
	[stockCode] ASC,
	[onDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stockCode]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stockCode](
	[code] [nvarchar](20) NOT NULL,
	[stockExchange] [nvarchar](10) NOT NULL,
	[tickerCode] [nvarchar](20) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[nameEn] [nvarchar](255) NULL,
	[address1] [nvarchar](255) NOT NULL,
	[address2] [nvarchar](255) NULL,
	[phone] [nvarchar](50) NOT NULL,
	[fax] [nvarchar](50) NOT NULL,
	[website] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NULL,
	[bizSectors] [nvarchar](1024) NOT NULL,
	[country] [nvarchar](2) NOT NULL,
	[regDate] [smalldatetime] NOT NULL,
	[noListedStock] [int] NOT NULL,
	[noOutstandingStock] [int] NOT NULL,
	[noForeignOwnedStock] [int] NOT NULL,
	[noTreasuryStock] [int] NOT NULL,
	[bookPrice] [decimal](18, 0) NOT NULL,
	[targetPrice] [decimal](18, 2) NOT NULL,
	[targetPriceVariant] [decimal](4, 0) NOT NULL,
	[workingCap] [decimal](18, 0) NOT NULL,
	[capitalUnit] [char](1) NOT NULL,
	[sales] [decimal](18, 0) NOT NULL,
	[profit] [decimal](18, 0) NOT NULL,
	[equity] [decimal](18, 0) NOT NULL,
	[totalDebt] [decimal](18, 0) NOT NULL,
	[totaAssets] [decimal](18, 0) NOT NULL,
	[PB] [decimal](10, 2) NOT NULL,
	[EPS] [decimal](10, 0) NOT NULL,
	[PE] [decimal](10, 2) NOT NULL,
	[ROA] [decimal](10, 2) NOT NULL,
	[ROE] [decimal](10, 2) NOT NULL,
	[BETA] [decimal](10, 2) NOT NULL,
	[status] [tinyint] NOT NULL,
 CONSTRAINT [PK_stockCode] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stockExchange]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stockExchange](
	[code] [nvarchar](10) NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[country] [nchar](3) NOT NULL,
	[workTime] [nvarchar](255) NOT NULL,
	[holidays] [nvarchar](255) NOT NULL,
	[minBuySellDay] [tinyint] NOT NULL,
	[tranFeePerc] [decimal](5, 2) NOT NULL,
	[priceRatio] [decimal](18, 0) NOT NULL,
	[volumeRatio] [decimal](18, 0) NOT NULL,
	[dataSource] [nvarchar](255) NULL,
	[weight] [tinyint] NULL,
	[priceAmplitude] [decimal](4, 2) NOT NULL,
 CONSTRAINT [PK_stockExchange] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[stockReport]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stockReport](
	[reportType] [nchar](3) NOT NULL,
	[stockCode] [nvarchar](20) NOT NULL,
	[onDate] [smalldatetime] NOT NULL,
	[reportCode] [nchar](4) NOT NULL,
	[currency] [nchar](1) NOT NULL,
	[val0] [decimal](18, 0) NOT NULL,
	[val1] [decimal](18, 0) NOT NULL,
	[val2] [decimal](18, 0) NOT NULL,
	[val3] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_companyReport] PRIMARY KEY CLUSTERED 
(
	[reportType] ASC,
	[stockCode] ASC,
	[onDate] ASC,
	[reportCode] ASC,
	[currency] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[sysAutoKey]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysAutoKey](
	[key] [nvarchar](50) NOT NULL,
	[value] [int] NULL,
 CONSTRAINT [PK_sysAutoKey] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[sysAutoKeyPending]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysAutoKeyPending](
	[key] [nvarchar](50) NOT NULL,
	[value] [nvarchar](10) NOT NULL,
	[timeStamp] [datetime] NOT NULL,
 CONSTRAINT [PK_sysAutoKeyPending] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[sysCode]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysCode](
	[category] [nvarchar](20) NOT NULL,
	[code] [nvarchar](20) NOT NULL,
	[inGroup] [nvarchar](20) NULL,
	[description1] [nvarchar](255) NOT NULL,
	[description2] [nvarchar](255) NULL,
	[weight] [int] NULL,
	[notes] [ntext] NULL,
	[tag1] [nvarchar](50) NULL,
	[tag2] [nvarchar](50) NULL,
	[isVisible] [bit] NOT NULL CONSTRAINT [DF_sysCode_isVisible]  DEFAULT ((1)),
	[isSystem] [bit] NOT NULL CONSTRAINT [DF_sysCode_isSystem]  DEFAULT ((0)),
 CONSTRAINT [PK_sysCode] PRIMARY KEY CLUSTERED 
(
	[category] ASC,
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[sysCodeCat]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysCodeCat](
	[category] [nvarchar](20) NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[notes] [nvarchar](255) NULL,
	[tag1] [nchar](50) NULL,
	[tag2] [nchar](50) NULL,
	[isVisible] [bit] NOT NULL,
	[isSystem] [bit] NOT NULL,
	[maxCodeLen] [tinyint] NULL,
	[weight] [smallint] NULL,
 CONSTRAINT [PK_sysCodeCat] PRIMARY KEY CLUSTERED 
(
	[category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[sysConfig]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysConfig](
	[key] [nvarchar](50) NOT NULL,
	[value] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_sysConfig] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[sysLog]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysLog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[onDate] [datetime] NULL CONSTRAINT [DF_sysLog_onDate]  DEFAULT (getdate()),
	[type] [tinyint] NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[source] [nvarchar](255) NULL,
	[message] [ntext] NULL,
	[investorCode] [nvarchar](10) NULL,
 CONSTRAINT [PK_sysLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[tradeAlert]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tradeAlert](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[onTime] [datetime] NOT NULL,
	[type] [nvarchar](3) NOT NULL,
	[tradeAction] [tinyint] NOT NULL,
	[portfolio] [nvarchar](10) NOT NULL,
	[stockCode] [nvarchar](20) NOT NULL,
	[timeScale] [char](2) NOT NULL,
	[strategy] [nvarchar](10) NOT NULL,
	[subject] [nvarchar](100) NOT NULL,
	[msg] [nvarchar](512) NOT NULL,
	[status] [tinyint] NOT NULL,
 CONSTRAINT [PK_message] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[transactions]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transactions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[onTime] [datetime] NOT NULL,
	[tranType] [tinyint] NOT NULL,
	[portfolio] [nvarchar](10) NOT NULL,
	[stockCode] [nvarchar](20) NOT NULL,
	[qty] [decimal](10, 0) NOT NULL,
	[amt] [decimal](18, 0) NOT NULL,
	[feeAmt] [decimal](18, 0) NOT NULL,
	[status] [tinyint] NOT NULL,
 CONSTRAINT [PK_stockTransaction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  View [dbo].[bizIndustry]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[bizIndustry]
AS
SELECT     TOP 100 PERCENT code, description1, description2, inGroup, weight
FROM         dbo.sysCode
WHERE     (category = 'BIZCODE_ICB') AND (tag1 = 'industry')
ORDER BY weight




GO
/****** Object:  View [dbo].[bizSector]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[bizSector]
AS
SELECT     TOP 100 PERCENT code, description1, description2, inGroup, weight
FROM         dbo.sysCode
WHERE     (category = 'BIZCODE_ICB') AND (tag1 = 'sector')
ORDER BY weight




GO
/****** Object:  View [dbo].[bizSubSector]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[bizSubSector]
AS
SELECT     TOP 100 PERCENT code, description1, description2, inGroup, weight
FROM         dbo.sysCode
WHERE     (category = 'BIZCODE_ICB') AND (tag1 = 'subSector')
ORDER BY weight




GO
/****** Object:  View [dbo].[bizSuperSector]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[bizSuperSector]
AS
SELECT     TOP 100 PERCENT code, description1, description2, inGroup, weight
FROM         dbo.sysCode
WHERE     (category = 'BIZCODE_ICB') AND (tag1 = 'superSector')
ORDER BY weight




GO
/****** Object:  View [dbo].[country]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[country]
AS
SELECT     TOP (100) PERCENT SUBSTRING(code, 1, 2) AS code, description1 AS description, SUBSTRING(description2, 1, 10) AS culture, weight
FROM         dbo.sysCode
WHERE     (category = 'COUNTRY')
ORDER BY weight




GO
/****** Object:  View [dbo].[currency]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[currency]
AS
SELECT     TOP 100 PERCENT SUBSTRING(code, 1,1) AS code, description1 AS description, weight
FROM         dbo.sysCode
WHERE     (category = 'currency')
ORDER BY weight





GO
/****** Object:  View [dbo].[employeeRange]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[employeeRange]
AS
SELECT     TOP 100 PERCENT SUBSTRING(code, 1, 8) AS code, description1 AS description, weight
FROM         dbo.sysCode
WHERE     (category = 'employeeRange')
ORDER BY weight




GO
/****** Object:  View [dbo].[feedbackCat]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[feedbackCat]
AS
SELECT     TOP (100) PERCENT SUBSTRING(code, 1, 3) AS code, description1 AS description, SUBSTRING(tag2, 1, 10) AS language, weight
FROM         dbo.sysCode
WHERE     (category = 'messageCat')
ORDER BY weight




GO
/****** Object:  View [dbo].[investorCat]    Script Date: 7/11/2017 8:54:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[investorCat]
AS
SELECT     TOP 100 PERCENT SUBSTRING(code, 1, 3) AS code, description1 AS description, weight
FROM         dbo.sysCode
WHERE     (category = 'investorCat')
ORDER BY weight





GO
INSERT [dbo].[financialCategory] ([id], [description]) VALUES (1, N'Balance Sheet')
INSERT [dbo].[financialCategory] ([id], [description]) VALUES (2, N'Income Statement')
INSERT [dbo].[financialCategory] ([id], [description]) VALUES (3, N'Cash Flow')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 2, N'2013      ', N'1.848.755           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 2, N'2014      ', N'6.755.059           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 2, N'2015      ', N'7.935.975           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 2, N'2016      ', N'3.444.826           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 3, N'2013      ', N'4.054.767           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 3, N'2014      ', N'781.665             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 3, N'2015      ', N'942.767             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 3, N'2016      ', N'3.174.321           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 4, N'2013      ', N'1.042.477           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 4, N'2014      ', N'1.065.026           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 4, N'2015      ', N'1.085.027           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 4, N'2016      ', N'1.090.133           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 5, N'2013      ', N'1.686.785           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 5, N'2014      ', N'1.845.255           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 5, N'2015      ', N'1.924.904           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 5, N'2016      ', N'2.126.217           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 6, N'2013      ', N'145.459             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 6, N'2014      ', N'123.818             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 6, N'2015      ', N'305.623             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 6, N'2016      ', N'878.849             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 7, N'2013      ', N'10.570.820          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 7, N'2014      ', N'10.570.820          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 7, N'2015      ', N'12.194.290          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 7, N'2016      ', N'10.714.350          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 9, N'2013      ', N'34                  ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 9, N'2014      ', N'40.374              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 9, N'2015      ', N'34.495              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 9, N'2016      ', N'38.349              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 10, N'2013      ', N'7.110.959           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 10, N'2014      ', N'7.168.238           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 10, N'2015      ', N'5.854.864           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 10, N'2016      ', N'5.462.594           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 11, N'2013      ', N'32.921              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 11, N'2014      ', N'36.303              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 11, N'2015      ', N'45.648              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 11, N'2016      ', N'66.481              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 12, N'2013      ', N'2.163.420           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 12, N'2014      ', N'2.263.714           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 12, N'2015      ', N'2.220.152           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 12, N'2016      ', N'1.938.641           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 13, N'2013      ', N'1.312.997           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 13, N'2014      ', N'1.288.338           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 13, N'2015      ', N'1.141.597           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 13, N'2016      ', N'895.665             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 14, N'2013      ', N'10.620.330          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 14, N'2014      ', N'11.198.780          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 14, N'2015      ', N'9.377.631           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 14, N'2016      ', N'8.478.519           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 16, N'2013      ', N'19.398.570          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 16, N'2014      ', N'21.769.600          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 16, N'2015      ', N'21.571.930          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 16, N'2016      ', N'19.192.860          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 18, N'2013      ', N'466.463             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 18, N'2014      ', N'437.368             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 18, N'2015      ', N'1.051.665           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 18, N'2016      ', N'1.042.310           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 19, N'2013      ', N'1.854.701           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 19, N'2014      ', N'1.792.272           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 19, N'2015      ', N'1.558.016           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 19, N'2016      ', N'1.876.186           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 20, N'2013      ', N'40.059              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 20, N'2014      ', N'77.000              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 20, N'2015      ', N'381.636             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 20, N'2016      ', N'490.296             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 21, N'2013      ', N'2.324.373           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 21, N'2014      ', N'3.208.201           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 21, N'2015      ', N'857.038             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 21, N'2016      ', N'803.769             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 22, N'2013      ', N'185.644             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 22, N'2014      ', N'263.921             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 22, N'2015      ', N'217.026             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 22, N'2016      ', N'265.149             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 23, N'2013      ', N'430.049             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 23, N'2014      ', N'470.125             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 23, N'2015      ', N'358.289             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 23, N'2016      ', N'375.962             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 24, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 24, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 24, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 24, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 25, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 25, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 25, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 25, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 26, N'2013      ', N'1.204.949           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 26, N'2014      ', N'1.028.237           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 26, N'2015      ', N'1.042.998           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 26, N'2016      ', N'1.288.893           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 27, N'2013      ', N'414.403             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 27, N'2014      ', N'647.718             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 27, N'2015      ', N'992.214             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 27, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 28, N'2013      ', N'7.172.400           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 28, N'2014      ', N'8.151.310           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 28, N'2015      ', N'6.759.695           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 28, N'2016      ', N'6.473.889           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 30, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 30, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 30, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 30, N'2016      ', N'0                   ')
GO
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 31, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 31, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 31, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 31, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 32, N'2013      ', N'68.467              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 32, N'2014      ', N'62.000              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 32, N'2015      ', N'65.183              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 32, N'2016      ', N'52.776              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 33, N'2013      ', N'1.670.006           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 33, N'2014      ', N'1.349.612           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 33, N'2015      ', N'1.662.116           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 33, N'2016      ', N'1.206.722           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 34, N'2013      ', N'54.710              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 34, N'2014      ', N'43.190              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 34, N'2015      ', N'15.183              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 34, N'2016      ', N'13.747              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 35, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 35, N'2014      ', N'55.656              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 35, N'2015      ', N'629                 ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 35, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 36, N'2013      ', N'55.656              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 36, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 36, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 36, N'2016      ', N'568                 ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 37, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 37, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 37, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 37, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 38, N'2013      ', N'1.443.339           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 38, N'2014      ', N'1.130.675           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 38, N'2015      ', N'747.527             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 38, N'2016      ', N'285.797             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 40, N'2013      ', N'8.615.739           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 40, N'2014      ', N'9.281.985           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 40, N'2015      ', N'7.507.222           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 40, N'2016      ', N'6.759.685           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 42, N'2013      ', N'10.037.160          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 42, N'2014      ', N'12.487.580          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 42, N'2015      ', N'14.064.670          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 42, N'2016      ', N'12.433.140          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 43, N'2013      ', N'37                  ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 43, N'2014      ', N'37                  ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 43, N'2015      ', N'37                  ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 43, N'2016      ', N'37                  ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 44, N'2013      ', N'10.037.200          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 44, N'2014      ', N'12.487.620          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 44, N'2015      ', N'14.064.700          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 44, N'2016      ', N'12.433.180          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 46, N'2013      ', N'745.639             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 46, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 46, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 46, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 48, N'2013      ', N'19.398.570          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 48, N'2014      ', N'21.769.600          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 48, N'2015      ', N'21.571.930          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'SAB', 48, N'2016      ', N'19.192.860          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 2, N'2013      ', N'2.745.646           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 2, N'2014      ', N'1.527.875           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 2, N'2015      ', N'1.358.683           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 2, N'2016      ', N'655.423             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 3, N'2013      ', N'4.167.318           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 3, N'2014      ', N'7.469.007           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 3, N'2015      ', N'8.668.378           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 3, N'2016      ', N'10.453.750          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 4, N'2013      ', N'2.728.422           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 4, N'2014      ', N'2.777.100           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 4, N'2015      ', N'2.685.469           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 4, N'2016      ', N'2.866.684           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 5, N'2013      ', N'3.217.483           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 5, N'2014      ', N'3.554.824           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 5, N'2015      ', N'3.810.095           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 5, N'2016      ', N'4.521.767           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 6, N'2013      ', N'160.063             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 6, N'2014      ', N'129.185             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 6, N'2015      ', N'209.251             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 6, N'2016      ', N'176.205             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 7, N'2013      ', N'15.457.990          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 7, N'2014      ', N'15.457.990          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 7, N'2015      ', N'16.731.880          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 7, N'2016      ', N'18.673.830          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 9, N'2013      ', N'737                 ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 9, N'2014      ', N'21.966              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 9, N'2015      ', N'20.898              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 9, N'2016      ', N'21.855              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 10, N'2013      ', N'8.918.417           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 10, N'2014      ', N'8.086.396           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 10, N'2015      ', N'8.214.135           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 10, N'2016      ', N'8.321.053           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 11, N'2013      ', N'149.446             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 11, N'2014      ', N'147.726             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 11, N'2015      ', N'142.368             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 11, N'2016      ', N'136.973             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 12, N'2013      ', N'318.308             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 12, N'2014      ', N'671.340             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 12, N'2015      ', N'940.365             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 12, N'2016      ', N'613.807             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 13, N'2013      ', N'295.113             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 13, N'2014      ', N'495.005             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 13, N'2015      ', N'584.855             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 13, N'2016      ', N'618.029             ')
GO
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 14, N'2013      ', N'9.856.484           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 14, N'2014      ', N'10.312.150          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 14, N'2015      ', N'10.746.300          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 14, N'2016      ', N'10.704.830          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 16, N'2013      ', N'22.875.410          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 16, N'2014      ', N'25.770.140          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 16, N'2015      ', N'27.478.180          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 16, N'2016      ', N'29.378.660          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 18, N'2013      ', N'178.944             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 18, N'2014      ', N'1.279.525           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 18, N'2015      ', N'1.475.359           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 18, N'2016      ', N'1.332.666           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 19, N'2013      ', N'1.968.257           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 19, N'2014      ', N'1.898.529           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 19, N'2015      ', N'2.193.603           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 19, N'2016      ', N'2.561.910           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 20, N'2013      ', N'20.929              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 20, N'2014      ', N'17.826              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 20, N'2015      ', N'19.882              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 20, N'2016      ', N'35.952              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 21, N'2013      ', N'456.726             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 21, N'2014      ', N'502.643             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 21, N'2015      ', N'215.808             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 21, N'2016      ', N'255.510             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 22, N'2013      ', N'137.540             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 22, N'2014      ', N'163.477             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 22, N'2015      ', N'452.476             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 22, N'2016      ', N'192.349             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 23, N'2013      ', N'490.761             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 23, N'2014      ', N'632.991             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 23, N'2015      ', N'593.486             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 23, N'2016      ', N'1.025.975           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 24, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 24, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 24, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 24, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 25, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 25, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 25, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 25, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 26, N'2013      ', N'1.341.763           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 26, N'2014      ', N'598.429             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 26, N'2015      ', N'644.468             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 26, N'2016      ', N'592.100             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 27, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 27, N'2014      ', N'4.123               ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 27, N'2015      ', N'2.420               ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 27, N'2016      ', N'890                 ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 28, N'2013      ', N'4.956.398           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 28, N'2014      ', N'5.453.281           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 28, N'2015      ', N'6.004.317           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 28, N'2016      ', N'6.457.498           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 30, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 30, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 30, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 30, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 31, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 31, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 31, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 31, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 32, N'2013      ', N'5.036               ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 32, N'2014      ', N'8.193               ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 32, N'2015      ', N'2.815               ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 32, N'2016      ', N'589                 ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 33, N'2013      ', N'363.087             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 33, N'2014      ', N'1.625.909           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 33, N'2015      ', N'1.843.529           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 33, N'2016      ', N'1.659.637           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 34, N'2013      ', N'91.066              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 34, N'2014      ', N'84.711              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 34, N'2015      ', N'89.034              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 34, N'2016      ', N'90.026              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 35, N'2013      ', N'69.583              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 35, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 35, N'2015      ', N'87.326              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 35, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 36, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 36, N'2014      ', N'77.334              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 36, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 36, N'2016      ', N'95.961              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 37, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 37, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 37, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 37, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 38, N'2013      ', N'350.663             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 38, N'2014      ', N'516.621             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 38, N'2015      ', N'549.943             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 38, N'2016      ', N'515.209             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 40, N'2013      ', N'5.307.061           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 40, N'2014      ', N'5.969.902           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 40, N'2015      ', N'6.554.260           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 40, N'2016      ', N'6.972.707           ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 42, N'2013      ', N'17.545.490          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 42, N'2014      ', N'19.680.280          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 42, N'2015      ', N'20.923.920          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 42, N'2016      ', N'22.405.950          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 43, N'2013      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 43, N'2014      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 43, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 43, N'2016      ', N'0                   ')
GO
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 44, N'2013      ', N'17.545.490          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 44, N'2014      ', N'19.680.280          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 44, N'2015      ', N'20.923.920          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 44, N'2016      ', N'22.405.950          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 46, N'2013      ', N'22.864              ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 46, N'2014      ', N'119.954             ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 46, N'2015      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 46, N'2016      ', N'0                   ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 48, N'2013      ', N'22.875.410          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 48, N'2014      ', N'25.770.140          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 48, N'2015      ', N'27.478.180          ')
INSERT [dbo].[financialData] ([stock], [rubric], [time], [value]) VALUES (N'VNM', 48, N'2016      ', N'29.378.660          ')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (2, 7, 1, N'Cash and cash equivalents')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (3, 7, 1, N'Short-term investments')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (4, 7, 1, N'Accounts receivable')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (5, 7, 1, N'Inventories')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (6, 7, 1, N'Other current assets')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (7, NULL, 1, N'CURRENT ASSETS')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (9, 14, 1, N'Long-term trade receivables')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (10, 14, 1, N'Fixed assets')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (11, 14, 1, N'Investment properties')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (12, 14, 1, N'Long-term investments')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (13, 14, 1, N'Other long-term assets')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (14, NULL, 1, N'LONG-TERM ASSETS')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (16, NULL, 1, N'TOTAL ASSETS')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (18, 28, 1, N'Short-term borrowings')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (19, 28, 1, N'Trade accounts payable')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (20, 28, 1, N'Advances from customers')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (21, 28, 1, N'Taxes and other payable to State Budget')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (22, 28, 1, N'Payable to employees')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (23, 28, 1, N'Accrued expenses')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (24, 28, 1, N'Intercompany payables')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (25, 28, 1, N'Construction contract in progress payables')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (26, 28, 1, N'Other payables')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (27, 28, 1, N'Provision for ST liabilities')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (28, NULL, 1, N'Current liabilities')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (30, 38, 1, N'Long-term trade payables')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (31, 38, 1, N'Long-term intercompany payables')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (32, 38, 1, N'Other long-term payables')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (33, 38, 1, N'Long-term borrowings')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (34, 38, 1, N'Deferred income tax liabilities')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (35, 38, 1, N'Provision for severance allowances')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (36, 38, 1, N'Provision for long-term liabilities')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (37, 38, 1, N'Technical provision (for Insurance entities)')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (38, NULL, 1, N'Long-term liabilities')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (40, NULL, 1, N'LIABILITIES')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (42, 44, 1, N'Capital and researves')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (43, 44, 1, N'Budget sources and other funds')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (44, NULL, 1, N'OWNERS EQUITY')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (46, NULL, 1, N'MINORITY INTERESTS')
INSERT [dbo].[financialRubric] ([id], [id_parent], [category], [description]) VALUES (48, NULL, 1, N'TOTAL RESOURCES')
INSERT [dbo].[investment] ([stockCode], [portfolio], [quantity], [cost]) VALUES (N'HPG', N'1', CAST(2000 AS Decimal(10, 0)), CAST(63000000 AS Decimal(18, 0)))
INSERT [dbo].[investment] ([stockCode], [portfolio], [quantity], [cost]) VALUES (N'HSG', N'3', CAST(2000 AS Decimal(10, 0)), CAST(100000000 AS Decimal(18, 0)))
INSERT [dbo].[investment] ([stockCode], [portfolio], [quantity], [cost]) VALUES (N'SAB', N'3', CAST(2000 AS Decimal(10, 0)), CAST(399000000 AS Decimal(18, 0)))
INSERT [dbo].[investment] ([stockCode], [portfolio], [quantity], [cost]) VALUES (N'VNM', N'1', CAST(2000 AS Decimal(10, 0)), CAST(310000000 AS Decimal(18, 0)))
INSERT [dbo].[investor] ([code], [type], [firstName], [lastName], [displayName], [sex], [address1], [address2], [email], [phone], [mobile], [city], [country], [account], [password], [catCode], [expireDate], [status], [note]) VALUES (N'0', 0, N'Ang Tony            ', N'Vincent                       ', N'Ang Tony Vincent', 0, N'Hồ Chí Minh', N'Tây Ninh', N'angtonyvincent@gmail.com', NULL, NULL, NULL, NULL, N'test', N'1234', N'TEST', CAST(N'2021-01-01 00:00:00' AS SmallDateTime), 0, NULL)
INSERT [dbo].[investor] ([code], [type], [firstName], [lastName], [displayName], [sex], [address1], [address2], [email], [phone], [mobile], [city], [country], [account], [password], [catCode], [expireDate], [status], [note]) VALUES (N'1', 1, N'Thái Toàn           ', N'Tô                            ', N'Tô Thái Toàn', 1, N'Hà Nội', N'Hải Phòng', N'angtonyvincent@yahoo.com.vn', NULL, NULL, NULL, NULL, N'qwerty', N'123456', N'TEST', CAST(N'2011-01-01 00:00:00' AS SmallDateTime), 1, NULL)
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'HPG', N'1', CAST(N'2017-07-07 18:26:00' AS SmallDateTime), CAST(1000 AS Decimal(10, 0)), CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'HPG', N'1', CAST(N'2017-07-07 21:09:00' AS SmallDateTime), CAST(1000 AS Decimal(10, 0)), CAST(33000 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'HPG', N'4', CAST(N'2017-07-10 12:05:00' AS SmallDateTime), CAST(0 AS Decimal(10, 0)), CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'HSG', N'2', CAST(N'2017-07-11 01:51:00' AS SmallDateTime), CAST(0 AS Decimal(10, 0)), CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'HSG', N'3', CAST(N'2017-07-07 18:27:00' AS SmallDateTime), CAST(1000 AS Decimal(10, 0)), CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'HSG', N'3', CAST(N'2017-07-07 21:09:00' AS SmallDateTime), CAST(1000 AS Decimal(10, 0)), CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'SAB', N'2', CAST(N'2017-07-11 01:51:00' AS SmallDateTime), CAST(0 AS Decimal(10, 0)), CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'SAB', N'3', CAST(N'2017-07-07 18:28:00' AS SmallDateTime), CAST(1000 AS Decimal(10, 0)), CAST(199000 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'SAB', N'3', CAST(N'2017-07-10 12:06:00' AS SmallDateTime), CAST(1000 AS Decimal(10, 0)), CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'VNM', N'1', CAST(N'2017-07-07 18:27:00' AS SmallDateTime), CAST(1000 AS Decimal(10, 0)), CAST(155000 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'VNM', N'1', CAST(N'2017-07-10 10:25:00' AS SmallDateTime), CAST(1000 AS Decimal(10, 0)), CAST(155000 AS Decimal(18, 0)))
INSERT [dbo].[investorStock] ([stockCode], [portfolio], [buyDate], [qty], [buyAmt]) VALUES (N'VNM', N'4', CAST(N'2017-07-10 12:06:00' AS SmallDateTime), CAST(0 AS Decimal(10, 0)), CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[portfolio] ([type], [code], [investorCode], [name], [description], [startCapAmt], [usedCapAmt], [stockAccumulatePerc], [stockReducePerc], [maxBuyAmtPerc], [interestedStock], [interestedSector]) VALUES (1, N'1', N'0', N'Danh mục đầu tư', N'Danh mục đầu tư của Tony', CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.0 AS Decimal(4, 1)), CAST(0.0 AS Decimal(4, 1)), CAST(0.0 AS Decimal(4, 1)), N'/HPG/', N'/THEP/')
INSERT [dbo].[portfolio] ([type], [code], [investorCode], [name], [description], [startCapAmt], [usedCapAmt], [stockAccumulatePerc], [stockReducePerc], [maxBuyAmtPerc], [interestedStock], [interestedSector]) VALUES (2, N'2', N'0', N'Danh sách theo dõi', N'Danh sách theo dõi của Tony', CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.0 AS Decimal(4, 1)), CAST(0.0 AS Decimal(4, 1)), CAST(0.0 AS Decimal(4, 1)), N'/HSG/', N'/THEP/')
INSERT [dbo].[portfolio] ([type], [code], [investorCode], [name], [description], [startCapAmt], [usedCapAmt], [stockAccumulatePerc], [stockReducePerc], [maxBuyAmtPerc], [interestedStock], [interestedSector]) VALUES (1, N'3', N'1', N'My portfolio', N'Tony''s portfolio', CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.0 AS Decimal(4, 1)), CAST(0.0 AS Decimal(4, 1)), CAST(0.0 AS Decimal(4, 1)), N'/VNM/', N'/SUA/')
INSERT [dbo].[portfolio] ([type], [code], [investorCode], [name], [description], [startCapAmt], [usedCapAmt], [stockAccumulatePerc], [stockReducePerc], [maxBuyAmtPerc], [interestedStock], [interestedSector]) VALUES (2, N'4', N'1', N'My watchlist', N'Tony''s watchlist', CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.0 AS Decimal(4, 1)), CAST(0.0 AS Decimal(4, 1)), CAST(0.0 AS Decimal(4, 1)), N'/SAB/', N'/NUOC/')
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'HPG', CAST(N'2017-05-10 00:00:00.000' AS DateTime), CAST(31000.0 AS Decimal(8, 1)), CAST(29000.0 AS Decimal(8, 1)), CAST(29000.0 AS Decimal(8, 1)), CAST(31000.0 AS Decimal(8, 1)), CAST(1000 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'HPG', CAST(N'2017-05-20 00:00:00.000' AS DateTime), CAST(30000.0 AS Decimal(8, 1)), CAST(30000.0 AS Decimal(8, 1)), CAST(30000.0 AS Decimal(8, 1)), CAST(30000.0 AS Decimal(8, 1)), CAST(2000 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'HPG', CAST(N'2017-05-30 00:00:00.000' AS DateTime), CAST(31000.0 AS Decimal(8, 1)), CAST(29000.0 AS Decimal(8, 1)), CAST(29000.0 AS Decimal(8, 1)), CAST(31000.0 AS Decimal(8, 1)), CAST(3000 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'HPG', CAST(N'2017-06-01 00:00:00.000' AS DateTime), CAST(30000.0 AS Decimal(8, 1)), CAST(30000.0 AS Decimal(8, 1)), CAST(30000.0 AS Decimal(8, 1)), CAST(30000.0 AS Decimal(8, 1)), CAST(4000 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'HSG', CAST(N'2017-05-10 00:00:00.000' AS DateTime), CAST(51000.0 AS Decimal(8, 1)), CAST(49000.0 AS Decimal(8, 1)), CAST(49000.0 AS Decimal(8, 1)), CAST(51000.0 AS Decimal(8, 1)), CAST(1000 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'HSG', CAST(N'2017-05-20 00:00:00.000' AS DateTime), CAST(50000.0 AS Decimal(8, 1)), CAST(50000.0 AS Decimal(8, 1)), CAST(50000.0 AS Decimal(8, 1)), CAST(50000.0 AS Decimal(8, 1)), CAST(2000 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'HSG', CAST(N'2017-05-30 00:00:00.000' AS DateTime), CAST(51000.0 AS Decimal(8, 1)), CAST(49000.0 AS Decimal(8, 1)), CAST(49000.0 AS Decimal(8, 1)), CAST(51000.0 AS Decimal(8, 1)), CAST(3000 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'HSG', CAST(N'2017-06-01 00:00:00.000' AS DateTime), CAST(50000.0 AS Decimal(8, 1)), CAST(50000.0 AS Decimal(8, 1)), CAST(50000.0 AS Decimal(8, 1)), CAST(50000.0 AS Decimal(8, 1)), CAST(4000 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'SAB', CAST(N'2017-05-10 00:00:00.000' AS DateTime), CAST(201000.0 AS Decimal(8, 1)), CAST(199000.0 AS Decimal(8, 1)), CAST(199000.0 AS Decimal(8, 1)), CAST(201000.0 AS Decimal(8, 1)), CAST(100 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'SAB', CAST(N'2017-05-20 00:00:00.000' AS DateTime), CAST(200000.0 AS Decimal(8, 1)), CAST(200000.0 AS Decimal(8, 1)), CAST(200000.0 AS Decimal(8, 1)), CAST(200000.0 AS Decimal(8, 1)), CAST(200 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'SAB', CAST(N'2017-05-30 00:00:00.000' AS DateTime), CAST(200000.0 AS Decimal(8, 1)), CAST(200000.0 AS Decimal(8, 1)), CAST(200000.0 AS Decimal(8, 1)), CAST(200000.0 AS Decimal(8, 1)), CAST(300 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'SAB', CAST(N'2017-06-01 00:00:00.000' AS DateTime), CAST(199000.0 AS Decimal(8, 1)), CAST(201000.0 AS Decimal(8, 1)), CAST(199000.0 AS Decimal(8, 1)), CAST(201000.0 AS Decimal(8, 1)), CAST(400 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'VNM', CAST(N'2017-05-10 00:00:00.000' AS DateTime), CAST(149000.0 AS Decimal(8, 1)), CAST(151000.0 AS Decimal(8, 1)), CAST(149000.0 AS Decimal(8, 1)), CAST(151000.0 AS Decimal(8, 1)), CAST(400 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'VNM', CAST(N'2017-05-20 00:00:00.000' AS DateTime), CAST(150000.0 AS Decimal(8, 1)), CAST(150000.0 AS Decimal(8, 1)), CAST(150000.0 AS Decimal(8, 1)), CAST(150000.0 AS Decimal(8, 1)), CAST(300 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'VNM', CAST(N'2017-05-30 00:00:00.000' AS DateTime), CAST(149000.0 AS Decimal(8, 1)), CAST(151000.0 AS Decimal(8, 1)), CAST(149000.0 AS Decimal(8, 1)), CAST(151000.0 AS Decimal(8, 1)), CAST(200 AS Decimal(16, 0)))
INSERT [dbo].[priceData] ([stockCode], [onDate], [openPrice], [closePrice], [lowPrice], [highPrice], [volume]) VALUES (N'VNM', CAST(N'2017-06-01 00:00:00.000' AS DateTime), CAST(150000.0 AS Decimal(8, 1)), CAST(150000.0 AS Decimal(8, 1)), CAST(150000.0 AS Decimal(8, 1)), CAST(150000.0 AS Decimal(8, 1)), CAST(100 AS Decimal(16, 0)))
INSERT [dbo].[stockCode] ([code], [stockExchange], [tickerCode], [name], [nameEn], [address1], [address2], [phone], [fax], [website], [email], [bizSectors], [country], [regDate], [noListedStock], [noOutstandingStock], [noForeignOwnedStock], [noTreasuryStock], [bookPrice], [targetPrice], [targetPriceVariant], [workingCap], [capitalUnit], [sales], [profit], [equity], [totalDebt], [totaAssets], [PB], [EPS], [PE], [ROA], [ROE], [BETA], [status]) VALUES (N'HPG', N'HOSE', N'', N'Hòa Phát Group', NULL, N'Hà Nội', NULL, N'0', N'0', N'hpg.vn', NULL, N'/THEP/', N'VN', CAST(N'2000-01-01 00:00:00' AS SmallDateTime), 0, 0, 0, 0, CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Decimal(4, 0)), CAST(0 AS Decimal(18, 0)), N'0', CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(10, 2)), CAST(0 AS Decimal(10, 0)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[stockCode] ([code], [stockExchange], [tickerCode], [name], [nameEn], [address1], [address2], [phone], [fax], [website], [email], [bizSectors], [country], [regDate], [noListedStock], [noOutstandingStock], [noForeignOwnedStock], [noTreasuryStock], [bookPrice], [targetPrice], [targetPriceVariant], [workingCap], [capitalUnit], [sales], [profit], [equity], [totalDebt], [totaAssets], [PB], [EPS], [PE], [ROA], [ROE], [BETA], [status]) VALUES (N'HSG', N'HOSE', N'', N'Hoa Sen Group', NULL, N'Hà Nội', NULL, N'0', N'0', N'hsg.vn', NULL, N'/THEP/', N'VN', CAST(N'2000-01-01 00:00:00' AS SmallDateTime), 0, 0, 0, 0, CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Decimal(4, 0)), CAST(0 AS Decimal(18, 0)), N'0', CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(10, 2)), CAST(0 AS Decimal(10, 0)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[stockCode] ([code], [stockExchange], [tickerCode], [name], [nameEn], [address1], [address2], [phone], [fax], [website], [email], [bizSectors], [country], [regDate], [noListedStock], [noOutstandingStock], [noForeignOwnedStock], [noTreasuryStock], [bookPrice], [targetPrice], [targetPriceVariant], [workingCap], [capitalUnit], [sales], [profit], [equity], [totalDebt], [totaAssets], [PB], [EPS], [PE], [ROA], [ROE], [BETA], [status]) VALUES (N'SAB', N'HOSE', N' ', N'Bia rượu Sài Gòn', NULL, N'Hồ Chí Minh', NULL, N'0', N'0', N'sab.vn', NULL, N'/NUOC/', N'VN', CAST(N'2000-01-01 00:00:00' AS SmallDateTime), 0, 0, 0, 0, CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Decimal(4, 0)), CAST(0 AS Decimal(18, 0)), N'0', CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(10, 2)), CAST(0 AS Decimal(10, 0)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[stockCode] ([code], [stockExchange], [tickerCode], [name], [nameEn], [address1], [address2], [phone], [fax], [website], [email], [bizSectors], [country], [regDate], [noListedStock], [noOutstandingStock], [noForeignOwnedStock], [noTreasuryStock], [bookPrice], [targetPrice], [targetPriceVariant], [workingCap], [capitalUnit], [sales], [profit], [equity], [totalDebt], [totaAssets], [PB], [EPS], [PE], [ROA], [ROE], [BETA], [status]) VALUES (N'VNM', N'HOSE', N'', N'Sữa Việt Nam', NULL, N'Hồ Chí Minh', NULL, N'0', N'0', N'vmn.vn', NULL, N'/SUA/', N'VN', CAST(N'2000-01-01 00:00:00' AS SmallDateTime), 0, 0, 0, 0, CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(18, 2)), CAST(0 AS Decimal(4, 0)), CAST(0 AS Decimal(18, 0)), N'0', CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(10, 2)), CAST(0 AS Decimal(10, 0)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[stockExchange] ([code], [description], [country], [workTime], [holidays], [minBuySellDay], [tranFeePerc], [priceRatio], [volumeRatio], [dataSource], [weight], [priceAmplitude]) VALUES (N'HOSE', N'Sàn giao dịch thành phố Hồ Chí Minh', N'VN ', N'Từ thứ 2 đến thứ 6', N'Thứ 7, chủ nhật và các ngày lễ', 0, CAST(0.00 AS Decimal(5, 2)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL, CAST(0.00 AS Decimal(4, 2)))
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_investor]    Script Date: 7/11/2017 8:54:25 AM ******/
ALTER TABLE [dbo].[investor] ADD  CONSTRAINT [IX_investor] UNIQUE NONCLUSTERED 
(
	[account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [PK_portpolio]    Script Date: 7/11/2017 8:54:25 AM ******/
ALTER TABLE [dbo].[portfolio] ADD  CONSTRAINT [PK_portpolio] PRIMARY KEY NONCLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [onDate]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE NONCLUSTERED INDEX [onDate] ON [dbo].[priceData]
(
	[onDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [stockCode]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE NONCLUSTERED INDEX [stockCode] ON [dbo].[priceData]
(
	[stockCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index_priceDataSum_49_1058818834__K1_K3_K2_4]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE NONCLUSTERED INDEX [_dta_index_priceDataSum_49_1058818834__K1_K3_K2_4] ON [dbo].[priceDataSum]
(
	[type] ASC,
	[onDate] ASC,
	[stockCode] ASC
)
INCLUDE ( 	[openPrice]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index_priceDataSum_49_1058818834__K1_K3_K2_5]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE NONCLUSTERED INDEX [_dta_index_priceDataSum_49_1058818834__K1_K3_K2_5] ON [dbo].[priceDataSum]
(
	[type] ASC,
	[onDate] ASC,
	[stockCode] ASC
)
INCLUDE ( 	[closePrice]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index_priceDataSum_49_1058818834__K2_K1_K3_4_5_6_7_8]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE NONCLUSTERED INDEX [_dta_index_priceDataSum_49_1058818834__K2_K1_K3_4_5_6_7_8] ON [dbo].[priceDataSum]
(
	[stockCode] ASC,
	[type] ASC,
	[onDate] ASC
)
INCLUDE ( 	[openPrice],
	[closePrice],
	[lowPrice],
	[highPrice],
	[volume]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index_priceDataSum_7_1058818834__K2_K1_K3_4_5_6_7_8]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE NONCLUSTERED INDEX [_dta_index_priceDataSum_7_1058818834__K2_K1_K3_4_5_6_7_8] ON [dbo].[priceDataSum]
(
	[stockCode] ASC,
	[type] ASC,
	[onDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [onDate]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE NONCLUSTERED INDEX [onDate] ON [dbo].[priceDataSum]
(
	[onDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [exchange]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE NONCLUSTERED INDEX [exchange] ON [dbo].[stockCode]
(
	[stockExchange] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [onTime]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE NONCLUSTERED INDEX [onTime] ON [dbo].[tradeAlert]
(
	[onTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [porfolio]    Script Date: 7/11/2017 8:54:25 AM ******/
CREATE NONCLUSTERED INDEX [porfolio] ON [dbo].[tradeAlert]
(
	[portfolio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE [dbo].[alarm]  WITH CHECK ADD  CONSTRAINT [FK_alarm_investor] FOREIGN KEY([investor])
REFERENCES [dbo].[investor] ([code])
GO
ALTER TABLE [dbo].[alarm] CHECK CONSTRAINT [FK_alarm_investor]
GO
ALTER TABLE [dbo].[alarm]  WITH CHECK ADD  CONSTRAINT [FK_alarm_stockCode] FOREIGN KEY([stockCode])
REFERENCES [dbo].[stockCode] ([code])
GO
ALTER TABLE [dbo].[alarm] CHECK CONSTRAINT [FK_alarm_stockCode]
GO
ALTER TABLE [dbo].[financialData]  WITH CHECK ADD  CONSTRAINT [FK_financialData_financialRubric] FOREIGN KEY([rubric])
REFERENCES [dbo].[financialRubric] ([id])
GO
ALTER TABLE [dbo].[financialData] CHECK CONSTRAINT [FK_financialData_financialRubric]
GO
ALTER TABLE [dbo].[financialData]  WITH CHECK ADD  CONSTRAINT [FK_financialData_stockCode] FOREIGN KEY([stock])
REFERENCES [dbo].[stockCode] ([code])
GO
ALTER TABLE [dbo].[financialData] CHECK CONSTRAINT [FK_financialData_stockCode]
GO
ALTER TABLE [dbo].[financialRubric]  WITH CHECK ADD  CONSTRAINT [FK_financialRubric_financialCategory] FOREIGN KEY([category])
REFERENCES [dbo].[financialCategory] ([id])
GO
ALTER TABLE [dbo].[financialRubric] CHECK CONSTRAINT [FK_financialRubric_financialCategory]
GO
ALTER TABLE [dbo].[investment]  WITH CHECK ADD  CONSTRAINT [FK_invesment_portfolio] FOREIGN KEY([portfolio])
REFERENCES [dbo].[portfolio] ([code])
GO
ALTER TABLE [dbo].[investment] CHECK CONSTRAINT [FK_invesment_portfolio]
GO
ALTER TABLE [dbo].[investment]  WITH CHECK ADD  CONSTRAINT [FK_invesment_stockCode] FOREIGN KEY([stockCode])
REFERENCES [dbo].[stockCode] ([code])
GO
ALTER TABLE [dbo].[investment] CHECK CONSTRAINT [FK_invesment_stockCode]
GO
ALTER TABLE [dbo].[investorStock]  WITH NOCHECK ADD  CONSTRAINT [FK_investorStock_portpolio] FOREIGN KEY([portfolio])
REFERENCES [dbo].[portfolio] ([code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[investorStock] CHECK CONSTRAINT [FK_investorStock_portpolio]
GO
ALTER TABLE [dbo].[investorStock]  WITH NOCHECK ADD  CONSTRAINT [FK_investorStock_stockCode] FOREIGN KEY([stockCode])
REFERENCES [dbo].[stockCode] ([code])
GO
ALTER TABLE [dbo].[investorStock] CHECK CONSTRAINT [FK_investorStock_stockCode]
GO
ALTER TABLE [dbo].[portfolio]  WITH NOCHECK ADD  CONSTRAINT [FK_portpolio_investor] FOREIGN KEY([investorCode])
REFERENCES [dbo].[investor] ([code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[portfolio] CHECK CONSTRAINT [FK_portpolio_investor]
GO
ALTER TABLE [dbo].[portfolioDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_portfolioDetail_portfolio] FOREIGN KEY([portfolio])
REFERENCES [dbo].[portfolio] ([code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[portfolioDetail] CHECK CONSTRAINT [FK_portfolioDetail_portfolio]
GO
ALTER TABLE [dbo].[priceData]  WITH NOCHECK ADD  CONSTRAINT [FK_priceData_stockCode] FOREIGN KEY([stockCode])
REFERENCES [dbo].[stockCode] ([code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[priceData] CHECK CONSTRAINT [FK_priceData_stockCode]
GO
ALTER TABLE [dbo].[priceDataSum]  WITH CHECK ADD  CONSTRAINT [FK_priceDataSum_stockCode] FOREIGN KEY([stockCode])
REFERENCES [dbo].[stockCode] ([code])
GO
ALTER TABLE [dbo].[priceDataSum] CHECK CONSTRAINT [FK_priceDataSum_stockCode]
GO
ALTER TABLE [dbo].[stockCode]  WITH NOCHECK ADD  CONSTRAINT [FK_stockCode_stockExchange] FOREIGN KEY([stockExchange])
REFERENCES [dbo].[stockExchange] ([code])
GO
ALTER TABLE [dbo].[stockCode] CHECK CONSTRAINT [FK_stockCode_stockExchange]
GO
ALTER TABLE [dbo].[stockReport]  WITH CHECK ADD  CONSTRAINT [FK_stockReport_stockCode] FOREIGN KEY([stockCode])
REFERENCES [dbo].[stockCode] ([code])
GO
ALTER TABLE [dbo].[stockReport] CHECK CONSTRAINT [FK_stockReport_stockCode]
GO
ALTER TABLE [dbo].[sysCode]  WITH NOCHECK ADD  CONSTRAINT [FK_sysCode_sysCodeCat] FOREIGN KEY([category])
REFERENCES [dbo].[sysCodeCat] ([category])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[sysCode] CHECK CONSTRAINT [FK_sysCode_sysCodeCat]
GO
ALTER TABLE [dbo].[tradeAlert]  WITH NOCHECK ADD  CONSTRAINT [FK_tradeAlert_portpolio] FOREIGN KEY([portfolio])
REFERENCES [dbo].[portfolio] ([code])
GO
ALTER TABLE [dbo].[tradeAlert] CHECK CONSTRAINT [FK_tradeAlert_portpolio]
GO
ALTER TABLE [dbo].[tradeAlert]  WITH NOCHECK ADD  CONSTRAINT [FK_tradeAlert_stockCode] FOREIGN KEY([stockCode])
REFERENCES [dbo].[stockCode] ([code])
GO
ALTER TABLE [dbo].[tradeAlert] CHECK CONSTRAINT [FK_tradeAlert_stockCode]
GO
ALTER TABLE [dbo].[transactions]  WITH NOCHECK ADD  CONSTRAINT [FK_investorTransHistory_portpolio] FOREIGN KEY([portfolio])
REFERENCES [dbo].[portfolio] ([code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[transactions] CHECK CONSTRAINT [FK_investorTransHistory_portpolio]
GO
ALTER TABLE [dbo].[transactions]  WITH NOCHECK ADD  CONSTRAINT [FK_investorTransHistory_stockCode] FOREIGN KEY([stockCode])
REFERENCES [dbo].[stockCode] ([code])
GO
ALTER TABLE [dbo].[transactions] CHECK CONSTRAINT [FK_investorTransHistory_stockCode]
GO
USE [master]
GO
ALTER DATABASE [QstockMobile] SET  READ_WRITE 
GO
