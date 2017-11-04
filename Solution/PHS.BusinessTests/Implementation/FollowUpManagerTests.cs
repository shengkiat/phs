using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
using PHS.BusinessTests;
using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class FollowUpManagerTests
    {
        private FollowUpManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void ImportCallerTest_FollowupGroupNotExist()
        {
            string message = string.Empty;

            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Files\no data file.xlsx");

            byte[] bytes = System.IO.File.ReadAllBytes(file);
            _target.ImportCaller(bytes, 1, out message);

            Assert.AreEqual("Follow-up group does not exist!", message);
        }

        [TestMethod()]
        public void ImportCallerTest_FollowupConfigurationNotDeployed()
        {
            string message = string.Empty;

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-1),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = true
            };

            FollowUpConfiguration followUpConfiguration = new FollowUpConfiguration()
            {
                Title = "Test Configuration",
                PHSEventID = 1,
                Deploy = false
            };

            FollowUpGroup followUpGroup = new FollowUpGroup()
            {
                Title = "Test Group"
            };

            followUpConfiguration.FollowUpGroups.Add(followUpGroup);

            phsEvent.FollowUpConfigurations.Add(followUpConfiguration);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Files\no data file.xlsx");

            byte[] bytes = System.IO.File.ReadAllBytes(file);
            _target.ImportCaller(bytes, 1, out message);

            Assert.AreEqual("Follow-up configuration is not deployed!", message);
        }

        [TestMethod()]
        public void ImportCallerTest_NoRecords()
        {
            string message = string.Empty;

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-1),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = true
            };

            FollowUpConfiguration followUpConfiguration = new FollowUpConfiguration()
            {
                Title = "Test Configuration",
                PHSEventID = 1,
                Deploy = true
            };

            FollowUpGroup followUpGroup = new FollowUpGroup()
            {
                Title = "Test Group"
            };

            followUpConfiguration.FollowUpGroups.Add(followUpGroup);

            phsEvent.FollowUpConfigurations.Add(followUpConfiguration);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Files\no data file.xlsx");

            byte[] bytes = System.IO.File.ReadAllBytes(file);
            _target.ImportCaller(bytes, 1, out message);

            Assert.AreEqual("No Volunteers/Comm Members found.", message);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockFollowUpManager(_unitOfWork);
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

        private class MockFollowUpManager : FollowUpManager
        {
            private IUnitOfWork _unitOfWork;

            public MockFollowUpManager(IUnitOfWork unitOfWork) : base(null)
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