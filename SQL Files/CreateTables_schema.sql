USE [MMTShop]
GO

/****** Object:  Table [dbo].[Categories]    Script Date: 20/01/2021 09:47:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Categories](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FeaturedProductCategories](
	[CategoryId] [int] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Products](
	[SKU] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Description] [nvarchar](20) NULL,
	[Price] [decimal](18, 2) NOT NULL
) ON [PRIMARY]
GO


