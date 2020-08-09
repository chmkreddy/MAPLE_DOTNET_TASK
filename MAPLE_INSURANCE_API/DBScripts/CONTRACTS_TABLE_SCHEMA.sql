
/****** Object:  Table [dbo].[CONTRACTS]    Script Date: 8/9/2020 3:10:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CONTRACTS]') AND type in (N'U'))
DROP TABLE [dbo].[CONTRACTS]
GO

/****** Object:  Table [dbo].[CONTRACTS]    Script Date: 8/9/2020 3:10:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CONTRACTS](
	[ContractId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](100) NULL,
	[CustomerAddress] [varchar](500) NULL,
	[CustomerGender] [nchar](1) NULL,
	[CustomerCountry] [nchar](10) NULL,
	[CustomerDOB] [datetime] NULL,
	[SaleDate] [datetime] NULL,
	[CoveragePlan] [varchar](100) NULL,
	[NetPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ContractId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


