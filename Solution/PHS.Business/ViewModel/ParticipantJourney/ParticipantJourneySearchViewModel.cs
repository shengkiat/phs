using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;

namespace PHS.Business.ViewModel.ParticipantJourney
{
    public class ParticipantJourneySearchViewModel
    {
        public string Nric { get; set; }
        public IEnumerable<PHSEvent> PHSEvents { get; set; }
    }
}
