using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHS.Business.Implementation;
using PHS.DB;
using PHS.Business.ViewModel;
using System.Security.Principal;
using System.Security.Claims;
using PHS.Web.Filter;

using PHS.Business.Interface;
using System.Text;
using PHS.Common;

namespace PHS.Web.Controllers
{
    [CustomAuthorize(Roles = Constants.Admin)]
    public class AdminController : BaseController
    {

        public ActionResult Index()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            return View();
        }

        // GET: /Admin/PersonDetails
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult PersonDetails(int personSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var getPerson = new PersonManager())
            {
                Person person = getPerson.GetPersonByPersonSid(personSid, out message);
                if (person == null)
                {
                    SetViewBagError(message);
                }

                //SetBackURL("UserDetail");
                return View(person);
            };
        }

        [HttpGet, ActionName("CreateUser")]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CreateUser()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            return View();
        }

        [HttpPost, ActionName("CreateUser")]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SaveNewUser(Person person)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            SetBackURL("SearchUser");

            string message = string.Empty;

            using (var personManager = new PersonManager())
            {
                //TODO remove hardcode
                person.Password = "12345";
                person.Role = Constants.User_Role_Student_Code;

                var newUser = personManager.AddPerson(person, out message);
                if (newUser == null)
                {
                    SetViewBagError(message);
                    SetBackURL("SearchUser");
                    return View();
                }
                else
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Student has been created"));
                    return RedirectToAction("SearchUser");
                }
            }

        }

        public ActionResult EditUser(int userSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var personManager = new PersonManager())
            {
                var user = personManager.GetPersonByPersonSid(userSid, out message);
                if (user == null)
                {
                    SetViewBagError(message);
                }
                else
                {
                    user.Password = string.Empty;
                }
                SetBackURL("SearchUser");
                return View(user);
            };
        }

        [HttpPost, ActionName("EditUser")]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult updateUser(Person person)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;

            using (var personManager = new PersonManager())
            {
                if (personManager.UpdatePerson(person, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("User has been updated"));
                    return RedirectToAction("SearchUser");
                }
                SetViewBagError(message);
                SetBackURL("SearchUser");
                return View();
            }
        }

        public ActionResult BackToSearchUser()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            return View("SearchUser");
        }


        // Both GET and POST: /Admin/SearchUser
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SearchUser(UserSearchModel usm)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            if(usm == null)
            {
                return View();
            }

            string message = string.Empty;
            UserSearchModel result = new UserSearchModel();

            using (var getPerson = new PersonManager())
            {
                IList<Person> persons = null;

                if ("FullName".Equals(usm.SearchBy))
                {
                    persons = getPerson.GetPersonsByFullName(usm.Content, out message);
                    if (persons == null)
                    {
                        SetViewBagError(message);
                        return View(result);
                    }

                }else
                {
                    Person person = getPerson.GetPersonByUserName(usm.Content, out message);
                    if(person != null)
                    {
                        persons = new List<Person>();
                        persons.Add(person);
                    }
                }

                result.persons = persons;

                return View(result);
            };
        }


    }
}