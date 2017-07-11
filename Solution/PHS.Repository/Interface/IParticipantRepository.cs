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
        Participant FindParticipant(Expression<Func<Participant, bool>> predicate);
    }
}
