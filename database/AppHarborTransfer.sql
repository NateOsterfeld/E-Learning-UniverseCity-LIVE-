CREATE TABLE [dbo].[User] (
    [UserId]    INT          IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (50) NOT NULL,
    [LastName]  VARCHAR (50) NOT NULL,
    [Email]     VARCHAR (50) NOT NULL,
    [Password]  VARCHAR (50) NOT NULL,
    [Salt]      VARCHAR (50) NOT NULL,
    [RoleId]    INT          NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId])
);

CREATE TABLE [dbo].[StudentCourse] (
    [UserId]   INT NOT NULL,
    [CourseId] INT NOT NULL,
    CONSTRAINT [PK_StudentCourse] PRIMARY KEY CLUSTERED ([UserId] ASC, [CourseId] ASC),
    CONSTRAINT [FK_StudentCourse_Course] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course] ([CourseId]),
    CONSTRAINT [FK_StudentCourse_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

CREATE TABLE [dbo].[StudentAssignment] (
    [StudentAssignmentId] INT           IDENTITY (1, 1) NOT NULL,
    [AssignmentId]        INT           NOT NULL,
    [FileID]              INT           NOT NULL,
    [IsCompleated]        BIT           NOT NULL,
    [Grade]               INT           NOT NULL,
    [TeacherComments]     VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Assignment] PRIMARY KEY CLUSTERED ([StudentAssignmentId] ASC, [AssignmentId] ASC),
    CONSTRAINT [FK_StudentAssignment_Assignment] FOREIGN KEY ([AssignmentId]) REFERENCES [dbo].[Assignment] ([AssignmentId]),
    CONSTRAINT [FK_StudentAssignment_User] FOREIGN KEY ([StudentAssignmentId]) REFERENCES [dbo].[User] ([UserId])
);

