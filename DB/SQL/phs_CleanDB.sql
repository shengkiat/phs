--------------------------Table Structure-----------------  
USE [phs]


IF OBJECT_ID('dbo.Person', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Person]; 
  
IF OBJECT_ID('dbo.form_field_values', 'U') IS NOT NULL 
  DROP TABLE [dbo].[form_field_values]; 
  
IF OBJECT_ID('dbo.form_form_fields', 'U') IS NOT NULL 
  DROP TABLE [dbo].[form_form_fields]; 
  
IF OBJECT_ID('dbo.form_fields', 'U') IS NOT NULL 
  DROP TABLE [dbo].[form_fields]; 
  
IF OBJECT_ID('dbo.form', 'U') IS NOT NULL 
  DROP TABLE [dbo].[form]; 
  
  
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


CREATE TABLE [dbo].[form](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Slug] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[ConfirmationMessage] [nvarchar](300) NOT NULL,
	[DateAdded] [datetime] NULL,
	[Theme] [nvarchar](50) NULL,
	[NotificationEmail] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_form] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[form_field_values](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FieldId] [int] NOT NULL,
	[EntryId] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](500) NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_form_field_values] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[form_fields](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](50) NULL,
	[Text] [nvarchar](100) NULL,
	[FieldType] [varchar](20) NULL CONSTRAINT [DF_form_fields_FieldType]  DEFAULT ('TEXTBOX'),
	[IsRequired] [bit] NULL CONSTRAINT [DF_form_fields_IsRequired]  DEFAULT ((0)),
	[MaxChars] [int] NULL CONSTRAINT [DF_form_fields_MaxCharacters]  DEFAULT ((50)),
	[HoverText] [nvarchar](150) NULL,
	[Hint] [nvarchar](150) NULL,
	[SubLabel] [nvarchar](50) NULL,
	[Size] [varchar](10) NULL,
	[SelectedOption] [nvarchar](50) NOT NULL,
	[Columns] [int] NULL,
	[Rows] [int] NULL,
	[Options] [nvarchar](300) NULL,
	[Validation] [varchar](50) NULL,
	[DomId] [int] NULL CONSTRAINT [DF_form_fields_DomId]  DEFAULT ((0)),
	[Order] [int] NULL CONSTRAINT [DF_form_fields_Order]  DEFAULT ((0)),
	[MinimumAge] [int] NULL,
	[MaximumAge] [int] NULL,
	[HelpText] [nvarchar](50) NULL,
	[DateAdded] [datetime] NOT NULL CONSTRAINT [DF_form_fields_DateAdded]  DEFAULT (getutcdate()),
	[MaxFilesizeInKb] [int] NULL CONSTRAINT [DF_form_fields_MaxFilesizeInKb]  DEFAULT ((5000)),
	[ValidFileExtensions] [nvarchar](50) NULL,
	[MinFilesizeInKb] [int] NULL CONSTRAINT [DF_form_fields_MinFilesizeInKb]  DEFAULT ((50)),
 CONSTRAINT [PK_form_fields] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[form_form_fields](
	[formId] [int] NOT NULL,
	[fieldId] [int] NOT NULL,
 CONSTRAINT [PK_form_form_fields] PRIMARY KEY CLUSTERED 
(
	[formId] ASC,
	[fieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[form_field_values] ADD  CONSTRAINT [DF_form_field_values_DateAdded]  DEFAULT (getutcdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[form_field_values]  WITH CHECK ADD  CONSTRAINT [FK_form_field_values_form_fields] FOREIGN KEY([FieldId])
REFERENCES [dbo].[form_fields] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[form_field_values] CHECK CONSTRAINT [FK_form_field_values_form_fields]
GO
ALTER TABLE [dbo].[form_form_fields]  WITH CHECK ADD  CONSTRAINT [FK form_fields_form_form_fields] FOREIGN KEY([fieldId])
REFERENCES [dbo].[form_fields] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[form_form_fields] CHECK CONSTRAINT [FK form_fields_form_form_fields]
GO
ALTER TABLE [dbo].[form_form_fields]  WITH CHECK ADD  CONSTRAINT [FK_form_fields] FOREIGN KEY([formId])
REFERENCES [dbo].[form] ([ID])
GO
ALTER TABLE [dbo].[form_form_fields] CHECK CONSTRAINT [FK_form_fields]
GO
--------------------------Data--------------------- 


---  [dbo].[Person]  --
DELETE FROM  [dbo].[Person] 
GO

GO
SET IDENTITY_INSERT [dbo].[Person] ON 

GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (1, N'Admin1', N'WG7qLtt1uUZI8IS49HuPRMXk14RdyGCE', N'Super Admin', 1, N'A', N'f+3W4Sgya5jto3JLOKiwC3TJtOPs7yhC', CAST(N'2016-04-13 23:15:17.527' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (2, N'Instructor1', N't5bahHgyfjMDi7jcwE7zz+CUTW6E8VZw', N'Kent White', 1, N'I', N'C0xyFiz5b3ZacMTRPwKleI0eFaSy0x8C ', CAST(N'2016-04-13 23:17:34.010' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (3, N'Instructor2', N'xk/xDrqvUTmp//xjd2VaxtsT0uJf9nDj', N'Eric Johnson', 1, N'I', N'0OTW6ch2NjxC1q7sIYl586hQMzftY+Wd ', CAST(N'2016-04-13 23:17:41.910' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (4, N'Instructor3', N'oa2koZYg8TLrW4qUfykT7ilKoTgL4lEy', N'Tom Cruise', 1, N'I', N'hTJOxnbQmKLChQqOwKBv6s6BCRrE2riL ', CAST(N'2016-04-13 23:18:59.883' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (5, N'Instructor4', N'TFQv51wShe7p5Mxinu6wEm66j7eK8AmT', N'Walker Smith', 1, N'I', N'i2k7XSN+Gi8kDpq9feHX2H7zMjVx4dGR ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (6, N'Instructor5', N'NSr+4maxT00PDMoKNGuJ4xWo+4OoCQpe', N'Celestino Douglas', 1, N'I', N'Luni0t7I0MCTOMbt8n0bpuQUpAAIgfdh ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (7, N'Instructor6', N'6yG3xPGAdpnlCy1UlodH4GGhNw5yEX08', N'Alexandros Hafiz', 1, N'I', N'cxxx/Zd2AKXaUaNAze+t20MF+gCnbMFZ ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (8, N'Instructor7', N'TmHNU5R0Fpw5INQrazI/PQA+7zy5iVta', N'Dimas Channing', 1, N'I', N'xnnwmyxMXSfQtcUpnSHceSMTlJZ+VdDO ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (9, N'Instructor8', N'm9Z8my2Kx0tAaEpWOJ3CDki2oua8bDgO', N'Codie Lucinda', 1, N'I', N'VklxCbwR/oHKBRV6npO0VmbAvbR46Sd/ ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (10, N'Instructor9', N'KUlleZSbFADpi8n+M7pIOlCmhEZYJnR6', N'Torger Kayleen', 1, N'I', N'VJkbnSovc1BtPy04RybAo8B8sAAwTn2J ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (11, N'Instructor10', N'MWM6zwwL1eeZxfZ3XjUeWYHWkbLM0A4C', N'Filippa Cynbel', 0, N'I', N'cZZ7m8Sj5pv4Ag7hP8ot9ZqDCHbrOooI ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (12, N'Student1', N'QV7JswCtrAikzyagdRArK41ZR7CHuqf7', N'Adil Hayati', 1, N'S', N'KrTW84isQkhtiMQxzbwLKfIsLmOhobSY ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (13, N'Student2', N'+iFZH18HLlJnGWWOWsanO5fxhVgHKIp3', N'Mette Iris', 1, N'S', N'HKjH9qgCG1tyVMVcEIJXdxtxJ5bXYufr ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (14, N'Student3', N'bV2Wt4MR/iKklHcxNfEl5YJd6gtwqZiH', N'Amelia Funanya', 1, N'S', N'8YQJqFAV9xUWwHiXfODFuhuDTS5ukQwt ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (15, N'Student4', N'B2Cw0JIvHX5auHKvd+qJawpAh0xEZFJR', N'Jurgen Sofia', 1, N'S', N'yKV616WHc2i8rUKGMXMhskBWkWXm4AQC ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (16, N'Student5', N'cP/4dmCYJMfCqT8zEEWCecJLZQzYVqc4', N'Athena Herbert', 1, N'S', N'7bDVnI3WlC1q2ApB+ASvPHsZwKjkvBzZ ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (17, N'Student6', N'eO060WBvNSFuNE2ZE3HU0yGjqQOK3+yf', N'Chrissy Ambre', 1, N'S', N'4c08nVFZyiI73vZd9P7bEuBvpvHJhYZO ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (18, N'Student7', N'p4ZhdGoLJKQW8sscjoP+Vkcg/F679pAH', N'Faddei Kir', 1, N'S', N'FHBd7pjdeFwdNJaRJPzxdfpg/zXWChDx ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (19, N'Student8', N'GucDVKmV2/8cVDywlKRr4g++4/OpJksc', N'Sima Folke', 1, N'S', N'qDzmSff2D9K2BlhQYrZ6YfGB1BETav+j ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (20, N'Student9', N'8KJLSQXv5bURV8U2ZU+rAI54+w7A9xYI', N'Wendelin Jaci', 1, N'S', N'zK1HYfERIWKCtu3djAlZD8SheRXI7w6B ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (21, N'Student10', N'czIthJ583ZfkcszEIEfEK+q2pWDq3ofB', N'Valencia Demeter', 0, N'S', N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
