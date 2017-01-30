using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using PHS.Repository.Repository.Core;
using PHS.Repository.Context;
using System.Data.Entity.Core.Objects;
using PHS.DB;
using PHS.FormBuilder.ViewModels;
using PHS.FormBuilder.Extensions;
using PHS.Common;
using PHS.FormBuilder.Helpers;
using PHS.FormBuilder.Models;
using System.Data.Entity;

namespace PHS.Repository.Repository
{
    public class FormRepository : BaseRespository<form, int>
    {    
        public form GelForm(int key)
        {
            this.DataContext = new PHSContext();
            return this.DataContext.forms.Where(u => u.ID == key).Include(x => x.form_fields).FirstOrDefault();
        }

        public FormRepository(PHSContext datacontext)
            : base(datacontext)
        {

        }

        public FormRepository()
            : this(new PHSContext())
        {

        }

        public override DbSet<form> EntitySet
        {
            get { return this.DataContext.forms; }
        }

        protected override form ConvertToNativeEntity(form entity)
        {
            return entity;
        }

        protected override int SelectPrimaryKey(form entity)
        {
            return entity.ID;
        }

        public override form GetByPrimaryKey(int key)
        {
            return this.GetByPrimaryKey(s => s.ID == key);
        }

        public void UpdateField(form form1, FormFieldViewModel fieldView)
        {
            if (form1 == null)
            {
                throw new Exception("Cannot update a field when a form is null");
            }

            if (!fieldView.Id.HasValue)
            {
                // create
                var fField = new form_fields
                {
                    DomId = fieldView.DomId,
                    Label = fieldView.Label.LimitWithElipses(40),
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
                    HelpText = fieldView.HelpText.Trim(),
                    Validation = fieldView.Validation,
                    Order = fieldView.Order,
                    MinimumAge = fieldView.MinimumAge,
                    MaximumAge = fieldView.MaximumAge,
                    MaxFilesizeInKb = fieldView.MaxFileSize,
                    MinFilesizeInKb = fieldView.MinFileSize,
                    ValidFileExtensions = fieldView.ValidFileExtensions,
                    DateAdded = DateTime.UtcNow
                };

                form1.form_fields.Add(fField);
                this.SaveChanges();
            }
            else
            {
                var fField = this.DataContext.form_fields.Where(field => field.ID == fieldView.Id.Value).FirstOrDefault();
                if (fField != null)
                {

                    fField.Label = fieldView.Label.LimitWithElipses(45);
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
                    fField.HelpText = fieldView.HelpText.Trim();
                    fField.Validation = fieldView.Validation;
                    fField.Order = fieldView.Order;
                    fField.MinimumAge = fieldView.MinimumAge;
                    fField.MaximumAge = fieldView.MaximumAge;
                    fField.MaxFilesizeInKb = fieldView.MaxFileSize;
                    fField.MinFilesizeInKb = fieldView.MinFileSize;
                    fField.ValidFileExtensions = fieldView.ValidFileExtensions;
                }

                this.SaveChanges();
            }

        }

        public form CreateNew()
        {
            string formName = "New Registration Form";
            var form = new form
            {
                Title = formName,
                Slug = formName.ToSlug(),
                Status = Constants.FormStatus.DRAFT.ToString(),
                DateAdded = DateTime.UtcNow,
                ConfirmationMessage = "Thank you for signing up"
            };

            this.DataContext.forms.Add(form);
            this.SaveChanges();
            return form;
        }

        public void Update(FormViewModel model, form form1)
        {
            if (model == null)
            {
                throw new Exception("Invalid update operation. Form view is null.");
            }

            if (form1 == null)
            {
                throw new Exception("Invalid update operation. Form not found.");
            }

            form1.Status = model.Status.ToString();
            form1.Title = string.IsNullOrEmpty(model.Title) ? "Registration" : model.Title;
            // form.TabOrder = model.TabOrder; // excluding tab order for first launch
            form1.ConfirmationMessage = model.ConfirmationMessage;
            form1.Theme = model.Theme;
            form1.NotificationEmail = model.NotificationEmail;
            this.SaveChanges();
        }

        public void DeleteField(int id)
        {
            var field = this.DataContext.form_fields.Where(f => f.ID == id).FirstOrDefault();
            if (field != null)
            {
                this.DataContext.form_fields.Remove(field);
                this.SaveChanges();
            }
        }

