using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHS.Business.ViewModel;
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
                IList<PatientEvent> patientEvents = getPatientJourney.GetPatientsByNric(psm.IcFirstDigit, psm.IcNumber, psm.IcLastDigit, out message);
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

            if (eventId == null)
            {
                return View();
            }


            return View();
        }
    }
}