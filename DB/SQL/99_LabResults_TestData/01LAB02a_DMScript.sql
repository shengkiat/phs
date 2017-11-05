use phsDM_Participant_Lab

;WITH cte AS
(SELECT *, ROW_NUMBER() OVER (PARTITION BY nric ORDER BY [Year of Visit] DESC) AS rn
   FROM Participant)
select * into #temp1 FROM cte WHERE rn = 1

insert into [phs].dbo.Participant (nric, FullName, HomeNumber, MobileNumber,  Language, Gender, Address, PostalCode, Race, Citizenship, Salutation) 
select nric, name, [home number], [handphone number],  CONCAT(english, mandarin, malay, tamil, others), gender,
'Blk: , Street: ' + address + ', Unit: ', [postal code], race, citizenship,
'A' from #temp1

INSERT INTO phs.[dbo].[PHSEvent]([Title],[StartDT],[EndDT],[Venue],[IsActive],[CreatedBy],[CreatedDateTime])
     VALUES ('PHS 2014 - Data Migration', '01-01-2014', '01-01-2014', 'Data Migration', 1, 'T', getdate()) 
--INSERT INTO phs.[dbo].[PHSEvent]([Title],[StartDT],[EndDT],[Venue],[IsActive],[CreatedBy],[CreatedDateTime])
--     VALUES ('PHS 2015 - Data Migration', '01-01-2015', '01-01-2015', 'Data Migration', 1, 'T', getdate()) 
--INSERT INTO phs.[dbo].[PHSEvent]([Title],[StartDT],[EndDT],[Venue],[IsActive],[CreatedBy],[CreatedDateTime])
--     VALUES ('PHS 2016 - Data Migration', '01-01-2016', '01-01-2016', 'Data Migration', 1, 'T', getdate()) 

update phs.dbo.PHSEvent set [Title] = 'PHS 2015 - Data Migration',[StartDT] = '01-01-2015', [EndDT] = '01-01-2015', [Venue] = 'Data Migration', [IsActive] = 1, [CreatedBy] = 'T', [CreatedDateTime] = getdate() where title like '%2015%'

update phs.dbo.PHSEvent set [Title] = 'PHS 2016 - Data Migration',[StartDT] = '01-01-2016', [EndDT] = '01-01-2016', [Venue] = 'Data Migration', [IsActive] = 1, [CreatedBy] = 'T', [CreatedDateTime] = getdate() where title like '%2016%'

delete from phs.dbo.EventModality where PHSEventID = (select PHSEventID from phs.dbo.phsEvent where title = 'PHS 2015 - Data Migration')
delete from phs.dbo.EventModality where PHSEventID = (select PHSEventID from phs.dbo.phsEvent where title = 'PHS 2016 - Data Migration')

	 
declare @yearEventID2014 as int 
declare @yearModalityID2014 as int
declare @yearEventID2015 as int 
declare @yearModalityID2015 as int
declare @yearEventID2016 as int 
declare @yearModalityID2016 as int
declare @formID as int 
declare @templateID as int
declare @templateFieldID as int


declare @year2014 as varchar(10)
set @year2014 = '2014'

declare @year2015 as varchar(10)
set @year2015 = '2015'

declare @year2016 as varchar(10)
set @year2016 = '2016'

-- 2014

select @yearEventID2014 = PHSEventID from phs.dbo.PHSEvent where title = 'PHS 2014 - Data Migration'
INSERT INTO phs.[dbo].[Modality] ([Name],[Position],[IconPath],[IsActive],[IsVisible],[IsMandatory],[HasParent],[Status])
     VALUES ('Data Migration 2014', 1, '~/Content/images/Modality\black.jpg', 1, 1, 1, 0, 'Pending')
select @yearModalityID2014 = modalityID from phs.dbo.Modality where Name = 'Data Migration 2014'
insert into phs.dbo.EventModality values (@yearEventID2014, @yearModalityID2014)

select @yearEventID2015 = PHSEventID from phs.dbo.PHSEvent where title = 'PHS 2015 - Data Migration'
INSERT INTO phs.[dbo].[Modality] ([Name],[Position],[IconPath],[IsActive],[IsVisible],[IsMandatory],[HasParent],[Status])
     VALUES ('Data Migration 2015', 1, '~/Content/images/Modality\black.jpg', 1, 1, 1, 0, 'Pending')
