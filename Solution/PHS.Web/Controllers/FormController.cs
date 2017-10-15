using Novacode;
using PHS.Business.Common;
using PHS.Business.Extensions;
using PHS.Business.Implementation;
using PHS.Business.ViewModel.Event;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels;
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
    [CustomAuthorize(Roles = Constants.User_Role_CommitteeMember_Code)]
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
        public ActionResult CreateForm([Bind(Include = "Title,Slug,IsPublic,PublicFormType,InternalFormType")] FormViewModel formViewModel)
        {
            using (var formManager = new FormManager(GetLoginUser()))
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
        public ActionResult EditForm([Bind(Include = "FormID,Title,Slug,IsPublic,PublicFormType,InternalFormType")] FormViewModel formViewModel)
        {
            using (var formManager = new FormManager(GetLoginUser()))
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
            using (var formManager = new FormManager(GetLoginUser()))
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

                    if (model1.TemplateID == id)
                    {
                        return View(model1);
                    }

                    else
                    {
                        return RedirectToAction("editTemplate", new { id = model1.TemplateID });
                    }
                    
                }

            }
        }

        [HttpPost]
        public ActionResult UpdateTemplate(bool isAutoSave, [Bind(Include = "TemplateID,NotificationEmail,Status,ConfirmationMessage,Theme,IsQuestion")]TemplateViewModel model, FormCollection collection, IDictionary<string, string> Fields)
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
                using (var manager = new FormManager(GetLoginUser()))
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
            using (var formManager = new FormManager(GetLoginUser()))
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

        public ActionResult EditModalityForm(int eventid, int modalityid)
        {
            using (var formManager = new FormManager())
            {
                ModalityFormViewModel model = new ModalityFormViewModel();


                model.ModalityFormList = formManager.FindModalityForm(modalityid);
                model.ModalityID = modalityid;
                model.EventID = eventid;

                using (var modalityManager = new ModalityManager(GetLoginUser()))
                {
                    string message = string.Empty;
                    Modality modality = modalityManager.GetModalityByID(modalityid, out message);
                    model.ModalityName = modality.Name;
                }

                using (var eventManager = new EventManager())
                {
                    string message = string.Empty;
                    PHSEvent phsEvent = eventManager.GetEventByID(eventid, out message);
                    model.EventName = phsEvent.Title; 
                }


                    return View(model);              
            }
            
        }

        public ActionResult AddModalityForm(int formID, int modalityID, int eventID)
        {
            using (var formManager = new FormManager(GetLoginUser()))
            {
                formManager.AddModalityForm(formID, modalityID, eventID);
                return RedirectToAction("EditModalityForm", new { eventid = eventID, modalityid = modalityID });
            }            
        }

        public ActionResult RemoveModalityForm(int formID, int modalityID, int eventID)
        {
            using (var formManager = new FormManager(GetLoginUser()))
            {
                formManager.RemoveModalityForm(formID, modalityID, eventID);
                return RedirectToAction("EditModalityForm", new { eventid = eventID, modalityid = modalityID });
            }
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
