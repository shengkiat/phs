using PHS.DB;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PHS.Repository.Interface
{
    public interface IEventPatientRepository : IRepository<EventPatient>
    {
        IEnumerable<EventPatient> FindEventPatient(Expression<Func<EventPatient, bool>> predicate);
    }
}
