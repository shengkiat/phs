using PHS.Business.Extensions;
using PHS.Business.Interface;
using PHS.Business.ViewModel.FormExport;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.FormBuilder.ViewModel;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PHS.Business.Implementation
{
    public class FormExportManager : BaseFormManager, IFormExportManager, IManagerFactoryBase<IFormExportManager>
    {
        public FormExportManager(PHSUser loginUser) : base(loginUser)
        {
        }

        public IFormExportManager Create(PHSUser loginUser)
        {
            return new FormExportManager(loginUser);
        }

        public FormExportViewModel RetrieveAllForms(int eventId, out string message)
        {
            message = string.Empty;
            using (var unitOfWork = CreateUnitOfWork())
            {
                FormExportViewModel result = FormExportViewModel.CreateFromObject(eventId);
                IEnumerable<Modality> modalities = unitOfWork.Modalities.GetModalityByEventID(eventId);
                if (modalities != null)
                {
                    foreach(var modality in modalities)
                    {
                        foreach(var form in modality.Forms)
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

        public SortFieldViewModel AddNewSortEntries(int formId)
        {
            var sortFieldViewModel = new SortFieldViewModel();
            sortFieldViewModel.SortFields = new List<SelectListItem>();
            int columnCount = 0;

            using (var unitOfWork = CreateUnitOfWork())
            {
                IDictionary<string, string> fieldColumnMapping = new System.Collections.Generic.Dictionary<string, string>();

                var form = unitOfWork.FormRepository.GetForm(formId);

                foreach(var template in form.Templates)
                {
                    var templateView = TemplateViewModel.CreateFromObject(template);
                    templateView.Entries = unitOfWork.FormRepository.GetTemplateFieldValuesByForm(templateView).ToList();
                    templateView.GroupedEntries = templateView.Entries.GroupBy(g => g.EntryId);

                    foreach (var s in templateView.Fields.OrderBy(f => f.TemplateFieldID))
                    {
                        string label = s.Label;
                        if (fieldColumnMapping.ContainsKey(label))
                        {
                            label = columnCount + ": " + label;
                        }

                        fieldColumnMapping.Add(label, label);

                        sortFieldViewModel.SortFields.Add(new SelectListItem
                        {
                            Text = label.StripHTML().Limit(100),
                            Value = "" + s.TemplateFieldID.Value
                        });

                        columnCount++;
                    }
                }

                sortFieldViewModel.SortFields.Add(new SelectListItem
                {
                    Text = Constants.Export_SubmittedOn,
                    Value = Constants.Export_SubmittedOn
                });
            }

            return sortFieldViewModel;
        }

        public CriteriaFieldViewModel AddNewCriteriaEntries(int formId)
        {
            var criteriaFieldViewModel = new CriteriaFieldViewModel();
            criteriaFieldViewModel.FieldLabels = new List<SelectListItem>();
            int columnCount = 0;

            using (var unitOfWork = CreateUnitOfWork())
            {
                IDictionary<string, string> fieldColumnMapping = new System.Collections.Generic.Dictionary<string, string>();

                var form = unitOfWork.FormRepository.GetForm(formId);

                foreach (var template in form.Templates)
                {
                    var templateView = TemplateViewModel.CreateFromObject(template);
                    templateView.Entries = unitOfWork.FormRepository.GetTemplateFieldValuesByForm(templateView).ToList();
                    templateView.GroupedEntries = templateView.Entries.GroupBy(g => g.EntryId);

                    criteriaFieldViewModel.Fields = templateView.Fields;
                    criteriaFieldViewModel.GroupedEntries = templateView.GroupedEntries;
                    criteriaFieldViewModel.CriteriaSubFields = Enumerable.Empty<CriteriaSubFieldViewModel>().ToList();

                    foreach (var s in templateView.Fields.OrderBy(f => f.TemplateFieldID))
                    {
                        string label = s.Label;
                        if (fieldColumnMapping.ContainsKey(label))
                        {
                            label = columnCount + ": " + label;
                        }

                        fieldColumnMapping.Add(label, label);

                        criteriaFieldViewModel.FieldLabels.Add(new SelectListItem
                        {
                            Text = label.StripHTML().Limit(100),
                            Value = "" + s.TemplateFieldID.Value
                        });

                        columnCount++;
                    }
                }

                criteriaFieldViewModel.FieldLabels.Add(new SelectListItem
                {
                    Text = Constants.Export_SubmittedOn,
                    Value = Constants.Export_SubmittedOn
                });
            }

            return criteriaFieldViewModel;
        }

        public CriteriaSubFieldViewModel AddNewCriteriaSubEntries(int formId)
        {
            var criteriaSubFieldViewModel = new CriteriaSubFieldViewModel();

            using (var unitOfWork = CreateUnitOfWork())
            {
                var form = unitOfWork.FormRepository.GetForm(formId);

                foreach (var template in form.Templates)
                {
                    var templateView = TemplateViewModel.CreateFromObject(template);

                    templateView.Entries = HasSubmissions(unitOfWork, templateView).ToList();

                    templateView.GroupedEntries = templateView.Entries.GroupBy(g => g.EntryId);

                    criteriaSubFieldViewModel.Fields = templateView.Fields;
                    criteriaSubFieldViewModel.GroupedEntries = templateView.GroupedEntries;
                }
            }

            return criteriaSubFieldViewModel;
        }

        private IEnumerable<TemplateFieldValueViewModel> HasSubmissions(IUnitOfWork unitOfWork, TemplateViewModel model)
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

        public FormExportResultViewModel CreateFormEntriesDataTable(FormExportViewModel model)
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

                FormExportResultViewModel result = new FormExportResultViewModel();
                result.ValuesDataTable = CreateDataTable(form, templateViews, unitOfWork, model.SortFields, model.CriteriaFields);
                result.Title = form.Title;

                return result;
            }
        }

        private DataTable CreateDataTable(Form form, IList<TemplateViewModel> templateViews, IUnitOfWork unitOfWork, List<SortFieldViewModel> sortFields, List<CriteriaFieldViewModel> criteriaFields)
        {
            var dt = new DataTable(form.Title);
            IDictionary<int, int> fieldIdColumnMapping = new System.Collections.Generic.Dictionary<int, int>();

            int columnCount = 0;

            AddColumn(dt, "Nric", columnCount);

            if (Constants.Internal_Form_Type_Phlebotomy.Equals(form.InternalFormType))
            {    
                AddColumn(dt, "Name", columnCount);
                AddColumn(dt, "DOB", columnCount);
                AddColumn(dt, "Sex", columnCount);
            }

            else
            {
                foreach (var template in templateViews)
                {
                    foreach (var field in template.Fields.OrderBy(f => f.TemplateFieldID))
                    {
                        fieldIdColumnMapping.Add(field.TemplateFieldID.Value, columnCount);

                        if (field.FieldType == Constants.TemplateFieldType.MATRIX)
                        {
                            string[] rows = field.MatrixRow.Split(Constants.Form_Option_Split);
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
            }

            dt.Columns.Add(new DataColumn(Constants.Export_SubmittedOn));

            foreach (var template in templateViews)
            {
                foreach (var group in template.GroupedEntries)
                {
                    DataRow row = dt.NewRow();

                    Participant participant = null;

                    row[0] = "";

                    var entryId = group.FirstOrDefault().EntryId;
                    IEnumerable<ParticipantJourneyModality> ptJourneyModalityItems = unitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModalityByFormIdAndEntryId(form.FormID, new Guid(entryId));
                    if (ptJourneyModalityItems != null && ptJourneyModalityItems.Count() > 0)
                    {
                        var participantId = ptJourneyModalityItems.FirstOrDefault().ParticipantID;
                        participant = unitOfWork.Participants.FindParticipant(participantId);
                        if (participant != null)
                        {
                            row[0] = participant.Nric;
                        }
                    }

                    if (Constants.Internal_Form_Type_Phlebotomy.Equals(form.InternalFormType))
                    {
                        if (participant != null)
                        {
                            row[1] = participant.FullName;
                            if (participant.DateOfBirth != null)
                            {
                                row[2] = participant.DateOfBirth.Value.ToString("dd MMM yyyy");
                            }

                            row[3] = participant.Gender;
                        }
                    }

                    else
                    {
                        foreach (var entry in group.OrderBy(f => f.TemplateFieldID))
                        {
                            int columnIndex = fieldIdColumnMapping[entry.TemplateFieldID] + 1;

                            if (entry.FieldType == Constants.TemplateFieldType.MATRIX)
                            {
                                var matrixField = entry.Value;

                                string[] submissions = matrixField.Split(Constants.Form_Option_Split);
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

                                if (address != null)
                                {
                                    row[columnIndex] = address.Blk;
                                    row[columnIndex + 1] = address.Unit;
                                    row[columnIndex + 2] = address.StreetAddress;
                                    row[columnIndex + 3] = address.ZipCode;
                                }
                            }
                            else if (entry.FieldType == Constants.TemplateFieldType.BMI)
                            {
                                var bmiField = entry.Value;

                                BMIViewModel bmi = bmiField.FromJson<BMIViewModel>();

                                if (bmi != null)
                                {
                                    row[columnIndex] = bmi.Weight;
                                    row[columnIndex + 1] = bmi.Height;
                                    row[columnIndex + 2] = bmi.BodyMassIndex;
                                }

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
                using (var unitOfWork = CreateUnitOfWork())
                {
                    foreach (var sortFieldViewModel in sortFields)
                    {
                        string sortF = null;
                        if (Constants.Export_SubmittedOn.Equals(sortFieldViewModel.TemplateFieldID))
                        {
                            sortF = Constants.Export_SubmittedOn;
                        }

                        else
                        {
                            var templateField = unitOfWork.FormRepository.GetTemplateField(Int32.Parse(sortFieldViewModel.TemplateFieldID));
                            if (templateField != null)
                            {
                                sortF = templateField.Label.StripHTML();
                            }
                        }

                        var sortO = sortFieldViewModel.SortOrder;
                        if (!string.IsNullOrEmpty(sortF) && !string.IsNullOrEmpty(sortO))
                        {
                            result += string.Format(", {0} {1}", sortF, sortO);
                        }
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
                using (var unitOfWork = CreateUnitOfWork())
                {
                    foreach (var criteriaField in criteriaFields)
                    {
                        if (!String.IsNullOrEmpty(criteriaField.TemplateFieldID)
                            && !String.IsNullOrEmpty(criteriaField.CriteriaLogic)
                            && !String.IsNullOrEmpty(criteriaField.CriteriaValue[criteriaField.TemplateFieldID]))
                        {
                            string label = null;
                            if (Constants.Export_SubmittedOn.Equals(criteriaField.TemplateFieldID))
                            {
                                label = Constants.Export_SubmittedOn;
                            }

                            else
                            {
                                var templateField = unitOfWork.FormRepository.GetTemplateField(Int32.Parse(criteriaField.TemplateFieldID));
                                if (templateField != null)
                                {
                                    label = templateField.Label.StripHTML();
                                }
                            }

                            result += string.Format(" OR [{0}] {1}", label, getConvertedCriteriaValue(criteriaField.CriteriaLogic, criteriaField.CriteriaValue[criteriaField.TemplateFieldID]));
                            if (criteriaField.CriteriaSubFields != null)
                            {
                                foreach (var criteriaSubField in criteriaField.CriteriaSubFields)
                                {
                                    result += string.Format(" {0} [{1}] {2}", criteriaSubField.OperatorLogic, label, getConvertedCriteriaValue(criteriaSubField.CriteriaLogic, criteriaSubField.CriteriaValue[criteriaField.TemplateFieldID]));
                                }
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

            string replaceValue = value.Replace("'", "''");

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
                    return string.Format("LIKE '{0}*'", replaceValue);
                case "endswith":
                    return string.Format("LIKE '*{0}'", replaceValue);
                case "contains":
                    return string.Format("LIKE '*{0}*'", replaceValue);
                case "doesnotcontain":
                    return string.Format("NOT LIKE '*{0}*'", replaceValue);
                case "in":
                    return string.Format("IN ({0})", replaceValue);
                default:
                    return string.Format("{0} '{1}'", mappedValues[criteriaLogic], replaceValue);
            }

        }


    }
}
