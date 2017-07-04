using PHS.Business.Common;
using PHS.Business.Implementation;
using PHS.Business.ViewModel;
using PHS.Common;
using PHS.DB;
using PHS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index(string searchBy, string searchString)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;

            using (var personManager = new PersonManager())
            {
                var users = personManager.GetAllPersons(out message);
                GetErrorAneMessage();
                if (users == null)
                {
                    SetViewBagError(message);
                }
                if (!String.IsNullOrEmpty(searchString))
                {
                    if (searchBy == "UserId")
                    {
                        users = personManager.GetPersonsByUserID(searchString, out message);
                    }
                    else
                    {
                        users = personManager.GetPersonsByFullName(searchString, out message);
                    }

                    GetErrorAneMessage();
                }
                return View(users);
            }
        }

        // GET: /Admin/UserDetails
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult UserDetails(int userid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var getPerson = new PersonManager())
            {
                Person person = getPerson.GetPersonByPersonSid(userid, out message);
                if (person == null)
                {
                    SetViewBagError(message);
                }

                SetBackURL("Index");
                return View(person);
            };
        }

        public ActionResult CreateUser()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CreateUser(Person person)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            SetBackURL("Index");

            string message = string.Empty;

            using (var personManager = new PersonManager())
            {
                //TODO remove hardcode
                //person.Password = "12345";
                string tempPassword = PasswordManager.GeneratePassword();
                person.Password = tempPassword;

                var newUser = personManager.AddPerson(GetLoginUser(), person, out message);
                if (newUser == null)
                {
                    SetViewBagError(message);
                    SetBackURL("Index");
                    return View();
                }

                SetTempDataMessage(person.Username + " has been created successfully with password " + tempPassword);
                SetBackURL("Index");
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditUser(int userid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var personManager = new PersonManager())
            {
                var user = personManager.GetPersonByPersonSid(userid, out message);
                if (user == null)
                {
                    SetViewBagError(message);
                }
                else
                {
                    user.Password = string.Empty;
                }
                SetBackURL("Index");
                return View(user);
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult EditUser(Person person)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;

            using (var personManager = new PersonManager())
            {
                if (personManager.UpdatePerson(GetLoginUser(), person, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("User has been updated"));
                    return RedirectToAction("Index");
                }
                //message = "Update failed!";
                SetViewBagError(message);
                SetBackURL("Index");

                return View(person);
            }
        }

        public ActionResult ResetPassword(Person person)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            //TODO implement reset password

            SetTempDataMessage(Constants.ValueSuccessfuly("Password has been reset!"));
            return View("EditUser", person);
        }

        public ActionResult LogOff()
        {
            LogUserOut();
            return RedirectToLogin();
        }
    }
}