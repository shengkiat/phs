﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHS.DB;
using System.Reflection;

using log4net;
using PHS.Business.Implementation;

using Microsoft.Owin;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;


namespace PHS.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Property
        public static string UserSessionParam = "LoginUser";
        protected const string LF = "\r\n";
        private const string SEPARATOR = "---------------------------------------------------------------------------------------------------------------";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // 6 hours
        public const int CacheLength = 21600;

        #endregion

        #region Message
        public string TempDataMessage
        {
            get { return TempData["Message"] == null ? null : TempData["Message"].ToString(); }
            set { TempData["Message"] = value; }
        }
        public string TempDataError
        {
            get { return TempData["Error"] == null ? null : TempData["Error"].ToString(); }
            set { TempData["Error"] = value; }
        }
        public string ViewBagMessage
        {
            get { return ViewBag.Message; }
            set { ViewBag.Message = value; }
        }
        public string ViewBagError
        {
            get { return ViewBag.Error; }
            set { ViewBag.Error = value; }
        }
        public void GetErrorAneMessage()
        {
            ViewBagMessage = TempDataMessage;
            ViewBagError = TempDataError;
        }
        public void SetViewBagError(string error)
        {
            TempDataError = null;
            TempDataMessage = null;
            ViewBagError = error;
            ViewBagMessage = null;
        }
        public void SetTempDataError(string error)
        {
            TempDataError = error;
            TempDataMessage = null;
            ViewBagError = null;
            ViewBagMessage = null;
        }
        public void SetViewBagMessage(string message)
        {
            TempDataError = null;
            TempDataMessage = null;
            ViewBagError = null;
            ViewBagMessage = message;
        }
        public void SetTempDataMessage(string message)
        {
            TempDataError = null;
            TempDataMessage = message;
            ViewBagError = null;
            ViewBagMessage = null;
        }

        //public void SetError(string error)
        //{
        //    TempDataError = error;
        //    ViewBagError = error;
        //    TempDataMessage = null;
        //    ViewBagMessage = null;
        //}
        //public void SetMessage(string message)
        //{
        //    TempDataMessage = message;
        //    ViewBagMessage = message;
        //    TempDataError = null;
        //    ViewBagError = null;
        //}
        #endregion

        #region Redirection
        public ActionResult RedirectToError(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }
        public ActionResult RedirectToLogin()
        {
            LogUserOut();
            return Redirect("~/home");
        }
        public ActionResult RedirectToHome()
        {
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            switch (GetLoginUserRole())
            {
                case Common.Constants.User_Role_Doctor_Code:
                    return Redirect("~/doctor");
                    
                case Common.Constants.User_Role_Admin_Code:
                    return Redirect("~/admin/user");
                   
                case Common.Constants.User_Role_Volunteer_Code:
                    return Redirect("~/patientjourney");
                   
                default:
                    return RedirectToLogin();
                    
            }
        }
        public ActionResult RedirectToPreviousURL()
        {
            if (Request.UrlReferrer != null && !string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
            {
                return new RedirectResult(Request.UrlReferrer.ToString());
            }
            return RedirectToError("Unknow error");
        }

        public void SetBackURL(string url)
        {
            ViewBag.BackURL = url;
        }
        #endregion

        #region Access

        public bool IsUserAuthenticated()
        {
            if (TempData.Peek(UserSessionParam) == null)
            {
                if (Session == null || Session[UserSessionParam] == null)
                    return false;

                return false;
            }
            return true;
        }
        public Person GetLoginUser()
        {
            if (TempData.Peek(UserSessionParam) == null)
            {
                if (Session == null || Session[UserSessionParam] == null)
                    return null;

                return Session[UserSessionParam] as Person;
            }
            return TempData.Peek(UserSessionParam) as Person;
        }

        public string GetLoginUserRole()
        {
            if (TempData.Peek(UserSessionParam) == null)
            {
                if (Session == null || Session[UserSessionParam] == null)
                    return null;

                return (Session[UserSessionParam] as Person).Role;
            }
            return (TempData.Peek(UserSessionParam) as Person).Role;
        }

        List<Claim> claims = new List<Claim>();
        ClaimsIdentity identity;
        public void LogUserIn(Person user)
        {
            try
            {
                claims.Add(new Claim(ClaimTypes.PrimarySid, user.PersonID.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.FullName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Username));
                claims.Add(new Claim(ClaimTypes.Role, user.Role));

                identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                {
                    //ExpiresUtc = DateTime.UtcNow.(200),
                    IsPersistent = true
                }, identity);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                //throw;
            }

            if (Session != null)
                Session[UserSessionParam] = user;

            if (!TempData.Keys.Contains(UserSessionParam))
            {
                TempData.Add(UserSessionParam, user);
                InfoLog("User: " + user.Username + " logged in");
            }
            TempData.Keep(UserSessionParam);
        }

        public void LogUserOut()
        {
            HttpContext.Request.Cookies.Clear();
            HttpContext.Response.Cookies.Clear();
            string error = TempDataError;
            string message = TempDataMessage;
            TempData.Clear();
            SetTempDataError(error);
            SetTempDataMessage(message);
            Session.Clear();
        }

        #endregion

        #region Profile

        public ActionResult EditProfile()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            var user = GetLoginUser();
            user.Password = string.Empty;
            return View(user);
        }

        [HttpPost]
        public ActionResult EditProfile(Person person)
        {
            var user = GetLoginUser();
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;

            using (var personManager = new PersonManager())
            {
                if (personManager.UpdatePerson(GetLoginUser(), person, out message))
                {
                    return RedirectToHome();
                }
                SetViewBagError(message);
                return View(person);
            }
        }
        public ActionResult ChangePassword()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            var user = GetLoginUser();
            user.Password = string.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPass, string newPass, string newPassConfirm)
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
                    SetTempDataMessage(("Password has been changed") + ". Please login using the new password");
                    return RedirectToLogin();
                }
            }
        }
        #endregion

        #region log
        public static void ExceptionLog(Exception ex)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (logEngine.IsFatalEnabled && !ex.Message.Contains("Thread was being aborted"))
            {
                sb.Append(ex.Message);
                sb.Append(LF);
                sb.Append(ex.StackTrace);
                sb.Append(LF);
                sb.Append(SEPARATOR);
                logEngine.Error(sb.ToString());
            }
        }

        public static void ExceptionLog(string userLog, Exception ex)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (logEngine.IsFatalEnabled && !ex.Message.Contains("Thread was being aborted"))
            {
                sb.Append(userLog);
                sb.Append(LF);
                sb.Append(ex.Message);
                sb.Append(LF);
                sb.Append(ex.StackTrace);
                sb.Append(LF);
                sb.Append(SEPARATOR);
                logEngine.Error(sb.ToString());
            }
        }

        public static void InfoLog(string message)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);

            if (logEngine.IsInfoEnabled)
            {
                message += LF;
                message += SEPARATOR;
                logEngine.Error(message);
            }
        }

        public static void DebugLog(string message)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);
            if (logEngine.IsDebugEnabled)
            {
                message += LF;
                message += SEPARATOR;
                logEngine.Debug(message);
            }
        }

        public static void ExceptionLog(string message)
        {
            string loggerName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            ILog logEngine = LogManager.GetLogger(loggerName);

            if (logEngine.IsDebugEnabled)
            {
                message += LF;
                message += SEPARATOR;
                logEngine.Debug(message);
            }
        }

        #endregion
    }
}