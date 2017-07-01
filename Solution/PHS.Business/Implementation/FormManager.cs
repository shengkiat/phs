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
                var formView = FormViewModel.CreateFromObject(form);

                List<TemplateViewModel> templates = null;

                if (form != null)
                {
                    templates = FindAllTemplatesByFormId(formId);

                    if (templates.Count() == 1)
                    {
                        result = "Unable to delete template - Unable to delete template when there is only one remains";
                    }

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

        

        public Template FindTemplate(int templateID)
        {
            Template template = new Template();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                template = unitOfWork.FormRepository.GetTemplate(templateID);
            }

            return template;
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

        public void UpdateTemplate(TemplateViewModel model, Template template)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.FormRepository.UpdateTemplate(model, template);

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }
        }

        public void UpdateTemplateFields(Template template, TemplateFieldViewModel fieldView)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.FormRepository.UpdateTemplateField(template, fieldView);

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

                var templateView = TemplateViewModel.CreateFromObject(template);

                if (template != null)
                {

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

        public Template CreateNewFormAndTemplate()
        {
            Template template = new Template();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var form = unitOfWork.FormRepository.CreateNewForm();
                    template = unitOfWork.FormRepository.CreateNewTemplate(form.FormID);

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }

            return template;

        }

        public Template CreateNewTemplate(int formId)
        {
            Template template = new Template();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    template = unitOfWork.FormRepository.CreateNewTemplate(formId);

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }

            return template;

        }

        public void InsertTemplateFieldValue(TemplateFieldViewModel field, string value, Guid entryId)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.FormRepository.InsertTemplateFieldValue(field, value, entryId);

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }
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
