using PHS.Business.Interface;
using PHS.Business.ViewModel.ParticipantJourney;
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

        public ParticipantJourneySearchViewModel RetrieveParticipantJourney(ParticipantJourneySearchViewModel psm)
        {
            if (psm == null)
            {
                throw new Exception("Parameter cannot be null");
            }

            if (string.IsNullOrEmpty(psm.Nric) || psm.PHSEventId == 0)
            {
                throw new Exception("Nric or PHSEventId cannot be null");
            }

            using (var unitOfWork = CreateUnitOfWork())
            {
                ParticipantJourneySearchViewModel result = new ParticipantJourneySearchViewModel();
                DateTime currentTime = DateTime.Now;
                result.PHSEvents = unitOfWork.Events.GetAll();
                //result.PHSEvents = unitOfWork.Events.GetAll().Where(e => e.IsActive == true && currentTime.Ticks > e.StartDT.Ticks && currentTime.Ticks < e.EndDT.Ticks);

                return result;
            }
        }
    }
}
