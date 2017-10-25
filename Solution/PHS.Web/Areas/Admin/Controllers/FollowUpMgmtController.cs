using PHS.Business.Implementation;
using PHS.Business.ViewModel.FollowUp;
using PHS.Common;
using PHS.Web.Controllers;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Areas.Admin.Controllers
{
    public class FollowUpMgmtController : BaseController
    {
        // GET: Admin/FollowUpMgmt
        public ActionResult Index()
        {
            using (var eventmanger = new EventManager())
            {
                var result = eventmanger.GetAllEvents().ToList();
                return View(result);
            }
        }
        public ActionResult GetFollowUpConfigurationByEventID(int phsEventId)
        {
            using (var followUpManager = new FollowUpConfigurationManager())
            {
                var result = followUpManager.GetAllFUConfigurationByEventID(phsEventId);
                //var result = followUpManager.GetAllFUConfiguration();
                return PartialView("_FollowUpConfigurations", result);
            }
        }

        public ActionResult GetParticipantsByFollowUpConfiguration(int? followupconfigurationid)
        {
            using (var followUpManager = new FollowUpManager())
            {
                var result = followUpManager.GetParticipantsByFollowUpConfiguration(followupconfigurationid.Value);
                return PartialView("_ParticipantsTable", result);
            }
        }

        [HttpPost]
        public ActionResult DeployFollowUpConfiguration(int followupconfigurationid)
        {
            string message = string.Empty;
            using (var followUpManager = new FollowUpManager())
            {
                var success = followUpManager.DeployFollowUpConfiguration(followupconfigurationid, out message);
                if (!success)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Error = message });
                }
                return Json(new { Success = "Deployment Successful." });
            }
        }

        [HttpPost]
        public ActionResult PrintHealthReportByFollowUpGroup(int followupconfigurationid)
        {
            string message = string.Empty;
            using (var followUpManager = new FollowUpManager())
            {
                var success = followUpManager.PrintHealthReportByFollowUpGroup(followupconfigurationid, out message);
                if (!success)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Error = message });
                }
                return Json(new { Success = "Printing Successful." });
            }
        }

    }
}