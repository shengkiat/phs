using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using System.IO;
using OfficeOpenXml;
using PHS.DB.ViewModels.Forms;
using PHS.Business.Extensions;
using System.Transactions;
using System.Web.Mvc;
using PHS.Common;
using static PHS.Common.Constants;

namespace PHS.Business.Implementation
{
    public class FormManager : BaseManager, IFormManager, IManagerFactoryBase<IFormManager>
    {
        public IFormManager Create()
        {
            return new FormManager();
        }

        public List<Template> FindAllTemplates()
        {
            List<Template> templates = new List<Template>();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                templates = unitOfWork.FormRepository.GetBaseTemplates();

                if (templates != null)
                {
                    return templates;
                }
            }

            return templates;

        }

        public List<FormViewModel> FindAllFormsByDes()
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var forms = unitOfWork.FormRepository.GetForms().OrderByDescending(f => f.DateAdded).ToList();

                return forms;
            }
        }

        public string DeleteFormAndTemplate(int formId)
        {
            string result = null;
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var form = unitOfWork.FormRepository.GetForm(formId);

                if (form != null)
                {
                    var formView = FormViewModel.CreateFromObject(form);

                    List<TemplateViewModel> templates = null;

                    templates = FindAllTemplatesByFormId(formId);

                    foreach (var templateView in templates)
                    {
                        templateView.Entries = HasSubmissions(templateView).ToList();
                        if (templateView.Entries.Any())
                        {
                            result = "Unable to delete - Templates must have no entries to be able to be deleted";
                        }
                    }

                    if (result.IsNullOrEmpty())
                    {
                        try
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                foreach (var templateView in templates)
                                {
                                    unitOfWork.FormRepository.DeleteTemplate(templateView.TemplateID.Value);
                                }

                                unitOfWork.FormRepository.DeleteForm(formId);

                                unitOfWork.Complete();
                                scope.Complete();

                                result = "success";
                            }


                        }
                        catch
                        {
                            result = "Unable to delete form - there is an error deleting the form and template";
                        }
                    }
                }

                else
                {
                    result = "Unable to delete form - invalid id";
                }
            }

            return result;
        }

        public List<TemplateViewModel> FindAllTemplatesByFormId(int formId)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var templates = unitOfWork.FormRepository.GetTemplates(formId).OrderByDescending(f => f.DateAdded).ToList();

                return templates;
            }
        }

        public Template FindPublicTemplate(string slug)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                Form form = unitOfWork.FormRepository.GetPublicForm(slug);
                if (form != null)
                {
                    return form.Templates.OrderBy(f => f.Version).First();
                }
                return null;
            }
        }


        public Template FindTemplate(int templateID)
        {
            Template template = null;
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                template = unitOfWork.FormRepository.GetTemplate(templateID);
            }

            return template;
        }

        public TemplateViewModel FindTemplateToEdit(int templateID)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                Template template = unitOfWork.FormRepository.GetTemplate(templateID);

                if (template == null)
                {
                    return null;
                }

                Constants.TemplateMode Mode = Constants.TemplateMode.EDIT;

                var templateView = TemplateViewModel.CreateFromObject(template);
                templateView.Entries = HasSubmissions(templateView).ToList();

                if (templateView.Entries.Any())
                {
                    var templates = FindAllTemplatesByFormId(template.FormID);
                    if (templates.Count() == template.Version)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            template = unitOfWork.FormRepository.CopyTemplate(template);

                            unitOfWork.Complete();
                            scope.Complete();
                        }
                    }

                    else
                    {
                        Mode = Constants.TemplateMode.READONLY;
                    }
                }

                TemplateViewModel model1 = TemplateViewModel.CreateFromObject(template);
                model1.Mode = Mode;

                return model1;
            }
        }

        public Template FindPreRegistrationForm()
        {
            Template template = new Template();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                template = unitOfWork.FormRepository.GetPreRegistrationForm();
            }

            return template;

        }

        public void UpdateTemplate(TemplateViewModel model, FormCollection collection, IDictionary<string, string> Fields)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var template = FindTemplate(model.TemplateID.Value);

                using (TransactionScope scope = new TransactionScope())
                {
                    // first update the form metadata
                    unitOfWork.FormRepository.UpdateTemplate(model, template);

                    // then if fields were passed in, update them
                    if (Fields.Count() > 0)
                    {
                        foreach (var kvp in Fields)
                        {

                            var domId = Convert.ToInt32(kvp.Key);
                            if (domId >= 0)
                            {
                                var fieldType = collection.FormFieldValue(domId, "FieldType");
                                var fieldId = collection.FormFieldValue(domId, "Id");
                                var minAge = collection.FormFieldValue(domId, "MinimumAge").IsInt(18);
                                var maxAge = collection.FormFieldValue(domId, "MaximumAge").IsInt(100);

                                if (minAge >= maxAge)
                                {
                                    minAge = 18;
                                    maxAge = 100;
                                }

                                var fieldTypeEnum = (Constants.TemplateFieldType)Enum.Parse(typeof(Constants.TemplateFieldType), fieldType.ToUpper());

                                var fieldView = new TemplateFieldViewModel
                                {
                                    DomId = Convert.ToInt32(domId),
                                    FieldType = fieldTypeEnum,
                                    MaxCharacters = collection.FormFieldValue(domId, "MaxCharacters").IsInt(),
                                    Text = collection.FormFieldValue(domId, "Text"),
                                    Label = collection.FormFieldValue(domId, "Label"),
                                    IsRequired = collection.FormFieldValue(domId, "IsRequired").IsBool(),
                                    Options = collection.FormFieldValue(domId, "Options"),
                                    SelectedOption = collection.FormFieldValue(domId, "SelectedOption"),
                                    AddOthersOption = collection.FormFieldValue(domId, "AddOthersOption").IsBool(),
                                    OthersOption = collection.FormFieldValue(domId, "OthersOption"),
                                    HoverText = collection.FormFieldValue(domId, "HoverText"),
                                    Hint = collection.FormFieldValue(domId, "Hint"),
                                    MinimumAge = minAge,
                                    MaximumAge = maxAge,
                                    HelpText = collection.FormFieldValue(domId, "HelpText"),
                                    SubLabel = collection.FormFieldValue(domId, "SubLabel"),
                                    Size = collection.FormFieldValue(domId, "Size"),
                                    Columns = collection.FormFieldValue(domId, "Columns").IsInt(20),
                                    Rows = collection.FormFieldValue(domId, "Columns").IsInt(2),
                                    Validation = collection.FormFieldValue(domId, "Validation"),
                                    Order = collection.FormFieldValue(domId, "Order").IsInt(1),
                                    MaxFileSize = collection.FormFieldValue(domId, "MaxFileSize").IsInt(5000),
                                    MinFileSize = collection.FormFieldValue(domId, "MinFileSize").IsInt(5),
                                    ValidFileExtensions = collection.FormFieldValue(domId, "ValidExtensions"),
                                    ImageBase64 = collection.FormFieldValue(domId, "ImageBase64"),
                                    MatrixColumn = collection.FormFieldValue(domId, "MatrixColumn"),
                                    MatrixRow = collection.FormFieldValue(domId, "MatrixRow")
                                };

                                if (!fieldId.IsNullOrEmpty() && fieldId.IsInteger())
                                {
                                    fieldView.TemplateFieldID = Convert.ToInt32(fieldId);
                                }

                                unitOfWork.FormRepository.UpdateTemplateField(template, fieldView);

                            }
                        }
                    }

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }
        }

        public string DeleteTemplate(int templateID)
        {
            string result = null;
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var template = FindTemplate(templateID);

                if (template != null)
                {
                    var templateView = TemplateViewModel.CreateFromObject(template);

                    var templates = FindAllTemplatesByFormId(template.FormID);
                    if (templates.Count() == 1)
                    {
                        result = "Unable to delete template - Unable to delete template when there is only one remains";
                    }

                    else
                    {
                        templateView.Entries = HasSubmissions(templateView).ToList();

                        if (!templateView.Entries.Any())
                        {
                            try
                            {
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    unitOfWork.FormRepository.DeleteTemplate(templateID);

                                    unitOfWork.Complete();
                                    scope.Complete();

                                    result = "success";
                                }
                            }
                            catch
                            {
                                result = "Unable to delete template - there is an error deleting the template";
                            }
                        }

                        else
                        {
                            result = "Unable to delete template - Template must have no entries to be able to be deleted";
                        }
                    }


                }

                else
                {
                    result = "Unable to delete template - invalid id";
                }
            }

            return result;
        }

        public void DeleteTemplateField(int templateFieldID)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.FormRepository.DeleteTemplateField(templateFieldID);

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }
        }

        public IEnumerable<TemplateFieldValueViewModel> HasSubmissions(TemplateViewModel model)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var fieldValues = unitOfWork.FormRepository.GetTemplateFieldValuesByTemplate(model.TemplateID.Value);
                var values = fieldValues
                             .Select((fv) =>
                             {
                                 return TemplateFieldValueViewModel.CreateFromObject(fv);
                             })
                             .OrderBy(f => f.FieldOrder)
                             .ThenByDescending(f => f.DateAdded);

                return values;
            }
        }

        public Template CreateNewFormAndTemplate(string title)
        {
            Template template = new Template();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var form = unitOfWork.FormRepository.CreateNewForm(title);
                    template = unitOfWork.FormRepository.CreateNewTemplate(title, form.FormID);

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }

            return template;

        }

        public string EditForm(int id, string title)
        {
            string result = null;
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                Form form = unitOfWork.FormRepository.GetForm(id);
                if (form != null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.FormRepository.UpdateForm(title, form);

                        unitOfWork.Complete();
                        scope.Complete();

                        result = "success";
                    }
                }

                else
                {
                    result = "Unable to edit form - invalid id";
                }
            }

            return result;
        }

        public string FillIn(IDictionary<string, string> SubmitFields, TemplateViewModel model, FormCollection formCollection)
        {
            string result = null;
            IList<string> errors = Enumerable.Empty<string>().ToList();
            //var formObj = this._formRepo.GetByPrimaryKey(model.Id.Value);

            var template = FindTemplate(model.TemplateID.Value);

            var templateView = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
            templateView.AssignInputValues(formCollection);
           // this.InsertValuesIntoTempData(SubmitFields, formCollection);

            if (templateView.Fields.Any())
            {
                // first validate fields
                foreach (var field in templateView.Fields)
                {
                    if (!field.SubmittedValueIsValid(formCollection))
                    {
                        field.SetFieldErrors();
                        errors.Add(field.Errors);
                    }

                    var value = field.SubmittedValue(formCollection);
                    if (field.IsRequired && value.IsNullOrEmpty())
                    {
                        field.Errors = "{0} is a required field".FormatWith(field.Label);
                        errors.Add(field.Errors);
                    }
                };

                if (errors.Count == 0)
                {
                    //then insert values
                    var entryId = Guid.NewGuid();

                    using (var unitOfWork = new UnitOfWork(new PHSContext()))
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            foreach (var field in templateView.Fields)
                            {
                                var value = field.SubmittedValue(formCollection);

                                //if it's a file, save it to hard drive
                                if (field.FieldType == Constants.TemplateFieldType.FILEPICKER && !string.IsNullOrEmpty(value))
                                {
                                    //var file = Request.Files[field.SubmittedFieldName()];
                                    //var fileValueObject = value.GetFileValueFromJsonObject();

                                    //if (fileValueObject != null)
                                    //{
                                    //    if (UtilityHelper.UseCloudStorage())
                                    //    {
                                    //        this.SaveImageToCloud(file, fileValueObject.SaveName);
                                    //    }
                                    //    else
                                    //    {
                                    //        file.SaveAs(Path.Combine(HostingEnvironment.MapPath(fileValueObject.SavePath), fileValueObject.SaveName));
                                    //    }
                                    //}
                                }


                                unitOfWork.FormRepository.InsertTemplateFieldValue(field, value, entryId);
                            }

                            unitOfWork.Complete();
                            scope.Complete();

                            result = "success";
                        }
                    }
                }
            }

            if (errors.Count > 0)
            {
                result = errors.ToUnorderedList();
            }
            

            return result;
        }

        public string InsertUploadDataToTemplate(byte[] data, int templateID)
        {
            Template template = new Template();

            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                template = unitOfWork.FormRepository.GetTemplate(templateID);

                // byte[] fileByte = System.IO.File.ReadAllBytes(filePath);
                using (MemoryStream ms = new MemoryStream(data))
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                        return ("Invalid File.");
                    else
                    {
                        foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                        {
                            int x = 1;
                            // check if header match
                            foreach (var field in template.TemplateFields)
                            {
                                if (field.FieldType == "ADDRESS")
                                {
                                    if (worksheet.Cells[1, x].Value.Equals("Blk/Hse No"))
                                    {
                                        x++;
                                    }
                                    else
                                    {
                                        return ("Invalid File.");
                                    }
                                    if (worksheet.Cells[1, x].Value.Equals("Unit"))
                                    {
                                        x++;
                                    }
                                    else
                                    {
                                        return ("Invalid File.");
                                    }
                                    if (worksheet.Cells[1, x].Value.Equals("Street Address"))
                                    {
                                        x++;
                                    }
                                    else
                                    {
                                        return ("Invalid File.");
                                    }
                                    if (worksheet.Cells[1, x].Value.Equals("Postal Code"))
                                    {
                                        x++;
                                    }
                                    else
                                    {
                                        return ("Invalid File.");
                                    }

                                }

                                if (worksheet.Cells[1, x].Value.Equals(field.Label))
                                {
                                    x++;
                                    continue;
                                }
                                else
                                {
                                    return ("Invalid File.");
                                }
                            }

                            int y = 1;

                            foreach (var field in template.TemplateFields)
                            {
                                if (field.FieldType == "ADDRESS")
                                {
                                    for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                                    {
                                        AddressViewModel address = new AddressViewModel();
                                        address.Blk = worksheet.Cells[row, y].Value.ToString();
                                        address.Unit = worksheet.Cells[row, y + 1].Value.ToString();
                                        address.StreetAddress = worksheet.Cells[row, y + 2].Value.ToString();
                                        address.ZipCode = worksheet.Cells[row, y + 3].Value.ToString();

                                        string value1 = address.ToJson();

                                        TemplateFieldValue value = new TemplateFieldValue();
                                        value.Value = value1;
                                        value.EntryId = Guid.NewGuid();
                                        value.DateAdded = DateTime.Now;
                                        value.TemplateFieldID = field.TemplateFieldID;

                                        field.TemplateFieldValues.Add(value);

                                        unitOfWork.ActiveLearningContext.TemplateFieldValues.Add(value);
                                    }

                                    y += 4;

                                }
                                else if (worksheet.Cells[1, y].Value.Equals(field.Label))
                                {

                                    for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                                    {
                                        if (worksheet.Cells[row, y].Value == null)
                                        {
                                            continue;
                                        }

                                        TemplateFieldValue value = new TemplateFieldValue();
                                        value.Value = worksheet.Cells[row, y].Value.ToString();
                                        value.EntryId = Guid.NewGuid();
                                        value.DateAdded = DateTime.Now;
                                        value.TemplateFieldID = field.TemplateFieldID;

                                        field.TemplateFieldValues.Add(value);

                                        unitOfWork.ActiveLearningContext.TemplateFieldValues.Add(value);

                                    }

                                    y++;

                                }


                            }
                        }
                    }
                }

                unitOfWork.Complete();
            }

            return "";

        }

        public string FindSaveValue(string entryId, int fieldID)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new PHSContext()))
                {
                    var guid = Guid.Parse(entryId);
                    var value = unitOfWork.TemplateFieldValues.Find(u => u.EntryId.Equals(guid) && u.TemplateFieldID == fieldID);

                    return value.First().Value;
                }
            }
            catch
            {
                return "";
            }

        }
    }
}
