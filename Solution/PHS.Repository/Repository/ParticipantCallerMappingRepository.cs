using PHS.DB;
using System.Linq;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace PHS.Repository.Repository
{
    public class ParticipantCallerMappingRepository : Repository<ParticipantCallerMapping>, IParticipantCallerMappingRepository
    {
        public ParticipantCallerMappingRepository(DbContext datacontext) : base(datacontext)
        {
        }

        public ParticipantCallerMapping GetParticipantCallerMapping(int id)
        {
            return dbContext.Set<ParticipantCallerMapping>().Where(p => p.ParticipantCallerMappingID == id).Include(p => p.Participant).FirstOrDefault();
        }

        public IEnumerable<ParticipantCallerMapping> GetAllParticipantCallerMappingByFollowUpGroup(int followupgroupid)
        {
            return dbContext.Set<ParticipantCallerMapping>().Where(p => p.FollowUpGroupID == followupgroupid).ToList();
        }
    }
}
