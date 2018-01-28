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