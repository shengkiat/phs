using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHS.Business.ViewModel.PatientJourney;
using PHS.Business.Implementation;
using PHS.DB;
using PHS.FormBuilder.ViewModels;
using PHS.Repository.Repository;
using PHS.Common;
using PHS.FormBuilder.Extensions;
using PHS.FormBuilder.Helpers;
using System.IO;
using System.Web.Hosting;
using PHS.Business.Common;

namespace PHS.Web.Controllers
{
    public class PatientJourneyController : BaseController
    {
        private FormRepository _formRepo { get; set; }

        public PatientJourneyController()
            : this(new FormRepository())
        {

        }

        public PatientJourneyController(FormRepository formRepo)
        {
            this._formRepo = formRepo;
        }


        // GET: PatientJourney
        public ActionResult Index()
        {
            return View();
        }

        // Both GET and POST: /PatientJourney/SearchPatient
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SearchPatient(PatientSearchModel psm)
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
            PatientSearchModel result = new PatientSearchModel();

            using (var getPatientJourney = new PatientJourneyManager())
            {
                IList<PatientEventViewModel> patientEvents = getPatientJourney.GetPatientEventsByNric(psm.IcFirstDigit, psm.IcNumber, psm.IcLastDigit, out message);
                if (patientEvents == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    result.PatientEvents = patientEvents;
                }
            }

            return View(result);
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

            using (var getPatientJourney = new PatientJourneyManager())
            {
                PatientEventViewModel patientEvent = getPatientJourney.GetPatientEvent(nric, eventId, out message);
                if (patientEvent == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    patientEvent.SelectedModalityId = patientEvent.Event.Modalities.First().ID;
                    result = patientEvent;
                }
            }

            TempData["Nric"] = nric;
            TempData["EventId"] = eventId;

            return View(result);
        }
        /*
        public ActionResult JourneyModalityCircles()
        {
            //ModalityCircleViewModel modalityCircle = new ModalityCircleViewModel();
            //List<ModalityCircleViewModel> modalityCircleList = createModalityCircleData();

            string message = string.Empty;
            string nric = "S8518538A";
            string eventId = "100"; 
            PatientEventViewModel result = new PatientEventViewModel();
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

            List<Modality> modalityList = new List<Modality>();
            modalityList = (List<Modality>) result.Event.Modalities; 

            return View(modalityList);
        }



        public PartialViewResult ActivateCirclesFromMSSS(string activateList)
        {
            

            string message = string.Empty;
            string nric = "S8518538A";
            string eventId = "100";
            PatientEventViewModel result = new PatientEventViewModel();
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

            List<Modality> modalityList = new List<Modality>();
            modalityList = (List<Modality>)result.Event.Modalities;
            

            string[] activateArray = activateList.Split('|');
            for (int i = 0; i < modalityList.Count; i++)
            {
                if(activateArray.ElementAt(i) == "1")
                {
                    modalityList.ElementAt(i).IsActive = true;
                }
                else
                {
                    modalityList.ElementAt(i).IsActive = false;
                }
            }
            
            return PartialView("_JourneyModalityCirclesPartial", modalityList);
        }*/

        public PartialViewResult RefreshModalityForms(string selectedModalityId)
        {

            string nric = TempData["Nric"].ToString();
            string eventId = TempData["EventId"].ToString();

            string message = string.Empty;
            PatientEventViewModel result = new PatientEventViewModel();

            using (var getPatientJourney = new PatientJourneyManager())
            {
                PatientEventViewModel patientEvent = getPatientJourney.GetPatientEvent(nric, eventId, out message);
                if (patientEvent == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    patientEvent.SelectedModalityId = Int32.Parse(selectedModalityId);
                    result = patientEvent;
                }
            }

            TempData.Keep();

            return PartialView("_JourneyModalityFormsPartial", result);
        }


        public ActionResult ViewForm(int formId, bool embed = false)
        {
            FormViewModel model = null;
            // var form = this._formRepo.GetByPrimaryKey(id);

            //TODO should retrieve form by eventId + formId?
            var form = this._formRepo.GetForm(formId);

            if (form != null)
            {
                model = FormViewModel.CreateFromObject(form, Constants.FormFieldMode.INPUT);
                model.Embed = embed;
            }
            else
            {
               // return RedirectToAction("edit", new { formId = form.ID });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(IDictionary<string, string> SubmitFields, FormViewModel model, FormCollection form)
        {
            IList<string> errors = Enumerable.Empty<string>().ToList();
            //var formObj = this._formRepo.GetByPrimaryKey(model.Id.Value);

            var formObj = this._formRepo.GetForm(model.Id.Value);


            var formView = FormViewModel.CreateFromObject(formObj, Constants.FormFieldMode.INPUT);
            formView.AssignInputValues(form);
            this.InsertValuesIntoTempData(SubmitFields, form);


            if (formView.Fields.Any())
            {
                // first validate fields
                foreach (var field in formView.Fields)
                {
                    var valId = field.ValidationId();
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
                IDictionary<string, FormFieldValueViewModel> notificationEntries = new Dictionary<string, FormFieldValueViewModel>();
                foreach (var field in formView.Fields)
                {
                    var value = field.SubmittedValue(form);

                    //if it's a file, save it to hard drive
                    if (field.FieldType == Constants.FieldType.FILEPICKER && !string.IsNullOrEmpty(value))
                    {
                        var file = Request.Files[field.SubmittedFieldName()];
                        var fileValueObject = value.GetFileValueFromJsonObject();

                        if (fileValueObject != null)
                        {
                            
                            file.SaveAs(Path.Combine(HostingEnvironment.MapPath(fileValueObject.SavePath), fileValueObject.SaveName));
                            
                        }
                    }

                    this.AddValueToDictionary(ref notificationEntries, field.Label, new FormFieldValueViewModel(field.FieldType, value));
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
                return Json(new { success = true, message = "Your changes were saved.", isautosave = false });

            }

        Error:
            TempData["error"] = errors.ToUnorderedList();
            var error = "Unable to save form ".AppendIfDebugMode(errors.ToUnorderedList());
            return Json(new { success = false, error = error, isautosave = false });
        }

        private void InsertValuesIntoTempData(IDictionary<string, string> submittedValues, FormCollection form)
        {
            foreach (var key in form.AllKeys)
            {
                ViewData[key.ToLower()] = form[key];
            }

        }

        private void AddValueToDictionary(ref IDictionary<string, FormFieldValueViewModel> collection, string key, FormFieldValueViewModel value)
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