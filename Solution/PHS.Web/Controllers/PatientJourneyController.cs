using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult SearchPatient()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            return View();
        }
    }
}