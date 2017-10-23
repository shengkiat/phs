using PHS.Business.Implementation;
using PHS.Business.ViewModel.FollowUp;
using PHS.Common;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Areas.Admin.Controllers
{
    public class FollowUpMgmtController : Controller
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
                //var result = followUpManager.GetAllFUConfigurationByEventID(phsEventId);
                var result = followUpManager.GetAllFUConfiguration();
                return PartialView("_FollowUpConfigurations", result);
            }
        }

        public ActionResult GetParticipants()
        {
            using (var followUpManager = new FollowUpManager())
            {
                var result = followUpManager.RetrieveScreeningEventParticipants(3);
                return PartialView("_ParticipantsTable", result);
            }
        }

    }
}