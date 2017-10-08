

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
    using Filter;
    using Business.ViewModel.ParticipantJourney;
    using static Common.Constants;

    [CustomAuthorize(Roles = Constants.User_Role_Doctor_Code + Constants.User_Role_CommitteeMember_Code)]
    public class PastParticipantJourneyController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchPastParticipantJourney(string Nric)
        {

            if (string.IsNullOrEmpty(Nric))
            {
                return View();
            }

            using (var participantJourneyManager = new PastParticipantJourneyManager(GetLoginUser()))
            {
                string message = string.Empty;
                PastParticipantJourneySearchViewModel result = new PastParticipantJourneySearchViewModel();

                IList<ParticipantJourneyViewModel> participantJourneyViewModels = participantJourneyManager.GetAllParticipantJourneyByNric(Nric, out message);
                if (!string.IsNullOrEmpty(message))
                {
                    SetViewBagError(message);
                }

                else
                {
                    result.ParticipantJourneyViewModels = participantJourneyViewModels;
                }

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_SearchPastParticipantJourneyResultPartial", result);
                }
                else
                {
                    return View(result);
                }
            }
        }

        [HttpPost]
        public ActionResult ViewPastParticipantJourney(string Nric, int PHSEventId)
        {
            if (string.IsNullOrEmpty(Nric) || PHSEventId == 0)
            {
                return Redirect("Index");
            }

            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel()
            {
                Nric = Nric,
                PHSEventId = PHSEventId
            };

            using (var participantJourneyManager = new PastParticipantJourneyManager(GetLoginUser()))
            {
                string message = string.Empty;

                ParticipantJourneyViewModel result = participantJourneyManager.RetrievePastParticipantJourney(psm, out message);

                if (!string.IsNullOrEmpty(message))
                {
                    SetViewBagError(message);
                    return Redirect("Index");
                }

                else
                {
                    List<ParticipantJourneyModalityCircleViewModel> participantJourneyModalityCircles = participantJourneyManager.GetParticipantMegaSortingStation(psm);

                    ParticipantJourneyFormViewModel participantJourneyformView = new ParticipantJourneyFormViewModel(result.Participant, psm.PHSEventId);
                    participantJourneyformView.SelectedModalityId = result.Event.Modalities.First().ModalityID;

                    TempData["ParticipantJourneySearchViewModel"] = psm;
                    TempData["ParticipantJourneyModalityCircleViewModel"] = participantJourneyModalityCircles;
                    TempData["ParticipantJourneyFormViewModel"] = participantJourneyformView;
                    TempData["SelectedModalityId"] = participantJourneyformView.SelectedModalityId;
                    TempData["ViewParticipantJourneyType"] = Constants.TemplateFieldMode.READONLY;

                    return View("~/Views/ParticipantJourney/ViewParticipantJourney.cshtml", result);

                }
            }
        }
    }
}