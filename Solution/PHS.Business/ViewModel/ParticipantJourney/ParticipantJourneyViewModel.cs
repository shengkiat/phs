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
            Address = participant.Address;
            PostalCode = participant.PostalCode;
            Race = participant.Race;
            Citizenship = participant.Citizenship;        
            Language = participant.Language;
            HomeNumber = participant.HomeNumber;
            MobileNumber = participant.MobileNumber;
            Gender = participant.Gender;

            if (participant.DateOfBirth != null)
            {
                DateOfBirth = participant.DateOfBirth.Value.ToString("dd MMM yyyy");
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
        public string Address { get; }
        public string PostalCode { get;  }
        public string Race { get; }
        public string Citizenship { get; }
        public string Nric { get; }
        public string FullName { get; }
        public int Age { get; }

        public string DateOfBirth { get; }

        public string Language { get; }
        public string HomeNumber { get; }
        public string MobileNumber { get; }
        public string Gender { get; }

        
    }
}
