use [phs]

declare @eventID as int 
select @eventID = PHSEventID from PHSEvent where Title like '%2017%'

delete from phs.dbo.EventModality where PHSEventID = @eventID

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Registration', 0, N'../../../Content/images/PHS2017/01_Registration.png', 1, 1, 1, 0, N'Pending', NULL, 2)
declare @ModReg as int
select @ModReg = IDENT_CURRENT('phs.dbo.Modality')

insert ModalityForm values(@ModReg, 8)

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Phlebotomy', 1, N'../../../Content/images/PHS2017/02_Phlebotomy.png', 0, 1, 0, 0, N'Pending', 
'Fasted for &ge; 10h
Have not done blood glucose/lipids test in past 1 year
Not diagnosed with diabetes mellitus or hyperlipidemia or hypertension', 3)
declare @ModPhlebo as int
select @ModPhlebo = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Hx Taking', 2, N'../../../Content/images/PHS2017/03_HxTaking.png', 1, 1, 1, 0, N'Pending', NULL, 0)
declare @ModHx as int
select @ModHx = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'FIT', 3, N'../../../Content/images/PHS2017/04_FIT.png', 0, 1, 0, 0, N'Pending', 
'&ge; 50 years old
Have not done FIT in the past 1 year
Have not done colonoscopy in the past 5 years
Not diagnosed with colorectal cancer', 3)
declare @ModFIT as int
select @ModFIT = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'WCE', 4, N'../../../Content/images/PHS2017/05_WCE.png', 0, 1, 0, 0, N'Pending', 'Female &ge; 40 years old', 0)
declare @ModWCE as int
select @ModWCE = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Geriatrics', 5, N'../../../Content/images/PHS2017/06_Geri.png', 0, 1, 0, 0, N'Pending', '&ge; 60 years old', 0)
declare @ModGeri as int
select @ModGeri = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Doctor''s Consult', 6, N'../../../Content/images/PHS2017/07_Doc.png', 0, 1, 0, 0, N'Pending', 'Referral only', 0)
declare @ModDoc as int
select @ModDoc = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Oral Health', 7, N'../../../Content/images/PHS2017/08_Oral.png', 0, 1, 0, 0, N'Pending', '&ge; 40 years old', 0)
declare @ModOral as int
select @ModOral = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Questionnaire Collection', 8, N'../../../Content/images/PHS2017/09_Question.png', 1, 1, 1, 0, N'Pending', NULL, 0)
declare @ModQuest as int
select @ModQuest = IDENT_CURRENT('phs.dbo.Modality')


INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Exhibition', 9, N'../../../Content/images/PHS2017/10_Exhibit.png', 1, 1, 0, 0, N'Pending', NULL, 0)
declare @ModExhibit as int
select @ModExhibit = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Social Support', 10, N'../../../Content/images/PHS2017/11_SocialSupport.png', 0, 1, 0, 0, N'Pending', NULL, 0)
declare @ModSocialSupport as int
select @ModSocialSupport = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Telehealth', 10, N'../../../Content/images/PHS2017/11_Tele.png', 0, 0, 0, 0, N'Pending', NULL, 0)
declare @ModTele as int
select @ModTele = IDENT_CURRENT('phs.dbo.Modality')



INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Public Forms', 99, N'../../../Content/images/PHS2017/.png', 1, 1, 1, 0, N'Public', NULL, 0)
declare @ModPublic as int
select @ModPublic = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModReg)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModPhlebo)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModHx)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModFIT)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModWCE)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModGeri)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModDoc)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModOral)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModQuest)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModExhibit)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModTele)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModSocialSupport)
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModPublic)

