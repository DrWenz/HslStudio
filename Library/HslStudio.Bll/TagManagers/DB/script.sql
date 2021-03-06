USE [HslStudioDB]
GO
/****** Object:  Table [dbo].[tblChannel]    Script Date: 25/11/2020 01:18:01 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblChannel](
	[ChannelId] [int] NOT NULL,
	[ChannelName] [nvarchar](100) NOT NULL,
	[ConnectionType] [varchar](50) NULL,
	[ChannelAddress] [varchar](50) NULL,
	[ChannelTypes] [varchar](50) NULL,
	[CPU] [varchar](50) NULL,
	[Mode] [varchar](50) NULL,
	[IsActive] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_ChannelId] PRIMARY KEY CLUSTERED 
(
	[ChannelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_ChannelName] UNIQUE NONCLUSTERED 
(
	[ChannelName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDataBlock]    Script Date: 25/11/2020 01:18:01 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDataBlock](
	[ChannelId] [int] NOT NULL,
	[DeviceId] [int] NOT NULL,
	[DataBlockId] [int] NOT NULL,
	[DataBlockName] [varchar](50) NULL,
	[StartAddress] [varchar](50) NULL,
	[Length] [varchar](50) NULL,
	[DataType] [varchar](50) NULL,
	[MemoryType] [varchar](50) NULL,
	[DBFunction] [varchar](50) NULL,
	[IsArray] [varchar](50) NULL,
	[IsActive] [varchar](50) NULL,
	[Description] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDevice]    Script Date: 25/11/2020 01:18:01 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDevice](
	[ChannelId] [int] NOT NULL,
	[DeviceId] [int] NOT NULL,
	[SlaveId] [int] NOT NULL,
	[DeviceName] [varchar](50) NULL,
	[IsActive] [varchar](50) NULL,
	[Description] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTag]    Script Date: 25/11/2020 01:18:01 م ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTag](
	[ChannelId] [int] NOT NULL,
	[DeviceId] [int] NOT NULL,
	[DataBlockId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
	[TagName] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[DataType] [varchar](50) NULL,
	[Description] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
