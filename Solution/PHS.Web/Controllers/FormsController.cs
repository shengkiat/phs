using Novacode;
using PHS.Business.Common;
using PHS.Business.Extensions;
using PHS.Business.Implementation;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Forms;
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
    [ValidateInput(false)]
    public class FormsController : BaseController
    {
        // GET: /Forms/
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

        public ActionResult GenerateDoctorMemo(string text)
        {

            if (text == null)
            {
                text = "";
            }

            String guid = Guid.NewGuid().ToString();
            string templatePath = Server.MapPath("~/App_Data/Doctor's Memo Template.docx");

            // Load template into memory
            var doc = DocX.Load(templatePath);

            doc.ReplaceText("Replaced", text);

            var ms = new MemoryStream();
            doc.SaveAs(ms);
            ms.Position = 0;
            byte[] fileBytes = ms.ToArray();

            TempData[guid] = fileBytes;

            return new JsonResult()
            {
                Data = new { FileGuid = guid, FileName = "Doctor's Memo.docx" }
            };
        }

        [HttpGet]
        public ActionResult DownloadDoctorMemo(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.ms-excel", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
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

        public ActionResult GetAddressByZipCode(string zipcode)
        {
            int postalCode = 0;

            if (!Int32.TryParse(zipcode, out postalCode))
            {
                // not int
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Error = "Invalid" });
            }

            using (var addressManager = new AddressManager())
            {
                var address = addressManager.FindAddress(zipcode);

                if (address == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Error = "Invalid" });
                }

                return Json(new { Blk = address.StreetNumber, Street = address.StreetName });
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

        public void InsertValuesIntoTempData(IDictionary<string, string> submittedValues, FormCollection formCollection)
        {
            foreach (var key in formCollection.AllKeys)
            {
                ViewData[key.ToLower()] = formCollection[key];
            }

        }

        //[SSl]
        public ActionResult PublicFillIn(string slug, bool embed = false)
        {
            using (var formManager = new FormManager())
            {
                var template = formManager.FindPublicTemplate(slug);

                if (template != null)
                {
                    TemplateViewModel model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
                    model.Embed = embed;
                    return View(model);
                }
                else
                {
                    return RedirectToError("Unable to find any form");
                }

               
            }
        }

        //[SSl]
        public ActionResult FillIn(int id, bool embed = false)
        {
            using (var formManager = new FormManager())
            {
                TemplateViewModel model = null;
                // var form = this._formRepo.GetByPrimaryKey(id);

                var template = formManager.FindTemplate(id);

                if (template != null)
                {
                    model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
                    model.Embed = embed;
                }
                else
                {
                    return RedirectToError("invalid id");
                }

                return View(model);
            }
        }

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

        [HttpPost]
        public ActionResult FillIn(IDictionary<string, string> SubmitFields, TemplateViewModel model, FormCollection formCollection)
        {
            using (var formManager = new FormManager())
            {
               
                var template = formManager.FindTemplate(model.TemplateID.Value);

                var templateView = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
                string result = formManager.FillIn(SubmitFields, model, formCollection);

                if (result.Equals("success"))
                {
                    //send notification
                    if (!templateView.NotificationEmail.IsNullOrEmpty() && WebConfig.Get<bool>("enablenotifications", true))
                    {
                        var notificationView = new NotificationEmailViewModel();
                        notificationView.FormName = templateView.Title;
                        notificationView.Email = templateView.NotificationEmail;

                        //TODO if need to use this, need to retrieve the entries
                        //notificationView.Entries = ??;

                        NotifyViaEmail(notificationView);
                    }

                    TempData["success"] = templateView.ConfirmationMessage;
                    return RedirectToRoute("form-submitconfirmation", new
                    {
                        id = template.TemplateID,
                        embed = model.Embed
                    });
                }
                
                else
                {
                    TempData["error"] = result;
                    return View("FillIn", templateView);
                }
            }
        }

        public ActionResult SubmitConfirmation(int id, bool? embed)
        {
            using (var formManager = new FormManager())
            {
                var template = formManager.FindTemplate(id);
                if (template != null)
                {
                    var templateView = TemplateViewModel.CreateFromObject(template);
                    templateView.Embed = embed ?? embed.Value;
                    TempData["success"] = templateView.ConfirmationMessage;
                    return View(templateView);
                }

                return RedirectToError("invalid id");
            }
        }

        private void NotifyViaEmail(NotificationEmailViewModel model)
        {
            EmailSender emailSender = new EmailSender("MailTemplates", false);
            var submimssionDetail = this.RenderPartialViewToString("_SubmissionEmailPartial", model);
            emailSender.SendSubmissionNotificationEmail(model.Email, "New Submission for form \"{0}\"".FormatWith(model.FormName), submimssionDetail);
        }

        private void SaveImageToCloud(HttpPostedFileBase file, string fileName)
        {

        }

        //public FileStreamResult GetFileFromDisk(int valueId)
        //{
        //    FileValueObject obj = _formRepo.GetFileFieldValue(valueId);
        //    if (obj != null)
        //    {
        //        if (obj.IsSavedInCloud)
        //        {

        //        }
        //        else
        //        {
        //            var filePath = Server.MapPath(obj.SavePath.ConcatWith("/", obj.SaveName));
        //            var stream = UtilityHelper.ReadFile(filePath);
        //            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, obj.FileName);
        //        }
        //    }

        //    throw new Exception("File Not Found");
        //}

        public ActionResult PreRegistration()
        {

            using (var formManager = new FormManager())
            {
                TemplateViewModel model = null;

                var template = formManager.FindPreRegistrationForm();

                if (template != null)
                {
                    model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);

                }
                else
                {
                    throw new HttpException(404, "No Pre Registration Form found");
                }

                return View("PreRegistration", model);
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

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
