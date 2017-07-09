﻿using Novacode;
using PHS.Business.Common;
using PHS.Business.Extensions;
using PHS.Business.Implementation;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.FormBuilder.ViewModel;
using PHS.Repository.Context;
using PHS.Repository.Repository;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PHS.Web.Controllers
{
    [ValidateInput(false)]
    [CustomAuthorize(Roles = Constants.User_Role_Admin_Code)]
    public class FormController : BaseController
    {

        public ActionResult Index()
        {
            var formCollectionView = new FormCollectionViewModel();

            using (var formManager = new FormManager())
            {
                formCollectionView.Forms = formManager.FindAllFormsByDes();
            }

            return View(formCollectionView);
        }

        public ActionResult ViewTemplate(int formId)
        {
            var templateCollectionView = new TemplateCollectionViewModel();

            using (var formManager = new FormManager())
            {
                templateCollectionView.Templates = formManager.FindAllTemplatesByFormId(formId);
            }

            return View(templateCollectionView);
        }

        public ActionResult CreateForm()
        {
            FormViewModel model1 = FormViewModel.Initialize();
            return View(model1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForm(FormViewModel formViewModel)
        {
            using (var formManager = new FormManager())
            {
                try
                {
                    Template template = formManager.CreateNewFormAndTemplate(formViewModel);
                    if (template != null)
                    {
                        return RedirectToAction("editTemplate", new { id = template.TemplateID });
                    }

                    else
                    {
                        TempData["error"] = "Error creating new form";
                        return View();
                    }
                }

                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View();
                }

            }
        }

        public ActionResult EditForm(int id)
        {
            using (var formManager = new FormManager())
            {
                FormViewModel model1 = formManager.FindFormToEdit(id);
                if (model1 == null)
                {
                    return RedirectToError("Invalid id");
                }

                else
                {
                    return View(model1);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForm(FormViewModel formViewModel)
        {
            using (var formManager = new FormManager())
            {
                try
                {
                    string result = formManager.EditForm(formViewModel);

                    if (result.Equals("success"))
                    {
                        return RedirectToAction("Index");
                    }

                    else
                    {
                        TempData["error"] = result;
                        return View(formViewModel);
                    }

                }

                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View(formViewModel);
                }
            }
        }

        public ActionResult DeleteForm(int formId)
        {
            using (var formManager = new FormManager())
            {
                string result = formManager.DeleteFormAndTemplate(formId);

                if (result.Equals("success"))
                {
                    TempData["success"] = "Form and Template Deleted";
                    return RedirectToRoute("form-home");
                }

                else
                {
                    TempData["error"] = result;
                    return RedirectToRoute("form-home");
                }
            }
        }

        

        public ActionResult EditTemplate(int id)
        {
            using (var formManager = new FormManager())
            {
                TemplateViewModel model1 = formManager.FindTemplateToEdit(id);
                if (model1 == null)
                {
                    return RedirectToError("Invalid id");
                }

                else
                {
                    return View(model1);
                }
                
            }
        }

        [HttpPost]
        public ActionResult UpdateTemplate(bool isAutoSave, TemplateViewModel model, FormCollection collection, IDictionary<string, string> Fields)
        {

            if (!model.TemplateID.HasValue)
            {
                return Json(new { success = false, error = "Unable to save changes. A valid form was not detected.", isautosave = isAutoSave });
            }

            if (Fields == null)
            {
                return Json(new { success = false, error = "Unable to detect field values.", isautosave = isAutoSave });
            }

            if (!model.NotificationEmail.IsNullOrEmpty() && !model.NotificationEmail.IsValidEmail())
            {
                return Json(new { success = false, error = "Invalid format for Notification Email.", isautosave = isAutoSave });
            }

            try
            {
                using (var manager = new FormManager())
                {
                    manager.UpdateTemplate(model, collection, Fields);

                    var template = manager.FindTemplate(model.TemplateID.Value);

                    var fieldOrderById = template.TemplateFields.Select(ff => new { domid = ff.DomId, id = ff.TemplateFieldID });

                    return Json(new { success = true, message = "Your changes were saved.", isautosave = isAutoSave, fieldids = fieldOrderById });
                }

            }
            catch
            {
                //TODO: log error
                // var error = "Unable to save form ".AppendIfDebugMode(ex.ToString());
                return Json(new { success = false, error = "Unable to save template ", isautosave = isAutoSave });
            }

        }

        public ActionResult DeleteTemplate(int formId, int templateId)
        {
            using (var formManager = new FormManager())
            {
                string result = formManager.DeleteTemplate(templateId);

                if (result.Equals("success"))
                {
                    TempData["success"] = "Template Deleted";
                    return RedirectToRoute("form-viewtemplate", new { formId = formId });
                }

                else
                {
                    TempData["error"] = result;
                    return RedirectToRoute("form-viewtemplate", new { formId = formId });
                }
                
            }
        }

        [HttpPost]
        public ActionResult DeleteTemplateField(int? fieldid)
        {
            if (fieldid.HasValue)
            {
                using (var formManager = new FormManager())
                {
                    formManager.DeleteTemplateField(fieldid.Value);
                    return Json(new { success = true, message = "Field was deleted." });
                }
            }

            return Json(new { success = false, message = "Unable to delete field." });
        }


        public ActionResult TogglePublish(bool toOn, int id)
        {
            using (var formManager = new FormManager())
            {
                // var form = this._formRepo.GetByPrimaryKey(id);
                var template = formManager.FindTemplate(id);

                if (template.TemplateFields.Count() > 0)
                {
                    template.Status = toOn ? Constants.TemplateStatus.PUBLISHED.ToString() : Constants.TemplateStatus.DRAFT.ToString();

                    //this._formRepo.SaveChanges();
                    if (toOn)
                    {
                        TempData["success"] = "This form has been published and is now live";
                    }
                    else
                    {
                        TempData["success"] = "This form is now offline";
                    }
                }
                else
                {
                    TempData["error"] = "Cannot publish form until fields have been added.";
                }

                return RedirectToAction("editTemplate", new { id = template.TemplateID });
            }
        }

        //[SSl]
        

        public ActionResult ViewSaveTemplate(int id, string entryId, bool embed = false)
        {
            using (var formManager = new FormManager())
            {
                TemplateViewModel model = null;

                var template = formManager.FindTemplate(id);

                if (template != null)
                {
                    model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
                    model.Embed = embed;

                    foreach (var field in model.Fields)
                    {
                        field.EntryId = entryId;
                    }

                    return View("FillIn", model);
                }

                else
                {
                    return RedirectToError("invalid id");
                }
            }
        }

        
        public ActionResult PreviewTemplate(int id)
        {
            if (!IsUserAuthenticated())
            {
                //TODO - View Form Authentication
                // return RedirectToLogin();
            }

            using (var formManager = new FormManager())
            {
                Template template = formManager.FindTemplate(id);

                if (template != null)
                {
                    TemplateViewModel model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
                    return View(model);
                }
                else
                {
                    return RedirectToError("invalid id");
                }
            }

            
        }

        
        public ActionResult Error()
        {
            return View();
        }

    }
}
