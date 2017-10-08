using PHS.Business.Common;
using PHS.Business.Implementation;
using PHS.Common;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Controllers
{
    public class HomeController : BaseController
    {

        [HttpGet]
        public ActionResult Index()
        {
            LogUserOut();
            return Redirect("~/Home/login");
        }

        // GET: /Home/Login
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login()
        {
            LogUserOut();
            GetErrorAneMessage();
            return View();
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login([Bind(Include = "Username,Password")] PHSUser user)
        {
            ActionResult result = View();
            using (var userManager = new UserManager(GetLoginUser()))
            {
                string message = string.Empty;
                var authenticatedUser = userManager.IsAuthenticated(user.Username, user.Password, out message);
                if (authenticatedUser != null)
                {
                    if (authenticatedUser.UsingTempPW)
                    {
                        authenticatedUser.Role = String.Empty;
                        LogUserIn(authenticatedUser);
                        return RedirectToAction("ChangePassword");
                    }
                    LogUserIn(authenticatedUser);
                    switch (GetLoginUserRole())
                    {
                        case Constants.User_Role_CommitteeMember_Code:
                            result = Redirect("~/Admin/User");
                            break;
                        case Constants.User_Role_Doctor_Code:
                            result = Redirect("~/ParticipantJourney/Index");
                            break;
                        case Constants.User_Role_Volunteer_Code:
                            result = Redirect("~/ParticipantJourney/Index");
                            break;
                    }
                }
                SetViewBagError(message);

                return result;
            }
        }

        public ActionResult LogOff()
        {
            LogUserOut();
            return RedirectToLogin();
        }
    }
}