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
    [ValidateInput(false)]
    public class FormsController : BaseController
    {
        //
        // GET: /Forms/
        private FormRepository _formRepo { get; set; }

        public FormsController()
            : this(new FormRepository())
        {

        }

        public FormsController(FormRepository formRepo)
        {
            this._formRepo = formRepo;
        }

        public ActionResult Index()
        {
            var formCollectionView = new TemplateCollectionViewModel();

            using (var formManager = new FormManager())
            {
                formCollectionView.Forms = formManager.FindAllFormsByDes();
            }

            return View(formCollectionView);
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


        public ActionResult Edit(int id)
        {
            Template form = new Template();
            using (var formManager = new FormManager())
            {
                 form = formManager.FindForm(id);
            }

            TemplateViewModel model1 = TemplateViewModel.CreateFromObject(form);

            return View(model1);
        }

        public ActionResult Create()
        {
            Template form;
            using (var formManager = new FormManager())
            {
                form = formManager.CreateNewForm();
            }

            return RedirectToAction("edit", new { id = form.ID });
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
        public ActionResult Update(bool isAutoSave, TemplateViewModel model, FormCollection collection, IDictionary<string, string> Fields)
        {

            if (!model.Id.HasValue)
            {
                return Json(new { success = false, error = "Unable to save changes. A valid form was not detected.", isautosave = isAutoSave });
            }

            var form = this._formRepo.GetByPrimaryKey(model.Id.Value);


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
                // first update the form metadata
                this._formRepo.Update(model, form);

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

                            var fieldTypeEnum = (Constants.FieldType)Enum.Parse(typeof(Constants.FieldType), fieldType.ToUpper());

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
                                fieldView.Id = Convert.ToInt32(fieldId);
                            }

                            this._formRepo.UpdateField(form, fieldView);
                        }
                    }
                }

                //  form.FormFields.Load();
                var fieldOrderById = form.FormFields.Select(ff => new { domid = ff.DomId, id = ff.ID });

                return Json(new { success = true, message = "Your changes were saved.", isautosave = isAutoSave, fieldids = fieldOrderById });

            }
            catch
            {
                //TODO: log error
                // var error = "Unable to save form ".AppendIfDebugMode(ex.ToString());
                return Json(new { success = false, error = "Unable to save form ", isautosave = isAutoSave });
            }

        }

        public ActionResult Delete(int formId)
        {
            using (var formManager = new FormManager())
            {
                var form = formManager.FindForm(formId);

                var formView = TemplateViewModel.CreateFromObject(form);

                if (form != null)
                {
                    formView.Entries = formManager.HasSubmissions(formView).ToList();

                    if (!formView.Entries.Any())
                    {
                        try
                        {
                            formManager.DeleteForm(formId);
                            TempData["success"] = "Form Deleted";
                            return RedirectToRoute("form-home");
                        }
                        catch
                        {
                            TempData["error"] = "Unable to delete form - Forms must have no entries to be able to be deleted";
                        }
                    }
                }
            }

            TempData["error"] = "Unable to delete form - Forms must have no entries to be able to be deleted";
            return RedirectToRoute("form-home");
        }

        [HttpPost]
        public ActionResult DeleteField(int? fieldid)
        {
            if (fieldid.HasValue)
            {
                this._formRepo.DeleteField(fieldid.Value);
                return Json(new { success = true, message = "Field was deleted." });
            }

            return Json(new { success = false, message = "Unable to delete field." });
        }

        public ActionResult TogglePublish(bool toOn, int id)
        {
            // var form = this._formRepo.GetByPrimaryKey(id);
            var form = this._formRepo.GetForm(id);

            if (form.FormFields.Count() > 0)
            {
                form.Status = toOn ? Constants.FormStatus.PUBLISHED.ToString() : Constants.FormStatus.DRAFT.ToString();

                this._formRepo.SaveChanges();
                if (toOn)
                {
                    TempData["success"] = "This registration form has been published and is now live";
                }
                else
                {
                    TempData["success"] = "This registration form is now offline";
                }
            }
            else
            {
                TempData["error"] = "Cannot publish form until fields have been added.";
            }

            return RedirectToAction("edit", new { id = form.ID });
        }

        public void InsertValuesIntoTempData(IDictionary<string, string> submittedValues, FormCollection form)
        {
            foreach (var key in form.AllKeys)
            {
                ViewData[key.ToLower()] = form[key];
            }

        }

        //[SSl]
        public ActionResult Register(int id, bool embed = false)
        {
            TemplateViewModel model = null;
            // var form = this._formRepo.GetByPrimaryKey(id);

            var form = this._formRepo.GetForm(id);

            if (form != null)
            {
                model = TemplateViewModel.CreateFromObject(form, Constants.FormFieldMode.INPUT);
                model.Embed = embed;
            }
            else
            {
                return RedirectToAction("edit", new { id = form.ID });
            }

            return View(model);
        }

        public ActionResult ViewSaveForm(int id, string entryId, bool embed = false)
        {
            TemplateViewModel model = null;

            var form = this._formRepo.GetForm(id);

            if (form != null)
            {
                model = TemplateViewModel.CreateFromObject(form, Constants.FormFieldMode.INPUT);
                model.Embed = embed;
            }
            else
            {
                return RedirectToAction("edit", new { id = form.ID });
            }

            foreach (var field in model.Fields)
            {
                field.EntryId = entryId;
            }

            return View("Register", model);
        }

        [HttpPost]
        public ActionResult Register(IDictionary<string, string> SubmitFields, TemplateViewModel model, FormCollection form)
        {
            IList<string> errors = Enumerable.Empty<string>().ToList();
            //var formObj = this._formRepo.GetByPrimaryKey(model.Id.Value);

            var formObj = this._formRepo.GetForm(model.Id.Value);

            var formView = TemplateViewModel.CreateFromObject(formObj, Constants.FormFieldMode.INPUT);
            formView.AssignInputValues(form);
            this.InsertValuesIntoTempData(SubmitFields, form);

            if (formView.Fields.Any())
            {
                // first validate fields
                foreach (var field in formView.Fields)
                {
                    if (!field.SubmittedValueIsValid(form))
                    {
                        field.SetFieldErrors();
                        errors.Add(field.Errors);
                        goto Error;
                    }

                    var value = field.SubmittedValue(form);
                    if (field.IsRequired && value.IsNullOrEmpty())
                    {
                        field.Errors = "{0} is a required field".FormatWith(field.Label);
                        errors.Add(field.Errors);
                        goto Error;
                    }
                };

                //then insert values
                var entryId = Guid.NewGuid();
                var notificationView = new NotificationEmailViewModel();
                notificationView.FormName = formView.Title;
                IDictionary<string, TemplateFieldValueViewModel> notificationEntries = new Dictionary<string, TemplateFieldValueViewModel>();
                foreach (var field in formView.Fields)
                {
                    var value = field.SubmittedValue(form);

                    //if it's a file, save it to hard drive
                    if (field.FieldType == Constants.FieldType.FILEPICKER && !string.IsNullOrEmpty(value))
                    {
                        //var file = Request.Files[field.SubmittedFieldName()];
                        //var fileValueObject = value.GetFileValueFromJsonObject();

                        //if (fileValueObject != null)
                        //{
                        //    if (UtilityHelper.UseCloudStorage())
                        //    {
                        //        this.SaveImageToCloud(file, fileValueObject.SaveName);
                        //    }
                        //    else
                        //    {
                        //        file.SaveAs(Path.Combine(HostingEnvironment.MapPath(fileValueObject.SavePath), fileValueObject.SaveName));
                        //    }
                        //}
                    }

                    this.AddValueToDictionary(ref notificationEntries, field.Label, new TemplateFieldValueViewModel(field.FieldType, value));
                    notificationView.Entries = notificationEntries;
                    this._formRepo.InsertFieldValue(field, value, entryId);
                }

                //send notification
                if (!formView.NotificationEmail.IsNullOrEmpty() && WebConfig.Get<bool>("enablenotifications", true))
                {
                    notificationView.Email = formView.NotificationEmail;
                    this.NotifyViaEmail(notificationView);
                }

                TempData["success"] = formView.ConfirmationMessage;
                return RedirectToRoute("form-confirmation", new
                {
                    id = formObj.ID,
                    embed = model.Embed
                });

            }

        Error:
            TempData["error"] = errors.ToUnorderedList();
            return View("Register", formView);
        }

        public ActionResult FormConfirmation(int id, bool? embed)
        {

            var form = this._formRepo.GetByPrimaryKey(id);
            if (form != null)
            {
                var formView = TemplateViewModel.CreateFromObject(form);
                formView.Embed = embed ?? embed.Value;
                TempData["success"] = formView.ConfirmationMessage;
                return View(formView);
            }

            return RedirectToAction("Index", "Error");
        }

        private void NotifyViaEmail(NotificationEmailViewModel model)
        {
            EmailSender emailSender = new EmailSender("MailTemplates", false);
            var submimssionDetail = this.RenderPartialViewToString("_SubmissionEmailPartial", model);
            emailSender.SendSubmissionNotificationEmail(model.Email, "New Submission for form \"{0}\"".FormatWith(model.FormName), submimssionDetail);
        }


        private void AddValueToDictionary(ref IDictionary<string, TemplateFieldValueViewModel> collection, string key, TemplateFieldValueViewModel value)
        {
            if (collection.ContainsKey(key))
            {
                var newKey = "";
                int counter = 2;
                do
                {
                    newKey = "{1} {0}".FormatWith(key, counter);
                    counter++;

                } while (collection.ContainsKey(newKey));

                collection.Add(newKey, value);
            }
            else
            {
                collection.Add(key, value);
            }
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

            TemplateViewModel model = null;

            var form = this._formRepo.GetPreRegistrationForm();

            if (form != null)
            {
                model = TemplateViewModel.CreateFromObject(form, Constants.FormFieldMode.INPUT);

            }
            else
            {
                throw new HttpException(404, "Some description");
            }

            return View("PreRegistration", model);
        }

        public ActionResult Preview(int id)
        {
            if (!IsUserAuthenticated())
            {
                //TODO - View Form Authentication
                // return RedirectToLogin();
            }

            TemplateViewModel model = null;
            // var form = this._formRepo.GetByPrimaryKey(id);
            Template form;
            using (var formManager = new FormManager())
            {
                 form = formManager.FindForm(id);
            }

            if (form != null)
            {
                model = TemplateViewModel.CreateFromObject(form, Constants.FormFieldMode.INPUT);
            }
            else
            {
                return RedirectToAction("edit", new { id = form.ID });
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
