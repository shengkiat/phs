using PHS.Business.Interface;
using PHS.Business.ViewModel;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.Common;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static PHS.Common.Constants;

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

        public ParticipantJourneyViewModel RetrieveParticipantJourney(ParticipantJourneySearchViewModel psm, out string message, out MessageType messageType)
        {
            message = string.Empty;
            messageType = MessageType.ERROR;

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
                        Participant participant = unitOfWork.Participants.FindParticipant(psm.Nric, psm.PHSEventId);

                        if (participant != null)
                        {
                            result = new ParticipantJourneyViewModel(participant);
                        }

                        else
                        {

                            messageType = MessageType.PROMPT;

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

        public string RegisterParticipant(ParticipantJourneySearchViewModel psm)
        {
            string result = null;

            if (psm == null)
            {
                return "Parameter cannot be null";
            }

            else if (string.IsNullOrEmpty(psm.Nric) || psm.PHSEventId == 0)
            {
                return "Nric or PHSEventId cannot be null";
            }

            else if (!NricChecker.IsNRICValid(psm.Nric))
            {
                return "Invalid Nric";
            }

            else
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    PHSEvent phsEvent = unitOfWork.Events.GetAllActiveEvents().Where(e => e.PHSEventID == psm.PHSEventId).FirstOrDefault();

                    if (phsEvent == null)
                    {
                        return "Screening Event is not active";
                    }

                    else
                    {
                        Participant participant = unitOfWork.Participants.FindParticipant(psm.Nric);

                        if (participant != null)
                        {
                            if (participant.PHSEvents.All(e => e.PHSEventID == psm.PHSEventId))
                            {
                                return "Invalid register participant";
                            }
                        }

                        else
                        {
                            participant = new Participant()
                            {
                                Nric = psm.Nric
                            };
                        }

                        using (TransactionScope scope = new TransactionScope())
                        {
                            participant.PHSEvents.Add(phsEvent);

                            unitOfWork.Complete();
                            scope.Complete();
                        }
                    }
                }
            }

            return result;
        }
    }
}
