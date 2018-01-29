using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
using PHS.Business.ViewModel.Event;
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
    public class ModalityManagerTests
    {
        private ModalityManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void GetModalityByIDTest_ShouldHaveRecord()
        {
            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-1),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = true
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality",
                IsMandatory = true,
                IsActive = false
            };

            phsEvent.Modalities.Add(modality);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string message = string.Empty;

            var result = _target.GetModalityByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Modality", result.Name);
            Assert.AreEqual(string.Empty, message);
        }

        [TestMethod()]
        public void GetModalityByIDTest_NoRecord()
        {
            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-1),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = true
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality",
                IsMandatory = true,
                IsActive = false
            };

            phsEvent.Modalities.Add(modality);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string message = string.Empty;

            var result = _target.GetModalityByID(3, out message);
            Assert.IsNull(result);
            Assert.AreEqual("Modality Not Found", message);
        }

        [TestMethod()]
        public void NewModalityTest_Success()
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

            string message = string.Empty;

            ModalityEventViewModel modalityEventViewModel = new ModalityEventViewModel()
            {
                Name = "Test Modality",
                IsMandatory = true,
                IsActive = false,
                EventID = 1,
                ModalityRole = new List<ModalityRole>()
            };

            var saveResult = _target.NewModality(modalityEventViewModel, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            var result = _target.GetModalityByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Modality", result.Name);
            Assert.AreEqual(string.Empty, message);
        }

        [TestMethod()]
        public void UpdateModalityTest_Success()
        {
            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-1),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = true
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality",
                IsMandatory = true,
                IsActive = false
            };

            phsEvent.Modalities.Add(modality);

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string message = string.Empty;

            var result = _target.GetModalityByID(1, out message);
            Assert.IsNotNull(result);

            ModalityEventViewModel modalityEventViewModel = new ModalityEventViewModel()
            {
                Name = "Test Modality 1234",
                IsMandatory = true,
                IsActive = false,
                EventID = 1,
                ModalityID = 1,
                ModalityRole = new List<ModalityRole>()
            };

            var saveResult = _target.UpdateModality(modalityEventViewModel, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            result = _target.GetModalityByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Modality 1234", result.Name);
            Assert.AreEqual(string.Empty, message);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockModalityManager(_unitOfWork);
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

        private class MockModalityManager : ModalityManager
        {
            private IUnitOfWork _unitOfWork;

            public MockModalityManager(IUnitOfWork unitOfWork) : base(null)
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