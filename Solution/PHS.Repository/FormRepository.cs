﻿using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Forms;
using PHS.Repository.Context;
using PHS.Repository.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace PHS.Repository.Repository
{
    public class FormRepository : BaseRespository<Template, int>
    {    
        public Template GetTemplate(int key)
        {
            this.DataContext = new PHSContext();
            return this.DataContext.Templates.Where(u => u.TemplateID == key).Include(x => x.TemplateFields).FirstOrDefault();
        }

        public FormRepository(PHSContext datacontext)
            : base(datacontext)
        {

        }

        public FormRepository()
            : this(new PHSContext())
        {

        }

        public override DbSet<Template> EntitySet
        {
            get { return this.DataContext.Templates; }
        }

        protected override Template ConvertToNativeEntity(Template entity)
        {
            return entity;
        }

        protected override int SelectPrimaryKey(Template entity)
        {
            return entity.TemplateID;
        }

        public override Template GetByPrimaryKey(int key)
        {
            return this.GetByPrimaryKey(s => s.TemplateID == key);
        }

        public void UpdateField(Template template1, TemplateFieldViewModel fieldView)
        {
            if (template1 == null)
            {
                throw new Exception("Cannot update a field when a form is null");
            }

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
                this.SaveChanges();
            }
            else
            {
                var fField = this.DataContext.TemplateFields.Where(field => field.TemplateFieldID == fieldView.TemplateFieldID.Value).FirstOrDefault();
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

                this.SaveChanges();
            }

        }

        public Template CreateNew()
        {
            string templateName = "New Registration Form";
            var template = new Template
            {
                Title = templateName,
                //Slug = formName.ToSlug(),
                Slug = templateName,
                Status = Constants.TemplateStatus.DRAFT.ToString(),
                DateAdded = DateTime.UtcNow,
                ConfirmationMessage = "Thank you for signing up",
                IsActive = true
            };

            this.DataContext.Templates.Add(template);
            this.SaveChanges();
            return template;
        }

        public void Update(TemplateViewModel model, Template template1)
        {
            if (model == null)
            {
                throw new Exception("Invalid update operation. Form view is null.");
            }

            if (template1 == null)
            {
                throw new Exception("Invalid update operation. Form not found.");
            }

            template1.Status = model.Status.ToString();
            template1.Title = string.IsNullOrEmpty(model.Title) ? "Registration" : model.Title;
            // form.TabOrder = model.TabOrder; // excluding tab order for first launch
            template1.ConfirmationMessage = model.ConfirmationMessage;
            template1.Theme = model.Theme;
            template1.IsPublic = model.IsPublic;
            template1.PublicFormType = model.PublicFormType;
            template1.IsQuestion = model.IsQuestion;
            template1.NotificationEmail = model.NotificationEmail;
            this.SaveChanges();
        }

        public void DeleteField(int id)
        {
            var field = this.DataContext.TemplateFields.Where(f => f.TemplateFieldID == id).FirstOrDefault();
            if (field != null)
            {
                this.DataContext.TemplateFields.Remove(field);
                this.SaveChanges();
            }
        }

        public IEnumerable<TemplateFieldValueViewModel> GetRegistrantsByForm(TemplateViewModel model)
        {
            var fieldValues = this.GetRegistrantsByForm(model.TemplateID.Value);
            var values = fieldValues
                         .Select((fv) =>
                         {
                             return TemplateFieldValueViewModel.CreateFromObject(fv);
                         })
                         .OrderBy(f => f.FieldOrder)
                         .ThenByDescending(f => f.DateAdded);

            return values;
        }

        public List<TemplateFieldValue> GetRegistrantsByForm(int templateId)
        {
            var fieldValues = this.DataContext.TemplateFieldValues;

            var templateFieldValues = this.DataContext
                             .TemplateFields
                             .Include("Templates")
                             .Where(field => field.Templates.Any(f => f.TemplateID == templateId))
                             .Join(fieldValues, fields => fields.TemplateFieldID, fieldValue => fieldValue.TemplateFieldID, (FormField, FormFieldValue) => FormFieldValue)
                             .ToList();


            foreach (var entry in templateFieldValues)
            {
                if (entry.TemplateField == null)
                {
                    var s =   this.DataContext.TemplateFieldValues.Include("TemplateField").Where(x => x.TemplateFieldValueID == entry.TemplateFieldValueID).FirstOrDefault();
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

        public void InsertFieldValue(TemplateFieldViewModel field, string value, Guid entryId, string userId = "")
        {
            if (field.FieldType != Constants.FieldType.HEADER)
            {
                var fieldVal = new TemplateFieldValue
                {
                    TemplateFieldID = field.TemplateFieldID.Value,
                    Value = value,
                    EntryId = entryId,
                    DateAdded = DateTime.UtcNow
                };

                this.DataContext.TemplateFieldValues.Add(fieldVal);
                this.SaveChanges();
            }
        }

        public void DeleteEntries(IEnumerable<string> selectedEntries)
        {
            var selectedGuids = selectedEntries.Select(se => new Guid(se));
            var entries = this.DataContext.TemplateFieldValues.Where(fv => selectedGuids.Any(se => fv.EntryId == se));

            foreach (var entry in entries)
            {
                //this.DeleteFileEntry(entry);
                this.DataContext.TemplateFieldValues.Remove(entry);
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

        public List<TemplateViewModel> GetTemplates()
        {
            var templateViews = new List<TemplateViewModel>();
            var templateSet = this.DataContext.Templates.ToList();
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
            var templateSet = this.DataContext.Templates.ToList();
           

            return templateSet;
        }


        public Template GetPreRegistrationForm(int year = -1)
        {
            
            var form = this.DataContext.Templates.First(u => u.IsPublic && u.IsActive && u.PublicFormType.Equals("PRE-REGISTRATION"));

            return form;
        }

        public void DeleteTemplate(int templateId)
        {
            var template = this.GetByPrimaryKey(templateId);
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

            this.SaveChanges();
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
            var Templates = this.DataContext.Templates.Where(f => f.DateAdded < deleteDate).ToList();

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
            var entries = this.DataContext.TemplateFieldValues.Where(fv => fv.DateAdded < deleteDate).ToList();

            if (entries.Any())
            {
                foreach (var entry in entries)
                {
                    this.DataContext.TemplateFieldValues.Remove(entry);
                    counter++;
                }

                this.SaveChanges();
            }

            return counter;
        }
    }
}