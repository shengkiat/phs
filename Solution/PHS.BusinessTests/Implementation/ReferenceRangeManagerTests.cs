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
    public class ReferenceRangeManagerTests
    {
        private ReferenceRangeManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void GetReferenceRangeByIDTest_ShouldHaveRecord()
        {
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            ReferenceRange referenceRange = new ReferenceRange()
            {
                Title = "Test",
                Result = "Testing 123",
            };

            stdReference.ReferenceRanges.Add(referenceRange);

            _unitOfWork.StandardReferences.Add(stdReference);

            _unitOfWork.Complete();

            string message = string.Empty;

            var result = _target.GetReferenceRangeByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Title);
            Assert.AreEqual(string.Empty, message);
        }

        [TestMethod()]
        public void GetReferenceRangeByIDTest_NoRecord()
        {
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            ReferenceRange referenceRange = new ReferenceRange()
            {
                Title = "Test",
                Result = "Testing 123",
            };

            stdReference.ReferenceRanges.Add(referenceRange);

            _unitOfWork.StandardReferences.Add(stdReference);

            _unitOfWork.Complete();

            string message = string.Empty;

            var result = _target.GetReferenceRangeByID(3, out message);
            Assert.IsNull(result);
            Assert.AreEqual("Invalid Reference Range ID", message);
        }

        [TestMethod()]
        public void AddReferenceRangeTest_Success()
        {
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            _unitOfWork.StandardReferences.Add(stdReference);

            _unitOfWork.Complete();

            string message = string.Empty;

            var result = _target.GetReferenceRangeByID(1, out message);
            Assert.IsNull(result);

            ReferenceRange referenceRange = new ReferenceRange()
            {
                Title = "Test",
                Result = "Testing 123",
                StandardReferenceID = 1
            };

            var saveResult = _target.AddReferenceRange(referenceRange, out message);
            Assert.IsNotNull(saveResult);
            Assert.AreEqual(string.Empty, message);

            result = _target.GetReferenceRangeByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Title);
        }

        [TestMethod()]
        public void UpdateReferenceRangeTest_Success()
        {
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            ReferenceRange referenceRange = new ReferenceRange()
            {
                Title = "Test",
                Result = "Testing 123",
            };

            stdReference.ReferenceRanges.Add(referenceRange);

            _unitOfWork.StandardReferences.Add(stdReference);

            _unitOfWork.Complete();

            string message = string.Empty;

            referenceRange = _target.GetReferenceRangeByID(1, out message);
            Assert.IsNotNull(stdReference);

            referenceRange.Title = "Test 1234";

            var saveResult = _target.UpdateReferenceRange(referenceRange, out message);
            Assert.IsTrue(saveResult);

            var result = _target.GetReferenceRangeByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test 1234", result.Title);
            Assert.AreEqual(string.Empty, message);
        }

        [TestMethod()]
        public void DeleteReferenceRangeTest_Success()
        {
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            ReferenceRange referenceRange = new ReferenceRange()
            {
                Title = "Test",
                Result = "Testing 123",
            };

            stdReference.ReferenceRanges.Add(referenceRange);

            _unitOfWork.StandardReferences.Add(stdReference);

            _unitOfWork.Complete();

            string message = string.Empty;

            referenceRange = _target.GetReferenceRangeByID(1, out message);
            Assert.IsNotNull(stdReference);

            var saveResult = _target.DeleteReferenceRange(1, out message);
            Assert.IsTrue(saveResult);

            var result = _target.GetReferenceRangeByID(1, out message);
            Assert.IsNull(result);
            Assert.AreEqual("Invalid Reference Range ID", message);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockReferenceRangeManager(_unitOfWork);
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

        private class MockReferenceRangeManager : ReferenceRangeManager
        {
            private IUnitOfWork _unitOfWork;

            public MockReferenceRangeManager(IUnitOfWork unitOfWork) : base(null)
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