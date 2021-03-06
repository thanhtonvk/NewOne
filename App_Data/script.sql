USE [NewOneManager]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 22/04/28 9:16:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Username] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryOfFood]    Script Date: 22/04/28 9:16:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryOfFood](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Image] [ntext] NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 22/04/28 9:16:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[PhoneNumber] [char](10) NULL,
	[Point] [int] NULL,
	[Avatar] [ntext] NULL,
	[Username] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 22/04/28 9:16:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[DateOfBirth] [date] NULL,
	[Address] [nvarchar](100) NULL,
	[PhoneNumber] [char](10) NULL,
	[Postion] [nvarchar](30) NULL,
	[Avatar] [ntext] NULL,
	[Username] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 22/04/28 9:16:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDCategory] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Image] [ntext] NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodDetails]    Script Date: 22/04/28 9:16:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDFood] [int] NOT NULL,
	[Size] [nvarchar](20) NULL,
	[Cost] [int] NULL,
	[Image] [ntext] NULL,
	[Point] [int] NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 22/04/28 9:16:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](50) NULL,
	[Name] [nvarchar](100) NULL,
	[Quantity] [float] NULL,
	[Unit] [nvarchar](20) NULL,
	[Cost] [int] NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 22/04/28 9:16:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDCustomer] [int] NOT NULL,
	[IDEmployee] [int] NOT NULL,
	[OrderDate] [datetime] NULL,
	[Type] [tinyint] NULL,
	[Status] [tinyint] NULL,
	[Position] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 22/04/28 9:16:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDOrder] [int] NOT NULL,
	[IDFoodDetails] [int] NOT NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsedMaterial]    Script Date: 22/04/28 9:16:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsedMaterial](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDFoodDetails] [int] NOT NULL,
	[IDMaterial] [int] NOT NULL,
	[Quantity] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [fk_1] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [fk_1]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [fk_2] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [fk_2]
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD  CONSTRAINT [fk_3] FOREIGN KEY([IDCategory])
REFERENCES [dbo].[CategoryOfFood] ([ID])
GO
ALTER TABLE [dbo].[Food] CHECK CONSTRAINT [fk_3]
GO
ALTER TABLE [dbo].[FoodDetails]  WITH CHECK ADD  CONSTRAINT [fk_11] FOREIGN KEY([IDFood])
REFERENCES [dbo].[Food] ([ID])
GO
ALTER TABLE [dbo].[FoodDetails] CHECK CONSTRAINT [fk_11]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [fk_6] FOREIGN KEY([IDCustomer])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [fk_6]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [fk_7] FOREIGN KEY([IDEmployee])
REFERENCES [dbo].[Employee] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [fk_7]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [fk_8] FOREIGN KEY([IDOrder])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [fk_8]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [fk_9] FOREIGN KEY([IDFoodDetails])
REFERENCES [dbo].[FoodDetails] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [fk_9]
GO
ALTER TABLE [dbo].[UsedMaterial]  WITH CHECK ADD  CONSTRAINT [fk_4] FOREIGN KEY([IDFoodDetails])
REFERENCES [dbo].[FoodDetails] ([ID])
GO
ALTER TABLE [dbo].[UsedMaterial] CHECK CONSTRAINT [fk_4]
GO
ALTER TABLE [dbo].[UsedMaterial]  WITH CHECK ADD  CONSTRAINT [fk_5] FOREIGN KEY([IDMaterial])
REFERENCES [dbo].[Material] ([ID])
GO
ALTER TABLE [dbo].[UsedMaterial] CHECK CONSTRAINT [fk_5]
GO
----------------------------------------------------------------
ALTER TABLE [dbo].[Customer]  drop  CONSTRAINT [fk_1] 
GO
ALTER TABLE [dbo].[Employee]  drop  CONSTRAINT [fk_2] 
GO

GO
ALTER TABLE [dbo].[Food]  drop  CONSTRAINT [fk_3] 
GO

GO
ALTER TABLE [dbo].[FoodDetails]  drop  CONSTRAINT [fk_11] 

GO

ALTER TABLE [dbo].[Order]  drop  CONSTRAINT [fk_6]
GO
ALTER TABLE [dbo].[Order]  drop  CONSTRAINT [fk_7] 

GO
ALTER TABLE [dbo].[OrderDetails]  drop  CONSTRAINT [fk_8] 

GO
GO
ALTER TABLE [dbo].[OrderDetails]  drop  CONSTRAINT [fk_9] 
GO
GO
ALTER TABLE [dbo].[UsedMaterial]  drop  CONSTRAINT [fk_4] 

GO
ALTER TABLE [dbo].[UsedMaterial]  drop  CONSTRAINT [fk_5]
