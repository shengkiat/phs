﻿using Novacode;
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

namespace PHS.Web.Controllers
{
    public class PublicFormController : BaseController
    {
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
                    throw new HttpException(404, "No public Form found");
                }
            }
        }

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

        public void InsertValuesIntoTempData(IDictionary<string, string> submittedValues, FormCollection formCollection)
        {
            foreach (var key in formCollection.AllKeys)
            {
                ViewData[key.ToLower()] = formCollection[key];
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

    }
}