use [phsDataLoading]
-- data cleansing
update DataCollection set Mandatory = 0 where Mandatory in ('NIL', 'NO', '-')
update DataCollection set Mandatory = 0 where Mandatory is null
update DataCollection set Mandatory = 1 where Mandatory = 'Yes'
update DataCollection set [Type ] = 'HEADER' where [Type ] is null 
update DataCollection set [Type ] = 'HEADER' where [Type ] = 'Label'
update DataCollection set [Type ] = 'RADIOBUTTON' where [Type ] in ('Radio','Radio & Textbox','Radio + Textbox','Radio/ Textbox','Scale','Sliding Scale (from 1-10)')
update DataCollection set [Type ] = 'CHECKBOX' where [Type ] in ('Checkbox', 'Checkbox + Textbox')
update DataCollection set [Type ] = 'TEXTAREA' where [Type ] in ('Large textbox','TextArea','TextField')
update DataCollection set [Type ] = 'TEXTBOX' where [Type ] in ('Text','Textbox')
update DataCollection set [Type ] = 'DROPDOWNLIST' where [Type ] in ('Dropdown')
update DataCollection set [Type ] = 'BIRTHDAYPICKER' where [Type ] in ('DateTime')
update DataCollection set Modality = 'Geriatrics' where Modality = 'Geri'
update DataCollection set Modality = 'Hx Taking' where Modality = 'History Taking'
update DataCollection set Modality = 'Public Forms' where Modality = 'Public'
update DataCollection set AddOthersOption = 0 where AddOthersOption is null 
update DataCollection set [Type ] = 'SIGNATURE' where [Type ] = 'Signature'
update DataCollection set [Type ] = 'RADIOBUTTON' where [Type ] in ('DROPDOWNLIST')
update DataCollection set [Type ] = 'HEADER' where [Type ] = 'Big Label'
update DataCollection set [Type ] = 'HEADERSUB' where [Type ] = 'Small Label'
update DataCollection set [Type ] = 'IMAGE' where [Type ] = 'Image'
update DataCollection set [Type ] = 'TEXTAREA' where [Type ] = 'Text Area'

update DataCollection set Mandatory = 0 where [Type ] = 'HEADER'
update DataCollection set Mandatory = 0 where [Type ] = 'HEADERSUB'


declare @modality varchar(100)
declare @form varchar(100)

declare @formPositionCounter int 
set @formPositionCounter = 0

declare @modalityID int
declare @formID int
declare @templateID int
declare @LASTfieldID int
declare @templateFieldID int 

declare @parentStagingConditionalFieldIDTemp int
declare @parentConditionalFieldIDTemp int

declare modalityList cursor for 
select distinct(modality) from DataCollection

open modalityList 
fetch NEXT from modalityList into @modality 

while @@FETCH_STATUS = 0
begin 
 
	
		declare formList cursor for 
		select distinct(form) from DataCollection where Modality = @modality
		open formList 
		fetch NEXT from formList into @form 
		
		while @@FETCH_STATUS = 0 
		begin 
			--print 'form: ' + @form;

			select @modalityID = ModalityID from phs.dbo.Modality where Name = @modality; 

			INSERT phs.[dbo].[Form] ([Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) 
			VALUES (@form, NULL, 0, NULL, NULL, getdate(), 1)
			SELECT @formID = IDENT_CURRENT('phs.dbo.Form')

			INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
			VALUES (@formID, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)
			SELECT @templateID = IDENT_CURRENT('phs.dbo.Template')

			SELECT @LASTfieldID = IDENT_CURRENT('phs.dbo.TemplateField')
			declare @fieldIDCount int 

			select * into #temp1 from DataCollection where Modality = @modality and Form = @form; 
			select @fieldIDCount = count(1) from #temp1

			
INSERT into phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge],[MaximumAge],[HelpText],[DateAdded],[MaxFilesizeInKb],[ValidFileExtensions],[MinFilesizeInKb],[ImageBase64],[MatrixRow],[MatrixColumn],[PreRegistrationFieldName],[RegistrationFieldName],[SummaryFieldName],[AddOthersOption],OthersPlaceHolder,SummaryType, ConditionTemplateFieldID,ConditionCriteria,ConditionOptions,StandardReferenceID) 
select [Label Text], [Label Text], [Type],Mandatory, 50, '','','','','',0,20,20,
substring(case when [value 1] is null then '' else [value 1] end +
case when [value 2] is null then '' else '|' + [value 2] end +  
case when [value 3] is null then '' else '|' + [value 3] end +
case when [value 4] is null then '' else '|' + [value 4] end +
case when [value 5] is null then '' else '|' + [value 5] end +
case when [value 6] is null then '' else '|' + [value 6] end +
case when [value 7] is null then '' else '|' + [value 7] end +
case when [value 8] is null then '' else '|' + [value 8] end +
case when [value 9] is null then '' else '|' + [value 9] end +
case when [value 10] is null then '' else '|' + [value 10] end +
case when [value 11] is null then '' else '|' + [value 11] end, 0 , 1999),
'', ROW_NUMBER() over (order by id),ROW_NUMBER() over (order by id),18,100,'',getdate(),5000, '.jpg,.png,.gif,.pdf,.bmp,.zip', 10, '', '', '', PreRegistrationFieldName, RegistrationFieldName, [Summary Field], [AddOthersOption],OtherOptionsLabel, SummaryType, iif(ConditionTemplateFIeldID is not null, ConditionTemplateFIeldID, null), iif([Condition Criteria] is not null, [Condition Criteria], null), iif(ConditionOptions is not null, ConditionOptions, null),StdReference  
from #temp1


			insert phs.dbo.ModalityForm values (@modalityID, @formID) 

			while @fieldIDCount > 0 
			begin 
				set @templateFieldID = @LASTfieldID + @fieldIDCount				
				insert phs.dbo.TemplateTemplateField values (@templateID, @templateFieldID)

				if exists (select templatefieldid from phs.dbo.TemplateField where TemplateFieldID = @templateFieldID and ConditionTemplateFieldID is not null )
				begin
					select @parentStagingConditionalFieldIDTemp = ConditionTemplateFieldID from phs.dbo.TemplateField where TemplateFieldID = @templateFieldID and ConditionTemplateFieldID is not null 			
					select @parentConditionalFieldIDTemp = TemplateFieldID from phs.dbo.TemplateField where Label = (select [Label Text] from DataCollection where ID = @parentStagingConditionalFieldIDTemp)
					update phs.dbo.TemplateField set ConditionTemplateFieldID = @parentConditionalFieldIDTemp where TemplateFieldID = @templateFieldID
				end 

				set @fieldIDCount = @fieldIDCount - 1; 
			end 



			drop table #temp1 

			

		fetch NEXT from formList into @form 
		end 
		close formList
		deallocate formList 
	fetch NEXT from modalityList into @modality 
