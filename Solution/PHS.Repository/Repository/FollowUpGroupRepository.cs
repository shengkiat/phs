using PHS.DB;
using System.Linq;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace PHS.Repository.Repository
{
    public class FollowUpGroupRepository : Repository<FollowUpGroup>, IFollowUpGroupRepository
    {
        public FollowUpGroupRepository(DbContext datacontext) : base(datacontext)
        {
        }
        public FollowUpGroup GetFollowUpGroup(int id)
        {
            return dbContext.Set<FollowUpGroup>().Where(u => u.FollowUpGroupID == id).Include(f =>f.ParticipantCallerMappings.Select(pcm => pcm.Participant)).FirstOrDefault();
        }
    }
}


