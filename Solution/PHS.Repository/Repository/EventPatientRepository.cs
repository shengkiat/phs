using PHS.DB;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;

namespace PHS.Repository.Repository
{
    public class EventPatientRepository : Repository<EventPatient>, IEventPatientRepository
    {
        public EventPatientRepository(DbContext context) : base(context)
        {
        }
    }
}
