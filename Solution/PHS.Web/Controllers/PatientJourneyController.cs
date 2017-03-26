using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHS.Business.ViewModel.PatientJourney;
using PHS.Business.Implementation;
using PHS.DB;

namespace PHS.Web.Controllers
{
    public class PatientJourneyController : BaseController
    {
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
                    result = patientEvent;
                }
            }

            return View(result);
        }


        public ActionResult _JourneyModalityCirclesPartial()
        {
            ModalityCircleViewModel modalityCircle = new ModalityCircleViewModel();
            List<ModalityCircleViewModel> modalityCircleList = new List<ModalityCircleViewModel>();

            modalityCircle.Name = "History Taking";
            modalityCircle.Position = 0;
            modalityCircle.Active = true;
            modalityCircle.Visible = true;
            modalityCircle.IconPath = "../../Content/images/Modality/abacus.png";
            modalityCircleList.Add(modalityCircle);

            modalityCircle = new ModalityCircleViewModel();
            modalityCircle.Name = "Registration";
            modalityCircle.Position = 1;
            modalityCircle.Active = true;
            modalityCircle.Visible = true;
            modalityCircle.IconPath = "../../Content/images/Modality/achievement.png";
            modalityCircleList.Add(modalityCircle);

            modalityCircle = new ModalityCircleViewModel();
            modalityCircle.Name = "FIT";
            modalityCircle.Position = 2;
            modalityCircle.Active = false;
            modalityCircle.Visible = true;
            modalityCircle.IconPath = "../../Content/images/Modality/agenda.png";
            modalityCircleList.Add(modalityCircle);

            modalityCircle = new ModalityCircleViewModel();
            modalityCircle.Name = "TeleHealth";
            modalityCircle.Position = 3;
            modalityCircle.Active = true;
            modalityCircle.Visible = false;
            modalityCircle.IconPath = "../../Content/images/Modality/balance.png";
            modalityCircleList.Add(modalityCircle);

            return View(modalityCircleList); 
        }
    }
}