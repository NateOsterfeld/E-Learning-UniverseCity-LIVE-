USE [master]
GO
/****** Object:  Database [eLearning]    Script Date: 8/23/2018 10:54:33 AM ******/
CREATE DATABASE [eLearning]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'eLearning', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\eLearning.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'eLearning_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\eLearning_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [eLearning] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [eLearning].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [eLearning] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [eLearning] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [eLearning] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [eLearning] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [eLearning] SET ARITHABORT OFF 
GO
ALTER DATABASE [eLearning] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [eLearning] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [eLearning] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [eLearning] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [eLearning] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [eLearning] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [eLearning] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [eLearning] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [eLearning] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [eLearning] SET  ENABLE_BROKER 
GO
ALTER DATABASE [eLearning] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [eLearning] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [eLearning] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [eLearning] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [eLearning] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [eLearning] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [eLearning] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [eLearning] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [eLearning] SET  MULTI_USER 
GO
ALTER DATABASE [eLearning] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [eLearning] SET DB_CHAINING OFF 
GO
ALTER DATABASE [eLearning] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [eLearning] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [eLearning] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [eLearning] SET QUERY_STORE = OFF
GO
USE [eLearning]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [eLearning]
GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 8/23/2018 10:54:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE SEQUENCE MySequence START WITH 1;
CREATE TABLE [dbo].[Assignment](
	[AssignmentId] [int] IDENTITY(1,1) NOT NULL,
	[AssignmentName] [varchar] (max) NOT NULL,
	[FileId] [int] NULL,
	[Instructions] [nvarchar](max) NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_Assignemnt] PRIMARY KEY CLUSTERED 
