using PHS.Business.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Controllers
{
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
    }
}