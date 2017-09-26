using OfficeOpenXml;
using PHS.Business.Extensions;
using PHS.Business.Interface;
using PHS.Business.ViewModel.FormImport;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Implementation
{
    public class FormImportManager : BaseFormManager, IFormImportManager, IManagerFactoryBase<IFormImportManager>
    {
        public IFormImportManager Create()
        {
            return new FormImportManager();
        }

        public FormImportViewModel RetrieveAllForms(int eventId, out string message)
        {
            message = string.Empty;
            using (var unitOfWork = CreateUnitOfWork())
            {
                FormImportViewModel result = FormImportViewModel.CreateFromObject(eventId);
                IEnumerable<Modality> modalities = unitOfWork.Modalities.GetModalityByEventID(eventId);
                if (modalities != null)
                {
                    foreach (var modality in modalities)
                    {
                        foreach (var form in modality.Forms)
                        {
                            result.Forms.Add(FormViewModel.CreateFromObject(form));
                        }
                    }
                }

                else
                {
                    message = "No Modality found";
                }

                return result;
            }
        }

        public string InsertUploadDataToTemplate(byte[] data, int formId)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                Template template = FindLatestTemplate(formId);

                // byte[] fileByte = System.IO.File.ReadAllBytes(filePath);
                using (MemoryStream ms = new MemoryStream(data))
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        return ("Invalid File.");
                    }
                        
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

                                        unitOfWork.TemplateFieldValues.Add(value);
                                    }

                                    y += 4;

                                }
                                else if (worksheet.Cells[1, y].Value.Equals(field.Label.StripHTML()))
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

                                        unitOfWork.TemplateFieldValues.Add(value);

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
    }
}
