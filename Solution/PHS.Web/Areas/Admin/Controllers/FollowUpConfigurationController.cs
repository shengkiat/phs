using PHS.Business.Implementation;
using PHS.Common;
using PHS.DB;
using PHS.Web.Controllers;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Areas.Admin.Controllers
{
    //[CustomAuthorize(Roles = Constants.User_Role_CommitteeMember_Code)]
    public class FollowUpConfigurationController : BaseController
    {
        // GET: Admin/FollowUpConfiguration
        [HttpGet]
        public ActionResult Index()
        {
            using (var fuconfigmanager = new FollowUpConfigurationManager(GetLoginUser()))
            {
                var result = fuconfigmanager.GetAllFUConfiguration();
                return View(result);
            }
        }

        [HttpGet]
        public ActionResult GetFollowUpConfigurationByID(int id)
        {
            string message = string.Empty;
            using (var fuconfigmanager = new FollowUpConfigurationManager(GetLoginUser()))
            {
                var result = fuconfigmanager.GetFUConfigurationByID(id, out message);
                //var result = fuconfigmanager.GetAllFUConfiguration();
                return PartialView("_FollowUpGroups", result);
            }
        }

        [HttpGet]
        public ActionResult CreateFollowUpConfiguration()
        {
            string message = string.Empty;
            using (var eventManager = new EventManager(GetLoginUser()))
            {
                ViewBag.Events = eventManager.GetAllEvents();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFollowUpConfiguration([Bind(Include = "PHSEventID,Title")] FollowUpConfiguration followupconfiguration)
        {
            string message = string.Empty;
            //TODO: move to keep
            using (var eventManager = new EventManager(GetLoginUser()))
            {
                ViewBag.Events = eventManager.GetAllEvents();
            }

            using (var followUpConfigurationManager = new FollowUpConfigurationManager(GetLoginUser()))
            {
                var newFollowupconfiguration = followUpConfigurationManager.NewFollowUpConfiguration(followupconfiguration, out message);
                if (!newFollowupconfiguration)
                {
                    SetViewBagError(message);
                    return View();
                }

                SetTempDataMessage(followupconfiguration.Title + " has been created successfully. Add Followup groups.");
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetEventByID()
        {
            string message = string.Empty;
            using (var eventmanager = new EventManager(GetLoginUser()))
            {
                var result = eventmanager.GetEventByID(3, out message);
                return PartialView("_CreateFollowUpGroup", result);
            }
        }

        [HttpGet]
        public ActionResult CreateFollowUpGroup()
        {
            string message = string.Empty;
            using (var eventManager = new EventManager(GetLoginUser()))
            {
                ViewBag.Events = eventManager.GetAllEvents();
            }
            return View();
        }
    }
}