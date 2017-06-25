

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
    using PHS.Business.ViewModel.PatientJourney;
    using PHS.Common;
    using PHS.DB.ViewModels.Forms;
    using PHS.Repository.Repository;

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
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

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
                IList<PatientEventViewModel> patientEvents = getPatientJourney.GetPatientEventsByNric(psm.Nric, out message);
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

            using (var getPatientJourney = new PatientJourneyManager())
            {
                PatientEventViewModel patientEvent = getPatientJourney.GetPatientEvent(nric, eventId, out message);
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


        public ActionResult ViewForm(int formId, bool embed = false)
        {
            TemplateViewModel model = null;
            // var form = this._formRepo.GetByPrimaryKey(id);

            //TODO should retrieve form by eventId + formId?
            var form = this._formRepo.GetForm(formId);

            if (form != null)
            {
                if (form.Title.Equals("Mega Sorting Station"))
                {
                    return PartialView("_MegaSortingStationPartial", TempData.Peek("PatientEventModalityViewModel"));
                }

                else
                {
                    model = TemplateViewModel.CreateFromObject(form, Constants.FormFieldMode.INPUT);
                    model.Embed = embed;
                }
                
            }
            else
            {
               // return RedirectToAction("edit", new { formId = form.ID });
            }

            return View(model);
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
                            
                        //    file.SaveAs(Path.Combine(HostingEnvironment.MapPath(fileValueObject.SavePath), fileValueObject.SaveName));
                            
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

                //TODO this codes might not needed after prototype
                List<PatientEventModalityViewModel> patientEventModalitys = (List<PatientEventModalityViewModel>) TempData.Peek("PatientEventModalityViewModel");

                foreach(var patientEventModality in patientEventModalitys)
                {
                    if (patientEventModality.isModalityFormsContain(model.Id.Value))
                    {
                        patientEventModality.modalityCompletedForms.Add(model.Id.Value);
                    }
                }

                TempData["PatientEventModalityViewModel"] = patientEventModalitys;

                return Json(new { success = true, message = "Your changes were saved.", isautosave = false });

            }

        Error:
            TempData["error"] = errors.ToUnorderedList();
           // var error = "Unable to save form ".AppendIfDebugMode(errors.ToUnorderedList());
            return Json(new { success = false, error = "Unable to save form ", isautosave = false });
        }

        private void InsertValuesIntoTempData(IDictionary<string, string> submittedValues, FormCollection form)
        {
            foreach (var key in form.AllKeys)
            {
                ViewData[key.ToLower()] = form[key];
            }

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