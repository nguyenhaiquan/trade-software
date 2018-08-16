USE [stock]
GO

/****** Object:  Table [dbo].[stockScore]    Script Date: 11/1/2017 11:04:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[stockScore](
	[stockCode] [nvarchar](20) NOT NULL,
	[onDate] [date] NOT NULL,
	[scoreType] [nvarchar](20) NULL,
	[value] [int] NULL,
 CONSTRAINT [PK_stockScore] PRIMARY KEY CLUSTERED 
(
	[stockCode] ASC,
	[onDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


