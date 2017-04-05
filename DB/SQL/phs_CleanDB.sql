﻿--------------------------Table Structure-----------------  
USE [phs]


IF OBJECT_ID('dbo.Person', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Person]; 
  
IF OBJECT_ID('dbo.form_field_values', 'U') IS NOT NULL 
  DROP TABLE [dbo].[form_field_values]; 
  
IF OBJECT_ID('dbo.form_form_fields', 'U') IS NOT NULL 
  DROP TABLE [dbo].[form_form_fields]; 
  
IF OBJECT_ID('dbo.form_fields', 'U') IS NOT NULL 
  DROP TABLE [dbo].[form_fields]; 
  

IF OBJECT_ID('dbo.EventModality', 'U') IS NOT NULL 
  DROP TABLE [dbo].[EventModality]; 
  
IF OBJECT_ID('dbo.ModalityForm', 'U') IS NOT NULL 
  DROP TABLE [dbo].[ModalityForm]; 
  
IF OBJECT_ID('dbo.Modality', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Modality]; 

IF OBJECT_ID('dbo.EventPatient', 'U') IS NOT NULL 
  DROP TABLE [dbo].[EventPatient]; 
  
IF OBJECT_ID('dbo.event', 'U') IS NOT NULL 
  DROP TABLE [dbo].[event]; 
  
  IF OBJECT_ID('dbo.form', 'U') IS NOT NULL 
  DROP TABLE [dbo].[form]; 


CREATE TABLE [dbo].[Person](
	[Sid] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Role] [char](1) NOT NULL,
	[ContactNumber] [nvarchar](max) NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
)

