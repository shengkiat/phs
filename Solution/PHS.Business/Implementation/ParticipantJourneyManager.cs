using PHS.Business.Interface;
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

        public IEnumerable<PHSEvent> FindActiveEvents()
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                DateTime currentTime = DateTime.Now;
                return unitOfWork.Events.GetAll().Where(e => e.IsActive == true && currentTime.Ticks > e.StartDT.Ticks && currentTime.Ticks < e.EndDT.Ticks);
            }
        }
    }
}
