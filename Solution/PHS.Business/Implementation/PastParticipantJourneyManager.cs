using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using PHS.Business.Common;
using PHS.Common;
using PHS.Business.ViewModel.PastParticipantJourney;
using PHS.Business.ViewModel.ParticipantJourney;
using static PHS.Common.Constants;

namespace PHS.Business.Implementation
{
    public class PastParticipantJourneyManager : BaseParticipantJourneyManager, IPastParticipantJourneyManager, IManagerFactoryBase<IPastParticipantJourneyManager>
    {
        public IPastParticipantJourneyManager Create()
        {
            return new PastParticipantJourneyManager();
        }

        public IList<ParticipantJourneyViewModel> GetAllParticipantJourneyByNric(string nric, out string message)
        {
            IList<ParticipantJourneyViewModel> result = new List<ParticipantJourneyViewModel>();
            message = string.Empty;

            if (string.IsNullOrEmpty(nric))
            {
                message = "Nric cannot be null";
            }

            // todo: this is removed for data migration purposes. to be added in before go live
            //else if (!NricChecker.IsNRICValid(nric))
            //{
            //    message = "Invalid Nric";
            //}

            else
            {
                try
                {
                    using (var unitOfWork = CreateUnitOfWork())
                    {
                        var participant = unitOfWork.Participants.FindParticipants(u => u.Nric.Equals(nric, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                        if (participant != null)
                        {
                            DateTime currentTime = DateTime.Now;
                            foreach (PHSEvent phsEvent in participant.PHSEvents)
                            {
                                if (currentTime.Ticks > phsEvent.EndDT.Ticks)
                                {
                                    result.Add(new ParticipantJourneyViewModel(participant, phsEvent.PHSEventID));
                                }
                                
                            }

                            if (result.Count == 0)
                            {
                                message = "No result found";
                            }
                            
                            return result;
                        }
                        else
                        {
                            message = "Participant not found!";
                            return result;
                        }
                    }
                }

                catch (Exception ex)
                {
                    ExceptionLog(ex);
                    message = Constants.OperationFailedDuringRetrievingValue("GetAllParticipantJourneyByNric");
                    return null;
                }
            }

            return result;
        }

        public ParticipantJourneyViewModel RetrievePastParticipantJourney(ParticipantJourneySearchViewModel psm, out string message)
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

            // todo: this is removed for data migration purposes. to be added in before go live
            //else if (!NricChecker.IsNRICValid(psm.Nric))
            //{
            //    message = "Invalid Nric";
            //}

            else
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    Participant participant = unitOfWork.Participants.FindParticipant(psm.Nric, psm.PHSEventId);

                    if (participant != null)
                    {
                        result = new ParticipantJourneyViewModel(participant, psm.PHSEventId);
                    }

                    else
                    {
                        message = "No result found";
                    }
                    
                }
            }

            return result;
        }


        
    }
    
}
