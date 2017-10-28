using Newtonsoft.Json;
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
                var success = followUpConfigurationManager.NewFollowUpConfiguration(followupconfiguration, out message);
                if (!success)
                {
                    SetViewBagError(message);
                    return View();
                }

                SetTempDataMessage(followupconfiguration.Title + " has been created successfully. Add Followup groups.");
                return View(followupconfiguration);
            }
        }

        [HttpGet]
        public ActionResult CreateFollowUpGroup(int followUpConfigurationID)
        {
            string message = string.Empty;
            using (var followUpConfigurationManager = new FollowUpConfigurationManager())
            {
                FollowUpConfiguration followupconfiguration = followUpConfigurationManager.GetFUConfigurationByID(followUpConfigurationID, out message);
                if (followupconfiguration == null)
                {
                    SetViewBagError("Invalid Follow-Up Configuratio ID. Create Follow-Up Configuration first.");
                    return View();
                }
                FollowUpGroup followupgroup = new FollowUpGroup();
                followupgroup.FollowUpConfigurationID = followUpConfigurationID;

                return View(followupgroup);
            }
        }

        [HttpPost]
        public ActionResult CreateFollowUpGroup([Bind(Exclude = "FollowUpGroupID")] FollowUpGroup followupgroup)
        {
            string message = string.Empty;

            using (var followUpConfigurationManager = new FollowUpConfigurationManager(GetLoginUser()))
            {
                followupgroup.Filter = "TESTFilter";
                var newFollowupgroup = followUpConfigurationManager.AddFollowUpGroup(followupgroup, out message);
                if (newFollowupgroup == null)
                {
                    SetViewBagError(message);
                    return View(followupgroup);
                }

                SetTempDataMessage(Constants.ValueSuccessfuly("Follow-up group " + newFollowupgroup.Title + " has been added"));
                return View(newFollowupgroup);
            }
        }

        [HttpGet]
        public ActionResult GetTeleHealthModalitiesByEventID(int phseventid)
        {
            string message = string.Empty;
            using (var followupconfigurationmanager = new FollowUpConfigurationManager())
            {
                var result = followupconfigurationmanager.GetTeleHealthModalitiesByEventID(phseventid, out message);
                return PartialView("_CreateFollowUpGroup", result);
            }
        }

        [HttpGet]
        public ActionResult CreateFollowUpGroupTemp()
        {
            string message = string.Empty;
            using (var eventManager = new EventManager(GetLoginUser()))
            {
                ViewBag.Events = eventManager.GetAllEvents();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            string message = string.Empty;

            using (var followUpConfigurationManager = new FollowUpConfigurationManager())
            {
                if (followUpConfigurationManager.DeleteFollowUpConfiguration(id, out message))
                {
                    return View();
                }
                SetViewBagError(message);

                return View();
            }
        }

        [HttpPost]
        public ActionResult GetForms(int modalityid)
        {
            string message = string.Empty;
            using (var modalityManager = new ModalityManager(GetLoginUser()))
            {
                var results = modalityManager.GetModalityByID(modalityid, out message).Forms.ToList();
                var list = JsonConvert.SerializeObject(results,
                            Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            });

                return Content(list, "application/json");
            }
        }

        [HttpPost]
        public ActionResult GetTemplates(int formid)
        {
            string message = string.Empty;
            using (var formManager = new FormManager(GetLoginUser()))
            {
                var results = formManager.FindAllTemplatesByFormId(formid).ToList();
                var list = JsonConvert.SerializeObject(results,
                            Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            });

                return Content(list, "application/json");
            }
        }

        
        [HttpPost]
        public ActionResult GetTemplateFields(int templateid)
        {
            string message = string.Empty;
            using (var formManager = new FormManager(GetLoginUser()))
            {
                var result = formManager.GetTemplateById(templateid).TemplateFields.ToList();
                var list = JsonConvert.SerializeObject(result,
                            Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            });

                return Content(list, "application/json");
            }
        }
    }
}