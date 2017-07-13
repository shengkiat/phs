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
                //result.PHSEvents = unitOfWork.Events.GetAll();
                result.PHSEvents = unitOfWork.Events.GetAllActiveEvents();

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
                    PHSEvent phsEvent = unitOfWork.Events.GetAllActiveEvents().Where(e => e.PHSEventID == psm.PHSEventId).FirstOrDefault();

                    if (phsEvent == null)
                    {
                        message = "Screening Event is not active";
                    }

                    else
                    {
                        Participant participant = unitOfWork.Participants.FindParticipant(p => p.Nric.Equals(psm.Nric) && p.PHSEvents.All(e => e.PHSEventID == psm.PHSEventId));

                        if (participant != null)
                        {
                            result = new ParticipantJourneyViewModel(participant);
                        }

                        else
                        {
                            //TODO retrieve pre-reg records
                            PreRegistration preRegistration = unitOfWork.PreRegistrations.FindPreRegistration(p => p.Nric.Equals(psm.Nric));
                            if (preRegistration == null)
                            {
                                message = "No registration record found. Do you want to register this Nric?";
                            }

                            else
                            {
                                message = "Do you want to register this Nric?";
                            }

                           
                        }
                    }
                    

                }
            }

            return result;
        }
    }
}
