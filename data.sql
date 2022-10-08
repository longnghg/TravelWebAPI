USE [master]
GO
/****** Object:  Database [travelRover]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE DATABASE [travelRover]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'travelRover', FILENAME = N'D:\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\travelRover.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'travelRover_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\travelRover_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [travelRover] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [travelRover].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [travelRover] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [travelRover] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [travelRover] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [travelRover] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [travelRover] SET ARITHABORT OFF 
GO
ALTER DATABASE [travelRover] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [travelRover] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [travelRover] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [travelRover] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [travelRover] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [travelRover] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [travelRover] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [travelRover] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [travelRover] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [travelRover] SET  ENABLE_BROKER 
GO
ALTER DATABASE [travelRover] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [travelRover] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [travelRover] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [travelRover] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [travelRover] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [travelRover] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [travelRover] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [travelRover] SET RECOVERY FULL 
GO
ALTER DATABASE [travelRover] SET  MULTI_USER 
GO
ALTER DATABASE [travelRover] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [travelRover] SET DB_CHAINING OFF 
GO
ALTER DATABASE [travelRover] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [travelRover] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [travelRover] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [travelRover] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'travelRover', N'ON'
GO
ALTER DATABASE [travelRover] SET QUERY_STORE = OFF
GO
USE [travelRover]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/8/2022 6:35:18 PM ******/
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
/****** Object:  Table [dbo].[Banners]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banners](
	[Id] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Banners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[Id] [uniqueidentifier] NOT NULL,
	[LiscensePlate] [nvarchar](15) NOT NULL,
	[Status] [int] NOT NULL,
	[AmountSeat] [int] NOT NULL,
	[NameDriver] [nvarchar](15) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[Id] [uniqueidentifier] NOT NULL,
	[IdService] [uniqueidentifier] NOT NULL,
	[ContractName] [nvarchar](50) NULL,
	[TypeService] [nvarchar](20) NULL,
	[IdFile] [uniqueidentifier] NOT NULL,
	[SignDate] [bigint] NOT NULL,
	[ExpDate] [bigint] NOT NULL,
	[ModifyDate] [bigint] NOT NULL,
	[CreateDate] [bigint] NOT NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CostTours]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostTours](
	[Id] [uniqueidentifier] NOT NULL,
	[IdTourDetail] [nvarchar](450) NULL,
	[Breakfast] [real] NOT NULL,
	[Water] [real] NOT NULL,
	[FeeGas] [real] NOT NULL,
	[Distance] [real] NOT NULL,
	[SellCost] [real] NOT NULL,
	[Depreciation] [real] NOT NULL,
	[OtherPrice] [real] NOT NULL,
	[Tolls] [real] NOT NULL,
	[CusExpected] [int] NOT NULL,
	[InsuranceFee] [real] NOT NULL,
	[IsHoliday] [bit] NOT NULL,
	[TotalCostTour] [real] NOT NULL,
	[IdHotel] [uniqueidentifier] NOT NULL,
	[PriceHotel] [real] NOT NULL,
	[IdRestaurant] [uniqueidentifier] NOT NULL,
	[PriceRestaurant] [real] NOT NULL,
	[IdPlace] [uniqueidentifier] NOT NULL,
	[PriceTicketPlace] [real] NOT NULL,
 CONSTRAINT [PK_CostTours] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](14) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Password] [nvarchar](255) NULL,
	[Birthday] [bigint] NOT NULL,
	[CreateDate] [bigint] NOT NULL,
	[AccessToken] [nvarchar](550) NULL,
	[Point] [int] NOT NULL,
	[FbToken] [nvarchar](550) NULL,
	[GoogleToken] [nvarchar](550) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Districts]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Districts](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IdProvince] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](255) NULL,
	[Birthday] [bigint] NOT NULL,
	[Image] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreateDate] [bigint] NOT NULL,
	[AccessToken] [nvarchar](550) NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [bigint] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IdSchedule] [nvarchar](50) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[FileSize] [int] NOT NULL,
	[FileExtension] [nvarchar](10) NULL,
	[FilePath] [nvarchar](150) NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotels]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotels](
	[Id] [uniqueidentifier] NOT NULL,
	[IdContract] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Star] [int] NOT NULL,
	[SingleRoomPrice] [real] NOT NULL,
	[DoubleRoomPrice] [real] NOT NULL,
	[QuantityDBR] [int] NOT NULL,
	[QuantitySR] [int] NOT NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [bigint] NOT NULL,
 CONSTRAINT [PK_Hotels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Size] [bigint] NOT NULL,
	[FilePath] [nvarchar](255) NULL,
	[IdService] [uniqueidentifier] NOT NULL,
	[Extension] [nvarchar](5) NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Type] [nvarchar](30) NULL,
	[IdTourBooking] [nvarchar](30) NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Places]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Places](
	[Id] [uniqueidentifier] NOT NULL,
	[IdContract] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[PriceTicket] [real] NOT NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [bigint] NOT NULL,
 CONSTRAINT [PK_Places] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotions]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotions](
	[Id] [uniqueidentifier] NOT NULL,
	[Value] [int] NOT NULL,
	[IdSchedule] [nvarchar](50) NULL,
	[ToDate] [bigint] NOT NULL,
	[FromDate] [bigint] NOT NULL,
 CONSTRAINT [PK_Promotions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provinces]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provinces](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Provinces] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restaurants]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurants](
	[Id] [uniqueidentifier] NOT NULL,
	[IdContract] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [bigint] NOT NULL,
 CONSTRAINT [PK_Restaurants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](100) NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[Id] [nvarchar](50) NOT NULL,
	[DepartureDate] [bigint] NOT NULL,
	[BeginDate] [bigint] NOT NULL,
	[EndDate] [bigint] NOT NULL,
	[TimePromotion] [bigint] NOT NULL,
	[Status] [int] NOT NULL,
	[FinalPrice] [real] NOT NULL,
	[QuantityAdult] [real] NOT NULL,
	[QuantityBaby] [real] NOT NULL,
	[MinCapacity] [real] NOT NULL,
	[QuantityChild] [real] NOT NULL,
	[IdTour] [nvarchar](450) NULL,
	[IdCar] [uniqueidentifier] NOT NULL,
	[IdPromotion] [int] NOT NULL,
	[IdEmployee] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timelines]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timelines](
	[Id] [nvarchar](450) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[FromTime] [bigint] NOT NULL,
	[ToTime] [bigint] NOT NULL,
	[ModifyBy] [nvarchar](100) NULL,
	[ModifyDate] [bigint] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IdSchedule] [nvarchar](50) NULL,
 CONSTRAINT [PK_Timelines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tour]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tour](
	[Id] [nvarchar](450) NOT NULL,
	[TourName] [nvarchar](150) NOT NULL,
	[Rating] [float] NOT NULL,
	[FromPlace] [nvarchar](100) NULL,
	[ToPlace] [nvarchar](100) NULL,
	[ApproveStatus] [nvarchar](100) NULL,
	[Status] [int] NOT NULL,
	[CreateDate] [bigint] NOT NULL,
	[ModifyBy] [nvarchar](100) NULL,
	[ModifyDate] [bigint] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Thumbsnail] [nvarchar](150) NULL,
 CONSTRAINT [PK_Tour] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tourbookingDetails]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tourbookingDetails](
	[Id] [nvarchar](450) NOT NULL,
	[Baby] [int] NOT NULL,
	[Child] [int] NOT NULL,
	[Adult] [int] NOT NULL,
	[Pincode] [nvarchar](10) NULL,
	[Status] [int] NOT NULL,
	[IsCalled] [bit] NOT NULL,
	[CallDate] [bigint] NOT NULL,
	[IdTourBooking] [nvarchar](30) NULL,
 CONSTRAINT [PK_tourbookingDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tourbookings]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tourbookings](
	[Id] [nvarchar](30) NOT NULL,
	[CustomerName] [nvarchar](100) NULL,
	[ContactName] [nvarchar](100) NULL,
	[Phone] [nvarchar](14) NOT NULL,
	[BookingNo] [nvarchar](30) NULL,
	[Pincode] [nvarchar](10) NULL,
	[DateBooking] [bigint] NOT NULL,
	[LastDate] [bigint] NOT NULL,
	[Vat] [float] NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[VoucherCode] [nvarchar](10) NULL,
	[IsCalled] [bit] NOT NULL,
	[Deposit] [real] NOT NULL,
	[RemainPrice] [real] NOT NULL,
	[TotalPrice] [real] NOT NULL,
	[ModifyBy] [nvarchar](100) NULL,
	[ModifyDate] [bigint] NOT NULL,
 CONSTRAINT [PK_Tourbookings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TourDetails]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TourDetails](
	[Id] [nvarchar](450) NOT NULL,
	[TourId] [nvarchar](450) NULL,
	[IdCostTour] [uniqueidentifier] NOT NULL,
	[PriceChild] [real] NOT NULL,
	[PriceBaby] [real] NOT NULL,
	[PriceAdult] [real] NOT NULL,
	[PriceChildPromotion] [real] NOT NULL,
	[PriceBabyPromotion] [real] NOT NULL,
	[PriceAdultPromotion] [real] NOT NULL,
	[DisplayPrice] [real] NOT NULL,
	[DisplayPromotionPrice] [real] NOT NULL,
	[Description] [nvarchar](300) NULL,
	[QuantityBooked] [int] NOT NULL,
	[IsPromotion] [bit] NOT NULL,
	[TotalCostTour] [real] NOT NULL,
	[Profit] [int] NOT NULL,
	[Vat] [real] NOT NULL,
	[FinalPrice] [real] NOT NULL,
 CONSTRAINT [PK_TourDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vouchers]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vouchers](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](20) NULL,
	[Description] [nvarchar](100) NULL,
	[Value] [int] NOT NULL,
	[StartDate] [bigint] NOT NULL,
	[EndDate] [bigint] NOT NULL,
	[CreateDate] [bigint] NOT NULL,
	[ModifyDate] [bigint] NOT NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[Point] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IdCustomer] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Vouchers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wards]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wards](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[IdDistrict] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Wards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_CostTours_IdHotel]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_CostTours_IdHotel] ON [dbo].[CostTours]
(
	[IdHotel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CostTours_IdPlace]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_CostTours_IdPlace] ON [dbo].[CostTours]
(
	[IdPlace] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CostTours_IdRestaurant]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_CostTours_IdRestaurant] ON [dbo].[CostTours]
(
	[IdRestaurant] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CostTours_IdTourDetail]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CostTours_IdTourDetail] ON [dbo].[CostTours]
(
	[IdTourDetail] ASC
)
WHERE ([IdTourDetail] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Districts_IdProvince]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_Districts_IdProvince] ON [dbo].[Districts]
(
	[IdProvince] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_RoleId]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_Employees_RoleId] ON [dbo].[Employees]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Payment_IdTourBooking]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_Payment_IdTourBooking] ON [dbo].[Payment]
(
	[IdTourBooking] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Promotions_IdSchedule]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_Promotions_IdSchedule] ON [dbo].[Promotions]
(
	[IdSchedule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schedules_IdCar]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_Schedules_IdCar] ON [dbo].[Schedules]
(
	[IdCar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schedules_IdEmployee]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_Schedules_IdEmployee] ON [dbo].[Schedules]
(
	[IdEmployee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Schedules_IdTour]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_Schedules_IdTour] ON [dbo].[Schedules]
(
	[IdTour] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Timelines_IdSchedule]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_Timelines_IdSchedule] ON [dbo].[Timelines]
(
	[IdSchedule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_tourbookingDetails_IdTourBooking]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_tourbookingDetails_IdTourBooking] ON [dbo].[tourbookingDetails]
(
	[IdTourBooking] ASC
)
WHERE ([IdTourBooking] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Wards_IdDistrict]    Script Date: 10/8/2022 6:35:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_Wards_IdDistrict] ON [dbo].[Wards]
(
	[IdDistrict] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Car] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT (N'0') FOR [Email]
GO
ALTER TABLE [dbo].[Vouchers] ADD  DEFAULT ((0)) FOR [Point]
GO
ALTER TABLE [dbo].[Vouchers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[CostTours]  WITH CHECK ADD  CONSTRAINT [FK_CostTours_Hotels_IdHotel] FOREIGN KEY([IdHotel])
REFERENCES [dbo].[Hotels] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CostTours] CHECK CONSTRAINT [FK_CostTours_Hotels_IdHotel]
GO
ALTER TABLE [dbo].[CostTours]  WITH CHECK ADD  CONSTRAINT [FK_CostTours_Places_IdPlace] FOREIGN KEY([IdPlace])
REFERENCES [dbo].[Places] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CostTours] CHECK CONSTRAINT [FK_CostTours_Places_IdPlace]
GO
ALTER TABLE [dbo].[CostTours]  WITH CHECK ADD  CONSTRAINT [FK_CostTours_Restaurants_IdRestaurant] FOREIGN KEY([IdRestaurant])
REFERENCES [dbo].[Restaurants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CostTours] CHECK CONSTRAINT [FK_CostTours_Restaurants_IdRestaurant]
GO
ALTER TABLE [dbo].[CostTours]  WITH CHECK ADD  CONSTRAINT [FK_CostTours_TourDetails_IdTourDetail] FOREIGN KEY([IdTourDetail])
REFERENCES [dbo].[TourDetails] ([Id])
GO
ALTER TABLE [dbo].[CostTours] CHECK CONSTRAINT [FK_CostTours_TourDetails_IdTourDetail]
GO
ALTER TABLE [dbo].[Districts]  WITH CHECK ADD  CONSTRAINT [FK_Districts_Provinces_IdProvince] FOREIGN KEY([IdProvince])
REFERENCES [dbo].[Provinces] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Districts] CHECK CONSTRAINT [FK_Districts_Provinces_IdProvince]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Roles_RoleId]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Tourbookings_IdTourBooking] FOREIGN KEY([IdTourBooking])
REFERENCES [dbo].[Tourbookings] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Tourbookings_IdTourBooking]
GO
ALTER TABLE [dbo].[Promotions]  WITH CHECK ADD  CONSTRAINT [FK_Promotions_Schedules_IdSchedule] FOREIGN KEY([IdSchedule])
REFERENCES [dbo].[Schedules] ([Id])
GO
ALTER TABLE [dbo].[Promotions] CHECK CONSTRAINT [FK_Promotions_Schedules_IdSchedule]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Car_IdCar] FOREIGN KEY([IdCar])
REFERENCES [dbo].[Car] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Car_IdCar]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Employees_IdEmployee] FOREIGN KEY([IdEmployee])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Employees_IdEmployee]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Tour_IdTour] FOREIGN KEY([IdTour])
REFERENCES [dbo].[Tour] ([Id])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Tour_IdTour]
GO
ALTER TABLE [dbo].[Timelines]  WITH CHECK ADD  CONSTRAINT [FK_Timelines_Schedules_IdSchedule] FOREIGN KEY([IdSchedule])
REFERENCES [dbo].[Schedules] ([Id])
GO
ALTER TABLE [dbo].[Timelines] CHECK CONSTRAINT [FK_Timelines_Schedules_IdSchedule]
GO
ALTER TABLE [dbo].[tourbookingDetails]  WITH CHECK ADD  CONSTRAINT [FK_tourbookingDetails_Tourbookings_IdTourBooking] FOREIGN KEY([IdTourBooking])
REFERENCES [dbo].[Tourbookings] ([Id])
GO
ALTER TABLE [dbo].[tourbookingDetails] CHECK CONSTRAINT [FK_tourbookingDetails_Tourbookings_IdTourBooking]
GO
ALTER TABLE [dbo].[Wards]  WITH CHECK ADD  CONSTRAINT [FK_Wards_Districts_IdDistrict] FOREIGN KEY([IdDistrict])
REFERENCES [dbo].[Districts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Wards] CHECK CONSTRAINT [FK_Wards_Districts_IdDistrict]
GO
/****** Object:  StoredProcedure [dbo].[SearchEmployees]    Script Date: 10/8/2022 6:35:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchEmployees]
 @KwId nvarchar(255),
 @KwName nvarchar(255),
 @KwEmail nvarchar(255),
 @KwPhone nvarchar(255),
 @KwRole nvarchar(255),
 @KwIsActive nvarchar(255),
 @PageNumber int,
 @PageSize int
AS
 DECLARE @Start int, @End int
	SET @Start = (((@PageNumber - 1) * @PageSize) + 1)
	SET @End = (@Start + @PageSize - 1)

																 
	SELECT *
		FROM (
			SELECT *, ROW_NUMBER() OVER (ORDER BY RoleId asc) AS RowNum
			FROM dbo.Employees AS e
			WHERE e.IsDelete = 0  AND  ( e.Id LIKE '%' + @KwId + '%' AND 
									e.Name LIKE '%' + @KwName + '%' AND
									e.Email LIKE '%' + @KwEmail + '%' AND
									e.Phone LIKE '%' + @KwPhone + '%' AND
									(((SELECT COUNT(VALUE) FROM  STRING_SPLIT(@KwRole, ',')) = 1 AND e.RoleId LIKE @KwRole + '%') OR
									((SELECT COUNT(VALUE) FROM  STRING_SPLIT(@KwRole, ',')) > 1 AND e.RoleId IN (SELECT VALUE FROM  STRING_SPLIT(@KwRole, ',')))) AND
									(((SELECT COUNT(VALUE) FROM  STRING_SPLIT(@KwIsActive, ',')) = 1 AND e.IsActive LIKE @KwIsActive + '%') OR
									((SELECT COUNT(VALUE) FROM  STRING_SPLIT(@KwIsActive, ',')) > 1 AND e.IsActive IN (SELECT VALUE FROM  STRING_SPLIT(@KwIsActive, ',')))))
		) AS r 
		WHERE  r.RowNum BETWEEN @Start AND @End
																 
-- [SearchEmployees] '', '', '', '', '', '', 1, 5

   
GO
USE [master]
GO
ALTER DATABASE [travelRover] SET  READ_WRITE 
GO
