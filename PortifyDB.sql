USE [PortifyDB]
GO
/****** Object:  Schema [HangFire]    Script Date: 11-04-2026 2:28:24 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'HangFire')
BEGIN
    EXEC('CREATE SCHEMA [HangFire]')
END
GO
/****** Object:  Table [dbo].[Downloads]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Downloads]', N'U') IS NULL
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
/****** Object:  Table [dbo].[Education]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Education]', N'U') IS NULL
CREATE TABLE [dbo].[Education](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PortfolioId] [int] NOT NULL,
	[Degree] [nvarchar](150) NULL,
	[Institution] [nvarchar](150) NULL,
	[Year] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Experiences]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Experiences]', N'U') IS NULL
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
/****** Object:  Table [dbo].[Feedback]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Feedback]', N'U') IS NULL
CREATE TABLE [dbo].[Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PortfolioId] [int] NULL,
	[Rating] [int] NULL,
	[Message] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[TemplateId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Notifications]', N'U') IS NULL
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Message] [nvarchar](max) NOT NULL,
	[IsRead] [bit] NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Otps]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Otps]', N'U') IS NULL
CREATE TABLE [dbo].[Otps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Purpose] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpiresAt] [datetime] NOT NULL,
	[IsUsed] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PendingRegistrations]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[PendingRegistrations]', N'U') IS NULL
CREATE TABLE [dbo].[PendingRegistrations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PortfolioPersonalInfo]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[PortfolioPersonalInfo]', N'U') IS NULL
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
/****** Object:  Table [dbo].[Portfolios]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Portfolios]', N'U') IS NULL
CREATE TABLE [dbo].[Portfolios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TemplateId] [int] NOT NULL,
	[Title] [nvarchar](150) NULL,
	[AboutMe] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[DataJson] [nvarchar](max) NULL,
	[IsPublished] [bit] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Projects]', N'U') IS NULL
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
/****** Object:  Table [dbo].[Skills]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Skills]', N'U') IS NULL
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
/****** Object:  Table [dbo].[SocialLinks]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[SocialLinks]', N'U') IS NULL
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
/****** Object:  Table [dbo].[Templates]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Templates]', N'U') IS NULL
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
/****** Object:  Table [dbo].[Testimonials]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Testimonials]', N'U') IS NULL
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
/****** Object:  Table [dbo].[Users]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[dbo].[Users]', N'U') IS NULL
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[IsBlocked] [bit] NULL,
	[CreatedAt] [datetime] NULL,
	[BlockReason] [nvarchar](max) NULL,
	[ReceiveNotifications] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[AggregatedCounter]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[AggregatedCounter]', N'U') IS NULL
CREATE TABLE [HangFire].[AggregatedCounter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [bigint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_CounterAggregated] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Counter]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[Counter]', N'U') IS NULL
CREATE TABLE [HangFire].[Counter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
	[ExpireAt] [datetime] NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_HangFire_Counter] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Hash]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[Hash]', N'U') IS NULL
CREATE TABLE [HangFire].[Hash](
	[Key] [nvarchar](100) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_HangFire_Hash] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Field] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Job]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[Job]', N'U') IS NULL
CREATE TABLE [HangFire].[Job](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StateId] [bigint] NULL,
	[StateName] [nvarchar](20) NULL,
	[InvocationData] [nvarchar](max) NOT NULL,
	[Arguments] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobParameter]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[JobParameter]', N'U') IS NULL
CREATE TABLE [HangFire].[JobParameter](
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobQueue]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[JobQueue]', N'U') IS NULL
CREATE TABLE [HangFire].[JobQueue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Queue] [nvarchar](50) NOT NULL,
	[FetchedAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED 
(
	[Queue] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[List]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[List]', N'U') IS NULL
CREATE TABLE [HangFire].[List](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Schema]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[Schema]', N'U') IS NULL
CREATE TABLE [HangFire].[Schema](
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_HangFire_Schema] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Server]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[Server]', N'U') IS NULL
CREATE TABLE [HangFire].[Server](
	[Id] [nvarchar](200) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[LastHeartbeat] [datetime] NOT NULL,
 CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Set]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[Set]', N'U') IS NULL
CREATE TABLE [HangFire].[Set](
	[Key] [nvarchar](100) NOT NULL,
	[Score] [float] NOT NULL,
	[Value] [nvarchar](256) NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Set] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = ON, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[State]    Script Date: 11-04-2026 2:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'[HangFire].[State]', N'U') IS NULL
CREATE TABLE [HangFire].[State](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_State] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_AggregatedCounter_ExpireAt]    Script Date: 11-04-2026 2:28:24 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HangFire].[AggregatedCounter]') AND name = N'IX_HangFire_AggregatedCounter_ExpireAt')
CREATE NONCLUSTERED INDEX [IX_HangFire_AggregatedCounter_ExpireAt] ON [HangFire].[AggregatedCounter]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Hash_ExpireAt]    Script Date: 11-04-2026 2:28:24 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HangFire].[Hash]') AND name = N'IX_HangFire_Hash_ExpireAt')
CREATE NONCLUSTERED INDEX [IX_HangFire_Hash_ExpireAt] ON [HangFire].[Hash]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Job_ExpireAt]    Script Date: 11-04-2026 2:28:24 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HangFire].[Job]') AND name = N'IX_HangFire_Job_ExpireAt')
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_ExpireAt] ON [HangFire].[Job]
(
	[ExpireAt] ASC
)
INCLUDE([StateName]) 
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Job_StateName]    Script Date: 11-04-2026 2:28:24 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HangFire].[Job]') AND name = N'IX_HangFire_Job_StateName')
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_StateName] ON [HangFire].[Job]
(
	[StateName] ASC
)
WHERE ([StateName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_List_ExpireAt]    Script Date: 11-04-2026 2:28:24 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HangFire].[List]') AND name = N'IX_HangFire_List_ExpireAt')
CREATE NONCLUSTERED INDEX [IX_HangFire_List_ExpireAt] ON [HangFire].[List]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Server_LastHeartbeat]    Script Date: 11-04-2026 2:28:24 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HangFire].[Server]') AND name = N'IX_HangFire_Server_LastHeartbeat')
CREATE NONCLUSTERED INDEX [IX_HangFire_Server_LastHeartbeat] ON [HangFire].[Server]
(
	[LastHeartbeat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Set_ExpireAt]    Script Date: 11-04-2026 2:28:24 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HangFire].[Set]') AND name = N'IX_HangFire_Set_ExpireAt')
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_ExpireAt] ON [HangFire].[Set]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Set_Score]    Script Date: 11-04-2026 2:28:24 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HangFire].[Set]') AND name = N'IX_HangFire_Set_Score')
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_Score] ON [HangFire].[Set]
(
	[Key] ASC,
	[Score] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_State_CreatedAt]    Script Date: 11-04-2026 2:28:24 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HangFire].[State]') AND name = N'IX_HangFire_State_CreatedAt')
CREATE NONCLUSTERED INDEX [IX_HangFire_State_CreatedAt] ON [HangFire].[State]
(
	[CreatedAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Downloads]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Downloads]'), N'DownloadedAt', 'ColumnId'))
ALTER TABLE [dbo].[Downloads] ADD DEFAULT (getdate()) FOR [DownloadedAt]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Feedback]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Feedback]'), N'CreatedAt', 'ColumnId'))
ALTER TABLE [dbo].[Feedback] ADD DEFAULT (getdate()) FOR [CreatedAt]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Notifications]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Notifications]'), N'IsRead', 'ColumnId'))
ALTER TABLE [dbo].[Notifications] ADD DEFAULT ((0)) FOR [IsRead]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Notifications]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Notifications]'), N'CreatedAt', 'ColumnId'))
ALTER TABLE [dbo].[Notifications] ADD DEFAULT (getdate()) FOR [CreatedAt]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Otps]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Otps]'), N'CreatedAt', 'ColumnId'))
ALTER TABLE [dbo].[Otps] ADD DEFAULT (getdate()) FOR [CreatedAt]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Otps]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Otps]'), N'IsUsed', 'ColumnId'))
ALTER TABLE [dbo].[Otps] ADD DEFAULT ((0)) FOR [IsUsed]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[PendingRegistrations]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[PendingRegistrations]'), N'CreatedAt', 'ColumnId'))
ALTER TABLE [dbo].[PendingRegistrations] ADD DEFAULT (getdate()) FOR [CreatedAt]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Portfolios]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Portfolios]'), N'CreatedAt', 'ColumnId'))
ALTER TABLE [dbo].[Portfolios] ADD DEFAULT (getdate()) FOR [CreatedAt]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Portfolios]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Portfolios]'), N'IsPublished', 'ColumnId'))
ALTER TABLE [dbo].[Portfolios] ADD DEFAULT ((1)) FOR [IsPublished]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Templates]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Templates]'), N'IsActive', 'ColumnId'))
ALTER TABLE [dbo].[Templates] ADD DEFAULT ((1)) FOR [IsActive]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Templates]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Templates]'), N'CreatedAt', 'ColumnId'))
ALTER TABLE [dbo].[Templates] ADD DEFAULT (getdate()) FOR [CreatedAt]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Users]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Users]'), N'IsBlocked', 'ColumnId'))
ALTER TABLE [dbo].[Users] ADD DEFAULT ((0)) FOR [IsBlocked]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Users]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Users]'), N'CreatedAt', 'ColumnId'))
ALTER TABLE [dbo].[Users] ADD DEFAULT (getdate()) FOR [CreatedAt]
GO
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE parent_object_id = OBJECT_ID(N'[dbo].[Users]') AND parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'[dbo].[Users]'), N'ReceiveNotifications', 'ColumnId'))
ALTER TABLE [dbo].[Users] ADD DEFAULT ((1)) FOR [ReceiveNotifications]
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Downloads]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Portfolios]')
)
ALTER TABLE [dbo].[Downloads] WITH CHECK ADD FOREIGN KEY([PortfolioId]) REFERENCES [dbo].[Portfolios] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Education]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Portfolios]')
)
ALTER TABLE [dbo].[Education] WITH CHECK ADD FOREIGN KEY([PortfolioId]) REFERENCES [dbo].[Portfolios] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Experiences]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Portfolios]')
)
ALTER TABLE [dbo].[Experiences] WITH CHECK ADD FOREIGN KEY([PortfolioId]) REFERENCES [dbo].[Portfolios] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Feedback]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Portfolios]')
)
ALTER TABLE [dbo].[Feedback] WITH CHECK ADD FOREIGN KEY([PortfolioId]) REFERENCES [dbo].[Portfolios] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Feedback]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Templates]')
)
ALTER TABLE [dbo].[Feedback] WITH CHECK ADD FOREIGN KEY([TemplateId]) REFERENCES [dbo].[Templates] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Feedback]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Users]')
)
ALTER TABLE [dbo].[Feedback] WITH CHECK ADD FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Notifications]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Users]')
)
ALTER TABLE [dbo].[Notifications] WITH CHECK ADD FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[PortfolioPersonalInfo]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Portfolios]')
)
ALTER TABLE [dbo].[PortfolioPersonalInfo] WITH CHECK ADD FOREIGN KEY([PortfolioId]) REFERENCES [dbo].[Portfolios] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Portfolios]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Templates]')
)
ALTER TABLE [dbo].[Portfolios] WITH CHECK ADD FOREIGN KEY([TemplateId]) REFERENCES [dbo].[Templates] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Portfolios]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Users]')
)
ALTER TABLE [dbo].[Portfolios] WITH CHECK ADD FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Projects]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Portfolios]')
)
ALTER TABLE [dbo].[Projects] WITH CHECK ADD FOREIGN KEY([PortfolioId]) REFERENCES [dbo].[Portfolios] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Skills]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Portfolios]')
)
ALTER TABLE [dbo].[Skills] WITH CHECK ADD FOREIGN KEY([PortfolioId]) REFERENCES [dbo].[Portfolios] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[SocialLinks]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Portfolios]')
)
ALTER TABLE [dbo].[SocialLinks] WITH CHECK ADD FOREIGN KEY([PortfolioId]) REFERENCES [dbo].[Portfolios] ([Id])
GO
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys 
    WHERE parent_object_id = OBJECT_ID(N'[dbo].[Testimonials]') 
    AND referenced_object_id = OBJECT_ID(N'[dbo].[Portfolios]')
)
ALTER TABLE [dbo].[Testimonials] WITH CHECK ADD FOREIGN KEY([PortfolioId]) REFERENCES [dbo].[Portfolios] ([Id])
GO
IF OBJECT_ID(N'[HangFire].[FK_HangFire_JobParameter_Job]', N'F') IS NULL
ALTER TABLE [HangFire].[JobParameter] WITH CHECK ADD CONSTRAINT [FK_HangFire_JobParameter_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[JobParameter] CHECK CONSTRAINT [FK_HangFire_JobParameter_Job]
GO
IF OBJECT_ID(N'[HangFire].[FK_HangFire_State_Job]', N'F') IS NULL
ALTER TABLE [HangFire].[State] WITH CHECK ADD CONSTRAINT [FK_HangFire_State_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[State] CHECK CONSTRAINT [FK_HangFire_State_Job]
GO
-- Note: Unnamed CHECK constraints on [dbo].[Feedback] might produce duplicate constraints if run multiple times
ALTER TABLE [dbo].[Feedback] WITH CHECK ADD CHECK (([Rating]>=(1) AND [Rating]<=(5)))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Role]='Admin' OR [Role]='User'))
GO
