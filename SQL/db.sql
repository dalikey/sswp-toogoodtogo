USE [master]
GO
/****** Object:  Database [AvansTooGoodToGo]    Script Date: 29-10-2022 20:23:59 ******/
CREATE DATABASE [AvansTooGoodToGo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AvansTooGoodToGo', FILENAME = N'C:\Users\Junhao\AvansTooGoodToGo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AvansTooGoodToGo_log', FILENAME = N'C:\Users\Junhao\AvansTooGoodToGo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AvansTooGoodToGo] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AvansTooGoodToGo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AvansTooGoodToGo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET ARITHABORT OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [AvansTooGoodToGo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AvansTooGoodToGo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AvansTooGoodToGo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AvansTooGoodToGo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AvansTooGoodToGo] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [AvansTooGoodToGo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AvansTooGoodToGo] SET  MULTI_USER 
GO
ALTER DATABASE [AvansTooGoodToGo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AvansTooGoodToGo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AvansTooGoodToGo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AvansTooGoodToGo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AvansTooGoodToGo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AvansTooGoodToGo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AvansTooGoodToGo] SET QUERY_STORE = OFF
GO
USE [AvansTooGoodToGo]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 29-10-2022 20:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CanteenEmployees]    Script Date: 29-10-2022 20:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CanteenEmployees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[EmployeeNumber] [nvarchar](max) NULL,
	[CanteenLocationEnum] [int] NOT NULL,
 CONSTRAINT [PK_CanteenEmployees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Canteens]    Script Date: 29-10-2022 20:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Canteens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CityEnum] [int] NOT NULL,
	[CanteenLocationEnum] [int] NOT NULL,
	[OffersHotMeals] [bit] NOT NULL,
	[CanteenEmployeeId] [int] NULL,
 CONSTRAINT [PK_Canteens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PackageProduct]    Script Date: 29-10-2022 20:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackageProduct](
	[ProductsId] [int] NOT NULL,
	[PackagesId] [int] NOT NULL,
 CONSTRAINT [PK_PackageProduct] PRIMARY KEY CLUSTERED 
(
	[ProductsId] ASC,
	[PackagesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Packages]    Script Date: 29-10-2022 20:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Packages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PickupDate] [datetime2](7) NULL,
	[EndOfPickupTime] [datetime2](7) NULL,
	[IsEighteenPlus] [bit] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[MealType] [int] NOT NULL,
	[ReservedById] [int] NULL,
	[CanteenId] [int] NULL,
	[CityEnum] [int] NOT NULL,
	[CanteenLocationEnum] [int] NOT NULL,
 CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 29-10-2022 20:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ContainsAlcohol] [bit] NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 29-10-2022 20:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Birthdate] [datetime2](7) NOT NULL,
	[StudentNumber] [nvarchar](max) NULL,
	[Emailadres] [nvarchar](max) NULL,
	[StudyCity] [int] NOT NULL,
	[Phonenumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221029181617_AttemptTwoThousand', N'6.0.10')
GO
SET IDENTITY_INSERT [dbo].[CanteenEmployees] ON 

INSERT [dbo].[CanteenEmployees] ([Id], [Name], [EmployeeNumber], [CanteenLocationEnum]) VALUES (1, N'firstemployee@hotmail.com', N'123456', 0)
INSERT [dbo].[CanteenEmployees] ([Id], [Name], [EmployeeNumber], [CanteenLocationEnum]) VALUES (2, N'secondemployee@hotmail.com', N'123457', 1)
INSERT [dbo].[CanteenEmployees] ([Id], [Name], [EmployeeNumber], [CanteenLocationEnum]) VALUES (3, N'Amanda Brook', N'456791', 2)
SET IDENTITY_INSERT [dbo].[CanteenEmployees] OFF
GO
SET IDENTITY_INSERT [dbo].[Canteens] ON 

INSERT [dbo].[Canteens] ([Id], [CityEnum], [CanteenLocationEnum], [OffersHotMeals], [CanteenEmployeeId]) VALUES (1, 0, 0, 1, 1)
INSERT [dbo].[Canteens] ([Id], [CityEnum], [CanteenLocationEnum], [OffersHotMeals], [CanteenEmployeeId]) VALUES (2, 2, 1, 1, 2)
INSERT [dbo].[Canteens] ([Id], [CityEnum], [CanteenLocationEnum], [OffersHotMeals], [CanteenEmployeeId]) VALUES (3, 1, 2, 0, 3)
SET IDENTITY_INSERT [dbo].[Canteens] OFF
GO
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (1, 1)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (2, 1)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (3, 1)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (5, 1)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (1, 2)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (2, 2)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (3, 2)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (5, 2)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (1, 3)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (2, 3)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (3, 3)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (1, 4)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (2, 4)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (2, 5)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (3, 5)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (4, 5)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (5, 5)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (1, 6)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (5, 6)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (1, 7)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (7, 7)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (1, 8)
INSERT [dbo].[PackageProduct] ([ProductsId], [PackagesId]) VALUES (6, 8)
GO
SET IDENTITY_INSERT [dbo].[Packages] ON 

INSERT [dbo].[Packages] ([Id], [Name], [PickupDate], [EndOfPickupTime], [IsEighteenPlus], [Price], [MealType], [ReservedById], [CanteenId], [CityEnum], [CanteenLocationEnum]) VALUES (1, N'Speciaal soep', CAST(N'2022-10-29T21:16:17.5910853' AS DateTime2), CAST(N'2022-10-29T22:16:17.5910898' AS DateTime2), 1, CAST(1.50 AS Decimal(18, 2)), 2, 1, 1, 0, 0)
INSERT [dbo].[Packages] ([Id], [Name], [PickupDate], [EndOfPickupTime], [IsEighteenPlus], [Price], [MealType], [ReservedById], [CanteenId], [CityEnum], [CanteenLocationEnum]) VALUES (2, N'Fruitmix', CAST(N'2022-10-29T21:16:17.5910906' AS DateTime2), CAST(N'2022-10-29T22:16:17.5910908' AS DateTime2), 0, CAST(1.20 AS Decimal(18, 2)), 3, 2, 1, 0, 0)
INSERT [dbo].[Packages] ([Id], [Name], [PickupDate], [EndOfPickupTime], [IsEighteenPlus], [Price], [MealType], [ReservedById], [CanteenId], [CityEnum], [CanteenLocationEnum]) VALUES (3, N'Panini mix', CAST(N'2022-10-29T21:16:17.5910913' AS DateTime2), CAST(N'2022-10-29T22:16:17.5910915' AS DateTime2), 0, CAST(5.20 AS Decimal(18, 2)), 0, NULL, 1, 0, 0)
INSERT [dbo].[Packages] ([Id], [Name], [PickupDate], [EndOfPickupTime], [IsEighteenPlus], [Price], [MealType], [ReservedById], [CanteenId], [CityEnum], [CanteenLocationEnum]) VALUES (4, N'Ijs mix', CAST(N'2022-10-29T21:16:17.5910918' AS DateTime2), CAST(N'2022-10-29T22:16:17.5910920' AS DateTime2), 0, CAST(1.50 AS Decimal(18, 2)), 4, NULL, 2, 0, 0)
INSERT [dbo].[Packages] ([Id], [Name], [PickupDate], [EndOfPickupTime], [IsEighteenPlus], [Price], [MealType], [ReservedById], [CanteenId], [CityEnum], [CanteenLocationEnum]) VALUES (5, N'Coffee mix', CAST(N'2022-10-29T21:16:17.5910923' AS DateTime2), CAST(N'2022-10-29T22:16:17.5910926' AS DateTime2), 0, CAST(3.20 AS Decimal(18, 2)), 2, NULL, 2, 0, 0)
INSERT [dbo].[Packages] ([Id], [Name], [PickupDate], [EndOfPickupTime], [IsEighteenPlus], [Price], [MealType], [ReservedById], [CanteenId], [CityEnum], [CanteenLocationEnum]) VALUES (6, N'Drank mix', CAST(N'2022-10-29T21:16:17.5910935' AS DateTime2), CAST(N'2022-10-29T22:16:17.5910937' AS DateTime2), 1, CAST(3.70 AS Decimal(18, 2)), 2, NULL, 3, 0, 0)
INSERT [dbo].[Packages] ([Id], [Name], [PickupDate], [EndOfPickupTime], [IsEighteenPlus], [Price], [MealType], [ReservedById], [CanteenId], [CityEnum], [CanteenLocationEnum]) VALUES (7, N'Snack mix', CAST(N'2022-10-29T21:16:17.5910940' AS DateTime2), CAST(N'2022-10-29T22:16:17.5910943' AS DateTime2), 0, CAST(4.20 AS Decimal(18, 2)), 3, NULL, 3, 0, 0)
INSERT [dbo].[Packages] ([Id], [Name], [PickupDate], [EndOfPickupTime], [IsEighteenPlus], [Price], [MealType], [ReservedById], [CanteenId], [CityEnum], [CanteenLocationEnum]) VALUES (8, N'Koekjes mix', CAST(N'2022-10-29T21:16:17.5910946' AS DateTime2), CAST(N'2022-10-29T22:16:17.5910948' AS DateTime2), 0, CAST(6.90 AS Decimal(18, 2)), 3, NULL, 3, 0, 0)
SET IDENTITY_INSERT [dbo].[Packages] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [ContainsAlcohol], [ImageUrl]) VALUES (1, N'Banaan', 0, N'https://cdn1.sph.harvard.edu/wp-content/uploads/sites/30/2018/08/bananas-1354785_1920-1024x683.jpg')
INSERT [dbo].[Products] ([Id], [Name], [ContainsAlcohol], [ImageUrl]) VALUES (2, N'Brood', 0, N'https://parade.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTkwNTgxNDM5MDYzMjA0OTg5/types-of-bread-jpg.jpg')
INSERT [dbo].[Products] ([Id], [Name], [ContainsAlcohol], [ImageUrl]) VALUES (3, N'Panini', 0, N'https://delitraiteur.lu/wp-content/uploads/2022/02/panini-1.png')
INSERT [dbo].[Products] ([Id], [Name], [ContainsAlcohol], [ImageUrl]) VALUES (4, N'Shake', 0, N'https://bakeitwithlove.com/wp-content/uploads/2022/03/Milk-Shakes-vs-Malts.jpg.webp')
INSERT [dbo].[Products] ([Id], [Name], [ContainsAlcohol], [ImageUrl]) VALUES (5, N'Coffee', 0, N'https://www.tastingtable.com/img/gallery/coffee-brands-ranked-from-worst-to-best/intro-1645231221.webp')
INSERT [dbo].[Products] ([Id], [Name], [ContainsAlcohol], [ImageUrl]) VALUES (6, N'Snoep', 0, N'https://upload.wikimedia.org/wikipedia/commons/thumb/1/10/Candy_in_Damascus.jpg/375px-Candy_in_Damascus.jpg')
INSERT [dbo].[Products] ([Id], [Name], [ContainsAlcohol], [ImageUrl]) VALUES (7, N'Bier', 1, N'https://media-cdn.tripadvisor.com/media/photo-s/1b/78/81/ac/jabeerwocky-craft-beer.jpg')
INSERT [dbo].[Products] ([Id], [Name], [ContainsAlcohol], [ImageUrl]) VALUES (8, N'Kroket', 0, N'https://mvlstreekvlees.nl/wp-content/uploads/2021/09/MVL2021-086-bewerkt.jpg')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([Id], [Name], [Birthdate], [StudentNumber], [Emailadres], [StudyCity], [Phonenumber]) VALUES (1, N'firststudent@hotmail.com', CAST(N'2000-10-28T18:35:13.0652791' AS DateTime2), N'2142135', N'firststudent@hotmail.com', 0, N'0612345611')
INSERT [dbo].[Students] ([Id], [Name], [Birthdate], [StudentNumber], [Emailadres], [StudyCity], [Phonenumber]) VALUES (2, N'secondstudent@hotmail.com', CAST(N'2005-10-28T18:35:13.0652791' AS DateTime2), N'2122346', N'secondstudent@hotmail.com', 1, N'0612345622')
INSERT [dbo].[Students] ([Id], [Name], [Birthdate], [StudentNumber], [Emailadres], [StudyCity], [Phonenumber]) VALUES (3, N'thirdstudent@hotmail.com', CAST(N'1992-10-28T18:35:13.0652791' AS DateTime2), N'2012347', N'thirdstudent@hotmail.com', 0, N'0612345633')
INSERT [dbo].[Students] ([Id], [Name], [Birthdate], [StudentNumber], [Emailadres], [StudyCity], [Phonenumber]) VALUES (4, N'Albert Macgenzie', CAST(N'2003-10-28T18:35:13.0652791' AS DateTime2), N'2011302', N'albert@hotmail.com', 2, N'0612194735')
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
/****** Object:  Index [IX_Canteens_CanteenEmployeeId]    Script Date: 29-10-2022 20:23:59 ******/
CREATE NONCLUSTERED INDEX [IX_Canteens_CanteenEmployeeId] ON [dbo].[Canteens]
(
	[CanteenEmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PackageProduct_PackagesId]    Script Date: 29-10-2022 20:23:59 ******/
CREATE NONCLUSTERED INDEX [IX_PackageProduct_PackagesId] ON [dbo].[PackageProduct]
(
	[PackagesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Packages_CanteenId]    Script Date: 29-10-2022 20:23:59 ******/
CREATE NONCLUSTERED INDEX [IX_Packages_CanteenId] ON [dbo].[Packages]
(
	[CanteenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Packages_ReservedById]    Script Date: 29-10-2022 20:23:59 ******/
CREATE NONCLUSTERED INDEX [IX_Packages_ReservedById] ON [dbo].[Packages]
(
	[ReservedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Canteens]  WITH CHECK ADD  CONSTRAINT [FK_Canteens_CanteenEmployees_CanteenEmployeeId] FOREIGN KEY([CanteenEmployeeId])
REFERENCES [dbo].[CanteenEmployees] ([Id])
GO
ALTER TABLE [dbo].[Canteens] CHECK CONSTRAINT [FK_Canteens_CanteenEmployees_CanteenEmployeeId]
GO
ALTER TABLE [dbo].[PackageProduct]  WITH CHECK ADD  CONSTRAINT [FK_PackageProduct_Packages_PackagesId] FOREIGN KEY([PackagesId])
REFERENCES [dbo].[Packages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PackageProduct] CHECK CONSTRAINT [FK_PackageProduct_Packages_PackagesId]
GO
ALTER TABLE [dbo].[PackageProduct]  WITH CHECK ADD  CONSTRAINT [FK_PackageProduct_Products_ProductsId] FOREIGN KEY([ProductsId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PackageProduct] CHECK CONSTRAINT [FK_PackageProduct_Products_ProductsId]
GO
ALTER TABLE [dbo].[Packages]  WITH CHECK ADD  CONSTRAINT [FK_Packages_Canteens_CanteenId] FOREIGN KEY([CanteenId])
REFERENCES [dbo].[Canteens] ([Id])
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_Packages_Canteens_CanteenId]
GO
ALTER TABLE [dbo].[Packages]  WITH CHECK ADD  CONSTRAINT [FK_Packages_Students_ReservedById] FOREIGN KEY([ReservedById])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[Packages] CHECK CONSTRAINT [FK_Packages_Students_ReservedById]
GO
USE [master]
GO
ALTER DATABASE [AvansTooGoodToGo] SET  READ_WRITE 
GO
