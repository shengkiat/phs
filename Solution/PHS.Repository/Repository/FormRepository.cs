﻿using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Forms;
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
            return dbContext.Set<Form>().Where(u => u.FormID == key).Include(x => x.Templates).FirstOrDefault();
        }

        public Template GetTemplate(int key)
        {
            return dbContext.Set<Template>().Where(u => u.TemplateID == key).Include(x => x.TemplateFields).FirstOrDefault();
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
                    MatrixColumn = fieldView.MatrixColumn
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
                }

              //  this.SaveChanges();
            }

        }

        public Form CreateNewForm()
        {
            string formName = "New Registration Form";
            var form = new Form
            {
                Title = formName,
                DateAdded = DateTime.UtcNow,
                IsActive = true
            };

            //Add(template);

            dbContext.Set<Form>().Add(form);

            // this.SaveChanges();
            return form;
        }


        public Template CreateNewTemplate(int formId)
        {
            string templateName = "New Registration Form";
            var template = new Template
            {
                Title = templateName,
                FormID = formId,
                //Slug = formName.ToSlug(),
                Slug = templateName,
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

        public void UpdateTemplate(TemplateViewModel model, Template template1)
        {
            if (model == null)
            {
                throw new Exception("Invalid update operation. Form view is null.");
            }

            if (template1 == null)
            {
                throw new Exception("Invalid update operation. Form not found.");
            }

            dbContext.Entry(template1).State = EntityState.Modified;

            template1.Status = model.Status.ToString();
            template1.Title = string.IsNullOrEmpty(model.Title) ? "Registration" : model.Title;
            // form.TabOrder = model.TabOrder; // excluding tab order for first launch
            template1.ConfirmationMessage = model.ConfirmationMessage;
            template1.Theme = model.Theme;
            template1.IsPublic = model.IsPublic;
            template1.PublicFormType = model.PublicFormType;
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

        public IEnumerable<TemplateFieldValueViewModel> GetRegistrantsByForm(TemplateViewModel model)
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
            var templateSet = dbContext.Set<Template>().Where(u => u.FormID == formId).ToList();
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


        public Template GetPreRegistrationForm(int year = -1)
        {
            
            var form = this.dbContext.Set<Template>().First(u => u.IsPublic && u.IsActive && u.PublicFormType.Equals("PRE-REGISTRATION"));

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