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
    public class AddressManagerTests
    {
        private AddressManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void FindAddressTest_AbleToFind()
        {
            MasterAddress masterAddressOne = new MasterAddress()
            {
                PostalCode = "112244",
                BuildingName = "NUS"
            };

            MasterAddress masterAddressTwo = new MasterAddress()
            {
                PostalCode = "123456",
                BuildingName = "CLEMENTI"
            };

            _unitOfWork.MasterAddress.Add(masterAddressOne);
            _unitOfWork.MasterAddress.Add(masterAddressTwo);

            _unitOfWork.Complete();

            var result = _target.FindAddress("112244");

            Assert.IsNotNull(result);
            Assert.AreEqual("NUS", result.BuildingName);
        }

        [TestMethod()]
        public void FindAddressTest_UnableToFind()
        {
            MasterAddress masterAddressOne = new MasterAddress()
            {
                PostalCode = "112244",
                BuildingName = "NUS"
            };

            MasterAddress masterAddressTwo = new MasterAddress()
            {
                PostalCode = "123456",
                BuildingName = "CLEMENTI"
            };

            _unitOfWork.MasterAddress.Add(masterAddressOne);
            _unitOfWork.MasterAddress.Add(masterAddressTwo);

            _unitOfWork.Complete();

            var result = _target.FindAddress("147852");

            Assert.IsNull(result);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockAddressManager(_unitOfWork);
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

        private class MockAddressManager : AddressManager
        {
            private IUnitOfWork _unitOfWork;

            public MockAddressManager(IUnitOfWork unitOfWork) : base(null)
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