using PHS.DB;
using PHS.Repository.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PHS.Repository.Interface;

namespace PHS.Repository.Repository
{
    public class ParticipantJourneyModalityRepository : Repository<ParticipantJourneyModality>, IParticipantJourneyModalityRepository
    {
        public ParticipantJourneyModalityRepository(DbContext context) : base(context)
        {
            
        }

        public ParticipantJourneyModality GetParticipantJourneyModality(string nric, int phsEventId, int formId, int modalityId)
        {
            return Find(p => p.PHSEventID == phsEventId && p.FormID == formId && p.ModalityID == modalityId && p.Participant.Nric.Equals(nric)).FirstOrDefault();
        }

        public void UpdateParticipantJourneyModality(ParticipantJourneyModality participantJourneyModality, int templateId, Guid entryId)
        {
            if (participantJourneyModality == null)
            {
                throw new Exception("Cannot update entryId when a participantJourneyModality is null");
            }

            dbContext.Entry(participantJourneyModality).State = EntityState.Modified;
            participantJourneyModality.EntryId = entryId;
            participantJourneyModality.TemplateID = templateId;
        }

        public IEnumerable<ParticipantJourneyModality> GetParticipantJourneyModalityByParticipantEvent(string nric, int phsEventId)
        {
            return Find(p => p.PHSEventID == phsEventId && p.Participant.Nric.Equals(nric));
        }

        public IEnumerable<ParticipantJourneyModality> GetParticipantJourneyModalityByFormIdAndEntryId(int formId, Guid entryId)
        {
            return Find(p => p.FormID == formId && p.EntryId.Equals(entryId));
        }

    }
}
