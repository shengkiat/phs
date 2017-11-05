--------------------------Table Structure-----------------  
USE [phs]

IF OBJECT_ID('dbo.ParticipantCallerMapping', 'U') IS NOT NULL 
  DROP TABLE [dbo].[ParticipantCallerMapping];

IF OBJECT_ID('dbo.Person', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Person];

IF OBJECT_ID('dbo.FollowUpGroup', 'U') IS NOT NULL 
  DROP TABLE [dbo].FollowUpGroup;

IF OBJECT_ID('dbo.FollowUpConfiguration', 'U') IS NOT NULL 
  DROP TABLE [dbo].FollowUpConfiguration;

IF OBJECT_ID('dbo.Summary', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Summary]; 

IF OBJECT_ID('dbo.ParticipantJourneyModality', 'U') IS NOT NULL 
  DROP TABLE [dbo].[ParticipantJourneyModality]; 

IF OBJECT_ID('dbo.PHSUser', 'U') IS NOT NULL 
  DROP TABLE [dbo].[PHSUser]; 
  
IF OBJECT_ID('dbo.FormFieldValue', 'U') IS NOT NULL 
  DROP TABLE [dbo].[FormFieldValue]; 
  
IF OBJECT_ID('dbo.FormFormField', 'U') IS NOT NULL 
  DROP TABLE [dbo].[FormFormField]; 
  
IF OBJECT_ID('dbo.FormField', 'U') IS NOT NULL 
  DROP TABLE [dbo].[FormField]; 
  
IF OBJECT_ID('dbo.TemplateFieldValue', 'U') IS NOT NULL 
  DROP TABLE [dbo].[TemplateFieldValue]; 
  
IF OBJECT_ID('dbo.TemplateTemplateField', 'U') IS NOT NULL 
  DROP TABLE [dbo].[TemplateTemplateField]; 
  
IF OBJECT_ID('dbo.TemplateField', 'U') IS NOT NULL 
  DROP TABLE [dbo].[TemplateField];   
  
IF OBJECT_ID('dbo.EventModality', 'U') IS NOT NULL 
  DROP TABLE [dbo].[EventModality]; 
  
IF OBJECT_ID('dbo.ModalityForm', 'U') IS NOT NULL 
  DROP TABLE [dbo].[ModalityForm]; 
  
IF OBJECT_ID('dbo.Modality', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Modality]; 

IF OBJECT_ID('dbo.EventPatient', 'U') IS NOT NULL 
  DROP TABLE [dbo].[EventPatient]; 
  
IF OBJECT_ID('dbo.ParticipantPHSEvent', 'U') IS NOT NULL 
  DROP TABLE [dbo].[ParticipantPHSEvent]; 
  
IF OBJECT_ID('dbo.Participant', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Participant]; 
  
IF OBJECT_ID('dbo.PreRegistration', 'U') IS NOT NULL 
  DROP TABLE [dbo].[PreRegistration]; 
  
IF OBJECT_ID('dbo.PHSEvent', 'U') IS NOT NULL 
  DROP TABLE [dbo].[PHSEvent]; 

IF OBJECT_ID('dbo.Template', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Template]; 
  
IF OBJECT_ID('dbo.Form', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Form];

IF OBJECT_ID('dbo.ReferenceRange', 'U') IS NOT NULL 
  DROP TABLE [dbo].[ReferenceRange];   
  
IF OBJECT_ID('dbo.StandardReference', 'U') IS NOT NULL 
  DROP TABLE [dbo].[StandardReference]; 

IF OBJECT_ID('dbo.AuditLog', 'U') IS NOT NULL 
  DROP TABLE [dbo].[AuditLog]; 

IF OBJECT_ID('dbo.SummaryMapping', 'U') IS NOT NULL 
  DROP TABLE [dbo].[SummaryMapping];
  
