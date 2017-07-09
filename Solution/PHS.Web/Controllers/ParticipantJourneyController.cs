﻿using PHS.Common;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Controllers
{
    [CustomAuthorize(Roles = Constants.User_Role_Doctor_Code + Constants.User_Role_Admin_Code + Constants.User_Role_Volunteer_Code)]
    public class ParticipantJourneyController : Controller
    {
        // GET: ParticipantJourney
        public ActionResult Index()
        {
            return View();
        }
    }
}