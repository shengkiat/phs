using PHS.DB;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PHS.Repository.Interface
{
    public interface IParticipantRepository : IRepository<Participant>
    {
        IEnumerable<Participant> FindParticipants(Expression<Func<Participant, bool>> predicate);
        IEnumerable<Participant> SearchParticipants(string searchstring);
        Participant FindParticipant(string nric);
        Participant FindParticipant(string nric, int phsEventId);
        Participant FindParticipant(int participantId);

        void AddParticipantWithPHSEvent(Participant participant, PHSEvent phsEvent);

        void AddPHSEventToParticipant(Participant participant, PHSEvent phsEvent);
    }
}
