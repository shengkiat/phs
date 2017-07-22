﻿using PHS.Business.Implementation;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.Business.ViewModel.PastParticipantJourney;
using PHS.Common;
using PHS.DB;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
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
    }
}