
-- to remove 
use [phs]

insert into [phs].dbo.Participant (nric) 
select nric from phsFollowUpGroups.dbo.['Report Summary'] where NRIC is not null and NRIC not in (select nric from Participant)
-- to remove 




-- to remove - change this before patching
use [phsLabResultsLIVE]
-- to remove


-- select * from [phs].dbo.Participant where Nric = @ptNRIC

--insert into [phs].dbo.Participant (nric) 
--select [Patient number] from LabResults

ALTER TABLE phs.dbo.TemplateFieldValue
DROP CONSTRAINT FK_template_field_values_template_fields

alter table phs.dbo.participantjourneymodality
drop constraint [FK participant_journey_modality_form]
alter table phs.dbo.participantjourneymodality
drop constraint [FK participant_journey_modality_modality]
alter table phs.dbo.participantjourneymodality
drop constraint [FK participant_journey_modality_template]
alter table phs.dbo.ParticipantCallerMapping 
drop constraint [FK participantcallermapping_followupgroup]
-- templatefieldvalue FK_template_field_values_template_fields
-- pjm FK participant_journey_modality_form
-- pjm FK participant_journey_modality_modality
-- pjm FK participant_journey_modality_template


declare @yearEventID2017 as int 
set @yearEventID2017 = 3

declare @TFIDPatient			as int set @TFIDPatient			 = 483
declare @TFIDPatientnumber		as int set @TFIDPatientnumber	 = 484
declare @TFIDBirthdate			as int set @TFIDBirthdate		 = 485
declare @TFIDSex				as int set @TFIDSex				 = 486
declare @TFIDAdditionaldata1	as int set @TFIDAdditionaldata1 = 487
declare @TFIDAdditionaldata2	as int set @TFIDAdditionaldata2 = 488
declare @TFIDRequest#		    as int set @TFIDRequest#		   = 489
declare @TFIDColldate		    as int set @TFIDColldate		   = 490
declare @TFIDSourcelab			as int set @TFIDSourcelab	   = 491
declare @TFIDLocation		    as int set @TFIDLocation		   = 492
declare @TFIDDoctor				as int set @TFIDDoctor		   = 493
declare @TFIDCholesterol	    as int set @TFIDCholesterol	   = 494
declare @TFIDTriglycerides		as int set @TFIDTriglycerides   = 495
declare @TFIDHDLC			    as int set @TFIDHDLC			   = 496
declare @TFIDLDLCcalculated		as int set @TFIDLDLCcalculated  = 497
declare @TFIDCholDLRatio	    as int set @TFIDCholDLRatio	   = 498
declare @TFIDGlucoseFasting		as int set @TFIDGlucoseFasting  = 499
declare @TFIDClinic				as int set @TFIDClinic = 597
declare @TFIDSexReg				as int set @TFIDSexReg = 567
declare @TFIDLangReg				as int set @TFIDLangReg = 576

create table #ptLab (
participantID int,
participantNRIC varchar(100)
)

insert into #ptLab (participantNRIC) select [Patient number] from LabResults

-- drop table #ptlab
-- select * from #ptlab where participantID is null 

update #ptLab set participantID = pt.ParticipantID from #ptlab ptlab left join phs.dbo.Participant pt
on ptlab.participantNRIC = pt.Nric

		

		-- add telesummary
		INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
		VALUES ('Health Report Summary', null, 0, null, 'TELESUM', GETDATE(), 1)
		declare @FormHealhReportSum as int
		select @FormHealhReportSum = IDENT_CURRENT('phs.dbo.Form')

		INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
		VALUES (@FormHealhReportSum, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)
		declare @FormHealhReportSumTemplateID as int
		select @FormHealhReportSumTemplateID = IDENT_CURRENT('phs.dbo.Template')

		INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID])  VALUES (28, @FormHealhReportSum)



declare @ptID as int
declare @ptNRIC as varchar(100)
declare @count as int 
set @count = 0 

declare ptLabList cursor for 
select participantNRIC from #ptLab where participantID is not null 

open ptLabList 
fetch NEXT from ptLabList into @ptNRIC 

