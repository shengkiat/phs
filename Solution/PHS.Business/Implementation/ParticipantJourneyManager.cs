using PHS.Business.Interface;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.Common;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Implementation
{
    public class ParticipantJourneyManager : BaseManager, IParticipantJourneyManager, IManagerFactoryBase<IParticipantJourneyManager>
    {
        public IParticipantJourneyManager Create()
        {
            return new ParticipantJourneyManager();
        }

        public ParticipantJourneySearchViewModel RetrieveActiveScreeningEvent()
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                ParticipantJourneySearchViewModel result = new ParticipantJourneySearchViewModel();
                DateTime currentTime = DateTime.Now;
                result.PHSEvents = unitOfWork.Events.GetAll();
                //result.PHSEvents = unitOfWork.Events.GetAll().Where(e => e.IsActive == true && currentTime.Ticks > e.StartDT.Ticks && currentTime.Ticks < e.EndDT.Ticks);

                return result;
            }
        }

        public ParticipantJourneyViewModel RetrieveParticipantJourney(ParticipantJourneySearchViewModel psm, out string message)
        {
            message = string.Empty;
            ParticipantJourneyViewModel result = null;

            if (psm == null)
            {
                message = "Parameter cannot be null";
            }

            else if (string.IsNullOrEmpty(psm.Nric) || psm.PHSEventId == 0)
            {
                message = "Nric or PHSEventId cannot be null";
            }

            else if (!NricChecker.IsNRICValid(psm.Nric))
            {
                message = "Invalid Nric";
            }

            else
            {
                using (var unitOfWork = CreateUnitOfWork())
                {

                    Participant participant = unitOfWork.Participants.FindParticipant(e => e.Nric.Equals(psm.Nric));

                    if (participant != null)
                    {
                        result = new ParticipantJourneyViewModel(participant);
                    }

                    else
                    {
                        message = "No registration record found. Do you want to register this Nric?";
                    }

                }
            }

            return result;
        }
    }
}
