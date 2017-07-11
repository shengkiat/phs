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

        public Participant FindParticipant(Expression<Func<Participant, bool>> predicate)
        {
            return dbContext.Set<Participant>().Where(predicate).Include(x => x.PHSEvents.FirstOrDefault().Modalities.Select(y => y.Forms)).FirstOrDefault();
        }
    }
}
