using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using PHS.Business.Common;
using PHS.Common;
using PHS.Business.ViewModel.PatientJourney;

namespace PHS.Business.Implementation
{
    public class PatientJourneyManager : BaseManager, IPatientJourneyManager, IManagerFactoryBase<IPatientJourneyManager>
    {
        public IPatientJourneyManager Create()
        {
            return new PatientJourneyManager();
        }

        public IList<PatientEventViewModel> GetPatientEventsByNric(string icFirstDigit, string icNumber, string icLastDigit, out string message)
        {
            IList<PatientEventViewModel> result = null;
            message = string.Empty;

            if (NricChecker.IsNRICValid(icFirstDigit, icNumber, icLastDigit))
            {
                string nric = icFirstDigit + icNumber + icLastDigit;
               
                result = getMockData(nric);
            }


            else
            {
                message = "Invalid Nric!";
            }

            return result;
        }

        public PatientEventViewModel GetPatientEvent(string nric, string eventId, out string message)
        {
            PatientEventViewModel result = null;
            message = string.Empty;

            if (NricChecker.IsNRICValid(nric) && !string.IsNullOrEmpty(eventId))
            {
                result = getMockData(nric, eventId);
                if (result == null)
                {
                    message = "Unable to find using Nric and Event";
                }
            }


            else
            {
                message = "Invalid Nric/Event!";
            }

            return result;
        }


        private List<PatientEventViewModel> getMockData(string nric)
        {
            Dictionary<string, List<PatientEventViewModel>> mockData = new Dictionary<string, List<PatientEventViewModel>>();

            List<PatientEventViewModel> firstRecords = new List<PatientEventViewModel>();
            PatientEventViewModel patientOne = new PatientEventViewModel();

            Modality modalityOne = new Modality();
            modalityOne.ID = 1;
            modalityOne.Name = "Registration";
            modalityOne.ModalityForms = new List<ModalityForm>();

            Modality modalityTwo = new Modality();
            modalityTwo.ID = 2;
            modalityTwo.Name = "History Taking";
            modalityTwo.ModalityForms = new List<ModalityForm>();

            @event eventOne = new @event();
            eventOne.ID = 100;
            eventOne.Title = "2016 - Event";
            eventOne.Modalities = new List<Modality>();
            eventOne.Modalities.Add(modalityOne);
            eventOne.Modalities.Add(modalityTwo);

            @event eventTwo = new @event();
            eventTwo.ID = 200;
            eventTwo.Title = "2015 - Event";
            eventTwo.Modalities = new List<Modality>();
            eventTwo.Modalities.Add(modalityOne);
            eventTwo.Modalities.Add(modalityTwo);

            patientOne.FullName = "ABCDE";
            patientOne.Nric = "S8518538A";
            patientOne.Event = eventOne;
            firstRecords.Add(patientOne);

            PatientEventViewModel patientTwo = new PatientEventViewModel();
            patientTwo.FullName = "ABCDE";
            patientTwo.Nric = "S8518538A";
            patientTwo.Event = eventTwo;
            firstRecords.Add(patientTwo);

            mockData.Add("S8518538A", firstRecords);

            return mockData[nric];
        }

        private PatientEventViewModel getMockData(string nric, string eventId)
        {
            List<PatientEventViewModel> patientEvents = getMockData(nric);

            foreach(PatientEventViewModel patientEvent in patientEvents)
            {
                if (patientEvent.EventId.Equals(eventId))
                {
                    return patientEvent;
                }
            }

            return null;
        }
    }
    
}
