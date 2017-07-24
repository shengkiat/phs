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

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class PastParticipantJourneyManagerTests
    {
        private PastParticipantJourneyManager _target;
        private IUnitOfWork _unitOfWork;
        private PHSContext _context;

        [TestMethod()]
        public void GetAllParticipantJourneyByNric_InvalidNric()
        {
            string message = string.Empty;
            _target.GetAllParticipantJourneyByNric("S82", out message);

            Assert.AreEqual("Invalid Nric", message);
        }

        [TestMethod()]
        public void GetAllParticipantJourneyByNric_OnlyPastScreeningEvents()
        {
            PHSEvent phsEventOne = new PHSEvent()
            {
                Title = "Test 15",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-200),
                EndDT = DateTime.Now.AddDays(-199),
                IsActive = false
            };

            PHSEvent phsEventTwo = new PHSEvent()
            {
                Title = "Test 16",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-2),
                EndDT = DateTime.Now.AddDays(1),
                IsActive = true
            };

            Participant participant = new Participant()
            {
                Nric = "S8250369B",
                DateOfBirth = DateTime.Now,
                HomeNumber = "88776655"
            };

            _unitOfWork.Events.Add(phsEventOne);
            _unitOfWork.Events.Add(phsEventTwo);

            _unitOfWork.Participants.Add(participant);

            participant.PHSEvents.Add(phsEventOne);
            participant.PHSEvents.Add(phsEventTwo);

            _unitOfWork.Complete();

            string message = string.Empty;
            IList<ParticipantJourneyViewModel> result = _target.GetAllParticipantJourneyByNric("S8250369B", out message);

            Assert.AreEqual(string.Empty, message);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Test 15", result.FirstOrDefault().Event.Title);
        }

        [TestMethod()]
        public void RetrievePastParticipantJourney_InvalidNric()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S82";
            psm.PHSEventId = 1;

            string message = string.Empty;

            _target.RetrievePastParticipantJourney(psm, out message);

            Assert.AreEqual("Invalid Nric", message);
        }

        [TestMethod()]
        public void RetrievePastParticipantJourney_ShouldHaveRecord()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;

            PHSEvent phsEventOne = new PHSEvent()
            {
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-200),
                EndDT = DateTime.Now.AddDays(-198),
                IsActive = false
            };

            Participant participant = new Participant()
            {
                Nric = "S8250369B",
                DateOfBirth = DateTime.Now
            };

            _unitOfWork.Events.Add(phsEventOne);

            participant.PHSEvents.Add(phsEventOne);
            _unitOfWork.Participants.Add(participant);

            _unitOfWork.Complete();

            string message = string.Empty;

            ParticipantJourneyViewModel result = _target.RetrievePastParticipantJourney(psm, out message);

            Assert.AreEqual(string.Empty, message);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Event.Title);
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _target = new MockPastParticipantJourneyManager(_unitOfWork);
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

        private class MockPastParticipantJourneyManager : PastParticipantJourneyManager
        {
            private IUnitOfWork _unitOfWork;

            public MockPastParticipantJourneyManager(IUnitOfWork _unitOfWork)
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