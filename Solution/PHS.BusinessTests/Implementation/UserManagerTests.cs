using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Common;
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

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class UserManagerTests
    {
        private UserManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        //[TestMethod()]
        public void ChangePasswordTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddUserTest_success()
        {
            string username = "tester";

            PHSUser user = new PHSUser()
            {
                Username = username,
                FullName = "tester 123",
                Password = PasswordManager.GeneratePassword()
            
            };

            PHSUser loginuser = new PHSUser()
            {
                Username = "login user"
            };

            string message = string.Empty;

            bool saveResult = _target.AddUser(loginuser, user, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            PHSUser expectedResult = _target.GetUserByUserName(username, out message);
            Assert.IsNotNull(expectedResult);
            Assert.IsNotNull(expectedResult.PasswordSalt);
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