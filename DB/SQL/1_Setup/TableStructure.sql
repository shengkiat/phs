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
