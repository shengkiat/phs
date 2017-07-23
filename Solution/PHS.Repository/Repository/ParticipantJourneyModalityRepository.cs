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
    }
}
