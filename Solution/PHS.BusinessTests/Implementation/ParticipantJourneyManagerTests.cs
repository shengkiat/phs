using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.Implementation;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.BusinessTests;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.Repository.Context;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static PHS.Common.Constants;

namespace PHS.Business.Implementation.Tests
{
    [TestClass()]
    public class ParticipantJourneyManagerTests
    {
        private ParticipantJourneyManager _target;
        private FormManager _formManager;
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
            psm.Nric = "S82";
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

            Modality modality = new Modality()
            {
                Name = "Test Modality"
            };

            Form form = new Form
            {
                Title = "Test form"
            };

            modality.Forms.Add(form);

            phsEvent.Modalities.Add(modality);

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

            Assert.IsNull(postResult.Gender);

            var pjmResult = _unitOfWork.ParticipantJourneyModalities.Find(u => u.PHSEventID == postResult.Event.PHSEventID).FirstOrDefault();
            Assert.IsNotNull(pjmResult);
            Assert.AreEqual(1, pjmResult.ParticipantID);
            Assert.AreEqual(1, pjmResult.ModalityID);
            Assert.AreEqual(1, pjmResult.PHSEventID);
            Assert.AreEqual(1, pjmResult.FormID);
        }

        [TestMethod()]
        public void RegisterParticipant_NewParticipantAndPHSEventWithPreRegistration()
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

            Modality modality = new Modality()
            {
                Name = "Test Modality"
            };

            Form form = new Form
            {
                Title = "Test form"
            };

            PreRegistration preRegistrationOne = new PreRegistration()
            {
                Nric = "S8250369B",
                Address = "Test Add",
                Citizenship = "Singaporean",
                HomeNumber = "12345678",
                FullName = "Tester",
                Gender = "Male",
                Salutation = "Mr",
                Race = "Chinese",
                Language = "English",
                DateOfBirth = DateTime.Now,
                CreatedDateTime = DateTime.Now.AddMinutes(-60),
                EntryId = Guid.NewGuid()
            };

            PreRegistration preRegistrationTwo = new PreRegistration()
            {
                Nric = "S8250369B",
                Address = "Test Add",
                Citizenship = "Singaporean",
                HomeNumber = "12345678",
                FullName = "Tester",
                Gender = "Female",
                Salutation = "Mr",
                Race = "Chinese",
                Language = "English",
                DateOfBirth = DateTime.Now,
                CreatedDateTime = DateTime.Now,
                EntryId = Guid.NewGuid()
            };


            modality.Forms.Add(form);

            phsEvent.Modalities.Add(modality);

            _unitOfWork.PreRegistrations.Add(preRegistrationOne);
            _unitOfWork.PreRegistrations.Add(preRegistrationTwo);

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

            Assert.AreEqual(preRegistrationTwo.Gender, postResult.Gender);

