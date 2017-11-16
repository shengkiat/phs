use [phs]

declare @eventID as int 
select @eventID = PHSEventID from PHSEvent where Title like '%2017%'


ALTER TABLE phs.dbo.templatefield
DROP CONSTRAINT [FK_template_field_template_field]

ALTER TABLE phs.dbo.Templatetemplatefield
DROP CONSTRAINT [FK template_fields_template_template_fields]

declare @ModTele as int
select @ModTele = modalityid from Modality where Name = 'Telehealth'


use phsFUDataLoading
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
update DataCollection set [Type ] = 'TEXTBOXNUMBER' where [Type ] = 'Number'
update DataCollection set [Type ] = 'ADDRESS' where [Type ] = 'Address'
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
select distinct(modality) from DataCollection order by id asc

open modalityList 
fetch NEXT from modalityList into @modality 

while @@FETCH_STATUS = 0
begin 
 
	
		declare formList cursor for 
		select distinct(form) from DataCollection where Modality = @modality order by id asc
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

			select * into #temp1 from DataCollection where Modality = @modality and Form = @form order by id asc; 
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


use phs
ALTER TABLE [dbo].[TemplateField]  WITH CHECK ADD  CONSTRAINT [FK_template_field_template_field] FOREIGN KEY([ConditionTemplateFieldId])
REFERENCES [dbo].[TemplateField] ([TemplateFieldId])

ALTER TABLE [dbo].[TemplateTemplateField]  WITH CHECK ADD  CONSTRAINT [FK template_fields_template_template_fields] FOREIGN KEY([TemplateFieldId])
REFERENCES [dbo].[TemplateField] ([TemplateFieldID])



ON DELETE CASCADE

