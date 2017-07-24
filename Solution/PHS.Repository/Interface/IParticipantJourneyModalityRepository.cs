using PHS.DB;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Repository.Interface
{
    public interface IParticipantJourneyModalityRepository : IRepository<ParticipantJourneyModality>
    {
        ParticipantJourneyModality GetParticipantJourneyModality(string nric, int phsEventId, int formId);
        void UpdateParticipantJourneyModalityEntryId(ParticipantJourneyModality participantJourneyModality, Guid entryId);

        IEnumerable<ParticipantJourneyModality> GetParticipantJourneyModalityByParticipantEvent(string nric, int phsEventId);
    }
}
