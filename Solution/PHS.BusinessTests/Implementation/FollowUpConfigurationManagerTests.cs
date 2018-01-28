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

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class FollowUpConfigurationManagerTests
    {
        private FollowUpConfigurationManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void GetAllFUConfigurationTest_ShouldHaveRecords()
        {
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

            phsEvent.FollowUpConfigurations.Add(followUpConfiguration);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            var result = _target.GetAllFUConfiguration();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod()]
        public void GetAllFUConfigurationByEventIDTest_ShouldHaveRecords()
        {
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

            phsEvent.FollowUpConfigurations.Add(followUpConfiguration);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            var result = _target.GetAllFUConfigurationByEventID(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod()]
        public void GetFUConfigurationByIDTest_ShouldHaveRecord()
        {
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

            phsEvent.FollowUpConfigurations.Add(followUpConfiguration);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string message = string.Empty;

            var result = _target.GetFUConfigurationByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Configuration", result.Title);
            Assert.AreEqual(string.Empty, message);
        }

        [TestMethod()]
        public void GetFUConfigurationByIDTest_NoRecord()
        {
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

            phsEvent.FollowUpConfigurations.Add(followUpConfiguration);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string message = string.Empty;

            var result = _target.GetFUConfigurationByID(3, out message);
            Assert.IsNull(result);
            Assert.AreEqual("Follow-up Configuration Not Found", message);
        }

        [TestMethod()]
        public void NewFollowUpConfigurationTest_Success()
        {
            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-1),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = true
            };

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();


            FollowUpConfiguration followUpConfiguration = new FollowUpConfiguration()
            {
                Title = "Test Configuration",
                PHSEventID = 1,
                Deploy = false
            };

            string message = string.Empty;

            var saveResult = _target.NewFollowUpConfiguration(followUpConfiguration, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            var result = _target.GetFUConfigurationByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Configuration", result.Title);
        }

        [TestMethod()]
        public void AddFollowUpGroupTest_Success()
        {
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

            phsEvent.FollowUpConfigurations.Add(followUpConfiguration);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            FollowUpGroup followUpGroup = new FollowUpGroup()
            {
                Title = "Test Group",
                FollowUpConfigurationID = 1
            };

            string message = string.Empty;

            var result = _target.AddFollowUpGroup(followUpGroup, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Group", result.Title);
        }

        [TestMethod()]
        public void UpdateFollowUpConfigurationTest_Success()
        {
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

            phsEvent.FollowUpConfigurations.Add(followUpConfiguration);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string message = string.Empty;

            followUpConfiguration = _target.GetFUConfigurationByID(1, out message);
            Assert.IsNotNull(followUpConfiguration);
            followUpConfiguration.Title = "Another Test Configuration";
            var saveResult = _target.UpdateFollowUpConfiguration(followUpConfiguration);
            Assert.IsTrue(saveResult);
 
            var result = _target.GetFUConfigurationByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Another Test Configuration", result.Title);
        }

        [TestMethod()]
        public void DeleteFollowUpConfigurationTest_Success()
        {
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

            phsEvent.FollowUpConfigurations.Add(followUpConfiguration);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string message = string.Empty;

            followUpConfiguration = _target.GetFUConfigurationByID(1, out message);
            Assert.IsNotNull(followUpConfiguration);
            var saveResult = _target.DeleteFollowUpConfiguration(1, out message);
            Assert.IsTrue(saveResult);

            var result = _target.GetFUConfigurationByID(1, out message);
            Assert.IsNull(result);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockFollowUpConfigurationManager(_unitOfWork);
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

        private class MockFollowUpConfigurationManager : FollowUpConfigurationManager
        {
            private IUnitOfWork _unitOfWork;

            public MockFollowUpConfigurationManager(IUnitOfWork unitOfWork) : base(null)
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