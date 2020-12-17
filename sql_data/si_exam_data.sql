USE [master]
GO
/****** Object:  Database [si_exam]    Script Date: 17/12/2020 14.08.46 ******/
CREATE DATABASE [si_exam]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'si_exam', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\si_exam.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'si_exam_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\si_exam_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [si_exam] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [si_exam].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [si_exam] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [si_exam] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [si_exam] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [si_exam] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [si_exam] SET ARITHABORT OFF 
GO
ALTER DATABASE [si_exam] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [si_exam] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [si_exam] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [si_exam] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [si_exam] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [si_exam] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [si_exam] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [si_exam] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [si_exam] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [si_exam] SET  DISABLE_BROKER 
GO
ALTER DATABASE [si_exam] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [si_exam] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [si_exam] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [si_exam] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [si_exam] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [si_exam] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [si_exam] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [si_exam] SET RECOVERY FULL 
GO
ALTER DATABASE [si_exam] SET  MULTI_USER 
GO
ALTER DATABASE [si_exam] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [si_exam] SET DB_CHAINING OFF 
GO
ALTER DATABASE [si_exam] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [si_exam] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [si_exam] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [si_exam] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'si_exam', N'ON'
GO
ALTER DATABASE [si_exam] SET QUERY_STORE = OFF
GO
USE [si_exam]
GO
/****** Object:  Table [dbo].[Airport]    Script Date: 17/12/2020 14.08.46 ******/
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
/****** Object:  Table [dbo].[Booking]    Script Date: 17/12/2020 14.08.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[id] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[Flight]    Script Date: 17/12/2020 14.08.46 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 17/12/2020 14.08.46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[username] [nvarchar](50) NOT NULL,
	[name] [nvarchar](128) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[email] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Airport] ([name], [code], [country]) VALUES (N'Kastrup Airport                                                                                                                                                                                                                                                 ', N'CPH', N'Denmark                                                                                                                                                                                                                                                         ')
INSERT [dbo].[Airport] ([name], [code], [country]) VALUES (N'Los Angeles International Airport                                                                                                                                                                                                                               ', N'LAX', N'United States                                                                                                                                                                                                                                                   ')
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([id], [user_id], [flight_id], [price], [status]) VALUES (1, 1, 1, 100, N'0')
INSERT [dbo].[Booking] ([id], [user_id], [flight_id], [price], [status]) VALUES (2, 1, 1, 100, N'RESERVED')
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
SET IDENTITY_INSERT [dbo].[Flight] ON 

INSERT [dbo].[Flight] ([departure_airport], [arrival_airport], [id], [image], [time], [price]) VALUES (N'CPH', N'LAX', 1, N'https://www.troutman.com/images/content/2/2/v1/227098.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                       ', N'14:52', 100000)
INSERT [dbo].[Flight] ([departure_airport], [arrival_airport], [id], [image], [time], [price]) VALUES (N'CPH', N'LAX', 2, N'https://www.troutman.com/images/content/2/2/v1/227098.jpg                                                                                                                                                                                                                                                                                                                                                                                                                                                                       ', N'18:30', 150000)
SET IDENTITY_INSERT [dbo].[Flight] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([username], [name], [id], [password], [email]) VALUES (N'adams', N'Adam Saidane', 1, N'passsword123', N'adamsaidane@hotmail.com')
INSERT [dbo].[User] ([username], [name], [id], [password], [email]) VALUES (N's_elementos', N'Sebastian Lundsgaard-Larsen', 2, N'vegan-chicken-little', N's_elementos@sebbelicious.com')
INSERT [dbo].[User] ([username], [name], [id], [password], [email]) VALUES (N'shroud', N'Emil Valbak Hermansen', 3, N'hermansen_deadlift', N'dizanos@shroud.com')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_username]    Script Date: 17/12/2020 14.08.46 ******/
CREATE UNIQUE NONCLUSTERED INDEX [unique_username] ON [dbo].[User]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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
USE [master]
GO
ALTER DATABASE [si_exam] SET  READ_WRITE 
GO
