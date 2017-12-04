USE [phs]

IF OBJECT_ID('dbo.ParticipantCallerMapping', 'U') IS NOT NULL 
  DROP TABLE [dbo].[ParticipantCallerMapping];

IF OBJECT_ID('dbo.FollowUpGroup', 'U') IS NOT NULL 
  DROP TABLE [dbo].FollowUpGroup;

IF OBJECT_ID('dbo.FollowUpConfiguration', 'U') IS NOT NULL 
  DROP TABLE [dbo].FollowUpConfiguration;

ALTER TABLE [dbo].[Modality]
  ADD [Role] [nvarchar](max) NULL
  
ALTER TABLE [dbo].[TemplateField] ALTER COLUMN [ConditionOptions] [nvarchar](200) NULL
  
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
