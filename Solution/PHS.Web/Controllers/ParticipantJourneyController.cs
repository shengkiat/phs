using PHS.Business.Common;
using PHS.Business.Extensions;
using PHS.Business.Implementation;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.Business.ViewModel.PastParticipantJourney;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static PHS.Common.Constants;

namespace PHS.Web.Controllers
{
    [CustomAuthorize(Roles = Constants.User_Role_Doctor_Code + Constants.User_Role_Admin_Code + Constants.User_Role_Volunteer_Code)]
    public class ParticipantJourneyController : BaseController
    {
        // GET: ParticipantJourney
        public ActionResult Index()
        {
            using (var participantJourneyManager = new ParticipantJourneyManager())
            {
                ParticipantJourneySearchViewModel result = participantJourneyManager.RetrieveActiveScreeningEvent();
                return View(result);
            }
        }

        [HttpPost]
        public ActionResult SearchParticipantJourney(ParticipantJourneySearchViewModel psm)
        {

            if (psm == null)
            {
                return View();
            }
            
            using (var participantJourneyManager = new ParticipantJourneyManager())
            {
                string message = string.Empty;
                MessageType messageType = MessageType.ERROR;

                ParticipantJourneyViewModel result = participantJourneyManager.RetrieveParticipantJourney(psm, out message, out messageType);
                if (result == null)
                {
                    if (MessageType.ERROR == messageType)
                    {
                        SetViewBagError(message);
                    }

                    else
                    {
                        SetViewBagMessage(message);
                    }

                    TempData["ToRegister"] = psm;


                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("_SearchParticipantJourneyResultPartial", psm);
                    }
                    else
                    {
                        return View(psm);
                    }
                }

                else
                {
                    string url = Url.Action("ViewParticipantJourney", "ParticipantJourney", new { Nric = psm.Nric , PHSEventId = psm.PHSEventId });
                    return JavaScript("window.location = '" + url + "'");
                }

            }
        }

        public ActionResult RegisterParticipant()
        {
            ParticipantJourneySearchViewModel psm = (ParticipantJourneySearchViewModel) TempData["ToRegister"];

            if (psm == null)
            {
                SetViewBagMessage("No Participant information found");
                return RedirectToAction("Index", psm);
            }

            using (var participantJourneyManager = new ParticipantJourneyManager())
            {

                string result = participantJourneyManager.RegisterParticipant(psm);

                if (!result.Equals("success"))
                {
                    SetViewBagMessage(result);
                    return RedirectToAction("Index", psm);
                }

                return RedirectToAction("ViewParticipantJourney", psm);
            }
        }

        public ActionResult ViewParticipantJourney(ParticipantJourneySearchViewModel psm)
        {
            if (psm == null)
            {
                return Redirect("Index");
            }

            using (var participantJourneyManager = new ParticipantJourneyManager())
            {
                string message = string.Empty;
                MessageType messageType = MessageType.ERROR;

                ParticipantJourneyViewModel result = participantJourneyManager.RetrieveParticipantJourney(psm, out message, out messageType);

                if (result == null)
                {
                    SetViewBagError(message);
                    return Redirect("Index");
                }

                else
                {
                    List<ParticipantJourneyModalityCircleViewModel> participantJourneyModalityCircles = new List<ParticipantJourneyModalityCircleViewModel>();

                    foreach (var modality in result.Event.Modalities)
                    {
                        participantJourneyModalityCircles.Add(new ParticipantJourneyModalityCircleViewModel(result, modality));
                    }

                    ParticipantJourneyFormViewModel participantJourneyformView = new ParticipantJourneyFormViewModel(result.Participant, psm.PHSEventId);

                    participantJourneyformView.SelectedModalityId = result.Event.Modalities.First().ModalityID;

                    TempData["ParticipantJourneySearchViewModel"] = psm;
                    TempData["ParticipantJourneyModalityCircleViewModel"] = participantJourneyModalityCircles;
                    TempData["ParticipantJourneyFormViewModel"] = participantJourneyformView;
                    TempData["SelectedModalityId"] = participantJourneyformView.SelectedModalityId;

                    return View(result);

                }
            }
        }

        public ActionResult RefreshViewParticipantJourneyForm(string selectedModalityId)
        {
            ParticipantJourneySearchViewModel psm = (ParticipantJourneySearchViewModel) TempData.Peek("ParticipantJourneySearchViewModel");

            if (psm == null)
            {
                return Redirect("Index");
            }

            using (var participantJourneyManager = new ParticipantJourneyManager())
            {
                string message = string.Empty;

                ParticipantJourneyFormViewModel result = participantJourneyManager.RetrieveParticipantJourneyForm(psm, out message);

                if (result == null)
                {
                    SetViewBagError(message);
                    return Redirect("Index");
                }

                else
                {
                    TempData["SelectedModalityId"] = selectedModalityId;
                    result.SelectedModalityId = Int32.Parse(selectedModalityId);
                }

                return PartialView("_ViewParticipantJourneyFormPartial", result);
            }
        }


