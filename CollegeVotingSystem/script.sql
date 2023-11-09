USE [master]
GO
/****** Object:  Database [dbCollegeVotingSystem]    Script Date: 09/11/2023 10:39:08 pm ******/
CREATE DATABASE [dbCollegeVotingSystem]
GO
USE [dbCollegeVotingSystem]
GO
/****** Object:  StoredProcedure [dbo].[tbl_Course_Proc]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tbl_Course_Proc]
@Type VARCHAR(50),
@Search VARCHAR(max) = null,
@ID int = null,
@Course varchar(5000) = null,
@Active bit = null,
@Timestamp datetime = null
AS
BEGIN
IF @Type = 'Create'
BEGIN
	IF (SELECT COUNT(*) FROM tbl_Course WHERE Course = @Course) >= 1
	BEGIN
		UPDATE [tbl_Course] SET Active = 1 WHERE Course = @Course
	END
	ELSE
	BEGIN
		INSERT INTO [tbl_Course] ([Course]) VALUES (@Course)
	END
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Update'
BEGIN
	UPDATE [tbl_Course] SET [Course] = @Course WHERE [ID] = @ID
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Search'
BEGIN
	SELECT * FROM [tbl_Course] WHERE Active = 1
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Find'
BEGIN
	SELECT * FROM [tbl_Course] WHERE  ID = @ID
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Delete'
BEGIN
	UPDATE [tbl_Course] SET Active = 0 WHERE [ID] = @ID
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------

END


GO
/****** Object:  StoredProcedure [dbo].[tbl_Position_Proc]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tbl_Position_Proc]
@Type VARCHAR(50),
@Search VARCHAR(max) = null,
@ID int = null,
@Position varchar(5000) = null,
@Active bit = null,
@Timestamp datetime = null
AS
BEGIN
IF @Type = 'Create'
BEGIN
	IF (SELECT COUNT(*) FROM [tbl_Position] WHERE Position = @Position) >= 1
	BEGIN
		UPDATE [tbl_Position] SET [Position] = @Position WHERE Position = @Position
	END
	ELSE
	BEGIN
		INSERT INTO [tbl_Position] ([Position]) VALUES (@Position)
	END
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Update'
BEGIN
UPDATE [tbl_Position] SET [Position] = @Position WHERE [ID] = @ID
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Search'
BEGIN
SELECT * FROM [tbl_Position] WHERE Active = 1
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Find'
BEGIN
SELECT * FROM [tbl_Position] WHERE  ID = @ID
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Delete'
BEGIN
UPDATE [tbl_Position] SET Active = 0 WHERE [ID] = @ID
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
END


GO
/****** Object:  StoredProcedure [dbo].[tbl_User_Proc]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tbl_User_Proc]
@Type VARCHAR(50),
@Search VARCHAR(max) = null,
@ID int = null,
@Username varchar(5000) = null,
@Password varchar(5000) = null,
@Role int = null,
@Active bit = null,
@Fingerprint1 varbinary(max) = null,
@StudentID varchar(5000) = null,
@fname varchar(5000) = null,
@mn varchar(5000) = null,
@lname varchar(5000) = null,
@Img varbinary(max) = null,
@Gender varchar(5000) = null,
@bday datetime = null,
@Course int = null,
@Timestamp datetime = null
AS
BEGIN
IF @Type = 'Create'
BEGIN
INSERT INTO [tbl_User]
([Username],[Password],[Role],[StudentID],[fname],[mn],[lname],[Img],[Gender],[bday],[Course])
VALUES
(@Username,@Password, 2,@StudentID,@fname,@mn,@lname,@Img,@Gender,@bday,@Course)

END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Update'
BEGIN
UPDATE [tbl_User] SET [Username] = @Username
,[Password] = @Password
,[StudentID] = @StudentID
,[fname] = @fname
,[mn] = @mn
,[lname] = @lname
,[Img] = @Img
,[Gender] = @Gender
,[bday] = @bday
,[Course] = @Course WHERE [ID] = @ID
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Search'
BEGIN
SELECT * FROM [vw_User] 
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Find'
BEGIN
SELECT * FROM [vw_User] WHERE  ID = @ID
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
IF @Type = 'Login'
BEGIN
	SELECT * FROM [tbl_User] WHERE HASHBYTES('MD5', Username) = HASHBYTES('MD5', @Username) AND HASHBYTES('MD5', [Password]) = HASHBYTES('MD5', @Password)
END
--------------------------------------------------------------------------------------------------------------------------------------------------------------
END


GO
/****** Object:  Table [dbo].[tbl_Candidate]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Candidate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ElectionID] [int] NULL,
	[UserID] [int] NULL,
	[Position] [int] NULL,
	[Plataforma] [varchar](max) NULL,
	[Active] [bit] NULL,
	[Timestamp] [datetime] NULL,
 CONSTRAINT [PK__tbl_Cand__3214EC27224D250D] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Course]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Course](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Course] [varchar](5000) NULL,
	[Active] [bit] NULL,
	[Timestamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Election]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Election](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](max) NULL,
	[ElectionDate] [datetime] NULL,
	[Timestamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Position]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Position](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Position] [varchar](5000) NULL,
	[Active] [bit] NULL,
	[Timestamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_UnverifiedVote]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_UnverifiedVote](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ElectionID] [int] NULL,
	[UserID] [int] NULL,
	[Candidate] [int] NULL,
	[Timestamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_User]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](5000) NULL,
	[Password] [varchar](5000) NULL,
	[Role] [int] NULL,
	[Active] [bit] NULL,
	[Fingerprint1] [varbinary](max) NULL,
	[StudentID] [varchar](5000) NULL,
	[fname] [varchar](5000) NULL,
	[mn] [varchar](5000) NULL,
	[lname] [varchar](5000) NULL,
	[Img] [varbinary](max) NULL,
	[Gender] [varchar](5000) NULL,
	[bday] [datetime] NULL,
	[Course] [int] NULL,
	[Timestamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Vote]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Vote](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ElectionID] [int] NULL,
	[UserID] [int] NULL,
	[Candidate] [int] NULL,
	[Timestamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vw_User]    Script Date: 09/11/2023 10:39:08 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_User]
AS
SELECT [ID]
      ,[Username]
      ,[Password]
      ,[Role]
      ,[Active]
      ,[Fingerprint1]
      ,[StudentID]
      ,[fname]
      ,[mn]
      ,[lname]
	  ,Fullname = CONCAT(fname, ' ', mn, ' ', lname)
      ,[Img]
      ,[Gender]
      ,[bday]
	  ,Age = DATEDIFF(YEAR, bday, GETDATE())
      ,[Course]
      ,[Timestamp]
  FROM [tbl_User]


GO
ALTER TABLE [dbo].[tbl_Candidate] ADD  CONSTRAINT [DF__tbl_Candi__Activ__1BFD2C07]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tbl_Candidate] ADD  CONSTRAINT [DF__tbl_Candi__Times__1CF15040]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[tbl_Course] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tbl_Course] ADD  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[tbl_Election] ADD  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[tbl_Position] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tbl_Position] ADD  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[tbl_UnverifiedVote] ADD  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[tbl_User] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tbl_User] ADD  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[tbl_Vote] ADD  DEFAULT (getdate()) FOR [Timestamp]
GO
USE [master]
GO
ALTER DATABASE [dbCollegeVotingSystem] SET  READ_WRITE 
GO
