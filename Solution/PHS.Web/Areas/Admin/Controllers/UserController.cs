using PHS.Business.Common;
using PHS.Business.Implementation;
using PHS.Business.ViewModel;
using PHS.Common;
using PHS.DB;
using PHS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            using (var userManager = new UserManager(GetLoginUser()))
            {
                var users = userManager.GetAllUsers(out message);
                GetErrorAneMessage();
                if (users == null)
                {
                    SetViewBagError(message);
                }
                if (!String.IsNullOrEmpty(searchString))
                {
                    if (searchBy == "UserId")
                    {
                        users = userManager.GetUsersByUserName(searchString, out message);
                    }
                    else
                    {
                        users = userManager.GetUsersByFullName(searchString, out message);
                    }

                    GetErrorAneMessage();
                }
                return View(users);
            }
        }

        // GET: /Admin/UserDetails
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult UserDetails(int userID = 0)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var userManager = new UserManager(GetLoginUser()))
            {
                PHSUser user = userManager.GetUserByID(userID, out message);
                if (user == null)
                {
                    SetViewBagError(message);
                }
                else
                {
                    user.Password = string.Empty;
                }
                return View(user);
            }
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
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
        public ActionResult CreateUser([Bind(Include = "Username,FullName,ContactNumber,Role,IsActive,EffectiveStartDate,EffectiveEndDate")] PHSUser user)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            SetBackURL("Index");

            string message = string.Empty;

            using (var userManager = new UserManager(GetLoginUser()))
            {
                string tempPassword = PasswordManager.Generate();
                user.Password = tempPassword;

                var newUserCreated = userManager.AddUser(GetLoginUser(), user, out message);
                if (!newUserCreated)
                {
                    SetViewBagError(message);
                    SetBackURL("Index");
                    return View();
                }

                SetTempDataMessage(user.Username + " has been created successfully with password " + tempPassword);
                SetBackURL("Index");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult EditUser(int userID = 0)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var userManager = new UserManager(GetLoginUser()))
            {
                var user = userManager.GetUserByID(userID, out message);
                if (user == null)
                {
                    SetViewBagError(message);
                }
                else
                {
                    user.Password = string.Empty;
                }
                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult EditUser([Bind(Include = "Username,FullName,ContactNumber,Role,IsActive,EffectiveStartDate,EffectiveEndDate,PHSUserID,CreatedDateTime")] PHSUser user)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;

            using (var userManager = new UserManager(GetLoginUser()))
            {
                if (userManager.UpdateUser(GetLoginUser(), user, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("User has been updated"));
                    return RedirectToAction("Index");
                }
                //message = "Update failed!";
                SetViewBagError(message);
                SetBackURL("Index");

                return View(user);
            }
        }

        public ActionResult DeleteUser(int userID = 0)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;

            using (var userManager = new UserManager(GetLoginUser()))
            {
                if (userManager.DeleteUser(userID, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("User has been deleted"));
                    return RedirectToAction("Index");
                }
                SetViewBagError(message);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Activate(String[] selectedusers, DateTime startDate, DateTime endDate)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            if (selectedusers == null || selectedusers.Length == 0)
            {
                SetTempDataMessage("No Selection made!");
                return View();
            }

            string message = string.Empty;
            using (var userManager = new UserManager(GetLoginUser()))
            {

                foreach (var username in selectedusers)
                {
                    var user = userManager.GetUserByUserName(username.ToString(), out message);
                    user.IsActive = true;
                    user.EffectiveStartDate = startDate;
                    user.EffectiveEndDate = endDate;
                    if (!userManager.UpdateUser(GetLoginUser(), user, out message))
                    {
                        SetViewBagError(message);
                    }
                }
            }

            SetTempDataMessage("Users are set Active!");
            return View();
        }

        [HttpPost]
        public ActionResult Inactivate(String[] selectedusers)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            if (selectedusers == null || selectedusers.Length == 0)
            {
                SetTempDataMessage("No Selection made!");
                return View();
            }
            string message = string.Empty;
            using (var userManager = new UserManager(GetLoginUser()))
            {

                foreach (var username in selectedusers)
                {
                    var user = userManager.GetUserByUserName(username.ToString(), out message);
                    user.IsActive = false;
                    if (!userManager.UpdateUser(GetLoginUser(), user, out message))
                    {
                        SetViewBagError(message);
                    }
                }
            }

            SetTempDataMessage("Users are set Inactive!");
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(String[] selectedusers)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            if (selectedusers == null || selectedusers.Length == 0)
            {
                TempData["ResetPasswordMessage"] = "No Selection made!";
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Error = "No Selection made!" });
            }

            string message = string.Empty;
            string tempPW = PasswordManager.Generate();

            using (var userManager = new UserManager(GetLoginUser()))
            {
                bool isResetPassword = userManager.ResetPassword(GetLoginUser(), selectedusers, tempPW, out message);
                if (!isResetPassword)
                {
                    SetViewBagError(message);
                    TempData["ResetPasswordMessage"] = message;
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Error = message });
                }
            }

            TempData["ResetPasswordMessage"] = "Password has been reset to " + tempPW;
            return Json(new { Success = "Success" });
        }
    }
}