USE [phs]


IF OBJECT_ID('dbo.Person', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Person]; 

CREATE TABLE [dbo].[Person](
	[Sid] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Role] [char](1) NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
)