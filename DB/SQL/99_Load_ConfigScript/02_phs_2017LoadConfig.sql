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
VALUES (N'Phlebotomy', 1, N'../../../Content/images/PHS2017/02_Phlebotomy.png', 0, 0, 0, 0, N'Pending', 'Fasted for >= 10h
Have not done glucose/lipids test in past 1 year
Not diagnosed with HLD/DM', 3)
declare @ModPhlebo as int
select @ModPhlebo = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Hx Taking', 2, N'../../../Content/images/PHS2017/03_HxTaking.png', 1, 1, 1, 0, N'Pending', NULL, 0)
declare @ModHx as int
select @ModHx = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'FIT', 3, N'../../../Content/images/PHS2017/04_FIT.png', 0, 0, 0, 0, N'Pending', '>= 50 years old
Have not done FIT in the past 1 year
Have not done colonoscopy in the past 5 years
Not diagnosed with colorectal cancer', 3)
declare @ModFIT as int
select @ModFIT = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'WCE', 4, N'../../../Content/images/PHS2017/05_WCE.png', 0, 0, 0, 0, N'Pending', 'Female >= 40 years old', 0)
declare @ModWCE as int
select @ModWCE = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Geriatrics', 5, N'../../../Content/images/PHS2017/06_Geri.png', 0, 0, 0, 0, N'Pending', '>= 60 years old', 0)
declare @ModGeri as int
select @ModGeri = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Doctor''s Consult', 6, N'../../../Content/images/PHS2017/07_Doc.png', 0, 0, 0, 0, N'Pending', 'Referral only', 0)
declare @ModDoc as int
select @ModDoc = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Oral Health', 7, N'../../../Content/images/PHS2017/08_Oral.png', 0, 0, 0, 0, N'Pending', '>= 40 years old', 0)
declare @ModOral as int
select @ModOral = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Questionnaire Collection', 8, N'../../../Content/images/PHS2017/09_Question.png', 1, 1, 1, 0, N'Pending', NULL, 0)
declare @ModQuest as int
select @ModQuest = IDENT_CURRENT('phs.dbo.Modality')


INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Exhibition', 9, N'../../../Content/images/PHS2017/10_Exhibit.png', 0, 0, 0, 0, N'Pending', NULL, 0)
declare @ModExhibit as int
select @ModExhibit = IDENT_CURRENT('phs.dbo.Modality')

INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Summary', 10, N'../../../Content/images/PHS2017/11_Tele.png', 1, 1, 1, 0, N'Pending', NULL, 0)
declare @ModSummary as int
select @ModSummary = IDENT_CURRENT('phs.dbo.Modality')

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
INSERT [dbo].[EventModality] ([PHSEventID], [ModalityID]) VALUES (@eventID, @ModSummary)
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

update DataCollection set [Label Text] = '4. Illnesses: For 11 illnesses, participants are asked, “Did a doctor ever tell you that you have [illness]?” 
The illnesses include hypertension, diabetes, cancer (other than a minor skin cancer), chronic lung disease, heart attack, congestive heart failure, angina, asthma, arthritis, stroke, and kidney disease.
The total illnesses (0–11) are recorded as 
0–4 = 0 and 5–11 = 1.' where id = 184

declare @modality varchar(100)
declare @form varchar(100)

declare @formPositionCounter int 
set @formPositionCounter = 0

