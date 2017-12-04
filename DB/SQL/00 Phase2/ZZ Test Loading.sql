
-- to remove 
use [phs]

insert into [phs].dbo.Participant (nric) 
select nric from phsFollowUpGroups.dbo.['Report Summary'] where NRIC is not null and NRIC not in (select nric from Participant)
-- to remove 

-- to remove - change this before patching
use [phsLabResultsLIVE]
-- to remove

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

declare @ptID as int
declare @ptNRIC as varchar(100)
declare @count as int 
set @count = 0 

create table #ptLab (
participantID int,
participantNRIC varchar(100)
)

-- new cursor

declare @clinicName varchar(500)

declare ptListDataEntry cursor for 
select rs.nric from phsFollowUpGroups.dbo.['Report Summary'] rs left join phs.dbo.participant pt on
rs.nric = pt.nric where rs.NRIC is not null and pt.nric is not null 


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

		
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDSexReg, cast(cast((@ptID + 2) as varbinary(32)) as uniqueidentifier), (select Gender from phs.dbo.participant where NRIC = @ptNRIC), GETDATE() 
		 
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select @TFIDLangReg, cast(cast((@ptID + 2) as varbinary(32)) as uniqueidentifier), (select Language from phs.dbo.participant where NRIC = @ptNRIC), GETDATE() 
		
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
		
		-- add values to hx taking vitals
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select 466, cast(cast((@ptID + 1) as varbinary(32)) as uniqueidentifier), (select [Average Reading - DYS] from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC), GETDATE()
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType, StandardReferenceID)
		select 'HxTakingField83', @ptID, 3, 19, 466, (select Label from phs.dbo.TemplateField where TemplateFieldID = 466),  (select [Average Reading - SYS] from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC), 'FILL', 1
 
		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select 467, cast(cast((@ptID + 1) as varbinary(32)) as uniqueidentifier), (select [Average Reading - SYS] from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC), GETDATE()
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType, StandardReferenceID)
		select 'HxTakingField79', @ptID, 3, 19, 467, (select Label from phs.dbo.TemplateField where TemplateFieldID = 467),  (select [Average Reading - DYS] from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC), 'FILL', 2

		insert into phs.dbo.TemplateFieldValue (TemplateFieldID, EntryId, Value, DateAdded) 
		select 470, cast(cast((@ptID + 1) as varbinary(32)) as uniqueidentifier), 
		CONCAT('{"Weight":"',(select Height from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'","Height":"', (select Weight from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'","BodyMassIndex":"',
		(select BMI from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'"}')
		, GETDATE() 
		insert into phs.dbo.Summary (Label, ParticipantID, PHSEventID, ModalityID, TemplateFieldID, SummaryLabel, SummaryValue, SummaryType,StandardReferenceID)
		select 'HxTakingField80', @ptID, 3, 19, 470, (select Label from phs.dbo.TemplateField where TemplateFieldID = 470),  
		CONCAT('{"Weight":"',(select Height from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'","Height":"', (select Weight from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'","BodyMassIndex":"',
		(select BMI from phsFollowUpGroups.dbo.['Report Summary'] where NRIC = @ptNRIC),'"}')
		, 'FILL', 4

		insert into phs.dbo.ParticipantPHSEvent (ParticipantID, PHSEventID)
		select @ptID, 3
		
	fetch NEXT from ptListDataEntry into @ptNRIC 
end 

close ptListDataEntry
deallocate ptListDataEntry 
	
--- new cursor 



