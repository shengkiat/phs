using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.ParticipantJourney
{
    public class ParticipantJourneyFormViewModel
    {

        public ParticipantJourneyFormViewModel(Participant participant, int PHSEventId)
        {
            Event = participant.PHSEvents.Where(e => e.PHSEventID == PHSEventId).FirstOrDefault();
        }

        private PHSEvent Event { get; }

        public int SelectedModalityId { get; set; }

        public List<Form> GetModalityFormsForTabs()
        {
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
