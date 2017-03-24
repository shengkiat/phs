using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using System.IO;
using OfficeOpenXml;

namespace PHS.Business.Implementation
{
    public class FormManager : BaseManager, IFormManager, IManagerFactoryBase<IFormManager>
    {
        public IFormManager Create()
        {
            return new FormManager();
        }

        public List<form> FindAllForms()
        {
            List<form> forms = new List<form>();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                forms = unitOfWork.formRepository.GetBaseForms();

                if (forms != null)
                {
                    return forms;
                }
            }

            return forms;

        }

        public form FindForm(int formID)
        {
            form form = new form();
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                form = unitOfWork.formRepository.GetForm(formID);

                if (form != null)
                {
                    return form;
                }
            }

            return form;

        }

        public string InsertUploadDataToForm(byte[] data, int formid)
        {
            form form = new form();

            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                form = unitOfWork.formRepository.GetForm(formid);

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
                            foreach (var field in form.form_fields)
                            {
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

                            foreach (var field in form.form_fields)
                            {
                                if (worksheet.Cells[1, y].Value.Equals(field.Label))
                                {

                                    for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                                    {
                                        if (worksheet.Cells[row, y].Value == null)
                                        {
                                            continue;
                                        }

                                        form_field_values value = new form_field_values();
                                        value.Value = worksheet.Cells[row, y].Value.ToString();
                                        value.EntryId = Guid.NewGuid();
                                        value.DateAdded = DateTime.Now;
                                        value.FieldId = field.ID;

                                        field.form_field_values.Add(value);

                                        unitOfWork.ActiveLearningContext.form_field_values.Add(value);

                                    }



                                }

                                y++;
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
