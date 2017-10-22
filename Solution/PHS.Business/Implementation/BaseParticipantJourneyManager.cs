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
        public BaseParticipantJourneyManager(PHSUser loginUser) : base(loginUser)
        {
        }

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
                    IList<ParticipantJourneyModality> matchedPjms = new List<ParticipantJourneyModality>();
                    foreach (ParticipantJourneyModality pjm in ptJourneyModalityItems)
                    {
                        if (modality.ModalityID == pjm.Modality.ModalityID && pjm.PHSEvent.Equals(phsEvent))
                        {
                            matchedPjms.Add(pjm);
                            modality.IsActive = true;
                        }
                    }
                    if (modality.IsVisible && modality.Status != "Public")
                    {
                        pjmCircles.Add(copyToPJMCVM(pjvm, modality, matchedPjms));
                    }
                }
                return pjmCircles;
            }
        }

        private ParticipantJourneyModalityCircleViewModel copyToPJMCVM(ParticipantJourneyViewModel pjvm, Modality modality, IList<ParticipantJourneyModality> matchedPjms)
        {
            ParticipantJourneyModalityCircleViewModel pjmcvm = new ParticipantJourneyModalityCircleViewModel(pjvm, modality, matchedPjms, GetLoginUserRole());

            return pjmcvm;
        }
    }
}
