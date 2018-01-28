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
    public class EventManagerTests
    {
        private EventManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void GetAllEventsTest_ShouldHaveRecords()
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

            var result = _target.GetAllEvents();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod()]
        public void GetEventByIDTest_ShouldHaveRecord()
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

            var result = _target.GetEventByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Title);
            Assert.AreEqual(string.Empty, message);
        }

        [TestMethod()]
        public void GetEventByIDTest_NoRecord()
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

            var result = _target.GetEventByID(3, out message);
            Assert.IsNull(result);
            Assert.AreEqual("Event Not Found", message);
        }

        [TestMethod()]
        public void NewEventTest_Success()
        {
            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(1),
                EndDT = DateTime.Now.AddDays(10),
                IsActive = false
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality",
                IsMandatory = true,
                IsActive = false
            };

            phsEvent.Modalities.Add(modality);

            string message = string.Empty;

            var saveResult = _target.NewEvent(phsEvent, out message);
            Assert.IsTrue(saveResult);
            Assert.AreEqual(string.Empty, message);

            var result = _target.GetEventByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Title);
            Assert.IsTrue(result.IsActive);

            Assert.IsNotNull(result.Modalities);
            Assert.AreEqual(1, result.Modalities.Count);
            Assert.IsTrue(result.Modalities.First().IsActive);
            Assert.AreEqual("Test Modality", result.Modalities.First().Name);
        }

        [TestMethod()]
        public void NewEventTest_FailedValidationDueToStartTimeLessThanToday()
        {
            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-1),
                EndDT = DateTime.Now.AddDays(10),
                IsActive = false
            };

            string message = string.Empty;

            var saveResult = _target.NewEvent(phsEvent, out message);
            Assert.IsFalse(saveResult);
            Assert.AreEqual("Input Date must larger than today", message);

        }

        [TestMethod()]
        public void UpdateEventTest_Success()
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

            var record = _target.GetEventByID(1, out message);
            Assert.IsNotNull(record);

            record.Title = "Test 1234";

            var saveResult = _target.UpdateEvent(record);
            Assert.IsTrue(saveResult);

            var result = _target.GetEventByID(1, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test 1234", result.Title);
            Assert.AreEqual(string.Empty, message);
        }

        [TestMethod()]
        public void DeleteEventTest_Success()
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

            var record = _target.GetEventByID(1, out message);
            Assert.IsNotNull(record);

            var saveResult = _target.DeleteEvent(record.PHSEventID, out message);
            Assert.IsTrue(saveResult);

            var result = _target.GetEventByID(record.PHSEventID, out message);
            Assert.IsNull(result);
            Assert.AreEqual("Event Not Found", message);
        }

        [TestMethod()]
        public void DeleteEventTest_UnableToDeleteWithParticipantsRegistered()
        {
            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-1),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = true
            };

            Participant participant = new Participant()
            {
                Nric = "S8250369B",
                DateOfBirth = DateTime.Now
            };

            _unitOfWork.Events.Add(phsEvent);

            participant.PHSEvents.Add(phsEvent);
            _unitOfWork.Participants.Add(participant);

            _unitOfWork.Complete();

            string message = string.Empty;

            var record = _target.GetEventByID(1, out message);
            Assert.IsNotNull(record);

            var saveResult = _target.DeleteEvent(record.PHSEventID, out message);
            Assert.IsFalse(saveResult);
            Assert.AreEqual("Can't delete event with partients!", message);

            var result = _target.GetEventByID(record.PHSEventID, out message);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeleteEventModalityTest_Success()
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

            var record = _target.GetEventByID(1, out message);
            Assert.IsNotNull(record);
            Assert.AreEqual(1, record.Modalities.Count);

            var saveResult = _target.DeleteEventModality(1, record.PHSEventID, out message);
            Assert.IsTrue(saveResult);

            var result = _target.GetEventByID(record.PHSEventID, out message);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Modalities.Count);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockEventManager(_unitOfWork);
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

        private class MockEventManager : EventManager
        {
            private IUnitOfWork _unitOfWork;

            public MockEventManager(IUnitOfWork unitOfWork) : base(null)
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