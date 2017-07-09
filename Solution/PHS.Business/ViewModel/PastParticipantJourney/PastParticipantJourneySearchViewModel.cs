using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;

namespace PHS.Business.ViewModel.PastParticipantJourney
{
    public class PastParticipantJourneySearchViewModel
    {
        public string Nric { get; set; }
        public IList<PatientEventViewModel> PatientEvents { get; set; }
    }
}
