

namespace PHS.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using PHS.Business.Common;
    using PHS.Business.Extensions;
    using PHS.Business.Implementation;
    using PHS.Business.ViewModel.PastParticipantJourney;
    using PHS.Common;
    using PHS.DB.ViewModels.Form;
    using PHS.Repository.Repository;
    using Repository.Context;

    public class PastParticipantJourneyController : BaseController
    {

        // GET: PatientJourney
        public ActionResult Index()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            return View();
        }

        [HttpPost]
        public ActionResult SearchParticipantJourney(PastParticipantJourneySearchViewModel psm)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            if (psm == null)
            {
                return View();
            }

            string message = string.Empty;
            PastParticipantJourneySearchViewModel result = new PastParticipantJourneySearchViewModel();

            using (var participantJourneyManager = new PastParticipantJourneyManager())
            {
                IList<PatientEventViewModel> patientEvents = participantJourneyManager.GetPatientEventsByNric(psm.Nric, out message);
                if (patientEvents == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    result.PatientEvents = patientEvents;
                }
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SearchParticipantJourneyResultPartial", result);
            }
            else
            {
                return View(result);
            }
        }

        // Both GET and POST: /PatientJourney/JourneyModality
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult JourneyModality(string nric, string eventId)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            if (string.IsNullOrEmpty(nric) || string.IsNullOrEmpty(eventId))
            {
                return Redirect("~/patientjourney");
            }

            string message = string.Empty;
            PatientEventViewModel result = new PatientEventViewModel();

            using (var participantJourneyManager = new PastParticipantJourneyManager())
            {
                PatientEventViewModel patientEvent = participantJourneyManager.GetPatientEvent(nric, eventId, out message);
                if (patientEvent == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    
                    result = patientEvent;

                    //TODO think of way to handle such methods

                    List<PatientEventModalityViewModel> patientEventModalitys = new List<PatientEventModalityViewModel>();

                    foreach(var modality in patientEvent.Event.Modalities)
                    {
                        patientEventModalitys.Add(new PatientEventModalityViewModel(result, modality));
                    }


                    TempData["Nric"] = nric;
                    TempData["EventId"] = eventId;
                    TempData["PatientEventModalityViewModel"] = patientEventModalitys;
                    TempData["SelectedModalityId"] = patientEvent.Event.Modalities.First().ModalityID;
                    patientEvent.SelectedModalityId = patientEvent.Event.Modalities.First().ModalityID;
                }
            }

            return View(result);
        }

        public PartialViewResult ActivateCirclesFromMSSS(string activateList)
        {
            

            string message = string.Empty;
            //string nric = "S8518538A";
            //string eventId = "100";

            string nric = TempData.Peek("Nric").ToString();
            string eventId = TempData.Peek("EventId").ToString();


            /*PatientEventViewModel result = new PatientEventViewModel();
            using (var getPatientJourney = new PatientJourneyManager())
            {
                PatientEventViewModel patientEvent = getPatientJourney.GetPatientEvent(nric, eventId, out message);
                if (patientEvent == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    result = patientEvent;
                }
            }

            ICollection<Modality> modalityList = result.Event.Modalities;*/

            ICollection<PatientEventModalityViewModel> modalityList = (List<PatientEventModalityViewModel>)TempData.Peek("PatientEventModalityViewModel");

            if (!string.IsNullOrEmpty(activateList))
            {
                string[] activateArray = activateList.Split('|');
                for (int i = 0; i < modalityList.Count; i++)
                {
                    if (activateArray.ElementAt(i) == "1")
                    {
                        modalityList.ElementAt(i).IsActive = true;
                    }
                    else
                    {
                        modalityList.ElementAt(i).IsActive = false;
                    }
                }
            }
            
            return PartialView("_JourneyModalityCirclesPartial", modalityList);
        }

        public PartialViewResult RefreshModalityForms(string selectedModalityId)
        {

            string nric = TempData.Peek("Nric").ToString();
            string eventId = TempData.Peek("EventId").ToString();

            string message = string.Empty;
            PatientEventViewModel result = new PatientEventViewModel();

            using (var participantJourneyManager = new PastParticipantJourneyManager())
            {
                PatientEventViewModel patientEvent = participantJourneyManager.GetPatientEvent(nric, eventId, out message);
                if (patientEvent == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    TempData["SelectedModalityId"] = selectedModalityId;
                    patientEvent.SelectedModalityId = Int32.Parse(selectedModalityId);
                    result = patientEvent;
                }
            }

            return PartialView("_JourneyModalityFormsPartial", result);
        }


        public ActionResult ViewForm(int templateId, bool embed = false)
        {
            TemplateViewModel model = null;
            // var form = this._formRepo.GetByPrimaryKey(id);

            //TODO should retrieve form by eventId + formId?
            using (var formManager = new FormManager())
            { 
                var template = formManager.FindTemplate(templateId);

                if (template != null)
                {
                    if (model.Title.Equals("Mega Sorting Station"))
                    {
                        return PartialView("_MegaSortingStationPartial", TempData.Peek("PatientEventModalityViewModel"));
                    }

                    else
                    {
                        model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
                        model.Embed = embed;
                    }

                }
                else
                {
                    // return RedirectToAction("edit", new { formId = form.ID });
                }

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult FillIn(IDictionary<string, string> SubmitFields, TemplateViewModel model, FormCollection formCollection)
        {
            using (var formManager = new FormManager())
            {
                IList<string> errors = Enumerable.Empty<string>().ToList();
                //var formObj = this._formRepo.GetByPrimaryKey(model.Id.Value);

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

                    //TODO this codes might not needed after prototype
                    List<PatientEventModalityViewModel> patientEventModalitys = (List<PatientEventModalityViewModel>)TempData.Peek("PatientEventModalityViewModel");

                    foreach (var patientEventModality in patientEventModalitys)
                    {
                        if (patientEventModality.isModalityFormsContain(model.TemplateID.Value))
                        {
                            patientEventModality.modalityCompletedForms.Add(model.TemplateID.Value);
                        }
                    }

                    TempData["PatientEventModalityViewModel"] = patientEventModalitys;

                    return Json(new { success = true, message = "Your changes were saved.", isautosave = false });
                }

                else
                {
                    TempData["error"] = result;
                    return Json(new { success = false, error = "Unable to save form ", isautosave = false });
                }

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
        


    }
}