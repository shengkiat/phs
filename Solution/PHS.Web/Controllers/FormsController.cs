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
            Template template;
            using (var formManager = new FormManager())
            {
                template = formManager.CreateNewFormAndTemplate();
            }

            return RedirectToAction("EditTemplate", new { id = template.TemplateID });
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
            Template template = new Template();
            using (var formManager = new FormManager())
            {
                 template = formManager.FindTemplate(id);
            }

            TemplateViewModel model1 = TemplateViewModel.CreateFromObject(template);

            return View(model1);
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
                    var template = manager.FindTemplate(model.TemplateID.Value);

                    // first update the form metadata
                    manager.UpdateTemplate(model, template);

                    // then if fields were passed in, update them
                    if (Fields.Count() > 0)
                    {
                        foreach (var kvp in Fields)
                        {

                            var domId = Convert.ToInt32(kvp.Key);
                            if (domId >= 0)
                            {
                                var fieldType = collection.FormFieldValue(domId, "FieldType");
                                var fieldId = collection.FormFieldValue(domId, "Id");
                                var minAge = collection.FormFieldValue(domId, "MinimumAge").IsInt(18);
                                var maxAge = collection.FormFieldValue(domId, "MaximumAge").IsInt(100);

                                if (minAge >= maxAge)
                                {
                                    minAge = 18;
                                    maxAge = 100;
                                }

                                var fieldTypeEnum = (Constants.TemplateFieldType)Enum.Parse(typeof(Constants.TemplateFieldType), fieldType.ToUpper());

                                var fieldView = new TemplateFieldViewModel
                                {
                                    DomId = Convert.ToInt32(domId),
                                    FieldType = fieldTypeEnum,
                                    MaxCharacters = collection.FormFieldValue(domId, "MaxCharacters").IsInt(),
                                    Text = collection.FormFieldValue(domId, "Text"),
                                    Label = collection.FormFieldValue(domId, "Label"),
                                    IsRequired = collection.FormFieldValue(domId, "IsRequired").IsBool(),
                                    Options = collection.FormFieldValue(domId, "Options"),
                                    SelectedOption = collection.FormFieldValue(domId, "SelectedOption"),
                                    AddOthersOption = collection.FormFieldValue(domId, "AddOthersOption").IsBool(),
                                    OthersOption = collection.FormFieldValue(domId, "OthersOption"),
                                    HoverText = collection.FormFieldValue(domId, "HoverText"),
                                    Hint = collection.FormFieldValue(domId, "Hint"),
                                    MinimumAge = minAge,
                                    MaximumAge = maxAge,
                                    HelpText = collection.FormFieldValue(domId, "HelpText"),
                                    SubLabel = collection.FormFieldValue(domId, "SubLabel"),
                                    Size = collection.FormFieldValue(domId, "Size"),
                                    Columns = collection.FormFieldValue(domId, "Columns").IsInt(20),
                                    Rows = collection.FormFieldValue(domId, "Columns").IsInt(2),
                                    Validation = collection.FormFieldValue(domId, "Validation"),
                                    Order = collection.FormFieldValue(domId, "Order").IsInt(1),
                                    MaxFileSize = collection.FormFieldValue(domId, "MaxFileSize").IsInt(5000),
                                    MinFileSize = collection.FormFieldValue(domId, "MinFileSize").IsInt(5),
                                    ValidFileExtensions = collection.FormFieldValue(domId, "ValidExtensions"),
                                    ImageBase64 = collection.FormFieldValue(domId, "ImageBase64"),
                                    MatrixColumn = collection.FormFieldValue(domId, "MatrixColumn"),
                                    MatrixRow = collection.FormFieldValue(domId, "MatrixRow")
                                };

                                if (!fieldId.IsNullOrEmpty() && fieldId.IsInteger())
                                {
                                    fieldView.TemplateFieldID = Convert.ToInt32(fieldId);
                                }

                                manager.UpdateTemplateFields(template, fieldView);

                            }
                        }
                    }

                    //  form.FormFields.Load();
                    var fieldOrderById = template.TemplateFields.Select(ff => new { domid = ff.DomId, id = ff.TemplateFieldID });

                    return Json(new { success = true, message = "Your changes were saved.", isautosave = isAutoSave, fieldids = fieldOrderById });
                }

            }
            catch
            {
                //TODO: log error
                // var error = "Unable to save form ".AppendIfDebugMode(ex.ToString());
                return Json(new { success = false, error = "Unable to save form ", isautosave = isAutoSave });
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

                return RedirectToAction("edit", new { id = template.TemplateID });
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
                    return RedirectToAction("edittemplate", new { id = template.TemplateID });
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
                }
                else
                {
                    return RedirectToAction("edit", new { id = template.TemplateID });
                }

                foreach (var field in model.Fields)
                {
                    field.EntryId = entryId;
                }

                return View("FillIn", model);
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

                return RedirectToAction("Index", "Error");
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

        public ActionResult PreRegistration(int id = -1)
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

            TemplateViewModel model = null;
            // var form = this._formRepo.GetByPrimaryKey(id);
            Template template;
            using (var formManager = new FormManager())
            {
                 template = formManager.FindTemplate(id);
            }

            if (template != null)
            {
                model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
            }
            else
            {
                return RedirectToAction("edittemplate", new { id = template.TemplateID });
            }

            return View(model);
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
