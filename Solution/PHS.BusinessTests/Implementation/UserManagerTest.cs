using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
using PHS.BusinessTests;
using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PHS.Common.Constants;

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
        public void AddUser_Success()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            var getUser = _target.GetUserByUserName(user.Username, out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual("testUsername", getUser.Username);
            Assert.AreEqual("testFullName", getUser.FullName);
            Assert.IsNotNull(getUser.Password);
            Assert.AreEqual("Volunteer", getUser.Role);
            Assert.IsNotNull(getUser.CreatedDateTime);
            Assert.AreEqual(_loginuser.Username, getUser.CreatedBy);
            Assert.IsTrue(getUser.UsingTempPW);
        }

        [TestMethod()]
        public void AddUser_NullUserReturnError()
        {
            string message = string.Empty;
            PHSUser user = null;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void AddUser_NullOrEmptyUsernameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Username = "";
            success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Username = " ";
            success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void AddUser_ExistingUsernameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername";
            user1.FullName = "testFullName";
            user1.Password = "testpassword";
            user1.Role = "Volunteer";

            success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Username already exists", message);
        }

        [TestMethod()]
        public void AddUser_NullOrEmptyFullnameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.FullName = "";
            success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.FullName = " ";
            success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void AddUser_NullOrEmptyRoleReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.Password = "testpassword";
            user.FullName = "testFullname";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Role = "";
            success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void AddUser_EffectiveStartDateLessThanEndDateReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullname";
            user.Password = "testpassword";
            user.Role = "Volunteer";
            user.EffectiveStartDate = Convert.ToDateTime("08/01/2017");
            user.EffectiveEndDate = Convert.ToDateTime("07/01/2017");

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsFalse(success);
        }

        [TestMethod()]
        public void GetAllUsers_Success()
        {
            string message = string.Empty;
            var preExecuteResult = _target.GetAllUsers(out message);
            Assert.IsNull(preExecuteResult);
            Assert.AreEqual("There is no User found.", message);

            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = "Volunteer";

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            var postExecuteResult = _target.GetAllUsers(out message);
            Assert.IsNotNull(postExecuteResult);
            Assert.AreEqual(2, postExecuteResult.Count());
            Assert.AreEqual("", message);
        }

        [TestMethod()]
        public void GetUserByID_Success()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = "Volunteer";
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = "Volunteer";
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser getUser = _target.GetUserByID(user2.PHSUserID, out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual(user2, getUser);
            Assert.AreEqual("", message);
        }

        [TestMethod()]
        public void GetUserByID_WrongIDReturnError()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = "Volunteer";
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            var getUser = _target.GetUserByID(5, out message);
            Assert.IsNull(getUser);
            Assert.AreEqual("User not found.", message);
        }

        [TestMethod()]
        public void GetUserByUserName_Success()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = "Volunteer";
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = "Volunteer";
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);
            var getUser = _target.GetUserByUserName("testUsername2", out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual("", message);
            Assert.AreEqual(user2, getUser);
        }

        [TestMethod()]
        public void GetUserByUserName_WrongUserNameReturnError()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = "Volunteer";
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = "Volunteer";
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);
            var getUser = _target.GetUserByUserName("testUsername3", out message);
            Assert.IsNull(getUser);
            Assert.AreEqual("User not found.", message);
        }

        [TestMethod()]
        public void GetUsersByUserName_Success()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = "Volunteer";
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = "Volunteer";
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);
            var getUser = _target.GetUsersByUserName("Username", out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual("", message);
            Assert.AreEqual(2, getUser.Count());
        }

        [TestMethod()]
        public void GetUsersByUserName_WrongUserNameReturnError()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = "Volunteer";
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = "Volunteer";
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);
            var getUser = _target.GetUsersByUserName("testFullname2", out message);
            Assert.IsNull(getUser);
            Assert.AreEqual("User not found.", message);
        }

        [TestMethod()]
        public void GetUsersByFullName_Success()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = "Volunteer";
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = "Volunteer";
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);
            var getUser = _target.GetUsersByFullName("testFullname", out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual("", message);
            Assert.AreEqual(2, getUser.Count());
        }

        [TestMethod()]
        public void GetUsersByFullName_WrongUserNameReturnError()
        {
            string message = string.Empty;
            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullname1";
            user1.Password = "testPassword1";
            user1.Role = "Volunteer";
            user1.IsActive = false;

            var success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser user2 = new PHSUser();
            user2.Username = "testUsername2";
            user2.FullName = "testFullname2";
            user2.Password = "testPassword2";
            user2.Role = "Volunteer";
            user2.IsActive = true;

            success = _target.AddUser(_loginuser, user2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);
            var getUser = _target.GetUsersByFullName("testUsername2", out message);
            Assert.IsNull(getUser);
            Assert.AreEqual("User not found.", message);
        }

        [TestMethod()]
        public void UpdateUser_Success()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            user.Username = "newUsername";
            user.FullName = "newFullName";
            user.Password = "newpassword";
            user.Role = "Doctor";

            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            var getUser = _target.GetUserByUserName(user.Username, out message);
            Assert.IsNotNull(getUser);
            Assert.AreEqual("newUsername", getUser.Username);
            Assert.AreEqual("newFullName", getUser.FullName);
            Assert.IsNotNull(getUser.Password);
            Assert.AreEqual("Doctor", getUser.Role);
            Assert.IsNotNull(getUser.CreatedDateTime);
            Assert.AreEqual(_loginuser.Username, getUser.CreatedBy);
            Assert.IsTrue(getUser.UsingTempPW);
            Assert.IsNotNull(getUser.UpdatedDateTime);
            Assert.AreEqual(_loginuser.Username, getUser.UpdatedBy);
        }

        [TestMethod()]
        public void UpdateUser_NullUserReturnError()
        {
            string message = string.Empty;
            PHSUser user = null;

            var success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void UpdateUser_NullOrEmptyUsernameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            user.Username = null;
            
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Username = "";

            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Username = " ";

            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void UpdateUser_ExistingUsernameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            PHSUser user1 = new PHSUser();
            user1.Username = "testUsername1";
            user1.FullName = "testFullName1";
            user1.Password = "testpassword1";
            user1.Role = "Volunteer";

            success = _target.AddUser(_loginuser, user1, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            user1.Username = "testUsername";

            success = _target.UpdateUser(_loginuser, user1, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Username already exists", message);
        }

        [TestMethod()]
        public void UpdateUser_NullorEmptyFullnameReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.Password = "testpassword";
            user.FullName = "testFullname";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            user.FullName = null;
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.FullName = "";
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.FullName = " ";
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void UpdateUser_NullOrEmptyRoleReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.Password = "testpassword";
            user.FullName = "testFullname";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            user.Role = null;
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Role = "";
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);

            user.Role = " ";
            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void UpdateUser_EffectiveStartDateLessThanEndDateReturnError()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullname";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            user.EffectiveStartDate = Convert.ToDateTime("08/01/2017");
            user.EffectiveEndDate = Convert.ToDateTime("07/01/2017");

            success = _target.UpdateUser(_loginuser, user, out message);
            Assert.IsFalse(success);
        }

        [TestMethod()]
        public void DeleteUser_Success()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            success = _target.DeleteUser(user.PHSUserID, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            var getUser = _target.GetUserByUserName(user.Username, out message);
            Assert.IsNull(getUser);
            Assert.AreEqual("User not found.", message);
        }

        [TestMethod()]
        public void DeleteUser_ReturnError()
        {
            string message = string.Empty;

            var success = _target.DeleteUser(5, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("User not found.", message);
        }


        [TestMethod()]
        public void UserNameExists_ReturnTrue()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            success = _target.UserNameExists("testUsername", 2, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("testUsername already exists", message);
        }

        [TestMethod()]
        public void UserNameExists_ReturnFalse()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            success = _target.UserNameExists("testUsername1", 0, out message);
            Assert.IsFalse(success);
            Assert.AreEqual("", message);
        }

        [TestMethod()]
        public void GetCurrentActiveStatus_FalseFlagReturnFalse()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";
            user.IsActive = false;

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            success = _target.GetCurrentActiveStatus(user);
            Assert.IsFalse(success);
        }

        [TestMethod()]
        public void GetCurrentActiveStatus_TrueFlagDateInRangeReturnTrue()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";
            user.IsActive = true;
            user.EffectiveStartDate = Convert.ToDateTime("08/01/2017");
            user.EffectiveEndDate = Convert.ToDateTime("09/01/2017");

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

            success = _target.GetCurrentActiveStatus(user);
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void GetCurrentActiveStatus_TrueFlagDateNotInRangeReturnTrue()
        {
            string message = string.Empty;
            PHSUser user = new PHSUser();
            user.Username = "testUsername";
            user.FullName = "testFullName";
            user.Password = "testpassword";
            user.Role = "Volunteer";
            user.IsActive = true;
            user.EffectiveStartDate = Convert.ToDateTime("07/01/2017");
            user.EffectiveEndDate = Convert.ToDateTime("08/01/2017");

            var success = _target.AddUser(_loginuser, user, out message);
            Assert.IsTrue(success);
            Assert.AreEqual("", message);

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
            _loginuser.Role = "Committee Member";
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
