using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
using PHS.Repository.Interface.Core;
using PHS.Repository.Context;
using Effort;
using PHS.DB;

namespace PHS.BusinessTests.Implementation
{
    [TestClass]
    public class StandardReferenceManagerTests
    {
        private StandardReferenceManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void AddStandardReferenceTest_Success()
        {
            string message = string.Empty;
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            var saveResult = _target.AddStandardReference(stdReference, out message);
            Assert.IsNotNull(saveResult);
            Assert.AreEqual(string.Empty, message);

            var expectedResult = _target.GetStandardReferenceByID(saveResult.StandardReferenceID, out message);
            Assert.IsNotNull(expectedResult);
            Assert.AreEqual("testStdReferenceTitle", expectedResult.Title);
        }

        [TestMethod()]
        public void AddStandardReferenceTest_NullStandardReferenceReturnError()
        {
            string message = string.Empty;
            StandardReference stdReference = null;

            var saveResult = _target.AddStandardReference(stdReference, out message);
            Assert.IsNull(saveResult);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void AddStandardReferenceTest_NullOrEmptyTitleReturnError()
        {
            string message = string.Empty;
            StandardReference stdReference = new StandardReference
            {
                Title = null
            };

            var saveResult = _target.AddStandardReference(stdReference, out message);
            Assert.IsNull(saveResult);
            Assert.AreEqual("Please fill in all required fields", message);

            stdReference.Title = string.Empty;
            saveResult = _target.AddStandardReference(stdReference, out message);
            Assert.IsNull(saveResult);
            Assert.AreEqual("Please fill in all required fields", message);

            stdReference.Title = "  ";
            saveResult = _target.AddStandardReference(stdReference, out message);
            Assert.IsNull(saveResult);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void AddStandardReferenceTest_ExistingTitleReturnError()
        {
            string message = string.Empty;
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            var saveResult = _target.AddStandardReference(stdReference, out message);
            Assert.IsNotNull(saveResult);
            Assert.AreEqual(string.Empty, message);

            StandardReference stdReference1 = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            saveResult = _target.AddStandardReference(stdReference1, out message);
            Assert.IsNull(saveResult);
            Assert.AreEqual("Standard Reference already exists", message);
        }

        [TestMethod()]
        public void UpdateStandardReferenceTest_Success()
        {
            string message = string.Empty;
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            var saveResult = _target.AddStandardReference(stdReference, out message);
            Assert.IsNotNull(saveResult);
            Assert.AreEqual(string.Empty, message);

            stdReference.Title = "updateStdReferenceTitle";
            var updateResult = _target.UpdateStandardReference(stdReference, out message);
            Assert.IsTrue(updateResult);
            Assert.AreEqual(string.Empty, message);

            var expectedResult = _target.GetStandardReferenceByID(saveResult.StandardReferenceID, out message);
            Assert.IsNotNull(expectedResult);
            Assert.AreEqual("updateStdReferenceTitle", expectedResult.Title);
        }

        [TestMethod()]
        public void UpdateStandardReferenceTest_NullStandardReferenceReturnError()
        {
            string message = string.Empty;
            StandardReference stdReference = null;

            var updateResult = _target.UpdateStandardReference(stdReference, out message);
            Assert.IsFalse(updateResult);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void UpdateStandardReferenceTest_NullOrEmptyTitleReturnError()
        {
            string message = string.Empty;
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            var saveResult = _target.AddStandardReference(stdReference, out message);
            Assert.IsNotNull(saveResult);
            Assert.AreEqual(string.Empty, message);

            stdReference.Title = null;
            var updateResult = _target.UpdateStandardReference(stdReference, out message);
            Assert.IsFalse(updateResult);
            Assert.AreEqual("Please fill in all required fields", message);

            stdReference.Title = string.Empty;
            updateResult = _target.UpdateStandardReference(stdReference, out message);
            Assert.IsFalse(updateResult);
            Assert.AreEqual("Please fill in all required fields", message);

            stdReference.Title = "  ";
            updateResult = _target.UpdateStandardReference(stdReference, out message);
            Assert.IsFalse(updateResult);
            Assert.AreEqual("Please fill in all required fields", message);
        }

        [TestMethod()]
        public void UpdateStandardReferenceTest_ExistingTitleReturnError()
        {
            string message = string.Empty;
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            var saveResult = _target.AddStandardReference(stdReference, out message);
            Assert.IsNotNull(saveResult);
            Assert.AreEqual(string.Empty, message);

            StandardReference stdReference1 = new StandardReference()
            {
                Title = "testStdReferenceTitle1"
            };

            saveResult = _target.AddStandardReference(stdReference1, out message);
            Assert.IsNotNull(saveResult);
            Assert.AreEqual(string.Empty, message);

            stdReference1.Title = "testStdReferenceTitle";
            var updateResult = _target.UpdateStandardReference(stdReference1, out message);
            Assert.IsFalse(updateResult);
            Assert.AreEqual("Standard Reference Title already exists", message);
        }

        [TestMethod()]
        public void DeleteStandardReferenceTest_Success()
        {
            string message = string.Empty;
            StandardReference stdReference = new StandardReference()
            {
                Title = "testStdReferenceTitle"
            };

            //ReferenceRange refrange1 = new ReferenceRange()
            //{
            //    Title = "normal",
            //    MinimumValue = 20,
            //    MaximumValue = 120,
            //    Result = "normal"
            //};
            //ReferenceRange refrange2 = new ReferenceRange()
            //{
            //    Title = "normal",
            //    MinimumValue = 20,
            //    MaximumValue = 120,
            //    Result = "normal"
            //};

            var saveResult = _target.AddStandardReference(stdReference, out message);
            Assert.IsNotNull(saveResult);
            Assert.AreEqual(string.Empty, message);

            var deleteResult = _target.DeleteStandardReference(stdReference.StandardReferenceID, out message);
            Assert.IsTrue(deleteResult);
            Assert.AreEqual(string.Empty, message);

            var expectedResult = _target.GetStandardReferenceByID(stdReference.StandardReferenceID, out message);
            Assert.IsNull(expectedResult);
            Assert.AreEqual("Invalid Standard Reference ID", message);
        }

        [TestMethod()]
        public void DeleteStandardReferenceTest_InvalidStandardReferenceIDReturnError()
        {
            string message = string.Empty;
            var deleteResult = _target.DeleteStandardReference(4, out message);
            Assert.IsFalse(deleteResult);
            Assert.AreEqual("Standard Reference not found", message);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockStandardReferenceManager(_unitOfWork);
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

        private class MockStandardReferenceManager : StandardReferenceManager
        {
            private IUnitOfWork _unitOfWork;

            public MockStandardReferenceManager(IUnitOfWork unitOfWork)
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
