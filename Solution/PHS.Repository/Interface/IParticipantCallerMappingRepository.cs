using PHS.DB;
using PHS.Repository.Interface.Core;
using System.Collections.Generic;

namespace PHS.Repository.Interface
{
    public interface IParticipantCallerMappingRepository : IRepository<ParticipantCallerMapping>
    {
        ParticipantCallerMapping GetParticipantCallerMapping(int id);
        IEnumerable<ParticipantCallerMapping> GetAllParticipantCallerMappingByFollowUpGroup(int followupgroupid);
    }

}