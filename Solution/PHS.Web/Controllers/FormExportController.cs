using Novacode;
using PHS.Business.Common;
using PHS.Business.Extensions;
using PHS.Business.Implementation;
using PHS.Business.ViewModel.FormExport;
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

        

        /*
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
        */

        public ActionResult Export(int eventid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            using (var formExportManager = new FormExportManager())
            {
                string message = string.Empty;

                FormExportViewModel formExportViewModel = formExportManager.RetrieveAllForms(eventid, out message);
                if (formExportViewModel == null)
                {
                    SetViewBagError(message);
                }

                return View(formExportViewModel);
            };
        }

        [HttpPost]
        public ActionResult Export(FormExportViewModel model, FormCollection collection)
        {
            using (var formExportManager = new FormExportManager())
            {
                var formExportResult = formExportManager.CreateFormEntriesDataTable(model);

                var gridView = new GridView();
                gridView.DataSource = formExportResult.ValuesDataTable;
                gridView.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename={0}.xls".FormatWith(formExportResult.Title.ToSlug()));
                //Response.ContentType = "application/vnd.ms-excel";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gridView.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }

            return RedirectToRoute("form-export", new { eventId = model.EventID });
        }

        public ActionResult AddNewSortEntries(string formId)
        {
            using (var formExportManager = new FormExportManager())
            {
                return PartialView("_ViewEntriesSortPartial", formExportManager.AddNewSortEntries(Int32.Parse(formId)));
            }
        }

        public ActionResult AddNewCriteriaEntries(string formId)
        {
            using (var formExportManager = new FormExportManager())
            {
                return PartialView("_ViewEntriesCriteriaPartial", formExportManager.AddNewCriteriaEntries(Int32.Parse(formId)));
            }
        }

        public ActionResult AddNewCriteriaSubEntries(string formId)
        {
            using (var formExportManager = new FormExportManager())
            {
                return PartialView("_ViewEntriesCriteriaSubPartial", formExportManager.AddNewCriteriaSubEntries(Int32.Parse(formId)));
            }
        }

        

        
    }
}