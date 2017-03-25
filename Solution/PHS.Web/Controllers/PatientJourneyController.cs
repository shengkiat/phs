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

            using (var getPatient = new PatientManager())
            {
                IList<Patient> patients = getPatient.GetPatientsByNric(psm.IcFirstDigit, psm.IcNumber, psm.IcLastDigit, out message);
                if (patients == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    result.Patients = patients;
                }
            }

            return View(result);
        }

        // Both GET and POST: /PatientJourney/GoToEvent
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult GoToEvent(string selectedEvent)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            if (selectedEvent == null)
            {
                return View();
            }

            

            return View();
        }
    }
}