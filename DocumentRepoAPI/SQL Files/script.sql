USE [master]
GO
/****** Object:  Database [filerepodb]    Script Date: 27-05-2021 03:06:51 PM ******/
CREATE DATABASE [filerepodb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'filerepodb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\filerepodb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'filerepodb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\filerepodb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [filerepodb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [filerepodb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [filerepodb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [filerepodb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [filerepodb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [filerepodb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [filerepodb] SET ARITHABORT OFF 
GO
ALTER DATABASE [filerepodb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [filerepodb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [filerepodb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [filerepodb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [filerepodb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [filerepodb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [filerepodb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [filerepodb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [filerepodb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [filerepodb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [filerepodb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [filerepodb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [filerepodb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [filerepodb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [filerepodb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [filerepodb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [filerepodb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [filerepodb] SET RECOVERY FULL 
GO
ALTER DATABASE [filerepodb] SET  MULTI_USER 
GO
ALTER DATABASE [filerepodb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [filerepodb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [filerepodb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [filerepodb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [filerepodb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [filerepodb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'filerepodb', N'ON'
GO
ALTER DATABASE [filerepodb] SET QUERY_STORE = OFF
GO
USE [filerepodb]
GO
/****** Object:  Table [dbo].[EventHistory]    Script Date: 27-05-2021 03:06:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventHistory](
	[EventId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventType] [int] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventTypes]    Script Date: 27-05-2021 03:06:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTypes](
	[EventTypeId] [int] IDENTITY(1,1) NOT NULL,
	[EventType] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 27-05-2021 03:06:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[FileId] [bigint] IDENTITY(1,1) NOT NULL,
	[FolderId] [bigint] NULL,
	[FileName] [varchar](max) NOT NULL,
	[FileUrl] [varchar](max) NOT NULL,
	[FileType] [varchar](max) NOT NULL,
	[ReadAccess] [bit] NOT NULL,
	[ModifyAccess] [bit] NOT NULL,
	[DeleteAccess] [bit] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [bigint] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Folders]    Script Date: 27-05-2021 03:06:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folders](
	[ParentId] [bigint] NULL,
	[FolderId] [bigint] IDENTITY(1,1) NOT NULL,
	[FolderName] [varchar](max) NOT NULL,
	[FolderUrl] [varchar](max) NOT NULL,
	[ReadAccess] [bit] NOT NULL,
	[ModifyAccess] [bit] NOT NULL,
	[DeleteAccess] [bit] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [bigint] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FolderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SharedFiles]    Script Date: 27-05-2021 03:06:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SharedFiles](
	[SharedFilesId] [bigint] IDENTITY(1,1) NOT NULL,
	[SharedFile] [bigint] NOT NULL,
	[SharedBy] [bigint] NOT NULL,
	[SharedTo] [bigint] NOT NULL,
	[ReadAccess] [bit] NOT NULL,
	[ModifyAccess] [bit] NOT NULL,
	[DeleteAccess] [bit] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [bigint] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SharedFilesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SharedFolders]    Script Date: 27-05-2021 03:06:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SharedFolders](
	[SharedFoldersId] [bigint] IDENTITY(1,1) NOT NULL,
	[SharedFolder] [bigint] NOT NULL,
	[SharedBy] [bigint] NOT NULL,
	[SharedTo] [bigint] NOT NULL,
	[ReadAccess] [bit] NOT NULL,
	[ModifyAccess] [bit] NOT NULL,
	[DeleteAccess] [bit] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [bigint] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SharedFoldersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserActiveTokens]    Script Date: 27-05-2021 03:06:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActiveTokens](
	[TokenId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Token] [varchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[TokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 27-05-2021 03:06:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailId] [varchar](200) NOT NULL,
	[PasswordHash] [varchar](512) NOT NULL,
	[UserTypeId] [int] NOT NULL,
	[UserActive] [bit] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[RecoveryPhoneNum] [varchar](50) NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [bigint] NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 27-05-2021 03:06:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypes](
	[UserTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserType] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventHistory] ADD  DEFAULT (getutcdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Files] ADD  DEFAULT ((1)) FOR [ReadAccess]
GO
ALTER TABLE [dbo].[Files] ADD  DEFAULT ((1)) FOR [ModifyAccess]
GO
ALTER TABLE [dbo].[Files] ADD  DEFAULT ((1)) FOR [DeleteAccess]
GO
ALTER TABLE [dbo].[Files] ADD  DEFAULT (getutcdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Files] ADD  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Folders] ADD  DEFAULT (NULL) FOR [ParentId]
GO
ALTER TABLE [dbo].[Folders] ADD  DEFAULT ((1)) FOR [ReadAccess]
GO
ALTER TABLE [dbo].[Folders] ADD  DEFAULT ((1)) FOR [ModifyAccess]
GO
ALTER TABLE [dbo].[Folders] ADD  DEFAULT ((1)) FOR [DeleteAccess]
GO
ALTER TABLE [dbo].[Folders] ADD  DEFAULT (getutcdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Folders] ADD  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[SharedFiles] ADD  DEFAULT ((0)) FOR [ReadAccess]
GO
ALTER TABLE [dbo].[SharedFiles] ADD  DEFAULT ((0)) FOR [ModifyAccess]
GO
ALTER TABLE [dbo].[SharedFiles] ADD  DEFAULT ((0)) FOR [DeleteAccess]
GO
ALTER TABLE [dbo].[SharedFiles] ADD  DEFAULT (getutcdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SharedFiles] ADD  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[SharedFolders] ADD  DEFAULT ((0)) FOR [ReadAccess]
GO
ALTER TABLE [dbo].[SharedFolders] ADD  DEFAULT ((0)) FOR [ModifyAccess]
GO
ALTER TABLE [dbo].[SharedFolders] ADD  DEFAULT ((0)) FOR [DeleteAccess]
GO
ALTER TABLE [dbo].[SharedFolders] ADD  DEFAULT (getutcdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SharedFolders] ADD  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[UserActiveTokens] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[UserActiveTokens] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [UserActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getutcdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
/****** Object:  StoredProcedure [dbo].[ValidateTokenRole]    Script Date: 27-05-2021 03:06:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ValidateTokenRole](
	@token varchar(100)
)
As
Begin
	If exists (
		select 1 from UserActiveTokens where Token = @token and Active = 1
	)
	Begin
		declare @userId bigint, @useRole varchar(50);
		set @userId = (select UserId from UserActiveTokens where Token = @token and Active = 1);
		set @useRole = (select UT.UserType from Users U join UserTypes UT on U.UserTypeId = UT.UserTypeId);
		select 1 status, 'Valid Token' message, @userId UserID, @useRole UserType;
		--select @userId = UserId from UserActiveTokens where Token = @token and Active = 1;
	End
	Else
	Begin
		select 0 status,'Invalid Token' message, -1 UserID, '' UserType;
	End
End
GO
USE [master]
GO
ALTER DATABASE [filerepodb] SET  READ_WRITE 
GO
