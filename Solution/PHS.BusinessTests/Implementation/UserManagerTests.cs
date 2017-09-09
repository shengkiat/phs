using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Common;
using PHS.Business.Implementation;
using PHS.BusinessTests;
using PHS.Common;
using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class UserManagerTests
    {
        private UserManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;
        private PHSUser _loginuser;

        [TestMethod()]
        public void IsAuthenticatedTest_Success()
        {
            string username = "tester";
            string stuff = "abcde12345";

            PHSUser user = new PHSUser()
            {
                Username = username,
                FullName = "tester 123",
                Password = stuff,
                IsActive = true,
                EffectiveStartDate = DateTime.Now,
                EffectiveEndDate = DateTime.Now.AddDays(1),
                Role = Constants.User_Role_Volunteer_Code

            };

            string message = string.Empty;

            bool saveResult = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            PHSUser expectedResult = _target.IsAuthenticated(username, stuff, out message);
            Assert.IsNotNull(expectedResult);
            Assert.AreEqual("tester 123", expectedResult.FullName);
        }


        [TestMethod()]
        public void ChangePasswordTest_Success()
        {
            string username = "tester";
            string stuff = "abcde12345";

            PHSUser user = new PHSUser()
            {
                Username = username,
                FullName = "tester 123",
                Password = stuff,
                IsActive = true,
                EffectiveStartDate = DateTime.Now,
                EffectiveEndDate = DateTime.Now.AddDays(1),
                Role = Constants.User_Role_Volunteer_Code

            };

            string message = string.Empty;

            bool saveResult = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            PHSUser loginUser = _target.IsAuthenticated(username, stuff, out message);
            Assert.IsNotNull(loginUser);
            Assert.IsTrue(loginUser.UsingTempPW);

            string newPassword = "Aabbccdd@1122";

            bool changeResult = _target.ChangePassword(loginUser, stuff, newPassword, newPassword, out message);
            Assert.IsTrue(changeResult);
            Assert.AreEqual(string.Empty, message);

            loginUser = _target.IsAuthenticated(username, newPassword, out message);
            Assert.IsNotNull(loginUser);
            Assert.IsFalse(loginUser.UsingTempPW);
        }

        [TestMethod()]
        public void ResetPasswordTest_Success()
        {
            string username = "tester";
            string stuff = "abcde12345";

            PHSUser user = new PHSUser()
            {
                Username = username,
                FullName = "tester 123",
                Password = stuff,
                IsActive = true,
                EffectiveStartDate = DateTime.Now,
                EffectiveEndDate = DateTime.Now.AddDays(1),
                Role = Constants.User_Role_Volunteer_Code

            };

            string message = string.Empty;

            bool saveResult = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            PHSUser loginUser = _target.IsAuthenticated(username, stuff, out message);
            Assert.IsNotNull(loginUser);

            string newPassword = PasswordManager.GeneratePassword();
            string[] selectedUsers = new string[1];
            selectedUsers[0] = username;

            bool resetResult = _target.ResetPassword(_loginuser, selectedUsers, newPassword, out message);
            Assert.IsTrue(resetResult);
            Assert.AreEqual(string.Empty, message);

            loginUser = _target.IsAuthenticated(username, newPassword, out message);
            Assert.IsNotNull(loginUser);
        }

        [TestMethod()]
        public void AddUserTest_Success()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            var expectedResult = _target.GetUserByUserName(user.Username, out message);
            Assert.IsNotNull(expectedResult);
            Assert.AreEqual("testUsername", expectedResult.Username);
            Assert.AreEqual("testFullName", expectedResult.FullName);
            Assert.IsNotNull(expectedResult.Password);
            Assert.IsNotNull(expectedResult.PasswordSalt);
            Assert.AreEqual(Constants.User_Role_Volunteer_Code, expectedResult.Role);
            Assert.IsNotNull(expectedResult.CreatedDateTime);
            Assert.AreEqual(_loginuser.Username, expectedResult.CreatedBy);
            Assert.IsTrue(expectedResult.UsingTempPW);
        }

        [TestMethod()]
        public void AddUserTest_NullUserReturnError()
        {
            string message = string.Empty;
            PHSUser user = null;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void AddUserTest_NullOrEmptyUsernameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Username = string.Empty;
            success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Username = " ";
            success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void AddUserTest_ExistingUsernameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername";
            user1.FullName = "testFullName";
            user1.Password = "testpassword";
            user1.Role = Constants.User_Role_Volunteer_Code;

            success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Username already exists", message);
        }

        [TestMethod()]
        public void AddUserTest_NullOrEmptyFullnameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.FullName = string.Empty;
            success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.FullName = " ";
            success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void AddUserTest_NullOrEmptyRoleReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.Password = "testpassword";
            user.FullName = "testFullname";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Role = string.Empty;
            success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void AddUserTest_EffectiveStartDateLessThanEndDateReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullname";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;
            user.EffectiveStartDate = Convert.ToDateTime("08/01/2017");
            user.EffectiveEndDate = Convert.ToDateTime("07/01/2017");

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
        }

        [TestMethod()]
        public void GetAllUsersTest_Success()
        {
            string message = string.Empty;
            var preExecuteResult = _target.GetAllUsers(out message);
            Assert.IsNull(preExecuteResult);
            Assert.AreEqual("There is no User found.", message);

            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = Constants.User_Role_Volunteer_Code;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            var postExecuteResult = _target.GetAllUsers(out message);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(2, postExecuteResult.Count());
            Assert.AreEqual(string.Empty, message);
        }

        [TestMethod()]
        public void GetUserByIDTest_Success()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = Constants.User_Role_Volunteer_Code;
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = Constants.User_Role_Volunteer_Code;
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser getUser = _target.GetUserByID(user2.PHSUserID, out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual(user2, getUser);
            Assert.AreEqual(string.Empty, message);
        }

        [TestMethod()]
        public void GetUserByIDTest_WrongIDReturnError()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = Constants.User_Role_Volunteer_Code;
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            var getUser = _target.GetUserByID(5, out message);
            Assert.IsNull(getUser);
            Assert.AreEqual("User not found.", message);
        }

        [TestMethod()]
        public void GetUserByUserNameTest_Success()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = Constants.User_Role_Volunteer_Code;
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = Constants.User_Role_Volunteer_Code;
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);
            var getUser = _target.GetUserByUserName("testUsername2", out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual(string.Empty, message);
            Assert.AreEqual(user2, getUser);
        }

        [TestMethod()]
        public void GetUserByUserNameTest_WrongUserNameReturnError()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = Constants.User_Role_Volunteer_Code;
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = Constants.User_Role_Volunteer_Code;
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);
            var getUser = _target.GetUserByUserName("testUsername3", out message);
            Assert.IsNull(getUser);
            Assert.AreEqual("User not found.", message);
        }

        [TestMethod()]
        public void GetUsersByUserNameTest_Success()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = Constants.User_Role_Volunteer_Code;
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = Constants.User_Role_Volunteer_Code;
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);
            var getUser = _target.GetUsersByUserName("Username", out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual(string.Empty, message);
            Assert.AreEqual(2, getUser.Count());
        }

        [TestMethod()]
        public void GetUsersByUserNameTest_WrongUserNameReturnError()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = Constants.User_Role_Volunteer_Code;
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = Constants.User_Role_Volunteer_Code;
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);
            var getUser = _target.GetUsersByUserName("testFullname2", out message);
            Assert.IsNull(getUser);
            Assert.AreEqual("User not found.", message);
        }

        [TestMethod()]
        public void GetUsersByFullNameTest_Success()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = Constants.User_Role_Volunteer_Code;
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = Constants.User_Role_Volunteer_Code;
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);
            var getUser = _target.GetUsersByFullName("testFullname", out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual(string.Empty, message);
            Assert.AreEqual(2, getUser.Count());
        }

        [TestMethod()]
        public void GetUsersByFullNameTest_WrongUserNameReturnError()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = Constants.User_Role_Volunteer_Code;
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = Constants.User_Role_Volunteer_Code;
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);
            var getUser = _target.GetUsersByFullName("testUsername2", out message);
            Assert.IsNull(getUser);
            Assert.AreEqual("User not found.", message);
        }

        [TestMethod()]
        public void UpdateUserTest_Success()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            user.Username = "newUsername";
            user.FullName = "newFullName";
            user.Password = "newpassword";
            user.Role = Constants.User_Role_Doctor_Code;

            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            var getUser = _target.GetUserByUserName(user.Username, out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual("newUsername", getUser.Username);
            Assert.AreEqual("newFullName", getUser.FullName);
            Assert.IsNotNull(getUser.Password);
            Assert.AreEqual(Constants.User_Role_Doctor_Code, getUser.Role);
            Assert.IsNotNull(getUser.CreatedDateTime);
            Assert.AreEqual(_loginuser.Username, getUser.CreatedBy);
            Assert.IsTrue(getUser.UsingTempPW);
            Assert.IsNotNull(getUser.UpdatedDateTime);
            Assert.AreEqual(_loginuser.Username, getUser.UpdatedBy);
        }

        [TestMethod()]
        public void UpdateUserTest_NullUserReturnError()
        {
            string message = string.Empty;
            PHSUser user = null;

            var success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void UpdateUserTest_NullOrEmptyUsernameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            user.Username = null;

            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Username = string.Empty;

            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Username = " ";

            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void UpdateUserTest_ExistingUsernameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullName1";
            user1.Password = "testpassword1";
            user1.Role = Constants.User_Role_Volunteer_Code;

            success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            user1.Username = "testUsername";

            success = _target.UpdateUser(_loginuser, user1, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Username already exists", message);
        }

        [TestMethod()]
        public void UpdateUserTest_NullorEmptyFullnameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.Password = "testpassword";
            user.FullName = "testFullname";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            user.FullName = null;
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.FullName = string.Empty;
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.FullName = " ";
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void UpdateUserTest_NullOrEmptyRoleReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.Password = "testpassword";
            user.FullName = "testFullname";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            user.Role = null;
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Role = string.Empty;
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Role = " ";
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void UpdateUserTest_EffectiveStartDateLessThanEndDateReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullname";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            user.EffectiveStartDate = Convert.ToDateTime("08/01/2017");
            user.EffectiveEndDate = Convert.ToDateTime("07/01/2017");

            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
        }

        [TestMethod()]
        public void DeleteUserTest_Success()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            success = _target.DeleteUser(user.PHSUserID, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            var getUser = _target.GetUserByUserName(user.Username, out message);
            Assert.IsNull(getUser);
            Assert.AreEqual("User not found.", message);
        }

        [TestMethod()]
        public void DeleteUserTest_ReturnError()
        {
            string message = string.Empty;

            var success = _target.DeleteUser(5, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("User not found.", message);
        }


        [TestMethod()]
        public void UserNameExistsTest_ReturnTrue()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            success = _target.UserNameExists("testUsername", 2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("testUsername already exists", message);
        }

        [TestMethod()]
        public void UserNameExistsTest_ReturnFalse()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            success = _target.UserNameExists("testUsername1", 0, out message);
            Assert.IsFalse(success);
            Assert.AreEqual(string.Empty, message);
        }

        [TestMethod()]
        public void GetCurrentActiveStatusTest_FalseFlagReturnFalse()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;
            user.IsActive = false;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            success = _target.GetCurrentActiveStatus(user);
            Assert.IsFalse(success);
        }

        [TestMethod()]
        public void GetCurrentActiveStatusTest_TrueFlagDateInRangeReturnTrue()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;
            user.IsActive = true;

            user.EffectiveStartDate = DateTime.Now.AddDays(-1);
            user.EffectiveEndDate = DateTime.Now.AddDays(1);
           // user.EffectiveStartDate = Convert.ToDateTime("08/01/2017");
          //  user.EffectiveEndDate = Convert.ToDateTime("09/01/2017");

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            success = _target.GetCurrentActiveStatus(user);
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void GetCurrentActiveStatusTest_TrueFlagDateNotInRangeReturnTrue()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = Constants.User_Role_Volunteer_Code;
            user.IsActive = true;
            user.EffectiveStartDate = Convert.ToDateTime("07/01/2017");
            user.EffectiveEndDate = Convert.ToDateTime("08/01/2017");

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, message);

            success = _target.GetCurrentActiveStatus(user);
            Assert.IsFalse(success);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockUserManager(_unitOfWork);

            _loginuser = new PHSUser();
            _loginuser.Username = "loginUsername";
            _loginuser.FullName = "loginFullName";
            _loginuser.Password = "loginpassword";
            _loginuser.Role = Constants.User_Role_CommitteeMember_Code;
        }

        [TestCleanup]
        public void CleanupTest()
        {
            // dispose of the database and connection
            _context.Dispose();
            _unitOfWork.Dispose();
            _target.Dispose();

            _unitOfWork = null;
            _context = null;
            _target = null;
            _loginuser = null;
        }

        private class MockUserManager : UserManager
        {
            private IUnitOfWork _unitOfWork;

            public MockUserManager(IUnitOfWork _unitOfWork)
            {
                this._unitOfWork = _unitOfWork;
            }

            protected override IUnitOfWork CreateUnitOfWork()
            {
                return _unitOfWork;
            }
        }
    }
}