        public ActionResult InternalFillIn(int id, bool embed = false)
        {
            using (var formManager = new FormAccessManager())
            {
                TemplateViewModel model = null;

                var template = formManager.FindLatestTemplate(id);

                if (template != null)
                {
                    model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
                    model.Embed = embed;


                    ParticipantJourneySearchViewModel psm = (ParticipantJourneySearchViewModel) TempData.Peek("ParticipantJourneySearchViewModel");

                    using (var participantJourneyManager = new ParticipantJourneyManager())
                    {
                        string message = string.Empty;
                        ParticipantJourneyModality participantJourneyModality = participantJourneyManager.RetrieveParticipantJourneyModality(psm, id, out message);

                        if ("MEG".Equals(model.InternalFormType))
                        {
                            List<ParticipantJourneyModalityCircleViewModel> pjmcyvmItems = participantJourneyManager.GetParticipantMegaSortingStation(psm); 

                            return PartialView("~/Views/ParticipantJourney/_MegaSortingStationPartial.cshtml", pjmcyvmItems);
                        }


                        if (participantJourneyModality != null)
                        {
                            // for mega sorting station
   

                            // to load non mega sorting station 
                            foreach (var field in model.Fields)
                            {
                                field.EntryId = participantJourneyModality.EntryId.ToString();
                            }
                        }
                        
                    }

                    return View("_FillInPartial", model);
                }
                else
                {
                    return RedirectToError("invalid id");
                }
            }
        }

        [HttpPost]
        public ActionResult InternalFillIn(IDictionary<string, string> SubmitFields, TemplateViewModel model, FormCollection formCollection)
        {
            InsertValuesIntoTempData(SubmitFields, formCollection);

            using (var formManager = new FormAccessManager())
            {
                var template = formManager.FindTemplate(model.TemplateID.Value);

                var templateView = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);

                string nric = null;
                string eventId = null;
                string modalityId = null;
                if(TempData.Peek("ParticipantJourneyFormViewModel") != null) { 
                    ParticipantJourneyFormViewModel participantJourneyFormViewModel = (ParticipantJourneyFormViewModel)TempData.Peek("ParticipantJourneyFormViewModel");
                    modalityId = participantJourneyFormViewModel.SelectedModalityId.ToString();
                }

                if(TempData.Peek("ParticipantJourneySearchViewModel") != null)
                {
                    ParticipantJourneySearchViewModel participantJourneySearchViewModel = (ParticipantJourneySearchViewModel)TempData.Peek("ParticipantJourneySearchViewModel");
                    eventId = participantJourneySearchViewModel.PHSEventId.ToString();
                    nric = participantJourneySearchViewModel.Nric;
                }

                string result = formManager.FillIn(SubmitFields, model, formCollection, nric, eventId, modalityId);

                if (result.Equals("success"))
                {
                    TempData["success"] = templateView.ConfirmationMessage;

                    List<ParticipantJourneyModalityCircleViewModel> participantJourneyModalityCircles = (List<ParticipantJourneyModalityCircleViewModel>)TempData.Peek("ParticipantJourneyModalityCircleViewModel");

                    foreach (var participantJourneyModalityCircle in participantJourneyModalityCircles)
                    {
                        if (participantJourneyModalityCircle.isModalityFormsContain(templateView.FormID))
                        {
                            participantJourneyModalityCircle.modalityCompletedForms.Add(templateView.FormID);
                        }
                    }

                    TempData["ParticipantJourneyModalityCircleViewModel"] = participantJourneyModalityCircles;

                    return Json(new { success = true, message = "Your changes were saved.", isautosave = false });
                }

                else
                {
                    TempData["error"] = result;
                    return Json(new { success = false, error = "Unable to save form ", isautosave = false });
                }
            }
        }

        private void InsertValuesIntoTempData(IDictionary<string, string> submittedValues, FormCollection formCollection)
        {
            foreach (var key in formCollection.AllKeys)
            {
                ViewData[key.ToLower()] = formCollection[key];
            }
        }

        public PartialViewResult ActivateCirclesFromMSSS(string activateList)
        {
            //string message = string.Empty;
            //string nric = "S8518538A";
            //string eventId = "100";

            //string nric = TempData.Peek("Nric").ToString();
            //string eventId = TempData.Peek("EventId").ToString();


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

            ICollection<ParticipantJourneyModalityCircleViewModel> modalityList = (List<ParticipantJourneyModalityCircleViewModel>)TempData.Peek("ParticipantJourneyModalityCircleViewModel");

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

            return PartialView("_ViewParticipantJourneyCirclePartial", modalityList);
        }
    }
}