USE [master]
GO
/****** Object:  Database [robotDb]    Script Date: 5/24/2023 9:23:32 AM ******/
CREATE DATABASE [robotDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'robotDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\robotDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'robotDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\robotDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [robotDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [robotDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [robotDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [robotDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [robotDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [robotDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [robotDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [robotDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [robotDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [robotDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [robotDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [robotDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [robotDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [robotDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [robotDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [robotDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [robotDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [robotDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [robotDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [robotDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [robotDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [robotDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [robotDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [robotDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [robotDb] SET RECOVERY FULL 
GO
ALTER DATABASE [robotDb] SET  MULTI_USER 
GO
ALTER DATABASE [robotDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [robotDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [robotDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [robotDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [robotDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [robotDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'robotDb', N'ON'
GO
ALTER DATABASE [robotDb] SET QUERY_STORE = OFF
GO
USE [robotDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/24/2023 9:23:32 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdentityId] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdentityId] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Environments]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Environments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Length] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Identities]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Identities](
	[Id] [varchar](255) NOT NULL,
	[Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materials]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materials](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuantityMaterials]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuantityMaterials](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[MaterialId] [int] NOT NULL,
	[EnvironmentId] [int] NULL,
	[TaskId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Robots]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Robots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[State] [int] NOT NULL,
	[EnvironmentId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[InitHour] [varchar](255) NOT NULL,
	[EndHour] [varchar](255) NOT NULL,
	[WeekDays] [varchar](255) NOT NULL,
	[Repeat] [bit] NULL,
	[Execution] [bit] NULL,
	[Stop] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TasksRobots]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TasksRobots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RobotId] [int] NOT NULL,
	[TaskId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warnings]    Script Date: 5/24/2023 9:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warnings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](255) NOT NULL,
	[State] [int] NOT NULL,
	[HourDay] [datetime] NULL,
	[RobotId] [int] NOT NULL,
	[IdentityId] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230515122554_AddIdentityTables', N'7.0.4')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1b9dd5e4-0d0e-4535-9864-db41aa21d58e', N'tino3', N'TINO3', N'tino3@gmail.com', N'TINO3@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEA3MlJRNqSRT2PEWxhX56jOM9sVxC6bnnMnw9m5xqkzlyNo2KKja/ZaBER8h/Soo2w==', N'DDFHWDFXVJULU6F3H6IFQYJAGF4RJKM6', N'a6d2ad85-57b1-40f0-8d5c-0c1ce6417e7a', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'27268307-6cbe-4e52-892a-cb5406956d85', N'asdas', N'ASDAS', N'dfgdf@gmail.com', N'DFGDF@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEI6gmFkdCzAw/3DZNHj9cHf+pafwLWOOel0OHcVgEg6kJ5YKnl3qoeL2MZUqoTMloA==', N'4P62PWW6RPFUSZNTI2QW6IIQYLE4STDF', N'1c775799-e5e6-4f45-a6ea-b68689c19b2c', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5844e483-27f3-48da-a003-99e486f11699', N'tino2', N'TINO2', N'tino2@gmail.com', N'TINO2@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEAsXuHh0VfgnrG7BNbdKpGAbiGYaAhTab5JmR7Wu8+wxc3jCDlYCyNJczF6r2MYgrw==', N'GJ7EHVUEVMYTOAWRL7ZZFEP4F76GSUVA', N'7128087e-db77-4144-a7ea-577bb0a66e63', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'76209015-d4ab-4485-813c-b4ce0e6d70fb', N'dfgd', N'DFGD', N'wer@gmail.com', N'WER@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEB83VzmQgzdnAErfHH3bAewnX+2gR+YXtYUic3cDWrLeM5uki3KN1HUinTrU1Jt/vg==', N'YQXHP6KDAHS5O47LUFAQ3YXO3PVAML5U', N'a776620d-55a4-4ea4-83d8-f7938e83f5c5', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e37e2f0e-4928-4796-8f8a-025a47533fbb', N'tino4', N'TINO4', N'tino4@gmail.com', N'TINO4@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAECIVbIFtvNsSNYBA3CMbtLDB/9cOqZ3Q8NIdFs7gFYzkZLjwgSE8AH3eFG3wWgpJ+g==', N'Q2YRQWMJRJO5F44ZLXOGIKGGI4XVSXLU', N'e335e8fa-0b9f-424b-9448-0ac3d158260c', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ffbd5b4d-53f5-4b95-b7ed-d140fb806cbe', N'tino', N'TINO', N'tino@gmail.com', N'TINO@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEByQg0BkRlGI998bXHNLYg+GZk1NqdsWyolWXGAX0s5AuHyKmch6Fsf6Q9XTpRtklw==', N'UUQWYI22J73ULKCRKCBSQSGFHZJFIYQD', N'5e09453b-fd3f-46c6-8c98-bd9b45d01140', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([Id], [IdentityId]) VALUES (1, N'5844e483-27f3-48da-a003-99e486f11699')
INSERT [dbo].[Clients] ([Id], [IdentityId]) VALUES (2, N'1b9dd5e4-0d0e-4535-9864-db41aa21d58e')
INSERT [dbo].[Clients] ([Id], [IdentityId]) VALUES (3, N'e37e2f0e-4928-4796-8f8a-025a47533fbb')
INSERT [dbo].[Clients] ([Id], [IdentityId]) VALUES (4, N'27268307-6cbe-4e52-892a-cb5406956d85')
INSERT [dbo].[Clients] ([Id], [IdentityId]) VALUES (5, N'76209015-d4ab-4485-813c-b4ce0e6d70fb')
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Environments] ON 

INSERT [dbo].[Environments] ([Id], [Name], [Length], [Width], [ClientId]) VALUES (2, N'test', 10, 20, 1)
SET IDENTITY_INSERT [dbo].[Environments] OFF
GO
INSERT [dbo].[Identities] ([Id], [Name]) VALUES (N'1b9dd5e4-0d0e-4535-9864-db41aa21d58e', N'Tiago')
INSERT [dbo].[Identities] ([Id], [Name]) VALUES (N'27268307-6cbe-4e52-892a-cb5406956d85', N'sdf')
INSERT [dbo].[Identities] ([Id], [Name]) VALUES (N'5844e483-27f3-48da-a003-99e486f11699', N'Tiago')
INSERT [dbo].[Identities] ([Id], [Name]) VALUES (N'76209015-d4ab-4485-813c-b4ce0e6d70fb', N'sdf')
INSERT [dbo].[Identities] ([Id], [Name]) VALUES (N'e37e2f0e-4928-4796-8f8a-025a47533fbb', N'Tiago')
GO
SET IDENTITY_INSERT [dbo].[Materials] ON 

INSERT [dbo].[Materials] ([Id], [Name]) VALUES (1, N'test')
SET IDENTITY_INSERT [dbo].[Materials] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 5/24/2023 9:23:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 5/24/2023 9:23:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 5/24/2023 9:23:32 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 5/24/2023 9:23:32 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD FOREIGN KEY([IdentityId])
REFERENCES [dbo].[Identities] ([Id])
GO
ALTER TABLE [dbo].[Companies]  WITH CHECK ADD FOREIGN KEY([IdentityId])
REFERENCES [dbo].[Identities] ([Id])
GO
ALTER TABLE [dbo].[Environments]  WITH CHECK ADD FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[QuantityMaterials]  WITH CHECK ADD FOREIGN KEY([EnvironmentId])
REFERENCES [dbo].[Environments] ([Id])
GO
ALTER TABLE [dbo].[QuantityMaterials]  WITH CHECK ADD FOREIGN KEY([MaterialId])
REFERENCES [dbo].[Materials] ([Id])
GO
ALTER TABLE [dbo].[QuantityMaterials]  WITH CHECK ADD FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[Robots]  WITH CHECK ADD FOREIGN KEY([EnvironmentId])
REFERENCES [dbo].[Environments] ([Id])
GO
ALTER TABLE [dbo].[TasksRobots]  WITH CHECK ADD FOREIGN KEY([RobotId])
REFERENCES [dbo].[Robots] ([Id])
GO
ALTER TABLE [dbo].[TasksRobots]  WITH CHECK ADD FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[Warnings]  WITH CHECK ADD FOREIGN KEY([IdentityId])
REFERENCES [dbo].[Identities] ([Id])
GO
ALTER TABLE [dbo].[Warnings]  WITH CHECK ADD FOREIGN KEY([RobotId])
REFERENCES [dbo].[Robots] ([Id])
GO
USE [master]
GO
ALTER DATABASE [robotDb] SET  READ_WRITE 
GO
