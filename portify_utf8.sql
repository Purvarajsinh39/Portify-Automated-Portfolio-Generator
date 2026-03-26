USE [master]
GO
/****** Object:  Database [Portify]    Script Date: 26-03-2026 22:15:44 ******/
CREATE DATABASE [Portify]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Portify_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Portify.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Portify_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Portify.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Portify] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Portify].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Portify] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Portify] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Portify] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Portify] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Portify] SET ARITHABORT OFF 
GO
ALTER DATABASE [Portify] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Portify] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Portify] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Portify] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Portify] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Portify] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Portify] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Portify] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Portify] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Portify] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Portify] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Portify] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Portify] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Portify] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Portify] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Portify] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Portify] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Portify] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Portify] SET  MULTI_USER 
GO
ALTER DATABASE [Portify] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Portify] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Portify] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Portify] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Portify] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Portify] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Portify] SET QUERY_STORE = ON
GO
ALTER DATABASE [Portify] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Portify]
GO
/****** Object:  Table [dbo].[Downloads]    Script Date: 26-03-2026 22:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Downloads](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PortfolioId] [int] NOT NULL,
	[DownloadedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Experiences]    Script Date: 26-03-2026 22:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Experiences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PortfolioId] [int] NOT NULL,
	[CompanyName] [nvarchar](150) NULL,
	[Role] [nvarchar](100) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 26-03-2026 22:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PortfolioId] [int] NULL,
	[Rating] [int] NULL,
	[Message] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PortfolioPersonalInfo]    Script Date: 26-03-2026 22:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortfolioPersonalInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PortfolioId] [int] NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Profession] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
	[Location] [nvarchar](100) NULL,
	[ProfileImagePath] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Portfolios]    Script Date: 26-03-2026 22:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Portfolios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TemplateId] [int] NOT NULL,
	[Title] [nvarchar](150) NULL,
	[AboutMe] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 26-03-2026 22:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PortfolioId] [int] NOT NULL,
	[ProjectTitle] [nvarchar](150) NULL,
	[Description] [nvarchar](max) NULL,
	[TechStack] [nvarchar](255) NULL,
	[GitHubLink] [nvarchar](255) NULL,
	[LiveLink] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skills]    Script Date: 26-03-2026 22:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PortfolioId] [int] NOT NULL,
	[SkillName] [nvarchar](100) NULL,
	[SkillLevel] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocialLinks]    Script Date: 26-03-2026 22:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocialLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PortfolioId] [int] NOT NULL,
	[Platform] [nvarchar](50) NULL,
	[Url] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 26-03-2026 22:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TemplateName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[ThumbnailPath] [nvarchar](255) NULL,
	[HtmlPath] [nvarchar](255) NOT NULL,
	[CssPath] [nvarchar](255) NOT NULL,
	[JsPath] [nvarchar](255) NULL,
	[ConfigPath] [nvarchar](255) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Testimonials]    Script Date: 26-03-2026 22:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Testimonials](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PortfolioId] [int] NOT NULL,
	[PersonName] [nvarchar](100) NULL,
	[Feedback] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 26-03-2026 22:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[IsBlocked] [bit] NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Templates] ON 

INSERT [dbo].[Templates] ([Id], [TemplateName], [Description], [ThumbnailPath], [HtmlPath], [CssPath], [JsPath], [ConfigPath], [IsActive], [CreatedAt]) VALUES (1, N'Portfolio1', N'Auto-scanned template', NULL, N'~/Templates/Portfolio1.html', N'', NULL, N'', 1, CAST(N'2026-03-25T08:08:01.970' AS DateTime))
INSERT [dbo].[Templates] ([Id], [TemplateName], [Description], [ThumbnailPath], [HtmlPath], [CssPath], [JsPath], [ConfigPath], [IsActive], [CreatedAt]) VALUES (2, N'Portfolio2', N'Auto-scanned template', NULL, N'~/Templates/Portfolio2.html', N'', NULL, N'', 1, CAST(N'2026-03-25T08:08:01.990' AS DateTime))
INSERT [dbo].[Templates] ([Id], [TemplateName], [Description], [ThumbnailPath], [HtmlPath], [CssPath], [JsPath], [ConfigPath], [IsActive], [CreatedAt]) VALUES (3, N'Portfolio3', N'Auto-scanned template', NULL, N'~/Templates/Portfolio3.html', N'', NULL, N'', 1, CAST(N'2026-03-25T08:08:01.993' AS DateTime))
INSERT [dbo].[Templates] ([Id], [TemplateName], [Description], [ThumbnailPath], [HtmlPath], [CssPath], [JsPath], [ConfigPath], [IsActive], [CreatedAt]) VALUES (4, N'Portfolio4', N'Auto-scanned template', NULL, N'~/Templates/Portfolio4.html', N'', NULL, N'', 1, CAST(N'2026-03-25T08:08:01.997' AS DateTime))
INSERT [dbo].[Templates] ([Id], [TemplateName], [Description], [ThumbnailPath], [HtmlPath], [CssPath], [JsPath], [ConfigPath], [IsActive], [CreatedAt]) VALUES (5, N'Portfolio5', N'Auto-scanned template', NULL, N'~/Templates/Portfolio5.html', N'', NULL, N'', 1, CAST(N'2026-03-25T08:08:01.997' AS DateTime))
INSERT [dbo].[Templates] ([Id], [TemplateName], [Description], [ThumbnailPath], [HtmlPath], [CssPath], [JsPath], [ConfigPath], [IsActive], [CreatedAt]) VALUES (6, N'Portfolio6', N'Auto-scanned template', NULL, N'~/Templates/Portfolio6.html', N'', NULL, N'', 1, CAST(N'2026-03-25T08:08:01.997' AS DateTime))
INSERT [dbo].[Templates] ([Id], [TemplateName], [Description], [ThumbnailPath], [HtmlPath], [CssPath], [JsPath], [ConfigPath], [IsActive], [CreatedAt]) VALUES (7, N'Portfolio7', N'Auto-scanned template', NULL, N'~/Templates/Portfolio7.html', N'', NULL, N'', 1, CAST(N'2026-03-25T08:08:02.000' AS DateTime))
INSERT [dbo].[Templates] ([Id], [TemplateName], [Description], [ThumbnailPath], [HtmlPath], [CssPath], [JsPath], [ConfigPath], [IsActive], [CreatedAt]) VALUES (8, N'Portfolio8', N'Auto-scanned template', NULL, N'~/Templates/Portfolio8.html', N'', NULL, N'', 1, CAST(N'2026-03-25T08:08:02.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Templates] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FullName], [Email], [PasswordHash], [Role], [IsBlocked], [CreatedAt]) VALUES (2, N'Admin', N'admin@portify.com', N'Admin@123', N'Admin', 0, CAST(N'2026-01-07T09:05:09.353' AS DateTime))
INSERT [dbo].[Users] ([Id], [FullName], [Email], [PasswordHash], [Role], [IsBlocked], [CreatedAt]) VALUES (3, N'Purvarajsinh', N'purvarajsinh.test@gmail.com', N'123456', N'User', 0, CAST(N'2026-01-18T15:16:11.357' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534B43595BE]    Script Date: 26-03-2026 22:15:45 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Downloads] ADD  DEFAULT (getdate()) FOR [DownloadedAt]
GO
ALTER TABLE [dbo].[Feedback] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Portfolios] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Templates] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Templates] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsBlocked]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD FOREIGN KEY([PortfolioId])
REFERENCES [dbo].[Portfolios] ([Id])
GO
ALTER TABLE [dbo].[Experiences]  WITH CHECK ADD FOREIGN KEY([PortfolioId])
REFERENCES [dbo].[Portfolios] ([Id])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([PortfolioId])
REFERENCES [dbo].[Portfolios] ([Id])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[PortfolioPersonalInfo]  WITH CHECK ADD FOREIGN KEY([PortfolioId])
REFERENCES [dbo].[Portfolios] ([Id])
GO
ALTER TABLE [dbo].[Portfolios]  WITH CHECK ADD FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Templates] ([Id])
GO
ALTER TABLE [dbo].[Portfolios]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD FOREIGN KEY([PortfolioId])
REFERENCES [dbo].[Portfolios] ([Id])
GO
ALTER TABLE [dbo].[Skills]  WITH CHECK ADD FOREIGN KEY([PortfolioId])
REFERENCES [dbo].[Portfolios] ([Id])
GO
ALTER TABLE [dbo].[SocialLinks]  WITH CHECK ADD FOREIGN KEY([PortfolioId])
REFERENCES [dbo].[Portfolios] ([Id])
GO
ALTER TABLE [dbo].[Testimonials]  WITH CHECK ADD FOREIGN KEY([PortfolioId])
REFERENCES [dbo].[Portfolios] ([Id])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD CHECK  (([Rating]>=(1) AND [Rating]<=(5)))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Role]='Admin' OR [Role]='User'))
GO
USE [master]
GO
ALTER DATABASE [Portify] SET  READ_WRITE 
GO
