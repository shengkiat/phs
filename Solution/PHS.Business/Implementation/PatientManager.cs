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

        public IList<Patient> GetPatientsByNric(string nric, out string message)
        {
            IList<Patient> result = null;

            if (string.IsNullOrEmpty(nric))
            {
                message = "Nric is empty!";
                return null;
            }



            message = string.Empty;
            return result;
        }
    }
}