while @@FETCH_STATUS = 0
begin 
--set identity_insert phs.dbo.TemplateFieldValue off
		select @ptID = participantID from #ptLab where participantNRIC = @ptNRIC
		
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDPatient, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), patient, GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDPatientnumber, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), [Patient number], GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDBirthdate, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), Birthdate, GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDSex, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), Sex, GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDAdditionaldata1, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), [Additional data 1], GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDAdditionaldata2, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), [Additional data 2], GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDRequest#, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), [Request #], GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDColldate, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), [Coll# date], GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDSourcelab, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), [Source lab], GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDLocation, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), Location, GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDDoctor, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), Doctor, GETDATE() from LabResults where [Patient number] = @ptNRIC

		-- lab related
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDCholesterol, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), Cholesterol, GETDATE() from LabResults where [Patient number] = @ptNRIC			
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType)
		select 'LabField0', @ptID, 3, 29, @TFIDCholesterol, (select Label from phs.dbo.TemplateField where TemplateFieldID = @TFIDCholesterol),  Cholesterol, 'FILL'
		from LabResults where  [Patient number] = @ptNRIC

		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDTriglycerides, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), Triglycerides, GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType)
		select 'LabField1', @ptID, 3, 29, @TFIDTriglycerides, (select Label from phs.dbo.TemplateField where TemplateFieldID = @TFIDTriglycerides),  Triglycerides, 'FILL'
		from LabResults where  [Patient number] = @ptNRIC

		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDHDLC, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), [HDL-C], GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType)
		select 'LabField2', @ptID, 3, 29, @TFIDHDLC, (select Label from phs.dbo.TemplateField where TemplateFieldID = @TFIDHDLC),  [HDL-C], 'FILL'
		from LabResults where  [Patient number] = @ptNRIC

		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDLDLCcalculated, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), [LDL-C, calculated], GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType)
		select 'LabField3', @ptID, 3, 29, @TFIDLDLCcalculated, (select Label from phs.dbo.TemplateField where TemplateFieldID = @TFIDLDLCcalculated),  [LDL-C, calculated], 'FILL'
		from LabResults where  [Patient number] = @ptNRIC

		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDCholDLRatio, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), [Chol : HDL Ratio], GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType)
		select 'LabField4', @ptID, 3, 29, @TFIDCholDLRatio, (select Label from phs.dbo.TemplateField where TemplateFieldID = @TFIDCholDLRatio),  [Chol : HDL Ratio], 'FILL'
		from LabResults where  [Patient number] = @ptNRIC

		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDGlucoseFasting, cast(cast((@ptID) as varbinary(32)) as uniqueidentifier), [Glucose, Fasting], GETDATE() from LabResults where [Patient number] = @ptNRIC
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType)
		select 'LabField5', @ptID, 3, 29, @TFIDGlucoseFasting, (select Label from phs.dbo.TemplateField where TemplateFieldID = @TFIDGlucoseFasting),  [Glucose, Fasting], 'FILL'
		from LabResults where  [Patient number] = @ptNRIC

		
		
		
		

	fetch NEXT from ptLabList into @ptNRIC 
end 
 
--set identity_insert phs.dbo.TemplateFieldValue on

close ptLabList
deallocate ptLabList 


-- new cursor

declare @clinicName varchar(500)

declare ptListDataEntry cursor for 
select nric from phsFollowUpGroups.dbo.['Report Summary'] where NRIC is not null 

open ptListDataEntry 
fetch NEXT from ptListDataEntry into @ptNRIC 