select @yearModalityID2015 = modalityID from phs.dbo.Modality where Name = 'Data Migration 2015'
insert into phs.dbo.EventModality values (@yearEventID2015, @yearModalityID2015)

select @yearEventID2016 = PHSEventID from phs.dbo.PHSEvent where title = 'PHS 2016 - Data Migration'
INSERT INTO phs.[dbo].[Modality] ([Name],[Position],[IconPath],[IsActive],[IsVisible],[IsMandatory],[HasParent],[Status])
     VALUES ('Data Migration 2016', 1, '~/Content/images/Modality\black.jpg', 1, 1, 1, 0, 'Pending')
select @yearModalityID2016 = modalityID from phs.dbo.Modality where Name = 'Data Migration 2016'
insert into phs.dbo.EventModality values (@yearEventID2016, @yearModalityID2016)



INSERT phs.[dbo].[Form] ([Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) 
VALUES ('Socio-Economic Status', NULL, 0, NULL, NULL, getdate(), 1)

select @formID = formid from phs.dbo.form where Title = 'Socio-Economic Status'
INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@formID, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

SELECT @templateID = IDENT_CURRENT('phs.dbo.Template')

INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) 
VALUES (N'Click to edit', N'Socio-Economic Status', N'HEADER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
select @templateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID, @templateFieldID)


-- @formSocialEco_Occu
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Occupation', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 2, 1, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSocialEco_Occu as int
select @formSocialEco_Occu = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID, @formSocialEco_Occu)

-- @formSocialEco_HighestEd
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Highest Education Level', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 3, 2, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSocialEco_HighestEd as int
select @formSocialEco_HighestEd = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID, @formSocialEco_HighestEd)

-- @formSocialEco_Housing
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Housing Type', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 4, 3, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSocialEco_Housing as int
select @formSocialEco_Housing = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID, @formSocialEco_Housing)

-- @formSocialEco_householdIncome
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Monthly Household Income', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 5, 4, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSocialEco_householdIncome as int
select @formSocialEco_householdIncome = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID, @formSocialEco_householdIncome)

-- @formSocialEco_householdMember
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'No. of Household Members', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 6, 5, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSocialEco_householdMember as int
select @formSocialEco_householdMember = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID, @formSocialEco_householdMember)

-- @formSocialEco_percapita
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Per Capita Income', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSocialEco_percapita as int
select @formSocialEco_percapita = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID, @formSocialEco_percapita)

-- @formSocialEco_finassist
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Financial Assistance', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSocialEco_finassist as int
select @formSocialEco_finassist = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID, @formSocialEco_finassist)

--@formSocialEco_chas
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'CHAS', N'Click to edit',N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSocialEco_chas as int
select @formSocialEco_chas = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID, @formSocialEco_chas)

-- @formSocialEco_pg
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'PG', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSocialEco_pg as int
select @formSocialEco_pg = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID, @formSocialEco_pg)




------------------------------------------
-- Personal Medical History Form
------------------------------------------

declare @formID_PersonalMedHist as int
declare @templateID_PersonalMedHist as int
declare @templateFiledID_PersonalMedHist as int

INSERT phs.[dbo].[Form] ([Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) 
VALUES ('Personal Medical History', NULL, 0, NULL, NULL, getdate(), 1)

select @formID_PersonalMedHist = formid from phs.dbo.form where Title = 'Personal Medical History'
INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@formID_PersonalMedHist, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

SELECT @templateID_PersonalMedHist = IDENT_CURRENT('phs.dbo.Template')

INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) 
VALUES (N'Click to edit', N'Personal Medical History', N'HEADER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
select @templateFiledID_PersonalMedHist = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @templateFiledID_PersonalMedHist)


-- @formDiabetesMellitus
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Diabetes Mellitus', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 2, 1, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formDiabetesMellitus as int
select @formDiabetesMellitus = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formDiabetesMellitus)


-- @formHyperLipid
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Hyperlipidaemia', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 3, 2, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formHyperLipid as int
select @formHyperLipid = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formHyperLipid)

-- @formhypertension
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Hypertension', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 4, 3, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formhypertension as int
select @formhypertension = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formhypertension)


-- @formHeartDisease
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Heart Disease', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 5, 4, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formHeartDisease as int
select @formHeartDisease = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formHeartDisease)

