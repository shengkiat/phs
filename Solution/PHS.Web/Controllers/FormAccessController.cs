using Novacode;
using PHS.Business.Common;
using PHS.Business.Implementation;
using PHS.Common;
using PHS.DB.ViewModels.Form;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PHS.Business.Extensions;
using PHS.Business.ViewModel.ParticipantJourney;
using static PHS.Common.Constants;
using PHS.Business.Helpers;
using PHS.DB;
using Spire.Doc;

namespace PHS.Web.Controllers
{
    public class FormAccessController : BaseController
    {
        //[SSl]
        public ActionResult PublicFillIn(string slug, bool embed = false)
        {
            using (var formManager = new FormAccessManager(GetLoginUser()))
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
                    throw new HttpException(404, "No public Form found");
                }
            }
        }

        public ActionResult PreRegistration()
        {

            using (var formManager = new FormAccessManager(GetLoginUser()))
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

        public ActionResult FillIn(int id, bool embed = false)
        {
            using (var formManager = new FormAccessManager(GetLoginUser()))
            {
                TemplateViewModel model = null;

                var template = formManager.FindTemplate(id);

                if (template != null)
                {
                    model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);

                    if (!model.IsPublic && !IsUserAuthenticated())
                    {
                        return RedirectToError("Unauthorized access");
                    }

                    model.Embed = embed;
                }
                else
                {
                    return RedirectToError("invalid id");
                }

                return View(model);
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


        [HttpPost]
        public ActionResult FillIn(IDictionary<string, string> SubmitFields, TemplateViewModel model, FormCollection formCollection)
        {
            InsertValuesIntoTempData(SubmitFields, formCollection);

            using (var formManager = new FormAccessManager(GetLoginUser()))
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

                    RemoveValuesFromTempData(formCollection);

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
            using (var formManager = new FormAccessManager(GetLoginUser()))
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

        private string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }
            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
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

        public ActionResult ViewSaveForm(int id, string entryId, bool embed = false)
        {
            using (var formManager = new FormAccessManager(GetLoginUser()))
            {
                TemplateViewModel model = null;

                var template = formManager.FindLatestTemplate(id);

                if (template != null)
                {
                    model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
                    model.Embed = embed;
                }
                else
                {
                    return RedirectToError("invalid id");
                }

                foreach (var field in model.Fields)
                {
                    field.EntryId = entryId;
                }

                return View("FillIn", model);
            }
        }


        public ActionResult GetAddressByZipCode(string zipcode)
        {
            //int postalCode = 0;

           /* if (!Int32.TryParse(zipcode, out postalCode))
            {
                // not int
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Error = "Invalid" });
            }
            */

            if (string.IsNullOrEmpty(zipcode))
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { Error = "Invalid" });
            }

            using (var addressManager = new AddressManager())
            {
                var address = addressManager.FindAddress(zipcode);

                if (address == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Error = "Invalid" });
                }

                return Json(new { Blk = address.StreetNumber, Street = address.StreetName });
            }
        }

        public ActionResult GetReferenceRange(int standardReferenceId, string value)
        {
            string message = string.Empty;
            using (var stdReferenceManager = new StandardReferenceManager())
            {
                var referenceRange = stdReferenceManager.GetReferenceRange(standardReferenceId, value, out message);

                if (referenceRange == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Error = message });
                }

                return Json(new { Status = referenceRange.Result, Highlight = referenceRange.Highlight });
            }
        }

        private void InsertValuesIntoTempData(IDictionary<string, string> submittedValues, FormCollection formCollection)
        {
            foreach (var key in formCollection.AllKeys)
            {
                ViewData[key.ToLower()] = formCollection[key];
            }
        }

        private void RemoveValuesFromTempData(FormCollection formCollection)
        {
            foreach (var key in formCollection.AllKeys)
            {
                ViewData.Remove(key.ToLower());
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
            
            doc.ReplaceText("<<Replaced>>", text);

            PHSUser loginUser = GetLoginUser();
            doc.ReplaceText("<<NAME>>", loginUser.FullName);

            if(loginUser.MCRNo != null)
            {
                doc.ReplaceText("<<MCRNo>>", loginUser.MCRNo); 
            }
            else
            {
                doc.ReplaceText("<<MCRNo>>", "");
            }

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

        public ActionResult GenerateLabel()
        {
            string templatePath = Server.MapPath("~/App_Data/Label.docx");
            
            // Load template into memory
            var doc = DocX.Load(templatePath);

            ParticipantJourneySearchViewModel psm = (ParticipantJourneySearchViewModel)TempData.Peek("ParticipantJourneySearchViewModel");

            using (var participantJourneyManager = new ParticipantJourneyManager())
            {
                string message = string.Empty;
                MessageType messageType = MessageType.ERROR;

                ParticipantJourneyViewModel result = participantJourneyManager.RetrieveParticipantJourney(psm, out message, out messageType);

                doc.ReplaceText("<<EVENT>>", result.Event.Title + " " + result.Event.Venue);
                doc.ReplaceText("<<CURRENT_DATE>>", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                doc.ReplaceText("<<NAME>>", UtilityHelper.GetString(result.FullName).Limit(100));
                doc.ReplaceText("<<NRIC>>", UtilityHelper.GetString(result.Nric));
                doc.ReplaceText("<<GENDER>>", UtilityHelper.GetString(result.Gender));
                doc.ReplaceText("<<DOB>>", UtilityHelper.GetString(result.DateOfBirth));
                doc.ReplaceText("<<RACE>>", UtilityHelper.GetString(result.Race).Limit(5));
                doc.ReplaceText("<<HOME>>", UtilityHelper.GetString(result.HomeNumber).Limit(8));
                doc.ReplaceText("<<HP>>", UtilityHelper.GetString(result.MobileNumber).Limit(8));
                doc.ReplaceText("<<ADD>>", UtilityHelper.GetString(result.GetAddressWithoutPrefix()).Limit(48));
                //doc.ReplaceText("<<UNIT>>", result.un);
                doc.ReplaceText("<<POSTAL>>", UtilityHelper.GetString(result.PostalCode)); 
                doc.ReplaceText("<<LANG>>", UtilityHelper.GetString(result.Language).Limit(8));

                var ms = new MemoryStream();
                doc.SaveAs(ms);
                ms.Position = 0;
                byte[] fileBytes = ms.ToArray();

                String guid = Guid.NewGuid().ToString();
                TempData[guid] = fileBytes;
                /*
                return new JsonResult()
                {
                    Data = new { FileGuid = guid, FileName = psm.Nric + ".docx" }
                };
                */

                Document docTest = new Document();
                docTest.LoadFromStream(ms, FileFormat.Docx);

                MemoryStream stream = new MemoryStream();
                docTest.SaveToStream(stream, FileFormat.PDF);

                stream.Flush(); //Always catches me out
                stream.Position = 0; //Not sure if this is required

                TempData[guid] = stream.ToArray();

                return new JsonResult()
                {
                    Data = new { FileGuid = guid, FileName = psm.Nric + ".pdf" }
                };

                /*
                //return File(stream, "application/pdf");
                Response.AppendHeader("Content-Disposition", "inline; filename=name.pdf");
                return new FileContentResult(stream.ToArray(), "application/pdf");
                */

            }
        }

        [HttpGet]
        public ActionResult DownloadLabel(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/pdf", fileName);
                //return File(data, "application/vnd.ms-excel", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }

    }
}