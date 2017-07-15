using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.BusinessTests;
using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PHS.Common.Constants;

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class ParticipantJourneyManagerTests
    {
        private ParticipantJourneyManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void RetrieveActiveScreeningEvent_AbleToFindActiveEvent()
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

            ParticipantJourneySearchViewModel result = _target.RetrieveActiveScreeningEvent();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.PHSEvents.Count());
        }

        [TestMethod()]
        public void RetrieveActiveScreeningEvent_NoActiveEventWhenIsInactive()
        {
            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-1),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = false
            };

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            ParticipantJourneySearchViewModel result = _target.RetrieveActiveScreeningEvent();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.PHSEvents.Count());
        }

        [TestMethod()]
        public void RetrieveActiveScreeningEvent_NoActiveEventWhenNonBetweenStartEndDate()
        {
            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-2),
                EndDT = DateTime.Now.AddDays(-1),
                IsActive = true
            };

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            ParticipantJourneySearchViewModel result = _target.RetrieveActiveScreeningEvent();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.PHSEvents.Count());
        }

        [TestMethod()]
        public void RetrieveParticipantJourney_InvalidNric()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric ="S82";
            psm.PHSEventId = 1;

            string message = string.Empty;
            MessageType messageType = MessageType.ERROR;

            _target.RetrieveParticipantJourney(psm, out message, out messageType);

            Assert.AreEqual("Invalid Nric", message);
        }

        [TestMethod()]
        public void RetrieveParticipantJourney_NotActiveEvent()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-2),
                EndDT = DateTime.Now.AddDays(-1),
                IsActive = false
            };

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string message = string.Empty;
            MessageType messageType = MessageType.ERROR;

            ParticipantJourneyViewModel result = _target.RetrieveParticipantJourney(psm, out message, out messageType);

            Assert.IsNull(result);
            Assert.AreEqual("Screening Event is not active", message);
        }

        [TestMethod()]
        public void RetrieveParticipantJourney_NoParticipantMessage()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-2),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = true
            };

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string message = string.Empty;
            MessageType messageType = MessageType.ERROR;

            ParticipantJourneyViewModel result = _target.RetrieveParticipantJourney(psm, out message, out messageType);

            Assert.IsNull(result);
            Assert.AreEqual("No registration record found. Do you want to register this Nric?", message);
        }

        [TestMethod()]
        public void RetrieveParticipantJourney_FindParticipantButNotActiveEvent()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 2;

            PHSEvent phsEventOne = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-200),
                EndDT = DateTime.Now.AddDays(-198),
                IsActive = false
            };

            PHSEvent phsEventTwo = new PHSEvent()
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

            _unitOfWork.Events.Add(phsEventOne);
            _unitOfWork.Events.Add(phsEventTwo);

            participant.PHSEvents.Add(phsEventOne);
            _unitOfWork.Participants.Add(participant);

            _unitOfWork.Complete();

            string message = string.Empty;
            MessageType messageType = MessageType.ERROR;

            ParticipantJourneyViewModel result = _target.RetrieveParticipantJourney(psm, out message, out messageType);

            Assert.IsNull(result);
            Assert.AreEqual("No registration record found. Do you want to register this Nric?", message);
        }

        [TestMethod()]
        public void RetrieveParticipantJourney_FoundParticipant()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-2),
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
            MessageType messageType = MessageType.ERROR;

            ParticipantJourneyViewModel result = _target.RetrieveParticipantJourney(psm, out message, out messageType);

            Assert.AreEqual(string.Empty, message);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Event);
            Assert.AreEqual(phsEvent.PHSEventID, result.Event.PHSEventID);
        }

        [TestMethod()]
        public void RegisterParticipant_InvalidNric()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S82";
            psm.PHSEventId = 1;

            string result = _target.RegisterParticipant(psm);

            Assert.AreEqual("Invalid Nric", result);
        }

        [TestMethod()]
        public void RegisterParticipant_NotActiveEvent()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-2),
                EndDT = DateTime.Now.AddDays(-1),
                IsActive = false
            };

            _unitOfWork.Events.Add(phsEvent);

            _unitOfWork.Complete();

            string result = _target.RegisterParticipant(psm);

            Assert.AreEqual("Screening Event is not active", result);
        }

        [TestMethod()]
        public void RegisterParticipant_AlreadyHasPHSEvent()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;

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

            string result = _target.RegisterParticipant(psm);

            Assert.AreEqual("Invalid register participant", result);
        }

        [TestMethod()]
        public void RegisterParticipant_NewParticipantAndPHSEvent()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;

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
            MessageType messageType = MessageType.ERROR;

            ParticipantJourneyViewModel preResult = _target.RetrieveParticipantJourney(psm, out message, out messageType);
            Assert.IsNull(preResult);

            string registerResult = _target.RegisterParticipant(psm);

            Assert.AreEqual("success", registerResult);

            ParticipantJourneyViewModel postResult = _target.RetrieveParticipantJourney(psm, out message, out messageType);
            Assert.IsNotNull(postResult);
            Assert.IsNotNull(postResult.Event);

        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockParticipantJourneyManager(_unitOfWork);
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

        private class MockParticipantJourneyManager : ParticipantJourneyManager
        {
            private IUnitOfWork _unitOfWork;

            public MockParticipantJourneyManager(IUnitOfWork _unitOfWork)
            {
                this._unitOfWork = _unitOfWork;
            }

            protected override IUnitOfWork CreateUnitOfWork()
            {
                return _unitOfWork;
            }
        }

    }
}