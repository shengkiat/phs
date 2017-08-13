using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHS.Business.Common;
using PHS.Web.Controllers;

namespace PHS.Web.Filter
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        Entities context = new Entities(); // my entity  
        private string allowedroles;
        public PHSUser user { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            user = httpContext.Session[BaseController.UserSessionParam] as PHSUser;

            if (user == null)
            {
                return false;
            }
            else
            {
                if (!allowedroles.Contains(user.Role))
                {
                    return false;
                }
                return true;
            }
        }

        public new string Roles
        {
            get
            {
                return allowedroles;
            }
            set
            {
                allowedroles = value;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }


}