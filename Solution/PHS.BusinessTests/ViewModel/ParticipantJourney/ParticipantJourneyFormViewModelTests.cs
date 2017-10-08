using Microsoft.VisualStudio.TestTools.UnitTesting;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.ParticipantJourney.Tests
{
    [TestClass()]
    public class ParticipantJourneyFormViewModelTests
    {
        [TestMethod()]
        public void GetModalityFormsForTabsTest()
        {
            PHSEvent phsEvent = new PHSEvent()
            {
                PHSEventID = 1,
                Title = "Test",
                Venue = "Test",
                StartDT = DateTime.Now.AddDays(-200),
                EndDT = DateTime.Now.AddDays(-198),
                IsActive = false
            };

            Modality modality = new Modality()
            {
                Name = "Test Modality",
                IsMandatory = true
            };

            Form formOne = new Form
            {
                Title = "1. Test form"
            };

            modality.Forms.Add(formOne);

            Form formTwo = new Form
            {
                Title = "3. Test form"
            };

            modality.Forms.Add(formTwo);

            Form formThree = new Form
            {
                Title = "2. Test form"
            };

            modality.Forms.Add(formThree);

            phsEvent.Modalities.Add(modality);

            Participant participant = new Participant()
            {
                Nric = "S8250369B",
                DateOfBirth = DateTime.Now
            };

            participant.PHSEvents.Add(phsEvent);


            ParticipantJourneyFormViewModel _target = new ParticipantJourneyFormViewModel(participant, 1);
            var result = _target.GetModalityFormsForTabs();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("1. Test form", result[0].Title);
            Assert.AreEqual("2. Test form", result[1].Title);
            Assert.AreEqual("3. Test form", result[2].Title);
        }
    }
}