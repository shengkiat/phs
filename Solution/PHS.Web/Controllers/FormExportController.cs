using Novacode;
using PHS.Business.Common;
using PHS.Business.Extensions;
using PHS.Business.Implementation;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.FormBuilder.ViewModel;
using PHS.Repository.Context;
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
            : this(new FormRepository(new PHSContext()))
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
            Template template;
            using (var formManager = new FormManager())
            {
                template = formManager.FindTemplate(model.TemplateID.Value);

                // var form = this._formRepo.GetForm(model.Id.Value);
                var templateView = TemplateViewModel.CreateFromObject(template);

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

            return RedirectToRoute("form-entries", new { templateid = model.TemplateID.Value });

        }

        public ActionResult ViewEntries(int templateId)
        {
            // var form = this._formRepo.GetByPrimaryKey(formId);

            var template = this._formRepo.GetTemplate(templateId);

            var templateView = TemplateViewModel.CreateFromObject(template);

            templateView.Entries = this._formRepo.GetTemplateFieldValuesByForm(templateView).ToList();
            templateView.GroupedEntries = templateView.Entries.GroupBy(g => g.EntryId);

            return View(templateView);
        }

        public ActionResult AddNewSortEntries(string templateId)
        {
            var template = this._formRepo.GetTemplate(Int32.Parse(templateId));

            var templateView = TemplateViewModel.CreateFromObject(template);

            templateView.Entries = this._formRepo.GetTemplateFieldValuesByForm(templateView).ToList();
            templateView.GroupedEntries = templateView.Entries.GroupBy(g => g.EntryId);

            var sortFieldViewModel = new SortFieldViewModel();

            sortFieldViewModel.SortFields = new List<SelectListItem>();

            foreach (var s in templateView.GroupedEntries.First())
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

        public ActionResult AddNewCriteriaEntries(string templateId)
        {
            var template = this._formRepo.GetTemplate(Int32.Parse(templateId));

            var templateView = TemplateViewModel.CreateFromObject(template);

            templateView.Entries = this._formRepo.GetTemplateFieldValuesByForm(templateView).ToList();
            templateView.GroupedEntries = templateView.Entries.GroupBy(g => g.EntryId);

            var criteriaFieldViewModel = new CriteriaFieldViewModel();
            criteriaFieldViewModel.Fields = templateView.Fields;
            criteriaFieldViewModel.GroupedEntries = templateView.GroupedEntries;
            criteriaFieldViewModel.CriteriaSubFields = Enumerable.Empty<CriteriaSubFieldViewModel>().ToList();

            criteriaFieldViewModel.FieldLabels = new List<SelectListItem>();

            foreach (var s in templateView.GroupedEntries.First())
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

        public ActionResult AddNewCriteriaSubEntries(string templateId)
        {
            var criteriaSubFieldViewModel = new CriteriaSubFieldViewModel();

            using (var formManager = new FormManager())
            {
                var template = formManager.FindTemplate(Int32.Parse(templateId));

                //  var form = this._formRepo.GetForm(Int32.Parse(formId));

                var templateView = TemplateViewModel.CreateFromObject(template);

                templateView.Entries = formManager.HasSubmissions(templateView).ToList();
                // formView.Entries = this._formRepo.GetRegistrantsByForm(formView).ToList();

                templateView.GroupedEntries = templateView.Entries.GroupBy(g => g.EntryId);

                criteriaSubFieldViewModel.Fields = templateView.Fields;
                criteriaSubFieldViewModel.GroupedEntries = templateView.GroupedEntries;
            }
            return PartialView("_ViewEntriesCriteriaSubPartial", criteriaSubFieldViewModel);
        }

        public ActionResult ExportToExcel(TemplateViewModel model, FormCollection collection)
        {
            int formId = model.TemplateID.Value;

            using (var formExportManager = new FormExportManager())
            {
                var gridView = new GridView();
                gridView.DataSource = formExportManager.CreateFormEntriesDataTable(model);
                gridView.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename={0}.xls".FormatWith(model.Title.ToSlug()));
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gridView.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }

            return RedirectToRoute("form-entries", new { formid = formId });

        }

        
    }
}