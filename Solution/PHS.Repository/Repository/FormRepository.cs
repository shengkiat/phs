using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.Repository.Context;
using PHS.Repository.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PHS.Repository.Interface;


namespace PHS.Repository.Repository
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        public FormRepository(DbContext datacontext) : base(datacontext)
        {

        }

        public Form GetForm(int key)
        {
            return dbContext.Set<Form>().Where(u => u.FormID == key && u.IsActive == true).Include(x => x.Templates.Select(t => t.TemplateFields)).FirstOrDefault();
        }

        public Form GetForm(string slug)
        {
            return dbContext.Set<Form>().Where(u => u.IsPublic == true && u.IsActive == true && (slug.Equals(u.Slug))).FirstOrDefault();
        }

        public Template GetTemplate(int templateId)
        {
            return dbContext.Set<Template>().Where(u => u.TemplateID == templateId && u.IsActive == true).Include(x => x.Form).Include(x => x.TemplateFields).FirstOrDefault();
        }

        public Form GetPublicForm(string slug)
        {
            return dbContext.Set<Form>().Where(u => u.IsPublic == true && slug.Equals(u.Slug)).Include(x => x.Templates.Select(t=> t.TemplateFields)).FirstOrDefault();
        }

        public void UpdateTemplateField(Template template1, TemplateFieldViewModel fieldView)
        {
            if (template1 == null)
            {
                throw new Exception("Cannot update a field when a form is null");
            }

            dbContext.Entry(template1).State = EntityState.Modified;

            if (!fieldView.TemplateFieldID.HasValue)
            {
                // create
                var fField = new TemplateField
                {
                    DomId = fieldView.DomId,
                   // Label = fieldView.Label.LimitWithElipses(40),

                    Label = fieldView.Label,
                    Text = fieldView.Text.Trim(),
                    FieldType = fieldView.FieldType.ToString(),
                    IsRequired = fieldView.IsRequired,
                    MaxChars = fieldView.MaxCharacters,
                    HoverText = fieldView.HoverText.Trim(),
                    Hint = fieldView.Hint.Trim(),
                    SubLabel = fieldView.SubLabel.Trim(),
                    Size = fieldView.Size,
                    Columns = fieldView.Columns,
                    Rows = fieldView.Rows,
                    Options = fieldView.Options,
                    SelectedOption = fieldView.SelectedOption,
                    AddOthersOption = fieldView.AddOthersOption,
                    OthersOption = fieldView.OthersOption,
                    HelpText = fieldView.HelpText.Trim(),
                    Validation = fieldView.Validation,
                    Order = fieldView.Order,
                    MinimumAge = fieldView.MinimumAge,
                    MaximumAge = fieldView.MaximumAge,
                    MaxFilesizeInKb = fieldView.MaxFileSize,
                    MinFilesizeInKb = fieldView.MinFileSize,
                    ValidFileExtensions = fieldView.ValidFileExtensions,
                    DateAdded = DateTime.UtcNow,
                    ImageBase64 = fieldView.ImageBase64,
                    MatrixRow = fieldView.MatrixRow,
                    MatrixColumn = fieldView.MatrixColumn,
                    PreRegistrationFieldName = fieldView.PreRegistrationFieldName,
                    RegistrationFieldName = fieldView.RegistrationFieldName,
                    SummaryType = fieldView.SummaryType,
                    ConditionCriteria = fieldView.ConditionCriteria,
                    ConditionOptions = fieldView.ConditionOptions,
                    ConditionTemplateFieldID = fieldView.ConditionTemplateFieldID,
                    StandardReferenceID = fieldView.StandardReferenceID
                };

                template1.TemplateFields.Add(fField);
               // this.SaveChanges();
            }
            else
            {
                var fField = dbContext.Set<TemplateField>().Where(field => field.TemplateFieldID == fieldView.TemplateFieldID.Value).FirstOrDefault();
                if (fField != null)
                {

                  //  fField.Label = fieldView.Label.LimitWithElipses(45);
                    fField.Label = fieldView.Label;
                    fField.Text = fieldView.Text.Trim();
                    fField.FieldType = fieldView.FieldType.ToString();
                    fField.IsRequired = fieldView.IsRequired;
                    fField.MaxChars = fieldView.MaxCharacters;
                    fField.HoverText = fieldView.HoverText.Trim();
                    fField.Hint = fieldView.Hint.Trim();
                    fField.SubLabel = fieldView.SubLabel.Trim();
                    fField.Size = fieldView.Size;
                    fField.Columns = fieldView.Columns;
                    fField.Rows = fieldView.Rows;
                    fField.Options = fieldView.Options;
                    fField.SelectedOption = fieldView.SelectedOption;
                    fField.AddOthersOption = fieldView.AddOthersOption;
                    fField.OthersOption = fieldView.OthersOption;
                    fField.HelpText = fieldView.HelpText.Trim();
                    fField.Validation = fieldView.Validation;
                    fField.Order = fieldView.Order;
                    fField.MinimumAge = fieldView.MinimumAge;
                    fField.MaximumAge = fieldView.MaximumAge;
                    fField.MaxFilesizeInKb = fieldView.MaxFileSize;
                    fField.MinFilesizeInKb = fieldView.MinFileSize;
                    fField.ValidFileExtensions = fieldView.ValidFileExtensions;
                    fField.ImageBase64 = fieldView.ImageBase64;
                    fField.MatrixColumn = fieldView.MatrixColumn;
                    fField.MatrixRow = fieldView.MatrixRow;
                    fField.PreRegistrationFieldName = fieldView.PreRegistrationFieldName;
                    fField.RegistrationFieldName = fieldView.RegistrationFieldName;
                    fField.SummaryType = fieldView.SummaryType;
                    fField.ConditionCriteria = fieldView.ConditionCriteria;
                    fField.ConditionOptions = fieldView.ConditionOptions;
                    fField.ConditionTemplateFieldID = fieldView.ConditionTemplateFieldID;
                    fField.StandardReferenceID = fieldView.StandardReferenceID;
                }

              //  this.SaveChanges();
            }

        }

        public Form CreateNewForm(FormViewModel formViewModel)
        {
            var form = new Form
            {
                Title = formViewModel.Title,
                Slug = formViewModel.Slug,
                IsPublic = formViewModel.IsPublic,
                PublicFormType = formViewModel.PublicFormType,
                InternalFormType = formViewModel.InternalFormType,
                DateAdded = DateTime.UtcNow,
                IsActive = true
            };

            //Add(template);

            dbContext.Set<Form>().Add(form);

            // this.SaveChanges();
            return form;
        }

        public void UpdateForm(FormViewModel formViewModel, Form form1)
        {
            if (form1 == null)
            {
                throw new Exception("Invalid update operation. Template not found.");
            }

            dbContext.Entry(form1).State = EntityState.Modified;
            form1.Title = formViewModel.Title;
            form1.InternalFormType = formViewModel.InternalFormType;
            form1.IsPublic = formViewModel.IsPublic;
            form1.Slug = formViewModel.Slug;
            form1.PublicFormType = form1.PublicFormType;
        }
        public Template CreateNewTemplate(int formId)
        {
            var template = new Template
            {
                FormID = formId,
                Status = Constants.TemplateStatus.DRAFT.ToString(),
                DateAdded = DateTime.UtcNow,
                ConfirmationMessage = "Thank you for signing up",
                IsActive = true,
                Version = 1
            };

            //Add(template);

            dbContext.Set<Template>().Add(template);

           // this.SaveChanges();
            return template;
        }

        public Template CopyTemplate(Template template1)
        {
            var template = new Template
            {
                FormID = template1.FormID,
                IsQuestion = template1.IsQuestion,
                NotificationEmail = template1.NotificationEmail,
                Theme = template1.Theme,
                Status = Constants.TemplateStatus.DRAFT.ToString(),
                DateAdded = DateTime.UtcNow,
                ConfirmationMessage = template1.ConfirmationMessage,
                IsActive = true,
                Version = template1.Version + 1
            };

            dbContext.Set<Template>().Add(template);

            foreach(var templateField1 in template1.TemplateFields)
            {
                var fField = new TemplateField
                {
                    DomId = templateField1.DomId,

                    Label = templateField1.Label,
                    Text = templateField1.Text.Trim(),
                    FieldType = templateField1.FieldType.ToString(),
                    IsRequired = templateField1.IsRequired,
                    MaxChars = templateField1.MaxChars,
                    HoverText = templateField1.HoverText.Trim(),
                    Hint = templateField1.Hint.Trim(),
                    SubLabel = templateField1.SubLabel.Trim(),
                    Size = templateField1.Size,
                    Columns = templateField1.Columns,
                    Rows = templateField1.Rows,
                    Options = templateField1.Options,
                    SelectedOption = templateField1.SelectedOption,
                    AddOthersOption = templateField1.AddOthersOption,
                    OthersOption = templateField1.OthersOption,
                    HelpText = templateField1.HelpText.Trim(),
                    Validation = templateField1.Validation,
                    Order = templateField1.Order,
                    MinimumAge = templateField1.MinimumAge,
                    MaximumAge = templateField1.MaximumAge,
                    MaxFilesizeInKb = templateField1.MaxFilesizeInKb,
                    MinFilesizeInKb = templateField1.MinFilesizeInKb,
                    ValidFileExtensions = templateField1.ValidFileExtensions,
                    DateAdded = DateTime.UtcNow,
                    ImageBase64 = templateField1.ImageBase64,
                    MatrixRow = templateField1.MatrixRow,
                    MatrixColumn = templateField1.MatrixColumn,
                    PreRegistrationFieldName = templateField1.PreRegistrationFieldName,
                    RegistrationFieldName = templateField1.RegistrationFieldName,
                    SummaryType = templateField1.RegistrationFieldName,
                    ConditionCriteria = templateField1.ConditionCriteria,
                    ConditionOptions = templateField1.ConditionOptions,
                    ConditionTemplateFieldID = templateField1.ConditionTemplateFieldID,
                    StandardReferenceID = templateField1.StandardReferenceID
                };

                template.TemplateFields.Add(fField);
            }

            return template;
        }

        public void UpdateTemplate(TemplateViewModel model, Template template1)
        {
            if (model == null)
            {
                throw new Exception("Invalid update operation. Template view is null.");
            }

            if (template1 == null)
            {
                throw new Exception("Invalid update operation. Template not found.");
            }

            dbContext.Entry(template1).State = EntityState.Modified;

            template1.Status = model.Status.ToString();
            // form.TabOrder = model.TabOrder; // excluding tab order for first launch
            template1.ConfirmationMessage = model.ConfirmationMessage;
            template1.Theme = model.Theme;
            template1.IsQuestion = model.IsQuestion;
            template1.NotificationEmail = model.NotificationEmail;
           // this.SaveChanges();
        }

        public void DeleteTemplateField(int id)
        {
            var field = dbContext.Set<TemplateField>().Where(f => f.TemplateFieldID == id).FirstOrDefault();
            if (field != null)
            {
                dbContext.Set<TemplateField>().Remove(field);
             //   this.SaveChanges();
            }
        }

        public IEnumerable<TemplateFieldValueViewModel> GetTemplateFieldValuesByForm(TemplateViewModel model)
        {
            var fieldValues = GetTemplateFieldValuesByTemplate(model.TemplateID.Value);
            var values = fieldValues
                         .Select((fv) =>
                         {
                             return TemplateFieldValueViewModel.CreateFromObject(fv);
                         })
                         .OrderBy(f => f.FieldOrder)
                         .ThenByDescending(f => f.DateAdded);

            return values;
        }

        public List<TemplateFieldValue> GetTemplateFieldValuesByTemplate(int templateId)
        {
            var fieldValues = this.dbContext.Set<TemplateFieldValue>();

            var templateFieldValues = this.dbContext
                             .Set<TemplateField>()
                             .Include("Templates")
                             .Where(field => field.Templates.Any(f => f.TemplateID == templateId))
                             .Join(fieldValues, fields => fields.TemplateFieldID, fieldValue => fieldValue.TemplateFieldID, (FormField, FormFieldValue) => FormFieldValue)
                             .ToList();


            foreach (var entry in templateFieldValues)
            {
                if (entry.TemplateField == null)
                {
                    var s =   this.dbContext.Set<TemplateFieldValue>().Include("TemplateField").Where(x => x.TemplateFieldValueID == entry.TemplateFieldValueID).FirstOrDefault();
                    entry.TemplateField = s.TemplateField;

                }
            }

            return templateFieldValues;


            //return this.DataContext
            //                 .FormFields
            //                 .Include("Forms")
            //                 .Where(field => field.Forms.Any(f => f.ID == formId))
            //                 .Join(fieldValues, fields => fields.ID, fieldValue => fieldValue.FieldId, (FormField, FormFieldValue) => FormFieldValue)
            //                 .ToList();
        }

        public void InsertTemplateFieldValue(TemplateFieldViewModel field, string value, Guid entryId, string userId = "")
        {
            if (field.FieldType != Constants.TemplateFieldType.HEADER)
            {
                var fieldVal = new TemplateFieldValue
                {
                    TemplateFieldID = field.TemplateFieldID.Value,
                    Value = value,
                    EntryId = entryId,
                    DateAdded = DateTime.UtcNow
                };

                dbContext.Set<TemplateFieldValue>().Add(fieldVal);
              //  this.SaveChanges();
            }
        }

        public TemplateFieldValue GetTemplateFieldValue(TemplateFieldViewModel field, Guid entryId)
        {
            return dbContext.Set<TemplateFieldValue>().Where(u => u.EntryId.Equals(entryId) && u.TemplateFieldID == field.TemplateFieldID).FirstOrDefault();
        }

        public void UpdateTemplateFieldValue(TemplateFieldValue fieldValue, TemplateFieldViewModel field, string value, string userId = "")
        {
            if (field.FieldType != Constants.TemplateFieldType.HEADER)
            {
                dbContext.Entry(fieldValue).State = EntityState.Modified;
                fieldValue.Value = value;
            }
        }

        public void DeleteEntries(IEnumerable<string> selectedEntries)
        {
            var selectedGuids = selectedEntries.Select(se => new Guid(se));
            var entries = this.dbContext.Set<TemplateFieldValue>().Where(fv => selectedGuids.Any(se => fv.EntryId == se));

            foreach (var entry in entries)
            {
                //this.DeleteFileEntry(entry);
                this.dbContext.Set<TemplateFieldValue>().Remove(entry);
            }

            this.SaveChanges();
        }

        //public void DeleteFileEntry(FormFieldValues entry)
        //{
        //    if (entry.FormFields.FieldType.ToUpper().IsTheSameAs(Constants.FieldType.FILEPICKER.ToString()))
        //    {
        //        var fileObj = entry.Value.FromJson<FileValueObject>();

        //        if (fileObj.IsSavedInCloud)
        //        {

        //        }
        //        else
        //        {
        //            if (fileObj != null && !fileObj.FileName.IsNullOrEmpty())
        //            {
        //                if (File.Exists(fileObj.FullFilePath().Replace(@"\\", @"\")))
        //                {
        //                    File.Delete(fileObj.FullFilePath().Replace(@"\\", @"\"));
        //                }
        //            }
        //        }
        //    }
        //}
        public List<FormViewModel> GetForms()
        {
            var formViews = new List<FormViewModel>();
            var formSet = this.dbContext.Set<Form>().ToList();
            foreach (var form in formSet)
            {
                if (form.IsActive)
                {
                    formViews.Add(FormViewModel.CreateBasicFromObject(form));
                }
            }

            return formViews;
        }

        public List<TemplateViewModel> GetTemplates(int formId)
        {
            var templateViews = new List<TemplateViewModel>();
            var templateSet = dbContext.Set<Template>().Where(u => u.FormID == formId).Include(u => u.Form).ToList();
            foreach (var template in templateSet)
            {
                if (template.IsActive)
                {
                    templateViews.Add(TemplateViewModel.CreateBasicFromObject(template));
                }
            }

            return templateViews;
        }

        public List<Template> GetBaseTemplates()
        {
            var templateSet = this.dbContext.Set<Template>().ToList();
            return templateSet;
        }

        public Form GetPreRegistrationForm(int year = -1)
        {
            var form = this.dbContext.Set<Form>().Where(u => u.IsPublic && u.IsActive && u.PublicFormType.Equals(Constants.Public_Form_Type_PreRegistration)).Include(x => x.Templates).FirstOrDefault();
            return form;
        }

        public void DeleteForm(int formId)
        {
            var form = this.GetForm(formId);
            this.DeleteForm(form);
        }

        public void DeleteForm(Form form)
        {
            form.IsActive = false;

            //this.DataContext.Forms.Remove(form1);
            //var fields = form1.FormFields.ToList();

            //foreach (var f in fields)
            //{
            //    this.DataContext.FormFields.Remove(f);
            //}

            //  this.SaveChanges();
        }

        public void DeleteTemplate(int templateId)
        {
            var template = this.GetTemplate(templateId);
            this.DeleteTemplate(template);
        }

        public void DeleteTemplate(Template template1)
        {
            template1.IsActive = false;

            //this.DataContext.Forms.Remove(form1);
            //var fields = form1.FormFields.ToList();

            //foreach (var f in fields)
            //{
            //    this.DataContext.FormFields.Remove(f);
            //}

          //  this.SaveChanges();
        }

        //public FileValueObject GetFileFieldValue(int valueId)
        //{
        //    var valueObject = this.DataContext.FormFieldValues.Where(v => v.ID == valueId).FirstOrDefault();
        //    if (valueObject != null)
        //    {
        //        var value = valueObject.Value.FromJson<FileValueObject>();
        //        return value;
        //    }

        //    return null;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="olderThanInDays"></param>
        /// <returns></returns>
        public int DeleteTemplates(int olderThanInDays)
        {
            int counter = 0;
            this.DeleteSubmissions(olderThanInDays);
            var deleteDate = DateTime.Now.AddDays(-olderThanInDays);
            var Templates = this.dbContext.Set<Template>().Where(f => f.DateAdded < deleteDate).ToList();

            foreach (var f in Templates)
            {
                this.DeleteTemplate(f);
                counter++; ;
            }

            return counter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="olderThanInDays"></param>
        /// <returns></returns>
        public int DeleteSubmissions(int olderThanInDays)
        {
            int counter = 0;
            var deleteDate = DateTime.Now.AddDays(-olderThanInDays);
            var entries = this.dbContext.Set<TemplateFieldValue>().Where(fv => fv.DateAdded < deleteDate).ToList();

            if (entries.Any())
            {
                foreach (var entry in entries)
                {
                    this.dbContext.Set<TemplateFieldValue>().Remove(entry);
                    counter++;
                }

                this.SaveChanges();
            }

            return counter;
        }

        public virtual void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        
    }
}