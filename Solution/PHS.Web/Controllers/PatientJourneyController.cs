﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Controllers
{
    public class PatientJourneyController : Controller
    {
        // GET: PatientJourney
        public ActionResult Index()
        {
            return View();
        }

        // Both GET and POST: /PatientJourney/SearchUser
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SearchPatient()
        {
            return View();
        }
    }
}