-- @formStroke
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Stroke', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 6, 5, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formStroke as int
select @formStroke = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formStroke)

-- @formAsthma
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Asthma', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formAsthma as int
select @formAsthma = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formAsthma)

-- @formCOPD
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'COPD', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formCOPD as int
select @formCOPD = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formCOPD)

--@formothers1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Others', N'Click to edit',N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formothers1 as int
select @formothers1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formothers1)

-- @formElaboration1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Elaboration', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formElaboration1 as int
select @formElaboration1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formElaboration1)

-- @formColorectal
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Colorectal', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formColorectal as int
select @formColorectal = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formColorectal)

-- @formBreast
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Breast', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formBreast as int
select @formBreast = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formBreast)

-- @formCervical
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Cervical', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formCervical as int
select @formCervical = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formCervical)

-- @formOthers2
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Others', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formOthers2 as int
select @formOthers2 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formOthers2)

-- @formElaboration2
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Elaboration', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formElaboration2 as int
select @formElaboration2 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formElaboration2)

-- @formOnRegularFollowUp
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'On Regular Follow Up?', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formOnRegularFollowUp as int
select @formOnRegularFollowUp = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_PersonalMedHist, @formOnRegularFollowUp)

------------------------------------------
-- END Family Medical History Form
------------------------------------------

------------------------------------------
-- Family Medical History Form
------------------------------------------

declare @formID_FamMedHist as int
declare @templateID_FamMedHist as int
declare @templateFiledID_FamMedHist as int

INSERT phs.[dbo].[Form] ([Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) 
VALUES ('Family Medical History', NULL, 0, NULL, NULL, getdate(), 1)

select @formID_FamMedHist = formid from phs.dbo.form where Title = 'Family Medical History'
INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@formID_FamMedHist, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

SELECT @templateID_FamMedHist = IDENT_CURRENT('phs.dbo.Template')

INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) 
VALUES (N'Click to edit', N'Family Medical History', N'HEADER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
select @templateFiledID_FamMedHist = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @templateFiledID_FamMedHist)


-- @formDiabetesMellitus1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Diabetes Mellitus', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 2, 1, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formDiabetesMellitus1 as int
select @formDiabetesMellitus1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formDiabetesMellitus1)

-- @formHyperLipid1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Hyperlipidaemia', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 3, 2, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formHyperLipid1 as int
select @formHyperLipid1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formHyperLipid1)


-- @formhypertension1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Hypertension', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 4, 3, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formhypertension1 as int
select @formhypertension1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formhypertension1)

-- @formHeartDisease1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Heart Disease', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 5, 4, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formHeartDisease1 as int
select @formHeartDisease1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formHeartDisease1)


-- @formStroke
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Stroke', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 6, 5, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formStroke1 as int
select @formStroke1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formStroke1)


-- @formAsthma1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Asthma', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formAsthma1 as int
select @formAsthma1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formAsthma1)


-- @formCOPD1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'COPD', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formCOPD1 as int
select @formCOPD1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formCOPD1)

--@formothers3
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Others', N'Click to edit',N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formothers3 as int
select @formothers3 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formothers3)

-- @Example
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Example', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @Example as int
select @Example = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @Example)

-- @formColorectal1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Colorectal', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formColorectal1 as int
select @formColorectal1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formColorectal1)


-- @formBreast1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Breast', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formBreast1 as int
select @formBreast1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formBreast1)

-- @formCervical1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Cervical', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formCervical1 as int
select @formCervical1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formCervical1)


-- @formOthers4
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Others', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formOthers4 as int
select @formOthers4 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formOthers4)

-- @formExample1
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Elaboration', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formExample1 as int
select @formExample1 = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_FamMedHist, @formExample1)

------------------------------------------
-- END Family Medical History Form
------------------------------------------


------------------------------------------
-- On-Site Assessment Form
------------------------------------------

declare @formID_OnSite as int
declare @templateID_OnSite as int
declare @templateFiledID_OnSite as int

