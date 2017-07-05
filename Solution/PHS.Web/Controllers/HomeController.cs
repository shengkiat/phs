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
        public ActionResult Login(Person person)
        {
            ActionResult result = View();
            using (var personManager = new PersonManager())
            {
                string message = string.Empty;
                var authenticatedUser = personManager.IsAuthenticated(person.Username, person.Password, out message);
                if (authenticatedUser != null)
                {
                    LogUserIn(authenticatedUser);
                    result = Redirect("~/Home/Reset");
                }
                SetViewBagError(message);

                return result;
            }
        }

        // GET: /Home/Reset
        [HttpGet]
        public ActionResult Reset()
        {
            Person loginuser = GetLoginUser();
            if (loginuser == null)
            {
                return RedirectToLogin();
            }
            ActionResult result = View();
            if (loginuser.UsingTempPW)
            {
                result = View();
            }
            else
            {
                switch (GetLoginUserRole())
                {
                    case Constants.User_Role_Admin_Code:
                        result = Redirect("~/Admin/User");
                        break;
                    case Constants.User_Role_Doctor_Code:
                        result = Redirect("~/PatientJourney/SearchPatient");
                        break;
                    case Constants.User_Role_Volunteer_Code:
                        result = Redirect("~/PatientJourney/SearchPatient");
                        break;
                }
            }
            return result;
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Reset(string oldPass, string newPass, string newPassConfirm)
        {
            var user = GetLoginUser();
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var userManager = new PersonManager())
            {
                if (!userManager.ChangePassword(user, oldPass, newPass, newPassConfirm, out message))//(GetLoginUser().Username, oldPass, out message) == null)
                {
                    SetViewBagError(message);
                    return View();
                }
                else
                {
                    ActionResult result = View();
                    switch (GetLoginUserRole())
                    {
                        case Constants.User_Role_Admin_Code:
                            result = Redirect("~/Admin/User");
                            break;
                        case Constants.User_Role_Doctor_Code:
                            result = Redirect("~/PatientJourney/SearchPatient");
                            break;
                        case Constants.User_Role_Volunteer_Code:
                            result = Redirect("~/PatientJourney/SearchPatient");
                            break;
                    }
                    return result;
                }
            }
        }

        public ActionResult LogOff()
        {
            LogUserOut();
            return RedirectToLogin();
        }


    }
}