CREATE TABLE [dbo].[Role] (
    [RoleId]   INT          IDENTITY (1, 1) NOT NULL,
    [RoleName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

CREATE TABLE [dbo].[Link] (
    [LinkId]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (100) NOT NULL,
    [Text]         VARCHAR (100) NOT NULL,
    [URL]          VARCHAR (MAX) NOT NULL,
    [AssignmentId] INT           NOT NULL,
    CONSTRAINT [PK_Link] PRIMARY KEY CLUSTERED ([LinkId] ASC),
    CONSTRAINT [FK_Link_Assignment] FOREIGN KEY ([AssignmentId]) REFERENCES [dbo].[Assignment] ([AssignmentId])
);

CREATE TABLE [dbo].[Course] (
    [CourseId]     INT            IDENTITY (1, 1) NOT NULL,
    [Description]  VARCHAR (MAX)  NOT NULL,
    [CostUSD]      DECIMAL (18)   NOT NULL,
    [Difficulty]   INT            NOT NULL,
    [CourseName]   VARCHAR (100)  NOT NULL,
    [TeacherID]    INT            NOT NULL,
    [CourseRating] DECIMAL (2, 1) NULL,
    CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED ([CourseId] ASC),
    CONSTRAINT [FK_Course_User] FOREIGN KEY ([TeacherID]) REFERENCES [dbo].[User] ([UserId])
);

CREATE TABLE [dbo].[Assignment] (
    [AssignmentId] INT           IDENTITY (1, 1) NOT NULL,
    [OrderNumber]  INT           NOT NULL,
    [FileId]       INT           NOT NULL,
    [Instructions] VARCHAR (MAX) NULL,
    [CourseId]     INT           NOT NULL,
    CONSTRAINT [PK_Assignemnt] PRIMARY KEY CLUSTERED ([AssignmentId] ASC),
    CONSTRAINT [FK_Assignment_Course] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course] ([CourseId]),
    CONSTRAINT [FK_Assignment_File] FOREIGN KEY ([FileId]) REFERENCES [dbo].[File] ([FileId])
);

CREATE TABLE [dbo].[File](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[FilePath] [nvarchar] NULL,
	[FileSize] [int] NULL,
    CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED ([FileId] ASC)
);

GO

CREATE procedure [dbo].[spAddNewVideoFile]  
(  
@Name nvarchar(50),  
@FileSize int,  
@FilePath nvarchar(100)  
)  
as  
begin  
insert into VideoFiles (Name,FileSize,FilePath)   
values (@Name,@FileSize,@FilePath)   
end  
  
CREATE procedure [dbo].[spGetAllVideoFile]  
as  
begin  
select ID,Name,FileSize,FilePath from VideoFiles  
end 

SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (1, N'Tom', N'Holland', N'tomholland@aol.com', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (2, N'Lucas', N'Blaine', N'lucas464@gmail.com', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (3, N'Lindsey', N'Biggs', N'lbiggs@nku.edu', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (4, N'Sarah', N'Baker', N'sbaker@twc.net', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (5, N'Atticus', N'Finch', N'afinch@hotmail.com', N'Password', N'Salt', 2)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (6, N'Harbor', N'Welsh', N'hwelsh@gmail.com', N'Password', N'Salt', 2)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (7, N'Lucy', N'Charmagne', N'lucyloo2323@yahoo.com', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (10, N'John', N'Hamm', N'jhamm@hdyd.edu', N'Password', N'Salt', 2)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (11, N'Hannah', N'Smith', N'hsmith@aol.com', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (12, N'Hammond', N'Ornethal', N'toolegit2quit@hotmail.com', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (13, N'Joe', N'Strummer', N'jstrummer@uc.edu', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (14, N'Dan', N'Riddle', N'driddlemethis@yahoo.com', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (15, N'Betty', N'Davis', N'bdavis@twc.net', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (16, N'James', N'Woods', N'jwoods32@aol.com', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (17, N'Sarah', N'Sanders', N'ssanders@hdyd.edu', N'Password', N'Salt', 2)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (18, N'Meadow', N'Soprano', N'msoprano@lockdey.uk', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (19, N'AJ', N'Soprano', N'99bustarhyme@hotmail.com', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (20, N'Daniel', N'Worthington', N'jackreachman@yahoo.com', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (21, N'Elizabeth', N'Reggens', N'ereggins@nku.edu', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (22, N'Richard', N'Foster', N'rfoster@hdyd.edu', N'Password', N'Salt', 2)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (23, N'Dean', N'Smithers', N'dsmithers@hdyd.edu', N'Password', N'Salt', 2)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (24, N'Antonio', N'Esfandiari', N'aesfandiari@twc.net', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (25, N'Peter', N'Ansenel', N'highclassbribe55@aol.com', N'Password', N'Salt', 2)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (26, N'Francisco', N'Pizzarre', N'fpizzarre@hdyd.edu', N'Password', N'Salt', 2)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (27, N'Ben', N'Parker', N'bparker@twc.net', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (28, N'Allison', N'Henrietta', N'ahenrietta@hotmail.com', N'Password', N'Salt', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (29, N'NateStudent', N'Osterfeld', N'student@gmail.com', N'w/YGQRTMZ5H2MaYax4dToE/BU5E=', N'5jED2eYXvGf7CB5O4vbaUQ==', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (30, N'NateTeacher', N'Osterfeld', N'teacher@gmail.com', N'HYcvzEmHo/eel5VgDdfNCGpkHTo=', N'tjk4bsYsJVbRpVP4lzQkdg==', 2)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (31, N'Howey', N'Mandell', N'howey@gmail.com', N'd/1R2ny8Af9rjg9pOeUqCco7z5g=', N'0s5uUZDe6a6CpKs9u59dtw==', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (32, N'Simon', N'Cowell', N'simon@gmail.com', N'Fx6eEL/Z/Z/4Fs5X1j+TUY9OyAU=', N'qcg1Aeavm69pVk1PGrk4Iw==', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (33, N'Heidi', N'Klum', N'heidi@gmail.com', N'6zArDt1+OWxvED4sSrjdBhD31KE=', N'q6O2jeiMDKz9kUb/yMZSqg==', 1)
INSERT INTO [dbo].[User] ([UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId]) VALUES (34, N'Alejandro', N'Bittlebee', N'alejandro@gmail.com', N'H3fBg7cclbU4pGuR3pJ6ln4W0ik=', N'Tipj9M7wMm5YUAa3QvrpSA==', 1)
SET IDENTITY_INSERT [dbo].[User] OFF

SET IDENTITY_INSERT [dbo].[StudentCourse] ON
INSERT INTO [dbo].[StudentCourse] ([UserId], [CourseId]) VALUES (29, 2)
INSERT INTO [dbo].[StudentCourse] ([UserId], [CourseId]) VALUES (29, 4)
INSERT INTO [dbo].[StudentCourse] ([UserId], [CourseId]) VALUES (29, 13)
INSERT INTO [dbo].[StudentCourse] ([UserId], [CourseId]) VALUES (29, 14)
INSERT INTO [dbo].[StudentCourse] ([UserId], [CourseId]) VALUES (31, 13)
INSERT INTO [dbo].[StudentCourse] ([UserId], [CourseId]) VALUES (32, 13)
INSERT INTO [dbo].[StudentCourse] ([UserId], [CourseId]) VALUES (33, 7)
INSERT INTO [dbo].[StudentCourse] ([UserId], [CourseId]) VALUES (33, 13)
INSERT INTO [dbo].[StudentCourse] ([UserId], [CourseId]) VALUES (34, 8)
INSERT INTO [dbo].[StudentCourse] ([UserId], [CourseId]) VALUES (34, 13)
SET IDENTITY_INSERT [dbo].[StudentCourse] OFF

SET IDENTITY_INSERT [dbo].[Role] ON
INSERT INTO [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'Student')
INSERT INTO [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Teacher')
SET IDENTITY_INSERT [dbo].[Role] OFF

SET IDENTITY_INSERT [dbo].[File] ON
INSERT INTO [dbo].[File] ([FileId], [Name], [Path], [StorageName]) VALUES (2, N'file', N'file', N'file')
SET IDENTITY_INSERT [dbo].[File] OFF

SET IDENTITY_INSERT [dbo].[Course] ON
INSERT INTO [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (2, N' This course is a sequence of developmental math that starts with basic arithmetic, then goes
on to pre-algebra, elementary algebra, and finally intermediate algebra. The nation is facing a great problem in that
students are making it to higher education without foundational math skills.  This course will help fill that void, and help 
you become more confident in your career.', CAST(0 AS Decimal(18, 0)), 1, N'Developmental Math', 25, CAST(5.0 AS Decimal(2, 1)))
INSERT INTO [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (3, N'Archaeology, or archeology, is the study of human activity through the recovery and analysis of material culture. The archaeological record consists of artifacts, architecture, biofacts or ecofacts and cultural landscapes. Archaeology can be considered both a social science and a branch of the humanities.
In this course, you will learn the fundamentals of Archaeology, as well as the potential careers.', CAST(0 AS Decimal(18, 0)), 3, N'Intro To Archaeology', 5, CAST(4.5 AS Decimal(2, 1)))
INSERT INTO [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (4, N'This course provides an overview of macroeconomic issues such as the determination of output, employment, unemployment, interest rates, and inflation. The course introduces basic models of macroeconomics and illustrates principles with the experience of the U.S. and foreign economies', CAST(0 AS Decimal(18, 0)), 3, N'Macroeconomics', 25, CAST(4.2 AS Decimal(2, 1)))
INSERT INTO [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (5, N'This course covers selected topics in substantive criminal law: principles underlying the definition of crime such as the requirements of actus reus and mens rea and general doctrines such as ignorance of fact and ignorance of law, causation, attempt, complicity and conspiracy. Principles of justification and excuse are examined with particular attention to the doctrines of necessity, intoxication, insanity, diminished capacity and automatism. The substantive offense of homicide is extensively reviewed, and from time to time other offenses such as theft. Throughout, emphasis is placed on the basic theory of the criminal law and the relationship between doctrines and the various justifications for imposition of punishment.', CAST(0 AS Decimal(18, 0)), 4, N'Criminal Law', 22, CAST(3.6 AS Decimal(2, 1)))
INSERT INTO [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (6, N'The Sociology of Aging course provides students with a comprehensive understanding of the process of aging and older people. This course reflects the multidisciplinary
field of gerontology, which includes the historical, cultural, biological, physiological, psychological, and social aspects of aging. Our focus is the sociology of
aging with an emphasis on “aging well.', CAST(0 AS Decimal(18, 0)), 3, N'Sociology of Aging', 26, CAST(5.0 AS Decimal(2, 1)))
INSERT INTO [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (7, N'This course will consider the roles of persuasion, commentary, and criticism in contemporary American culture and will explore the techniques of these forms of expression. Students will prepare and discuss their own writing assignments, including opinion columns, editorials, and critical reviews of performances or books. Ethics and responsibilities in contemporary American journalism in expression of opinions also will be explored. Assignments will serve as the examinations in this course, which is taught by a political columnist for the South Bend Tribune who also serves as host of public affairs programs on WNIT-TV, Public Broadcasting. Open to Journalism, Ethics, and Democracy minors only. Other applicants must submit writing samples for review.', CAST(0 AS Decimal(18, 0)), 4, N'Ethics, Persuasion, and Commentary in Journalism', 10, CAST(2.2 AS Decimal(2, 1)))
INSERT INTO [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (8, N'This course provides a rigorous treatment of non-cooperative solution concepts in game theory, including rationalizability and Nash, sequential, and stable equilibria. It covers topics such as epistemic foundations, higher order beliefs, bargaining, repeated games, reputation, supermodular games, and global games. It also introduces cooperative solution concepts—Nash bargaining solution, core, Shapley value—and develops corresponding non-cooperative foundations.', CAST(0 AS Decimal(18, 0)), 4, N'Game Theory', 26, CAST(3.9 AS Decimal(2, 1)))
INSERT INTO [dbo].[Course] ([CourseId], [Description], [CostUSD], [Difficulty], [CourseName], [TeacherID], [CourseRating]) VALUES (13, N'WARNING: Please start on the ground', CAST(0 AS Decimal(18, 0)), 5, N'How To Fly', 30, NULL)
SET IDENTITY_INSERT [dbo].[Course] OFF

SET IDENTITY_INSERT [dbo].[Assignment] ON
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (4, 1, 2, N'Intro to Multiplication: Please download the file and read the instructions regarding the proper format for submission.  You
will be prompted with a series of problems.', 2)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (5, 2, 2, N'Fractions: Please download the file and read the instructions regarding the proper format for submission.  You will be presented with a 
series of problems.', 2)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (6, 3, 2, N'Decimals: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series
of problems.', 2)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (7, 1, 2, N'Archaeological Data and Concepts: Please download the file and read the instructions regarding the proper format for submission. You will be prompted with a series of questions.', 3)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (8, 2, 2, N'Surveys and Excavations: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 3)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (9, 3, 2, N'Dating and Chronologies: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 3)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (11, 2, 2, N'Role of Government in Global Economies: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 4)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (12, 3, 2, N'Inflation and Unemployment: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 4)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (13, 1, 2, N'Theories of Punishment: Please download the file and read the instructions regarding the proper format for submission. You will be prompted with a series of questions.', 5)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (14, 2, 2, N'Homicide: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 5)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (15, 3, 2, N'Exculpation: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 5)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (16, 1, 2, N'Aging and Society: Please download the file and read the instructions regarding the proper format for submission. You will be prompted with a series of questions.', 6)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (17, 2, 2, N'The Life Course Perspective: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 6)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (18, 3, 2, N'Cultural Images of Aging: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 6)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (19, 1, 2, N'Multiculturalism: Please download the file and read the instructions regarding the proper format for submission. You will be prompted with a series of questions.', 7)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (20, 2, 2, N'Media Ethics: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 7)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (21, 3, 2, N'Media History: Please download the file and read the instructions regarding the proper format for submission.  You will be prompted with a series of questions.', 7)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (22, 1, 2, N'Supermodular Games: Please download the above link and read the instructions.  You will be prompted with a series of questions, and required reading.', 8)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (23, 2, 2, N'Solutions for Static Games: Please download the above link and read the instructions.  You will be prompted with a series of questions, and required reading.', 8)
INSERT INTO [dbo].[Assignment] ([AssignmentId], [OrderNumber], [FileId], [Instructions], [CourseId]) VALUES (24, 3, 2, N'Reputation Formation: Please download the above link and read the instructions.  You will be prompted with a series of questions, and required reading.', 8)
SET IDENTITY_INSERT [dbo].[Assignment] OFF

