using PHS.Business.Implementation;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.Common;
using PHS.DB;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                    return RedirectToAction("JourneyModality", psm);
                }

            }
        }

        public ActionResult RegisterParticipant()
        {
            ParticipantJourneySearchViewModel psm = (ParticipantJourneySearchViewModel) TempData.Peek("ToRegister");

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

                return RedirectToAction("JourneyModality", psm);
            }
        }
    }
}