using PHS.Business.ViewModel.ParticipantJourney;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Implementation
{
    public class BaseParticipantJourneyManager : BaseManager
    {
        public List<ParticipantJourneyModalityCircleViewModel> GetParticipantMegaSortingStation(ParticipantJourneySearchViewModel psm)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {

                PHSEvent phsEvent = unitOfWork.Events.Get(psm.PHSEventId);
                Participant participant = unitOfWork.Participants.FindParticipant(psm.Nric, phsEvent.PHSEventID);

                IEnumerable<ParticipantJourneyModality> ptJourneyModalityItems = unitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModalityByParticipantEvent(psm.Nric, phsEvent.PHSEventID);

                ParticipantJourneyViewModel pjvm = new ParticipantJourneyViewModel(participant, psm.PHSEventId);

                List<ParticipantJourneyModalityCircleViewModel> pjmCircles = new List<ParticipantJourneyModalityCircleViewModel>();

                foreach (Modality modality in phsEvent.Modalities)
                {
                    foreach (ParticipantJourneyModality pjm in ptJourneyModalityItems)
                    {
                        if (modality.ModalityID == pjm.Modality.ModalityID && pjm.PHSEvent.Equals(phsEvent))
                        {
                            modality.IsActive = true;
                        }
                    }
                    if (modality.Status != "Public")
                    {
                        pjmCircles.Add(copyToPJMCVM(pjvm, modality));
                    }
                }
                return pjmCircles;
            }
        }

        private ParticipantJourneyModalityCircleViewModel copyToPJMCVM(ParticipantJourneyViewModel pjvm, Modality modality)
        {
            ParticipantJourneyModalityCircleViewModel pjmcvm = new ParticipantJourneyModalityCircleViewModel(pjvm, modality);

            return pjmcvm;
        }
    }
}
