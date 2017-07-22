using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.ParticipantJourney
{
    public class ParticipantJourneyViewModel
    {

        public ParticipantJourneyViewModel(Participant participant, int PHSEventId)
        {
            Event = participant.PHSEvents.Where(e=> e.PHSEventID == PHSEventId).FirstOrDefault();
            Participant = participant;

            FullName = participant.FullName;
            Nric = participant.Nric;
            Language = participant.Language;
            ContactNumber = participant.ContactNumber;
            Gender = participant.Gender;

            if (participant.DateOfBirth != null)
            {
                int now = int.Parse(Event.StartDT.ToString("yyyy"));
                int dob = int.Parse(participant.DateOfBirth.Value.ToString("yyyy"));
                Age = (now - dob);
            }
            else
            {
                Age = 0;
            }
        }

        public PHSEvent Event { get; }
        public Participant Participant { get; }
        public string EventId { get { return Event.PHSEventID.ToString(); } }

        public string Nric { get; }
        public string FullName { get; }
        public int Age { get; }
        public string Language { get; }
        public string ContactNumber { get; }
        public string Gender { get; }

        
    }
}
