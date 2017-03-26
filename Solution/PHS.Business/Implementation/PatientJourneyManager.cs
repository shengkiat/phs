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

namespace PHS.Business.Implementation
{
    public class PatientJourneyManager : BaseManager, IPatientJourneyManager, IManagerFactoryBase<IPatientJourneyManager>
    {
        public IPatientJourneyManager Create()
        {
            return new PatientJourneyManager();
        }

        public IList<PatientEvent> GetPatientEventsByNric(string icFirstDigit, string icNumber, string icLastDigit, out string message)
        {
            IList<PatientEvent> result = null;
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

        private List<PatientEvent> getMockData(string nric)
        {
            Dictionary<string, List<PatientEvent>> mockData = new Dictionary<string, List<PatientEvent>>();

            List<PatientEvent> firstRecords = new List<PatientEvent>();
            PatientEvent patientOne = new PatientEvent();

            patientOne.FullName = "ABCDE";
            patientOne.Nric = "S8518538A";
            @event eventOne = new @event();
            eventOne.ID = 100;
            eventOne.Title = "2016 - Event";
            patientOne.Event = eventOne;
            firstRecords.Add(patientOne);

            PatientEvent patientTwo = new PatientEvent();
            patientTwo.FullName = "ABCDE";
            patientTwo.Nric = "S8518538A";
            @event eventTwo = new @event();
            eventTwo.ID = 200;
            eventTwo.Title = "2015 - Event";
            patientTwo.Event = eventTwo;
            firstRecords.Add(patientTwo);

            mockData.Add("S8518538A", firstRecords);

            return mockData[nric];
        }
    }
    
}
