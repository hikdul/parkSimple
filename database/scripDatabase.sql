USE [BDParkSimple]
GO

/****** Object:  Table [dbo].[park]    Script Date: 20/11/2020 17:22:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[park](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[activo] [bit] NOT NULL,
	[costo] [int] NOT NULL,
	[fechaI] [varchar](10) NOT NULL,
	[horaI] [varchar](5) NOT NULL,
	[fechaO] [varchar](10) NULL,
	[horaO] [varchar](5) NULL,
	[foto] [image] NULL,
 CONSTRAINT [PK_park] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

