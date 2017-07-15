using PHS.DB;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace PHS.Repository.Repository
{
    public class ParticipantRepository : Repository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Participant> FindParticipants(Expression<Func<Participant, bool>> predicate)
        {
            return dbContext.Set<Participant>().Where(predicate).Include(x => x.PHSEvents);
        }

        public Participant FindParticipant(string nric, int phsEventId)
        {
            return find(p => p.Nric.Equals(nric) && p.PHSEvents.Any(e => e.PHSEventID == phsEventId));
        }

        public Participant FindParticipant(string nric)
        {
            return find(p => p.Nric.Equals(nric));
        }

        public void AddParticipantWithPHSEvent(string nric, PHSEvent phsEvent)
        {
            Participant participant = new Participant()
            {
                Nric = nric
            };

            Add(participant);
            participant.PHSEvents.Add(phsEvent);
        }

        public void AddPHSEventToParticipant(Participant participant, PHSEvent phsEvent)
        {
            dbContext.Entry(participant).State = EntityState.Modified;
            participant.PHSEvents.Add(phsEvent);
        }

        private Participant find(Expression<Func<Participant, bool>> predicate)
        {
            return dbContext.Set<Participant>().Where(predicate).Include(p => p.PHSEvents.Select(e => e.Modalities.Select(y => y.Forms))).FirstOrDefault();
        }
    }
}
