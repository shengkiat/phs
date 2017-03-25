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
    public class PatientManager : BaseManager, IPatientManager, IManagerFactoryBase<IPatientManager>
    {
        public IPatientManager Create()
        {
            return new PatientManager();
        }

        public IList<Patient> GetPatientsByNric(string icFirstDigit, string icNumber, string icLastDigit, out string message)
        {
            IList<Patient> result = null;
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

        private List<Patient> getMockData(string nric)
        {
            Dictionary<string, List<Patient>> mockData = new Dictionary<string, List<Patient>>();

            List<Patient> firstRecords = new List<Patient>();
            Patient patientOne = new Patient();
            patientOne.FullName = "ABCDE";
            patientOne.Nric = "S8518538A";
            patientOne.Event = "2016 - Event";
            firstRecords.Add(patientOne);

            Patient patientTwo = new Patient();
            patientTwo.FullName = "ABCDE";
            patientTwo.Nric = "S8518538A";
            patientTwo.Event = "2015 - Event";
            firstRecords.Add(patientTwo);

            mockData.Add("S8518538A", firstRecords);

            return mockData[nric];
        }
    }
    
}
