USE [TrainingCourse]
GO
/****** Object:  Table [dbo].[TrainerTopic]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[TrainerTopic]
GO
/****** Object:  Table [dbo].[TrainerCourse]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[TrainerCourse]
GO
/****** Object:  Table [dbo].[Trainer]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[Trainer]
GO
/****** Object:  Table [dbo].[TraineeTopic]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[TraineeTopic]
GO
/****** Object:  Table [dbo].[TraineeCourse]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[TraineeCourse]
GO
/****** Object:  Table [dbo].[Trainee]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[Trainee]
GO
/****** Object:  Table [dbo].[Topic]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[Topic]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[Staff]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[Role]
GO
/****** Object:  Table [dbo].[Functions]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[Functions]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[Course]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[Category]
GO
/****** Object:  Table [dbo].[AccountFunction]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[AccountFunction]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/30/2022 3:06:52 PM ******/
DROP TABLE IF EXISTS [dbo].[Account]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Email] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[Admin] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountFunction]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountFunction](
	[AccountFuntionId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NULL,
	[FunctionId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountFuntionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](255) NULL,
	[CategoryCode] [nvarchar](255) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Descrip] [nvarchar](255) NULL,
	[Status] [int] NULL,
	[CategoryId] [int] NULL,
	[Forum] [nvarchar](max) NULL,
	[Author] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Functions]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Functions](
	[FunctionId] [int] IDENTITY(1,1) NOT NULL,
	[FunctionCode] [nvarchar](50) NULL,
	[FunctionName] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[FunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleCode] [nvarchar](50) NULL,
	[RoleName] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[StaffId] [int] NULL,
	[StaffName] [nvarchar](255) NULL,
	[Dob] [date] NULL,
	[Image] [nvarchar](max) NULL,
	[Gender] [bit] NULL,
	[AccountId] [int] NULL,
	[Position] [nvarchar](max) NULL,
	[Phone] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topic]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topic](
	[TopicId] [int] IDENTITY(1,1) NOT NULL,
	[TopicName] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Descrip] [nvarchar](255) NULL,
	[Status] [int] NULL,
	[CategoryId] [int] NULL,
	[Author] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[TopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainee]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainee](
	[TraineeId] [int] IDENTITY(1,1) NOT NULL,
	[TraineeName] [nvarchar](255) NULL,
	[Dob] [date] NULL,
	[Image] [nvarchar](max) NULL,
	[Gender] [bit] NULL,
	[AccountId] [int] NULL,
	[Education] [nvarchar](255) NULL,
	[Phone] [nvarchar](50) NULL,
	[MainProgram] [nvarchar](255) NULL,
	[TOEICScore] [int] NULL,
	[Detail] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[TraineeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TraineeCourse]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TraineeCourse](
	[TraineeCourseId] [int] IDENTITY(1,1) NOT NULL,
	[TraineeId] [int] NULL,
	[CourseId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TraineeCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TraineeTopic]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TraineeTopic](
	[TraineeTopicId] [int] IDENTITY(1,1) NOT NULL,
	[TraineeId] [int] NULL,
	[TopicId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TraineeTopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainer]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainer](
	[TrainerId] [int] IDENTITY(1,1) NOT NULL,
	[TrainerName] [nvarchar](255) NULL,
	[Dob] [date] NULL,
	[Image] [nvarchar](max) NULL,
	[Gender] [bit] NULL,
	[AccountId] [int] NULL,
	[Education] [nvarchar](255) NULL,
	[Phone] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainerCourse]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainerCourse](
	[TrainerCourseId] [int] IDENTITY(1,1) NOT NULL,
	[TrainerId] [int] NULL,
	[CourseId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TrainerCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainerTopic]    Script Date: 10/30/2022 3:06:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainerTopic](
	[TrainerTopicId] [int] IDENTITY(1,1) NOT NULL,
	[TrainerId] [int] NULL,
	[TopicId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TrainerTopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