            var pjmResult = _unitOfWork.ParticipantJourneyModalities.Find(u => u.PHSEventID == postResult.Event.PHSEventID).FirstOrDefault();
            Assert.IsNotNull(pjmResult);
            Assert.AreEqual(1, pjmResult.ParticipantID);
            Assert.AreEqual(1, pjmResult.ModalityID);
            Assert.AreEqual(1, pjmResult.PHSEventID);
            Assert.AreEqual(1, pjmResult.FormID);

        }

        [TestMethod()]
        public void RegisterParticipant_ExistingParticipantAndPHSEvent()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 2;

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

            Modality modality = new Modality()
            {
                Name = "Test Modality"
            };

            Form form = new Form
            {
                Title = "Test form"
            };

            _unitOfWork.Events.Add(phsEventOne);
            _unitOfWork.Events.Add(phsEventTwo);

            participant.PHSEvents.Add(phsEventOne);

            _unitOfWork.Participants.Add(participant);

            modality.Forms.Add(form);

            phsEventTwo.Modalities.Add(modality);

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

            Assert.AreEqual("88776655", postResult.HomeNumber);

            var pjmResult = _unitOfWork.ParticipantJourneyModalities.Find(u => u.PHSEventID == postResult.Event.PHSEventID).FirstOrDefault();
            Assert.IsNotNull(pjmResult);
            Assert.AreEqual(1, pjmResult.ParticipantID);
            Assert.AreEqual(1, pjmResult.ModalityID);
            Assert.AreEqual(2, pjmResult.PHSEventID);
            Assert.AreEqual(1, pjmResult.FormID);
        }

        [TestMethod()]
        public void RegisterParticipant_ExistingParticipantAndPHSEventWithPreRegistration()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 2;

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

            PreRegistration preRegistration = new PreRegistration()
            {
                Nric = "S8250369B",
                Address = "Test Add",
                Citizenship = "Singaporean",
                HomeNumber = "12345678",
                FullName = "Tester",
                Gender = "Male",
                Salutation = "Mr",
                Race = "Chinese",
                Language = "English",
                DateOfBirth = DateTime.Now,
                EntryId = Guid.NewGuid()
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality"
            };

            Form form = new Form
            {
                Title = "Test form"
            };


            _unitOfWork.PreRegistrations.Add(preRegistration);

            _unitOfWork.Events.Add(phsEventOne);
            _unitOfWork.Events.Add(phsEventTwo);

            participant.PHSEvents.Add(phsEventOne);

            _unitOfWork.Participants.Add(participant);

            modality.Forms.Add(form);

            phsEventTwo.Modalities.Add(modality);

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

            Assert.AreEqual("12345678", postResult.HomeNumber);

            var pjmResult = _unitOfWork.ParticipantJourneyModalities.Find(u => u.PHSEventID == postResult.Event.PHSEventID).FirstOrDefault();
            Assert.IsNotNull(pjmResult);
            Assert.AreEqual(1, pjmResult.ParticipantID);
            Assert.AreEqual(1, pjmResult.ModalityID);
            Assert.AreEqual(2, pjmResult.PHSEventID);
            Assert.AreEqual(1, pjmResult.FormID);
        }

        [TestMethod()]
        public void RetrieveParticipantJourneyForm_InvalidNric()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S82";
            psm.PHSEventId = 1;

            string message = string.Empty;

            _target.RetrieveParticipantJourneyForm(psm, out message);

            Assert.AreEqual("Invalid Nric", message);
        }

        [TestMethod()]
        public void RetrieveParticipantJourneyForm_ShouldHaveRecord()
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

            ParticipantJourneyFormViewModel result = _target.RetrieveParticipantJourneyForm(psm, out message);

            Assert.AreEqual(string.Empty, message);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void RetrieveParticipantJourneyModality_InvalidNric()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S82";
            psm.PHSEventId = 1;

            int formId = 1;

            string message = string.Empty;

            _target.RetrieveParticipantJourneyModality(psm, formId, 1, out message);

            Assert.AreEqual("Invalid Nric", message);
        }

        [TestMethod()]
        public void RetrieveParticipantJourneyModality_NoRecordFound()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;

            int formId = 1;

            string message = string.Empty;

            ParticipantJourneyModality result = _target.RetrieveParticipantJourneyModality(psm, formId, 1, out message);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void RetrieveParticipantJourneyModality_ShouldHaveRecord()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;

            int formId = 1;
            Guid entryId = new Guid();

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test 15",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-200),
                EndDT = DateTime.Now.AddDays(-199),
                IsActive = false
            };

            Participant participant = new Participant()
            {
                Nric = "S8250369B",
                DateOfBirth = DateTime.Now,
                HomeNumber = "88776655"
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality"
            };

            Form form = new Form
            {
                Title = "Test form"
            };

            _unitOfWork.Events.Add(phsEvent);

            participant.PHSEvents.Add(phsEvent);

            _unitOfWork.Participants.Add(participant);

            modality.Forms.Add(form);

            phsEvent.Modalities.Add(modality);

            ParticipantJourneyModality journeyModality = new ParticipantJourneyModality()
            {
                ParticipantID = 1,
                PHSEventID = psm.PHSEventId,
                ModalityID = 1,
                FormID = formId,
                EntryId = entryId
            };

            _unitOfWork.ParticipantJourneyModalities.Add(journeyModality);

            _unitOfWork.Complete();

            string message = string.Empty;

            ParticipantJourneyModality result = _target.RetrieveParticipantJourneyModality(psm, formId, 1, out message);

            Assert.IsNotNull(result);
            Assert.AreEqual(entryId, result.EntryId);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "No participantJourneyModality found")]
        public void InternalFillIn_ExceptionRequiredParticipantJourneyModality()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;
            TemplateViewModel templateViewModel = CreateFormAndTemplateWithSampleField();

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.InternalFillIn(psm, 1, submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            Assert.Fail("Expecting exception");

        }

        [TestMethod()]
        public void InternalFillIn_InsertValues()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;
            TemplateViewModel templateViewModel = CreateFormAndTemplateWithSampleField();

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test 15",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-200),
                EndDT = DateTime.Now.AddDays(-199),
                IsActive = false
            };

            Participant participant = new Participant()
            {
                Nric = "S8250369B",
                DateOfBirth = DateTime.Now,
                HomeNumber = "88776655"
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality"
            };

            Form form = new Form
            {
                Title = "Test form"
            };

            _unitOfWork.Events.Add(phsEvent);

            participant.PHSEvents.Add(phsEvent);

            _unitOfWork.Participants.Add(participant);

            modality.Forms.Add(form);

            phsEvent.Modalities.Add(modality);

            ParticipantJourneyModality journeyModality = new ParticipantJourneyModality()
            {
                ParticipantID = 1,
                PHSEventID = psm.PHSEventId,
                ModalityID = 1,
                FormID = 1
            };

            _unitOfWork.ParticipantJourneyModalities.Add(journeyModality);

            _unitOfWork.Complete();

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            string result = _target.InternalFillIn(psm, 1, submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(1, templateViewModel.Entries.Count);
            Assert.AreEqual("HelloTest", templateViewModel.Entries.FirstOrDefault().Value);

            ParticipantJourneyModality journeyModalityResult = _unitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModality("S8250369B", 1, 1, 1);
            Assert.IsNotNull(journeyModalityResult);
            Assert.AreEqual(templateViewModel.Entries.FirstOrDefault().EntryId, journeyModalityResult.EntryId.ToString());
        }

        [TestMethod()]
        public void InternalFillIn_UpdateValues()
        {
            ParticipantJourneySearchViewModel psm = new ParticipantJourneySearchViewModel();
            psm.Nric = "S8250369B";
            psm.PHSEventId = 1;
            TemplateViewModel templateViewModel = CreateFormAndTemplateWithSampleField();

            PHSEvent phsEvent = new PHSEvent()
            {
                Title = "Test 15",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-200),
                EndDT = DateTime.Now.AddDays(-199),
                IsActive = false
            };

            Participant participant = new Participant()
            {
                Nric = "S8250369B",
                DateOfBirth = DateTime.Now,
                HomeNumber = "88776655"
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality"
            };

            Form form = new Form
            {
                Title = "Test form"
            };

            _unitOfWork.Events.Add(phsEvent);

            participant.PHSEvents.Add(phsEvent);

            _unitOfWork.Participants.Add(participant);

            modality.Forms.Add(form);

            phsEvent.Modalities.Add(modality);

            ParticipantJourneyModality journeyModality = new ParticipantJourneyModality()
            {
                ParticipantID = 1,
                PHSEventID = psm.PHSEventId,
                ModalityID = 1,
                FormID = 1
            };

            _unitOfWork.ParticipantJourneyModalities.Add(journeyModality);

            _unitOfWork.Complete();

            FormCollection submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "HelloTest");

            IDictionary<string, string> submissionFields = new System.Collections.Generic.Dictionary<string, string>();
            submissionFields.Add("1", "1");

            _target.InternalFillIn(psm, 1, submissionFields, templateViewModel, submissionCollection);

            submissionCollection = new FormCollection();
            submissionCollection.Add("SubmitFields[1].TextBox", "Hello Test 2");

            string result = _target.InternalFillIn(psm, 1, submissionFields, templateViewModel, submissionCollection);
            Assert.AreEqual(result, "success");

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(1, templateViewModel.Entries.Count);
            Assert.AreEqual("Hello Test 2", templateViewModel.Entries.FirstOrDefault().Value);

            ParticipantJourneyModality journeyModalityResult = _unitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModality("S8250369B", 1, 1, 1);
            Assert.IsNotNull(journeyModalityResult);
            Assert.AreEqual(templateViewModel.Entries.FirstOrDefault().EntryId, journeyModalityResult.EntryId.ToString());
        }

        private TemplateViewModel CreateFormAndTemplateWithSampleField()
        {
            Template template = _formManager.CreateNewFormAndTemplate(new FormViewModel());
            Assert.IsNotNull(template);

            TemplateViewModel templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);

            FormCollection fieldCollection;
            IDictionary<string, string> fields;
            CeateFieldForm(1, out fieldCollection, out fields);

            _formManager.UpdateTemplate(templateViewModel, fieldCollection, fields);

            templateViewModel = _formManager.FindTemplateToEdit(template.TemplateID);
            Assert.IsNotNull(templateViewModel.Fields);
            Assert.AreEqual(1, templateViewModel.Fields.Count);

            templateViewModel.Entries = _formManager.HasSubmissions(templateViewModel).ToList();
            Assert.AreEqual(0, templateViewModel.Entries.Count);
            return templateViewModel;
        }

        [TestInitialize]
        public void SetupTest()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new PHSContext(connection);
            _unitOfWork = new MockUnitOfWork(_context);

            _formManager = new MockFormManager(_unitOfWork);
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
            _formManager = null;
            _target = null;
        }

        private FormCollection CeateFieldForm(int id, out FormCollection fieldCollection, out IDictionary<string, string> fields)
        {
            fieldCollection = new FormCollection();

            //collection.Add("SubmitFields[1].TextBox", "SubmitFields[1].TextBox");
            fieldCollection.Add("Fields[1].FieldType", "TEXTBOX");
            fieldCollection.Add("Fields[1].MaxCharacters", "200");
            fieldCollection.Add("Fields[1].IsRequired", "false");
            fieldCollection.Add("Fields[1].AddOthersOption", "false");
            fieldCollection.Add("Fields[1].MinimumAge", "18");
            fieldCollection.Add("Fields[1].MaximumAge", "100");
            fieldCollection.Add("Fields[1].Text", "");
            fieldCollection.Add("Fields[1].Label", "Click to edit");
            fieldCollection.Add("Fields[1].HoverText", "");
            fieldCollection.Add("Fields[1].SubLabel", "");
            fieldCollection.Add("Fields[1].HelpText", "");
            fieldCollection.Add("Fields[1].Hint", "");

            fields = new System.Collections.Generic.Dictionary<string, string>();
            fields.Add("1", "1");

            return fieldCollection;
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

        private class MockFormManager : FormManager
        {
            private IUnitOfWork _unitOfWork;

            public MockFormManager(IUnitOfWork unitOfWork)
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