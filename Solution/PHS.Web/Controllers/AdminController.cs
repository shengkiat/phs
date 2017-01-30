using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHS.Business.Implementation;
using PHS.DB;
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





    }
}