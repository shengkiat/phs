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

        [TestMethod()]
        public void AddUserTest_Success()
        {
            string username = "tester";

            PHSUser user = new PHSUser()
            {
                Username = username,
                FullName = "tester 123",
                Password = PasswordManager.GeneratePassword(),
                IsActive = true,
                EffectiveStartDate = DateTime.Now,
                EffectiveEndDate = DateTime.Now.AddDays(1),
                Role = Constants.User_Role_Volunteer_Code

            };

            PHSUser tempUser = new PHSUser()
            {
                Username = "login user"
            };

            string message = string.Empty;

            bool saveResult = _target.AddUser(tempUser, user, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            PHSUser expectedResult = _target.GetUserByUserName(username, out message);
            Assert.IsNotNull(expectedResult);
            Assert.IsNotNull(expectedResult.PasswordSalt);
        }

        [TestMethod()]
        public void IsAuthenticatedTest_Success()
        {
            string username = "tester";
            string password = "abcde12345";

            PHSUser user = new PHSUser()
            {
                Username = username,
                FullName = "tester 123",
                Password = password,
                IsActive = true,
                EffectiveStartDate = DateTime.Now,
                EffectiveEndDate = DateTime.Now.AddDays(1),
                Role = Constants.User_Role_Volunteer_Code

            };

            PHSUser tempUser = new PHSUser()
            {
                Username = "login user"
            };

            string message = string.Empty;

            bool saveResult = _target.AddUser(tempUser, user, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            PHSUser expectedResult = _target.IsAuthenticated(username, password, out message);
            Assert.IsNotNull(expectedResult);
            Assert.AreEqual("tester 123", expectedResult.FullName);
        }


        [TestMethod()]
        public void ChangePasswordTest_Success()
        {
            string username = "tester";
            string password = "abcde12345";

            PHSUser user = new PHSUser()
            {
                Username = username,
                FullName = "tester 123",
                Password = password,
                IsActive = true,
                EffectiveStartDate = DateTime.Now,
                EffectiveEndDate = DateTime.Now.AddDays(1),
                Role = Constants.User_Role_Volunteer_Code

            };

            PHSUser tempUser = new PHSUser()
            {
                Username = "login user"
            };

            string message = string.Empty;

            bool saveResult = _target.AddUser(tempUser, user, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            PHSUser loginUser = _target.IsAuthenticated(username, password, out message);
            Assert.IsNotNull(loginUser);
            Assert.IsTrue(loginUser.UsingTempPW);

            string newPassword = "Aabbccdd@1122";

            bool changeResult = _target.ChangePassword(loginUser, password, newPassword, newPassword, out message);
            Assert.IsTrue(changeResult);
            Assert.AreEqual(string.Empty, message);

            loginUser = _target.IsAuthenticated(username, newPassword, out message);
            Assert.IsNotNull(loginUser);
            Assert.IsFalse(loginUser.UsingTempPW);
        }

        [TestMethod()]
        public void ResetPasswordTest()
        {
            string username = "tester";
            string password = "abcde12345";

            PHSUser user = new PHSUser()
            {
                Username = username,
                FullName = "tester 123",
                Password = password,
                IsActive = true,
                EffectiveStartDate = DateTime.Now,
                EffectiveEndDate = DateTime.Now.AddDays(1),
                Role = Constants.User_Role_Volunteer_Code

            };

            PHSUser tempUser = new PHSUser()
            {
                Username = "login user"
            };

            string message = string.Empty;

            bool saveResult = _target.AddUser(tempUser, user, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            PHSUser loginUser = _target.IsAuthenticated(username, password, out message);
            Assert.IsNotNull(loginUser);

            string newPassword = "Aabbccdd@1122";
            string[] selectedUsers = new string[1];
            selectedUsers[0] = username;

            bool resetResult = _target.ResetPassword(tempUser, selectedUsers, newPassword, out message);
            Assert.IsTrue(resetResult);
            Assert.AreEqual(string.Empty, message);

            loginUser = _target.IsAuthenticated(username, newPassword, out message);
            Assert.IsNotNull(loginUser);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockUserManager(_unitOfWork);
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
        }

        private class MockUserManager : UserManager
        {
            private IUnitOfWork _unitOfWork;

            public MockUserManager(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            protected override IUnitOfWork CreateUnitOfWork()
            {
                return _unitOfWork;
            }
        }


    }
}