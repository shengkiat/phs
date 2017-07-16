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

        public ParticipantJourneyViewModel(Participant participant)
        {
            Event = participant.PHSEvents.FirstOrDefault();
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
        public string EventId { get { return Event.PHSEventID.ToString(); } }

        public string Nric { get; }
        public string FullName { get; }
        public int Age { get; }
        public string Language { get; }
        public string ContactNumber { get; }
        public string Gender { get; }

        public int SelectedModalityId { get; set; }

        public List<Form> GetModalityFormsForTabs() {
            List<Form> result = new List<Form>();

            foreach (var modality in Event.Modalities)
            {
                if (modality.ModalityID.Equals(SelectedModalityId))
                {
                    result = modality.Forms.ToList();
                }
            }
            
            return result;
        }

        public bool isSummarySelected()
        {
            bool result = false;
            foreach (var modality in Event.Modalities)
            {
                if (modality.ModalityID.Equals(SelectedModalityId))
                {
                    result = modality.Name.Equals("Summary");
                }
            }
            return result;
        }
    }
}