declare @modalityID int
declare @formID int
declare @templateID int
declare @LASTfieldID int
declare @templateFieldID int 

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

			
INSERT into phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge],[MaximumAge],[HelpText],[DateAdded],[MaxFilesizeInKb],[ValidFileExtensions],[MinFilesizeInKb],[ImageBase64],[MatrixRow],[MatrixColumn],[PreRegistrationFieldName],[RegistrationFieldName],[SummaryFieldName],[AddOthersOption],SummaryType) 
select [Label Text], [Label Text], [Type],Mandatory, 50, '','','','','option1',0,20,20,
substring(case when [value 1] is null then '' else [value 1] end +
case when [value 2] is null then '' else ',' + [value 2] end +  
case when [value 3] is null then '' else ',' + [value 3] end +
case when [value 4] is null then '' else ',' + [value 4] end +
case when [value 5] is null then '' else ',' + [value 5] end +
case when [value 6] is null then '' else ',' + [value 6] end +
case when [value 7] is null then '' else ',' + [value 7] end +
case when [value 8] is null then '' else ',' + [value 8] end +
case when [value 9] is null then '' else ',' + [value 9] end +
case when [value 10] is null then '' else ',' + [value 10] end +
case when [value 11] is null then '' else ',' + [value 11] end, 0 , 1999),
'', ROW_NUMBER() over (order by id),ROW_NUMBER() over (order by id),18,100,'',getdate(),5000, '.jpg,.png,.gif,.pdf,.bmp,.zip', 10, '', '', '', PreRegistrationFieldName, RegistrationFieldName, [Summary Field], [AddOthersOption], SummaryType  from #temp1


			insert phs.dbo.ModalityForm values (@modalityID, @formID) 

			while @fieldIDCount > 0 
			begin 
				set @templateFieldID = @LASTfieldID + @fieldIDCount
				
				insert phs.dbo.TemplateTemplateField values (@templateID, @templateFieldID)
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

update phs.dbo.Form set InternalFormType = 'REG' where Title = 'Registration Form' 
update phs.dbo.Form set PublicFormType = 'PRE-REGISTRATION', IsPublic = 1, Slug = 'phs2017' where DateAdded > (GETDATE() - 1) and title = 'Pre-Registration Form'

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('Summary Form', null, 0, null, 'SUM', GETDATE(), 1)
declare @FormSum as int
select @FormSum = IDENT_CURRENT('phs.dbo.Form')

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('Doctor Summary Form', null, 0, null, 'DSY', GETDATE(), 1)
declare @FormDocSum as int
select @FormDocSum = IDENT_CURRENT('phs.dbo.Form')

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('Cognitive 2nd Tier Summary Form', null, 0, null, 'COG2', GETDATE(), 1)
declare @FormCog2Sum as int
select @FormCog2Sum = IDENT_CURRENT('phs.dbo.Form')

INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@FormCog2Sum, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('Summary for PT Consult', null, 0, null, 'PTSUM', GETDATE(), 1)
declare @FormPTSum as int
select @FormPTSum = IDENT_CURRENT('phs.dbo.Form')

INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@FormPTSum, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

INSERT INTO phs.[dbo].[Form] ([Title],[Slug],[IsPublic],[PublicFormType],[InternalFormType],[DateAdded],[IsActive])
VALUES ('Summary for OT Consult', null, 0, null, 'OTSUM', GETDATE(), 1)
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
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID]) VALUES (@ModSummary, @FormPTSum)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID]) VALUES (@ModSummary, @FormOTSum)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID])  VALUES (@ModSummary, @FormCog2Sum)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID])  VALUES (@ModExhibit, @FormExhibition)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID])  VALUES (@ModSummary, @FormSocialSup)

--INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID]) VALUES (@ModDoc, @FormDocSum)
INSERT INTO phs.[dbo].[ModalityForm] ([ModalityID],[FormID]) VALUES (@ModDoc, 9) 

use phs
ALTER TABLE [dbo].[TemplateTemplateField]  WITH CHECK ADD  CONSTRAINT [FK template_fields_template_template_fields] FOREIGN KEY([TemplateFieldId])
REFERENCES [dbo].[TemplateField] ([TemplateFieldID])
ON DELETE CASCADE

update TemplateField set label = REPLACE(label, char(10),'<br/>') where label LIKE '%' + CHAR(10) + '%' OR label LIKE '%' + CHAR(13) + '%'
update TemplateField set text = REPLACE(text, char(10),'<br/>') where text LIKE '%' + CHAR(10) + '%' OR text LIKE '%' + CHAR(13) + '%'