CREATE TABLE [dbo].[AuditLog](
	[AuditLogID] [int] IDENTITY(1,1) NOT NULL,
	[PHSUserID] [int] NOT NULL,
	[AuditDateTime] [datetime2](7) NOT NULL,
	[AuditState] [nchar](10) NOT NULL,
	[TableName] [nchar](100) NOT NULL,
	[RecordID] [nvarchar](max) NULL,
	[ColumnName] [nchar](100) NOT NULL,
	[OriginalValue] [nvarchar](max) NULL,
	[NewValue] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED 
(
	[AuditLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
  
CREATE TABLE [dbo].[PHSUser](
	[PHSUserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[EffectiveStartDate] [datetime2](7) NULL,
	[EffectiveEndDate] [datetime2](7) NULL,
	[Role] [nvarchar](max) NOT NULL,
	[ContactNumber] [nvarchar](max) NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedDateTime] [datetime2](7) NULL,
	[UsingTempPW] [bit] NOT NULL,
	[DeleteStatus] [bit] NOT NULL,
	[MCRNo] [nvarchar](50) NULL,
 CONSTRAINT [PK_PHSUser] PRIMARY KEY CLUSTERED 
(
	[PHSUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)

CREATE TABLE [dbo].[PHSEvent](
	[PHSEventID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[StartDT] [datetime2](7) NOT NULL,
	[EndDT] [datetime2](7) NOT NULL,
	[Venue] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](1) NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDateTime] [datetime2](7) NULL,
 CONSTRAINT [PK_event] PRIMARY KEY CLUSTERED 
(
	[PHSEventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

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
	[PHSEventID] [int] NOT NULL,
	[ModalityID] [int] NOT NULL,
 CONSTRAINT [PK_EventModality_1] PRIMARY KEY CLUSTERED 
(
	[PHSEventID] ASC,
	[ModalityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Modality](
	[ModalityID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Position] [int] NOT NULL,
	[IconPath] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[IsMandatory] [bit] NOT NULL,
	[HasParent] [bit] NULL,
	[Status] [nvarchar](max) NULL,
	[Eligiblity] [nvarchar](max) NULL,
	[Labels] [int] NULL,
	[Role] [nvarchar](max) NULL
 CONSTRAINT [PK_Modality] PRIMARY KEY CLUSTERED 
(
	[ModalityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[Participant](
	[ParticipantID] [int] IDENTITY(1,1) NOT NULL,
	[Nric] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Salutation] [nvarchar](4) NULL,
	[HomeNumber] [nvarchar](max) NULL,
	[MobileNumber] [nvarchar](max) NULL,
	[DateOfBirth] [datetime] NULL,
	[Language] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[Race] [nvarchar](max) NULL,
	[Citizenship] [nvarchar](max) NULL,
 CONSTRAINT [PK_Participant] PRIMARY KEY CLUSTERED 
(
	[ParticipantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[PreRegistration](
	[PreRegistrationID] [int] IDENTITY(1,1) NOT NULL,
	[Nric] [nvarchar](max) NOT NULL,
	[EntryId] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Salutation] [nvarchar](4) NOT NULL,
	[HomeNumber] [nvarchar](max) NULL,
	[MobileNumber] [nvarchar](max) NULL,
	[DateOfBirth] [datetime] NULL,
	[Citizenship] [nvarchar](max) NOT NULL,
	[Race] [nvarchar](max) NOT NULL,
	[PostalCode] [nvarchar](max) NULL,
	[Language] [nvarchar](max) NOT NULL,
	[PreferedTime] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[OPTION1] [nvarchar](max) NOT NULL,
	[OPTION2] [nvarchar](max) NOT NULL,
	[OPTION3] [nvarchar](max) NOT NULL,
	[OPTION4] [nvarchar](max) NOT NULL,
	[OPTION5] [nvarchar](max) NOT NULL,
	[OPTION6] [nvarchar](max) NOT NULL,
	[OPTION7] [nvarchar](max) NOT NULL,
	[OPTION8] [nvarchar](max) NOT NULL,
	[OPTION9] [nvarchar](max) NOT NULL,
	[OPTION10] [nvarchar](max) NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PreRegistration] PRIMARY KEY CLUSTERED 
(
	[PreRegistrationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[Template](
	[TemplateID] [int] IDENTITY(1,1) NOT NULL,
	[FormID] [int] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[ConfirmationMessage] [nvarchar](300) NOT NULL,
	[DateAdded] [datetime] NULL,
	[Theme] [nvarchar](50) NULL,
	[NotificationEmail] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[EventID] [int] NULL,
	[IsQuestion] [bit] NOT NULL DEFAULT 0,
	[Version] [int] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDateTime] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedDateTime] [datetime2](7) NULL
 CONSTRAINT [PK_template] PRIMARY KEY CLUSTERED 
(
	[TemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[TemplateFieldValue](
	[TemplateFieldValueID] [int] IDENTITY(1,1) NOT NULL,
	[TemplateFieldID] [int] NOT NULL,
	[EntryId] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[DateAdded] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDateTime] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedDateTime] [datetime2](7) NULL
 CONSTRAINT [PK_template_field_values] PRIMARY KEY CLUSTERED 
(
	[TemplateFieldValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[TemplateField](
	[TemplateFieldID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](MAX) NULL,
	[Text] [nvarchar](MAX) NULL,
	[FieldType] [varchar](20) NULL CONSTRAINT [DF_template_fields_FieldType]  DEFAULT ('TEXTBOX'),
	[IsRequired] [bit] NULL CONSTRAINT [DF_template_fields_IsRequired]  DEFAULT ((0)),
	[MaxChars] [int] NULL CONSTRAINT [DF_template_fields_MaxCharacters]  DEFAULT ((50)),
	[HoverText] [nvarchar](150) NULL,
	[Hint] [nvarchar](150) NULL,
	[SubLabel] [nvarchar](50) NULL,
	[Size] [varchar](10) NULL,
	[SelectedOption] [nvarchar](50) NOT NULL,
	[AddOthersOption] [bit] NULL DEFAULT ((0)),
	[OthersOption] [nvarchar](50) NULL,
	[OthersPlaceHolder] [nvarchar](150) NULL,
	[Columns] [int] NULL,
	[Rows] [int] NULL,
	[Options] [nvarchar](2000) NULL,
	[Validation] [varchar](50) NULL,
	[DomId] [int] NULL CONSTRAINT [DF_template_fields_DomId]  DEFAULT ((0)),
	[Order] [int] NULL CONSTRAINT [DF_template_fields_Order]  DEFAULT ((0)),
	[MinimumAge] [int] NULL,
	[MaximumAge] [int] NULL,
	[HelpText] [nvarchar](50) NULL,
	[DateAdded] [datetime] NOT NULL CONSTRAINT [DF_template_fields_DateAdded]  DEFAULT (getutcdate()),
	[MaxFilesizeInKb] [int] NULL CONSTRAINT [DF_template_fields_MaxFilesizeInKb]  DEFAULT ((5000)),
	[ValidFileExtensions] [nvarchar](50) NULL,
	[MinFilesizeInKb] [int] NULL CONSTRAINT [DF_template_fields_MinFilesizeInKb]  DEFAULT ((50)),
	[ImageBase64] [varchar](max) NULL,
	[MatrixRow] [varchar](max) NULL,
	[MatrixColumn] [varchar](max) NULL,
	[PreRegistrationFieldName] [varchar](max) NULL,
	[RegistrationFieldName] [varchar](max) NULL,
	[SummaryFieldName] [nvarchar](50) NULL,
	[SummaryType] [nvarchar](50) NULL,
	[ConditionTemplateFieldID] [int] NULL,
	[ConditionCriteria] [varchar](50) NULL,
	[ConditionOptions] [nvarchar](50) NULL,
	[StandardReferenceID] [int] NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDateTime] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedDateTime] [datetime2](7) NULL
 CONSTRAINT [PK_template_fields] PRIMARY KEY CLUSTERED 
(
	[TemplateFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[TemplateTemplateField](
	[TemplateID] [int] NOT NULL,
	[TemplateFieldID] [int] NOT NULL,
 CONSTRAINT [PK_template_template_fields] PRIMARY KEY CLUSTERED 
(
	[TemplateID] ASC,
	[TemplateFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Form](
	[FormID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Slug] [nvarchar](50) NULL,
	[IsPublic] [bit] NOT NULL,
	[PublicFormType] [nvarchar](50) NULL,
	[InternalFormType] [nvarchar](50) NULL,
	[DateAdded] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDateTime] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedDateTime] [datetime2](7) NULL
 CONSTRAINT [PK_form] PRIMARY KEY CLUSTERED 
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[StandardReference](
	[StandardReferenceID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[DataType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_standard_reference] PRIMARY KEY CLUSTERED 
(
	[StandardReferenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ReferenceRange](
	[ReferenceRangeID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[MinimumValue] [float] NULL,
	[MaximumValue] [float] NULL,
	[StringValue] [nvarchar](max) NULL,
	[Result] [nvarchar](max) NULL,
	[Highlight] [bit] NOT NULL,
	[StandardReferenceID] [int] NOT NULL,
 CONSTRAINT [PK_reference_range] PRIMARY KEY CLUSTERED 
(
	[ReferenceRangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[ParticipantPHSEvent](
	[ParticipantID] [int] NOT NULL,
	[PHSEventID] [int] NOT NULL,
 CONSTRAINT [PK_participant_phs_event] PRIMARY KEY CLUSTERED 
(
	[ParticipantID] ASC,
	[PHSEventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ParticipantJourneyModality](
	[ParticipantJourneyModalityID] [int] IDENTITY(1,1) NOT NULL,
	[ParticipantID] [int] NOT NULL,
	[PHSEventID] [int] NOT NULL,
	[ModalityID] [int] NOT NULL,
	[FormID] [int] NOT NULL,
	[TemplateID] [int] NULL,
	[EntryId] [uniqueidentifier] NOT NULL
 CONSTRAINT [PK_participant_journey_modality] PRIMARY KEY CLUSTERED 
(
	[ParticipantID] ASC,
	[PHSEventID] ASC,
	[ModalityID] ASC,
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Summary](
	[SummaryID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](50) NULL,
	[ParticipantID] [int] NOT NULL,
	[PHSEventID] [int] NOT NULL,
	[ModalityID] [int] NOT NULL,
	[TemplateFieldID] [int] NOT NULL,
	[SummaryLabel] [nvarchar](MAX) NULL,
	[SummaryValue] [nvarchar](MAX) NULL,
	[SummaryType] [nvarchar](50) NULL,
	[StandardReferenceID] [int] NULL
 CONSTRAINT [PK_summary] PRIMARY KEY CLUSTERED 
(
	[ParticipantID] ASC,
	[PHSEventID] ASC,
	[ModalityID] ASC,
	[TemplateFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[SummaryMapping](
	[SummaryMappingID] [int] IDENTITY(1,1) NOT NULL,
	[SummaryType] [nvarchar](50) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[SummaryFieldName] [nvarchar](50) NOT NULL
 CONSTRAINT [PK_summary_map] PRIMARY KEY CLUSTERED 
(
	[SummaryType] ASC,
	[CategoryName] ASC,
	[SummaryFieldName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].FollowUpConfiguration(
	[FollowUpConfigurationID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Deploy] [bit] NOT NULL,
	[DeployDateTime] [datetime2](7) NULL,
	[PHSEventID] [int] NOT NULL,
 CONSTRAINT [PK_followup_configuration] PRIMARY KEY CLUSTERED 
(
	[FollowUpConfigurationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].FollowUpGroup(
	[FollowUpGroupID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Filter] [nvarchar](max) NOT NULL,
	[HealthReportType] [nvarchar](max) NULL,
	[PrintHealthReport] [bit] NULL,
	[FreeConsultationEligible] [bit] NULL,
	[TeleHealthEligible] [bit] NULL,
	[BuddyFormType] [nvarchar](max) NULL,
	[FollowUpConfigurationID] [int] NOT NULL,
 CONSTRAINT [PK_followup_group] PRIMARY KEY CLUSTERED 
(
	[FollowUpGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].ParticipantCallerMapping(
	[ParticipantCallerMappingID] [int] IDENTITY(1,1) NOT NULL,
	[ParticipantID] [int] NOT NULL,
	[FollowUpGroupID] [int] NOT NULL,
	[PhaseIFollowUpVolunteer] [nvarchar](max) NULL,
	[PhaseIFollowUpVolunteerCallStatus] [nvarchar](max) NULL,
	[PhaseIFollowUpVolunteerCallDateTime] [datetime2](7) NULL,
	[PhaseIFollowUpVolunteerCallRemark] [nvarchar](max) NULL,
	[PhaseICommitteeMember] [nvarchar](max) NULL,
	[PhaseICommitteeMemberCallStatus] [nvarchar](max) NULL,
	[PhaseICommitteeMemberCallDateTime] [datetime2](7) NULL,
	[PhaseICommitteeMemberCallRemark] [nvarchar](max) NULL,
	[PhaseIIFollowUpVolunteer] [nvarchar](max) NULL,
	[PhaseIIFollowUpVolunteerCallStatus] [nvarchar](max) NULL,
	[PhaseIIFollowUpVolunteerCallDateTime] [datetime2](7) NULL,
	[PhaseIIFollowUpVolunteerCallRemark] [nvarchar](max) NULL,
	[PhaseIICommitteeMember] [nvarchar](max) NULL,
	[PhaseIICommitteeMemberCallStatus] [nvarchar](max) NULL,
	[PhaseIICommitteeMemberCallDateTime] [datetime2](7) NULL,
	[PhaseIICommitteeMemberCallRemark] [nvarchar](max) NULL,
 CONSTRAINT [PK_participant_caller_mapping] PRIMARY KEY CLUSTERED 
(
	[ParticipantCallerMappingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EventModality]  WITH CHECK ADD  CONSTRAINT [FK_EventModality_event] FOREIGN KEY([PHSEventID])
REFERENCES [dbo].[PHSEvent] ([PHSEventID])
GO
ALTER TABLE [dbo].[EventModality] CHECK CONSTRAINT [FK_EventModality_event]
GO
ALTER TABLE [dbo].[EventModality]  WITH CHECK ADD  CONSTRAINT [FK_EventModality_Modality] FOREIGN KEY([ModalityID])
REFERENCES [dbo].[Modality] ([ModalityID])
GO
ALTER TABLE [dbo].[EventModality] CHECK CONSTRAINT [FK_EventModality_Modality]
GO
ALTER TABLE [dbo].[TemplateFieldValue]  WITH CHECK ADD  CONSTRAINT [FK_template_field_values_template_fields] FOREIGN KEY([TemplateFieldID])
REFERENCES [dbo].[TemplateField] ([TemplateFieldID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TemplateFieldValue] CHECK CONSTRAINT [FK_template_field_values_template_fields]
GO
ALTER TABLE [dbo].[TemplateTemplateField]  WITH CHECK ADD  CONSTRAINT [FK template_fields_template_template_fields] FOREIGN KEY([TemplateFieldId])
REFERENCES [dbo].[TemplateField] ([TemplateFieldID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TemplateTemplateField] CHECK CONSTRAINT [FK template_fields_template_template_fields]
GO
ALTER TABLE [dbo].[TemplateTemplateField]  WITH CHECK ADD  CONSTRAINT [FK_template_fields] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Template] ([TemplateId])
GO
ALTER TABLE [dbo].[TemplateTemplateField] CHECK CONSTRAINT [FK_template_fields]
GO
ALTER TABLE [dbo].[TemplateField]  WITH CHECK ADD  CONSTRAINT [FK_template_field_template_field] FOREIGN KEY([ConditionTemplateFieldId])
REFERENCES [dbo].[TemplateField] ([TemplateFieldId])
GO
ALTER TABLE [dbo].[TemplateField] CHECK CONSTRAINT [FK_template_field_template_field]
GO
ALTER TABLE [dbo].[TemplateField]  WITH CHECK ADD  CONSTRAINT [FK_template_field_standard_reference] FOREIGN KEY([StandardReferenceID])
REFERENCES [dbo].[StandardReference] ([StandardReferenceID])
GO
ALTER TABLE [dbo].[TemplateField] CHECK CONSTRAINT [FK_template_field_standard_reference]
GO
ALTER TABLE [dbo].[ModalityForm]  WITH CHECK ADD  CONSTRAINT [FK_ModalityForm_form] FOREIGN KEY([FormID])
REFERENCES [dbo].[Form] ([FormID])
GO
ALTER TABLE [dbo].[ModalityForm] CHECK CONSTRAINT [FK_ModalityForm_form]
GO
ALTER TABLE [dbo].[ModalityForm]  WITH CHECK ADD  CONSTRAINT [FK_ModalityForm_Modality] FOREIGN KEY([ModalityID])
REFERENCES [dbo].[Modality] ([ModalityID])
GO
ALTER TABLE [dbo].[ModalityForm] CHECK CONSTRAINT [FK_ModalityForm_Modality]
GO
ALTER TABLE [dbo].[Template]  WITH CHECK ADD  CONSTRAINT [FK_Form_Template] FOREIGN KEY([FormID])
REFERENCES [dbo].[Form] ([FormID])
GO
ALTER TABLE [dbo].[Template] CHECK CONSTRAINT [FK_Form_Template]
GO
ALTER TABLE [dbo].[ParticipantPHSEvent]  WITH CHECK ADD  CONSTRAINT [FK participant_phs_event_participant] FOREIGN KEY([ParticipantID])
REFERENCES [dbo].[Participant] ([ParticipantID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ParticipantPHSEvent] CHECK CONSTRAINT [FK participant_phs_event_participant]
GO
ALTER TABLE [dbo].[ParticipantPHSEvent]  WITH CHECK ADD  CONSTRAINT [FK participant_phs_event_phs_event] FOREIGN KEY([PHSEventID])
REFERENCES [dbo].[PHSEvent] ([PHSEventID])
GO
ALTER TABLE [dbo].[ParticipantPHSEvent] CHECK CONSTRAINT [FK participant_phs_event_phs_event]
GO
ALTER TABLE [dbo].[ParticipantJourneyModality]  WITH CHECK ADD  CONSTRAINT [FK participant_journey_modality_phs_event] FOREIGN KEY([PHSEventID])
REFERENCES [dbo].[PHSEvent] ([PHSEventID])
GO
ALTER TABLE [dbo].[ParticipantJourneyModality] CHECK CONSTRAINT [FK participant_journey_modality_phs_event]
GO
ALTER TABLE [dbo].[ParticipantJourneyModality]  WITH CHECK ADD  CONSTRAINT [FK participant_journey_modality_participant] FOREIGN KEY([ParticipantID])
REFERENCES [dbo].[Participant] ([ParticipantID])
GO
ALTER TABLE [dbo].[ParticipantJourneyModality] CHECK CONSTRAINT [FK participant_journey_modality_participant]
GO
ALTER TABLE [dbo].[ParticipantJourneyModality]  WITH CHECK ADD  CONSTRAINT [FK participant_journey_modality_form] FOREIGN KEY([FormID])
REFERENCES [dbo].[Form] ([FormID])
GO
ALTER TABLE [dbo].[ParticipantJourneyModality] CHECK CONSTRAINT [FK participant_journey_modality_form]
GO
ALTER TABLE [dbo].[ParticipantJourneyModality]  WITH CHECK ADD  CONSTRAINT [FK participant_journey_modality_modality] FOREIGN KEY([ModalityID])
REFERENCES [dbo].[Modality] ([ModalityID])
GO
ALTER TABLE [dbo].[ParticipantJourneyModality] CHECK CONSTRAINT [FK participant_journey_modality_modality]
GO
ALTER TABLE [dbo].[ParticipantJourneyModality]  WITH CHECK ADD  CONSTRAINT [FK participant_journey_modality_template] FOREIGN KEY([TemplateID])
REFERENCES [dbo].[Template] ([TemplateID])
GO
ALTER TABLE [dbo].[ParticipantJourneyModality] CHECK CONSTRAINT [FK participant_journey_modality_modality]
GO
ALTER TABLE [dbo].[ReferenceRange]  WITH CHECK ADD  CONSTRAINT [FK reference_range_standard_reference] FOREIGN KEY([StandardReferenceID])
REFERENCES [dbo].[StandardReference] ([StandardReferenceID])
GO
ALTER TABLE [dbo].[ReferenceRange] CHECK CONSTRAINT [FK reference_range_standard_reference]
GO
ALTER TABLE [dbo].[Summary]  WITH CHECK ADD  CONSTRAINT [FK summary_phs_event] FOREIGN KEY([PHSEventID])
REFERENCES [dbo].[PHSEvent] ([PHSEventID])
GO
ALTER TABLE [dbo].[Summary] CHECK CONSTRAINT [FK summary_phs_event]
GO
ALTER TABLE [dbo].[Summary]  WITH CHECK ADD  CONSTRAINT [FK summary_participant] FOREIGN KEY([ParticipantID])
REFERENCES [dbo].[Participant] ([ParticipantID])
GO
ALTER TABLE [dbo].[Summary] CHECK CONSTRAINT [FK summary_participant]
GO
ALTER TABLE [dbo].[Summary]  WITH CHECK ADD  CONSTRAINT [FK summary_modality] FOREIGN KEY([ModalityID])
REFERENCES [dbo].[Modality] ([ModalityID])
GO
ALTER TABLE [dbo].[Summary] CHECK CONSTRAINT [FK summary_modality]
GO
ALTER TABLE [dbo].[Summary]  WITH CHECK ADD  CONSTRAINT [FK summary_template_field] FOREIGN KEY([TemplateFieldID])
REFERENCES [dbo].[TemplateField] ([TemplateFieldID])
GO
ALTER TABLE [dbo].[Summary] CHECK CONSTRAINT [FK summary_template_field]
GO
ALTER TABLE [dbo].[Summary]  WITH CHECK ADD  CONSTRAINT [FK summary_standard_reference] FOREIGN KEY([StandardReferenceID])
REFERENCES [dbo].[StandardReference] ([StandardReferenceID])
GO
ALTER TABLE [dbo].[Summary] CHECK CONSTRAINT [FK summary_standard_reference]
GO
ALTER TABLE [dbo].[FollowUpConfiguration]  WITH CHECK ADD  CONSTRAINT [FK followup_configuration_event] FOREIGN KEY([PHSEventID])
REFERENCES [dbo].[PHSEvent] ([PHSEventID])
GO
ALTER TABLE [dbo].[FollowUpConfiguration] CHECK CONSTRAINT [FK followup_configuration_event]
GO
ALTER TABLE [dbo].[FollowUpGroup]  WITH CHECK ADD  CONSTRAINT [FK followup_group_configuration] FOREIGN KEY([FollowUpConfigurationID])
REFERENCES [dbo].[FollowUpConfiguration] ([FollowUpConfigurationID])
GO
ALTER TABLE [dbo].[FollowUpGroup] CHECK CONSTRAINT [FK followup_group_configuration]
GO
ALTER TABLE [dbo].[ParticipantCallerMapping]  WITH CHECK ADD  CONSTRAINT [FK participantcallermapping_participant] FOREIGN KEY([ParticipantID])
REFERENCES [dbo].[Participant] ([ParticipantID])
GO
ALTER TABLE [dbo].[ParticipantCallerMapping] CHECK CONSTRAINT [FK participantcallermapping_participant]
GO
ALTER TABLE [dbo].[ParticipantCallerMapping]  WITH CHECK ADD  CONSTRAINT [FK participantcallermapping_followupgroup] FOREIGN KEY([FollowUpGroupID])
REFERENCES [dbo].[FollowUpGroup] ([FollowUpGroupID])
GO
ALTER TABLE [dbo].[ParticipantCallerMapping] CHECK CONSTRAINT [FK participantcallermapping_followupgroup]
GO
--------------------------Data--------------------- 


---  [dbo].[PHSUser]  --
DELETE FROM  [dbo].[PHSUser] 
GO

GO
SET IDENTITY_INSERT [dbo].[PHSUser] ON 

GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (1, N'Admin1', N'WG7qLtt1uUZI8IS49HuPRMXk14RdyGCE', N'Super Admin', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Committee Member', NULL, N'f+3W4Sgya5jto3JLOKiwC3TJtOPs7yhC', NULL, CAST(N'2016-04-13 23:15:17.5270000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (2, N'Volunteer1', N't5bahHgyfjMDi7jcwE7zz+CUTW6E8VZw', N'Volunteer 1', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'C0xyFiz5b3ZacMTRPwKleI0eFaSy0x8C ', NULL, CAST(N'2016-04-13 23:17:34.0000000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (3, N'Volunteer2', N'xk/xDrqvUTmp//xjd2VaxtsT0uJf9nDj', N'Volunteer 2', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Volunteer', NULL, N'0OTW6ch2NjxC1q7sIYl586hQMzftY+Wd ', NULL, CAST(N'2016-04-13 23:17:41.9100000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (4, N'Volunteer3', N'oa2koZYg8TLrW4qUfykT7ilKoTgL4lEy', N'Volunteer 3', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Volunteer', NULL, N'hTJOxnbQmKLChQqOwKBv6s6BCRrE2riL ', NULL, CAST(N'2016-04-13 23:18:59.8830000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (5, N'Volunteer4', N'TFQv51wShe7p5Mxinu6wEm66j7eK8AmT', N'Volunteer 4', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Volunteer', NULL, N'i2k7XSN+Gi8kDpq9feHX2H7zMjVx4dGR ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (6, N'Volunteer5', N'NSr+4maxT00PDMoKNGuJ4xWo+4OoCQpe', N'Volunteer 5', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Volunteer', NULL, N'Luni0t7I0MCTOMbt8n0bpuQUpAAIgfdh ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (7, N'Volunteer6', N'6yG3xPGAdpnlCy1UlodH4GGhNw5yEX08', N'Volunteer 6', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Volunteer', NULL, N'cxxx/Zd2AKXaUaNAze+t20MF+gCnbMFZ ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (8, N'Volunteer7', N'TmHNU5R0Fpw5INQrazI/PQA+7zy5iVta', N'Volunteer 7', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Volunteer', NULL, N'xnnwmyxMXSfQtcUpnSHceSMTlJZ+VdDO ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (9, N'Volunteer8', N'm9Z8my2Kx0tAaEpWOJ3CDki2oua8bDgO', N'Volunteer 8', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Volunteer', NULL, N'VklxCbwR/oHKBRV6npO0VmbAvbR46Sd/ ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (10, N'Volunteer9', N'KUlleZSbFADpi8n+M7pIOlCmhEZYJnR6', N'Volunteer 9', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Volunteer', NULL, N'VJkbnSovc1BtPy04RybAo8B8sAAwTn2J ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (11, N'Volunteer10', N'MWM6zwwL1eeZxfZ3XjUeWYHWkbLM0A4C', N'Volunteer 10', 0, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Volunteer', NULL, N'cZZ7m8Sj5pv4Ag7hP8ot9ZqDCHbrOooI ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (12, N'Doctor1', N'QV7JswCtrAikzyagdRArK41ZR7CHuqf7', N'Doctor 1', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'KrTW84isQkhtiMQxzbwLKfIsLmOhobSY ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (13, N'Doctor2', N'+iFZH18HLlJnGWWOWsanO5fxhVgHKIp3', N'Doctor 2', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'HKjH9qgCG1tyVMVcEIJXdxtxJ5bXYufr ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (14, N'Doctor3', N'bV2Wt4MR/iKklHcxNfEl5YJd6gtwqZiH', N'Doctor 3', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'8YQJqFAV9xUWwHiXfODFuhuDTS5ukQwt ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (15, N'Doctor4', N'B2Cw0JIvHX5auHKvd+qJawpAh0xEZFJR', N'Doctor 4', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'yKV616WHc2i8rUKGMXMhskBWkWXm4AQC ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (16, N'Doctor5', N'cP/4dmCYJMfCqT8zEEWCecJLZQzYVqc4', N'Doctor 5', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'7bDVnI3WlC1q2ApB+ASvPHsZwKjkvBzZ ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (17, N'Doctor6', N'eO060WBvNSFuNE2ZE3HU0yGjqQOK3+yf', N'Doctor 6', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'4c08nVFZyiI73vZd9P7bEuBvpvHJhYZO ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (18, N'Doctor7', N'p4ZhdGoLJKQW8sscjoP+Vkcg/F679pAH', N'Doctor 7', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'FHBd7pjdeFwdNJaRJPzxdfpg/zXWChDx ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (19, N'Doctor8', N'GucDVKmV2/8cVDywlKRr4g++4/OpJksc', N'Doctor 8', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'qDzmSff2D9K2BlhQYrZ6YfGB1BETav+j ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (20, N'Doctor9', N'8KJLSQXv5bURV8U2ZU+rAI54+w7A9xYI', N'Doctor 9', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'zK1HYfERIWKCtu3djAlZD8SheRXI7w6B ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (21, N'Doctor10', N'czIthJ583ZfkcszEIEfEK+q2pWDq3ofB', N'Doctor 10', 0, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Doctor', NULL, N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (22, N'FollowupVolunteer1', N'czIthJ583ZfkcszEIEfEK+q2pWDq3ofB', N'FollowupVolunteer1', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Follow-up Volunteer', NULL, N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (23, N'FollowupVolunteer2', N'czIthJ583ZfkcszEIEfEK+q2pWDq3ofB', N'FollowupVolunteer2', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Follow-up Volunteer', NULL, N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (24, N'FollowupVolunteer3', N'czIthJ583ZfkcszEIEfEK+q2pWDq3ofB', N'FollowupVolunteer3', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Follow-up Volunteer', NULL, N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (25, N'FollowupVolunteer4', N'czIthJ583ZfkcszEIEfEK+q2pWDq3ofB', N'FollowupVolunteer4', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Follow-up Volunteer', NULL, N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (26, N'FollowupVolunteer5', N'czIthJ583ZfkcszEIEfEK+q2pWDq3ofB', N'FollowupVolunteer5', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Follow-up Volunteer', NULL, N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
INSERT [dbo].[PHSUser] ([PHSUserID], [Username], [Password], [FullName], [IsActive], [EffectiveStartDate], [EffectiveEndDate], [Role], [ContactNumber], [PasswordSalt], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [UsingTempPW], [DeleteStatus]) VALUES (21, N'FollowupVolunteer6', N'czIthJ583ZfkcszEIEfEK+q2pWDq3ofB', N'FollowupVolunteer6', 1, CAST(N'2016-04-13 00:00:00.0000000' AS DateTime2), CAST(N'2099-04-13 00:00:00.0000000' AS DateTime2), N'Follow-up Volunteer', NULL, N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ', NULL, CAST(N'2016-04-13 23:21:00.9330000' AS DateTime2), NULL, NULL, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[PHSUser] OFF
GO

---  Events Sample  --

GO
SET IDENTITY_INSERT [dbo].[PHSEvent] ON 

GO
INSERT [dbo].[PHSEvent] ([PHSEventID], [Title], [StartDT], [EndDT], [Venue], [IsActive], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (1, N'2015 - Event', CAST(N'2015-04-13 10:00:00.5270000' AS DateTime2), CAST(N'2015-04-14 12:00:00.5270000' AS DateTime2), N'PHS', 1, N'T', CAST(N'2015-04-11 10:00:00.5270000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PHSEvent] ([PHSEventID], [Title], [StartDT], [EndDT], [Venue], [IsActive], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (2, N'2016 - Event', CAST(N'2016-04-13 10:00:00.5270000' AS DateTime2), CAST(N'2016-04-14 12:00:00.5270000' AS DateTime2), N'PHS', 1, N'T', CAST(N'2016-04-11 10:00:00.5270000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[PHSEvent] ([PHSEventID], [Title], [StartDT], [EndDT], [Venue], [IsActive], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (3, N'PHS 2017-Jurong', CAST(N'2017-07-12 10:00:00.5270000' AS DateTime2), CAST(N'2017-12-31 12:00:00.5270000' AS DateTime2), N'PHS', 1, N'T', CAST(N'2016-04-11 10:00:00.5270000' AS DateTime2), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[PHSEvent] OFF
GO

---  Participant Sample  --

GO
SET IDENTITY_INSERT [dbo].[Participant] ON 

GO
INSERT [dbo].[Participant] ([ParticipantID], [Nric], [FullName], [SALUTATION], [HomeNumber], [MobileNumber], [DateOfBirth], [Language], [Gender], [Address], [PostalCode], [Race], [Citizenship]) VALUES (1, N'S8250369B', N'Lawrence Fay DDS', N'Mr', N'00000000', N'81274563', CAST(N'1982-04-13 10:00:00.527' AS DateTime), N'English, Mandarin', N'Male', N'Blk 363 Clementi Ave 2 #04-425', N'120363', N'Chinese', N'Singapore Citizen')
GO
INSERT [dbo].[Participant] ([ParticipantID], [Nric], [FullName], [SALUTATION], [HomeNumber], [MobileNumber], [DateOfBirth], [Language], [Gender]) VALUES (2, N'S7931278I', N'Maxwell Schulist', N'Mr', N'69639756', N'81274563', CAST(N'1979-02-13 10:00:00.527' AS DateTime), N'English', N'Male')

GO
SET IDENTITY_INSERT [dbo].[Participant] OFF
GO

---  Participant PHSEvent Sample  --

GO
SET IDENTITY_INSERT [dbo].[Participant] ON 

GO
INSERT [dbo].[ParticipantPHSEvent] ([ParticipantID], [PHSEventID]) VALUES (1, 1)
GO
INSERT [dbo].[ParticipantPHSEvent] ([ParticipantID], [PHSEventID]) VALUES (1, 2)
GO
INSERT [dbo].[ParticipantPHSEvent] ([ParticipantID], [PHSEventID]) VALUES (1, 3)
GO
INSERT [dbo].[ParticipantPHSEvent] ([ParticipantID], [PHSEventID]) VALUES (2, 2)
GO

SET IDENTITY_INSERT [dbo].[Participant] OFF
GO


---  Modality Sample  --

GO
SET IDENTITY_INSERT [dbo].[Modality] ON 

GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (1, N'Registration', 0, N'../../../Content/images/Modality/01registration.png', 1, 1, 1, 0, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (2, N'History Taking', 1, N'../../../Content/images/Modality/02historytaking.png', 1, 1, 1, 1, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (4, N'Phlebotomy', 3, N'../../../Content/images/Modality/04phlebo.png', 0, 0, 0, 1, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (5, N'FIT', 4, N'../../../Content/images/Modality/05fit.png', 0, 0, 0, 1, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (6, N'Woman Cancer', 5, N'../../../Content/images/Modality/06woman.png', 0, 0, 0, 1, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (7, N'Geriatric Screening', 6, N'../../../Content/images/Modality/07geri.png', 0, 0, 0, 1, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (8, N'Oral', 7, N'../../../Content/images/Modality/08oral.png', 0, 0, 0, 1, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (9, N'Doctor Consult', 8, N'../../../Content/images/Modality/09doctor.png', 0, 0, 0, 0, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (10, N'Exhibition', 9, N'../../../Content/images/Modality/10exhibition.png', 1, 0, 0, 0, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (11, N'Summary', 10, N'../../../Content/images/Modality/11summary.png', 1, 1, 0, 0, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (12, N'Miscellaneous Forms', 90, N'../../../Content/images/Modality/11summary.png', 1, 1, 1, 1, N'Pending', NULL, NULL)
GO
INSERT [dbo].[Modality] ([ModalityID], [Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) VALUES (13, N'Public Forms', 99, N'../../../Content/images/Modality/11summary.png', 1, 0, 1, 0, N'Public', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Modality] OFF



--- Event Modality Sample  --

GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (1, 1)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (1, 2)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (1, 4)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (1, 5)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (1, 6)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (1, 7)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (1, 8)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (1, 9)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (1, 10)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (1, 11)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 1)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 2)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 4)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 5)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 6)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 7)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 8)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 9)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 10)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 11)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 12)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (2, 13)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 1)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 2)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 4)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 5)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 6)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 7)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 8)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 9)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 10)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 11)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 12)
GO
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (3, 13)
GO

--- Standard Reference Sample  --

SET IDENTITY_INSERT [phs].[dbo].[StandardReference] ON
GO
INSERT [phs].[dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (1, N'Systolic Blood Pressure ', N'Number')
GO

GO
INSERT [phs].[dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (2, N'Diastolic Blood Pressure ', N'Number')
GO

GO
INSERT [phs].[dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (3, N'Sugar', N'Number')
GO

GO
INSERT [phs].[dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (4, N'BMI', N'Number')
GO

INSERT [dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (5, N'DoctorAlertYes', N'String')
INSERT [dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (6, N'ICIQAlert', N'Number')
INSERT [dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (7, N'WeightLossFrailScale', N'Number')
INSERT [dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (8, N'OTQuestionDizzi', N'String')
INSERT [dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (9, N'OTQuestionUrin', N'String')
INSERT [dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (10, N'FailedOT', N'String')
INSERT [dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (11, N'SPPBFallRisk', N'String')
INSERT [dbo].[StandardReference] ([StandardReferenceID], [Title], [DataType]) VALUES (12, N'TUGFallRisk', N'String')

SET IDENTITY_INSERT [phs].[dbo].[StandardReference] OFF
GO

--- Reference range Sample  --
SET IDENTITY_INSERT [phs].[dbo].[ReferenceRange] ON
GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (1, N'Systolic Normal', 0, 119, N'Normal', 0, 1)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (2, N'Systolic Pre-HTN', 120, 139, N'Pre-HTN', 0, 1)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (3, N'Systolic Stage 1 HTN', 140, 159, N'Stage 1 HTN', 1, 1)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (4, N'Systolic Stage 2 HTN', 160, 179, N'Stage 2 HTN', 1, 1)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (5, N'Systolic Hypertensive Crisis', 180, 999, N'Hypertensive Crisis', 1, 1)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (6, N'Diastolic Normal', 0, 79, N'Normal', 0, 2)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (7, N'Diastolic Pre-HTN', 80, 89, N'Pre-HTN', 0, 2)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (8, N'Diastolic Stage 1 HTN', 90, 99, N'Stage 1 HTN', 1, 2)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (9, N'Diastolic Stage 2 HTN', 100, 119, N'Stage 2 HTN', 1, 2)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (10, N'Diastolic Hypertensive Crisis', 120, 999, N'Hypertensive Crisis', 1, 2)
GO



GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (11, N'Fasting Sugar Normal Range BP', 70, 100, N'IDEAL', 0, 3)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (12, N'Postmeal Sugar Normal Range BP', 70, 140, N'IDEAL', 0, 3)
GO

GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (13, N'Underweight', 0, 18.4, N'UNDERWEIGHT', 1, 4)
GO
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (14, N'Ideal', 18.5, 22.9, N'IDEAL', 0, 4)
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (15, N'Overweight', 23, 27.5, N'OVERWEIGHT', 1, 4)
INSERT [phs].[dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [Result], [Highlight], [StandardReferenceID]) VALUES (16, N'Obese', 27.6, 9999, N'OBESE', 1, 4)
INSERT [dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [StringValue], [Result], [Highlight], [StandardReferenceID]) VALUES (17, N'Yes', NULL, NULL, N'Yes', N'Doctor''s Attention Required!', 1, 5)
INSERT [dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [StringValue], [Result], [Highlight], [StandardReferenceID]) VALUES (18, N'Score High', 1, 999, NULL, N'ICIQ Score Alert', 1, 6)
INSERT [dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [StringValue], [Result], [Highlight], [StandardReferenceID]) VALUES (19, N'YesDizzi', NULL, NULL, N'Yes', N'Frequent dizzyness when standing from seated or supine position', 1, 8)
INSERT [dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [StringValue], [Result], [Highlight], [StandardReferenceID]) VALUES (20, N'YesUrin', NULL, NULL, N'Yes', N'Urinary incontinence or nocturia', 1, 9)
INSERT [dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [StringValue], [Result], [Highlight], [StandardReferenceID]) VALUES (21, N'FailedOT', NULL, NULL, N'Referred to Doctor''s Consult', N'Failed OT Questionnaire', 1, 10)
INSERT [dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [StringValue], [Result], [Highlight], [StandardReferenceID]) VALUES (22, N'HighFallRisk', NULL, NULL, N'High Falls Risk (score ≤ 6)', N'High Falls Risk', 1, 11)
INSERT [dbo].[ReferenceRange] ([ReferenceRangeID], [Title], [MinimumValue], [MaximumValue], [StringValue], [Result], [Highlight], [StandardReferenceID]) VALUES (23, N'TUGFallRisk', NULL, NULL, N'High Falls Risk (> 15sec)', N'High Falls Risk ', 1, 12)


SET IDENTITY_INSERT [phs].[dbo].[ReferenceRange] OFF
GO

---  Forms Sample  --
SET IDENTITY_INSERT [phs].[dbo].[Form] ON 
INSERT [phs].[dbo].[Form] ([FormID], [Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (1, N'Registration Form', NULL, 0, NULL, N'REG', CAST(N'2017-02-19 09:15:43.527' AS DateTime), 1)
GO
INSERT [phs].[dbo].[Form] ([FormID], [Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (2, N'New Registration Form', NULL, 0, NULL, NULL, CAST(N'2017-03-06 13:16:46.333' AS DateTime), 1)
GO
INSERT [phs].[dbo].[Form] ([FormID], [Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (3, N'BMI + History Taking', NULL, 0, NULL, NULL, CAST(N'2017-03-06 13:19:35.327' AS DateTime), 1)
GO
INSERT [phs].[dbo].[Form] ([FormID], [Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (4, N'PreRegistration Form', N'prereg', 1, N'PRE-REGISTRATION', NULL, CAST(N'2017-03-10 13:08:21.207' AS DateTime), 1)
GO
INSERT [phs].[dbo].[Form] ([FormID], [Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (5, N'Doctor Form', NULL, 0, NULL, NULL, CAST(N'2017-03-26 05:46:01.207' AS DateTime), 1)
GO
INSERT [phs].[dbo].[Form] ([FormID], [Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (6, N'Vitals', NULL, 0, NULL, NULL, CAST(N'2017-03-26 05:47:55.413' AS DateTime), 1)
GO
INSERT [phs].[dbo].[Form] ([FormID], [Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (7, N'Phlebotomy DataEntry', NULL, 0, NULL, NULL, CAST(N'2017-04-05 18:47:55.413' AS DateTime), 1)
GO
INSERT [phs].[dbo].[Form] ([FormID], [Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (8, N'Mega Sorting Station', NULL, 0, NULL,  N'MEG', CAST(N'2017-04-05 18:47:55.413' AS DateTime), 1)
GO
INSERT [phs].[dbo].[Form] ([FormID], [Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (9, N'Doctor Summary', NULL, 0, NULL,  N'DSY', CAST(N'2017-04-05 18:47:55.413' AS DateTime), 1)
GO
INSERT [phs].[dbo].[Form] ([FormID], [Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (11, N'form condition', NULL, 0, NULL, NULL, CAST(0x0000A7F0009274FA AS DateTime), 1)
SET IDENTITY_INSERT [phs].[dbo].[Form] OFF
GO

SET IDENTITY_INSERT [phs].[dbo].[Template] ON 

GO
INSERT [phs].[dbo].[Template] ([TemplateID], [FormId], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (1, 1, N'DRAFT', N'Thank you for signing up', CAST(N'2017-02-19 09:15:43.527' AS DateTime), NULL, NULL, 1, NULL, 0, 1)
GO
INSERT [phs].[dbo].[Template] ([TemplateID], [FormId], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (2, 2, N'DRAFT', N'Thank you for signing up', CAST(N'2017-03-06 13:16:46.333' AS DateTime), NULL, NULL, 0, NULL, 0, 1)
GO
INSERT [phs].[dbo].[Template] ([TemplateID], [FormId], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (3, 3, N'DRAFT', N'Thank you for signing up', CAST(N'2017-03-06 13:19:35.327' AS DateTime), NULL, NULL, 1, NULL, 0, 1)
GO
INSERT [phs].[dbo].[Template] ([TemplateID], [FormId], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (4, 4, N'PUBLISHED', N'Thank you for signing up', CAST(N'2017-03-10 13:08:21.207' AS DateTime), NULL, NULL, 1, NULL, 0, 1)
GO
INSERT [phs].[dbo].[Template] ([TemplateID], [FormId], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (5, 5, N'DRAFT', N'Thank you for signing up', CAST(N'2017-03-26 05:46:01.207' AS DateTime), NULL, NULL, 1, NULL, 0, 1)
GO
INSERT [phs].[dbo].[Template] ([TemplateID], [FormId], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (6, 6, N'DRAFT', N'Thank you for signing up', CAST(N'2017-03-26 05:47:55.413' AS DateTime), NULL, NULL, 1, NULL, 0, 1)
GO
INSERT [phs].[dbo].[Template] ([TemplateID], [FormId], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (7, 7, N'DRAFT', N'Thank you for signing up', CAST(N'2017-04-05 18:47:55.413' AS DateTime), NULL, NULL, 1, NULL, 0, 1)
GO
INSERT [phs].[dbo].[Template] ([TemplateID], [FormId], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (8, 8, N'DRAFT', N'Thank you for signing up', CAST(N'2017-04-05 18:47:55.413' AS DateTime), NULL, NULL, 1, NULL, 0, 1)
GO
INSERT [phs].[dbo].[Template] ([TemplateID], [FormId], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (9, 9, N'DRAFT', N'Thank you for signing up', CAST(N'2017-04-05 18:47:55.413' AS DateTime), NULL, NULL, 1, NULL, 0, 1)
GO
INSERT [phs].[dbo].[Template] ([TemplateID], [FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (12, 11, N'DRAFT', N'Thank you for signing up', CAST(0x0000A7F000927504 AS DateTime), NULL, NULL, 1, NULL, 0, 1)
SET IDENTITY_INSERT [phs].[dbo].[Template] OFF

GO
SET IDENTITY_INSERT [dbo].[TemplateField] ON 

GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (76, N'Does the participant intend to undergo phlebotomy? 要去抽血吗？', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes|No', N'', 1, 12, 18, 100, N'', CAST(N'2017-03-05 08:20:27.433' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (77, N'Check that ALL THREE of the following eligibility criteria are fulfilled. Otherwise,change the above question to "no".', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Have not done a blood test in a year,Have not been previously diagnosed with diabetes mellitus AND / OR dyslipidaemia,Have fasted for at least 8 hours prior', N'', 2, 13, 18, 100, N'', CAST(N'2017-03-05 08:21:27.443' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (78, N'<p>PHS will be printing a health report for you. What language(s) would you prefer theletter to be in? 健康报告</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'English and Chinese,English and Malay,English and Tamil,English only', N'', 3, 14, 18, 100, N'', CAST(N'2017-03-05 08:22:12.443' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (79, N'Name (as in NRIC) 姓名', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 4, 0, 18, 100, N'', CAST(N'2017-03-05 08:23:12.440' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'FULLNAME')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (80, N'Gender 性别', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Male|Female', N'', 5, 1, 18, 100, N'', CAST(N'2017-03-05 08:23:27.447' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'GENDER')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (81, N'Salutation', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Mr|Ms|Mrs|Dr', N'', 6, 2, 18, 100, N'', CAST(N'2017-03-05 08:23:57.467' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'SALUTATION')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (82, N'Date of Birth 生日', N'Click to edit', N'BIRTHDAYPICKER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 7, 3, 18, 100, N'', CAST(N'2017-03-05 08:24:42.453' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'DATEOFBIRTH')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (83, N'Citizenship 国籍', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Singapore Citizen 新加坡公民,Singapore Permanent Resident (PR) 新加坡永久居民', N'', 8, 4, 18, 100, N'', CAST(N'2017-03-05 08:25:27.453' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'CITIZENSHIP')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (84, N'Race 种族', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Chinese|Malay|Indian|Others', N'', 9, 5, 18, 100, N'', CAST(N'2017-03-05 08:25:57.457' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'RACE')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (85, N'Language spoken by participant?', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'English|Mandarin|Malay|Tamil|Others', N'', 10, 6, 18, 100, N'', CAST(N'2017-03-05 08:26:57.483' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'LANGUAGE')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (86, N'Address', N'Click to edit', N'ADDRESS', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 11, 7, 18, 100, N'', CAST(N'2017-03-05 08:27:42.463' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'ADDRESS')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (87, N'Housing Type 住宿', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'HDB Rental Flat|1-Room HDB Flat|2-Room HDB Flat|3-Room HDB Flat|4-Room HDB Flat|5-Room HDB Flat|Executive Flats|Condominium / Private Flats|Landed Property', N'', 12, 8, 18, 100, N'', CAST(N'2017-03-05 08:28:12.463' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (88, N'Home Number', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 13, 9, 18, 100, N'', CAST(N'2017-03-05 08:29:42.477' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'HOMENUMBER')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (89, N'Mobile Number', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 14, 10, 18, 100, N'', CAST(N'2017-03-05 08:29:57.467' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'MOBILENUMBER')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (90, N'Highest Education Qualification 教育水平', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'No formal qualifications,Primary / PSLE,Secondary / O Levels,ITE / NITEC / Higher NITEC,Pre-U / JC / A Levels,Polytechnic / Diploma,University / Degree,Master''s, PhD and above', N'', 15, 11, 18, 100, N'', CAST(N'2017-03-05 08:30:27.470' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (92, N'<p><b>Follow Up</b></p><p>In the event of any abnormal test result, I understand that I will be contacted for follow-up usingmy contact details provided herein and that I should see a doctor if any of my test results are abnormal. I also understand that there are limitations to the screening tests and that they are not conclusive in detecting or ruling out medical risk factors or conditions. I should seek medicaladvice if I feel unwell or have any symptoms even if the test results are normal.</p><p>Depending on my test results, I may be contacted and/or referred for post-screening follow-upunder one or more of the Organisers’ screening partner service providers. I may also be referred,where appropriate, to participate in community programmes/activities organised by the HealthPromotionBoard, PA, Voluntary Welfare Organisations, constituency managers, service providersor grassroots organisations for follow-up or participation in community programmes/activities. Iunderstand that the decision to participate in the above-mentioned activities is entirely mine.</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes participant consents to the above.', N'', 17, 16, 18, 100, N'', CAST(N'2017-03-05 08:32:57.497' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (93, N'<p><b>Introduction </b><br></p><p>This is a public health screening event jointly organised by National University Health System(NUHS),People’s Association (PA), and the medical students of National University of Singapore(NUS) Yong Loo Lin School of Medicine (YLLSoM) (collectively the “Organisers”)</p><p><b>Consent to Screen</b></p><p>I consent to undergo health screening tests for one or more of the following: obesity, high bloodpressure, fasting venous blood test for diabetes and dyslipidemia, cancers (specifically breastand cervical cancer screening registration for females, colorectal cancer screening for bothgenders) and additional screening modalities (geriatrics and oral health). I understand that all mypersonal data and information will be recorded and stored in a secure and confidential manner bythe Organisers.</p><p><b>Declaration</b></p><p>I am eligible for the health screening tests offered to me. I hereby declare that all the informationIam providing is true to the best of my knowledge.</p><p><b>Collection and Use of Information<sup>[1]</sup></b></p><p>I acknowledge that my personal data and relevant screening and follow-up information, includingthe test results, will be collected and used by the Organisers for the purposes of conducting thetests and managing and implementing follow-up action arising from the test results. I alsoacknowledge that the information will be retained by PHS, NUHS, Health Promotion Board (HPB), Singapore Cancer Society (SCS), the National e-Health Records (NEHR) and the Ministry ofHealth (MOH), and that aggregate/de-identified Information may be used for research, statisticaland planning purposes.</p><p><b>Authorisation</b></p><p>I authorise the Organisers to approach other healthcare institutions/clinics which are in thepossession of my screening, follow-up, further assessment and/or treatment records to requestfor such relevant records (if any) for the purposes of patient care, treatment or clinical /programme review.</p><p><b>Disclosure of Information</b></p><p>I agree and consent to the Organisers and the healthcare organisation(s) administering the teststo disclose:</p><blockquote><p>a. My information, past screening and follow-up information to the Organisers’ authorisedpartners which may include public healthcare institutions and their affiliatedagencies,hospitals, polyclinics, doctors, laboratories, Singapore Cancer Society andother related healthcare providers; and,</p><p>b. My past screening results and follow-up information to/from the authorised partners forthe purposes of checking if I require re-screening, further tests, follow-up action and/orreferral to community programmes/activities. <br></p></blockquote><p>[1] Including data collected underBreastScreen Singapore, CervicalScreen Singapore, National Colorectal CancerScreening Programme,HPB’s Integrated Screening Programme, Community FunctionalScreening Programme and other screening programmes conducted by HPB and itsauthorised partners.[1] Including data collected under BreastScreen Singapore, CervicalScreen Singapore, NationalColorectal Cancer Screening Programme,HPB’s Integrated Screening Programme, CommunityFunctional Screening Programme and other screening programmes conducted by HPB and itsauthorised partners.</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes participant consents to the above.', N'', 18, 15, 18, 100, N'', CAST(N'2017-03-05 08:43:27.550' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (94, N'<p><b>Follow Up</b><br></p><p>I understand that I may be contacted regardless of my results for administrative reasons, but suchcommunications will only be made when absolutely necessary. I also understand that I will receive periodic SMS updates with health tips and advice on healthy living, as well as remindersto visit my primary care doctor for follow-up care.</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Participant consents to receiving periodic SMS updates with health tips and advice on healthy living as well as reminders to visit my primary care doctor for follow-up care.', N'', 19, 17, 18, 100, N'', CAST(N'2017-03-05 08:44:57.480' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (103, N'<p><b>Compliance to Personal Data Protection Act (PDPA)</b></p><p>I hereby give my consent to the Public Health Service Executive Committee 
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
 be strictly used by these parties for the purposes stated.<b><br></b></p>', N'Click to edit', N'RADIOBUTTON', 1, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes', N'', 20, 18, 18, 100, N'', CAST(N'2017-03-06 12:10:28.377' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (104, N'Click to edit', N'Click to edit', N'MATRIX', 0, 50, N'', N'', N'', N'', N'option1', NULL, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', CAST(N'2017-03-06 13:17:01.830' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, NULL, NULL, NULL, N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (113, N'Click to Health Concerns
If the participant has any concern(s), please take a brief history. (Please write NIL if
otherwise).
E.g."Is there any health issues there''s worrying you right now?"
"最近有没有哪里不舒服？”
Please advise that there will be no diagnosis or prescription made at our screening.
Kindly advise the participant to see a GP/polyclinic instead if he/she is expecting
treatment for their problems.', N'Click to edit', N'TEXTAREA', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', CAST(N'2017-03-08 13:46:50.953' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL, N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [SummaryFieldName], [SummaryType]) VALUES (115, N'Past Medical History (select all that applies)', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'NIL,Hypertension 高血压,Diabetes Mellitus 糖尿病,Hyperlipidemia 高血脂／高胆固醇,Ischemic Heart Disease (including Coronary Heart Diseases) 缺血性心脏病（包括心脏血管阻塞）,Stroke/TIA 中风', N'', 3, 1, 18, 100, N'', CAST(N'2017-03-08 13:51:42.713' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL, N'', N'', N'PAST_MEDICAL_HISTORY', N'SUM')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [SummaryFieldName], [SummaryType]) VALUES (116, N'Personal cancer history: (select all that applies)', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'NIL,Colorectal Cancer 大肠癌,Breast Cancer 乳癌,Cervical Cancer 子宫颈癌', N'', 4, 2, 18, 100, N'', CAST(N'2017-03-08 13:54:54.303' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL, N'', N'', N'PERSONAL_CANCER_HISTORY', N'SUM')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [SummaryFieldName], [SummaryType]) VALUES (117, N'Family History (select all that applies, only for first degree family members)', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'NIL,Hypertension 高血压,Diabetes Mellitus 糖尿病,Hyperlipidemia 高血脂／高胆固醇,Ischemic Heart Disease (including Coronary Artery Diseases) 缺血性心脏病（包括心脏血管阻塞）,Stroke/TIA 中风', N'', 6, 3, 18, 100, N'', CAST(N'2017-03-08 13:56:09.310' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL, N'', N'', N'FAMILY_HISTORY', N'SUM')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [SummaryFieldName], [SummaryType]) VALUES (118, N'Family cancer history (select all that applies, only for first degree family members)', N'Click to edit', N'CHECKBOX', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'NIL,Colorectal Cancer 大肠癌,Breast Cancer 乳癌,Cervical Cancer 子宫颈癌', N'', 5, 4, 18, 100, N'', CAST(N'2017-03-08 13:56:09.327' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL, N'', N'', N'FAMILY_CANCER_HISTORY', N'DSY')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [SummaryFieldName], [SummaryType]) VALUES (119, N'Are you currently smoking?', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes|No', N'', 7, 5, 18, 100, N'', CAST(N'2017-03-08 13:58:39.320' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL, N'', N'', N'CURRENTLY_SMOKE', N'SUM')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [SummaryFieldName], [SummaryType]) VALUES (120, N'Number of Pack Years:
Pack Years = (Number of sticks per day / 20 ) x Number of years smoking', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 8, 6, 18, 100, N'', CAST(N'2017-03-08 14:00:09.327' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL, N'', N'', N'NO_OF_PACK_YEAR', N'DSY')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [SummaryFieldName], [SummaryType]) VALUES (121, N'<p>Have you smoked before?</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes|No', N'', 9, 7, 18, 100, N'', CAST(N'2017-03-08 14:00:39.313' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', NULL, NULL, N'', N'', N'SMOKE_BEFORE', N'DSY')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (125, N'NRIC', N'Click to edit', N'NRICPICKER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 1, 1, 18, 100, N'', CAST(N'2017-03-18 10:00:21.747' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'NRIC', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (126, N'Gender', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Male|Female', N'', 3, 3, 18, 100, N'', CAST(N'2017-03-18 10:00:21.817' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'GENDER', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (127, N'Address', N'Click to edit', N'ADDRESS', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 2, 9, 18, 100, N'', CAST(N'2017-03-18 10:00:21.820' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'ADDRESS', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (128, N'Name (as in NRIC)', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 4, 0, 18, 100, N'', CAST(N'2017-03-25 20:45:27.263' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'FULLNAME', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (129, N'Salutation', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Mr|Ms|Mrs|Dr', N'', 5, 2, 18, 100, N'', CAST(N'2017-03-25 20:45:57.273' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'SALUTATION', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (130, N'Date of Birth', N'Click to edit', N'BIRTHDAYPICKER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 6, 4, 18, 100, N'', CAST(N'2017-03-25 20:46:42.260' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'DATEOFBIRTH', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (131, N'Citizenship', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Singapore Citizen|Singapore Permanent Resident (PR)', N'', 7, 5, 18, 100, N'', CAST(N'2017-03-25 20:47:27.283' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'CITIZENSHIP', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (132, N'Race', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'Chinese|Malay|Indian', N'', 8, 6, 18, 100, N'', CAST(N'2017-03-25 20:48:57.250' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'RACE', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (133, N'Language spoken by participant?', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'English|Mandarin|Malay|Tamil', N'', 9, 7, 18, 100, N'', CAST(N'2017-03-25 20:49:42.263' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'LANGUAGE', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (134, N'Prefered Time Slot', N'Click to edit', N'DROPDOWNLIST', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Morning|Afternoon', N'', 10, 8, 18, 100, N'', CAST(N'2017-03-25 20:50:27.263' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'PreferedTime', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (135, N'Does this patient require follow-up?', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes|No', N'', 1, 1, 18, 100, N'', CAST(N'2017-03-26 05:46:21.917' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (136, N'<p><span style="color: rgb(0, 0, 0); font-family: &quot;Times New Roman&quot;; font-size: medium; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Memo/Referral Letter</span></p>', N'Click to edit', N'DOCTORMEMO', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 2, 2, 18, 100, N'', CAST(N'2017-03-26 05:46:36.860' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [StandardReferenceID]) VALUES (137, N'BMI', N'Click to edit', N'BMI', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 1, 0, 18, 100, N'', CAST(N'2017-03-26 05:48:10.863' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'', 4)
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (138, N'1st Systolic Reading', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 2, 1, 18, 100, N'', CAST(N'2017-03-26 05:50:25.873' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (139, N'1st Diastolic Reading', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 4, 2, 18, 100, N'', CAST(N'2017-03-26 05:50:55.863' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (140, N'2nd Systolic Reading', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 6, 3, 18, 100, N'', CAST(N'2017-03-26 05:50:55.877' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (141, N'2nd Diastolic Reading', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 5, 4, 18, 100, N'', CAST(N'2017-03-26 05:50:55.877' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (142, N'3rd Diastolic Reading (if needed)', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 3, 6, 18, 100, N'', CAST(N'2017-03-26 05:50:55.880' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (143, N'3rd Systolic Reading (if needed)', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 8, 5, 18, 100, N'', CAST(N'2017-03-26 05:51:40.873' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [StandardReferenceID]) VALUES (144, N'Average Diastolic Reading (of all readings)', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 8, 18, 100, N'', CAST(N'2017-03-26 05:51:40.873' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'', 2)
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [StandardReferenceID]) VALUES (145, N'Average Systolic Reading (of all readings)', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 9, 7, 18, 100, N'', CAST(N'2017-03-26 05:51:55.867' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'', 1)
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (147, N'<p><b>Compliance to Personal Data Protection Act (PDPA)</b><br>I hereby give my consent to the Public Health Service Executive Committee to collect my personal information for the purpose of recruitment for the Public Health Service (hereby called “PHS”) and its related events, and to contact me via calls, SMS, text messages or emails regarding the event and follow-up process. Should you wish to withdraw your consent for us to contact you for the purposes stated above, please notify a member of the PHS Executive Committee at ask.phs@gmail.com in writing. We will then remove your personal information from our database. Please allow 3 business days for your withdrawal of consent to take effect. All personal information will be kept confidential, will only be disseminated to members of the PHS Executive Committee, and will be strictly used by these parties for the purposes stated.</p>', N'Click to edit', N'RADIOBUTTON', 1, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes', N'', 11, 10, 18, 100, N'', CAST(N'2017-04-01 15:11:40.763' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (150, N'Click to edit', N'Participant is being referred from: HX Taking Modality', N'HEADER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 3, 0, 18, 100, N'', CAST(N'2017-04-01 15:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (151, N'PatientID', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 1, 0, 18, 100, N'', CAST(N'2017-04-05 19:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (152, N'Name', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 2, 1, 18, 100, N'', CAST(N'2017-04-05 19:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (153, N'First Name', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 3, 2, 18, 100, N'', CAST(N'2017-04-05 19:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (154, N'DOB', N'Click to edit', N'BIRTHDAYPICKER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 4, 3, 18, 100, N'', CAST(N'2017-04-05 19:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (155, N'<p>Sex</p>', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Male|Female', N'', 5, 4, 18, 100, N'', CAST(N'2017-04-05 19:17:29.333' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [SummaryFieldName], [SummaryType], [ConditionTemplateFieldID], [ConditionCriteria], [ConditionOptions], [StandardReferenceID]) VALUES (158, N'Click to edit', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'', 0, NULL, 20, 20, N'option1|option2|option3', N'', 1, 0, 18, 100, N'', CAST(0x0000A7F000928860 AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'', N'', N'', NULL, N'', N'', NULL)
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [SummaryFieldName], [SummaryType], [ConditionTemplateFieldID], [ConditionCriteria], [ConditionOptions], [StandardReferenceID]) VALUES (159, N'for option 1', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 2, 1, 18, 100, N'', CAST(0x0000A7F0009299EB AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'', N'', N'', 158, N'==', N'option1', NULL)
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName], [SummaryFieldName], [SummaryType], [ConditionTemplateFieldID], [ConditionCriteria], [ConditionOptions], [StandardReferenceID]) VALUES (160, N'non option 1', N'Click to edit', N'DROPDOWNLIST', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1|option2', N'', 3, 2, 18, 100, N'', CAST(0x0000A7F00093336C AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'', N'', N'', 158, N'!=', N'option1', NULL)

SET IDENTITY_INSERT [dbo].[TemplateField] OFF
GO

INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 76)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 77)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 78)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 79)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 80)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 81)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 82)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 83)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 84)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 85)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 86)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 87)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 88)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 89)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 90)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 92)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 93)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 94)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (1, 103)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (2, 104)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (3, 113)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (3, 115)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (3, 116)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (3, 117)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (3, 118)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (3, 119)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (3, 120)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (3, 121)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 125)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 126)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 127)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 128)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 129)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 130)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 131)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 132)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 133)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 134)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (4, 147)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (5, 135)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (5, 136)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (5, 150)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (6, 137)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (6, 138)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (6, 139)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (6, 140)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (6, 141)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (6, 142)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (6, 143)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (6, 144)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (6, 145)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (7, 151)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (7, 152)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (7, 153)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (7, 154)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (7, 155)
GO
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (12, 158)
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (12, 159)
INSERT [dbo].[TemplateTemplateField] ([TemplateID], [TemplateFieldID]) VALUES (12, 160)
SET IDENTITY_INSERT [dbo].[TemplateFieldValue] ON 

GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (4, 125, N'74ce535d-af74-4db5-a745-9af7e6a41cef', N'S8939505D', CAST(N'2017-03-24 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (5, 126, N'74ce535d-af74-4db5-a745-9af7e6a41cef', N'Male', CAST(N'2017-03-24 14:30:56.520' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (6, 127, N'74ce535d-af74-4db5-a745-9af7e6a41cef', N'{"Id":null,"Blk":"BUKIT BATOK CENTRAL","Unit":"","StreetAddress":"BUKIT BATOK CENTRAL","State":null,"ZipCode":"650625","Country":null,"Longitude":null,"Latitude":null}', CAST(N'2017-03-24 14:30:56.523' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (300, 151, N'abc5a11a-f526-4616-8e51-853d2d796926', N'S8250369B', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (301, 152, N'abc5a11a-f526-4616-8e51-853d2d796926', N'Lawrence DDS', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (302, 153, N'abc5a11a-f526-4616-8e51-853d2d796926', N'Fay', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (303, 154, N'abc5a11a-f526-4616-8e51-853d2d796926', N'13/4/1982 12:00:00 AM', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (304, 155, N'abc5a11a-f526-4616-8e51-853d2d796926', N'Male', CAST(N'2017-04-05 14:30:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (305, 151, N'b611fbb9-4cbf-4414-959f-a7a95d39a0d3', N'S7931278I', CAST(N'2017-04-05 14:32:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (306, 152, N'b611fbb9-4cbf-4414-959f-a7a95d39a0d3', N'Maxwell', CAST(N'2017-04-05 14:32:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (307, 153, N'b611fbb9-4cbf-4414-959f-a7a95d39a0d3', N'Schulist', CAST(N'2017-04-05 14:32:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (308, 154, N'b611fbb9-4cbf-4414-959f-a7a95d39a0d3', N'13/2/1979 12:00:00 AM', CAST(N'2017-04-05 14:32:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (309, 155, N'b611fbb9-4cbf-4414-959f-a7a95d39a0d3', N'Male', CAST(N'2017-04-05 14:32:56.493' AS DateTime))
GO
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (310, 128, N'7051512f-7156-4e6b-9130-564c20e85d2e', N'Ben', CAST(0x0000A7B100C3DAB5 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (311, 125, N'7051512f-7156-4e6b-9130-564c20e85d2e', N'S8518538A', CAST(0x0000A7B100C3DAB6 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (312, 129, N'7051512f-7156-4e6b-9130-564c20e85d2e', N'Mr', CAST(0x0000A7B100C3DAB6 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (313, 126, N'7051512f-7156-4e6b-9130-564c20e85d2e', N'Male', CAST(0x0000A7B100C3DAB6 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (314, 130, N'7051512f-7156-4e6b-9130-564c20e85d2e', N'18/10/1982 12:00:00 AM', CAST(0x0000A7B100C3DAB6 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (315, 131, N'7051512f-7156-4e6b-9130-564c20e85d2e', NULL, CAST(0x0000A7B100C3DAB6 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (316, 132, N'7051512f-7156-4e6b-9130-564c20e85d2e', N'Chinese', CAST(0x0000A7B100C3DAB6 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (317, 133, N'7051512f-7156-4e6b-9130-564c20e85d2e', N'English', CAST(0x0000A7B100C3DAB6 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (318, 134, N'7051512f-7156-4e6b-9130-564c20e85d2e', N'Morning', CAST(0x0000A7B100C3DAB6 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (319, 127, N'7051512f-7156-4e6b-9130-564c20e85d2e', N'{"Id":null,"Blk":"484D","Unit":"","StreetAddress":"CHOA CHU KANG AVENUE 5","ZipCode":"684484"}', CAST(0x0000A7B100C3DAB6 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (320, 147, N'7051512f-7156-4e6b-9130-564c20e85d2e', N'Yes', CAST(0x0000A7B100C3DAB6 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (321, 79, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'ben', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (322, 80, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'Male', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (323, 81, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'Mr', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (324, 82, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'3/7/1982 12:00:00 AM', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (325, 83, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'Singapore Citizen 新加坡公民', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (326, 84, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'Chinese', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (327, 85, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'English', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (328, 86, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (329, 87, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'4-Room HDB Flat', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (330, 88, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'61234568', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (331, 89, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'98765432', CAST(0x0000A7B800B2039A AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (332, 90, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'University / Degree', CAST(0x0000A7B800B2039B AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (333, 76, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'Yes', CAST(0x0000A7B800B2039B AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (334, 77, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'Have not done a blood test in a year', CAST(0x0000A7B800B2039B AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (335, 78, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'English and Chinese', CAST(0x0000A7B800B2039B AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (336, 93, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'Yes participant consents to the above.', CAST(0x0000A7B800B2039B AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (337, 92, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'Yes participant consents to the above.', CAST(0x0000A7B800B2039B AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (338, 94, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'Participant consents to receiving periodic SMS updates with health tips and advice on healthy living as well as reminders to visit my primary care doctor for follow-up care.', CAST(0x0000A7B800B2039B AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (339, 103, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea', N'Yes', CAST(0x0000A7B800B2039B AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (340, 113, N'4e19b13c-8c4c-48e8-8489-f3b43a461d46', N'', CAST(0x0000A7B800D8913D AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (341, 115, N'4e19b13c-8c4c-48e8-8489-f3b43a461d46', N'NIL', CAST(0x0000A7B800D8913D AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (342, 116, N'4e19b13c-8c4c-48e8-8489-f3b43a461d46', N'NIL', CAST(0x0000A7B800D8913D AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (343, 117, N'4e19b13c-8c4c-48e8-8489-f3b43a461d46', N'Diabetes Mellitus 糖尿病', CAST(0x0000A7B800D8913D AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (344, 118, N'4e19b13c-8c4c-48e8-8489-f3b43a461d46', N'NIL', CAST(0x0000A7B800D8913D AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (345, 119, N'4e19b13c-8c4c-48e8-8489-f3b43a461d46', N'No', CAST(0x0000A7B800D8913D AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (346, 120, N'4e19b13c-8c4c-48e8-8489-f3b43a461d46', N'', CAST(0x0000A7B800D8913D AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (347, 121, N'4e19b13c-8c4c-48e8-8489-f3b43a461d46', N'No', CAST(0x0000A7B800D8913D AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (348, 137, N'c36dc1a4-8566-4710-a22f-5410adb4efe3', N'{"Weight":"85","Height":"178","BodyMassIndex":"26.83"}', CAST(0x0000A7B800DC92D0 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (349, 138, N'c36dc1a4-8566-4710-a22f-5410adb4efe3', N'89', CAST(0x0000A7B800DC92D1 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (350, 139, N'c36dc1a4-8566-4710-a22f-5410adb4efe3', N'90', CAST(0x0000A7B800DC92D1 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (351, 140, N'c36dc1a4-8566-4710-a22f-5410adb4efe3', N'100', CAST(0x0000A7B800DC92D1 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (352, 141, N'c36dc1a4-8566-4710-a22f-5410adb4efe3', N'101', CAST(0x0000A7B800DC92D1 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (353, 143, N'c36dc1a4-8566-4710-a22f-5410adb4efe3', N'121', CAST(0x0000A7B800DC92D1 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (354, 142, N'c36dc1a4-8566-4710-a22f-5410adb4efe3', N'120', CAST(0x0000A7B800DC92D1 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (355, 145, N'c36dc1a4-8566-4710-a22f-5410adb4efe3', N'150', CAST(0x0000A7B800DC92D1 AS DateTime))
INSERT [dbo].[TemplateFieldValue] ([TemplateFieldValueID], [TemplateFieldID], [EntryId], [Value], [DateAdded]) VALUES (356, 144, N'c36dc1a4-8566-4710-a22f-5410adb4efe3', N'144', CAST(0x0000A7B800DC92D1 AS DateTime))
SET IDENTITY_INSERT [dbo].[TemplateFieldValue] OFF
GO

--- Modality forms Sample  --


GO
INSERT [phs].[dbo].[ModalityForm] ([ModalityID], [FormID]) VALUES (1, 1)
GO

GO
INSERT [phs].[dbo].[ModalityForm] ([ModalityID], [FormID]) VALUES (1, 8)
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
INSERT [phs].[dbo].[ModalityForm] ([ModalityID], [FormID]) VALUES (9, 9)
GO

GO
INSERT [phs].[dbo].[ModalityForm] ([ModalityID], [FormID]) VALUES (13, 6)
GO

INSERT [phs].[dbo].[ParticipantJourneyModality] ([ParticipantID], [PHSEventID], [ModalityID], [FormID], [TemplateID], [EntryId]) VALUES (1, 2, 1, 1, 1, N'0d925b5a-c6f0-45d9-ba08-fe4840fe7aea')
INSERT [phs].[dbo].[ParticipantJourneyModality] ([ParticipantID], [PHSEventID], [ModalityID], [FormID], [EntryId]) VALUES (1, 2, 1, 8, N'00000000-0000-0000-0000-000000000000')
INSERT [phs].[dbo].[ParticipantJourneyModality] ([ParticipantID], [PHSEventID], [ModalityID], [FormID], [TemplateID], [EntryId]) VALUES (1, 2, 2, 3, 3, N'4e19b13c-8c4c-48e8-8489-f3b43a461d46')
INSERT [phs].[dbo].[ParticipantJourneyModality] ([ParticipantID], [PHSEventID], [ModalityID], [FormID], [TemplateID], [EntryId]) VALUES (1, 2, 2, 6, 6, N'c36dc1a4-8566-4710-a22f-5410adb4efe3')
INSERT [phs].[dbo].[ParticipantJourneyModality] ([ParticipantID], [PHSEventID], [ModalityID], [FormID], [TemplateID], [EntryId]) VALUES (1, 3, 1, 1, NULL, N'00000000-0000-0000-0000-000000000000')
INSERT [phs].[dbo].[ParticipantJourneyModality] ([ParticipantID], [PHSEventID], [ModalityID], [FormID], [TemplateID], [EntryId]) VALUES (1, 3, 1, 8, NULL, N'00000000-0000-0000-0000-000000000000')
INSERT [phs].[dbo].[ParticipantJourneyModality] ([ParticipantID], [PHSEventID], [ModalityID], [FormID], [TemplateID], [EntryId]) VALUES (1, 3, 2, 3, NULL, N'00000000-0000-0000-0000-000000000000')
INSERT [phs].[dbo].[ParticipantJourneyModality] ([ParticipantID], [PHSEventID], [ModalityID], [FormID], [TemplateID], [EntryId]) VALUES (1, 3, 2, 6, NULL, N'00000000-0000-0000-0000-000000000000')
INSERT [phs].[dbo].[ParticipantJourneyModality] ([ParticipantID], [PHSEventID], [ModalityID], [FormID], [TemplateID], [EntryId]) VALUES (1, 3, 13, 6, NULL, N'00000000-0000-0000-0000-000000000000')
GO


/**
GO
INSERT [phs].[dbo].[Summary] ([Label], [ParticipantID], [PHSEventID], [ModalityID], [TemplateFieldID], [SummaryValue], [SummaryType]) VALUES (N'Name', 1, 2, 1, 79, N'Test Name', N'ESY')
INSERT [phs].[dbo].[Summary] ([Label], [ParticipantID], [PHSEventID], [ModalityID], [TemplateFieldID], [SummaryValue], [SummaryType]) VALUES (N'Gender', 1, 2, 1, 80, N'Male', N'ESY')
INSERT [phs].[dbo].[Summary] ([Label], [ParticipantID], [PHSEventID], [ModalityID], [TemplateFieldID], [SummaryValue], [SummaryType]) VALUES (N'DOCTORMEMO', 1, 2, 9, 136, N'Memo Test', N'DSY')
INSERT [phs].[dbo].[Summary] ([Label], [ParticipantID], [PHSEventID], [ModalityID], [TemplateFieldID], [SummaryValue], [SummaryType]) VALUES (N'Name', 1, 3, 1, 79, N'Test Name', N'ESY')
INSERT [phs].[dbo].[Summary] ([Label], [ParticipantID], [PHSEventID], [ModalityID], [TemplateFieldID], [SummaryValue], [SummaryType]) VALUES (N'Gender', 1, 3, 1, 80, N'Male', N'ESY')
INSERT [phs].[dbo].[Summary] ([Label], [ParticipantID], [PHSEventID], [ModalityID], [TemplateFieldID], [SummaryValue], [SummaryType]) VALUES (N'DOCTORMEMO', 1, 3, 9, 136, N'Memo Test', N'DSY')
GO
**/

--- Follow-up configuration Sample  --
SET IDENTITY_INSERT [phs].[dbo].[FollowUpConfiguration] ON
GO
INSERT [phs].[dbo].[FollowUpConfiguration] ([FollowUpConfigurationID], [Title], [Deploy], [PHSEventID]) VALUES (1, N'PHS 2017-Jurong Followup Configuration', 0, 3)
GO

--GO
--INSERT [phs].[dbo].[FollowUpConfiguration] ([FollowUpConfigurationID], [Title], [Deploy], [PHSEventID]) VALUES (2, N'Configuration 2', 0, 3)
--GO
SET IDENTITY_INSERT [phs].[dbo].[FollowUpConfiguration] OFF
GO

--- Follow-up configuration Sample  --

SET IDENTITY_INSERT [phs].[dbo].[FollowUpGroup] ON
GO
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (1, N'C1 - N', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (2, N'C2 - NE/T', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (3, N'C3 - N/_', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (4, N'C4 - E/T', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (5, N'C5 - NE/P', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (6, N'C6 - NE/TP', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (7, N'C7 - NE/_P', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (8, N'C8 - E/P', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (9, N'C9 - E/TP', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (10, N'C10 - NE/M', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (11, N'C11 - NE/TM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (12, N'C12 - NE/_M', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (13, N'C13 - E/TM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (14, N'C14 - NE/PM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (15, N'C15 - NE/TPM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (16, N'C16 - NE/_PM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (17, N'C17 - E/PM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (18, N'C18 - E/TPM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (19, N'M1 - N', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (20, N'M2 - NE/T', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (21, N'M3 - N/_', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (22, N'M4 - E/T', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (23, N'M5 - NE/P', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (24, N'M6 - NE/TP', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (25, N'M7 - NE/_P', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (26, N'M8 - E/P', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (27, N'M9 - E/TP', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (28, N'M10 - NE/M', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (29, N'M11 - NE/TM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (30, N'M12 - NE/_M', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (31, N'M13 - E/TM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (32, N'M14 - NE/PM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (33, N'M15 - NE/TPM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (34, N'M16 - NE/_PM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (35, N'M17 - E/PM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (36, N'M18 - E/TPM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (37, N'T1 - N', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (38, N'T2 - NE/T', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (39, N'TI3 - N/_', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (40, N'T4 - E/T', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (41, N'T3 - N/_', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (42, N'T5 - NE/P', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (43, N'T6 - NE/TP', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (44, N'T7 - NE/_P', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (45, N'T8 - E/P', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (46, N'T9 - E/TP', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (47, N'T10 - NE/M', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (48, N'T11 - NE/TM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (49, N'T12 - NE/_M', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (50, N'T13 - E/TM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (51, N'T14 - NE/PM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (52, N'T15 - NE/TPM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (53, N'T16 - NE/_PM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (54, N'T17 - E/PM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (55, N'T18 - E/TPM', N'3#1#1#1#80#==#Male', 1)
INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (56, N'Others', N'3#1#1#1#80#==#Male', 1)
GO
--INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (57, N'Group TestA', N'3#1#1#1#80#==#Male', 2)
--GO

--GO
--INSERT [phs].[dbo].[FollowUpGroup] ([FollowUpGroupID], [Title], [Filter], [FollowUpConfigurationID]) VALUES (58, N'Group TestB', N'3#1#1#1#80#==#Male', 2)
--GO
SET IDENTITY_INSERT [phs].[dbo].[FollowUpGroup] OFF
GO