CREATE TABLE [dbo].[event](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[StartDT] [datetime2] NOT NULL,
	[EndDT] [datetime2] NOT NULL,
	[Venue] [nvarchar](max) NULL,
	[CreateBy] [nvarchar] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_event] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ModalityForm](
	[ModalityID] [int] NOT NULL,
	[FormID] [int] NOT NULL,
 CONSTRAINT [PK_ModalityForm] PRIMARY KEY CLUSTERED 
(
	[ModalityID] ASC,
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[EventModality](
	[EventID] [int] NOT NULL,
	[ModalityID] [int] NOT NULL,
 CONSTRAINT [PK_EventModality_1] PRIMARY KEY CLUSTERED 
(
	[EventID] ASC,
	[ModalityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Modality](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Position] [int] NOT NULL,
	[IconPath] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[HasParent] [bit] NULL,
	[Status] [nvarchar](max) NULL,
	[Eligiblity] [nvarchar](max) NULL,
	[Labels] [int] NULL
 CONSTRAINT [PK_Modality] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[EventPatient](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventID] [int] NOT NULL,
	[Nric] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[ContactNumber] [nvarchar](max) NULL,
	[DateOfBirth] [datetime] NULL,
	[Language] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL
 CONSTRAINT [PK_event_patient] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


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
	[EventID] [int] NULL,
	[IsPublic] [bit] NOT NULL,
	[PublicFormType] [nvarchar](50) NULL,
	[IsQuestion] [bit] NOT NULL DEFAULT 0
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
	[Label] [nvarchar](MAX) NULL,
	[Text] [nvarchar](MAX) NULL,
	[FieldType] [varchar](20) NULL CONSTRAINT [DF_form_fields_FieldType]  DEFAULT ('TEXTBOX'),
	[IsRequired] [bit] NULL CONSTRAINT [DF_form_fields_IsRequired]  DEFAULT ((0)),
	[MaxChars] [int] NULL CONSTRAINT [DF_form_fields_MaxCharacters]  DEFAULT ((50)),
	[HoverText] [nvarchar](150) NULL,
	[Hint] [nvarchar](150) NULL,
	[SubLabel] [nvarchar](50) NULL,
	[Size] [varchar](10) NULL,
	[SelectedOption] [nvarchar](50) NOT NULL,
	[AddOthersOption] [bit] NULL DEFAULT ((0)),
	[OthersOption] [nvarchar](50) NULL,
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
	[ImageBase64] [varchar](max) NULL,
	[MatrixRow] [varchar](max) NULL,
	[MatrixColumn] [varchar](max) NULL,
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

GO
ALTER TABLE [dbo].[EventModality]  WITH CHECK ADD  CONSTRAINT [FK_EventModality_event] FOREIGN KEY([EventID])
REFERENCES [dbo].[event] ([ID])
GO
ALTER TABLE [dbo].[EventModality] CHECK CONSTRAINT [FK_EventModality_event]
GO
ALTER TABLE [dbo].[EventModality]  WITH CHECK ADD  CONSTRAINT [FK_EventModality_Modality] FOREIGN KEY([ModalityID])
REFERENCES [dbo].[Modality] ([ID])
GO
ALTER TABLE [dbo].[EventModality] CHECK CONSTRAINT [FK_EventModality_Modality]
GO
ALTER TABLE [dbo].[ModalityForm]  WITH CHECK ADD  CONSTRAINT [FK_ModalityForm_form] FOREIGN KEY([FormID])
REFERENCES [dbo].[form] ([ID])
GO
ALTER TABLE [dbo].[ModalityForm] CHECK CONSTRAINT [FK_ModalityForm_form]
GO
ALTER TABLE [dbo].[ModalityForm]  WITH CHECK ADD  CONSTRAINT [FK_ModalityForm_Modality] FOREIGN KEY([ModalityID])
REFERENCES [dbo].[Modality] ([ID])
GO
ALTER TABLE [dbo].[ModalityForm] CHECK CONSTRAINT [FK_ModalityForm_Modality]
GO
ALTER TABLE [dbo].[EventPatient]  WITH CHECK ADD  CONSTRAINT [FK_EventPatient_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[EventPatient] CHECK CONSTRAINT [FK_EventPatient_Event]
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
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (2, N'Volunteer1', N't5bahHgyfjMDi7jcwE7zz+CUTW6E8VZw', N'Kent White', 1, N'V', N'C0xyFiz5b3ZacMTRPwKleI0eFaSy0x8C ', CAST(N'2016-04-13 23:17:34.010' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (3, N'Volunteer2', N'xk/xDrqvUTmp//xjd2VaxtsT0uJf9nDj', N'Eric Johnson', 1, N'V', N'0OTW6ch2NjxC1q7sIYl586hQMzftY+Wd ', CAST(N'2016-04-13 23:17:41.910' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (4, N'Volunteer3', N'oa2koZYg8TLrW4qUfykT7ilKoTgL4lEy', N'Tom Cruise', 1, N'V', N'hTJOxnbQmKLChQqOwKBv6s6BCRrE2riL ', CAST(N'2016-04-13 23:18:59.883' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (5, N'Volunteer4', N'TFQv51wShe7p5Mxinu6wEm66j7eK8AmT', N'Walker Smith', 1, N'V', N'i2k7XSN+Gi8kDpq9feHX2H7zMjVx4dGR ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (6, N'Volunteer5', N'NSr+4maxT00PDMoKNGuJ4xWo+4OoCQpe', N'Celestino Douglas', 1, N'V', N'Luni0t7I0MCTOMbt8n0bpuQUpAAIgfdh ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (7, N'Volunteer6', N'6yG3xPGAdpnlCy1UlodH4GGhNw5yEX08', N'Alexandros Hafiz', 1, N'V', N'cxxx/Zd2AKXaUaNAze+t20MF+gCnbMFZ ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (8, N'Volunteer7', N'TmHNU5R0Fpw5INQrazI/PQA+7zy5iVta', N'Dimas Channing', 1, N'V', N'xnnwmyxMXSfQtcUpnSHceSMTlJZ+VdDO ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (9, N'Volunteer8', N'm9Z8my2Kx0tAaEpWOJ3CDki2oua8bDgO', N'Codie Lucinda', 1, N'V', N'VklxCbwR/oHKBRV6npO0VmbAvbR46Sd/ ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (10, N'Volunteer9', N'KUlleZSbFADpi8n+M7pIOlCmhEZYJnR6', N'Torger Kayleen', 1, N'V', N'VJkbnSovc1BtPy04RybAo8B8sAAwTn2J ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (11, N'Volunteer10', N'MWM6zwwL1eeZxfZ3XjUeWYHWkbLM0A4C', N'Filippa Cynbel', 0, N'V', N'cZZ7m8Sj5pv4Ag7hP8ot9ZqDCHbrOooI ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (12, N'Doctor1', N'QV7JswCtrAikzyagdRArK41ZR7CHuqf7', N'Adil Hayati', 1, N'D', N'KrTW84isQkhtiMQxzbwLKfIsLmOhobSY ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (13, N'Doctor2', N'+iFZH18HLlJnGWWOWsanO5fxhVgHKIp3', N'Mette Iris', 1, N'D', N'HKjH9qgCG1tyVMVcEIJXdxtxJ5bXYufr ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (14, N'Doctor3', N'bV2Wt4MR/iKklHcxNfEl5YJd6gtwqZiH', N'Amelia Funanya', 1, N'D', N'8YQJqFAV9xUWwHiXfODFuhuDTS5ukQwt ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (15, N'Doctor4', N'B2Cw0JIvHX5auHKvd+qJawpAh0xEZFJR', N'Jurgen Sofia', 1, N'D', N'yKV616WHc2i8rUKGMXMhskBWkWXm4AQC ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (16, N'Doctor5', N'cP/4dmCYJMfCqT8zEEWCecJLZQzYVqc4', N'Athena Herbert', 1, N'D', N'7bDVnI3WlC1q2ApB+ASvPHsZwKjkvBzZ ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (17, N'Doctor6', N'eO060WBvNSFuNE2ZE3HU0yGjqQOK3+yf', N'Chrissy Ambre', 1, N'D', N'4c08nVFZyiI73vZd9P7bEuBvpvHJhYZO ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (18, N'Doctor7', N'p4ZhdGoLJKQW8sscjoP+Vkcg/F679pAH', N'Faddei Kir', 1, N'D', N'FHBd7pjdeFwdNJaRJPzxdfpg/zXWChDx ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (19, N'Doctor8', N'GucDVKmV2/8cVDywlKRr4g++4/OpJksc', N'Sima Folke', 1, N'D', N'qDzmSff2D9K2BlhQYrZ6YfGB1BETav+j ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (20, N'Doctor9', N'8KJLSQXv5bURV8U2ZU+rAI54+w7A9xYI', N'Wendelin Jaci', 1, N'D', N'zK1HYfERIWKCtu3djAlZD8SheRXI7w6B ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Person] ([Sid], [Username], [Password], [FullName], [IsActive], [Role], [PasswordSalt], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (21, N'Doctor10', N'czIthJ583ZfkcszEIEfEK+q2pWDq3ofB', N'Valencia Demeter', 0, N'D', N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ', CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Person] OFF
GO

---  Events Sample  --

GO
SET IDENTITY_INSERT [phs].[dbo].[event] ON 

GO
INSERT [phs].[dbo].[event] ([Id], [Title], [Venue], [IsActive], [StartDT], [EndDT], [CreateDate], [CreateBy]) VALUES (1, N'2015 - Event', N'PHS', 1, CAST(N'2015-04-13 10:00:00.527' AS DateTime), CAST(N'2015-04-14 12:00:00.527' AS DateTime), CAST(N'2015-04-11 10:00:00.527' AS DateTime), N'T')
GO

GO
INSERT [phs].[dbo].[event] ([Id], [Title], [Venue], [IsActive], [StartDT], [EndDT], [CreateDate], [CreateBy]) VALUES (2, N'2016 - Event', N'PHS', 1, CAST(N'2016-04-13 10:00:00.527' AS DateTime), CAST(N'2016-04-14 12:00:00.527' AS DateTime), CAST(N'2016-04-11 10:00:00.527' AS DateTime), N'T')
GO

GO
SET IDENTITY_INSERT [phs].[dbo].[event] OFF

---  Events Patient Sample  --

GO
SET IDENTITY_INSERT [phs].[dbo].[EventPatient] ON 

GO
INSERT [phs].[dbo].[EventPatient] ([Id], [EventId], [Nric], [FullName], [ContactNumber], [DateOfBirth], [Language], [Gender]) VALUES (1, 1, N'S8250369B', N'Lawrence Fay DDS', 81274563, CAST(N'1982-04-13 10:00:00.527' AS DateTime), N'English', N'Male')
GO

GO
INSERT [phs].[dbo].[EventPatient] ([Id], [EventId], [Nric], [FullName], [ContactNumber], [DateOfBirth], [Language], [Gender]) VALUES (2, 2, N'S8250369B', N'Lawrence Fay DDS', 81274563, CAST(N'1982-04-13 10:00:00.527' AS DateTime), N'English', N'Male')
GO

GO
INSERT [phs].[dbo].[EventPatient] ([Id], [EventId], [Nric], [FullName], [ContactNumber], [DateOfBirth], [Language], [Gender]) VALUES (3, 2, N'S7931278I', N'Maxwell Schulist', 69639756, CAST(N'1979-02-13 10:00:00.527' AS DateTime), N'English', N'Male')
GO

GO
SET IDENTITY_INSERT [phs].[dbo].[EventPatient] OFF


---  Modality Sample  --

GO
SET IDENTITY_INSERT [phs].[dbo].[Modality] ON 

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (1, N'Registration', 0, N'../../Content/images/Modality/01registration.png', 1, 1, 0, N'Pending')
GO

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (2, N'History Taking', 1, N'../../Content/images/Modality/02historytaking.png', 1, 1, 1, N'Pending')
GO

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (3, N'Mega Sorting Station', 2, N'../../Content/images/Modality/03megasorting.png', 1, 1, 1, N'Pending')
GO

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (4, N'Phlebotomy', 3, N'../../Content/images/Modality/04phlebo.png', 0, 0, 1, N'Pending')
GO

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (5, N'FIT', 4, N'../../Content/images/Modality/05fit.png', 0, 0, 1, N'Pending')
GO

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (6, N'Woman Cancer', 5, N'../../Content/images/Modality/06woman.png', 0, 0, 1, N'Pending')
GO

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (7, N'Geriatric Screening', 6, N'../../Content/images/Modality/07geri.png', 0, 0, 1, N'Pending')
GO

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (8, N'Oral', 7, N'../../Content/images/Modality/08oral.png', 0, 0, 1, N'Pending')
GO

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (9, N'Doctor Consult', 8, N'../../Content/images/Modality/09doctor.png', 0, 0, 0, N'Pending')
GO

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (10, N'Exhibition', 9, N'../../Content/images/Modality/10exhibition.png', 1, 0, 0, N'Pending')
GO

GO
INSERT [phs].[dbo].[Modality] ([Id], [Name], [Position], [IconPath], [IsActive], [IsVisible], [HasParent], [Status]) VALUES (11, N'Summary', 10, N'../../Content/images/Modality/11summary.png', 1, 1, 0, N'Pending')
GO

GO
SET IDENTITY_INSERT [phs].[dbo].[Modality] OFF



--- Event Modality Sample  --

GO
SET IDENTITY_INSERT [phs].[dbo].[EventModality] ON 

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 1)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 1)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 2)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 2)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 3)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 3)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 4)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 4)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 5)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 5)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 6)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 6)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 7)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 7)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 8)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 8)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 9)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 9)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 10)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 10)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (1, 11)
GO

