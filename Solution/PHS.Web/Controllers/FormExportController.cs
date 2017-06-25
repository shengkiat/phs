using Novacode;
using PHS.Business.Common;
using PHS.Business.Extensions;
using PHS.Business.Implementation;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Forms;
using PHS.FormBuilder.ViewModel;
using PHS.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PHS.Web.Controllers
{
    public class FormExportController : BaseController
    {
        private FormRepository _formRepo { get; set; }

        public FormExportController()
            : this(new FormRepository())
        {

        }

        public FormExportController(FormRepository formRepo)
        {
            this._formRepo = formRepo;
        }

        // GET: FormExport
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ViewEntriesSubmit(string submitButton, IEnumerable<string> selectedEntries, TemplateViewModel model, FormCollection collection)
        {
            //Console.Write(submitButton);

            switch (submitButton)
            {
                case "Delete Selected":
                    // delegate sending to another controller action
                    return DeleteEntries(selectedEntries, model);
                case "Export to Excel":
                    // call another action to perform the cancellation
                    return ExportToExcel(model, collection);
                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteEntries(IEnumerable<string> selectedEntries, TemplateViewModel model)
        {
            // TODO This method not required?
            Template form;
            using (var formManager = new FormManager())
            {
                form = formManager.FindForm(model.TemplateID.Value);

                // var form = this._formRepo.GetForm(model.Id.Value);
                var formView = TemplateViewModel.CreateFromObject(form);

                try
                {
                    if (selectedEntries != null && selectedEntries.Any())
                    {
                        this._formRepo.DeleteEntries(selectedEntries);
                        TempData["success"] = "The selected entries were deleted";
                    }
                }
                catch
                {
                    TempData["error"] = "An error occured while deleting entries. Try again later.";
                }
            }

            return RedirectToRoute("form-entries", new { formid = model.TemplateID.Value });

        }

        public ActionResult ViewEntries(int formId)
        {
            // var form = this._formRepo.GetByPrimaryKey(formId);

            var form = this._formRepo.GetForm(formId);

            var formView = TemplateViewModel.CreateFromObject(form);

            formView.Entries = this._formRepo.GetRegistrantsByForm(formView).ToList();
            formView.GroupedEntries = formView.Entries.GroupBy(g => g.EntryId);

            return View(formView);
        }

        public ActionResult AddNewSortEntries(string formId)
        {
            var form = this._formRepo.GetForm(Int32.Parse(formId));

            var formView = TemplateViewModel.CreateFromObject(form);

            formView.Entries = this._formRepo.GetRegistrantsByForm(formView).ToList();
            formView.GroupedEntries = formView.Entries.GroupBy(g => g.EntryId);

            var sortFieldViewModel = new SortFieldViewModel();

            sortFieldViewModel.SortFields = new List<SelectListItem>();

            foreach (var s in formView.GroupedEntries.First())
            {
                sortFieldViewModel.SortFields.Add(new SelectListItem
                {
                    Text = s.FieldLabel.Limit(100),
                    Value = s.FieldLabel
                });
            }

            sortFieldViewModel.SortFields.Add(new SelectListItem
            {
                Text = "Submitted On",
                Value = "Submitted On"
            });

            return PartialView("_ViewEntriesSortPartial", sortFieldViewModel);
        }

        public ActionResult AddNewCriteriaEntries(string formId)
        {
            var form = this._formRepo.GetForm(Int32.Parse(formId));

            var formView = TemplateViewModel.CreateFromObject(form);

            formView.Entries = this._formRepo.GetRegistrantsByForm(formView).ToList();
            formView.GroupedEntries = formView.Entries.GroupBy(g => g.EntryId);

            var criteriaFieldViewModel = new CriteriaFieldViewModel();
            criteriaFieldViewModel.Fields = formView.Fields;
            criteriaFieldViewModel.GroupedEntries = formView.GroupedEntries;
            criteriaFieldViewModel.CriteriaSubFields = Enumerable.Empty<CriteriaSubFieldViewModel>().ToList();

            criteriaFieldViewModel.FieldLabels = new List<SelectListItem>();

            foreach (var s in formView.GroupedEntries.First())
            {
                criteriaFieldViewModel.FieldLabels.Add(new SelectListItem
                {
                    Text = s.FieldLabel.Limit(100),
                    Value = s.FieldLabel
                });
            }

            criteriaFieldViewModel.FieldLabels.Add(new SelectListItem
            {
                Text = "Submitted On",
                Value = "Submitted On"
            });

            return PartialView("_ViewEntriesCriteriaPartial", criteriaFieldViewModel);
        }

        public ActionResult AddNewCriteriaSubEntries(string formId)
        {
            var criteriaSubFieldViewModel = new CriteriaSubFieldViewModel();

            using (var formManager = new FormManager())
            {
                var form = formManager.FindForm(Int32.Parse(formId));

                //  var form = this._formRepo.GetForm(Int32.Parse(formId));

                var formView = TemplateViewModel.CreateFromObject(form);

                formView.Entries = formManager.HasSubmissions(formView).ToList();
                // formView.Entries = this._formRepo.GetRegistrantsByForm(formView).ToList();

                formView.GroupedEntries = formView.Entries.GroupBy(g => g.EntryId);

                criteriaSubFieldViewModel.Fields = formView.Fields;
                criteriaSubFieldViewModel.GroupedEntries = formView.GroupedEntries;
            }
            return PartialView("_ViewEntriesCriteriaSubPartial", criteriaSubFieldViewModel);
        }

        public ActionResult ExportToExcel(TemplateViewModel model, FormCollection collection)
        {
            int formId = model.TemplateID.Value;

            using (var formManager = new FormManager())
            {
                var form = formManager.FindForm(model.TemplateID.Value);
                // var form = this._formRepo.GetForm(formId);
                var formView = TemplateViewModel.CreateFromObject(form);

                // formView.Entries = formManager.HasSubmissions(formView).ToList();
                formView.Entries = this._formRepo.GetRegistrantsByForm(formView).ToList();

                formView.GroupedEntries = formView.Entries.GroupBy(g => g.EntryId);

                var gridView = new GridView();
                gridView.DataSource = this.CreateFormEntriesDataTable(formView, model.SortFields, model.CriteriaFields);
                gridView.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename={0}.xls".FormatWith(form.Title.ToSlug()));
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gridView.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }

            return RedirectToRoute("form-entries", new { formid = formId });

        }

        private DataTable CreateFormEntriesDataTable(TemplateViewModel form, List<SortFieldViewModel> sortFields, List<CriteriaFieldViewModel> criteriaFields)
        {
            var dt = new DataTable(form.Title);
            List<string> columnNames = new List<string>();
            int columnCount = 0;

            //TODO - Format Export Forms Data for BMI and Address
            foreach (var field in form.Fields)
            {

                if (field.FieldType == Constants.FieldType.MATRIX)
                {
                    string[] rows = field.MatrixRow.Split(",");
                    foreach (string row in rows)
                    {
                        dt.Columns.Add(new DataColumn(row));

                        columnCount++;
                    }
                }
                else if (field.FieldType == Constants.FieldType.ADDRESS)
                {
                    dt.Columns.Add(new DataColumn("Blk"));
                    dt.Columns.Add(new DataColumn("Unit"));
                    dt.Columns.Add(new DataColumn("Street Address"));
                    dt.Columns.Add(new DataColumn("ZipCode"));

                    columnCount += 4;

                }
                else if (field.FieldType == Constants.FieldType.BMI)
                {
                    dt.Columns.Add(new DataColumn("Weight"));
                    dt.Columns.Add(new DataColumn("Height"));
                    dt.Columns.Add(new DataColumn("BMI"));

                    columnCount += 3;
                }
                else
                {
                    var colName = field.Label;

                    if (dt.Columns.Contains(colName))
                    {
                        dt.Columns.Add(new DataColumn(columnCount + ": " + colName));
                    }
                    else
                    {
                        dt.Columns.Add(new DataColumn(colName));
                    }

                    columnCount++;
                }
            }

            // Comment out original

            //foreach (var field in form.GroupedEntries.FirstOrDefault())
            //{
            //    if (field.FieldType != Constants.FieldType.HEADER)
            //    {
            //        var colName = field.FieldLabel;
            //        if (columnNames.Any(cn => cn.IsTheSameAs(colName)))
            //        {
            //            int colNumber = 1;
            //            do
            //            {
            //                colName = string.Format("{0} ({1})", colName, colNumber);
            //                colNumber++;

            //            } while (columnNames.Any(cn => cn.IsTheSameAs(colName)));
            //        }
            //        columnNames.Add(colName);
            //        dt.Columns.Add(new DataColumn(colName));
            //    }
            //    columnCount++;
            //}

            dt.Columns.Add(new DataColumn("Submitted On"));

            foreach (var group in form.GroupedEntries)
            {
                DataRow row = dt.NewRow();
                var fieldAddedOn = group.FirstOrDefault().DateAdded;
                int columnIndex = 0;

                foreach (var entry in group)
                {
                    // for (columnIndex = 0; columnIndex < columnCount; )
                    //  {
                    if (entry.FieldType == Constants.FieldType.MATRIX)
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
                    else if (entry.FieldType == Constants.FieldType.ADDRESS)
                    {
                        var addressField = entry.Value;

                        AddressViewModel address = addressField.FromJson<AddressViewModel>();

                        row[columnIndex] = address.Blk;
                        row[columnIndex + 1] = address.Unit;
                        row[columnIndex + 2] = address.StreetAddress;
                        row[columnIndex + 3] = address.ZipCode;

                        columnIndex += 4;
                    }
                    else if (entry.FieldType == Constants.FieldType.BMI)
                    {
                        var bmiField = entry.Value;

                        BMIViewModel bmi = bmiField.FromJson<BMIViewModel>();

                        row[columnIndex] = bmi.Weight;
                        row[columnIndex + 1] = bmi.Height;
                        row[columnIndex + 2] = bmi.BodyMassIndex;

                        columnIndex += 3;
                    }
                    else if (columnIndex < group.Count())
                    {
                        var field = group.ElementAt(columnIndex);
                        row[columnIndex] = field.Format(true);
                        columnIndex++;
                    }
                    else
                    {

                        row[columnIndex] = entry.Value;
                        columnIndex++;
                    }
                    // }
                }

                row[columnIndex] = fieldAddedOn.ToString("yyyy-MM-dd HH:mm");
                dt.Rows.Add(row);
            }



            DataView dv = new DataView(dt);
            dv.RowFilter = GenerateFlitering(criteriaFields);
            dv.Sort = GenerateSorting(sortFields);

            return dv.ToTable();
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