while @@FETCH_STATUS = 0
begin 
		select @ptID = participantID from phs.dbo.Participant where Nric = @ptNRIC

		select @clinicName = [Name of Clinic] from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC

		-- add values to reg form 
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDClinic, cast(cast((@ptID + 2) as varbinary(32)) as uniqueidentifier), @clinicName, GETDATE() 
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 17, 44, 45,  cast(cast((@ptID + 2) as varbinary(32)) as uniqueidentifier) 
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType)
		select 'RegField2', @ptID, 3, 17, @TFIDClinic, (select Label from phs.dbo.TemplateField where TemplateFieldID = @TFIDClinic),  @clinicName, 'FILL'


		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDSexReg, cast(cast((@ptID + 2) as varbinary(32)) as uniqueidentifier), (select Gender from phs.dbo.participant where NRIC = @ptNRIC), GETDATE() 
		 
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDLangReg, cast(cast((@ptID + 2) as varbinary(32)) as uniqueidentifier), (select Language from phs.dbo.participant where NRIC = @ptNRIC), GETDATE() 




		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 28, @FormHealhReportSum, @FormHealhReportSumTemplateID,  cast(cast((0) as varbinary(32)) as uniqueidentifier) 	

		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 17, 8	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 17, 43	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 17, 44	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 19, 30	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 19, 31	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 19, 32	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 19, 33	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 19, 34	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 19, 35	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		--select @ptID, 3, 19, 36	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		--insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 19, 37	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 25, 42	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 26, 52	, 52,	cast(cast((0) as varbinary(32)) as uniqueidentifier)
		--insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		--select @ptID, 3, 30, 41	, NULL, cast(cast((0) as varbinary(32)) as uniqueidentifier)
		
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 19, 35, 36,  cast(cast((@ptID + 1) as varbinary(32)) as uniqueidentifier) 
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 29, 40, 41,  cast(cast((@ptID) as varbinary(32)) as uniqueidentifier) 


		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 28, 54, 54,  cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 28, 55, 55,  cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 28, 56, 56,  cast(cast((0) as varbinary(32)) as uniqueidentifier)
		insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
		select @ptID, 3, 28, 57, 57,  cast(cast((0) as varbinary(32)) as uniqueidentifier)



		-- add values to hx taking vitals
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select 466, cast(cast((@ptID + 1) as varbinary(32)) as uniqueidentifier), (select [Average Reading - DYS] from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC), GETDATE()
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType)
		select 'HxTakingField83', @ptID, 3, 19, 466, (select Label from phs.dbo.TemplateField where TemplateFieldID = 466),  (select [Average Reading - SYS] from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC), 'FILL'
 
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select 467, cast(cast((@ptID + 1) as varbinary(32)) as uniqueidentifier), (select [Average Reading - SYS] from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC), GETDATE() 
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType)
		select 'HxTakingField79', @ptID, 3, 19, 467, (select Label from phs.dbo.TemplateField where TemplateFieldID = 467),  (select [Average Reading - DYS] from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC), 'FILL'

		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select 470, cast(cast((@ptID + 1) as varbinary(32)) as uniqueidentifier), 
		CONCAT('{"Weight":"',(select Height from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'","Height":"', (select Weight from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'","BodyMassIndex":"',
		(select BMI from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'"}')
		, GETDATE() 
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType)
		select 'HxTakingField80', @ptID, 3, 19, 470, (select Label from phs.dbo.TemplateField where TemplateFieldID = 470),  
		CONCAT('{"Weight":"',(select Height from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'","Height":"', (select Weight from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'","BodyMassIndex":"',
		(select BMI from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'"}')
		, 'FILL'


		insert into phs.dbo.ParticipantPHSEvent (ParticipantID, PHSEventID)
		select @ptID, 3
		
	fetch NEXT from ptListDataEntry into @ptNRIC 
end 

close ptListDataEntry
deallocate ptListDataEntry 
	
--- new cursor 



drop table #ptlab



update phs.dbo.TemplateField set SummaryFieldName = 'RegField2' where templatefieldid = 597


update phs.dbo.Modality set IsVisible = 1 where ModalityID = 28
update phs.dbo.Modality set IsVisible = 1 where ModalityID = 29

insert into phs.dbo.SummaryMapping (SummaryType, CategoryName, SummaryFieldName)
select 'TeleHealth_SUM', 'Health Summary Report', 'RegField2'
insert into phs.dbo.SummaryMapping (SummaryType, CategoryName, SummaryFieldName)
select 'TeleHealth_SUM', 'Health Summary Report', 'HxTakingField83'
insert into phs.dbo.SummaryMapping (SummaryType, CategoryName, SummaryFieldName)
select 'TeleHealth_SUM', 'Health Summary Report', 'HxTakingField79'
insert into phs.dbo.SummaryMapping (SummaryType, CategoryName, SummaryFieldName)
select 'TeleHealth_SUM', 'Health Summary Report', 'HxTakingField80'
insert into phs.dbo.SummaryMapping (SummaryType, CategoryName, SummaryFieldName)
select 'TeleHealth_SUM', 'Health Summary Report', 'LabField0'
insert into phs.dbo.SummaryMapping (SummaryType, CategoryName, SummaryFieldName)
select 'TeleHealth_SUM', 'Health Summary Report', 'LabField1'
insert into phs.dbo.SummaryMapping (SummaryType, CategoryName, SummaryFieldName)
select 'TeleHealth_SUM', 'Health Summary Report', 'LabField2'
insert into phs.dbo.SummaryMapping (SummaryType, CategoryName, SummaryFieldName)
select 'TeleHealth_SUM', 'Health Summary Report', 'LabField3'
insert into phs.dbo.SummaryMapping (SummaryType, CategoryName, SummaryFieldName)
select 'TeleHealth_SUM', 'Health Summary Report', 'LabField4'
insert into phs.dbo.SummaryMapping (SummaryType, CategoryName, SummaryFieldName)
select 'TeleHealth_SUM', 'Health Summary Report', 'LabField5'


use [phsFollowUpGroups]

declare @fuPTNRIC varchar(100)
declare @fuPTID int 
declare @fuGroupID int 

insert into phs.dbo.ParticipantCallerMapping (ParticipantID, FollowUpGroupID)
select ISNULL(pt.ParticipantID,0), isnull((select FollowUpGroupID from phs.dbo.FollowUpGroup where Title = rs.reportname),0)
from phs.dbo.Participant pt left join ['Report Summary'] rs on
pt.Nric = rs.nric where rs.nric is not null 



--set identity_insert phs.dbo.TemplateFieldValue on



use [phs]
update FollowUpConfiguration set Deploy = 1 where Title = 'PHS 2017-Jurong Followup Configuration'
ALTER TABLE [dbo].[TemplateFieldValue]  WITH CHECK ADD  CONSTRAINT [FK_template_field_values_template_fields] FOREIGN KEY([TemplateFieldID])
REFERENCES [dbo].[TemplateField] ([TemplateFieldID])
ON DELETE CASCADE
ALTER TABLE [dbo].[ParticipantJourneyModality]  WITH CHECK ADD  CONSTRAINT [FK participant_journey_modality_form] FOREIGN KEY([FormID])
REFERENCES [dbo].[Form] ([FormID])

ALTER TABLE [dbo].[ParticipantJourneyModality]  WITH CHECK ADD  CONSTRAINT [FK participant_journey_modality_modality] FOREIGN KEY([ModalityID])
REFERENCES [dbo].[Modality] ([ModalityID])

--ALTER TABLE [dbo].[ParticipantJourneyModality]  WITH CHECK ADD  CONSTRAINT [FK participant_journey_modality_template] FOREIGN KEY([TemplateID])
--REFERENCES [dbo].[Template] ([TemplateID])


