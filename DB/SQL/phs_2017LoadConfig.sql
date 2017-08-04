use [phs]


declare @eventID as int 
select @eventID = PHSEventID from PHSEvent where Title like '%2017%'



INSERT [dbo].[Modality] ([Name], [Position], [IconPath], [IsActive], [IsVisible], [IsMandatory], [HasParent], [Status], [Eligiblity], [Labels]) 
VALUES (N'Registration', 0, N'../../../Content/images/PHS2017/01_Registration.png', 1, 1, 1, 0, N'Pending', NULL, 2)
declare @ModReg as int
select @ModReg = IDENT_CURRENT('phs.dbo.Modality')

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
VALUES (N'Oral Health', 7, N'../../../Content/images/PHS2017/08_Oral.png', 0, 0, 0, 0, N'Pending', '≥ 40 years old', 0)
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


select * from TemplateField

-------------------------
-- Form: Registration
-------------------------
INSERT [phs].[dbo].[Form] ([Title], [Slug], [IsPublic], [PublicFormType], [InternalFormType], [DateAdded], [IsActive]) VALUES (N'Registration Form', NULL, 0, NULL, N'REG', GETDATE(), 1)
declare @FormReg as int
select @FormReg = IDENT_CURRENT('phs.dbo.Form')

INSERT [phs].[dbo].[Template] ([FormId], [Status], [ConfirmationMessage], [DateAdded], [Theme], [NotificationEmail], [IsActive], [EventID], [IsQuestion], [Version]) VALUES (@FormReg, N'DRAFT', N'Thank you for signing up', GETDATE(), NULL, NULL, 1, NULL, 0, 1)
declare @TemplateReg as int
select @TemplateReg = IDENT_CURRENT('phs.dbo.Template')

declare @TemplateFieldID as int 

INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) 
VALUES (N'Click to edit', N'Phlebotomy Eligibility', N'HEADER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 0, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)

INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'1.Does the participant intend to undergo phlebotomy? 要去抽血吗？', N'Click to edit', N'DROPDOWNLIST', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes,No', N'', 1, 1, 18, 100, N'', CAST(N'2017-03-05 08:20:27.433' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)

INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'2. Check that ALL THREE of the following eligibility criteria are fulfilled. Otherwise, change the answer of the above question to "No".', N'Click to edit', N'CHECKBOX', 1, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Fasted for at least 10 hours 十个小时内没有吃东西或喝饮料,Have not done a blood test in past 1 year 一年内没有做抽血检查,Have not been previously diagnosed with diabetes mellitus AND / OR hyperlipidemia 没有被诊断患有高血压或高血脂', N'', 2, 2, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)


INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'3. Would the participant like to sign up for the Overflow Phlebotomy Service?
Note: Only for Singapore Citizens, charges may apply according to the Screen For Life programme
Check that the following eligibility criteria are fulfilled:
- Have not done a blood test in past 1 year 一年内没有做抽血检查
- Have not been previously diagnosed with diabetes mellitus AND / OR hyperlipidemia 没有被诊断患有高血压或高血脂
- Ineligible for phlebotomy at PHS health screening e.g. did not fast or past phlebotomy service timing', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Yes,No', N'', 3, 3, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)


INSERT phs.[dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) 
VALUES (N'Click to edit', N'Registration', N'HEADER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 4, 18, 100, N'', getdate(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)


INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'4. Name (as in NRIC) 姓名', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 4, 5, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'FULLNAME')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)

INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'5. Gender 性别', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Male 男性,Female 女性', N'', 5, 6, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'GENDER')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)

INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'6. Salutation 称谓', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Mr,Ms,Mrs,Dr', N'', 6, 7, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'SALUTATION')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)

INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'7. NRIC 身份证号码', N'Click to edit', N'NRICPICKER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 8, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'NRIC', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)

INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'8. Date of Birth (dd/mm/yyyy) 生日
Note: Participants less than 40 years old in 2017 are unfortunately not eligible for this health screening', N'Click to edit', N'BIRTHDAYPICKER', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 6, 4, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'DATEOFBIRTH', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)

INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES ( N'10. Citizenship 国籍 
Note: Non-Singaporean Citizens/PRs are unfortunately not eligible for this health
screening', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'Singapore Citizen 新加坡公民, Singapore Permanent Resident (PR) 新加坡永久居民', N'', 7, 5, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'CITIZENSHIP', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)

INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES ( N'Race', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'Chinese,Malay,Indian', N'', 8, 6, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'RACE', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)

INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES ( N'12. Spoken Language 语言', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 1, NULL, 20, 20, N'English,Mandarin,Malay,Tamil,Others', N'', 9, 7, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'LANGUAGE', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)


INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'13. Address 住址', N'Click to edit', N'ADDRESS', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 11, 7, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'ADDRESS')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)

INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (87, N'Housing Type 住宿', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'HDB Rental Flat,1-Room HDB Flat,2-Room HDB Flat,3-Room HDB Flat,4-Room HDB Flat,5-Room HDB Flat,Executive Flats,Condominium / Private Flats,Landed Property', N'', 12, 8, 18, 100, N'', CAST(N'2017-03-05 08:28:12.463' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (88, N'Home Number', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 13, 9, 18, 100, N'', CAST(N'2017-03-05 08:29:42.477' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'HOMENUMBER')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (89, N'Mobile Number', N'Click to edit', N'TEXTBOX', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 14, 10, 18, 100, N'', CAST(N'2017-03-05 08:29:57.467' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'MOBILENUMBER')
GO
INSERT [dbo].[TemplateField] ([TemplateFieldID], [Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (90, N'Highest Education Qualification 教育水平', N'Click to edit', N'RADIOBUTTON', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'No formal qualifications,Primary / PSLE,Secondary / O Levels,ITE / NITEC / Higher NITEC,Pre-U / JC / A Levels,Polytechnic / Diploma,University / Degree,Master''s, PhD and above', N'', 15, 11, 18, 100, N'', CAST(N'2017-03-05 08:30:27.470' AS DateTime), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')



INSERT [dbo].[TemplateField] ([Label], [Text], [FieldType], [IsRequired], [MaxChars], [HoverText], [Hint], [SubLabel], [Size], [SelectedOption], [AddOthersOption], [OthersOption], [Columns], [Rows], [Options], [Validation], [DomId], [Order], [MinimumAge], [MaximumAge], [HelpText], [DateAdded], [MaxFilesizeInKb], [ValidFileExtensions], [MinFilesizeInKb], [ImageBase64], [MatrixRow], [MatrixColumn], [PreRegistrationFieldName], [RegistrationFieldName]) VALUES (N'Signature of Participant', N'Click to edit', N'SIGNATURE', 0, 50, N'', N'', N'', N'', N'option1', 0, NULL, 20, 20, N'option1,option2', N'', 1, 99, 18, 100, N'', GETDATE(), 5000, N'.jpg,.png,.gif,.pdf,.bmp,.zip', 10, N'', N'', N'', N'', N'')
select @TemplateFieldID = IDENT_CURRENT('phs.dbo.TemplateField')
insert phs.dbo.TemplateTemplateField values (@TemplateReg, @TemplateFieldID)


insert phs.dbo.ModalityForm values (@ModReg, @FormReg)
-------------------------
-- END Form: Registration
-------------------------

-------------------------
-- Form: Mega Sorting Station
-------------------------

INSERT [phs].[dbo].[ModalityForm] ([ModalityID], [FormID]) VALUES (@ModReg, 8)

-------------------------
-- END Form: Mega Sorting Station
-------------------------










select * from Form

-- 3
select * from PHSEvent

select * from Modality where ModalityID in (
select ModalityID from EventModality where PHSEventID = 3)






