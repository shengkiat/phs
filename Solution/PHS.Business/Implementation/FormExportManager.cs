using PHS.Business.Extensions;
using PHS.Business.Interface;
using PHS.Business.ViewModel.FormExport;
using PHS.Common;
using PHS.DB.ViewModels.Form;
using PHS.FormBuilder.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHS.Business.Implementation
{
    public class FormExportManager : BaseFormManager, IFormExportManager, IManagerFactoryBase<IFormExportManager>
    {
        public IFormExportManager Create()
        {
            return new FormExportManager();
        }

        public DataTable CreateFormEntriesDataTable(FormExportViewModel model)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var form = unitOfWork.FormRepository.GetForm(model.FormID.Value);

                IList<TemplateViewModel> templateViews = new List<TemplateViewModel>();

                foreach(var template in form.Templates)
                {
                    var templateView = TemplateViewModel.CreateFromObject(template);
                    templateView.Entries = unitOfWork.FormRepository.GetTemplateFieldValuesByForm(templateView).ToList();

                    templateView.GroupedEntries = templateView.Entries.GroupBy(g => g.EntryId);

                    templateViews.Add(templateView);
                }
                
                return CreateFormEntriesDataTable(model.Title, templateViews, model.SortFields, model.CriteriaFields);
            }
        }

        private DataTable CreateFormEntriesDataTable(string title, IList<TemplateViewModel> templateViews, List<SortFieldViewModel> sortFields, List<CriteriaFieldViewModel> criteriaFields)
        {
            var dt = new DataTable(title);
            IDictionary<int, int> fieldIdColumnMapping = new System.Collections.Generic.Dictionary<int, int>();
            int columnCount = 0;

            foreach(var template in templateViews)
            {
                foreach (var field in template.Fields.OrderBy(f=> f.TemplateFieldID))
                {
                    fieldIdColumnMapping.Add(field.TemplateFieldID.Value, columnCount);

                    if (field.FieldType == Constants.TemplateFieldType.MATRIX)
                    {
                        string[] rows = field.MatrixRow.Split(",");
                        foreach (string row in rows)
                        {
                            AddColumn(dt, row, columnCount);

                            //dt.Columns.Add(new DataColumn(row));

                            columnCount++;
                        }
                    }
                    else if (field.FieldType == Constants.TemplateFieldType.ADDRESS)
                    {
                        AddColumn(dt, "Blk", columnCount);
                        AddColumn(dt, "Unit", columnCount);
                        AddColumn(dt, "Street Address", columnCount);
                        AddColumn(dt, "ZipCode", columnCount);

                        //dt.Columns.Add(new DataColumn("Blk"));
                        //dt.Columns.Add(new DataColumn("Unit"));
                        //dt.Columns.Add(new DataColumn("Street Address"));
                        //dt.Columns.Add(new DataColumn("ZipCode"));

                        columnCount += 4;

                    }
                    else if (field.FieldType == Constants.TemplateFieldType.BMI)
                    {
                        AddColumn(dt, "Weight", columnCount);
                        AddColumn(dt, "Height", columnCount);
                        AddColumn(dt, "BMI", columnCount);

                        //dt.Columns.Add(new DataColumn("Weight"));
                        //dt.Columns.Add(new DataColumn("Height"));
                        //dt.Columns.Add(new DataColumn("BMI"));

                        columnCount += 3;
                    }
                    else
                    {
                        var colName = field.Label.StripHTML();

                        AddColumn(dt, colName, columnCount);

                        columnCount++;
                    }
                }
            }

            dt.Columns.Add(new DataColumn("Submitted On"));

            foreach (var template in templateViews)
            {
                foreach (var group in template.GroupedEntries)
                {
                    DataRow row = dt.NewRow();

                    foreach (var entry in group.OrderBy(f => f.TemplateFieldID))
                    {

                        int columnIndex = fieldIdColumnMapping[entry.TemplateFieldID];

                        if (entry.FieldType == Constants.TemplateFieldType.MATRIX)
                        {
                            var matrixField = entry.Value;

                            string[] submissions = matrixField.Split(",");
                            foreach (string submitValue in submissions)
                            {
                                row[columnIndex] = submitValue;
                                columnIndex++;
                            }

                            columnIndex--;
                        }
                        else if (entry.FieldType == Constants.TemplateFieldType.ADDRESS)
                        {
                            var addressField = entry.Value;

                            AddressViewModel address = addressField.FromJson<AddressViewModel>();

                            row[columnIndex] = address.Blk;
                            row[columnIndex + 1] = address.Unit;
                            row[columnIndex + 2] = address.StreetAddress;
                            row[columnIndex + 3] = address.ZipCode;
                        }
                        else if (entry.FieldType == Constants.TemplateFieldType.BMI)
                        {
                            var bmiField = entry.Value;

                            BMIViewModel bmi = bmiField.FromJson<BMIViewModel>();

                            row[columnIndex] = bmi.Weight;
                            row[columnIndex + 1] = bmi.Height;
                            row[columnIndex + 2] = bmi.BodyMassIndex;
                        }
                        /*
                        else if (columnIndex < group.Count())
                        {
                            var field = group.ElementAt(columnIndex);
                            row[columnIndex] = field.Format(true);
                        }
                        */
                        else
                        {
                            //row[columnIndex] = entry.Value;
                            row[columnIndex] = entry.Format(true);
                        }
                    }

                    var fieldAddedOn = group.FirstOrDefault().DateAdded;
                    row[dt.Columns.Count-1] = fieldAddedOn.ToString("yyyy-MM-dd HH:mm");
                    dt.Rows.Add(row);
                }
            }

            DataView dv = new DataView(dt);
            dv.RowFilter = GenerateFlitering(criteriaFields);
            dv.Sort = GenerateSorting(sortFields);

            return dv.ToTable();
        }

        private void AddColumn(DataTable dt, string colName, int columnCount)
        {
            if (dt.Columns.Contains(colName))
            {
                dt.Columns.Add(new DataColumn(columnCount + ": " + colName));
            }
            else
            {
                dt.Columns.Add(new DataColumn(colName));
            }
        }

        private string GenerateSorting(List<SortFieldViewModel> sortFields)
        {
            string result = "";

            if (sortFields != null)
            {
                foreach (var sortFieldViewModel in sortFields)
                {
                    var sortF = sortFieldViewModel.FieldLabel;
                    var sortO = sortFieldViewModel.SortOrder;
                    if (!String.IsNullOrEmpty(sortF) && !String.IsNullOrEmpty(sortO))
                    {
                        result += string.Format(", {0} {1}", sortF, sortO);
                    }
                }
            }

            if (result.Length > 1)
            {
                result = result.Remove(0, 2);
            }

            return result;
        }

        private string GenerateFlitering(List<CriteriaFieldViewModel> criteriaFields)
        {
            string result = "";

            if (criteriaFields != null)
            {
                foreach (var criteriaField in criteriaFields)
                {
                    if (!String.IsNullOrEmpty(criteriaField.FieldLabel)
                        && !String.IsNullOrEmpty(criteriaField.CriteriaLogic)
                        && !String.IsNullOrEmpty(criteriaField.CriteriaValue[criteriaField.FieldLabel]))
                    {
                        result += string.Format(" OR [{0}] {1}", criteriaField.FieldLabel, getConvertedCriteriaValue(criteriaField.CriteriaLogic, criteriaField.CriteriaValue[criteriaField.FieldLabel]));
                        if (criteriaField.CriteriaSubFields != null)
                        {
                            foreach (var criteriaSubField in criteriaField.CriteriaSubFields)
                            {
                                result += string.Format(" {0} [{1}] {2}", criteriaSubField.OperatorLogic, criteriaField.FieldLabel, getConvertedCriteriaValue(criteriaSubField.CriteriaLogic, criteriaSubField.CriteriaValue[criteriaField.FieldLabel]));
                            }
                        }
                    }

                }
            }

            if (result.Length > 1)
            {
                result = result.Remove(0, 4);
            }

            return result;
        }


        private string getConvertedCriteriaValue(string criteriaLogic, string value)
        {
            Dictionary<string, string> mappedValues = new Dictionary<string, string>();
            mappedValues.Add("eq", "=");
            mappedValues.Add("neq", "<>");
            mappedValues.Add("gt", ">");
            mappedValues.Add("gte", ">=");
            mappedValues.Add("lt", "<");
            mappedValues.Add("lte", "<=");

            switch (criteriaLogic)
            {
                case "startswith":
                    return string.Format("LIKE '{0}*'", value);
                case "endswith":
                    return string.Format("LIKE '*{0}'", value);
                case "contains":
                    return string.Format("LIKE '*{0}*'", value);
                case "doesnotcontain":
                    return string.Format("NOT LIKE '*{0}*'", value);
                case "in":
                    return string.Format("IN ({0})", value);
                default:
                    return string.Format("{0} '{1}'", mappedValues[criteriaLogic], value);
            }

        }


    }
}
