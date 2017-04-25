using PHS.Common;
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
    public class FormRepository : BaseRespository<Form, int>
    {    
        public Form GetForm(int key)
        {
            this.DataContext = new PHSContext();
            return this.DataContext.Forms.Where(u => u.ID == key).Include(x => x.FormFields).FirstOrDefault();
        }

        public FormRepository(PHSContext datacontext)
            : base(datacontext)
        {

        }

        public FormRepository()
            : this(new PHSContext())
        {

        }

        public override DbSet<Form> EntitySet
        {
            get { return this.DataContext.Forms; }
        }

        protected override Form ConvertToNativeEntity(Form entity)
        {
            return entity;
        }

        protected override int SelectPrimaryKey(Form entity)
        {
            return entity.ID;
        }

        public override Form GetByPrimaryKey(int key)
        {
            return this.GetByPrimaryKey(s => s.ID == key);
        }

        public void UpdateField(Form form1, FormFieldViewModel fieldView)
        {
            if (form1 == null)
            {
                throw new Exception("Cannot update a field when a form is null");
            }

            if (!fieldView.Id.HasValue)
            {
                // create
                var fField = new FormField
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

                form1.FormFields.Add(fField);
                this.SaveChanges();
            }
            else
            {
                var fField = this.DataContext.FormFields.Where(field => field.ID == fieldView.Id.Value).FirstOrDefault();
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

        public Form CreateNew()
        {
            string formName = "New Registration Form";
            var form = new Form
            {
                Title = formName,
                //Slug = formName.ToSlug(),
                Slug = formName,
                Status = Constants.FormStatus.DRAFT.ToString(),
                DateAdded = DateTime.UtcNow,
                ConfirmationMessage = "Thank you for signing up",
                IsActive = true
            };

            this.DataContext.Forms.Add(form);
            this.SaveChanges();
            return form;
        }

        public void Update(FormViewModel model, Form form1)
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
            form1.IsPublic = model.IsPublic;
            form1.PublicFormType = model.PublicFormType;
            form1.IsQuestion = model.IsQuestion;
            form1.NotificationEmail = model.NotificationEmail;
            this.SaveChanges();
        }

        public void DeleteField(int id)
        {
            var field = this.DataContext.FormFields.Where(f => f.ID == id).FirstOrDefault();
            if (field != null)
            {
                this.DataContext.FormFields.Remove(field);
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

        public List<FormFieldValue> GetRegistrantsByForm(int formId)
        {
            var fieldValues = this.DataContext.FormFieldValues;

            var formFieldValues = this.DataContext
                             .FormFields
                             .Include("Forms")
                             .Where(field => field.Forms.Any(f => f.ID == formId))
                             .Join(fieldValues, fields => fields.ID, fieldValue => fieldValue.FieldId, (FormField, FormFieldValue) => FormFieldValue)
                             .ToList();


            foreach (var entry in formFieldValues)
            {
                if (entry.FormField == null)
                {
                  var s =   this.DataContext.FormFieldValues.Include("FormField").Where(x => x.ID == entry.ID).FirstOrDefault();
                    entry.FormField = s.FormField;

                }
            }

            return formFieldValues;


            //return this.DataContext
            //                 .FormFields
            //                 .Include("Forms")
            //                 .Where(field => field.Forms.Any(f => f.ID == formId))
            //                 .Join(fieldValues, fields => fields.ID, fieldValue => fieldValue.FieldId, (FormField, FormFieldValue) => FormFieldValue)
            //                 .ToList();
        }

        public void InsertFieldValue(FormFieldViewModel field, string value, Guid entryId, string userId = "")
        {
            if (field.FieldType != Constants.FieldType.HEADER)
            {
                var fieldVal = new FormFieldValue
                {
                    FieldId = field.Id.Value,
                    Value = value,
                    EntryId = entryId,
                    DateAdded = DateTime.UtcNow
                };

                this.DataContext.FormFieldValues.Add(fieldVal);
                this.SaveChanges();
            }
        }

        public void DeleteEntries(IEnumerable<string> selectedEntries)
        {
            var selectedGuids = selectedEntries.Select(se => new Guid(se));
            var entries = this.DataContext.FormFieldValues.Where(fv => selectedGuids.Any(se => fv.EntryId == se));

            foreach (var entry in entries)
            {
                //this.DeleteFileEntry(entry);
                this.DataContext.FormFieldValues.Remove(entry);
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
            var formSet = this.DataContext.Forms.ToList();
            foreach (var form in formSet)
            {
                if (form.IsActive)
                {
                    formViews.Add(FormViewModel.CreateBasicFromObject(form));
                }
            }

            return formViews;
        }

        public List<Form> GetBaseForms()
        {
            var formSet = this.DataContext.Forms.ToList();
           

            return formSet;
        }


        public Form GetPreRegistrationForm(int year = -1)
        {
            
            var form = this.DataContext.Forms.First(u => u.IsPublic && u.IsActive && u.PublicFormType.Equals("PRE-REGISTRATION"));

            return form;
        }

        public void DeleteForm(int formId)
        {
            var form = this.GetByPrimaryKey(formId);
            this.DeleteForm(form);
        }

        public void DeleteForm(Form form1)
        {
            form1.IsActive = false;

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
        public int DeleteForms(int olderThanInDays)
        {
            int counter = 0;
            this.DeleteSubmissions(olderThanInDays);
            var deleteDate = DateTime.Now.AddDays(-olderThanInDays);
            var Forms = this.DataContext.Forms.Where(f => f.DateAdded < deleteDate).ToList();

            foreach (var f in Forms)
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
            var entries = this.DataContext.FormFieldValues.Where(fv => fv.DateAdded < deleteDate).ToList();

            if (entries.Any())
            {
                foreach (var entry in entries)
                {
                    this.DataContext.FormFieldValues.Remove(entry);
                    counter++;
                }

                this.SaveChanges();
            }

            return counter;
        }
    }
}