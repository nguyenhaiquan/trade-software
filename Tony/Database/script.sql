USE [master]
GO
/****** Object:  Database [Stock]    Script Date: 5/24/2017 10:47:38 AM ******/
CREATE DATABASE [Stock]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Stock', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Stock.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'Stock_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Stock_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Stock] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Stock].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Stock] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Stock] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Stock] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Stock] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Stock] SET ARITHABORT OFF 
GO
ALTER DATABASE [Stock] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Stock] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Stock] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Stock] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Stock] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Stock] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Stock] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Stock] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Stock] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Stock] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Stock] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Stock] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Stock] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Stock] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Stock] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Stock] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Stock] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Stock] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Stock] SET  MULTI_USER 
GO
ALTER DATABASE [Stock] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Stock] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Stock] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Stock] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Stock] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Stock', N'ON'
GO
ALTER DATABASE [Stock] SET QUERY_STORE = OFF
GO
USE [Stock]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Stock]
GO
/****** Object:  Table [dbo].[sysCode]    Script Date: 5/24/2017 10:47:38 AM ******/
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
	[isVisible] [bit] NOT NULL,
	[isSystem] [bit] NOT NULL,
 CONSTRAINT [PK_sysCode] PRIMARY KEY CLUSTERED 
(
	[category] ASC,
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[bizIndustry]    Script Date: 5/24/2017 10:47:38 AM ******/
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
/****** Object:  View [dbo].[bizSector]    Script Date: 5/24/2017 10:47:38 AM ******/
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
/****** Object:  View [dbo].[bizSubSector]    Script Date: 5/24/2017 10:47:38 AM ******/
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
/****** Object:  View [dbo].[bizSuperSector]    Script Date: 5/24/2017 10:47:38 AM ******/
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
/****** Object:  View [dbo].[country]    Script Date: 5/24/2017 10:47:38 AM ******/
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
/****** Object:  View [dbo].[currency]    Script Date: 5/24/2017 10:47:38 AM ******/
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
/****** Object:  View [dbo].[employeeRange]    Script Date: 5/24/2017 10:47:38 AM ******/
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
/****** Object:  View [dbo].[feedbackCat]    Script Date: 5/24/2017 10:47:38 AM ******/
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
/****** Object:  View [dbo].[investorCat]    Script Date: 5/24/2017 10:47:38 AM ******/
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
/****** Object:  Table [dbo].[alarm]    Script Date: 5/24/2017 10:47:38 AM ******/
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
	[investor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[exchangeDetail]    Script Date: 5/24/2017 10:47:38 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[financialCategory]    Script Date: 5/24/2017 10:47:38 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[financialData]    Script Date: 5/24/2017 10:47:38 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[financialRubric]    Script Date: 5/24/2017 10:47:38 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[investor]    Script Date: 5/24/2017 10:47:38 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_investor] UNIQUE NONCLUSTERED 
(
	[account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[investorStock]    Script Date: 5/24/2017 10:47:38 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[messages]    Script Date: 5/24/2017 10:47:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[messages](
	[MsgId] [int] IDENTITY(1,1) NOT NULL,
	[type] [smallint] NOT NULL,
	[OnDate] [datetime] NOT NULL,
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[portfolio]    Script Date: 5/24/2017 10:47:39 AM ******/
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
	[interestedSector] [ntext] NOT NULL,
 CONSTRAINT [PK_portpolio] PRIMARY KEY NONCLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[portfolioDetail]    Script Date: 5/24/2017 10:47:39 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[priceData]    Script Date: 5/24/2017 10:47:39 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[priceDataSum]    Script Date: 5/24/2017 10:47:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stockCode]    Script Date: 5/24/2017 10:47:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stockExchange]    Script Date: 5/24/2017 10:47:39 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stockReport]    Script Date: 5/24/2017 10:47:39 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sysAutoKey]    Script Date: 5/24/2017 10:47:39 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sysAutoKeyPending]    Script Date: 5/24/2017 10:47:39 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sysCodeCat]    Script Date: 5/24/2017 10:47:39 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sysConfig]    Script Date: 5/24/2017 10:47:39 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sysLog]    Script Date: 5/24/2017 10:47:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysLog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[onDate] [datetime] NULL,
	[type] [tinyint] NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[source] [nvarchar](255) NULL,
	[message] [ntext] NULL,
	[investorCode] [nvarchar](10) NULL,
 CONSTRAINT [PK_sysLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tradeAlert]    Script Date: 5/24/2017 10:47:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[transactions]    Script Date: 5/24/2017 10:47:39 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [onDate]    Script Date: 5/24/2017 10:47:39 AM ******/
