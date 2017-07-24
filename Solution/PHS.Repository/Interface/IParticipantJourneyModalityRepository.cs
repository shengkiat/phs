﻿using PHS.DB;
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
        ParticipantJourneyModality GetParticipantJourneyModality(string nric, int phsEventId, int formId, int modalityId);
        void UpdateParticipantJourneyModality(ParticipantJourneyModality participantJourneyModality, int templateId, Guid entryId);

        IEnumerable<ParticipantJourneyModality> GetParticipantJourneyModalityByParticipantEvent(string nric, int phsEventId);
    }
}
