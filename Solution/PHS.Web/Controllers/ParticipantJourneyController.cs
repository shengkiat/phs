﻿using PHS.Business.Implementation;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.Common;
using PHS.DB;
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
            using (var participantJourneyManager = new ParticipantJourneyManager())
            {
                ParticipantJourneySearchViewModel result = participantJourneyManager.FindActiveEvents();
                return View(result);
            }
                
        }
    }
}