(
	[AssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 8/23/2018 10:54:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATE SEQUENCE FileSequence START WITH 1;
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CostUSD] [decimal](18, 0) NOT NULL,
	[Difficulty] [int] NOT NULL,
	[CourseName] [varchar](100) NOT NULL,
	[TeacherID] [int] NOT NULL,
	[CourseRating] [decimal] (2,1),
	[CourseFileId] [int] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseFile]    Script Date: 9/27/2018 10:54:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseFile](
	[CourseFileId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[FilePath] [nvarchar] (max) NULL,
	[FileSize] [int] NULL,
	[CourseId] [int] NOT NULL
 CONSTRAINT [PK_CourseFile] PRIMARY KEY CLUSTERED 
(
	[CourseFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY] 
GO
/****** Object:  Table [dbo].[File]    Script Date: 8/23/2018 10:54:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[File](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[FilePath] [nvarchar] (max) NULL,
	[FileSize] [int] NULL,
	[AssignmentId] [int] NOT NULL
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY] 
GO

CREATE procedure [dbo].[spAddNewVideoFile]  
(  
@Name nvarchar(50),  
@FileSize int,  
@FilePath nvarchar(100)  
)  
as  
begin  
insert into [File] (Name,FileSize,FilePath)   
values (@Name,@FileSize,@FilePath)   
end  
  
CREATE procedure [dbo].[spGetAllVideoFile]  
as  
begin  
select FileId,Name,FileSize,FilePath from [File]
end  
/****** Object:  Table [dbo].[Link]    Script Date: 8/23/2018 10:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Link](
	[LinkId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Text] [varchar](100) NOT NULL,
	[URL] [varchar](max) NOT NULL,
	[AssignmentId] [int] NOT NULL,
 CONSTRAINT [PK_Link] PRIMARY KEY CLUSTERED 
(
	[LinkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 8/23/2018 10:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentAssignment]    Script Date: 8/23/2018 10:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAssignment](
	[StudentAssignmentId] [int] IDENTITY(1,1) NOT NULL,
	[AssignmentId] [int] NOT NULL,
	[AssignmentName] [varchar](50) NOT NULL,
	[FileID] [int] NOT NULL,
	[IsCompleated] [bit] NOT NULL,
	[Grade] [int] NOT NULL,
	[TeacherComments] [varchar](max) NULL,
 CONSTRAINT [PK_Assignment] PRIMARY KEY CLUSTERED 
(
	[StudentAssignmentId] ASC,
	[AssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentCourse]    Script Date: 8/23/2018 10:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentCourse](
	[UserId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_StudentCourse] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/23/2018 10:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Salt] [varchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Assignment] ON 
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (4, N'Intro to Multiplication: Please download the file and read the instructions regarding the proper format for submission.  You
will be prompted with a series of problems.', 2)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (5, N'Fractions: Please download the file and read the instructions regarding the proper format for submission.  You will be presented with a 
series of problems.', 2)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (6, N'Decimals: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series
of problems.', 2)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (7, N'Archaeological Data and Concepts: Please download the file and read the instructions regarding the proper format for submission. You will be prompted with a series of questions.', 3)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (8, N'Surveys and Excavations: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 3)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (9, N'Dating and Chronologies: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 3)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (10, N'Developing Economies: Please download the file and read the instructions regarding the proper format for submission. You will be prompted with a series of questions.', 4)
GO
INSERT [dbo].[Assignment] ([AssignmentId],[Instructions], [CourseId]) VALUES (11, N'Role of Government in Global Economies: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 4)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (12, N'Inflation and Unemployment: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 4)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (13, N'Theories of Punishment: Please download the file and read the instructions regarding the proper format for submission. You will be prompted with a series of questions.', 5)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (14, N'Homicide: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 5)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (15, N'Exculpation: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 5)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (16, N'Aging and Society: Please download the file and read the instructions regarding the proper format for submission. You will be prompted with a series of questions.', 6)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (17, N'The Life Course Perspective: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 6)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (18, N'Cultural Images of Aging: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 6)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (19, N'Multiculturalism: Please download the file and read the instructions regarding the proper format for submission. You will be prompted with a series of questions.', 7)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (20, N'Media Ethics: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 7)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (21, N'Media History: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 7)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (22, N'Supermodular Games: Please download the above link and read the instructions.  You will be prompted with a series of questions, and required reading.', 8)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (23, N'Solutions for Static Games: Please download the above link and read the instructions.  You will be prompted with a series of questions, and required reading.', 8)
GO
INSERT [dbo].[Assignment] ([AssignmentId], [Instructions], [CourseId]) VALUES (24, N'Reputation Formation: Please download the above link and read the instructions.  You will be prompted with a series of questions, and required reading.', 8)
GO
SET IDENTITY_INSERT [dbo].[Assignment] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 
GO
INSERT [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (2, N' This course is a sequence of developmental math that starts with basic arithmetic, then goes
on to pre-algebra, elementary algebra, and finally intermediate algebra. The nation is facing a great problem in that
students are making it to higher education without foundational math skills.  This course will help fill that void, and help 
you become more confident in your career.', CAST(0 AS Decimal(18, 0)), 1, N'Developmental Math', 25, CAST(5 AS Decimal(2, 1)))
GO
INSERT [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (3, N'Archaeology, or archeology, is the study of human activity through the recovery and analysis of material culture. The archaeological record consists of artifacts, architecture, biofacts or ecofacts and cultural landscapes. Archaeology can be considered both a social science and a branch of the humanities.
In this course, you will learn the fundamentals of Archaeology, as well as the potential careers.', CAST(0 AS Decimal(18, 0)), 3, N'Intro To Archaeology', 5, CAST(4.5 AS Decimal(2, 1)))
GO
INSERT [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (4, N'This course provides an overview of macroeconomic issues such as the determination of output, employment, unemployment, interest rates, and inflation. The course introduces basic models of macroeconomics and illustrates principles with the experience of the U.S. and foreign economies', CAST(0 AS Decimal(18, 0)), 3, N'Macroeconomics', 25, CAST(4.2 AS Decimal(2, 1)))
GO
INSERT [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (5, N'This course covers selected topics in substantive criminal law: principles underlying the definition of crime such as the requirements of actus reus and mens rea and general doctrines such as ignorance of fact and ignorance of law, causation, attempt, complicity and conspiracy. Principles of justification and excuse are examined with particular attention to the doctrines of necessity, intoxication, insanity, diminished capacity and automatism. The substantive offense of homicide is extensively reviewed, and from time to time other offenses such as theft. Throughout, emphasis is placed on the basic theory of the criminal law and the relationship between doctrines and the various justifications for imposition of punishment.', CAST(0 AS Decimal(18, 0)), 4, N'Criminal Law', 22, CAST(3.6 AS Decimal(2, 1)))
GO
INSERT [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (6, N'The Sociology of Aging course provides students with a comprehensive understanding of the process of aging and older people. This course reflects the multidisciplinary
field of gerontology, which includes the historical, cultural, biological, physiological, psychological, and social aspects of aging. Our focus is the sociology of
aging with an emphasis on “aging well.', CAST(0 AS Decimal(18, 0)), 3, N'Sociology of Aging', 26, CAST(5 AS Decimal(2, 1)))
GO
INSERT [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (7, N'This course will consider the roles of persuasion, commentary, and criticism in contemporary American culture and will explore the techniques of these forms of expression. Students will prepare and discuss their own writing assignments, including opinion columns, editorials, and critical reviews of performances or books. Ethics and responsibilities in contemporary American journalism in expression of opinions also will be explored. Assignments will serve as the examinations in this course, which is taught by a political columnist for the South Bend Tribune who also serves as host of public affairs programs on WNIT-TV, Public Broadcasting. Open to Journalism, Ethics, and Democracy minors only. Other applicants must submit writing samples for review.', CAST(0 AS Decimal(18, 0)), 4, N'Ethics, Persuasion, and Commentary in Journalism', 10, CAST(2.2 AS Decimal(2, 1)))
GO
INSERT [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (8, N'This course provides a rigorous treatment of non-cooperative solution concepts in game theory, including rationalizability and Nash, sequential, and stable equilibria. It covers topics such as epistemic foundations, higher order beliefs, bargaining, repeated games, reputation, supermodular games, and global games. It also introduces cooperative solution concepts—Nash bargaining solution, core, Shapley value—and develops corresponding non-cooperative foundations.', CAST(0 AS Decimal(18, 0)), 4, N'Game Theory', 26, CAST(3.9 AS Decimal(2, 1)))
GO
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
--SET IDENTITY_INSERT [dbo].[File] ON 
--GO
--INSERT [dbo].[File] ([FileId], [Name], [FileSize], [FilePath], [AssignmentId]) VALUES (2, 'Name', 20343, '~/VideoFileUpload/5. Adding Elements into Local Storage.mp4', 3)
--GO
--SET IDENTITY_INSERT [dbo].[File] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'Student')
GO
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Teacher')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (1, N'Tom', N'Holland', N'tomholland@aol.com', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (2, N'Lucas', N'Blaine', N'lucas464@gmail.com', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (3, N'Lindsey', N'Biggs', N'lbiggs@nku.edu', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (4, N'Sarah', N'Baker', N'sbaker@twc.net', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (5, N'Atticus', N'Finch', N'afinch@hotmail.com', N'Password', N'Salt', 2)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (6, N'Harbor', N'Welsh', N'hwelsh@gmail.com', N'Password', N'Salt', 2)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (7, N'Lucy', N'Charmagne', N'lucyloo2323@yahoo.com', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (10, N'John', N'Hamm', N'jhamm@hdyd.edu', N'Password', N'Salt', 2)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (11, N'Hannah', N'Smith', N'hsmith@aol.com', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (12, N'Hammond', N'Ornethal', N'toolegit2quit@hotmail.com', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (13, N'Joe', N'Strummer', N'jstrummer@uc.edu', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (14, N'Dan', N'Riddle', N'driddlemethis@yahoo.com', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (15, N'Betty', N'Davis', N'bdavis@twc.net', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (16, N'James', N'Woods', N'jwoods32@aol.com', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (17, N'Sarah', N'Sanders', N'ssanders@hdyd.edu', N'Password', N'Salt', 2)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (18, N'Meadow', N'Soprano', N'msoprano@lockdey.uk', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (19, N'AJ', N'Soprano', N'99bustarhyme@hotmail.com', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (20, N'Daniel', N'Worthington', N'jackreachman@yahoo.com', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (21, N'Elizabeth', N'Reggens', N'ereggins@nku.edu', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (22, N'Richard', N'Foster', N'rfoster@hdyd.edu', N'Password', N'Salt', 2)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (23, N'Dean', N'Smithers', N'dsmithers@hdyd.edu', N'Password', N'Salt', 2)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (24, N'Antonio', N'Esfandiari', N'aesfandiari@twc.net', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (25, N'Peter', N'Ansenel', N'highclassbribe55@aol.com', N'Password', N'Salt', 2)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (26, N'Francisco', N'Pizzarre', N'fpizzarre@hdyd.edu', N'Password', N'Salt', 2)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (27, N'Ben', N'Parker', N'bparker@twc.net', N'Password', N'Salt', 1)
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (28, N'Allison', N'Henrietta', N'ahenrietta@hotmail.com', N'Password', N'Salt', 1)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Assignment_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_Assignment_Course]
GO
--ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Assignment_File] FOREIGN KEY([FileId])
--REFERENCES [dbo].[File] ([FileId])
--GO
--ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_Assignment_File]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_User] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_User]
GO
ALTER TABLE [dbo].[Link]  WITH CHECK ADD  CONSTRAINT [FK_Link_Assignment] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([AssignmentId])
GO
ALTER TABLE [dbo].[Link] CHECK CONSTRAINT [FK_Link_Assignment]
GO
ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_Assignment] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([AssignmentId])
GO
ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_Assignment]
GO
ALTER TABLE [dbo].[CourseFile]  WITH CHECK ADD  CONSTRAINT [FK_CourseFile_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[CourseFile] CHECK CONSTRAINT [FK_CourseFile_Course]
GO
ALTER TABLE [dbo].[StudentAssignment]  WITH CHECK ADD  CONSTRAINT [FK_StudentAssignment_Assignment] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([AssignmentId])
GO
ALTER TABLE [dbo].[StudentAssignment] CHECK CONSTRAINT [FK_StudentAssignment_Assignment]
GO
ALTER TABLE [dbo].[StudentAssignment]  WITH CHECK ADD  CONSTRAINT [FK_StudentAssignment_User] FOREIGN KEY([StudentAssignmentId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[StudentAssignment] CHECK CONSTRAINT [FK_StudentAssignment_User]
GO
ALTER TABLE [dbo].[StudentCourse]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourse_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[StudentCourse] CHECK CONSTRAINT [FK_StudentCourse_Course]
GO
ALTER TABLE [dbo].[StudentCourse]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourse_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[StudentCourse] CHECK CONSTRAINT [FK_StudentCourse_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Roles]
GO
USE [master]
GO
ALTER DATABASE [eLearning] SET  READ_WRITE 
GO
