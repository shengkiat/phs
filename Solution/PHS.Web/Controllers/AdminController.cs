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

        // Both GET and POST: /Admin/SearchUser
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SearchUser(UserSearchModel usm)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            UserSearchModel result = new UserSearchModel();

            using (var getPerson = new PersonManager())
            {
                IList<Person> persons = null;

                if ("FullName".Equals(usm.SearchBy))
                {
                    persons = getPerson.GetPersonsByUserName(usm.Content, out message);
                    if (persons == null)
                    {
                        SetViewBagError(message);
                        return View(result);
                    }

                }

                result.persons = persons;

                return View(result);
            };
        }


    }
}