CREATE NONCLUSTERED INDEX [onDate] ON [dbo].[priceData]
(
	[onDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [stockCode]    Script Date: 5/24/2017 10:47:39 AM ******/
CREATE NONCLUSTERED INDEX [stockCode] ON [dbo].[priceData]
(
	[stockCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index_priceDataSum_49_1058818834__K1_K3_K2_4]    Script Date: 5/24/2017 10:47:39 AM ******/
CREATE NONCLUSTERED INDEX [_dta_index_priceDataSum_49_1058818834__K1_K3_K2_4] ON [dbo].[priceDataSum]
(
	[type] ASC,
	[onDate] ASC,
	[stockCode] ASC
)
INCLUDE ( 	[openPrice]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index_priceDataSum_49_1058818834__K1_K3_K2_5]    Script Date: 5/24/2017 10:47:39 AM ******/
CREATE NONCLUSTERED INDEX [_dta_index_priceDataSum_49_1058818834__K1_K3_K2_5] ON [dbo].[priceDataSum]
(
	[type] ASC,
	[onDate] ASC,
	[stockCode] ASC
)
INCLUDE ( 	[closePrice]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index_priceDataSum_49_1058818834__K2_K1_K3_4_5_6_7_8]    Script Date: 5/24/2017 10:47:39 AM ******/
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
	[volume]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [_dta_index_priceDataSum_7_1058818834__K2_K1_K3_4_5_6_7_8]    Script Date: 5/24/2017 10:47:39 AM ******/
CREATE NONCLUSTERED INDEX [_dta_index_priceDataSum_7_1058818834__K2_K1_K3_4_5_6_7_8] ON [dbo].[priceDataSum]
(
	[stockCode] ASC,
	[type] ASC,
	[onDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [onDate]    Script Date: 5/24/2017 10:47:39 AM ******/
CREATE NONCLUSTERED INDEX [onDate] ON [dbo].[priceDataSum]
(
	[onDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [exchange]    Script Date: 5/24/2017 10:47:39 AM ******/
CREATE NONCLUSTERED INDEX [exchange] ON [dbo].[stockCode]
(
	[stockExchange] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [onTime]    Script Date: 5/24/2017 10:47:39 AM ******/
CREATE NONCLUSTERED INDEX [onTime] ON [dbo].[tradeAlert]
(
	[onTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [porfolio]    Script Date: 5/24/2017 10:47:39 AM ******/
CREATE NONCLUSTERED INDEX [porfolio] ON [dbo].[tradeAlert]
(
	[portfolio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[messages] ADD  CONSTRAINT [DF_messages_OnDate]  DEFAULT (getdate()) FOR [OnDate]
GO
ALTER TABLE [dbo].[sysCode] ADD  CONSTRAINT [DF_sysCode_isVisible]  DEFAULT ((1)) FOR [isVisible]
GO
ALTER TABLE [dbo].[sysCode] ADD  CONSTRAINT [DF_sysCode_isSystem]  DEFAULT ((0)) FOR [isSystem]
GO
ALTER TABLE [dbo].[sysLog] ADD  CONSTRAINT [DF_sysLog_onDate]  DEFAULT (getdate()) FOR [onDate]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'KhoiLuongNiemYet' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'stockCode', @level2type=N'COLUMN',@level2name=N'noListedStock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'KhoiLuongLuuHanh' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'stockCode', @level2type=N'COLUMN',@level2name=N'noOutstandingStock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cô phiếu quỹ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'stockCode', @level2type=N'COLUMN',@level2name=N'noTreasuryStock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vốn lưu động' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'stockCode', @level2type=N'COLUMN',@level2name=N'workingCap'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vốn chủ sở hữu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'stockCode', @level2type=N'COLUMN',@level2name=N'equity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Price-to-Book ratio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'stockCode', @level2type=N'COLUMN',@level2name=N'PB'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Earning Per Share' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'stockCode', @level2type=N'COLUMN',@level2name=N'EPS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Prixe per Earning ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'stockCode', @level2type=N'COLUMN',@level2name=N'PE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Return on total assets' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'stockCode', @level2type=N'COLUMN',@level2name=N'ROA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Return on common equyty' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'stockCode', @level2type=N'COLUMN',@level2name=N'ROE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[18] 4[44] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "sysCode"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 141
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2715
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'country'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'country'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[8] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "sysCode"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'feedbackCat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'feedbackCat'
GO
USE [master]
GO
ALTER DATABASE [Stock] SET  READ_WRITE 
GO