        public IEnumerable<FormFieldValueViewModel> GetRegistrantsByForm(FormViewModel model)
        {
            var fieldValues = this.GetRegistrantsByForm(model.Id.Value);
            var values = fieldValues
                         .Select((fv) =>
                         {
                             return FormFieldValueViewModel.CreateFromObject(fv);
                         })
                         .OrderBy(f => f.FieldOrder)
                         .ThenByDescending(f => f.DateAdded);

            return values;
        }

        public List<form_field_values> GetRegistrantsByForm(int formId)
        {
            var fieldValues = this.DataContext.form_field_values;
            return this.DataContext
                             .form_fields
                             .Include("Forms")
                             .Where(field => field.forms.Any(f => f.ID == formId))
                             .Join(fieldValues, fields => fields.ID, fieldValue => fieldValue.FieldId, (FormField, FormFieldValue) => FormFieldValue)
                             .ToList();
        }

        public void InsertFieldValue(FormFieldViewModel field, string value, Guid entryId, string userId = "")
        {
            if (field.FieldType != Constants.FieldType.HEADER)
            {
                var fieldVal = new form_field_values
                {
                    FieldId = field.Id.Value,
                    Value = value,
                    EntryId = entryId,
                    DateAdded = DateTime.UtcNow
                };

                this.DataContext.form_field_values.Add(fieldVal);
                this.SaveChanges();
            }
        }

        public void DeleteEntries(IEnumerable<string> selectedEntries)
        {
            var selectedGuids = selectedEntries.Select(se => new Guid(se));
            var entries = this.DataContext.form_field_values.Where(fv => selectedGuids.Any(se => fv.EntryId == se));

            foreach (var entry in entries)
            {
                this.DeleteFileEntry(entry);
                this.DataContext.form_field_values.Remove(entry);
            }

            this.SaveChanges();
        }

        public void DeleteFileEntry(form_field_values entry)
        {
            if (entry.form_fields.FieldType.ToUpper().IsTheSameAs(Constants.FieldType.FILEPICKER.ToString()))
            {
                var fileObj = entry.Value.FromJson<FileValueObject>();

                if (fileObj.IsSavedInCloud)
                {
                    
                }
                else
                {
                    if (fileObj != null && !fileObj.FileName.IsNullOrEmpty())
                    {
                        if (File.Exists(fileObj.FullFilePath().Replace(@"\\", @"\")))
                        {
                            File.Delete(fileObj.FullFilePath().Replace(@"\\", @"\"));
                        }
                    }
                }
            }
        }


        public List<FormViewModel> GetForms()
        {
            var formViews = new List<FormViewModel>();
            var formSet = this.DataContext.forms.ToList();
            foreach (var form in formSet)
            {
                formViews.Add(FormViewModel.CreateBasicFromObject(form));
            }

            return formViews;
        }

        public void DeleteForm(int formId)
        {
            var form = this.GetByPrimaryKey(formId);
            this.DeleteForm(form);
        }

        public void DeleteForm(form form1)
        {
           // form1.FormFields.LoadOnce();
            this.DataContext.forms.Remove(form1);
            var fields = form1.form_fields.ToList();

            foreach (var f in fields)
            {
                this.DataContext.form_fields.Remove(f);
            }

            this.SaveChanges();
        }

        public FileValueObject GetFileFieldValue(int valueId)
        {
            var valueObject = this.DataContext.form_field_values.Where(v => v.ID == valueId).FirstOrDefault();
            if (valueObject != null)
            {
                var value = valueObject.Value.FromJson<FileValueObject>();
                return value;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="olderThanInDays"></param>
        /// <returns></returns>
        public int DeleteForms(int olderThanInDays)
        {
            int counter = 0;
            this.DeleteSubmissions(olderThanInDays);
            var deleteDate = DateTime.Now.AddDays(-olderThanInDays);
            var forms = this.DataContext.forms.Where(f => f.DateAdded < deleteDate).ToList();

            foreach (var f in forms)
            {
                this.DeleteForm(f);
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
            var entries = this.DataContext.form_field_values.Where(fv => fv.DateAdded < deleteDate).ToList();

            if (entries.Any())
            {
                foreach (var entry in entries)
                {
                    this.DataContext.form_field_values.Remove(entry);
                    counter++;
                }

                this.SaveChanges();
            }

            return counter;
        }
    }
}