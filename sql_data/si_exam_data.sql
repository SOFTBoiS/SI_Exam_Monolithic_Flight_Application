USE [si_exam]
GO
/****** Object:  Table [dbo].[Airport]    Script Date: 17/12/2020 13.28.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airport](
	[name] [nvarchar](256) NOT NULL,
	[code] [nchar](3) NOT NULL,
	[country] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Airport] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 17/12/2020 13.28.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[flight_id] [int] NOT NULL,
	[price] [int] NOT NULL,
	[status] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flight]    Script Date: 17/12/2020 13.28.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[departure_airport] [nchar](3) NOT NULL,
	[arrival_airport] [nchar](3) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[image] [nvarchar](512) NOT NULL,
	[time] [nchar](5) NOT NULL,
	[price] [int] NOT NULL,
 CONSTRAINT [PK_Flight] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 17/12/2020 13.28.13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[username] [nvarchar](50) NOT NULL,
	[name] [nvarchar](128) NOT NULL,
	[id] [int] NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[email] [nvarchar](256) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Airport] ([name], [code], [country]) VALUES (N'Kastrup Airport                                                                                                                                                                                                                                                 ', N'CPH', N'Denmark                                                                                                                                                                                                                                                         ')
INSERT [dbo].[Airport] ([name], [code], [country]) VALUES (N'Los Angeles International Airport                                                                                                                                                                                                                               ', N'LAX', N'United States                                                                                                                                                                                                                                                   ')
GO
SET IDENTITY_INSERT [dbo].[Flight] ON 

INSERT [dbo].[Flight] ([departure_airport], [arrival_airport], [id], [image], [time], [price]) VALUES (N'CPH', N'LAX', 1, N'https://www.troutman.com/images/content/2/2/v1/227098.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                       ', N'14:52', 100000)
INSERT [dbo].[Flight] ([departure_airport], [arrival_airport], [id], [image], [time], [price]) VALUES (N'CPH', N'LAX', 2, N'https://www.troutman.com/images/content/2/2/v1/227098.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                       ', N'18:30', 150000)
SET IDENTITY_INSERT [dbo].[Flight] OFF
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_flight_id] FOREIGN KEY([flight_id])
REFERENCES [dbo].[Flight] ([id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_flight_id]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_user_id]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_arrival_ariport] FOREIGN KEY([arrival_airport])
REFERENCES [dbo].[Airport] ([code])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_arrival_ariport]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_departure_airport] FOREIGN KEY([departure_airport])
REFERENCES [dbo].[Airport] ([code])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_departure_airport]
GO
