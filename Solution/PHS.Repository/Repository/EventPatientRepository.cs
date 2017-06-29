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
    public class EventPatientRepository : Repository<EventPatient>, IEventPatientRepository
    {
        public EventPatientRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<EventPatient> FindEventPatients(Expression<Func<EventPatient, bool>> predicate)
        {
            return dbContext.Set<EventPatient>().Where(predicate).Include(x => x.PHSEvent);
        }

        public EventPatient FindEventPatient(Expression<Func<EventPatient, bool>> predicate)
        {
            return dbContext.Set<EventPatient>().Where(predicate).Include(x => x.PHSEvent.Modalities.Select(y => y.Forms)).FirstOrDefault();
        }
    }
}
