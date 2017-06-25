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

        public List<Template> FindAllForms()
        {
            List<Template> Forms = new List<Template>();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                Forms = unitOfWork.FormRepository.GetBaseForms();

                if (Forms != null)
                {
                    return Forms;
                }
            }

            return Forms;

        }

        public List<TemplateViewModel> FindAllFormsByDes()
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var forms = unitOfWork.FormRepository.GetForms().OrderByDescending(f => f.DateAdded).ToList();

                return forms;
            }
        }

        public Template FindForm(int formID)
        {
            Template form = new Template();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                form = unitOfWork.FormRepository.GetForm(formID);

                if (form != null)
                {
                    return form;
                }
            }

            return form;

        }

        public void DeleteForm(int formID)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    unitOfWork.FormRepository.DeleteForm(formID);

                    unitOfWork.Complete();
                    scope.Complete();
                }
            }
        }

        public IEnumerable<TemplateFieldValueViewModel> HasSubmissions(TemplateViewModel model)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var fieldValues = unitOfWork.FormRepository.GetRegistrantsByForm(model.Id.Value);
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

        public Template CreateNewForm()
        {
            Template form = new Template();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                form = unitOfWork.FormRepository.CreateNew();

                if (form != null)
                {
                    return form;
                }
            }

            return form;

        }

        public string InsertUploadDataToForm(byte[] data, int formid)
        {
            Template form = new Template();

            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                form = unitOfWork.FormRepository.GetForm(formid);

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
                            foreach (var field in form.FormFields)
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

                            foreach (var field in form.FormFields)
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

                                        FormFieldValue value = new FormFieldValue();
                                        value.Value = value1;
                                        value.EntryId = Guid.NewGuid();
                                        value.DateAdded = DateTime.Now;
                                        value.FieldId = field.ID;

                                        field.FormFieldValues.Add(value);

                                        unitOfWork.ActiveLearningContext.FormFieldValues.Add(value);
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

                                        FormFieldValue value = new FormFieldValue();
                                        value.Value = worksheet.Cells[row, y].Value.ToString();
                                        value.EntryId = Guid.NewGuid();
                                        value.DateAdded = DateTime.Now;
                                        value.FieldId = field.ID;

                                        field.FormFieldValues.Add(value);

                                        unitOfWork.ActiveLearningContext.FormFieldValues.Add(value);

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
                    var value = unitOfWork.FormViewValues.Find(u => u.EntryId.Equals(guid) && u.FieldId == fieldID);

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