end 



close modalityList
deallocate modalityList 

update phs.dbo.Form set InternalFormType = 'REG' where Title = '0 - Registration Form' 
update phs.dbo.Form set PublicFormType = 'PRE-REGISTRATION', IsPublic = 1, Slug = 'phs2017' where DateAdded > (GETDATE() - 1) and title = 'Pre-Registration Form'
update phs.dbo.Form set InternalFormType = 'PHLEBOTOMY' where Title = 'Phlebotomy Form'


INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('Summary Form', null, 0, null, 'SUM', GETDATE(), 1)
declare @FormSum as int
select @FormSum = IDENT_CURRENT('phs.dbo.Form')

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('Doctor Summary Form', null, 0, null, 'DSY', GETDATE(), 1)
declare @FormDocSum as int
select @FormDocSum = IDENT_CURRENT('phs.dbo.Form')

INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@FormDocSum, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('03 - Cognitive 2nd Tier Summary Form', null, 0, null, 'COG2', GETDATE(), 1)
declare @FormCog2Sum as int
select @FormCog2Sum = IDENT_CURRENT('phs.dbo.Form')

INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@FormCog2Sum, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('11 - Summary for PT Consult', null, 0, null, 'PTSUM', GETDATE(), 1)
declare @FormPTSum as int
select @FormPTSum = IDENT_CURRENT('phs.dbo.Form')

INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@FormPTSum, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('13 - Summary for OT Consult', null, 0, null, 'OTSUM', GETDATE(), 1)
declare @FormOTSum as int
select @FormOTSum = IDENT_CURRENT('phs.dbo.Form')

INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@FormOTSum, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('Exhibition Summary', null, 0, null, 'EXHIBITION', GETDATE(), 1)
declare @FormExhibition as int
select @FormExhibition = IDENT_CURRENT('phs.dbo.Form')

INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@FormExhibition, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('Social Support', null, 0, null, 'SOCIALSUP', GETDATE(), 1)
declare @FormSocialSup as int
select @FormSocialSup = IDENT_CURRENT('phs.dbo.Form')

INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@FormSocialSup, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

-- INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID]) VALUES (@ModSummary, @FormSum)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID])  VALUES (@ModGeri, @FormCog2Sum)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID]) VALUES (@ModGeri, @FormPTSum)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID]) VALUES (@ModGeri, @FormOTSum)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID])  VALUES (@ModSocialSupport, @FormSocialSup)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID]) VALUES (@ModDoc, @FormDocSum)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID])  VALUES (@ModExhibit, @FormExhibition)


--INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID]) VALUES (@ModDoc, 9) 