GO
INSERT [phs].[dbo].[EventModality] ([EventId], [ModalityId]) VALUES (2, 11)
GO


GO
SET IDENTITY_INSERT [phs].[dbo].[EventModality] OFF
GO
---  Forms Sample  --


SET IDENTITY_INSERT [phs].[dbo].[form] ON 

GO
INSERT [phs].[dbo].[form] ([ID], [Title], [Slug], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsPublic], [PublicFormType], [IsQuestion]) VALUES (1, N'Registration Form', N'new-registration-form', N'DRAFT', N'Thank you for signing up', CAST(N'2017-02-19 09:15:43.527' AS DateTime), NULL, NULL, 1, NULL, 0, NULL, 0)
GO
INSERT [phs].[dbo].[form] ([ID], [Title], [Slug], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsPublic], [PublicFormType], [IsQuestion]) VALUES (2, N'New Registration Form', N'new-registration-form', N'DRAFT', N'Thank you for signing up', CAST(N'2017-03-06 13:16:46.333' AS DateTime), NULL, NULL, 0, NULL, 0, NULL, 0)
GO
INSERT [phs].[dbo].[form] ([ID], [Title], [Slug], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsPublic], [PublicFormType], [IsQuestion]) VALUES (3, N'BMI + History Taking', N'new-registration-form', N'DRAFT', N'Thank you for signing up', CAST(N'2017-03-06 13:19:35.327' AS DateTime), NULL, NULL, 1, NULL, 0, NULL, 0)
GO
INSERT [phs].[dbo].[form] ([ID], [Title], [Slug], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsPublic], [PublicFormType], [IsQuestion]) VALUES (4, N'PreRegistration Form', N'new-registration-form', N'PUBLISHED', N'Thank you for signing up', CAST(N'2017-03-10 13:08:21.207' AS DateTime), NULL, NULL, 1, NULL, 1, N'PRE-REGISTRATION', 0)
GO
INSERT [phs].[dbo].[form] ([ID], [Title], [Slug], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsPublic], [PublicFormType], [IsQuestion]) VALUES (5, N'Doctor Form', N'new-registration-form', N'DRAFT', N'Thank you for signing up', CAST(N'2017-03-26 05:46:01.207' AS DateTime), NULL, NULL, 1, NULL, 0, NULL, 0)
GO
INSERT [phs].[dbo].[form] ([ID], [Title], [Slug], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsPublic], [PublicFormType], [IsQuestion]) VALUES (6, N'Vitals', N'new-registration-form', N'DRAFT', N'Thank you for signing up', CAST(N'2017-03-26 05:47:55.413' AS DateTime), NULL, NULL, 1, NULL, 0, NULL, 0)
GO
INSERT [phs].[dbo].[form] ([ID], [Title], [Slug], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsPublic], [PublicFormType], [IsQuestion]) VALUES (7, N'Phlebotomy DataEntry', N'New Registration Form', N'DRAFT', N'Thank you for signing up', CAST(N'2017-04-05 18:47:55.413' AS DateTime), NULL, NULL, 1, NULL, 0, NULL, 0)
GO
SET IDENTITY_INSERT [phs].[dbo].[form] OFF
GO
SET IDENTITY_INSERT [dbo].[form_fields] ON 

GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (76, N'Does the participant intend to undergo phlebotomy? 要去抽血吗？', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes,No', N'', 1, 12, 18, 100, N'', CAST(N'2017-03-05 08:20:27.433' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (77, N'Check that ALL THREE of the following eligibility criteria are fulfilled. Otherwise,change the above question to "no".', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Have not done a blood test in a year,Have not been previously diagnosed with diabetes mellitus AND / OR dyslipidaemia,Have fasted for at least 8 hours prior', N'', 2, 13, 18, 100, N'', CAST(N'2017-03-05 08:21:27.443' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (78, N'<p>PHS will be printing a health report for you. What language(s) would you prefer theletter to be in? 健康报告</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'English and Chinese,English and Malay,English and Tamil,English only', N'', 3, 14, 18, 100, N'', CAST(N'2017-03-05 08:22:12.443' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (79, N'Name (as in NRIC) 姓名', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 4, 0, 18, 100, N'', CAST(N'2017-03-05 08:23:12.440' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (80, N'Gender 性别', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Male,Female', N'', 5, 1, 18, 100, N'', CAST(N'2017-03-05 08:23:27.447' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (81, N'Salutation', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Mr,Ms,Mrs,Dr', N'', 6, 2, 18, 100, N'', CAST(N'2017-03-05 08:23:57.467' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (82, N'Date of Birth 生日', N'Click to edit', N'BIRTHDAYPICKER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 3, 18, 100, N'', CAST(N'2017-03-05 08:24:42.453' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (83, N'Citizenship 国籍', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Singapore Citizen 新加坡公民,Singapore Permanent Resident (PR) 新加坡永久居民', N'', 8, 4, 18, 100, N'', CAST(N'2017-03-05 08:25:27.453' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (84, N'Race 种族', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Chinese,Malay,Indian,Others', N'', 9, 5, 18, 100, N'', CAST(N'2017-03-05 08:25:57.457' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (85, N'Language spoken by participant?', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'English,Mandarin,Malay,Tamil,Others', N'', 10, 6, 18, 100, N'', CAST(N'2017-03-05 08:26:57.483' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (86, N'Address', N'Click to edit', N'ADDRESS', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 11, 7, 18, 100, N'', CAST(N'2017-03-05 08:27:42.463' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (87, N'Housing Type 住宿', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'HDB Rental Flat,1-Room HDB Flat,2-Room HDB Flat,3-Room HDB Flat,4-Room HDB Flat,5-Room HDB Flat,Executive Flats,Condominium / Private Flats,Landed Property', N'', 12, 8, 18, 100, N'', CAST(N'2017-03-05 08:28:12.463' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (88, N'Home Number', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 13, 9, 18, 100, N'', CAST(N'2017-03-05 08:29:42.477' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (89, N'Mobile Number', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 14, 10, 18, 100, N'', CAST(N'2017-03-05 08:29:57.467' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (90, N'Highest Education Qualification 教育水平', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'No formal qualifications,Primary / PSLE,Secondary / O Levels,ITE / NITEC / Higher NITEC,Pre-U / JC / A Levels,Polytechnic / Diploma,University / Degree,Master''s, PhD and above', N'', 15, 11, 18, 100, N'', CAST(N'2017-03-05 08:30:27.470' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (92, N'<p><b>Follow Up</b></p><p>In the event of any abnormal test result, I understand that I will be contacted for follow-up usingmy contact details provided herein and that I should see a doctor if any of my test results are abnormal. I also understand that there are limitations to the screening tests and that they are not conclusive in detecting or ruling out medical risk factors or conditions. I should seek medicaladvice if I feel unwell or have any symptoms even if the test results are normal.</p><p>Depending on my test results, I may be contacted and/or referred for post-screening follow-upunder one or more of the Organisers’ screening partner service providers. I may also be referred,where appropriate, to participate in community programmes/activities organised by the HealthPromotionBoard, PA, Voluntary Welfare Organisations, constituency managers, service providersor grassroots organisations for follow-up or participation in community programmes/activities. Iunderstand that the decision to participate in the above-mentioned activities is entirely mine.</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes participant consents to the above.', N'', 17, 16, 18, 100, N'', CAST(N'2017-03-05 08:32:57.497' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (93, N'<p><b>Introduction </b><br></p><p>This is a public health screening event jointly organised by National University Health System(NUHS),People’s Association (PA), and the medical students of National University of Singapore(NUS) Yong Loo Lin School of Medicine (YLLSoM) (collectively the “Organisers”)</p><p><b>Consent to Screen</b></p><p>I consent to undergo health screening tests for one or more of the following: obesity, high bloodpressure, fasting venous blood test for diabetes and dyslipidemia, cancers (specifically breastand cervical cancer screening registration for females, colorectal cancer screening for bothgenders) and additional screening modalities (geriatrics and oral health). I understand that all mypersonal data and information will be recorded and stored in a secure and confidential manner bythe Organisers.</p><p><b>Declaration</b></p><p>I am eligible for the health screening tests offered to me. I hereby declare that all the informationIam providing is true to the best of my knowledge.</p><p><b>Collection and Use of Information<sup>[1]</sup></b></p><p>I acknowledge that my personal data and relevant screening and follow-up information, includingthe test results, will be collected and used by the Organisers for the purposes of conducting thetests and managing and implementing follow-up action arising from the test results. I alsoacknowledge that the information will be retained by PHS, NUHS, Health Promotion Board (HPB), Singapore Cancer Society (SCS), the National e-Health Records (NEHR) and the Ministry ofHealth (MOH), and that aggregate/de-identified Information may be used for research, statisticaland planning purposes.</p><p><b>Authorisation</b></p><p>I authorise the Organisers to approach other healthcare institutions/clinics which are in thepossession of my screening, follow-up, further assessment and/or treatment records to requestfor such relevant records (if any) for the purposes of patient care, treatment or clinical /programme review.</p><p><b>Disclosure of Information</b></p><p>I agree and consent to the Organisers and the healthcare organisation(s) administering the teststo disclose:</p><blockquote><p>a. My information, past screening and follow-up information to the Organisers’ authorisedpartners which may include public healthcare institutions and their affiliatedagencies,hospitals, polyclinics, doctors, laboratories, Singapore Cancer Society andother related healthcare providers; and,</p><p>b. My past screening results and follow-up information to/from the authorised partners forthe purposes of checking if I require re-screening, further tests, follow-up action and/orreferral to community programmes/activities. <br></p></blockquote><p>[1] Including data collected underBreastScreen Singapore, CervicalScreen Singapore, National Colorectal CancerScreening Programme,HPB’s Integrated Screening Programme, Community FunctionalScreening Programme and other screening programmes conducted by HPB and itsauthorised partners.[1] Including data collected under BreastScreen Singapore, CervicalScreen Singapore, NationalColorectal Cancer Screening Programme,HPB’s Integrated Screening Programme, CommunityFunctional Screening Programme and other screening programmes conducted by HPB and itsauthorised partners.</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes participant consents to the above.', N'', 18, 15, 18, 100, N'', CAST(N'2017-03-05 08:43:27.550' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (94, N'<p><b>Follow Up</b><br></p><p>I understand that I may be contacted regardless of my results for administrative reasons, but suchcommunications will only be made when absolutely necessary. I also understand that I will receive periodic SMS updates with health tips and advice on healthy living, as well as remindersto visit my primary care doctor for follow-up care.</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Participant consents to receiving periodic SMS updates with health tips and advice on healthy living as well as reminders to visit my primary care doctor for follow-up care.', N'', 19, 17, 18, 100, N'', CAST(N'2017-03-05 08:44:57.480' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (103, N'<p><b>Compliance to Personal Data Protection Act (PDPA)</b></p><p>I hereby give my consent to the Public Health Service Executive Committee 
to collect my personal information for the purpose of recruitment for 
the Public Health Service (hereby called “PHS”) and its related events, 
and to contact me via calls, SMS, text messages or<br>emails regarding 
the event and follow-up process. Should you wish to withdraw your 
consent for us to contact you for the purposes stated above, please 
notify a member of the PHS Executive Committee at ask.phs@gmail.com in 
writing. We will then remove your personal information from our 
database. Please allow 3 business days for your withdrawal of consent to
 take effect. All personal information will be kept confidential, will 
only be disseminated to members of the PHS Executive Committee, and will
 be strictly used by these parties for the purposes stated.<b><br></b></p>', N'Click to edit', N'RADIOBUTTON', 1, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes', N'', 20, 18, 18, 100, N'', CAST(N'2017-03-06 12:10:28.377' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (104, N'Click to edit', N'Click to edit', N'MATRIX', 0, 50, N'', N'', N'', N'', N'option1', NULL, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', CAST(N'2017-03-06 13:17:01.830' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, NULL, NULL, NULL)
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (113, N'Click to Health Concerns
If the participant has any concern(s), please take a brief history. (Please write NIL if
otherwise).
E.g."Is there any health issues there''s worrying you right now?"
"最近有没有哪里不舒服？”
Please advise that there will be no diagnosis or prescription made at our screening.
Kindly advise the participant to see a GP/polyclinic instead if he/she is expecting
treatment for their problems.', N'Click to edit', N'TEXTAREA', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', CAST(N'2017-03-08 13:46:50.953' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL)
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (115, N'Past Medical History (select all that applies)', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'NIL,Hypertension 高血压,Diabetes Mellitus 糖尿病,Hyperlipidemia 高血脂／高胆固醇,Ischemic Heart Disease (including Coronary Heart Diseases) 缺血性心脏病（包括心脏血管阻塞）,Stroke/TIA 中风', N'', 3, 1, 18, 100, N'', CAST(N'2017-03-08 13:51:42.713' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL)
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (116, N'Personal cancer history: (select all that applies)', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'NIL,Colorectal Cancer 大肠癌,Breast Cancer 乳癌,Cervical Cancer 子宫颈癌', N'', 4, 2, 18, 100, N'', CAST(N'2017-03-08 13:54:54.303' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL)
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (117, N'Family History (select all that applies, only for first degree family members)', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'NIL,Hypertension 高血压,Diabetes Mellitus 糖尿病,Hyperlipidemia 高血脂／高胆固醇,Ischemic Heart Disease (including Coronary Artery Diseases) 缺血性心脏病（包括心脏血管阻塞）,Stroke/TIA 中风', N'', 6, 3, 18, 100, N'', CAST(N'2017-03-08 13:56:09.310' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL)
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (118, N'Family cancer history (select all that applies, only for first degree family members)', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'NIL,Colorectal Cancer 大肠癌,Breast Cancer 乳癌,Cervical Cancer 子宫颈癌', N'', 5, 4, 18, 100, N'', CAST(N'2017-03-08 13:56:09.327' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL)
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (119, N'Are you currently smoking?', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes,No', N'', 7, 5, 18, 100, N'', CAST(N'2017-03-08 13:58:39.320' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL)
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (120, N'Number of Pack Years:

Pack Years = (Number of sticks per day / 20 ) x Number of years smoking', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 8, 6, 18, 100, N'', CAST(N'2017-03-08 14:00:09.327' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL)
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (121, N'<p>Have you smoked before?</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes,No', N'', 9, 7, 18, 100, N'', CAST(N'2017-03-08 14:00:39.313' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL)
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (125, N'NRIC', N'Click to edit', N'NRICPICKER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 1, 18, 100, N'', CAST(N'2017-03-18 10:00:21.747' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (126, N'Gender', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Male,Female', N'', 3, 3, 18, 100, N'', CAST(N'2017-03-18 10:00:21.817' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (127, N'Address', N'Click to edit', N'ADDRESS', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 2, 9, 18, 100, N'', CAST(N'2017-03-18 10:00:21.820' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (128, N'Name (as in NRIC)', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 4, 0, 18, 100, N'', CAST(N'2017-03-25 20:45:27.263' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (129, N'Salutation', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Mr,Ms,Mrs,Dr', N'', 5, 2, 18, 100, N'', CAST(N'2017-03-25 20:45:57.273' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (130, N'Date of Birth', N'Click to edit', N'BIRTHDAYPICKER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 6, 4, 18, 100, N'', CAST(N'2017-03-25 20:46:42.260' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (131, N'Citizenship', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Singapore Citizen,Singapore Permanent Resident (PR)', N'', 7, 5, 18, 100, N'', CAST(N'2017-03-25 20:47:27.283' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (132, N'Race', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'Chinese,Malay,Indian', N'', 8, 6, 18, 100, N'', CAST(N'2017-03-25 20:48:57.250' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (133, N'Language spoken by participant?', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'English,Mandarin,Malay,Tamil', N'', 9, 7, 18, 100, N'', CAST(N'2017-03-25 20:49:42.263' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (134, N'Prefered Time Slot', N'Click to edit', N'DROPDOWNLIST', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Morning,Afternoon', N'', 10, 8, 18, 100, N'', CAST(N'2017-03-25 20:50:27.263' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (135, N'Does this patient require follow-up?', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes,No', N'', 1, 1, 18, 100, N'', CAST(N'2017-03-26 05:46:21.917' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (136, N'<p><span style="color: rgb(0, 0, 0); font-family: &quot;Times New Roman&quot;; font-size: medium; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Memo/Referral Letter</span></p>', N'Click to edit', N'DOCTORMEMO', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 2, 2, 18, 100, N'', CAST(N'2017-03-26 05:46:36.860' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (137, N'BMI', N'Click to edit', N'BMI', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', CAST(N'2017-03-26 05:48:10.863' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (138, N'1st Systolic Reading', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 2, 1, 18, 100, N'', CAST(N'2017-03-26 05:50:25.873' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (139, N'1st Diastolic Reading', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 4, 2, 18, 100, N'', CAST(N'2017-03-26 05:50:55.863' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (140, N'2nd Systolic Reading', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 6, 3, 18, 100, N'', CAST(N'2017-03-26 05:50:55.877' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (141, N'2nd Diastolic Reading', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 5, 4, 18, 100, N'', CAST(N'2017-03-26 05:50:55.877' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (142, N'3rd Diastolic Reading (if needed)', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 3, 6, 18, 100, N'', CAST(N'2017-03-26 05:50:55.880' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (143, N'3rd Systolic Reading (if needed)', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 8, 5, 18, 100, N'', CAST(N'2017-03-26 05:51:40.873' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (144, N'Average Diastolic Reading (of all readings)', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 8, 18, 100, N'', CAST(N'2017-03-26 05:51:40.873' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (145, N'Average Systolic Reading (of all readings)', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 9, 7, 18, 100, N'', CAST(N'2017-03-26 05:51:55.867' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (147, N'<p><b>Compliance to Personal Data Protection Act (PDPA)</b><br>I hereby give my consent to the Public Health Service Executive Committee to collect my personal information for the purpose of recruitment for the Public Health Service (hereby called “PHS”) and its related events, and to contact me via calls, SMS, text messages or emails regarding the event and follow-up process. Should you wish to withdraw your consent for us to contact you for the purposes stated above, please notify a member of the PHS Executive Committee at ask.phs@gmail.com in writing. We will then remove your personal information from our database. Please allow 3 business days for your withdrawal of consent to take effect. All personal information will be kept confidential, will only be disseminated to members of the PHS Executive Committee, and will be strictly used by these parties for the purposes stated.</p>', N'Click to edit', N'RADIOBUTTON', 1, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes', N'', 11, 10, 18, 100, N'', CAST(N'2017-04-01 15:11:40.763' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (150, N'Click to edit', N'Participant is being referred from: HX Taking Modality', N'HEADER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 3, 0, 18, 100, N'', CAST(N'2017-04-01 15:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (151, N'PatientID', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', CAST(N'2017-04-05 19:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (152, N'Name', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 2, 1, 18, 100, N'', CAST(N'2017-04-05 19:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (153, N'First Name', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 3, 2, 18, 100, N'', CAST(N'2017-04-05 19:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (154, N'DOB', N'Click to edit', N'BIRTHDAYPICKER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 4, 3, 18, 100, N'', CAST(N'2017-04-05 19:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
INSERT [dbo].[form_fields] ([ID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn]) VALUES (155, N'<p>Sex</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Male,Female', N'', 5, 4, 18, 100, N'', CAST(N'2017-04-05 19:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'')
GO
SET IDENTITY_INSERT [dbo].[form_fields] OFF
GO

INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 76)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 77)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 78)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 79)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 80)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 81)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 82)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 83)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 84)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 85)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 86)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 87)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 88)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 89)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 90)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 92)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 93)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 94)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (1, 103)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (2, 104)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (3, 113)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (3, 115)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (3, 116)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (3, 117)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (3, 118)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (3, 119)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (3, 120)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (3, 121)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 125)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 126)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 127)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 128)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 129)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 130)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 131)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 132)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 133)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 134)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (4, 147)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (5, 135)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (5, 136)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (5, 150)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (6, 137)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (6, 138)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (6, 139)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (6, 140)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (6, 141)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (6, 142)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (6, 143)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (6, 144)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (6, 145)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (7, 151)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (7, 152)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (7, 153)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (7, 154)
GO
INSERT [dbo].[form_form_fields] ([formId], [fieldId]) VALUES (7, 155)
GO
SET IDENTITY_INSERT [dbo].[form_field_values] ON 

GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (4, 125, N'74ce535d-af74-4db5-a745-9af7e6a41cef', N'S8939505D', CAST(N'2017-03-24 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (5, 126, N'74ce535d-af74-4db5-a745-9af7e6a41cef', N'Male', CAST(N'2017-03-24 14:30:56.520' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (6, 127, N'74ce535d-af74-4db5-a745-9af7e6a41cef', N'{"Id":null,"Blk":"BUKIT BATOK CENTRAL","Unit":"","StreetAddress":"BUKIT BATOK CENTRAL","State":null,"ZipCode":"650625","Country":null,"Longitude":null,"Latitude":null}', CAST(N'2017-03-24 14:30:56.523' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (300, 151, N'abc5a11a-f526-4616-8e51-853d2d796926', N'S8250369B', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (301, 152, N'abc5a11a-f526-4616-8e51-853d2d796926', N'Lawrence DDS', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (302, 153, N'abc5a11a-f526-4616-8e51-853d2d796926', N'Fay', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (303, 154, N'abc5a11a-f526-4616-8e51-853d2d796926', N'13/4/1982 12:00:00 AM', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (304, 155, N'abc5a11a-f526-4616-8e51-853d2d796926', N'Male', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (305, 151, N'b611fbb9-4cbf-4414-959f-a7a95d39a0d3', N'S7931278I', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (306, 152, N'b611fbb9-4cbf-4414-959f-a7a95d39a0d3', N'Maxwell', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (307, 153, N'b611fbb9-4cbf-4414-959f-a7a95d39a0d3', N'Schulist', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (308, 154, N'b611fbb9-4cbf-4414-959f-a7a95d39a0d3', N'13/2/1979 12:00:00 AM', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[form_field_values] ([ID], [FieldId], [EntryId], [Value], [DateAdded]) VALUES (309, 155, N'b611fbb9-4cbf-4414-959f-a7a95d39a0d3', N'Male', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[form_field_values] OFF
GO

--- Modality forms Sample  --

GO
SET IDENTITY_INSERT [phs].[dbo].[ModalityForm] ON 

GO
INSERT [phs].[dbo].[ModalityForm] ([ModalityID], [FormID]) VALUES (1, 1)
GO

GO
INSERT [phs].[dbo].[ModalityForm] ([ModalityID], [FormID]) VALUES (2, 3)
GO

GO
INSERT [phs].[dbo].[ModalityForm] ([ModalityID], [FormID]) VALUES (2, 6)
GO

GO
INSERT [phs].[dbo].[ModalityForm] ([ModalityID], [FormID]) VALUES (9, 5)
GO

GO
SET IDENTITY_INSERT [phs].[dbo].[ModalityForm