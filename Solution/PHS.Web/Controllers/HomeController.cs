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
                    if (authenticatedUser.UsingTempPW)
                    {
                        return RedirectToAction("ChangePassword");
                    }
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