INSERT phs.[dbo].[Form] ([Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) 
VALUES ('On-Site Assessment', NULL, 0, NULL, NULL, getdate(), 1)

select @formID_OnSite = formid from phs.dbo.form where Title = 'On-Site Assessment'
INSERT phs.[dbo].[Template] ([FormID], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) 
VALUES (@formID_OnSite, N'DRAFT', N'Thank you for signing up', getdate(), NULL, NULL, 1, NULL, 0, 1)

SELECT @templateID_OnSite = IDENT_CURRENT('phs.dbo.Template')

INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) 
VALUES (N'Click to edit', N'On-Site Assessment', N'HEADER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
select @templateFiledID_OnSite = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @templateFiledID_OnSite)


-- @formICIQ
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'ICIQ Score', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 2, 1, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formICIQ as int
select @formICIQ = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @formICIQ)

-- @formWeight
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Weight', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 3, 2, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formWeight as int
select @formWeight = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @formWeight)


-- @formHeight
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Height', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 4, 3, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formHeight as int
select @formHeight = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @formHeight)


-- @formBMI
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'BMI', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 5, 4, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formBMI as int
select @formBMI = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @formBMI)

-- @formSys
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Systolic BP', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 6, 5, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSys as int
select @formSys = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @formSys)

-- @formDias
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Diastolic BP', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formDias as int
select @formDias = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @formDias)

-- @formPhle
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Phlebotomy?', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formPhle as int
select @formPhle = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @formPhle)

--@formSmoker
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Smoker', N'Click to edit',N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formSmoker as int
select @formSmoker = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @formSmoker)

-- @ExSmoker
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Ex-Smoker?', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @ExSmoker as int
select @ExSmoker = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @ExSmoker)


-- @formAlcohol
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Alcohol History', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formAlcohol as int
select @formAlcohol = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @formAlcohol)

-- @formExercise
INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Exercise Frequency per Week', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 7, 6, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
declare @formExercise as int
select @formExercise = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@templateID_OnSite, @formExercise)

------------------------------------------
-- END On-Site Assessment Form
------------------------------------------


-- modalityform
insert phs.dbo.ModalityForm values (@yearModalityID2014, @formID)
insert phs.dbo.ModalityForm values (@yearModalityID2015, @formID)
insert phs.dbo.ModalityForm values (@yearModalityID2016, @formID)





-----------------------
-- Participant data
-----------------------



insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_Occu,  cast(cast((ptnew.ParticipantID * @templateID * @year2014) as varbinary(32)) as uniqueidentifier)
, isnull(Occupation, ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_Occu,  cast(cast((ptnew.ParticipantID * @templateID * @year2015) as varbinary(32)) as uniqueidentifier)
, isnull(Occupation, ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_Occu,  cast(cast((ptnew.ParticipantID * @templateID * @year2016) as varbinary(32)) as uniqueidentifier)
, isnull(Occupation, ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016



insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_HighestEd,  cast(cast((ptnew.ParticipantID * @templateID * @year2014) as varbinary(32)) as uniqueidentifier)
, isnull([Highest Education Level], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_HighestEd,  cast(cast((ptnew.ParticipantID * @templateID * @year2015) as varbinary(32)) as uniqueidentifier)
, isnull([Highest Education Level], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_HighestEd,  cast(cast((ptnew.ParticipantID * @templateID * @year2016) as varbinary(32)) as uniqueidentifier)
, isnull([Highest Education Level], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016


insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_Housing,  cast(cast((ptnew.ParticipantID * @templateID * @year2014) as varbinary(32)) as uniqueidentifier)
, isnull([Housing Type], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_Housing,  cast(cast((ptnew.ParticipantID * @templateID * @year2015) as varbinary(32)) as uniqueidentifier)
, isnull([Housing Type], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_Housing,  cast(cast((ptnew.ParticipantID * @templateID * @year2016) as varbinary(32)) as uniqueidentifier)
, isnull([Housing Type], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016


insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_householdIncome,  cast(cast((ptnew.ParticipantID * @templateID * @year2014) as varbinary(32)) as uniqueidentifier)
, isnull([Monthly Household Income], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_householdIncome,  cast(cast((ptnew.ParticipantID * @templateID * @year2015) as varbinary(32)) as uniqueidentifier)
, isnull([Monthly Household Income], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_householdIncome,  cast(cast((ptnew.ParticipantID * @templateID * @year2016) as varbinary(32)) as uniqueidentifier)
, isnull([Monthly Household Income], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016


insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_householdMember,  cast(cast((ptnew.ParticipantID * @templateID * @year2014) as varbinary(32)) as uniqueidentifier)
, isnull([No# of Household Members], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_householdMember,  cast(cast((ptnew.ParticipantID * @templateID * @year2015) as varbinary(32)) as uniqueidentifier)
, isnull([No# of Household Members], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_householdMember,  cast(cast((ptnew.ParticipantID * @templateID * @year2016) as varbinary(32)) as uniqueidentifier)
, isnull([No# of Household Members], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016



insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_percapita,  cast(cast((ptnew.ParticipantID * @templateID * @year2014) as varbinary(32)) as uniqueidentifier)
, isnull([Per Capita Income], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_percapita,  cast(cast((ptnew.ParticipantID * @templateID * @year2015) as varbinary(32)) as uniqueidentifier)
, isnull([Per Capita Income], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_percapita,  cast(cast((ptnew.ParticipantID * @templateID * @year2016) as varbinary(32)) as uniqueidentifier)
, isnull([Per Capita Income], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016


insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_finassist,  cast(cast((ptnew.ParticipantID * @templateID * @year2014) as varbinary(32)) as uniqueidentifier)
, isnull([Financial Assistance], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_finassist,  cast(cast((ptnew.ParticipantID * @templateID * @year2015) as varbinary(32)) as uniqueidentifier)
, isnull([Financial Assistance], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_finassist,  cast(cast((ptnew.ParticipantID * @templateID * @year2016) as varbinary(32)) as uniqueidentifier)
, isnull([Financial Assistance], ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016


insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_chas,  cast(cast((ptnew.ParticipantID * @templateID * @year2014) as varbinary(32)) as uniqueidentifier)
, isnull(CHAS, ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_chas,  cast(cast((ptnew.ParticipantID * @templateID * @year2015) as varbinary(32)) as uniqueidentifier)
, isnull(CHAS, ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_chas,  cast(cast((ptnew.ParticipantID * @templateID * @year2016) as varbinary(32)) as uniqueidentifier)
, isnull(CHAS, ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016


insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_pg,  cast(cast((ptnew.ParticipantID * @templateID * @year2014) as varbinary(32)) as uniqueidentifier)
, isnull(PG, ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_pg,  cast(cast((ptnew.ParticipantID * @templateID * @year2015) as varbinary(32)) as uniqueidentifier)
, isnull(PG, ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015

insert into phs.dbo.TemplateFieldValue ([templatefieldid], [EntryId], [value], [DateAdded])  
select @formSocialEco_pg,  cast(cast((ptnew.ParticipantID * @templateID * @year2016) as varbinary(32)) as uniqueidentifier)
, isnull(PG, ''), GETDATE()  from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016

-- participantjourneymodality
insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
select ptnew.ParticipantID, @yearEventID2014, @yearModalityID2014, @formID, @templateID,  cast(cast((ptnew.ParticipantID * @templateID * @year2014) as varbinary(32)) as uniqueidentifier)
from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014
insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
select ptnew.ParticipantID, @yearEventID2015, @yearModalityID2015, @formID, @templateID,  cast(cast((ptnew.ParticipantID * @templateID * @year2015) as varbinary(32)) as uniqueidentifier)
from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015
insert into phs.dbo.ParticipantJourneyModality (ParticipantID, PHSEventID, ModalityID, FormID, TemplateID, EntryId) 
select ptnew.ParticipantID, @yearEventID2016, @yearModalityID2016, @formID, @templateID,  cast(cast((ptnew.ParticipantID * @templateID * @year2016) as varbinary(32)) as uniqueidentifier)
from Participant pt left join phs.dbo.Participant ptNEW on 
pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016

-- participant to event 
insert into phs.dbo.ParticipantPHSEvent 
select ptnew.ParticipantID, @yearEventID2014 from Participant as pt left join phs.dbo.Participant as ptNEW on pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2014

insert into phs.dbo.ParticipantPHSEvent 
select ptnew.ParticipantID, @yearEventID2015 from Participant as pt left join phs.dbo.Participant as ptNEW on pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2015

insert into phs.dbo.ParticipantPHSEvent 
select ptnew.ParticipantID, @yearEventID2016 from Participant as pt left join phs.dbo.Participant as ptNEW on pt.NRIC = ptNEW.Nric where pt.[Year of Visit] = @year2016


drop table #temp1
