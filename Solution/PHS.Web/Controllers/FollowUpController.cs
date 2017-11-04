﻿using PHS.Business.Implementation;
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
    [CustomAuthorize(Roles = Constants.User_Role_CommitteeMember_Code + Constants.User_Role_FollowUpVolunteer_Code)]
    public class FollowUpController : BaseController
    {
        // GET: FollowUp
        public ActionResult Index()
        {
            using (var eventmanger = new EventManager())
            {
                var result = eventmanger.GetAllEvents().ToList();
                return View(result);
            }
        }
        public ActionResult GetParticipantsByLoginUser(int? eventid)
        {
            string message = string.Empty;
            using (var followUpManager = new FollowUpManager())
            {
                var result = followUpManager.GetParticipantsByLoginUser(eventid.Value, GetLoginUser(), out message);
                return PartialView("_ParticipantCallerTable", result);
            }
        }

        [HttpPost]
        public ActionResult SaveParticipantCallerMapping([Bind(Exclude = "ParticipantID,FollowUpGroupID,PhaseIFollowUpVolunteer,PhaseIIFollowUpVolunteer,PhaseICommitteeMember,PhaseIICommitteeMember")]ParticipantCallerMapping participantCallerMapping)
        {
            string message = string.Empty;

            using (var followUpManager = new FollowUpManager())
            {
                bool isSaved = followUpManager.SaveParticipantCallerMapping(participantCallerMapping, out message);

                if (!isSaved)
                {
                    return Json(new { success = false, error = message, isautosave = false });
                }

                else
                {
                    return Json(new { success = true, message = "Your changes were saved.", isautosave = false });
                }
            }

            
        }
    }
}