use phs
ALTER TABLE [dbo].[TemplateField]  WITH CHECK ADD  CONSTRAINT [FK_template_field_template_field] FOREIGN KEY([ConditionTemplateFieldId])
REFERENCES [dbo].[TemplateField] ([TemplateFieldId])

ALTER TABLE [dbo].[TemplateTemplateField]  WITH CHECK ADD  CONSTRAINT [FK template_fields_template_template_fields] FOREIGN KEY([TemplateFieldId])
REFERENCES [dbo].[TemplateField] ([TemplateFieldID])



ON DELETE CASCADE



update TemplateField set label = REPLACE(label, char(10),'<br/>') where label LIKE '%' + CHAR(10) + '%' OR label LIKE '%' + CHAR(13) + '%'
update TemplateField set text = REPLACE(text, char(10),'<br/>') where text LIKE '%' + CHAR(10) + '%' OR text LIKE '%' + CHAR(13) + '%'

update TemplateField  set ConditionTemplateFieldID = null where ConditionCriteria is null
update TemplateField  set IsRequired = 0 where ConditionTemplateFieldID is not null 

SET IDENTITY_INSERT [dbo].[SummaryMapping] ON 

GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (1, N'EVENT_SUM', N'Cardiovascular Health', N'CURRENTLY_SMOKE')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (2, N'EVENT_SUM', N'Cardiovascular Health', N'FAMILY_HISTORY')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (3, N'EVENT_SUM', N'Cardiovascular Health', N'PAST_MEDICAL_HISTORY')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (4, N'EVENT_SUM', N'Obesity, Metabolic Syndrome & Diabetes', N'PAST_MEDICAL_HISTORY')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (5, N'EVENT_SUM', N'Obesity, Metabolic Syndrome & Diabetes', N'FAMILY_HISTORY')
GO


INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (6, N'DOCT_SUM', N'Reason for Referral', N'HxTakingField6')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (7, N'DOCT_SUM', N'Reason for Referral', N'HxTakingField7')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (8, N'DOCT_SUM', N'Reason for Referral', N'HxTakingField8')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (9, N'DOCT_SUM', N'Blood Pressure', N'HxTakingField0')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (10, N'DOCT_SUM', N'Blood Pressure', N'HxTakingField1')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (11, N'DOCT_SUM', N'BMI', N'HxTakingField5')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (12, N'DOCT_SUM', N'Phleblotomy', N'PhelbField0')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (13, N'DOCT_SUM', N'Phleblotomy', N'PhelbField1')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (14, N'DOCT_SUM', N'Medical History', N'HxTakingField3')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (15, N'DOCT_SUM', N'Medical History', N'HxTakingField12')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (16, N'DOCT_SUM', N'Medical History', N'HxTakingField8')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (17, N'DOCT_SUM', N'Medical History', N'HxTakingField4')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (18, N'DOCT_SUM', N'Medical History', N'HxTakingField9')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (19, N'DOCT_SUM', N'Medical History', N'HxTakingField13')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (20, N'DOCT_SUM', N'Social History', N'HxTakingField2')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (21, N'DOCT_SUM', N'Social History', N'HxTakingField16')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (22, N'DOCT_SUM', N'Social History', N'HxTakingField17')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (23, N'DOCT_SUM', N'Social History', N'HxTakingField14')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (24, N'DOCT_SUM', N'Social History', N'HxTakingField15')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (25, N'DOCT_SUM', N'Social History', N'HxTakingField6')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (26, N'DOCT_SUM', N'Social History', N'HxTakingField18')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (27, N'DOCT_SUM', N'Social History', N'HxTakingField19')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (28, N'DOCT_SUM', N'Urinary Incontinence', N'HxTakingField7')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (29, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField0')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (30, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField1')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (31, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField2')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (32, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField3')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (33, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField4')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (34, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField5')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (35, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField6')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (36, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField7')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (37, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField8')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (38, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField9')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (39, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField10')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (40, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField11')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (41, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField12')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (42, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField13')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (43, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField14')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (44, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField15')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (45, N'DOCT_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField16')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (46, N'DOCT_SUM', N'Physical Therapist Memo', N'GeriField109')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (47, N'DOCT_SUM', N'Occupational Therapist Memo', N'GeriField115')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (48, N'DOCT_SUM', N'Vision', N'GeriField33')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (49, N'DOCT_SUM', N'Vision', N'GeriField34')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (50, N'DOCT_SUM', N'Vision', N'GeriField35')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (51, N'DOCT_SUM', N'Vision', N'GeriField36')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (52, N'DOCT_SUM', N'Vision', N'GeriField37')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (53, N'DOCT_SUM', N'Vision', N'GeriField38')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (54, N'DOCT_SUM', N'Vision', N'GeriField39')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (55, N'DOCT_SUM', N'Vision', N'GeriField40')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (56, N'DOCT_SUM', N'Vision', N'GeriField41')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (57, N'DOCT_SUM', N'Vision', N'GeriField42')
GO


INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (58, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField0')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (59, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField1')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (60, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField2')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (61, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField3')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (62, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField4')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (63, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField5')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (64, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField6')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (65, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField7')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (66, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField8')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (67, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField9')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (68, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField10')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (69, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField11')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (70, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField12')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (71, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField13')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (72, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField14')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (73, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField15')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (74, N'COG2_SUM', N'Cognitive 2nd Tier - AMT', N'GeriField16')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (75, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField17')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (76, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField18')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (77, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField19')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (78, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField20')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (79, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField21')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (80, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField22')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (81, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField23')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (82, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField24')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (83, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField25')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (84, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField26')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (85, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField27')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (86, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField28')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (87, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField29')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (88, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField30')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (89, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField31')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (90, N'COG2_SUM', N'Cognitive 2nd Tier - EBAS', N'GeriField32')
GO


INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (91, N'PTCON_SUM', N'Reasons for referral to Dr''s Consult from PT', N'GeriField108')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (92, N'PTCON_SUM', N'Reasons for referral to Dr''s Consult from PT', N'GeriField109')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (93, N'PTCON_SUM', N'Reasons for referral to Dr''s Consult from PT', N'GeriField110')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (94, N'PTCON_SUM', N'Reasons for referral to Dr''s Consult from PT', N'GeriField111')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (95, N'PTCON_SUM', N'PAR-Q Results', N'GeriField43')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (96, N'PTCON_SUM', N'PAR-Q Results', N'GeriField44')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (97, N'PTCON_SUM', N'PAR-Q Results', N'GeriField45')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (98, N'PTCON_SUM', N'PAR-Q Results', N'GeriField46')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (99, N'PTCON_SUM', N'PAR-Q Results', N'GeriField47')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (100, N'PTCON_SUM', N'PAR-Q Results', N'GeriField48')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (101, N'PTCON_SUM', N'PAR-Q Results', N'GeriField49')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (102, N'PTCON_SUM', N'PAR-Q Results', N'GeriField50')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (103, N'PTCON_SUM', N'PAR-Q Results', N'GeriField51')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (104, N'PTCON_SUM', N'Physical Activity Levels Results', N'GeriField52')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (105, N'PTCON_SUM', N'Physical Activity Levels Results', N'GeriField53')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (106, N'PTCON_SUM', N'Physical Activity Levels Results', N'GeriField54')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (107, N'PTCON_SUM', N'Physical Activity Levels Results', N'GeriField55')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (108, N'PTCON_SUM', N'Physical Activity Levels Results', N'GeriField56')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (109, N'PTCON_SUM', N'Physical Activity Levels Results', N'GeriField57')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (110, N'PTCON_SUM', N'Physical Activity Levels Results', N'GeriField58')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (111, N'PTCON_SUM', N'Physical Activity Levels Results', N'GeriField59')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (112, N'PTCON_SUM', N'Frail Scale', N'GeriField60')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (113, N'PTCON_SUM', N'Frail Scale', N'GeriField61')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (114, N'PTCON_SUM', N'Frail Scale', N'GeriField62')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (115, N'PTCON_SUM', N'Frail Scale', N'GeriField63')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (116, N'PTCON_SUM', N'Frail Scale', N'GeriField64')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (117, N'PTCON_SUM', N'Frail Scale', N'GeriField65')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (118, N'PTCON_SUM', N'Frail Scale', N'GeriField66')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (119, N'PTCON_SUM', N'Frail Scale', N'GeriField67')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (120, N'PTCON_SUM', N'Frail Scale', N'GeriField68')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (121, N'PTCON_SUM', N'Frail Scale', N'GeriField69')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (122, N'PTCON_SUM', N'Frail Scale', N'GeriField70')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (123, N'PTCON_SUM', N'Frail Scale', N'GeriField71')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (124, N'PTCON_SUM', N'SPPB scores', N'GeriField85')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (125, N'PTCON_SUM', N'SPPB scores', N'GeriField86')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (126, N'PTCON_SUM', N'SPPB scores', N'GeriField87')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (127, N'PTCON_SUM', N'SPPB scores', N'GeriField88')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (128, N'PTCON_SUM', N'SPPB scores', N'GeriField89')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (129, N'PTCON_SUM', N'SPPB scores', N'GeriField90')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (130, N'PTCON_SUM', N'SPPB scores', N'GeriField91')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (131, N'PTCON_SUM', N'SPPB scores', N'GeriField92')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (132, N'PTCON_SUM', N'SPPB scores', N'GeriField93')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (133, N'PTCON_SUM', N'SPPB scores', N'GeriField94')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (134, N'PTCON_SUM', N'SPPB scores', N'GeriField95')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (135, N'PTCON_SUM', N'SPPB scores', N'GeriField96')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (136, N'PTCON_SUM', N'SPPB scores', N'GeriField97')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (137, N'PTCON_SUM', N'SPPB scores', N'GeriField98')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (138, N'PTCON_SUM', N'SPPB scores', N'GeriField99')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (139, N'PTCON_SUM', N'SPPB scores', N'GeriField100')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (140, N'PTCON_SUM', N'Time-up and go Results', N'GeriField101')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (141, N'PTCON_SUM', N'Time-up and go Results', N'GeriField102')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (142, N'PTCON_SUM', N'Time-up and go Results', N'GeriField103')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (143, N'PTCON_SUM', N'Time-up and go Results', N'GeriField104')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (144, N'PTCON_SUM', N'Time-up and go Results', N'GeriField105')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (145, N'PTCON_SUM', N'Time-up and go Results', N'GeriField106')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (146, N'PTCON_SUM', N'Time-up and go Results', N'GeriField107')
GO


INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (147, N'OTCON_SUM', N'Reasons for referral to Dr''s Consult from OT', N'GeriField114')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (148, N'OTCON_SUM', N'Reasons for referral to Dr''s Consult from OT', N'GeriField115')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (149, N'OTCON_SUM', N'Reasons for referral to Dr''s Consult from OT', N'GeriField116')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (150, N'OTCON_SUM', N'Reasons for referral to Dr''s Consult from OT', N'GeriField117')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (151, N'OTCON_SUM', N'Vision - Snellen''s Test Results', N'GeriField33')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (152, N'OTCON_SUM', N'Vision - Snellen''s Test Results', N'GeriField34')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (153, N'OTCON_SUM', N'Vision - Snellen''s Test Results', N'GeriField35')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (154, N'OTCON_SUM', N'Vision - Snellen''s Test Results', N'GeriField36')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (155, N'OTCON_SUM', N'Vision - Snellen''s Test Results', N'GeriField37')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (156, N'OTCON_SUM', N'Vision - Snellen''s Test Results', N'GeriField38')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (157, N'OTCON_SUM', N'Vision - Snellen''s Test Results', N'GeriField39')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (158, N'OTCON_SUM', N'Vision - Snellen''s Test Results', N'GeriField40')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (159, N'OTCON_SUM', N'Vision - Snellen''s Test Results', N'GeriField41')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (160, N'OTCON_SUM', N'Vision - Snellen''s Test Results', N'GeriField42')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (161, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField71')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (162, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField72')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (163, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField73')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (164, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField74')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (165, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField75')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (166, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField76')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (167, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField77')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (168, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField78')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (169, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField79')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (170, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField80')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (171, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField81')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (172, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField82')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (173, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField83')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (174, N'OTCON_SUM', N'OT Questionnaire Results', N'GeriField84')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (175, N'OTCON_SUM', N'SPPB scores', N'GeriField85')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (176, N'OTCON_SUM', N'SPPB scores', N'GeriField86')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (177, N'OTCON_SUM', N'SPPB scores', N'GeriField87')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (178, N'OTCON_SUM', N'SPPB scores', N'GeriField88')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (179, N'OTCON_SUM', N'SPPB scores', N'GeriField89')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (180, N'OTCON_SUM', N'SPPB scores', N'GeriField90')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (181, N'OTCON_SUM', N'SPPB scores', N'GeriField91')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (182, N'OTCON_SUM', N'SPPB scores', N'GeriField92')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (183, N'OTCON_SUM', N'SPPB scores', N'GeriField93')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (184, N'OTCON_SUM', N'SPPB scores', N'GeriField94')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (185, N'OTCON_SUM', N'SPPB scores', N'GeriField95')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (186, N'OTCON_SUM', N'SPPB scores', N'GeriField96')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (187, N'OTCON_SUM', N'SPPB scores', N'GeriField97')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (188, N'OTCON_SUM', N'SPPB scores', N'GeriField98')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (189, N'OTCON_SUM', N'SPPB scores', N'GeriField99')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (190, N'OTCON_SUM', N'SPPB scores', N'GeriField100')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (191, N'OTCON_SUM', N'Time-up and go Results', N'GeriField101')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (192, N'OTCON_SUM', N'Time-up and go Results', N'GeriField102')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (193, N'OTCON_SUM', N'Time-up and go Results', N'GeriField103')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (194, N'OTCON_SUM', N'Time-up and go Results', N'GeriField104')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (195, N'OTCON_SUM', N'Time-up and go Results', N'GeriField105')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (196, N'OTCON_SUM', N'Time-up and go Results', N'GeriField106')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (197, N'OTCON_SUM', N'Time-up and go Results', N'GeriField107')
GO


INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (198, N'EXHI_SUM', N'Cardiovascular Health', N'HxTakingField0')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (199, N'EXHI_SUM', N'Cardiovascular Health', N'HxTakingField1')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (200, N'EXHI_SUM', N'Cardiovascular Health', N'HxTakingField2')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (201, N'EXHI_SUM', N'Cardiovascular Health', N'HxTakingField3')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (202, N'EXHI_SUM', N'Cardiovascular Health', N'HxTakingField4')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (203, N'EXHI_SUM', N'Obesity, Metabolic Syndrome & Diabetes', N'HxTakingField5')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (204, N'EXHI_SUM', N'Obesity, Metabolic Syndrome & Diabetes', N'HxTakingField3')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (205, N'EXHI_SUM', N'Obesity, Metabolic Syndrome & Diabetes', N'HxTakingField4')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (206, N'EXHI_SUM', N'Lifestyle Choices', N'HxTakingField2')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (207, N'EXHI_SUM', N'Lifestyle Choices', N'HxTakingField6')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (208, N'EXHI_SUM', N'Geriatrics and Mental Health', N'GeriField13')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (209, N'EXHI_SUM', N'Geriatrics and Mental Health', N'GeriField27')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (210, N'EXHI_SUM', N'Geriatrics and Mental Health', N'GeriField36')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (211, N'EXHI_SUM', N'Geriatrics and Mental Health', N'GeriField37')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (212, N'EXHI_SUM', N'Geriatrics and Mental Health', N'RegField0')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (213, N'EXHI_SUM', N'Renal Health', N'HxTakingField7')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (214, N'EXHI_SUM', N'Cancers', N'HxTakingField8')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (215, N'EXHI_SUM', N'Cancers', N'HxTakingField9')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (216, N'EXHI_SUM', N'Social Support', N'HxTakingField10')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (217, N'EXHI_SUM', N'Social Support', N'SocialSupportField0')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (218, N'EXHI_SUM', N'Social Support', N'SocialSupportField1')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (219, N'EXHI_SUM', N'Social Support', N'SocialSupportField2')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (220, N'EXHI_SUM', N'Social Support', N'SocialSupportField3')
GO


INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (221, N'SOCIAL_SUM', N'CHAS Card Status', N'RegField1')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (222, N'SOCIAL_SUM', N'CHAS Card Status', N'RegField2')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (223, N'SOCIAL_SUM', N'CHAS Card Status', N'RegField3')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (224, N'SOCIAL_SUM', N'CHAS Card Status', N'RegField4')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (225, N'SOCIAL_SUM', N'CHAS Card Status', N'RegField5')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (226, N'SOCIAL_SUM', N'CHAS Card Status', N'RegField6')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (227, N'SOCIAL_SUM', N'CHAS Card Status', N'RegField7')
GO
INSERT [dbo].[SummaryMapping] ([SummaryMappingID], [SummaryType], [CategoryName], [SummaryFieldName]) VALUES (228, N'SOCIAL_SUM', N'CHAS Card Status', N'HxTakingField11')
GO

SET IDENTITY_INSERT [dbo].[SummaryMapping] OFF

select * from TemplateField where FieldType = 'IMAGE' and ImageBase64 = ''



